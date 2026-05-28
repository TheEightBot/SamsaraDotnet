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

    for k in old_keys & new_keys:
        o, n = old[k], new[k]

        # Check for deprecation status change
        if not o["deprecated"] and n["deprecated"]:
            deprecated_added[k] = n
        elif o["deprecated"] and not n["deprecated"]:
            deprecated_removed[k] = n

        # Check for meaningful changes
        changes = []
        if o["operationId"] != n["operationId"]:
            changes.append(f"operationId: `{o['operationId']}` → `{n['operationId']}`")
        if o["summary"] != n["summary"]:
            changes.append(f"summary: `{o['summary']}` → `{n['summary']}`")
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
    }


def diff_schemas(old: dict, new: dict) -> dict:
    """Compare schema (component) keys between two specs."""
    old_schemas = set(old.get("components", {}).get("schemas", {}).keys())
    new_schemas = set(new.get("components", {}).get("schemas", {}).keys())
    return {
        "added": sorted(new_schemas - old_schemas),
        "removed": sorted(old_schemas - new_schemas),
    }


def format_report(
    old_version: str,
    new_version: str,
    endpoint_diff: dict,
    schema_diff: dict,
    timestamp: str,
) -> str:
    """Render a Markdown diff report."""
    added = endpoint_diff["added"]
    removed = endpoint_diff["removed"]
    changed = endpoint_diff["changed"]
    dep_added = endpoint_diff["deprecated_added"]
    dep_removed = endpoint_diff["deprecated_removed"]

    total_changes = len(added) + len(removed) + len(changed) + len(dep_added)
    schema_changes = len(schema_diff["added"]) + len(schema_diff["removed"])

    lines = [
        "# Samsara API Sync Diff Report",
        "",
        f"> **Generated**: {timestamp}  ",
        f"> **Old version**: `{old_version}`  ",
        f"> **New version**: `{new_version}`  ",
        f"> **Endpoint changes**: {total_changes}  ",
        f"> **Schema changes**: {schema_changes}  ",
        "",
    ]

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
    if schema_diff["added"] or schema_diff["removed"]:
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
    parser.add_argument("--spec-url", default=SPEC_URL, help="URL to the OpenAPI spec")
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
        "--update-baseline",
        action="store_true",
        help="Save the fetched spec as the new baseline",
    )
    parser.add_argument(
        "--fail-on-diff",
        action="store_true",
        help="Exit with code 1 if differences are found",
    )
    args = parser.parse_args()

    new_spec = fetch_spec(args.spec_url)
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

    total_ep_changes = (
        len(endpoint_diff["added"])
        + len(endpoint_diff["removed"])
        + len(endpoint_diff["changed"])
        + len(endpoint_diff["deprecated_added"])
    )
    total_schema_changes = len(schema_diff["added"]) + len(schema_diff["removed"])

    timestamp = datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M UTC")
    report = format_report(old_version, new_version, endpoint_diff, schema_diff, timestamp)

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
    else:
        print("\n✅ No changes detected.")

    if args.update_baseline:
        save_spec(new_spec, args.baseline)
        print(f"Baseline updated to version {new_version}")

    if args.fail_on_diff and (total_ep_changes > 0 or total_schema_changes > 0):
        sys.exit(1)


if __name__ == "__main__":
    main()
