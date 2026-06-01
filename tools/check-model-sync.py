#!/usr/bin/env python3
"""
Samsara SDK <-> Spec MODEL Checker
==================================
Compares hand-written SDK record properties against the live Samsara OpenAPI
spec schemas, **property by property, matched BY ENDPOINT** (never by type
name). Where check-sdk-sync.py verifies the *paths* the SDK calls exist, this
verifies the *shapes* the SDK sends/receives line up with the spec's
request/response bodies. It codifies the one-time manual model-parity audit so
drift is caught automatically in CI.

How it works
------------
1. Reuse check-sdk-sync.py's parser to enumerate every SDK client method with a
   resolvable (verb, path). Extend that parsing to also capture, per method, the
   C# *response type* (the generic arg of GetDataAsync<T>/PaginateAsync<T>/...)
   and the C# *request type* (the parameter type of the body argument passed to
   Post/Patch/Put helpers).
2. For each method, look up the matching spec operation. Resolve the spec
   request inner schema (Post/Patch/Put) and response inner schema by following
   requestBody/responses[2xx] -> $ref -> unwrap ONE { data: T } envelope layer.
3. Parse SDK models (`public sealed record NAME { ... }`) into
   record -> { json_prop -> {ctype, required, nullable} }.
4. Compare spec inner schema properties against the SDK record: MISSING / EXTRA
   / type-mismatch / required-ness drift / wrapper-shape mismatch. Also compare
   spec query parameters against SDK method parameters.

Severity: CRITICAL / HIGH / MEDIUM / LOW (see classify()). Beta-tagged
endpoints and the `Clients/Beta/*` weak-typed clients cap at MEDIUM.

Usage:
    python3 tools/check-model-sync.py [--spec-url URL | --spec-file PATH]
                                      [--json] [--by-domain]
                                      [--fail-on-severity {CRITICAL,HIGH,MEDIUM,LOW}]

Exit codes:
    0 — no findings at/above --fail-on-severity (or flag not set)
    1 — findings at/above --fail-on-severity exist
    2 — error (network/parse)
"""
from __future__ import annotations

import argparse
import importlib.util
import json
import re
import sys
from collections import defaultdict
from pathlib import Path

REPO_ROOT = Path(__file__).resolve().parent.parent
SDK_ROOT = REPO_ROOT / "src" / "Samsara.Sdk"
SDK_CLIENTS = SDK_ROOT / "Clients"
SDK_MODELS = SDK_ROOT / "Models"

SEVERITIES = ["CRITICAL", "HIGH", "MEDIUM", "LOW"]
SEV_RANK = {s: i for i, s in enumerate(SEVERITIES)}  # lower index == more severe

# Pagination query params handled by PaginateAsync; never flag as missing.
PAGINATION_PARAMS = {"limit", "after", "endcursor", "startcursor"}
# Time-range query params handled by QueryBuilder.WithTimeRange. The helper
# emits startTime/endTime, but the equivalent v1 endpoints name the same
# delegated window startMs/endMs — both are skipped when WithTimeRange is used.
TIMERANGE_PARAMS = {"starttime", "endtime", "startms", "endms"}


# ---------------------------------------------------------------- import parser
def _load_cs_module():
    spec_loader = importlib.util.spec_from_file_location(
        "cs", str(Path(__file__).resolve().parent / "check-sdk-sync.py")
    )
    cs = importlib.util.module_from_spec(spec_loader)
    spec_loader.loader.exec_module(cs)
    return cs


cs = _load_cs_module()


# ============================================================================
# Spec schema resolution
# ============================================================================
class SpecResolver:
    """Resolves $ref chains and unwraps the { data: T } envelope to the inner
    type the endpoint actually sends/receives."""

    def __init__(self, spec: dict):
        self.spec = spec
        self.schemas = spec.get("components", {}).get("schemas", {})

    def deref(self, schema: dict | None, depth: int = 0) -> dict | None:
        if schema is None or depth > 30:
            return schema
        seen = 0
        while isinstance(schema, dict) and "$ref" in schema and seen < 30:
            name = schema["$ref"].split("/")[-1]
            schema = self.schemas.get(name)
            seen += 1
        # Collapse single-branch allOf wrappers (common envelope pattern).
        if isinstance(schema, dict) and "properties" not in schema:
            for key in ("allOf", "oneOf", "anyOf"):
                branch = schema.get(key)
                if isinstance(branch, list) and len(branch) == 1:
                    return self.deref(branch[0], depth + 1)
        return schema

    def op_schema(self, op: dict, kind: str) -> dict | None:
        """Return the top-level (enveloped) schema for an operation's request
        body (kind='request') or success response (kind='response')."""
        if kind == "request":
            body = op.get("requestBody")
            if not isinstance(body, dict):
                return None
            content = body.get("content", {})
        else:
            responses = op.get("responses", {})
            content = None
            for code in ("200", "201", "202", "203", "204"):
                if code in responses:
                    content = responses[code].get("content", {})
                    if content:
                        break
            if content is None:
                return None
        media = content.get("application/json") or content.get("application/json; charset=utf-8")
        if not isinstance(media, dict):
            # Fall back to first JSON-ish media type.
            for k, v in content.items():
                if "json" in k.lower():
                    media = v
                    break
        if not isinstance(media, dict):
            return None
        return media.get("schema")

    def unwrap_envelope(self, schema: dict | None):
        """Given an enveloped schema, return (inner_schema, is_list, required_set).

        Unwraps exactly ONE { data: T } / { data: T[] } layer when present.

        The SDK's modelling convention mirrors this exact layer: when the spec
        wraps a named list one level deeper (e.g. { data: { vehicleReports: [...] } }
        or data: [ { violations: [...] } ]), the SDK models the `data` layer
        directly as a record that *contains* a vehicleReports/violations property
        — it does NOT pre-flatten to the inner item. Therefore we deliberately do
        NOT double-unwrap; doing so would compare the inner-item schema against
        the wrong SDK record and emit false MISSING findings. required_set is the
        set of required property names on the unwrapped object schema.
        """
        s = self.deref(schema)
        if not isinstance(s, dict):
            return None, False, set()
        props = s.get("properties")
        if isinstance(props, dict) and "data" in props:
            data = self.deref(props["data"])
            if isinstance(data, dict):
                is_list = data.get("type") == "array" or "items" in data
                inner = self.deref(data.get("items")) if is_list else data
                if isinstance(inner, dict):
                    return inner, is_list, set(inner.get("required", []) or [])
                return inner, is_list, set()
        # No envelope — schema is the payload directly.
        is_list = s.get("type") == "array" or "items" in s
        if is_list:
            inner = self.deref(s.get("items"))
            if isinstance(inner, dict):
                return inner, True, set(inner.get("required", []) or [])
            return inner, True, set()
        return s, False, set(s.get("required", []) or [])

    def resolve_named_wrapper(self, inner: dict | None, sdk_prop_names: set[str]):
        """Disambiguate the single-key named-list wrapper pattern.

        After unwrapping the `{ data: ... }` envelope, some endpoints expose a
        single-key object such as { driverReports: [...] } / { violations: [...] }
        / { media: [...] }. The SDK is inconsistent about whether it models that
        wrapper as its own record (FuelEnergyDriverReportsResponse HAS a
        `driverReports` property) or flattens it and models the inner item
        directly (MediaFile mirrors the media[] item, with no `media` property).

        We pick based on the SDK record: if the SDK record already has a property
        for the wrapper key, keep the wrapper schema (no descent). Otherwise
        descend into the wrapper's single list/object and return its item schema
        so we compare against the shape the SDK actually models.

        Returns (schema, required_set).
        """
        d = self.deref(inner)
        if not isinstance(d, dict):
            return inner, set()
        props = d.get("properties")
        if not (isinstance(props, dict) and len(props) == 1 and "pagination" not in props):
            return d, set(d.get("required", []) or [])
        (only_key, only_val), = props.items()
        # SDK models the wrapper directly -> compare against the wrapper object.
        if only_key in sdk_prop_names:
            return d, set(d.get("required", []) or [])
        ov = self.deref(only_val)
        if not isinstance(ov, dict):
            return d, set(d.get("required", []) or [])
        if ov.get("type") == "array" or "items" in ov:
            item = self.deref(ov.get("items"))
            if isinstance(item, dict):
                return item, set(item.get("required", []) or [])
        elif ov.get("type") == "object" or "properties" in ov:
            return ov, set(ov.get("required", []) or [])
        return d, set(d.get("required", []) or [])

    def wrapper_is_data_enveloped(self, schema: dict | None) -> bool:
        """True if the top-level schema is a { data: ... } envelope."""
        s = self.deref(schema)
        if not isinstance(s, dict):
            return False
        props = s.get("properties")
        return isinstance(props, dict) and "data" in props

    def data_is_array(self, schema: dict | None) -> bool:
        """True if the envelope's `data` member is an array."""
        s = self.deref(schema)
        if not isinstance(s, dict):
            return False
        props = s.get("properties", {})
        data = self.deref(props.get("data")) if isinstance(props, dict) else None
        if not isinstance(data, dict):
            return False
        return data.get("type") == "array" or "items" in data


# ============================================================================
# SDK model parsing
# ============================================================================
RECORD_RE = re.compile(
    r'public\s+(?:sealed\s+|abstract\s+)?record\s+([A-Za-z0-9_]+)\b'
)
JSONPROP_RE = re.compile(r'\[JsonPropertyName\(\s*"([^"]+)"\s*\)\]')
# A property declaration: modifiers, type, name, accessors.
PROP_DECL_RE = re.compile(
    r'public\s+(required\s+)?([A-Za-z0-9_<>,\.\?\[\]\s]+?)\s+([A-Za-z0-9_]+)\s*\{\s*get'
)


def _record_body(text: str, brace_open: int) -> tuple[str, int]:
    """Return (body, index_after_close) for a record body starting at the
    opening brace index."""
    depth, i = 0, brace_open
    while i < len(text):
        c = text[i]
        if c == "{":
            depth += 1
        elif c == "}":
            depth -= 1
            if depth == 0:
                return text[brace_open + 1:i], i + 1
        i += 1
    return text[brace_open + 1:], len(text)


def _strip_generic_suffix(t: str) -> str:
    return t.strip().rstrip("?").strip()


# Collection wrappers whose top-level record key is their ELEMENT type. When a
# client method returns one of these directly (e.g. GetHosClocksAsync ->
# IReadOnlyList<HosClocksForDriver>), the spec response is a { data: [item] }
# envelope and the SDK record to compare against is the element, not the
# collection. Listed innermost-first is unnecessary since we unwrap iteratively.
_COLLECTION_GENERICS = (
    "IReadOnlyList",
    "IList",
    "List",
    "IEnumerable",
    "IReadOnlyCollection",
    "ICollection",
    "IAsyncEnumerable",
)


def _record_key(t: str) -> str:
    """Reduce a C# response/request type to the SDK record key used in `models`.

    Strips a trailing top-level generic-argument list so a closed generic
    wrapper (e.g. ``V1SensorReadingsResponse<V1CargoReading>``) resolves to the
    record actually declared in source (``V1SensorReadingsResponse``). When the
    type is a bare collection of a record (``IReadOnlyList<HosClocksForDriver>``),
    the meaningful top-level record is the *element*, so the collection wrapper is
    peeled to its single type argument. Unlike :func:`_strip_generic_suffix`
    (which is applied to *property* types and must preserve element info such as
    ``IReadOnlyList<long>``), this is only for top-level record lookups, so
    reducing to the inner record is correct."""
    t = _strip_generic_suffix(t)
    # Peel collection wrappers to their element record (iteratively, e.g. a
    # hypothetical IReadOnlyList<List<T>> reduces to T).
    changed = True
    while changed:
        changed = False
        lt = t.find("<")
        if lt == -1:
            break
        head = t[:lt].strip()
        if head in _COLLECTION_GENERICS and t.endswith(">"):
            t = t[lt + 1:-1].strip()
            changed = True
    lt = t.find("<")
    return t[:lt].strip() if lt != -1 else t


def parse_models() -> dict[str, dict[str, dict]]:
    """record_name -> { json_prop -> {ctype, required, nullable} }.

    Skips positional-record declarations (those use `record Name(...)` with no
    brace body of properties) — none of the SDK models in scope use them for the
    DTOs we compare, but we guard anyway.
    """
    models: dict[str, dict[str, dict]] = {}
    for path in sorted(SDK_MODELS.rglob("*.cs")):
        text = path.read_text()
        for m in RECORD_RE.finditer(text):
            name = m.group(1)
            # Find the next '{' that opens the body (skip an optional
            # positional param list and base-type list).
            rest = text[m.end():]
            brace_rel = rest.find("{")
            semic_rel = rest.find(";")
            if brace_rel == -1:
                continue
            if semic_rel != -1 and semic_rel < brace_rel:
                continue  # `record X;` or `record X(...) : Y;` with no body
            brace_abs = m.end() + brace_rel
            body, _ = _record_body(text, brace_abs)
            props: dict[str, dict] = {}
            # Walk property declarations; the JsonPropertyName attribute (if
            # any) immediately precedes the declaration.
            for pm in PROP_DECL_RE.finditer(body):
                required = bool(pm.group(1))
                ctype = pm.group(2).strip()
                # Look back for the nearest JsonPropertyName attribute.
                preceding = body[:pm.start()]
                jm = None
                for jm in JSONPROP_RE.finditer(preceding):
                    pass  # keep the last one before this declaration
                if jm is None:
                    continue  # no JSON mapping -> not a serialized DTO prop
                # Ensure the attribute is "close" (no other property between).
                gap = preceding[jm.end():]
                if re.search(r'\}\s*get', gap):
                    continue  # another accessor sits between attr and this prop
                json_name = jm.group(1)
                nullable = ctype.endswith("?")
                props[json_name] = {
                    "ctype": _strip_generic_suffix(ctype),
                    "required": required,
                    "nullable": nullable,
                }
            # Last writer wins on duplicate record names; record both is rare.
            if props or name not in models:
                models[name] = props
    return models


# ============================================================================
# Extended SDK client parsing (response/request C# types)
# ============================================================================
# Generic helper call: HttpClient.<Verb>Async<TYPE>( ... )  -> capture TYPE
GENERIC_HELPER_RE = re.compile(
    r'HttpClient\.(GetData|Get|PostData|Post|PatchData|Patch|PutData|Put)Async'
    r'<((?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*)>\('
)
PAGINATE_GENERIC_RE = re.compile(
    r'Paginate(?:Data)?Async<((?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*)>\('
)
# Body-bearing helper call capturing verb + full arg list start.
BODY_HELPER_RE = re.compile(
    r'HttpClient\.(PostData|Post|PatchData|Patch|PutData|Put)Async'
    r'(?:<(?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*>)?\(\s*(.*)$',
    re.S,
)
# Parameter list capture for a method header.
PARAM_METHOD_RE = re.compile(
    r'public\s+(?:async\s+)?[\w<>,\.\?\[\]\s]+?\s+([A-Za-z0-9_]+)Async\s*\(', re.M
)
# Literal query-key in a WithParams/WithTimeRange tuple: ("startMs", ...).
QUERY_KEY_RE = re.compile(r'\(\s*"([A-Za-z_][A-Za-z0-9_]*)"\s*,')


def _split_top_args(s: str) -> list[str]:
    """Split a call argument list (s starts just after '(') at top-level commas,
    stopping at the matching close paren."""
    args, depth, start = [], 0, 0
    for i, ch in enumerate(s):
        if ch in "([{<":
            depth += 1
        elif ch in ")]}>":
            if depth == 0:
                args.append(s[start:i])
                return args
            depth -= 1
        elif ch == "," and depth == 0:
            args.append(s[start:i])
            start = i + 1
    args.append(s)
    return args


def _split_generic_args(s: str) -> list[str]:
    """Split a top-level generic argument list (the text already inside the
    outer ``<>``) at top-level commas, respecting nested angle brackets."""
    args, depth, start = [], 0, 0
    for i, ch in enumerate(s):
        if ch == "<":
            depth += 1
        elif ch == ">":
            depth -= 1
        elif ch == "," and depth == 0:
            args.append(s[start:i])
            start = i + 1
    args.append(s[start:])
    return [a.strip() for a in args if a.strip()]


def _parse_params(text: str, header_match) -> dict[str, str]:
    """Return {param_name: param_type} for the method whose header regex match
    is given. Captures the parenthesized parameter list following the name."""
    # header_match.end() lands just after the opening '('.
    depth, j = 1, header_match.end()
    start = j
    while j < len(text) and depth > 0:
        c = text[j]
        if c == "(":
            depth += 1
        elif c == ")":
            depth -= 1
        j += 1
    param_str = text[start:j - 1]
    params: dict[str, str] = {}
    for raw in _split_params(param_str):
        raw = raw.strip()
        if not raw:
            continue
        # Drop default value.
        raw = raw.split("=", 1)[0].strip()
        # Split type and name on the last whitespace.
        mm = re.match(r'^(.*\S)\s+([A-Za-z0-9_]+)$', raw)
        if not mm:
            continue
        ptype = mm.group(1).strip()
        pname = mm.group(2).strip()
        # Strip leading modifiers like 'this', 'params', 'in', 'ref', 'out'.
        ptype = re.sub(r'^(?:this|params|in|ref|out|readonly)\s+', '', ptype)
        params[pname] = ptype
    return params


def _split_params(param_str: str) -> list[str]:
    """Split a parameter list at top-level commas (generics/arrays aware)."""
    out, depth, start = [], 0, 0
    for i, ch in enumerate(param_str):
        if ch in "<([{":
            depth += 1
        elif ch in ">)]}":
            depth = max(0, depth - 1)
        elif ch == "," and depth == 0:
            out.append(param_str[start:i])
            start = i + 1
    out.append(param_str[start:])
    return out


def parse_client_types() -> dict[tuple[str, str], dict]:
    """(file, method) -> {response_type, request_type, params}.

    response_type: generic arg of the terminal HTTP/Paginate helper, else None.
    request_type: resolved C# type of the body argument for Post/Patch/Put, else None.
    params: {param_name_lower: param_type} for query-param checks.
    """
    info: dict[tuple[str, str], dict] = {}
    for path in sorted(SDK_CLIENTS.rglob("*Client.cs")):
        text = path.read_text()
        if "class " not in text:
            continue
        headers = list(PARAM_METHOD_RE.finditer(text))
        for idx, hm in enumerate(headers):
            method = hm.group(1) + "Async"
            params = _parse_params(text, hm)
            # Body of this method = from after its param list to next header.
            # Re-find the end of the param list.
            depth, j = 1, hm.end()
            while j < len(text) and depth > 0:
                c = text[j]
                if c == "(":
                    depth += 1
                elif c == ")":
                    depth -= 1
                j += 1
            body_end = headers[idx + 1].start() if idx + 1 < len(headers) else len(text)
            body = text[j:body_end]

            response_type = None
            gm = GENERIC_HELPER_RE.search(body)
            if gm:
                response_type = gm.group(2).strip()
            else:
                pm = PAGINATE_GENERIC_RE.search(body)
                if pm:
                    # PaginateAsync<TItem> -> TItem. The nested-wrapper overload
                    # PaginateAsync<TData, TItem> -> TItem: the last generic arg
                    # is the element type that maps to the spec's paginated array
                    # item (TData is the inner { items: [...] } wrapper object).
                    gen_args = _split_generic_args(pm.group(1))
                    response_type = gen_args[-1] if gen_args else pm.group(1).strip()

            request_type = None
            bm = BODY_HELPER_RE.search(body)
            if bm:
                arglist = _split_top_args(bm.group(2))
                # Helpers are <Verb>DataAsync(path, body, ct) — body is arg[1].
                if len(arglist) >= 2:
                    body_arg = arglist[1].strip()
                    # Resolve identifier -> its declared parameter type.
                    ident = re.match(r'^([A-Za-z_][A-Za-z0-9_]*)\b', body_arg)
                    if ident and ident.group(1) in params:
                        request_type = params[ident.group(1)]
                    elif ident:
                        # Could be a local var built in-method; try a local decl.
                        decl = re.search(
                            r'\bvar\s+' + re.escape(ident.group(1)) + r'\s*=\s*new\s+([A-Za-z0-9_<>\.]+)',
                            body,
                        )
                        if decl:
                            request_type = decl.group(1)

            is_beta = "/Beta/" in str(path) or "\\Beta\\" in str(path)
            body_tr = "WithTimeRange" in body
            delegates = bool(re.search(r'\b(Filter|QueryBuilder)\b', "".join(params.values())))
            params_lc = {k.lower(): v for k, v in params.items()}
            # Literal query-key names the method emits, e.g. WithParams(path,
            # ("startMs", ...)) — these are the actual wire param names even when
            # the C# parameter is named differently (startTime -> startMs).
            emitted_keys = {m.group(1).lower() for m in QUERY_KEY_RE.finditer(body)}

            key = (path.name, method)
            if key in info:
                # Merge across overloads: union params, prefer first concrete
                # response/request type, OR the boolean flags. This keeps the
                # overload that makes the terminal HTTP call from being shadowed
                # by a thin delegating overload (e.g. SetAssignmentAsync(request)).
                g = info[key]
                g["params"].update({k: v for k, v in params_lc.items() if k not in g["params"]})
                g["emitted_keys"].update(emitted_keys)
                g["response_type"] = g["response_type"] or response_type
                g["request_type"] = g["request_type"] or request_type
                g["body_uses_timerange"] = g["body_uses_timerange"] or body_tr
                # delegates_filter only if EVERY overload delegates (an overload
                # exposing explicit params should drive the query-param check).
                g["delegates_filter"] = g["delegates_filter"] and delegates
            else:
                info[key] = {
                    "response_type": response_type,
                    "request_type": request_type,
                    "params": params_lc,
                    "emitted_keys": emitted_keys,
                    "is_beta": is_beta,
                    "body_uses_timerange": body_tr,
                    "delegates_filter": delegates,
                }
    return info


# ============================================================================
# Type comparison (spec type/format -> acceptable C# types)
# ============================================================================
def _csharp_base(ctype: str) -> str:
    """Reduce a C# type to a comparable base token (strip namespaces, generics
    wrappers we don't care about, nullability)."""
    t = ctype.strip().rstrip("?").strip()
    # Collapse namespace-qualified names to the leaf.
    return t


def _is_collection(ctype: str) -> bool:
    t = ctype.strip().rstrip("?")
    return bool(re.search(r'(IReadOnlyList|IList|List|IEnumerable|\[\])\s*<', t)) or t.endswith("[]") \
        or t.startswith("IReadOnlyList") or t.startswith("IEnumerable") or t.startswith("List") \
        or t.startswith("IList")


def _is_dictionary(ctype: str) -> bool:
    t = ctype.strip().rstrip("?")
    return "Dictionary" in t


def _spec_scalar_types(prop_schema: dict) -> set[str] | None:
    """Acceptable C# base type tokens for a scalar spec property, or None if the
    spec type is non-scalar (object/array) and should be checked structurally."""
    st = prop_schema.get("type")
    fmt = prop_schema.get("format")
    if isinstance(st, list):
        st = next((x for x in st if x != "null"), None)
    if st == "string":
        if fmt in ("date-time", "date"):
            return {"DateTimeOffset", "DateTime", "string"}
        return {"string"}
    if st == "integer":
        return {"int", "long", "Int32", "Int64"}
    if st == "number":
        return {"double", "float", "decimal"}
    if st == "boolean":
        return {"bool", "Boolean"}
    return None  # object / array / untyped


def type_mismatch(spec_prop: dict, sdk_prop: dict, resolver: SpecResolver) -> str | None:
    """Return a human reason if the SDK C# type is clearly wrong for the spec
    property type, else None. Conservative: only flags clear scalar/shape
    contradictions to avoid false positives on nested records."""
    ctype = sdk_prop["ctype"]
    base = _csharp_base(ctype)
    leaf = base.split(".")[-1].split("<")[0]

    sp = resolver.deref(spec_prop) or spec_prop
    st = sp.get("type")
    if isinstance(st, list):
        st = next((x for x in st if x != "null"), None)

    # Array expectations.
    if st == "array":
        if not _is_collection(ctype):
            # object/object? is an acceptable weak stand-in (caught elsewhere).
            if leaf == "object":
                return None
            return f"spec array but SDK type '{ctype}' is not a collection"
        return None
    # Object expectations: SDK may use a nested record or object — don't flag
    # unless SDK used a scalar where spec is a structured object.
    if st == "object" or (st is None and "properties" in sp):
        if leaf in ("string", "int", "long", "double", "float", "bool", "decimal",
                     "DateTimeOffset", "DateTime"):
            return f"spec object but SDK type '{ctype}' is scalar"
        return None

    # Scalar expectations.
    allowed = _spec_scalar_types(sp)
    if allowed is None:
        return None
    if leaf in allowed:
        return None
    if leaf == "object":
        return None  # weak typing handled as its own finding
    if _is_collection(ctype) or _is_dictionary(ctype):
        return f"spec scalar {st}/{sp.get('format')} but SDK type '{ctype}' is a collection/map"
    # Enums modeled as string in SDK are fine; custom record for a scalar is a
    # genuine mismatch only when spec is a primitive and SDK is clearly numeric
    # vs string.
    if "string" in allowed and leaf not in allowed:
        # SDK might use an enum type (rendered as string). Treat unknown
        # PascalCase leaf as a possible enum -> not a hard mismatch.
        if leaf[:1].isupper():
            return None
        return f"spec {st} but SDK type '{ctype}'"
    if leaf[:1].isupper():
        return None  # nested/enum record — shallow check, don't over-flag
    return f"spec {st}/{sp.get('format')} but SDK type '{ctype}'"


# ============================================================================
# Findings
# ============================================================================
class Finding:
    __slots__ = ("severity", "ftype", "sdk_type", "prop", "detail", "endpoint", "tag")

    def __init__(self, severity, ftype, sdk_type, prop, detail, endpoint, tag):
        self.severity = severity
        self.ftype = ftype
        self.sdk_type = sdk_type
        self.prop = prop
        self.detail = detail
        self.endpoint = endpoint
        self.tag = tag

    def key(self):
        return (self.sdk_type, self.prop, self.ftype)


def is_weak_type(ctype: str | None) -> bool:
    if not ctype:
        return False
    t = ctype.strip().rstrip("?").strip()
    return t in ("object", "JsonElement", "JsonNode", "JsonObject")


# Known intentional SDK->spec flattening (driverId vs driver.id, etc.) keeps
# such findings capped. We detect them heuristically: an EXTRA SDK scalar whose
# name == <specObjectProp> + "Id"/"Name" while spec has a nested object of that
# base name. Handled inline in compare_record.
def compare_record(
    sdk_record_name: str,
    sdk_props: dict[str, dict] | None,
    inner_schema: dict | None,
    spec_required: set[str],
    resolver: SpecResolver,
    side: str,  # 'request' or 'response'
    endpoint: str,
    tag: str,
    beta_capped: bool,
    findings: list[Finding],
):
    if not isinstance(inner_schema, dict):
        return
    spec_props = inner_schema.get("properties")
    if not isinstance(spec_props, dict):
        return

    cap = (lambda s: _cap(s, beta_capped))

    # Weakly typed SDK side (object?) where spec has a concrete schema.
    if sdk_props is None:
        findings.append(Finding(
            cap("MEDIUM"), "weak-typing", sdk_record_name or "object", "*",
            f"{side} is weakly typed (object) but spec defines "
            f"{len(spec_props)} properties", endpoint, tag))
        return

    sdk_json_names = set(sdk_props.keys())
    spec_names = set(spec_props.keys())

    # Spec -> SDK: MISSING / mismatch / required drift.
    for sname, sschema in spec_props.items():
        sschema_d = resolver.deref(sschema) or sschema
        is_req = sname in spec_required
        if sname not in sdk_json_names:
            # Flattened-object heuristic: spec nested object whose scalar parts
            # the SDK promoted to <name><Field>.
            stype = sschema_d.get("type")
            flattened = False
            if stype == "object" or "properties" in sschema_d:
                for child in (sschema_d.get("properties") or {}):
                    if (sname + child[:1].upper() + child[1:]) in sdk_json_names \
                       or any(n.lower() == (sname + child).lower() for n in sdk_json_names):
                        flattened = True
                        break
            if flattened:
                findings.append(Finding(
                    cap("LOW"), "flattened-nested", sdk_record_name, sname,
                    f"spec nested object '{sname}' flattened into scalar(s) on SDK "
                    f"(known/expected)", endpoint, tag))
                continue
            if side == "request" and is_req:
                findings.append(Finding(
                    cap("HIGH"), "missing-required-request", sdk_record_name, sname,
                    f"spec REQUIRED request field '{sname}' absent from SDK record",
                    endpoint, tag))
            elif side == "response" and is_req:
                findings.append(Finding(
                    cap("HIGH"), "missing-required-response", sdk_record_name, sname,
                    f"SDK response record drops spec REQUIRED field '{sname}'",
                    endpoint, tag))
            else:
                findings.append(Finding(
                    cap("MEDIUM"), "missing-optional", sdk_record_name, sname,
                    f"spec optional field '{sname}' absent from SDK ({side})",
                    endpoint, tag))
            continue

        sdk_prop = sdk_props[sname]
        # Type mismatch (shallow).
        reason = type_mismatch(sschema_d, sdk_prop, resolver)
        if reason:
            findings.append(Finding(
                cap("MEDIUM"), "type-mismatch", sdk_record_name, sname,
                reason, endpoint, tag))
        # Weak object where spec concrete.
        elif is_weak_type(sdk_prop["ctype"]):
            sd_type = sschema_d.get("type")
            if sd_type == "object" or "properties" in sschema_d or (
                sd_type == "array"
            ):
                # Only note when the spec actually has structure to model.
                inner_obj = sschema_d
                if sd_type == "array":
                    inner_obj = resolver.deref(sschema_d.get("items")) or {}
                if isinstance(inner_obj, dict) and inner_obj.get("properties"):
                    findings.append(Finding(
                        cap("MEDIUM"), "weak-typing", sdk_record_name, sname,
                        f"SDK uses weak '{sdk_prop['ctype']}' but spec '{sname}' has a "
                        f"concrete schema", endpoint, tag))

        # Required-ness drift.
        if side == "request":
            if is_req and not sdk_prop["required"]:
                findings.append(Finding(
                    cap("HIGH"), "required-drift-request", sdk_record_name, sname,
                    f"spec REQUIRES request '{sname}' but SDK property is not 'required'",
                    endpoint, tag))
            elif not is_req and sdk_prop["required"]:
                findings.append(Finding(
                    cap("LOW"), "over-tightened", sdk_record_name, sname,
                    f"SDK marks '{sname}' 'required' but spec lists it optional ({side})",
                    endpoint, tag))
        else:  # response
            if not is_req and sdk_prop["required"]:
                findings.append(Finding(
                    cap("LOW"), "over-tightened", sdk_record_name, sname,
                    f"SDK marks response '{sname}' 'required' but spec lists it optional",
                    endpoint, tag))

    # SDK -> spec: EXTRA.
    for sdk_name in sdk_json_names - spec_names:
        findings.append(Finding(
            cap("LOW"), "extra-property", sdk_record_name, sdk_name,
            f"SDK property '{sdk_name}' not present in spec {side} schema",
            endpoint, tag))


def _cap(severity: str, beta_capped: bool) -> str:
    if beta_capped and SEV_RANK[severity] < SEV_RANK["MEDIUM"]:
        return "MEDIUM"
    return severity


# ============================================================================
# Query parameter comparison
# ============================================================================
def compare_query_params(op, op_uses_paginate, info, endpoint, tag, beta_capped, findings):
    params = op.get("parameters", []) or []
    if info.get("delegates_filter"):
        return
    sdk_params = info.get("params", {})
    emitted_keys = info.get("emitted_keys", set())
    body_timerange = info.get("body_uses_timerange", False)
    for p in params:
        if not isinstance(p, dict) or p.get("in") != "query":
            continue
        name = p.get("name", "")
        if not name:
            continue
        lname = name.lower()
        required = bool(p.get("required"))
        if lname in PAGINATION_PARAMS and op_uses_paginate:
            continue
        if lname in TIMERANGE_PARAMS and body_timerange:
            continue
        # Match by camel/Pascal-insensitive C# parameter name...
        if lname in sdk_params:
            continue
        # ...or by the literal query key the method emits (covers C# params
        # renamed on the wire, e.g. startTime -> ("startMs", ...)).
        if lname in emitted_keys:
            continue
        # The QueryBuilder string params are positional tuples in-method, not C#
        # parameters; only flag REQUIRED params the method clearly can't supply.
        if required:
            findings.append(Finding(
                _cap("HIGH", beta_capped), "missing-required-query",
                endpoint.split("::")[0], name,
                f"spec REQUIRES query param '{name}' but no matching SDK method parameter",
                endpoint, tag))


# ============================================================================
# Main analysis
# ============================================================================
def analyze(spec: dict):
    resolver = SpecResolver(spec)
    ops = cs.spec_operations(spec)            # (VERB, normpath) -> meta
    raw_ops = spec.get("paths", {})           # for full op objects
    eps = cs.sdk_endpoints()                  # list of {file, method, verb, path, rawarg}
    ctypes = parse_client_types()             # (file, method) -> types/params
    models = parse_models()                   # record -> props

    findings: list[Finding] = []
    examined = 0

    for e in eps:
        verb, np = e["verb"], e["path"]
        if np is None:
            continue
        key = (verb, np)
        if key not in ops:
            continue  # path mismatch handled by check-sdk-sync.py, not here
        meta = ops[key]
        tags = meta.get("tags") or ["Untagged"]
        tag = tags[0]
        beta_tag = any("beta" in t.lower() for t in tags)
        info = ctypes.get((e["file"], e["method"]), {})
        beta_client = info.get("is_beta", False)
        beta_capped = beta_tag or beta_client

        # Locate the full spec op object (by rawpath + verb).
        raw = raw_ops.get(meta["rawpath"], {})
        op = raw.get(verb.lower(), {}) if isinstance(raw, dict) else {}
        endpoint = f"{e['file']}::{e['method']} [{verb} {meta['rawpath']}]"

        op_uses_paginate = "Paginate" in (e.get("rawarg", "") or "") or bool(
            # PaginateAsync sets response via PAGINATE_GENERIC; detect via type capture
            info.get("response_type") and verb == "GET" and _method_paginates(e["file"], e["method"])
        )

        # ---- Request side (Post/Patch/Put) -------------------------------
        if verb in ("POST", "PATCH", "PUT"):
            req_schema_top = resolver.op_schema(op, "request")
            if req_schema_top is not None:
                inner, is_list, req_required = resolver.unwrap_envelope(req_schema_top)
                req_type = info.get("request_type")
                # Wrapper-shape check: spec wants { data: ... } envelope.
                if resolver.wrapper_is_data_enveloped(req_schema_top):
                    sdk_req_props = models.get(_record_key(req_type or ""))
                    has_data = bool(sdk_req_props and "data" in sdk_req_props)
                    if req_type and not is_weak_type(req_type) and not has_data:
                        findings.append(Finding(
                            "CRITICAL", "wrapper-shape", req_type or "?", "data",
                            f"spec request expects {{ data: T{'[]' if resolver.data_is_array(req_schema_top) else ''} }} "
                            f"envelope but SDK posts bare record '{req_type}' (no 'data' member)",
                            endpoint, tag))
                    elif has_data:
                        # Envelope present — compare the INNER record against inner schema.
                        data_ctype = sdk_req_props["data"]["ctype"]
                        inner_rec = _strip_generic_suffix(_collection_element(data_ctype))
                        compare_record(
                            inner_rec, models.get(inner_rec), inner, req_required,
                            resolver, "request", endpoint, tag, beta_capped, findings)
                else:
                    examined += 1
                    if req_type is None:
                        pass  # no typed body param (e.g. builds inline) — skip
                    elif is_weak_type(req_type):
                        compare_record(req_type, None, inner, req_required,
                                       resolver, "request", endpoint, tag, beta_capped, findings)
                    else:
                        rec = _record_key(req_type)
                        sdk_props = models.get(rec)
                        rec_label = rec if sdk_props is not None else _strip_generic_suffix(req_type)
                        if sdk_props is not None and isinstance(inner, dict):
                            inner, req_required = resolver.resolve_named_wrapper(
                                inner, set(sdk_props.keys()))
                        compare_record(rec_label, sdk_props, inner, req_required,
                                       resolver, "request", endpoint, tag, beta_capped, findings)

        # ---- Response side ----------------------------------------------
        resp_schema_top = resolver.op_schema(op, "response")
        resp_type = info.get("response_type")
        if resp_schema_top is not None and resp_type:
            inner, is_list, resp_required = resolver.unwrap_envelope(resp_schema_top)
            examined += 1
            if is_weak_type(resp_type):
                # Beta clients intentionally weak — cap at MEDIUM (done via beta_capped
                # only if beta; otherwise still note as weak-typing MEDIUM).
                if isinstance(inner, dict) and inner.get("properties"):
                    findings.append(Finding(
                        _cap("MEDIUM", beta_capped), "weak-typing", "object", "*",
                        f"response weakly typed (object) but spec defines "
                        f"{len(inner.get('properties'))} properties", endpoint, tag))
            else:
                rec = _record_key(resp_type)
                sdk_props = models.get(rec)
                # Keep the descriptive (possibly generic) type in findings when the
                # stripped key does not resolve to a declared record.
                rec_label = rec if sdk_props is not None else _strip_generic_suffix(resp_type)
                # Endpoint-aware named-list-wrapper resolution (Fuel vs Media).
                if sdk_props is not None and isinstance(inner, dict):
                    inner, resp_required = resolver.resolve_named_wrapper(
                        inner, set(sdk_props.keys()))
                compare_record(rec_label, sdk_props, inner, resp_required,
                               resolver, "response", endpoint, tag, beta_capped, findings)

        # ---- Query params -----------------------------------------------
        compare_query_params(op, op_uses_paginate, info, endpoint, tag, beta_capped, findings)

    return findings, examined, len(eps)


_PAGINATE_CACHE: dict[tuple[str, str], bool] = {}


def _method_paginates(file_name: str, method: str) -> bool:
    k = (file_name, method)
    if k in _PAGINATE_CACHE:
        return _PAGINATE_CACHE[k]
    result = False
    for path in SDK_CLIENTS.rglob(file_name):
        text = path.read_text()
        hm = re.search(
            r'public\s+(?:async\s+)?[\w<>,\.\?\[\]\s]+?\s+'
            + re.escape(method[:-5]) + r'Async\s*\(', text)
        if hm:
            tail = text[hm.end():hm.end() + 1200]
            result = "PaginateAsync" in tail or "PaginateDataAsync" in tail
        break
    _PAGINATE_CACHE[k] = result
    return result


def _collection_element(ctype: str) -> str:
    """Element type of a collection C# type, else the type itself."""
    m = re.search(r'<\s*([A-Za-z0-9_\.]+)\s*>', ctype)
    if m:
        return m.group(1)
    if ctype.endswith("[]"):
        return ctype[:-2]
    return ctype


# ============================================================================
# Dedup + reporting
# ============================================================================
def dedup(findings: list[Finding]):
    grouped: dict[tuple, dict] = {}
    for f in findings:
        k = f.key()
        if k not in grouped:
            grouped[k] = {
                "severity": f.severity, "ftype": f.ftype, "sdk_type": f.sdk_type,
                "prop": f.prop, "detail": f.detail, "tag": f.tag,
                "endpoints": [],
            }
        g = grouped[k]
        # Keep the most-severe label for the group.
        if SEV_RANK[f.severity] < SEV_RANK[g["severity"]]:
            g["severity"] = f.severity
        if f.endpoint not in g["endpoints"]:
            g["endpoints"].append(f.endpoint)
    return list(grouped.values())


def report_human(groups, examined, n_eps, by_domain: bool):
    by_sev = defaultdict(int)
    by_type = defaultdict(int)
    domain = defaultdict(lambda: defaultdict(int))
    for g in groups:
        by_sev[g["severity"]] += 1
        by_type[g["ftype"]] += 1
        domain[g["tag"]][g["severity"]] += 1

    line = "=" * 72
    print(line)
    print("Samsara SDK <-> Spec MODEL parity check")
    print(line)
    print(f"SDK endpoints scanned:     {n_eps}")
    print(f"Endpoint bodies compared:  {examined}")
    print(f"Findings (deduped):        {len(groups)}")
    print()
    print("By severity:")
    for s in SEVERITIES:
        print(f"  {s:9} {by_sev.get(s, 0)}")
    print()
    print("By finding type:")
    for t in sorted(by_type, key=lambda x: -by_type[x]):
        print(f"  {t:28} {by_type[t]}")

    print()
    print("Per-domain (spec tag):")
    width = max((len(d) for d in domain), default=6)
    width = max(width, 6)
    header = f"  {'domain':<{width}}  " + "  ".join(f"{s[:4]:>5}" for s in SEVERITIES) + "   total"
    print(header)
    print("  " + "-" * (len(header) - 2))
    for d in sorted(domain, key=lambda x: (-sum(domain[x].values()), x)):
        cells = "  ".join(f"{domain[d].get(s, 0):>5}" for s in SEVERITIES)
        total = sum(domain[d].values())
        print(f"  {d:<{width}}  {cells}   {total:>5}")

    # Always surface CRITICAL/HIGH in detail (these are the gate-worthy ones).
    crit_high = [g for g in groups if g["severity"] in ("CRITICAL", "HIGH")]
    if crit_high:
        print()
        print("--- CRITICAL / HIGH findings (detail) ---")
        for g in sorted(crit_high, key=lambda x: SEV_RANK[x["severity"]]):
            print(f"  [{g['severity']}] {g['ftype']} :: {g['sdk_type']}.{g['prop']}")
            print(f"      {g['detail']}")
            for ep in g["endpoints"][:6]:
                print(f"        - {ep}")
            if len(g["endpoints"]) > 6:
                print(f"        ... (+{len(g['endpoints']) - 6} more endpoints)")

    if by_domain:
        print()
        print("--- Findings grouped by domain ---")
        bd = defaultdict(list)
        for g in groups:
            bd[g["tag"]].append(g)
        for d in sorted(bd):
            print(f"\n### {d} ({len(bd[d])})")
            for g in sorted(bd[d], key=lambda x: SEV_RANK[x["severity"]]):
                print(f"  [{g['severity']:8}] {g['ftype']:24} {g['sdk_type']}.{g['prop']}")


def report_json(groups, examined, n_eps):
    print(json.dumps({
        "sdk_endpoints_scanned": n_eps,
        "bodies_compared": examined,
        "finding_count": len(groups),
        "by_severity": {
            s: sum(1 for g in groups if g["severity"] == s) for s in SEVERITIES
        },
        "findings": [
            {
                "severity": g["severity"],
                "type": g["ftype"],
                "sdk_type": g["sdk_type"],
                "property": g["prop"],
                "detail": g["detail"],
                "domain": g["tag"],
                "endpoints": g["endpoints"],
            }
            for g in sorted(groups, key=lambda x: (SEV_RANK[x["severity"]], x["sdk_type"], x["prop"]))
        ],
    }, indent=2))


def main() -> None:
    ap = argparse.ArgumentParser(description="Check SDK record shapes against the Samsara spec.")
    ap.add_argument("--spec-url", default=cs.SPEC_URL)
    ap.add_argument("--spec-file")
    ap.add_argument("--json", action="store_true", help="emit machine-readable JSON")
    ap.add_argument("--by-domain", action="store_true", help="also list findings grouped by spec tag")
    ap.add_argument("--fail-on-severity", choices=SEVERITIES,
                    help="exit 1 if any finding at this severity OR higher exists")
    args = ap.parse_args()

    spec = cs.load_spec(args.spec_url, args.spec_file)
    findings, examined, n_eps = analyze(spec)
    groups = dedup(findings)

    if args.json:
        report_json(groups, examined, n_eps)
    else:
        report_human(groups, examined, n_eps, args.by_domain)

    if args.fail_on_severity:
        threshold = SEV_RANK[args.fail_on_severity]
        worst = min((SEV_RANK[g["severity"]] for g in groups), default=99)
        if worst <= threshold:
            sys.exit(1)


if __name__ == "__main__":
    main()
