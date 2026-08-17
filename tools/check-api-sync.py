#!/usr/bin/env python3
"""
Samsara API Sync Checker
========================
Fetches the latest Samsara OpenAPI spec and compares it to a cached baseline,
producing a diff report of new/removed/changed endpoints.

Usage:
    python3 tools/check-api-sync.py [--spec-url URL] [--baseline PATH] [--output PATH]

Options:
    --spec-url URL      URL to the OpenAPI spec JSON (default: Samsara dev portal)
    --baseline PATH     Path to the cached baseline spec (default: .github/cache/samsara-api-baseline.json)
    --output PATH       Path to write the diff report (default: docs/api-sync/diff-report.md)
    --update-baseline   After diffing, save the new spec as the new baseline
    --fail-on-diff      Exit with code 1 if any differences are found (for CI)
    --help              Show this help

Exit codes:
    0 — No differences found (or --fail-on-diff not set)
    1 — Differences found (only when --fail-on-diff is set)
    2 — Error (network failure, parse error, etc.)
"""

import argparse
import hashlib
import json
import os
import sys
import urllib.request
from collections import defaultdict
from datetime import datetime, timezone
from pathlib import Path

SPEC_URL = "https://developers.samsara.com/openapi/samsara-api.json"
SCRIPT_DIR = Path(__file__).parent
REPO_ROOT = SCRIPT_DIR.parent
DEFAULT_BASELINE = REPO_ROOT / ".github" / "cache" / "samsara-api-baseline.json"
DEFAULT_OUTPUT = REPO_ROOT / "docs" / "api-sync" / "diff-report.md"


def fetch_spec(url: str) -> dict:
    """Download and parse the OpenAPI spec from a URL."""
    print(f"Fetching spec from {url} ...", flush=True)
    try:
        req = urllib.request.Request(
            url,
            headers={"User-Agent": "SamsaraApiSyncChecker/1.0 (github.com/TheEightBot/SamsaraDotnet)"},
        )
        with urllib.request.urlopen(req, timeout=60) as resp:
            data = json.loads(resp.read().decode("utf-8"))
    except Exception as exc:
        print(f"ERROR: Failed to fetch spec: {exc}", file=sys.stderr)
        sys.exit(2)
    return data


def load_spec(path: Path) -> dict | None:
    """Load a spec from disk, or return None if not found."""
    if not path.exists():
        return None
    try:
        with open(path) as f:
            return json.load(f)
    except Exception as exc:
        print(f"WARNING: Could not load baseline {path}: {exc}", file=sys.stderr)
        return None


def save_spec(spec: dict, path: Path) -> None:
    """Save a spec to disk."""
    path.parent.mkdir(parents=True, exist_ok=True)
    with open(path, "w") as f:
        json.dump(spec, f, indent=2)
    print(f"Saved spec to {path}")


def extract_endpoints(spec: dict) -> dict[str, dict]:
    """
    Extract all endpoints from an OpenAPI spec.
    Returns a dict keyed by "<METHOD> <path>" with endpoint details.
    """
    endpoints = {}
    for path, methods in spec.get("paths", {}).items():
        for method, details in methods.items():
            if method not in ("get", "post", "put", "patch", "delete"):
                continue
            key = f"{method.upper()} {path}"
            params = sorted(
                [
                    {
                        "name": p.get("name"),
                        "in": p.get("in"),
                        "required": p.get("required", False),
                    }
                    for p in details.get("parameters", [])
                ],
                key=lambda x: x["name"] or "",
            )
            endpoints[key] = {
                "operationId": details.get("operationId", ""),
                "summary": details.get("summary", ""),
                "tags": details.get("tags", []),
                "deprecated": details.get("deprecated", False),
                "hasBody": details.get("requestBody") is not None,
                "parameters": params,
            }
    return endpoints


def diff_endpoints(old: dict, new: dict) -> dict:
    """
    Compare old and new endpoint maps.
    Returns a dict with keys: added, removed, changed, deprecated_added, deprecated_removed.
    """
    old_keys = set(old.keys())
    new_keys = set(new.keys())

    added = {k: new[k] for k in new_keys - old_keys}
    removed = {k: old[k] for k in old_keys - new_keys}
    changed = {}
    deprecated_added = {}
    deprecated_removed = {}
    summary_only = {}

    for k in old_keys & new_keys:
        o, n = old[k], new[k]

        # Check for deprecation status change
        if not o["deprecated"] and n["deprecated"]:
            deprecated_added[k] = n
        elif o["deprecated"] and not n["deprecated"]:
            deprecated_removed[k] = n

        # Check for meaningful changes
        # `summary` is prose, not contract. Samsara rewords descriptions constantly
        # without touching behaviour, so a summary edit must NOT read as structural
        # drift — it is tracked separately and classified as cosmetic.
        if o["summary"] != n["summary"]:
            summary_only[k] = {
                "old": o["summary"],
                "new": n["summary"],
                "endpoint": n,
            }

        changes = []
        if o["operationId"] != n["operationId"]:
            changes.append(f"operationId: `{o['operationId']}` → `{n['operationId']}`")
        if o["hasBody"] != n["hasBody"]:
            changes.append(f"requestBody: `{o['hasBody']}` → `{n['hasBody']}`")

        old_params = {p["name"]: p for p in o["parameters"]}
        new_params = {p["name"]: p for p in n["parameters"]}
        for pname in set(new_params) - set(old_params):
            changes.append(f"new param: `{pname}` (in={new_params[pname]['in']}, required={new_params[pname]['required']})")
        for pname in set(old_params) - set(new_params):
            changes.append(f"removed param: `{pname}`")
        for pname in set(old_params) & set(new_params):
            op, np_ = old_params[pname], new_params[pname]
            if op["required"] != np_["required"]:
                changes.append(f"param `{pname}` required: `{op['required']}` → `{np_['required']}`")
            if op["in"] != np_["in"]:
                changes.append(f"param `{pname}` location: `{op['in']}` → `{np_['in']}`")

        if changes:
            changed[k] = {"details": changes, "endpoint": n}

    return {
        "added": added,
        "removed": removed,
        "changed": changed,
        "deprecated_added": deprecated_added,
        "deprecated_removed": deprecated_removed,
        "summary_only": summary_only,
    }


def _schema_fingerprint(schema: dict) -> dict:
    """Shallow property-name + required-set fingerprint for a single schema."""
    props = schema.get("properties")
    return {
        "props": set(props.keys()) if isinstance(props, dict) else set(),
        "required": set(schema.get("required", []) or []),
    }


def diff_schemas(old: dict, new: dict) -> dict:
    """Compare component schemas between two specs.

    Reports schemas added/removed by name, AND — for schemas present in both —
    property additions/removals and required-set changes. The latter is the
    model-drift signal that a name-only diff misses: Samsara frequently adds a
    field to an existing schema (or flips its required-ness) WITHOUT bumping
    info.version, which silently rots the hand-written SDK records.
    """
    old_s = old.get("components", {}).get("schemas", {})
    new_s = new.get("components", {}).get("schemas", {})
    old_keys, new_keys = set(old_s), set(new_s)

    changed: dict[str, dict] = {}
    for name in old_keys & new_keys:
        o = _schema_fingerprint(old_s[name])
        n = _schema_fingerprint(new_s[name])
        delta = {
            "added_props": sorted(n["props"] - o["props"]),
            "removed_props": sorted(o["props"] - n["props"]),
            "added_required": sorted(n["required"] - o["required"]),
            "removed_required": sorted(o["required"] - n["required"]),
        }
        if any(delta.values()):
            changed[name] = delta

    return {
        "added": sorted(new_keys - old_keys),
        "removed": sorted(old_keys - new_keys),
        "changed": changed,
    }


def content_fingerprint(spec: dict) -> dict:
    """Counts + a short content hash of the parts the SDK depends on.

    Lets a reviewer see at a glance that the spec content moved even when
    info.version is frozen (Samsara mutates the 2025-10-23 spec in place).
    """
    paths = spec.get("paths", {})
    ops = sum(
        1
        for methods in paths.values()
        for m in methods
        if m in ("get", "post", "put", "patch", "delete")
    )
    schemas = spec.get("components", {}).get("schemas", {})
    canonical = json.dumps({"paths": paths, "schemas": schemas}, sort_keys=True)
    return {
        "paths": len(paths),
        "ops": ops,
        "schemas": len(schemas),
        "hash": hashlib.sha256(canonical.encode("utf-8")).hexdigest()[:12],
    }


def classify(endpoint_diff: dict, schema_diff: dict, old_fp: dict, new_fp: dict) -> str:
    """Classify drift as 'none', 'cosmetic', or 'structural'.

    structural = anything that can change how the SDK must call the API or shape
                 its records: an operation added/removed/changed, a deprecation
                 flip, a schema added/removed, or a property/required delta on a
                 schema present in both specs.
    cosmetic   = the content hash moved but nothing structural did. In practice
                 this is summary/description/example churn, which Samsara ships
                 continuously under the frozen 2025-10-23 info.version.
    none       = identical content.

    The distinction is what lets the daily workflow absorb cosmetic churn
    automatically while never auto-absorbing a real contract change.
    """
    structural = (
        endpoint_diff["added"]
        or endpoint_diff["removed"]
        or endpoint_diff["changed"]
        or endpoint_diff["deprecated_added"]
        or endpoint_diff["deprecated_removed"]
        or schema_diff["added"]
        or schema_diff["removed"]
        or schema_diff["changed"]
    )
    if structural:
        return "structural"
    if old_fp["hash"] != new_fp["hash"]:
        return "cosmetic"
    return "none"


def build_summary(
    spec_source: str,
    old_version: str,
    new_version: str,
    endpoint_diff: dict,
    schema_diff: dict,
    old_fp: dict,
    new_fp: dict,
) -> dict:
    """Machine-readable drift summary consumed by render-drift-report.py."""

    def ep(key: str, data: dict) -> dict:
        return {
            "key": key,
            "operationId": data.get("operationId", ""),
            "tags": data.get("tags", []),
            "summary": data.get("summary", ""),
            "deprecated": data.get("deprecated", False),
        }

    return {
        "generated": datetime.now(timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ"),
        "spec_source": spec_source,
        "old_version": old_version,
        "new_version": new_version,
        "old_fingerprint": old_fp,
        "new_fingerprint": new_fp,
        "classification": classify(endpoint_diff, schema_diff, old_fp, new_fp),
        "counts": {
            "added": len(endpoint_diff["added"]),
            "removed": len(endpoint_diff["removed"]),
            "changed": len(endpoint_diff["changed"]),
            "deprecated_added": len(endpoint_diff["deprecated_added"]),
            "deprecated_removed": len(endpoint_diff["deprecated_removed"]),
            "summary_only": len(endpoint_diff["summary_only"]),
            "schemas_added": len(schema_diff["added"]),
            "schemas_removed": len(schema_diff["removed"]),
            "schemas_changed": len(schema_diff["changed"]),
        },
        "endpoints": {
            "added": sorted(
                (ep(k, v) for k, v in endpoint_diff["added"].items()),
                key=lambda e: (e["tags"][0] if e["tags"] else "", e["key"]),
            ),
            "removed": sorted(
                (ep(k, v) for k, v in endpoint_diff["removed"].items()),
                key=lambda e: e["key"],
            ),
            "changed": sorted(
                (
                    {**ep(k, v["endpoint"]), "details": v["details"]}
                    for k, v in endpoint_diff["changed"].items()
                ),
                key=lambda e: e["key"],
            ),
            "deprecated_added": sorted(endpoint_diff["deprecated_added"]),
            "deprecated_removed": sorted(endpoint_diff["deprecated_removed"]),
            "summary_only": sorted(endpoint_diff["summary_only"]),
        },
        "schemas": {
            "added": schema_diff["added"],
            "removed": schema_diff["removed"],
            "changed": schema_diff["changed"],
        },
    }


def format_report(
    old_version: str,
    new_version: str,
    endpoint_diff: dict,
    schema_diff: dict,
    timestamp: str,
    old_fp: dict | None = None,
    new_fp: dict | None = None,
) -> str:
    """Render a Markdown diff report."""
    added = endpoint_diff["added"]
    removed = endpoint_diff["removed"]
    changed = endpoint_diff["changed"]
    dep_added = endpoint_diff["deprecated_added"]
    dep_removed = endpoint_diff["deprecated_removed"]
    schema_changed = schema_diff.get("changed", {})

    total_changes = len(added) + len(removed) + len(changed) + len(dep_added)
    schema_changes = len(schema_diff["added"]) + len(schema_diff["removed"]) + len(schema_changed)

    lines = [
        "# Samsara API Sync Diff Report",
        "",
        f"> **Generated**: {timestamp}  ",
        f"> **Old version**: `{old_version}`  ",
        f"> **New version**: `{new_version}`  ",
        f"> **Endpoint changes**: {total_changes}  ",
        f"> **Schema changes**: {schema_changes}  ",
    ]
    if old_fp and new_fp:
        moved = old_fp["hash"] != new_fp["hash"]
        content = (
            f"> **Content**: {old_fp['ops']}→{new_fp['ops']} ops, "
            f"{old_fp['schemas']}→{new_fp['schemas']} schemas, "
            f"hash `{old_fp['hash']}`→`{new_fp['hash']}`  "
        )
        if old_version == new_version and moved:
            content += "\n> ⚠️ **Spec content changed under the same `info.version`** — model drift is possible.  "
        lines.append(content)
    lines.append("")

    if total_changes == 0 and schema_changes == 0:
        lines += [
            "## ✅ No Changes Detected",
            "",
            "The Samsara API spec is identical to the baseline. No action required.",
            "",
        ]
        return "\n".join(lines)

    lines += ["---", ""]

    # New endpoints
    if added:
        lines += [f"## 🆕 New Endpoints ({len(added)})", ""]
        by_tag: dict[str, list] = defaultdict(list)
        for key, ep in sorted(added.items()):
            for tag in ep["tags"] or ["Untagged"]:
                by_tag[tag].append((key, ep))
        for tag in sorted(by_tag.keys()):
            lines.append(f"### {tag}")
            for key, ep in by_tag[tag]:
                lines.append(f"- `{key}` — {ep['summary']} *(operationId: `{ep['operationId']}`)*")
            lines.append("")

    # Removed endpoints
    if removed:
        lines += [f"## 🗑️ Removed Endpoints ({len(removed)})", ""]
        for key in sorted(removed.keys()):
            ep = removed[key]
            lines.append(f"- `{key}` — {ep['summary']}")
        lines.append("")

    # Changed endpoints
    if changed:
        lines += [f"## 🔄 Changed Endpoints ({len(changed)})", ""]
        for key in sorted(changed.keys()):
            item = changed[key]
            lines.append(f"### `{key}`")
            for detail in item["details"]:
                lines.append(f"- {detail}")
            lines.append("")

    # Newly deprecated
    if dep_added:
        lines += [f"## ⚠️ Newly Deprecated ({len(dep_added)})", ""]
        for key in sorted(dep_added.keys()):
            ep = dep_added[key]
            lines.append(f"- `{key}` — {ep['summary']}")
        lines.append("")

    # Un-deprecated
    if dep_removed:
        lines += [f"## ✨ Un-deprecated ({len(dep_removed)})", ""]
        for key in sorted(dep_removed.keys()):
            ep = dep_removed[key]
            lines.append(f"- `{key}` — {ep['summary']}")
        lines.append("")

    # Schema changes
    if schema_diff["added"] or schema_diff["removed"] or schema_changed:
        lines += [f"## 📦 Schema Changes ({schema_changes})", ""]
        if schema_diff["added"]:
            lines.append(f"**Added schemas** ({len(schema_diff['added'])}):")
            for s in schema_diff["added"][:50]:
                lines.append(f"- `{s}`")
            if len(schema_diff["added"]) > 50:
                lines.append(f"- *(and {len(schema_diff['added']) - 50} more...)*")
            lines.append("")
        if schema_diff["removed"]:
            lines.append(f"**Removed schemas** ({len(schema_diff['removed'])}):")
            for s in schema_diff["removed"][:50]:
                lines.append(f"- `{s}`")
            if len(schema_diff["removed"]) > 50:
                lines.append(f"- *(and {len(schema_diff['removed']) - 50} more...)*")
            lines.append("")
        if schema_changed:
            lines.append(
                f"**Changed schemas** ({len(schema_changed)}) — property / required-set deltas "
                "on schemas present in both versions (the model-drift signal):"
            )
            for name in sorted(schema_changed)[:60]:
                d = schema_changed[name]
                parts = []
                if d["added_props"]:
                    shown = ", ".join(f"`{p}`" for p in d["added_props"][:8])
                    parts.append("+props " + shown + ("…" if len(d["added_props"]) > 8 else ""))
                if d["removed_props"]:
                    shown = ", ".join(f"`{p}`" for p in d["removed_props"][:8])
                    parts.append("−props " + shown + ("…" if len(d["removed_props"]) > 8 else ""))
                if d["added_required"]:
                    parts.append("+required " + ", ".join(f"`{p}`" for p in d["added_required"]))
                if d["removed_required"]:
                    parts.append("−required " + ", ".join(f"`{p}`" for p in d["removed_required"]))
                lines.append(f"- `{name}`: " + "; ".join(parts))
            if len(schema_changed) > 60:
                lines.append(f"- *(and {len(schema_changed) - 60} more...)*")
            lines.append("")

    lines += [
        "---",
        "",
        "## Next Steps",
        "",
        "1. Review each new endpoint and decide if it should be implemented in the SDK",
        "2. Update the relevant checklist file(s) in `docs/api-sync/`",
        "3. Implement the endpoint(s), models, and serialization context",
        "4. Update `CHANGELOG.md` with the changes",
        "5. Update the baseline: `python3 tools/check-api-sync.py --update-baseline`",
        "",
    ]

    return "\n".join(lines)


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Compare Samsara OpenAPI spec against a cached baseline."
    )
    source = parser.add_mutually_exclusive_group()
    source.add_argument("--spec-url", help=f"URL to the OpenAPI spec (default: {SPEC_URL})")
    source.add_argument(
        "--spec-file",
        type=Path,
        help="Read the spec from a local file instead of fetching it (hermetic)",
    )
    parser.add_argument(
        "--baseline",
        type=Path,
        default=DEFAULT_BASELINE,
        help="Path to the cached baseline spec",
    )
    parser.add_argument(
        "--output",
        type=Path,
        default=DEFAULT_OUTPUT,
        help="Path for the diff report output",
    )
    parser.add_argument(
        "--no-report",
        action="store_true",
        help="Do not write the Markdown report (keeps the working tree clean)",
    )
    parser.add_argument(
        "--summary-json",
        type=Path,
        help="Write a machine-readable drift summary to this path",
    )
    parser.add_argument(
        "--update-baseline",
        action="store_true",
        help="Save the fetched spec as the new baseline",
    )
    parser.add_argument(
        "--fail-on-diff",
        action="store_true",
        help="Exit with code 1 if any differences are found (including cosmetic)",
    )
    parser.add_argument(
        "--fail-on-structural",
        action="store_true",
        help="Exit with code 1 only on structural drift; cosmetic churn exits 0",
    )
    args = parser.parse_args()

    if args.spec_file:
        spec_source = str(args.spec_file)
        new_spec = load_spec(args.spec_file)
        if new_spec is None:
            print(f"ERROR: spec file not found: {args.spec_file}", file=sys.stderr)
            sys.exit(2)
    else:
        spec_source = args.spec_url or SPEC_URL
        new_spec = fetch_spec(spec_source)
    old_spec = load_spec(args.baseline)

    new_version = new_spec.get("info", {}).get("version", "unknown")

    if old_spec is None:
        print(f"No baseline found at {args.baseline}. Saving current spec as baseline.")
        save_spec(new_spec, args.baseline)
        print(f"Baseline saved. Re-run to compare against it.")
        sys.exit(0)

    old_version = old_spec.get("info", {}).get("version", "unknown")
    print(f"Comparing spec versions: {old_version} → {new_version}")

    old_endpoints = extract_endpoints(old_spec)
    new_endpoints = extract_endpoints(new_spec)
    endpoint_diff = diff_endpoints(old_endpoints, new_endpoints)
    schema_diff = diff_schemas(old_spec, new_spec)
    old_fp = content_fingerprint(old_spec)
    new_fp = content_fingerprint(new_spec)

    total_ep_changes = (
        len(endpoint_diff["added"])
        + len(endpoint_diff["removed"])
        + len(endpoint_diff["changed"])
        + len(endpoint_diff["deprecated_added"])
    )
    total_schema_changes = (
        len(schema_diff["added"])
        + len(schema_diff["removed"])
        + len(schema_diff["changed"])
    )

    if old_version == new_version and old_fp["hash"] != new_fp["hash"]:
        print(
            f"NOTE: spec content changed but info.version is unchanged ({new_version}); "
            f"hash {old_fp['hash']} → {new_fp['hash']}"
        )

    classification = classify(endpoint_diff, schema_diff, old_fp, new_fp)
    print(f"Drift classification: {classification}")

    if args.summary_json:
        summary = build_summary(
            spec_source,
            old_version,
            new_version,
            endpoint_diff,
            schema_diff,
            old_fp,
            new_fp,
        )
        args.summary_json.parent.mkdir(parents=True, exist_ok=True)
        with open(args.summary_json, "w") as f:
            json.dump(summary, f, indent=2)
        print(f"Summary written to {args.summary_json}")

    if not args.no_report:
        timestamp = datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M UTC")
        report = format_report(
            old_version, new_version, endpoint_diff, schema_diff, timestamp, old_fp, new_fp
        )
        args.output.parent.mkdir(parents=True, exist_ok=True)
        with open(args.output, "w") as f:
            f.write(report)
        print(f"Report written to {args.output}")

    if total_ep_changes > 0 or total_schema_changes > 0:
        print(
            f"\n⚠️  Changes detected: {total_ep_changes} endpoint change(s), "
            f"{total_schema_changes} schema change(s)"
        )
        print(f"   New endpoints:    {len(endpoint_diff['added'])}")
        print(f"   Removed endpoints: {len(endpoint_diff['removed'])}")
        print(f"   Changed endpoints: {len(endpoint_diff['changed'])}")
        print(f"   Newly deprecated:  {len(endpoint_diff['deprecated_added'])}")
        print(f"   Schema additions:  {len(schema_diff['added'])}")
        print(f"   Schema removals:   {len(schema_diff['removed'])}")
        print(f"   Schema changed:    {len(schema_diff['changed'])}")
        print(f"   Summary-only:      {len(endpoint_diff['summary_only'])} (cosmetic)")
    elif classification == "cosmetic":
        print(
            f"\n📝 Cosmetic drift only: content hash {old_fp['hash']} → {new_fp['hash']} "
            f"with no endpoint or schema-property changes "
            f"({len(endpoint_diff['summary_only'])} summary edit(s))."
        )
    else:
        print("\n✅ No changes detected.")

    if args.update_baseline:
        save_spec(new_spec, args.baseline)
        print(f"Baseline updated to version {new_version}")

    if args.fail_on_structural and classification == "structural":
        sys.exit(1)

    if args.fail_on_diff and classification != "none":
        sys.exit(1)


if __name__ == "__main__":
    main()
