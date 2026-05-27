# Trailer Assignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/46-trailer-assignments.md`](../46-trailer-assignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

