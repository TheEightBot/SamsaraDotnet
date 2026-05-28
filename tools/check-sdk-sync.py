#!/usr/bin/env python3
"""
Samsara SDK <-> Spec Endpoint Checker
=====================================
Parses the actual HTTP paths the SDK calls (from src/Samsara.Sdk/Clients/**/*Client.cs)
and compares them against the live Samsara OpenAPI spec. Unlike check-api-sync.py (which
diffs the spec against a cached baseline), this verifies that what the SDK *calls* actually
exists in the spec — the class of bug that previously went undetected (e.g. the SDK calling
`/fleet/tags` when the spec path is `/tags`).

Usage:
    python3 tools/check-sdk-sync.py [--spec-url URL | --spec-file PATH]
                                    [--json] [--fail-on-mismatch] [--show-missing]

Exit codes:
    0 — no SDK endpoint mismatches (wrong-path / fabricated)
    1 — mismatches found (only when --fail-on-mismatch is set)
    2 — error (network/parse)
"""
from __future__ import annotations

import argparse
import json
import re
import sys
import urllib.request
from collections import defaultdict
from pathlib import Path

SPEC_URL = "https://developers.samsara.com/openapi/samsara-api.json"
REPO_ROOT = Path(__file__).resolve().parent.parent
SDK_CLIENTS = REPO_ROOT / "src" / "Samsara.Sdk" / "Clients"
BASELINE = REPO_ROOT / ".github" / "cache" / "samsara-api-baseline.json"

HTTP_VERB = {
    "Get": "GET", "GetData": "GET",
    "Post": "POST", "PostData": "POST",
    "Patch": "PATCH", "PatchData": "PATCH",
    "Put": "PUT", "PutData": "PUT",
    "Delete": "DELETE", "DeleteData": "DELETE",
}

# A call to one of the HTTP helpers, capturing verb + first argument expression.
CALL_RE = re.compile(
    r'HttpClient\.(Get|GetData|Post|PostData|Patch|PatchData|Put|PutData|Delete|DeleteData)Async'
    r'(?:<(?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*>)?\(\s*([^,;]+)'
)
PAGINATE_RE = re.compile(
    r'Paginate(?:Data)?Async<(?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*>\(\s*([^,;]+)'
)
METHOD_RE = re.compile(
    r'public\s+(?:async\s+)?[\w<>,\.\?\[\]\s]+?\s+([A-Za-z0-9_]+)Async\s*\(', re.M
)
BASEPATH_RE = re.compile(r'BasePath\s*=\s*"([^"]+)"')
CONST_STRING_RE = re.compile(
    r'(?:private|protected|internal|public)\s+const\s+string\s+(\w+)\s*=\s*"([^"]*)"\s*;')


# ---------------------------------------------------------------- spec loading
def load_spec(spec_url: str | None, spec_file: str | None) -> dict:
    if spec_file:
        return json.loads(Path(spec_file).read_text())
    try:
        req = urllib.request.Request(
            spec_url or SPEC_URL,
            headers={"User-Agent": "SamsaraSdkSyncChecker/1.0"},
        )
        with urllib.request.urlopen(req, timeout=60) as resp:
            return json.loads(resp.read().decode("utf-8"))
    except Exception as exc:  # noqa: BLE001
        if BASELINE.exists():
            print(f"WARNING: live fetch failed ({exc}); using cached baseline", file=sys.stderr)
            return json.loads(BASELINE.read_text())
        print(f"ERROR: could not load spec: {exc}", file=sys.stderr)
        sys.exit(2)


def norm_path(p: str) -> str:
    p = p.strip().strip('"').split("?")[0]
    p = re.sub(r"\{[^}]*\}", "{}", p)
    return p.strip("/")


def spec_operations(spec: dict) -> dict[tuple[str, str], dict]:
    ops: dict[tuple[str, str], dict] = {}
    for path, methods in spec.get("paths", {}).items():
        for verb, details in methods.items():
            if verb.lower() not in ("get", "post", "put", "patch", "delete"):
                continue
            ops[(verb.upper(), norm_path(path))] = {
                "rawpath": path,
                "operationId": details.get("operationId", ""),
                "tags": details.get("tags", []) or ["Untagged"],
                "deprecated": details.get("deprecated", False),
            }
    return ops


# --------------------------------------------------------------- SDK parsing
def _iter_methods(text: str):
    """Yield (method_name, body_text) for each public *Async method.

    Body starts AFTER the parameter list so that parameter defaults
    (e.g. `startTime = null`) are not mistaken for local assignments.
    """
    matches = list(METHOD_RE.finditer(text))
    for i, m in enumerate(matches):
        # m.end() is just inside the parameter list (after the opening '(')
        depth, j = 1, m.end()
        while j < len(text) and depth > 0:
            c = text[j]
            if c == "(":
                depth += 1
            elif c == ")":
                depth -= 1
            j += 1
        end = matches[i + 1].start() if i + 1 < len(matches) else len(text)
        yield m.group(1) + "Async", text[j:end]


def _first_arg(s: str) -> str:
    """Return the first top-level argument from a call argument list (s starts after '(')."""
    depth = 0
    for i, ch in enumerate(s):
        if ch in "([{":
            depth += 1
        elif ch in ")]}":
            if depth == 0:
                return s[:i]
            depth -= 1
        elif ch == "," and depth == 0:
            return s[:i]
    return s


def _resolve_expr(
    expr: str,
    basepath: str | None,
    assigns: dict[str, str],
    class_consts: dict[str, str] | None = None,
    depth: int = 0,
) -> str | None:
    """Resolve a path expression to a normalized path, or None if not a literal path."""
    if depth > 8:
        return None
    expr = expr.strip()
    class_consts = class_consts or {}
    # Unwrap QueryBuilder.WithTimeRange( / WithParams( wrappers -> resolve their FIRST arg
    m = re.match(r"(?:QueryBuilder\.)?With(?:TimeRange|Params)\(\s*(.*)$", expr, re.S)
    if m:
        return _resolve_expr(_first_arg(m.group(1)), basepath, assigns, class_consts, depth + 1)
    # Bare identifier -> resolve via assignment in the same method
    # (covers both local vars like `path` and class consts like `ConfigurationsPath`).
    if re.fullmatch(r"[A-Za-z][A-Za-z0-9_]*", expr):
        if expr in assigns:
            return _resolve_expr(assigns[expr], basepath, assigns, class_consts, depth + 1)
        if expr in class_consts:
            return norm_path(class_consts[expr]) or None
        return None  # unresolved local (treated as unknown, not a literal)
    s = expr
    if basepath:
        s = s.replace("{BasePath}", basepath)
        s = re.sub(r"\bBasePath\b", basepath, s)
    # Substitute class-level const aliases inside interpolated strings (e.g. {ConfigurationsPath}).
    for name, lit in class_consts.items():
        s = s.replace("{" + name + "}", lit)
    # ternary / concatenation: take the first quoted or interpolated string literal
    lit = re.search(r'\$?"([^"]*)"', s)
    if lit:
        s = lit.group(1)
    elif "(" in s:
        return None  # unresolved method-call expression
    s = re.sub(r"\{[^}]*\}", "{}", s)
    np = norm_path(s)
    return np or None


def sdk_endpoints() -> list[dict]:
    out: list[dict] = []
    for path in sorted(SDK_CLIENTS.rglob("*Client.cs")):
        text = path.read_text()
        # Skip files that only declare an interface (no concrete class with methods).
        if "class " not in text:
            continue
        bp_m = BASEPATH_RE.search(text)
        basepath = bp_m.group(1) if bp_m else None
        # Class-level const string aliases (e.g. ConfigurationsPath, IncidentsStreamPath).
        class_consts = {m.group(1): m.group(2) for m in CONST_STRING_RE.finditer(text)}
        for method_name, body in _iter_methods(text):
            # collect local assignments: `var x = <expr>;` and `x = <expr>;`
            assigns: dict[str, str] = {}
            for am in re.finditer(r'(?:var\s+)?([a-z][A-Za-z0-9_]*)\s*=\s*([^;]+);', body):
                assigns.setdefault(am.group(1), am.group(2))
            # find terminal HTTP / Paginate call
            verb = arg = None
            cm = CALL_RE.search(body)
            if cm:
                verb, arg = HTTP_VERB[cm.group(1)], cm.group(2)
            else:
                pm = PAGINATE_RE.search(body)
                if pm:
                    verb, arg = "GET", pm.group(1)
            if not verb:
                continue
            np = _resolve_expr(arg, basepath, assigns, class_consts)
            out.append({
                "file": path.name, "method": method_name,
                "verb": verb, "path": np, "rawarg": arg.strip(),
            })
    return out


# --------------------------------------------------------------- reporting
def analyze(spec: dict):
    ops = spec_operations(spec)
    eps = sdk_endpoints()
    sdk_keys = {(e["verb"], e["path"]) for e in eps if e["path"]}

    matched, mismatched, unresolved = [], [], []
    for e in eps:
        if e["path"] is None:
            unresolved.append(e)
        elif (e["verb"], e["path"]) in ops:
            matched.append(e)
        else:
            mismatched.append(e)

    # missing = spec ops not implemented
    by_tag_missing = defaultdict(list)
    for (verb, np), meta in ops.items():
        if (verb, np) not in sdk_keys:
            for t in meta["tags"]:
                by_tag_missing[t].append((verb, meta["rawpath"], meta["operationId"], meta["deprecated"]))

    return ops, eps, matched, mismatched, unresolved, by_tag_missing


def main() -> None:
    ap = argparse.ArgumentParser(description="Check SDK endpoints against the Samsara spec.")
    ap.add_argument("--spec-url", default=SPEC_URL)
    ap.add_argument("--spec-file")
    ap.add_argument("--json", action="store_true", help="emit machine-readable JSON")
    ap.add_argument("--show-missing", action="store_true", help="list unimplemented spec ops")
    ap.add_argument("--fail-on-mismatch", action="store_true")
    args = ap.parse_args()

    spec = load_spec(args.spec_url, args.spec_file)
    ops, eps, matched, mismatched, unresolved, by_tag_missing = analyze(spec)
    total_missing = sum(len(v) for v in by_tag_missing.values())

    if args.json:
        print(json.dumps({
            "spec_operations": len(ops),
            "sdk_endpoints": len(eps),
            "matched": len(matched),
            "mismatched": [{"verb": e["verb"], "path": e["path"], "method": e["method"], "file": e["file"]} for e in mismatched],
            "unresolved": [{"method": e["method"], "file": e["file"], "rawarg": e["rawarg"]} for e in unresolved],
            "missing_count": total_missing,
        }, indent=2))
    else:
        print("=" * 64)
        print("Samsara SDK <-> Spec endpoint check")
        print("=" * 64)
        print(f"Spec operations:        {len(ops)}")
        print(f"SDK endpoints parsed:   {len(eps)}")
        print(f"  matched:              {len(matched)}")
        print(f"  MISMATCHED:           {len(mismatched)}")
        print(f"  unresolved (dynamic): {len(unresolved)}")
        print(f"Spec ops not implemented: {total_missing}")
        if mismatched:
            print("\n--- MISMATCHED SDK endpoints (wrong path or fabricated) ---")
            for e in sorted(mismatched, key=lambda x: (x["file"], x["path"] or "")):
                print(f"  {e['verb']:7} /{e['path']:42} {e['file']}::{e['method']}")
        if unresolved:
            print("\n--- UNRESOLVED (could not statically resolve path; verify manually) ---")
            for e in sorted(unresolved, key=lambda x: x["file"]):
                print(f"  {e['file']}::{e['method']}  arg={e['rawarg']!r}")
        if args.show_missing:
            print("\n--- MISSING (spec ops with no SDK endpoint) ---")
            for tag in sorted(by_tag_missing):
                print(f"\n### {tag} ({len(by_tag_missing[tag])})")
                for verb, rawpath, oid, dep in sorted(by_tag_missing[tag]):
                    print(f"  {verb:7} {rawpath:52} {oid}{' [DEP]' if dep else ''}")

    if args.fail_on_mismatch and mismatched:
        sys.exit(1)


if __name__ == "__main__":
    main()
