# Driver-Trailer Assignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/13-driver-trailer-assignments.md`](../13-driver-trailer-assignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `f2bdc2b` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied; LOW findings on the response were
intentionally retained as nullable back-compat properties per the workflow
precedent established in `08-carrier-proposed-assignments` (response-side
flat-scalar conveniences kept; request-side spec-absent fields removed).

Files touched: `src/Samsara.Sdk/Models/Assignments/AssignmentModels.cs`,
`src/Samsara.Sdk/Clients/Assignments/DriverTrailerAssignmentsClient.cs`,
`src/Samsara.Sdk/Clients/Assignments/IDriverTrailerAssignmentsClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`.

**HIGH (10)**

- **`(no SDK type)` query — `driverIds` REQUIRED on GET**:
  `IDriverTrailerAssignmentsClient.ListAsync` now takes
  `IReadOnlyList<string> driverIds` (no default), appended via
  `QueryBuilder.WithParams("driverIds", string.Join(",", driverIds))` —
  same pattern as the prior `12-driver-qr-codes` and `08-carrier-proposed`
  implementations.
- **`(no SDK type)` query — `id` REQUIRED on PATCH**: `UpdateAsync` now takes
  `string id` separately and appends it via
  `QueryBuilder.WithParams(BasePath, ("id", id))`.
- **`DriverTrailerAssignment` response — required `id`, `driver`, `trailer`,
  `startTime`**: added all four as `required` properties. `driver` and
  `trailer` are typed via new nested records
  `DriverTrailerAssignmentDriver` (required `driverId` + optional
  `externalIds`, mirrors spec `DriverWithExternalIdObjectResponseBody`) and
  `DriverTrailerAssignmentTrailer` (required `trailerId`, mirrors spec
  `TrailerObjectResponseBody`). Both nested records are registered in
  `SamsaraJsonContext`. `startTime` is typed as `string` to match the spec
  (RFC 3339 string with no `format` declared — matches the pattern used by
  `CarrierProposedAssignment.activeTime`).
- **`DriverTrailerAssignment` response — optional `createdAtTime`, `endTime`,
  `updatedAtTime`**: added as `string?` per spec.
- **`UpdateDriverTrailerAssignmentRequest` — required `endTime`**: added
  `required string EndTime`. The body had previously carried SDK-only
  `driverId`/`trailerId` properties which were not in the spec body; those
  were dropped (they would have been silently ignored by the API).

**MEDIUM (2)**

- **`(no SDK type)` query — optional `includeExternalIds`**: added as
  `bool? includeExternalIds = null` on `ListAsync`, serialized as
  lowercase boolean string.
- **`CreateDriverTrailerAssignmentRequest` — optional `startTime`**: added
  `[JsonPropertyName("startTime")] public string? StartTime { get; init; }`
  (RFC 3339 string per spec, defaults to "now" server-side).

**LOW (7)**

- **`DriverTrailerAssignment.driverId/driverName/trailerId/trailerName/time`
  (response)**: kept as nullable back-compat properties with XML doc comments
  noting they are not in the spec inner schema. Same approach as the
  `08-carrier-proposed-assignments` workflow precedent: response-side flat
  scalars that previously existed are preserved as a non-breaking convenience.
- **`UpdateDriverTrailerAssignmentRequest.driverId/trailerId` (request)**:
  REMOVED. These were never in the spec request body — the spec body only
  has `endTime`. Removing them matches the
  `08-carrier-proposed-assignments` precedent for request-side cleanup
  (spec-absent body fields don't help callers and are misleading).

Verification: `dotnet build` green (0 warnings, 0 errors), all 59 unit tests
pass, `python3 tools/check-sdk-sync.py` exits 0 (matched=323/323,
mismatched=0, unresolved=0, not implemented=0).

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

