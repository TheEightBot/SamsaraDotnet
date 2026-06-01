#!/usr/bin/env python3
"""
Samsara SDK Fabrication / Mis-homing Checker
============================================
Complements ``check-sdk-sync.py``. That tool verifies *coverage* (every spec op has an
SDK method, and every SDK path exists in the spec). This tool verifies the **reverse,
semantic** property that coverage is blind to:

    Every public SDK client method maps to a DISTINCT, correctly-homed spec operation.

It catches the class of bug where a method compiles and even resolves to a *real* spec
path, yet is fabricated or mis-homed. Concrete example this was written for: ``HubsClient``
shipped ``Get/Create/Update/Delete`` CRUD pointed at ``BasePath = "addresses"``. Those
matched the real ``/addresses`` operations, so ``check-sdk-sync.py`` reported
``mismatched=0`` while Hubs carried four phantom methods (the spec has only ``GET /hubs``).
At runtime ``ListAsync`` then threw ``missing required properties: timeZone, createdAt,
updatedAt`` because it deserialized an address payload into the tightened ``Hub`` record.

Checks
------
1. **DUPLICATE COVERAGE** (gating) — a single spec operation reached from more than one
   client file. The fingerprint of a copy/paste mis-homing. Genuine intentional duplicates
   (rare) go in ``ALLOW_DUPLICATE`` below.
2. **CLIENT ↔ TAG DRIFT** (gating) — a method reaches a spec op whose OpenAPI tag is not in
   that client's committed allowed-tag set (``tools/sdk-client-tags.json``). A method
   reaching a brand-new tag for its client is either a legitimate new cross-domain method
   (re-run with ``--update-tags`` after review) or a mis-homing (fix it).

Usage
-----
    python3 tools/check-sdk-fabrication.py [--spec-url URL | --spec-file PATH]
        [--json] [--fail-on-issues] [--update-tags]

Exit codes
----------
    0 — no issues (or issues found but --fail-on-issues not set)
    1 — issues found and --fail-on-issues set
    2 — error (network / parse / missing baseline)
"""
from __future__ import annotations

import argparse
import importlib.util
import json
import sys
from collections import defaultdict
from pathlib import Path

REPO_ROOT = Path(__file__).resolve().parent.parent
TAGS_BASELINE = REPO_ROOT / "tools" / "sdk-client-tags.json"

# Spec operations that are intentionally reached from more than one client file.
# Key: "VERB /normalized/path"  ->  reason. Keep this list empty unless a duplicate is
# a deliberate, documented convenience (it usually is not).
ALLOW_DUPLICATE: dict[str, str] = {}


def _load_checker():
    """Import check-sdk-sync.py (hyphenated filename) to reuse its spec/SDK parser."""
    path = REPO_ROOT / "tools" / "check-sdk-sync.py"
    spec = importlib.util.spec_from_file_location("check_sdk_sync", path)
    mod = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(mod)
    return mod


def build_coverage(eps: list[dict]):
    """Map (verb, path) -> list of (file, method) that reach it."""
    cover: dict[tuple[str, str], list[tuple[str, str]]] = defaultdict(list)
    for e in eps:
        if e["path"] is None:
            continue
        cover[(e["verb"], e["path"])].append((e["file"], e["method"]))
    return cover


def current_client_tags(eps: list[dict], ops: dict) -> dict[str, list[str]]:
    """For each client file, the sorted set of spec tags its methods legitimately touch."""
    tags: dict[str, set[str]] = defaultdict(set)
    for e in eps:
        if e["path"] is None:
            continue
        meta = ops.get((e["verb"], e["path"]))
        if not meta:
            continue  # fabricated-with-invalid-path is check-sdk-sync's job
        for t in meta["tags"]:
            tags[e["file"]].add(t)
    return {f: sorted(v) for f, v in sorted(tags.items())}


def analyze(mod, spec: dict):
    ops = mod.spec_operations(spec)
    eps = mod.sdk_endpoints()
    cover = build_coverage(eps)

    # Check 1 — duplicate coverage
    duplicates = []
    for (verb, path), hits in sorted(cover.items()):
        files = {f for f, _ in hits}
        if len(files) > 1 and f"{verb} /{path}" not in ALLOW_DUPLICATE:
            duplicates.append({
                "verb": verb, "path": path,
                "operationId": ops.get((verb, path), {}).get("operationId", "<not-in-spec>"),
                "methods": [f"{f}::{m}" for f, m in hits],
            })

    # Check 2 — client<->tag drift vs committed baseline
    tag_drift = []
    baseline = json.loads(TAGS_BASELINE.read_text()) if TAGS_BASELINE.exists() else None
    if baseline is not None:
        for e in eps:
            if e["path"] is None:
                continue
            meta = ops.get((e["verb"], e["path"]))
            if not meta:
                continue
            allowed = set(baseline.get(e["file"], []))
            stray = [t for t in meta["tags"] if t not in allowed]
            if stray:
                tag_drift.append({
                    "method": f"{e['file']}::{e['method']}",
                    "endpoint": f"{e['verb']} /{e['path']}",
                    "stray_tags": stray,
                })

    return ops, eps, duplicates, tag_drift, baseline


def main() -> None:
    ap = argparse.ArgumentParser(description="Detect fabricated / mis-homed SDK methods.")
    ap.add_argument("--spec-url")
    ap.add_argument("--spec-file")
    ap.add_argument("--json", action="store_true")
    ap.add_argument("--fail-on-issues", action="store_true")
    ap.add_argument("--update-tags", action="store_true",
                    help="regenerate tools/sdk-client-tags.json from the current SDK (review the diff!)")
    args = ap.parse_args()

    mod = _load_checker()
    spec = mod.load_spec(args.spec_url, args.spec_file)

    if args.update_tags:
        ops = mod.spec_operations(spec)
        eps = mod.sdk_endpoints()
        mapping = current_client_tags(eps, ops)
        TAGS_BASELINE.write_text(json.dumps(mapping, indent=2) + "\n")
        print(f"Wrote {TAGS_BASELINE.relative_to(REPO_ROOT)} ({len(mapping)} clients). "
              "Review the diff before committing.")
        return

    ops, eps, duplicates, tag_drift, baseline = analyze(mod, spec)
    issues = len(duplicates) + len(tag_drift)

    if args.json:
        print(json.dumps({
            "duplicate_coverage": duplicates,
            "tag_drift": tag_drift,
            "baseline_present": baseline is not None,
        }, indent=2))
    else:
        print("=" * 64)
        print("Samsara SDK fabrication / mis-homing check")
        print("=" * 64)
        print(f"SDK endpoints parsed: {len([e for e in eps if e['path']])}")
        print(f"Duplicate coverage:   {len(duplicates)}")
        print(f"Client<->tag drift:   {len(tag_drift)}"
              + ("" if baseline is not None else "  (no baseline — run --update-tags)"))
        if duplicates:
            print("\n--- DUPLICATE COVERAGE (one spec op reached from >1 client) ---")
            for d in duplicates:
                print(f"  {d['verb']:7} /{d['path']:40} op={d['operationId']}")
                for m in d["methods"]:
                    print(f"        {m}")
        if tag_drift:
            print("\n--- CLIENT<->TAG DRIFT (method reaches a tag outside its client's baseline) ---")
            for t in tag_drift:
                print(f"  {t['method']}  ->  {t['endpoint']}  stray_tags={t['stray_tags']}")
            print("\n  If these are legitimate new cross-domain methods, review then re-run with")
            print("  --update-tags. Otherwise the method is mis-homed — point it at the right path.")
        if issues == 0:
            print("\n✅ No fabricated or mis-homed methods detected.")

    if args.fail_on_issues and issues:
        sys.exit(1)


if __name__ == "__main__":
    main()
