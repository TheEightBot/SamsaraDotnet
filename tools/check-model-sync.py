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
5. RECURSE. After comparing a record against its schema, descend into every
   property where BOTH sides have somewhere to go — the SDK type resolves to
   another declared SDK record, and the spec property resolves (deref'd,
   descending `items` for arrays) to another object schema — and compare that
   pair too, to arbitrary depth. Without this the checker was one level deep:
   a record reached only as a property of a property was never compared at
   all, so weak typing and property drift below the endpoint's top-level
   record were completely invisible. Cycles are stopped by a visited set keyed
   on (record, schema identity, side) plus a MAX_NEST_DEPTH cap; findings
   discovered N levels down still report the endpoint that reached them and
   carry the dotted property path in their detail.

Severity: CRITICAL / HIGH / MEDIUM / LOW (see classify()). Beta-tagged
endpoints and the `Clients/Beta/*` weak-typed clients cap at MEDIUM: the cap
only prevents a CRITICAL/HIGH *label*, it does not exempt anything from the
gate. With the CI gate at `--fail-on-severity MEDIUM`, a beta-capped finding
still fails the build exactly like any other MEDIUM finding.

Usage:
    python3 tools/check-model-sync.py [--spec-url URL | --spec-file PATH]
                                      [--json] [--json-file PATH] [--by-domain]
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

# ----------------------------------------------------------------- allowlist
# Findings that are DELIBERATE, not defects. Keyed by (sdk_type, property,
# finding_type) -> human reason. These are suppressed from the gating counts but
# printed in their own "Allowlisted (intentional)" section so they stay visible
# and reviewable. Anything NOT in this map is a real finding.
#
# RULE FOR ADDITIONS: only put something here once you've confirmed it is an
# intentional modelling decision (back-compat or deliberate over-tightening) —
# never to paper over a genuine drift. Each entry carries the rationale inline.
#
# RULE FOR `weak-typing` ENTRIES SPECIFICALLY: an entry is admissible ONLY if its
# reason cites the exact spec pointer (components.schemas.<Schema>.properties.<p>)
# proving the schema is genuinely FREE-FORM — i.e. a bare `{type: object}` with no
# `properties`, no `enum`, no `oneOf`/`anyOf`/`allOf`, and no meaningful
# `additionalProperties`. "Beta", "preview", "volatile", "not worth typing" are
# NOT admissible reasons: if the spec describes a shape, the SDK must model it.
#
# BLANKET / WILDCARD ENTRIES ARE FORBIDDEN. Every key must name a concrete
# (record, property) pair. A wildcard key such as ("object", "*", "weak-typing")
# collapses unrelated findings into one dedup group and silently suppresses an
# unbounded backlog — that exact entry hid 94 weakly-typed endpoint bodies and
# 93 weakly-typed properties, and was deleted for that reason.
ALLOWLIST: dict[tuple[str, str, str], str] = {
    # --- TrailerAssignment: v1 dual-envelope record, deliberately weak/back-compat ---
    # This single record deserializes BOTH v1 wrapper shapes (list endpoint
    # { pagination, trailers } and per-trailer endpoint { id, name,
    # trailerAssignments }). The 7 fields below are NOT in the current spec for
    # either shape; they are retained on the record for backward compatibility
    # with older Samsara v1 payloads and existing consumers. See
    # Models/Assignments/AssignmentModels.cs (TrailerAssignment).
    ("TrailerAssignment", "trailerId", "extra-property"):
        "back-compat: legacy v1 field, not in current spec (documented on record)",
    ("TrailerAssignment", "trailerName", "extra-property"):
        "back-compat: legacy v1 field, not in current spec (documented on record)",
    ("TrailerAssignment", "vehicleId", "extra-property"):
        "back-compat: legacy v1 field, not in current spec (documented on record)",
    ("TrailerAssignment", "vehicleName", "extra-property"):
        "back-compat: legacy v1 field, not in current spec (documented on record)",
    ("TrailerAssignment", "driverId", "extra-property"):
        "back-compat: legacy v1 field, not in current spec (documented on record)",
    ("TrailerAssignment", "startTime", "extra-property"):
        "back-compat: legacy v1 field, not in current spec (documented on record)",
    ("TrailerAssignment", "endTime", "extra-property"):
        "back-compat: legacy v1 field, not in current spec (documented on record)",
    # The list-shape `pagination` is a nested v1 envelope cursor the SDK models
    # as object? (the surrounding pagination is handled by the HTTP layer for
    # --- Tags: deliberate over-tightening ---
    # The shared Tag record marks id/name `required`: every tag the API returns
    # has both, so requiring them gives consumers non-null guarantees. The spec
    # lists them optional only because the Tag schema carries no `required`
    # block. Deliberate, not drift.
    ("Tag", "id", "over-tightened"):
        "intentional: every returned Tag has an id; SDK guarantees non-null",
    ("Tag", "name", "over-tightened"):
        "intentional: every returned Tag has a name; SDK guarantees non-null",
    # CreateTagRequest.name is required for POST /tags (create) where a name is
    # mandatory; the same record also serves PUT /tags/{id} (replace) where the
    # spec marks name optional. Requiring a name on replace is deliberate.
    ("CreateTagRequest", "name", "over-tightened"):
        "intentional: name required on create; shared with PUT replace where spec lists it optional",

    # --- Industrial: deliberate over-tightening ---
    # Every data-input data point returned by the snapshot/feed/history
    # endpoints carries an id; SDK requires it for a non-null guarantee. Spec
    # lists it optional. Deliberate.
    ("DataInputDataPoint", "id", "over-tightened"):
        "intentional: every returned data point has an id; SDK guarantees non-null",

    # --- Readings: the spec itself leaves these free-form ---
    # These are the ONLY weakly-typed properties the spec justifies: each is a
    # bare `{type: object}` with no properties/enum/composition, so JsonElement is
    # the honest C# type. Pointers verified against components.schemas.
    ("ReadingDefinition", "type", "weak-typing"):
        "spec components.schemas.ReadingDefinitionResponseBody.properties.type is a free-form {type: object} (no properties) — JsonElement is the honest type",
    ("ReadingHistory", "value", "weak-typing"):
        "spec components.schemas.ReadingHistoryResponseBody.properties.value is free-form {type: object} — value shape depends on the reading's dataType",
    ("ReadingSnapshot", "value", "weak-typing"):
        "spec components.schemas.ReadingSnapshotResponseBody.properties.value is free-form {type: object} — value shape depends on the reading's dataType",
}


def allowlist_reason(sdk_type: str, prop: str, ftype: str) -> str | None:
    return ALLOWLIST.get((sdk_type, prop, ftype))

# Maximum nesting depth for the recursive record<->schema descent (see
# analyze()._descend). Records reference one another and the spec has
# self-referential schemas, so the descent needs a hard stop in addition to the
# visited set. 12 is far deeper than any real Samsara payload (the deepest
# observed is ~4: SafetySettings -> harshEventSensitivityV2 -> harshAccel ->
# heavyDuty) while still being cheap.
MAX_NEST_DEPTH = 12

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
            # Multi-branch allOf is COMPOSITION: the effective object is the union
            # of every branch's properties (required = union of each branch's
            # required). Several v1 response bodies use this (e.g.
            # `V1TrailerBase` + `{ trailerAssignments }`, or
            # `Attribute` + `{ entities }` => `AttributeExpanded`). Without
            # merging, deref returns a propertyless schema and those composed
            # fields look "missing"/"extra". oneOf/anyOf are alternatives, not
            # composition, so we do NOT merge them. Already-deref'd branches keep
            # this O(branches) and bounded by `depth`.
            all_of = schema.get("allOf")
            if isinstance(all_of, list) and len(all_of) > 1:
                merged_props: dict = {}
                merged_required: list = []
                obj_type = None
                for branch in all_of:
                    bd = self.deref(branch, depth + 1)
                    if not isinstance(bd, dict):
                        continue
                    bp = bd.get("properties")
                    if isinstance(bp, dict):
                        merged_props.update(bp)
                    for r in bd.get("required", []) or []:
                        if r not in merged_required:
                            merged_required.append(r)
                    obj_type = obj_type or bd.get("type")
                if merged_props:
                    out = {"type": obj_type or "object", "properties": merged_props}
                    if merged_required:
                        out["required"] = merged_required
                    return out
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
# Generic helper call: HttpClient.<Verb>Async<TYPE>( ... )  -> capture TYPE.
#
# The alternation must spell out EVERY public generic helper on
# src/Samsara.Sdk/Http/SamsaraHttpClient.cs, longest name first. A helper the
# regex does not know about makes its endpoint invisible to this checker: the
# response type is never resolved, so neither the response NOR the request body
# is ever compared against the spec. `PostListDataAsync` was missing, which hid
# POST /hub/locations (HubsClient.CreateLocationAsync) entirely.
GENERIC_HELPER_RE = re.compile(
    r'HttpClient\.(GetPage|GetData|Get|PostListData|PostData|Post'
    r'|PatchData|Patch|PutData|Put)Async'
    r'<((?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*)>\('
)
PAGINATE_GENERIC_RE = re.compile(
    r'Paginate(?:Data)?Async<((?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*)>\('
)
# Body-bearing helper call capturing verb + full arg list start. Covers every
# public helper that takes an `object body` argument, including the
# DeleteAsync(path, body, ct) overload.
BODY_HELPER_RE = re.compile(
    r'HttpClient\.(PostListData|PostData|Post|PatchData|Patch|PutData|Put'
    r'|Delete)Async'
    r'(?:<(?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*>)?\(\s*(.*)$',
    re.S,
)
# Parameter types that are never a request body (the bodyless DeleteAsync /
# PostAsync overloads put the cancellation token where a body would sit).
NON_BODY_PARAM_TYPES = {"CancellationToken", "System.Threading.CancellationToken"}
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
                # GetPageAsync<TData, TItem> mirrors PaginateAsync<TData, TItem>:
                # the last generic arg is the element type that maps to the
                # spec's paginated array item.
                gen_args = _split_generic_args(gm.group(2))
                response_type = gen_args[-1] if gen_args else gm.group(2).strip()
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
                        if request_type in NON_BODY_PARAM_TYPES:
                            # Bodyless overload, e.g. DeleteAsync(path, ct).
                            request_type = None
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
    # Weak types (object / JsonElement / IReadOnlyList<JsonElement> / ...) are
    # reported once, as `weak-typing`. Returning early keeps them from ALSO
    # surfacing as `type-mismatch` now that is_weak_type() recognises the
    # namespace-qualified and collection-wrapped spellings.
    if is_weak_type(ctype):
        return None
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


# Type tokens that carry NO schema information: the C# compiler cannot tell a
# caller anything about the payload's shape.
WEAK_LEAVES = {"object", "JsonElement", "JsonNode", "JsonObject", "JsonDocument"}
# Collection wrappers we peel exactly ONE level of: IReadOnlyList<JsonElement> is
# every bit as weakly typed as JsonElement.
_COLLECTION_WRAPPERS = {
    "IReadOnlyList", "IList", "List", "IEnumerable", "IReadOnlyCollection",
    "ICollection", "Collection",
}
_DICT_WRAPPERS = {"Dictionary", "IDictionary", "IReadOnlyDictionary"}
_GENERIC_RE = re.compile(r'^([A-Za-z0-9_\.]+)\s*<\s*(.+)\s*>$', re.S)


def _leaf_token(t: str) -> str:
    """Leaf of a possibly namespace-qualified type name (System.Text.Json.X -> X)."""
    return t.strip().split(".")[-1].strip()


def _strip_nullable(t: str) -> str:
    t = t.strip()
    while t.endswith("?"):
        t = t[:-1].strip()
    return t


def is_weak_type(ctype: str | None) -> bool:
    """True when the C# type conveys no shape at all.

    Recognises, beyond the bare tokens: nullable (`JsonElement?`),
    namespace-qualified (`System.Text.Json.JsonElement?`) and ONE level of
    collection wrapping (`IReadOnlyList<JsonElement>`, `object[]`). Dictionaries
    are deliberately NOT handled here — a `Dictionary<string, object>` is a
    legitimate model for a genuine free-form map, so it is only weak in context
    (see ``is_weak_dictionary``).
    """
    if not ctype:
        return False
    t = _strip_nullable(ctype)
    m = _GENERIC_RE.match(t)
    if m and _leaf_token(m.group(1)) in _COLLECTION_WRAPPERS:
        t = _strip_nullable(m.group(2))
    elif t.endswith("[]"):
        t = _strip_nullable(t[:-2])
    return _leaf_token(t) in WEAK_LEAVES


def is_weak_dictionary(ctype: str | None) -> bool:
    """True for `Dictionary<string, object>` / `IReadOnlyDictionary<string, JsonElement>`
    and friends — a map whose VALUE type is weak.

    A weak-valued map is the right model for a genuine free-form map, so callers
    must additionally confirm the spec property is a *structured* object (one
    with `properties`) before treating this as a finding.
    """
    if not ctype:
        return False
    t = _strip_nullable(ctype)
    m = _GENERIC_RE.match(t)
    if not m or _leaf_token(m.group(1)) not in _DICT_WRAPPERS:
        return False
    args = m.group(2)
    # Split on the top-level comma (value type may itself be generic).
    depth, split_at = 0, -1
    for i, ch in enumerate(args):
        if ch == "<":
            depth += 1
        elif ch == ">":
            depth -= 1
        elif ch == "," and depth == 0:
            split_at = i
            break
    if split_at < 0:
        return False
    return is_weak_type(args[split_at + 1:])


def _spec_is_concrete(schema: dict | None, resolver: SpecResolver, depth: int = 0) -> bool:
    """True when the spec schema describes a shape the SDK could model.

    CONCRETE: has `properties`, or an `enum`, or `oneOf`/`anyOf`/`allOf`
    composition, or a scalar `type` (string/integer/number/boolean), or
    `type: object` with a non-trivial `additionalProperties` schema. Arrays are
    judged by their element schema.

    FREE-FORM: a bare `{type: object}` with nothing else, or a schema with no
    `type` and no structure at all. For those, `JsonElement` is the honest C#
    type and the finding is only worth LOW (kept visible, not gate-worthy).
    """
    if depth > 8:
        return False
    sd = resolver.deref(schema) or schema
    if not isinstance(sd, dict):
        return False
    st = sd.get("type")
    if isinstance(st, list):
        st = next((x for x in st if x != "null"), None)
    if st == "array" or ("items" in sd and st is None):
        return _spec_is_concrete(sd.get("items"), resolver, depth + 1)
    if isinstance(sd.get("properties"), dict) and sd["properties"]:
        return True
    if sd.get("enum"):
        return True
    for key in ("oneOf", "anyOf", "allOf"):
        branch = sd.get(key)
        if isinstance(branch, list) and branch:
            return True
    if st in ("string", "integer", "number", "boolean"):
        return True
    if st == "object":
        ap = sd.get("additionalProperties")
        if isinstance(ap, dict) and ap:
            apd = resolver.deref(ap) or ap
            if isinstance(apd, dict) and (
                apd.get("properties") or apd.get("type") or apd.get("enum")
                or apd.get("oneOf") or apd.get("anyOf") or apd.get("allOf")
            ):
                return True
    return False


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
    union_spec_names: set[str] | None = None,
    path: str | None = None,
):
    """Compare one SDK record against one endpoint's inner spec schema.

    ``path`` is the dotted property path from the endpoint's TOP-LEVEL record
    down to this record (e.g. ``SafetySettings.harshEventSensitivityV2``). It is
    set only for records reached by the nested descent; when present, every
    finding's detail is suffixed with ``[at <path>.<prop>]`` so a defect found
    three levels down is still traceable back through the endpoint that reached
    it. Top-level comparisons pass ``None`` and their details are unchanged.

    ``union_spec_names`` is the UNION of every spec property name that appears on
    ANY endpoint (request or response) mapped to this same SDK record. A shared
    SDK record legitimately serves multiple endpoints with diverging shapes
    (e.g. the GET-stream response nests a ``driver`` object while the POST
    response returns a flat ``message``); a property valid on a *different*
    mapped endpoint must therefore NOT be flagged ``extra-property`` here. When
    provided, the EXTRA check uses this union so a property is only flagged when
    it is in NO mapped endpoint's schema. The MISSING / type / required checks
    deliberately stay per-endpoint (those are about THIS endpoint's contract).
    """
    if not isinstance(inner_schema, dict):
        return
    spec_props = inner_schema.get("properties")
    if not isinstance(spec_props, dict):
        return

    cap = (lambda s: _cap(s, beta_capped))
    # Property-path annotation, empty for top-level records so their existing
    # detail strings (and the drift report built from them) are untouched.
    at = (lambda p: f" [at {path}.{p}]" if path else "")

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
            child_props = sschema_d.get("properties") or {}
            matched: list[str] = []
            if stype == "object" or "properties" in sschema_d:
                sdk_by_lower = {n.lower(): n for n in sdk_json_names}
                for child in child_props:
                    # Promotion spellings, in order of confidence:
                    #   <name><Child>  e.g. driver.id  -> driverId
                    #   <name>_<child> e.g. driver.id  -> driver_id
                    #   <child>        e.g. driver.id  -> id   (ONLY when `child`
                    #                  is not itself a top-level spec property,
                    #                  which would make it a real sibling)
                    cands = [
                        (sname + child[:1].upper() + child[1:]).lower(),
                        f"{sname}_{child}".lower(),
                    ]
                    if child not in spec_names:
                        cands.append(child.lower())
                    for cand in cands:
                        hit = sdk_by_lower.get(cand)
                        if hit is not None:
                            matched.append(hit)
                            break
            if matched:
                findings.append(Finding(
                    cap("MEDIUM"), "flattened", sdk_record_name, sname,
                    f"spec nested object '{sname}' ({len(child_props)} props) has been "
                    f"flattened onto '{sdk_record_name}' as "
                    f"{', '.join(sorted(set(matched)))}{at(sname)}", endpoint, tag))
                continue
            if side == "request" and is_req:
                findings.append(Finding(
                    cap("HIGH"), "missing-required-request", sdk_record_name, sname,
                    f"spec REQUIRED request field '{sname}' absent from SDK record"
                    f"{at(sname)}", endpoint, tag))
            elif side == "response" and is_req:
                findings.append(Finding(
                    cap("HIGH"), "missing-required-response", sdk_record_name, sname,
                    f"SDK response record drops spec REQUIRED field '{sname}'"
                    f"{at(sname)}", endpoint, tag))
            else:
                findings.append(Finding(
                    cap("MEDIUM"), "missing-optional", sdk_record_name, sname,
                    f"spec optional field '{sname}' absent from SDK ({side})"
                    f"{at(sname)}", endpoint, tag))
            continue

        sdk_prop = sdk_props[sname]
        # Type mismatch (shallow).
        reason = type_mismatch(sschema_d, sdk_prop, resolver)
        if reason:
            findings.append(Finding(
                cap("MEDIUM"), "type-mismatch", sdk_record_name, sname,
                reason + at(sname), endpoint, tag))
        # Weak SDK type. Reported either way, but the severity depends on whether
        # the spec actually describes a shape: MEDIUM when it does (real work to
        # do), LOW when the spec is genuinely free-form (JsonElement is honest,
        # but the decision stays visible/allowlistable). Same ftype on both
        # branches so ALLOWLIST keys keep matching.
        elif is_weak_type(sdk_prop["ctype"]) or (
            is_weak_dictionary(sdk_prop["ctype"])
            and isinstance(sschema_d.get("properties"), dict)
            and sschema_d["properties"]
        ):
            if _spec_is_concrete(sschema_d, resolver):
                findings.append(Finding(
                    cap("MEDIUM"), "weak-typing", sdk_record_name, sname,
                    f"SDK uses weak '{sdk_prop['ctype']}' but spec '{sname}' has a "
                    f"concrete schema{at(sname)}", endpoint, tag))
            else:
                findings.append(Finding(
                    cap("LOW"), "weak-typing", sdk_record_name, sname,
                    f"SDK uses weak '{sdk_prop['ctype']}' and spec '{sname}' is "
                    f"free-form (no properties/enum/composition) — weak type is "
                    f"defensible; allowlist it with a spec pointer to accept"
                    f"{at(sname)}", endpoint, tag))

        # Required-ness drift.
        if side == "request":
            if is_req and not sdk_prop["required"]:
                findings.append(Finding(
                    cap("HIGH"), "required-drift-request", sdk_record_name, sname,
                    f"spec REQUIRES request '{sname}' but SDK property is not 'required'"
                    f"{at(sname)}", endpoint, tag))
            elif not is_req and sdk_prop["required"]:
                findings.append(Finding(
                    cap("LOW"), "over-tightened", sdk_record_name, sname,
                    f"SDK marks '{sname}' 'required' but spec lists it optional ({side})"
                    f"{at(sname)}", endpoint, tag))
        else:  # response
            if not is_req and sdk_prop["required"]:
                findings.append(Finding(
                    cap("LOW"), "over-tightened", sdk_record_name, sname,
                    f"SDK marks response '{sname}' 'required' but spec lists it optional"
                    f"{at(sname)}", endpoint, tag))

    # SDK -> spec: EXTRA. A property is only truly extra when it is absent from
    # the UNION of every endpoint mapped to this record (dual-shape records share
    # one C# type across endpoints whose schemas diverge — see docstring). Fall
    # back to this endpoint's schema only when no union was supplied.
    extra_baseline = union_spec_names if union_spec_names is not None else spec_names
    for sdk_name in sdk_json_names - extra_baseline:
        findings.append(Finding(
            cap("LOW"), "extra-property", sdk_record_name, sdk_name,
            f"SDK property '{sdk_name}' not present in spec schema of any endpoint "
            f"mapped to '{sdk_record_name}'{at(sdk_name)}",
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

    # Record comparisons are deferred into "jobs" so we can compute, per SDK
    # record, the UNION of spec property names across every endpoint mapped to
    # it BEFORE emitting extra-property findings. A shared C# record may back
    # several endpoints with diverging shapes; without the union, fields valid on
    # a sibling endpoint are mis-flagged as extras. See compare_record's
    # docstring. Each job carries everything compare_record needs.
    jobs: list[dict] = []
    # rec_label -> set of every spec property name seen on any mapped endpoint.
    record_union: dict[str, set[str]] = defaultdict(set)

    def _fold_nested_union(sdk_props, inner):
        """Contribute nested element-record property names to the union.

        A shared record can map to an endpoint not as the top-level body but
        NESTED inside another record's property (e.g. ``UserRole`` lives in
        ``CreateUserRequest.roles[]``; ``MediaRetrieval`` lives in
        ``MediaRetrievalListResponse.media[]``). For such a property, the spec's
        same-named member carries the element's real schema. Folding its
        property names into that element record's union lets the EXTRA check see
        fields the element legitimately has on THIS (nested) endpoint shape,
        exactly as the top-level union does for direct endpoints. Only descends
        one level into record-typed SDK properties — enough for the SDK's
        single-level request/response nesting and bounded work."""
        if not (isinstance(sdk_props, dict) and isinstance(inner, dict)):
            return
        spec_props = inner.get("properties")
        if not isinstance(spec_props, dict):
            return
        for pname, pmeta in sdk_props.items():
            if pname not in spec_props:
                continue
            elem = _record_key(_collection_element(pmeta["ctype"]))
            if elem not in models:
                continue  # not a known SDK record — nothing to fold
            child = resolver.deref(spec_props[pname])
            if not isinstance(child, dict):
                continue
            # Descend an array's items to the element object schema if needed.
            if child.get("type") == "array" or "items" in child:
                child = resolver.deref(child.get("items"))
            if isinstance(child, dict) and isinstance(child.get("properties"), dict):
                record_union[elem] |= set(child["properties"].keys())

    # Visited guard for the nested descent, keyed on
    # (record_name, schema_identity, side). Two endpoints that reach the same
    # record through the same schema would produce byte-identical findings and
    # an identical union contribution, so the second descent is pure duplicate
    # work; the FIRST endpoint to reach it keeps the attribution (dedup() then
    # merges endpoint lists across the whole run anyway). `side` is part of the
    # key because required-ness drift is asymmetric between request and
    # response. This set is also what makes a self-referential schema
    # terminate — MAX_NEST_DEPTH is the belt-and-braces second stop.
    visited_nested: set[tuple[str, str, str]] = set()

    def _child_object_schema(raw):
        """Resolve a spec PROPERTY schema to the object schema an SDK record models.

        Follows ``$ref`` chains and descends ``items`` for (nested) arrays, so
        both ``{"$ref": Foo}`` and ``{"type":"array","items":{"$ref":Foo}}``
        resolve to Foo. Returns ``(schema, ref_name | None)``; ``(None, None)``
        when the target is not an object schema with properties (scalars, enums
        and free-form ``{type: object}`` have nothing to recurse into — those are
        handled by the per-property checks in compare_record)."""
        ref = raw.get("$ref", "").split("/")[-1] if isinstance(raw, dict) else None
        s = resolver.deref(raw)
        hops = 0
        while isinstance(s, dict) and (s.get("type") == "array" or "items" in s) and hops < 4:
            item = s.get("items")
            if isinstance(item, dict) and "$ref" in item:
                ref = item["$ref"].split("/")[-1]
            s = resolver.deref(item)
            hops += 1
        if isinstance(s, dict) and isinstance(s.get("properties"), dict) and s["properties"]:
            return s, ref
        return None, None

    def _descend(rec_label, sdk_props, inner, side, endpoint, tag, beta_capped,
                 path, depth):
        """Queue a comparison for every nested (SDK record, spec object) pair.

        This is the fix for the checker's original one-level-deep blind spot: a
        record reached only as a property of a property (e.g.
        ``SafetySettings.distractedDrivingDetectionAlerts
        .inattentiveDrivingDetectionAlerts``) was never compared against its
        spec schema at all, so weak typing and property drift down there were
        invisible. We recurse whenever BOTH sides have somewhere to go: the SDK
        property's type resolves to a declared SDK record, and the spec property
        resolves to an object schema with properties."""
        if depth >= MAX_NEST_DEPTH:
            return
        if not (isinstance(sdk_props, dict) and isinstance(inner, dict)):
            return
        spec_props = inner.get("properties")
        if not isinstance(spec_props, dict):
            return
        for pname, pmeta in sdk_props.items():
            raw = spec_props.get(pname)
            if raw is None:
                continue  # SDK-only property — extra-property check covers it
            child_rec = _record_key(_collection_element(pmeta["ctype"]))
            child_props = models.get(child_rec)
            if child_props is None:
                continue  # scalar, weak type, or an unknown type — nothing to recurse into
            child_schema, ref = _child_object_schema(raw)
            if child_schema is None:
                continue
            # Identity: the schema's $ref name when it has one, else the shape's
            # property-name signature. Same identity => same property set =>
            # same union contribution, which is what makes skipping safe.
            sig = ref or ",".join(sorted(child_schema["properties"]))
            vkey = (child_rec, sig, side)
            if vkey in visited_nested:
                continue
            visited_nested.add(vkey)
            _queue(child_rec, child_props, child_schema,
                   set(child_schema.get("required", []) or []),
                   side, endpoint, tag, beta_capped,
                   path=f"{path}.{pname}", depth=depth + 1)

    def _queue(rec_label, sdk_props, inner, required, side, endpoint, tag, beta_capped,
               path=None, depth=0):
        if isinstance(inner, dict):
            sp = inner.get("properties")
            if isinstance(sp, dict):
                record_union[rec_label] |= set(sp.keys())
            else:
                record_union[rec_label]  # touch so key exists (empty union)
        _fold_nested_union(sdk_props, inner)
        jobs.append({
            "rec_label": rec_label, "sdk_props": sdk_props, "inner": inner,
            "required": required, "side": side, "endpoint": endpoint,
            "tag": tag, "beta_capped": beta_capped,
            # None for top-level records so their finding details stay unchanged.
            "path": path,
        })
        # Recurse. Queueing happens in pass 1 (this pass) precisely so that
        # record_union is COMPLETE — including nested records — before pass 2
        # emits any extra-property finding.
        _descend(rec_label, sdk_props, inner, side, endpoint, tag, beta_capped,
                 path or rec_label, depth)

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

        # ---- Request side (Post/Patch/Put/Delete) ------------------------
        # DELETE is included because SamsaraHttpClient exposes a
        # DeleteAsync(path, body, ct) overload and the spec defines request
        # bodies on four DELETE operations.
        if verb in ("POST", "PATCH", "PUT", "DELETE"):
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
                        _queue(inner_rec, models.get(inner_rec), inner, req_required,
                               "request", endpoint, tag, beta_capped)
                else:
                    examined += 1
                    if req_type is None:
                        pass  # no typed body param (e.g. builds inline) — skip
                    elif is_weak_type(req_type):
                        # Whole-body weak typing. The dedup key MUST be per
                        # endpoint-method (see the response site below), otherwise
                        # every weakly-typed body in the SDK collapses into one
                        # finding that a single allowlist entry can hide.
                        if isinstance(inner, dict) and inner.get("properties"):
                            findings.append(Finding(
                                _cap("MEDIUM", beta_capped), "weak-typing",
                                f"{e['file']}::{e['method']}", "<request>",
                                f"request body weakly typed ('{req_type}') but spec "
                                f"defines {len(inner['properties'])} properties",
                                endpoint, tag))
                    else:
                        rec = _record_key(req_type)
                        sdk_props = models.get(rec)
                        rec_label = rec if sdk_props is not None else _strip_generic_suffix(req_type)
                        if sdk_props is not None and isinstance(inner, dict):
                            inner, req_required = resolver.resolve_named_wrapper(
                                inner, set(sdk_props.keys()))
                        _queue(rec_label, sdk_props, inner, req_required,
                               "request", endpoint, tag, beta_capped)

        # ---- Response side ----------------------------------------------
        resp_schema_top = resolver.op_schema(op, "response")
        resp_type = info.get("response_type")
        if resp_schema_top is not None and resp_type:
            inner, is_list, resp_required = resolver.unwrap_envelope(resp_schema_top)
            examined += 1
            if is_weak_type(resp_type):
                # Keyed per endpoint-method, NOT by the literal ("object", "*"):
                # Finding.key() is (sdk_type, prop, ftype), so the old literal
                # deduped ~94 distinct weakly-typed responses into a single group
                # — which is precisely how one blanket allowlist entry hid them
                # all. Severity is unchanged (beta cap keeps it at MEDIUM).
                if isinstance(inner, dict) and inner.get("properties"):
                    findings.append(Finding(
                        _cap("MEDIUM", beta_capped), "weak-typing",
                        f"{e['file']}::{e['method']}", "<response>",
                        f"response weakly typed ('{resp_type}') but spec defines "
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
                _queue(rec_label, sdk_props, inner, resp_required,
                       "response", endpoint, tag, beta_capped)

        # ---- Query params -----------------------------------------------
        compare_query_params(op, op_uses_paginate, info, endpoint, tag, beta_capped, findings)

    # Second pass: now that record_union holds the cross-endpoint property union
    # for every shared record, emit the per-endpoint comparisons. The union is
    # passed so extra-property fires ONLY when a property is in NO mapped
    # endpoint's schema; MISSING/type/required checks remain per-endpoint.
    for j in jobs:
        compare_record(
            j["rec_label"], j["sdk_props"], j["inner"], j["required"],
            resolver, j["side"], j["endpoint"], j["tag"], j["beta_capped"],
            findings, union_spec_names=record_union.get(j["rec_label"]),
            path=j["path"])

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


def split_allowlisted(groups):
    """Partition deduped groups into (active, allowlisted).

    `active` is the set of REAL findings used for the headline counts and the
    severity gate. `allowlisted` carries the documented, intentional ones with
    their `reason` attached, reported separately so they stay visible and are
    never silently dropped."""
    active, allowlisted = [], []
    for g in groups:
        reason = allowlist_reason(g["sdk_type"], g["prop"], g["ftype"])
        if reason is None:
            active.append(g)
        else:
            g = dict(g)
            g["reason"] = reason
            allowlisted.append(g)
    return active, allowlisted


def report_human(groups, examined, n_eps, by_domain: bool, allowlisted=None):
    allowlisted = allowlisted or []
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
    print(f"Active findings (deduped): {len(groups)}")
    print(f"Allowlisted (intentional): {len(allowlisted)}")
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

    # Allowlisted, intentional findings — always shown so they stay reviewable.
    if allowlisted:
        print()
        print(f"--- Allowlisted (intentional, suppressed from gate): {len(allowlisted)} ---")
        for g in sorted(allowlisted, key=lambda x: (x["sdk_type"], x["prop"], x["ftype"])):
            print(f"  [{g['severity']}] {g['ftype']} :: {g['sdk_type']}.{g['prop']}")
            print(f"      reason: {g['reason']}")


# Endpoint lists are capped in JSON output: a weakly-typed shared record can be
# reachable from dozens of endpoints, and the full cross-product bloats the file
# the drift-report builder consumes. The untruncated count is kept alongside.
MAX_JSON_ENDPOINTS = 5


def _endpoint_block(g: dict) -> dict:
    eps = g["endpoints"]
    out = {"endpoints": eps[:MAX_JSON_ENDPOINTS], "endpoint_count": len(eps)}
    if len(eps) > MAX_JSON_ENDPOINTS:
        out["endpoints_truncated"] = len(eps) - MAX_JSON_ENDPOINTS
    return out


def build_json_payload(groups, examined, n_eps, allowlisted=None,
                       fail_on_severity: str | None = None) -> dict:
    """Machine-readable summary. Shared by --json (stdout) and --json-file."""
    allowlisted = allowlisted or []
    worst_rank = min((SEV_RANK[g["severity"]] for g in groups), default=None)
    worst = SEVERITIES[worst_rank] if worst_rank is not None else "NONE"
    failed = bool(
        fail_on_severity and worst_rank is not None
        and worst_rank <= SEV_RANK[fail_on_severity]
    )
    by_type: dict[str, int] = defaultdict(int)
    for g in groups:
        by_type[g["ftype"]] += 1
    return {
        "spec_source": getattr(cs, "LAST_SPEC_SOURCE", "unknown"),
        "sdk_endpoints_scanned": n_eps,
        "bodies_compared": examined,
        "finding_count": len(groups),
        "allowlisted_count": len(allowlisted),
        "by_severity": {
            s: sum(1 for g in groups if g["severity"] == s) for s in SEVERITIES
        },
        "by_type": dict(sorted(by_type.items(), key=lambda kv: (-kv[1], kv[0]))),
        "gate": {
            "threshold": fail_on_severity,
            "worst": worst,
            "failed": failed,
        },
        "findings": [
            {
                "severity": g["severity"],
                "type": g["ftype"],
                "ftype": g["ftype"],
                "sdk_type": g["sdk_type"],
                "property": g["prop"],
                "prop": g["prop"],
                "detail": g["detail"],
                "domain": g["tag"],
                **_endpoint_block(g),
            }
            for g in sorted(groups, key=lambda x: (SEV_RANK[x["severity"]], x["sdk_type"], x["prop"]))
        ],
        "allowlisted": [
            {
                "severity": g["severity"],
                "type": g["ftype"],
                "ftype": g["ftype"],
                "sdk_type": g["sdk_type"],
                "property": g["prop"],
                "prop": g["prop"],
                "domain": g["tag"],
                "reason": g["reason"],
                **_endpoint_block(g),
            }
            for g in sorted(allowlisted, key=lambda x: (x["sdk_type"], x["prop"], x["ftype"]))
        ],
    }


def report_json(groups, examined, n_eps, allowlisted=None, fail_on_severity=None):
    print(json.dumps(
        build_json_payload(groups, examined, n_eps, allowlisted, fail_on_severity),
        indent=2))


def main() -> None:
    ap = argparse.ArgumentParser(description="Check SDK record shapes against the Samsara spec.")
    ap.add_argument("--spec-url", default=cs.SPEC_URL)
    ap.add_argument("--spec-file")
    ap.add_argument("--json", action="store_true", help="emit machine-readable JSON")
    ap.add_argument("--json-file", metavar="PATH",
                    help="also write the machine-readable JSON payload to PATH "
                         "(so one run serves both the human log and the report builder)")
    ap.add_argument("--by-domain", action="store_true", help="also list findings grouped by spec tag")
    ap.add_argument("--fail-on-severity", choices=SEVERITIES,
                    help="exit 1 if any finding at this severity OR higher exists")
    args = ap.parse_args()

    spec = cs.load_spec(args.spec_url, args.spec_file)
    findings, examined, n_eps = analyze(spec)
    groups = dedup(findings)
    # Intentional, documented findings are partitioned out: they remain visible
    # in the report but do NOT count toward the headline totals or the gate.
    active, allowlisted = split_allowlisted(groups)

    if args.json:
        report_json(active, examined, n_eps, allowlisted, args.fail_on_severity)
    else:
        report_human(active, examined, n_eps, args.by_domain, allowlisted)

    if args.json_file:
        payload = build_json_payload(active, examined, n_eps, allowlisted,
                                     args.fail_on_severity)
        out = Path(args.json_file)
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(json.dumps(payload, indent=2) + "\n")

    if args.fail_on_severity:
        threshold = SEV_RANK[args.fail_on_severity]
        worst = min((SEV_RANK[g["severity"]] for g in active), default=99)
        if worst <= threshold:
            sys.exit(1)


if __name__ == "__main__":
    main()
