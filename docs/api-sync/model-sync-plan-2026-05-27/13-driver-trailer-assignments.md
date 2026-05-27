# Driver-Trailer Assignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/13-driver-trailer-assignments.md`](../13-driver-trailer-assignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `DriverTrailerAssignment` | response | 0 | 7 | 0 | 5 |
| `(no SDK type)` | query | 0 | 2 | 1 | 0 |
| `UpdateDriverTrailerAssignmentRequest` | request | 0 | 1 | 0 | 2 |
| `CreateDriverTrailerAssignmentRequest` | request | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=10, MEDIUM=2, LOW=7  
**Total deduped findings**: 19

## HIGH (10)

### `(no SDK type)` (query)

- **[missing_required_query]** ListAsync (GET /driver-trailer-assignments) is missing query parameter `driverIds` (spec REQUIRED, type=array).
  - Endpoints: `GET /driver-trailer-assignments`
  - Recommended fix: Add a required parameter (e.g. `IReadOnlyList<string> driverIds` , no default) to the SDK method and append it via `QueryBuilder.WithParams("driverIds", ...)`.
- **[missing_required_query]** UpdateAsync (PATCH /driver-trailer-assignments) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `PATCH /driver-trailer-assignments`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.

### `DriverTrailerAssignment` (response)

- **[response_drift_optional]** DriverTrailerAssignment (response) missing property `createdAtTime` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /driver-trailer-assignments`, `PATCH /driver-trailer-assignments`, `POST /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public string? CreatedAtTime { get; init; }` to response record `DriverTrailerAssignment`.
- **[response_drift_optional]** DriverTrailerAssignment (response) missing property `endTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /driver-trailer-assignments`, `PATCH /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("endTime")] public string? EndTime { get; init; }` to response record `DriverTrailerAssignment`.
- **[response_drift_optional]** DriverTrailerAssignment (response) missing property `updatedAtTime` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /driver-trailer-assignments`, `PATCH /driver-trailer-assignments`, `POST /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public string? UpdatedAtTime { get; init; }` to response record `DriverTrailerAssignment`.
- **[response_drift_required]** DriverTrailerAssignment (response) missing REQUIRED property `driver` (spec type=object).
  - Endpoints: `GET /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object Driver { get; init; }` to response record `DriverTrailerAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverTrailerAssignment (response) missing REQUIRED property `id` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /driver-trailer-assignments`, `PATCH /driver-trailer-assignments`, `POST /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("id")] public string Id { get; init; }` to response record `DriverTrailerAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverTrailerAssignment (response) missing REQUIRED property `startTime` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /driver-trailer-assignments`, `PATCH /driver-trailer-assignments`, `POST /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("startTime")] public string StartTime { get; init; }` to response record `DriverTrailerAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverTrailerAssignment (response) missing REQUIRED property `trailer` (spec type=object).
  - Endpoints: `GET /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("trailer")] public object Trailer { get; init; }` to response record `DriverTrailerAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `UpdateDriverTrailerAssignmentRequest` (request)

- **[missing_required]** UpdateDriverTrailerAssignmentRequest is missing REQUIRED property `endTime` (spec type=string).
  - Endpoints: `PATCH /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("endTime")] public required string EndTime { get; init; }` to `UpdateDriverTrailerAssignmentRequest`.

## MEDIUM (2)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /driver-trailer-assignments) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /driver-trailer-assignments`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateDriverTrailerAssignmentRequest` (request)

- **[missing_optional]** CreateDriverTrailerAssignmentRequest is missing property `startTime` (spec type=string).
  - Endpoints: `POST /driver-trailer-assignments`
  - Recommended fix: Add `[JsonPropertyName("startTime")] public string? StartTime { get; init; }` to `CreateDriverTrailerAssignmentRequest`.

## LOW (7)

### `DriverTrailerAssignment` (response)

- **[extra_property]** DriverTrailerAssignment.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /driver-trailer-assignments`
  - Recommended fix: Remove `DriverTrailerAssignment.DriverId` (not in spec).
- **[extra_property]** DriverTrailerAssignment.driverName (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /driver-trailer-assignments`, `PATCH /driver-trailer-assignments`, `POST /driver-trailer-assignments`
  - Recommended fix: Remove `DriverTrailerAssignment.DriverName` (not in spec).
- **[extra_property]** DriverTrailerAssignment.time (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /driver-trailer-assignments`, `PATCH /driver-trailer-assignments`, `POST /driver-trailer-assignments`
  - Recommended fix: Remove `DriverTrailerAssignment.Time` (not in spec).
- **[extra_property]** DriverTrailerAssignment.trailerId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /driver-trailer-assignments`
  - Recommended fix: Remove `DriverTrailerAssignment.TrailerId` (not in spec).
- **[extra_property]** DriverTrailerAssignment.trailerName (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /driver-trailer-assignments`, `PATCH /driver-trailer-assignments`, `POST /driver-trailer-assignments`
  - Recommended fix: Remove `DriverTrailerAssignment.TrailerName` (not in spec).

### `UpdateDriverTrailerAssignmentRequest` (request)

- **[extra_property]** UpdateDriverTrailerAssignmentRequest.driverId: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /driver-trailer-assignments`
  - Recommended fix: Remove `UpdateDriverTrailerAssignmentRequest.DriverId` (not in spec).
- **[extra_property]** UpdateDriverTrailerAssignmentRequest.trailerId: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /driver-trailer-assignments`
  - Recommended fix: Remove `UpdateDriverTrailerAssignmentRequest.TrailerId` (not in spec).

