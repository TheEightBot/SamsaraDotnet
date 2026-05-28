# Trailer Assignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/46-trailer-assignments.md`](../46-trailer-assignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `8eb19a8` on 2026-05-27**

## Implementation notes

The SDK's `TrailerAssignment` is a single v1 "kitchen-sink" record that has to
deserialize BOTH v1 wrapper shapes, which is why the plan's findings appear to
contradict each other. The real shapes are:

- **List** (`GET /v1/fleet/trailers/assignments`) → `{ pagination: object, trailers: [...] }` — NO top-level `id`/`name`.
- **Per-trailer** (`GET /v1/fleet/trailers/{trailerId}/assignments`) → `{ id: int64 (required), name: string (required), trailerAssignments: [...] }`.

Because one record covers both wrappers, fields that exist in only one shape MUST
be nullable. This drives several deliberate deviations from the plan's verbatim
recommendations:

**HIGH (1)**

- **`TrailerAssignment.name` (`response_drift_required`)**: added as **nullable**
  `string?`, NOT `required`. Although the spec marks it required on the per-trailer
  shape, it is absent on the list-wrapper shape, so the plan's "else nullable"
  branch applies — a `required` member would fail to deserialize the list response.

**MEDIUM (8)**

- **Query params (4)**: `ListAsync` and `GetByTrailerAsync` each gained optional
  `startMs` / `endMs`. Modeled **`long?` NOT `int?`** (the plan said `int?`): these
  are millisecond-epoch values (e.g. `1462881998034`) that overflow `Int32`, and
  the repo convention for `*Ms` params is `long` (cf.
  `SafetyClient.V1GetDriverSafetyScoreAsync(string, long startMs, long endMs)`).
  Appended conditionally via `QueryBuilder.WithParams(..., ("startMs", startMs?.ToString()), ("endMs", endMs?.ToString()))`.
- **`pagination` (`response_drift_optional`)**: added as `object?` (list shape).
- **`trailerAssignments` (`response_drift_optional`)**: added as
  `IReadOnlyList<object>?` (per-trailer shape).
- **`trailers` (`response_drift_optional`)**: added as `IReadOnlyList<object>?`
  (list shape).
- **`id` (`type_mismatch` string→int64)**: changed to **`long?`** — applies the
  int64 type change AND stays nullable because the list shape carries no top-level
  `id`. **Breaking**: was `required string`. This single remodel also subsumes the
  conflicting LOW "`id` is an extra" finding (the property stays, just retyped).

**LOW (8)**

- The `id` extra-property finding is handled by the `long?` remodel above (kept,
  not removed).
- The remaining **7 flat extras** (`trailerId`, `trailerName`, `vehicleId`,
  `vehicleName`, `driverId`, `startTime`, `endTime`) are retained as nullable
  back-compat extras (per the precedent in `44-tachograph` / `45-tags`) rather
  than removed, grouped after a `// Not in current spec; retained for back-compat.`
  comment.

**Other files**: the CLI call site in `tools/Samsara.Cli/TuiApp.cs` was fixed for
two compile-breakers introduced by the changes — `ListAsync(Timeout60s())` now
passes the token by name (`cancellationToken:`) so it no longer binds to
`long? startMs`, and `a.Id` (now `long?`) is rendered via `a.Id?.ToString() ?? ""`.
No `SamsaraJsonContext` change (the record is already registered and the new props
are weakly-typed `object`/`IReadOnlyList<object>`, introducing no new top-level
types) and no test change (no construction sites; the facade test only substitutes
the interface).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `TrailerAssignment` | response | 0 | 1 | 4 | 8 |
| `(no SDK type)` | query | 0 | 0 | 4 | 0 |

**Counts**: CRITICAL=0, HIGH=1, MEDIUM=8, LOW=8  
**Total deduped findings**: 17

## HIGH (1)

### `TrailerAssignment` (response)

- **[response_drift_required]** TrailerAssignment (response) missing REQUIRED property `name` (spec type=string/string).
  - Endpoints: `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Add `[JsonPropertyName("name")] public string Name { get; init; }` to response record `TrailerAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (8)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /v1/fleet/trailers/assignments) is missing query parameter `endMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/fleet/trailers/assignments`
  - Recommended fix: Add an optional parameter `int? endMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetByTrailerAsync (GET /v1/fleet/trailers/{trailerId}/assignments) is missing query parameter `endMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Add an optional parameter `int? endMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /v1/fleet/trailers/assignments) is missing query parameter `startMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/fleet/trailers/assignments`
  - Recommended fix: Add an optional parameter `int? startMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetByTrailerAsync (GET /v1/fleet/trailers/{trailerId}/assignments) is missing query parameter `startMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Add an optional parameter `int? startMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `TrailerAssignment` (response)

- **[response_drift_optional]** TrailerAssignment (response) missing property `pagination` (spec type=object).
  - Endpoints: `GET /v1/fleet/trailers/assignments`
  - Recommended fix: Add `[JsonPropertyName("pagination")] public object? Pagination { get; init; }` to response record `TrailerAssignment`.
- **[response_drift_optional]** TrailerAssignment (response) missing property `trailerAssignments` (spec type=array).
  - Endpoints: `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Add `[JsonPropertyName("trailerAssignments")] public IReadOnlyList<object>? TrailerAssignments { get; init; }` to response record `TrailerAssignment`.
- **[response_drift_optional]** TrailerAssignment (response) missing property `trailers` (spec type=array).
  - Endpoints: `GET /v1/fleet/trailers/assignments`
  - Recommended fix: Add `[JsonPropertyName("trailers")] public IReadOnlyList<object>? Trailers { get; init; }` to response record `TrailerAssignment`.
- **[type_mismatch]** TrailerAssignment.id (response): SDK `string` vs spec `integer/int64`.
  - Endpoints: `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Change `TrailerAssignment.Id` from `string` to `long`.

## LOW (8)

### `TrailerAssignment` (response)

- **[extra_property]** TrailerAssignment.driverId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /v1/fleet/trailers/assignments`, `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Remove `TrailerAssignment.DriverId` (not in spec).
- **[extra_property]** TrailerAssignment.endTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /v1/fleet/trailers/assignments`, `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Remove `TrailerAssignment.EndTime` (not in spec).
- **[extra_property]** TrailerAssignment.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /v1/fleet/trailers/assignments`
  - Recommended fix: Remove `TrailerAssignment.Id` (not in spec).
- **[extra_property]** TrailerAssignment.startTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /v1/fleet/trailers/assignments`, `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Remove `TrailerAssignment.StartTime` (not in spec).
- **[extra_property]** TrailerAssignment.trailerId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /v1/fleet/trailers/assignments`, `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Remove `TrailerAssignment.TrailerId` (not in spec).
- **[extra_property]** TrailerAssignment.trailerName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /v1/fleet/trailers/assignments`, `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Remove `TrailerAssignment.TrailerName` (not in spec).
- **[extra_property]** TrailerAssignment.vehicleId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /v1/fleet/trailers/assignments`, `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Remove `TrailerAssignment.VehicleId` (not in spec).
- **[extra_property]** TrailerAssignment.vehicleName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /v1/fleet/trailers/assignments`, `GET /v1/fleet/trailers/{trailerId}/assignments`
  - Recommended fix: Remove `TrailerAssignment.VehicleName` (not in spec).

