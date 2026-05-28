# Maintenance — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/30-maintenance.md`](../30-maintenance.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `a68f5ea` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. LOW response-side findings were
intentionally retained as nullable back-compat properties per the workflow
precedent established in `08-carrier-proposed-assignments`,
`13-driver-trailer-assignments`, `14-driver-vehicle-assignments`,
`28-live-sharing-links`, and `29-location-and-speed`. LOW request-side
findings on `UpdateDefectRequest` (`comment`, `resolvedAt`) were removed
since the spec `DefectPatch` schema does not include them; the spec-aligned
replacements (`mechanicNotes`, `resolvedAtTime`, `resolvedBy`) were added in
the MEDIUM pass.

Files touched: `src/Samsara.Sdk/Models/Maintenance/MaintenanceModels.cs`,
`src/Samsara.Sdk/Clients/Maintenance/IMaintenanceClient.cs`,
`src/Samsara.Sdk/Clients/Maintenance/MaintenanceClient.cs`,
`tools/Samsara.Cli/TuiApp.cs` (CLI call sites updated for new optional
parameters).

**HIGH (9)**

- **`DefectRecord` response — required `dvirId`**: added as
  `[JsonPropertyName("dvirId")] public string? DvirId { get; init; }`. Kept
  nullable because not all defect-source endpoints are guaranteed to include
  it (the spec marks REQUIRED for `GET /defects/stream` and `GET /defects/{id}`
  but the PATCH response schema diverges).
- **`DefectType` response — required `createdAtTime`, `label`, `sectionType`**:
  all three added as non-nullable `required` properties to match the spec
  guarantee on `GET /defect-types`.
- **`MaintenanceDvir` response — required `authorSignature`,
  `dvirSubmissionBeginTime`, `dvirSubmissionTime`, `type`, `updatedAtTime`**:
  `authorSignature` modeled as a typed `JsonElement?` to preserve the nested
  signature payload; the four time/string fields added as nullable since the
  POST/PATCH response shapes don't always echo every spec-required field on
  the stream/get schemas. Kept nullable to avoid runtime deserialization
  failures when the server omits one of these on a mutation response.

**MEDIUM (40)**

- **Query parameters (7 missing)**: added `ids` to `ListDefectTypesAsync`;
  added `includeExternalIds` to `GetDvirsStreamAsync`, `GetDvirByIdAsync`,
  `GetDefectsStreamAsync`, and `GetDefectAsync`; added `isResolved` to
  `GetDefectsStreamAsync`; added `safetyStatus` (array, comma-joined) to
  `GetDvirsStreamAsync`. All append conditionally via
  `QueryBuilder.WithParams(...)`.
- **`DefectRecord` response optionals (10)**: added `createdAtTime`,
  `defectPhotos`, `defectTypeId`, `mechanicNotes`, `mechanicNotesUpdatedAtTime`,
  `resolvedAtTime`, `resolvedBy`, `trailer`, `updatedAtTime`, `vehicle`.
  Nested object/array shapes modeled as `JsonElement?` /
  `IReadOnlyList<JsonElement>?`.
- **`DefectRecord` required drift (2)**: tightened `Comment` and `IsResolved`
  to non-nullable `required` per spec.
- **`DefectType` response optional (1)**: added `severity`.
- **`MaintenanceDvir` response optionals (16)**: added `defectIds`, `endTime`,
  `formattedAddress`, `licensePlate`, `location`, `mechanicNotes`,
  `odometerMeters`, `safetyStatus`, `secondSignature`, `startTime`,
  `thirdSignature`, `trailer`, `trailerDefects`, `trailerName`, `vehicle`,
  `vehicleDefects`, `walkaroundPhotos`. Nested object/array shapes modeled
  as `JsonElement?` / `IReadOnlyList<JsonElement>?`.
- **`UpdateDefectRequest` request optionals (3)**: added `mechanicNotes`,
  `resolvedAtTime`, `resolvedBy`. The legacy `comment` and `resolvedAt`
  fields were dropped (see LOW notes below).

**LOW (16)**

- **Response extras retained as nullable back-compat (14)**: `DefectRecord`
  (`createdAt`, `defectType`, `driverId`, `resolvedAt`, `vehicleId`,
  `vehicleName`), `DefectType` (`category`, `name`), `MaintenanceDvir`
  (`defects`, `inspectionType`, `safeToOperate`, `timeMs`, `vehicleId`,
  `vehicleName`). All carry XML doc pointers to the canonical spec field.
- **Request extras removed (2)**: `UpdateDefectRequest.comment` and
  `UpdateDefectRequest.resolvedAt` were removed because they are not in the
  spec `DefectPatch` schema; on a request body, sending unknown fields would
  fail server validation. The spec-aligned replacements (`mechanicNotes`,
  `resolvedAtTime`, `resolvedBy`) cover the same use cases and were added
  under MEDIUM.

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `MaintenanceDvir` | response | 0 | 5 | 17 | 6 |
| `DefectType` | response | 0 | 3 | 1 | 2 |
| `DefectRecord` | response | 0 | 1 | 12 | 6 |
| `(no SDK type)` | query | 0 | 0 | 7 | 0 |
| `UpdateDefectRequest` | request | 0 | 0 | 3 | 2 |

**Counts**: CRITICAL=0, HIGH=9, MEDIUM=40, LOW=16  
**Total deduped findings**: 65

## HIGH (9)

### `DefectRecord` (response)

- **[response_drift_required]** DefectRecord (response) missing REQUIRED property `dvirId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("dvirId")] public string DvirId { get; init; }` to response record `DefectRecord` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `DefectType` (response)

- **[response_drift_required]** DefectType (response) missing REQUIRED property `createdAtTime` (spec type=string).
  - Endpoints: `GET /defect-types`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public string CreatedAtTime { get; init; }` to response record `DefectType` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DefectType (response) missing REQUIRED property `label` (spec type=string).
  - Endpoints: `GET /defect-types`
  - Recommended fix: Add `[JsonPropertyName("label")] public string Label { get; init; }` to response record `DefectType` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DefectType (response) missing REQUIRED property `sectionType` (spec type=string).
  - Endpoints: `GET /defect-types`
  - Recommended fix: Add `[JsonPropertyName("sectionType")] public string SectionType { get; init; }` to response record `DefectType` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `MaintenanceDvir` (response)

- **[response_drift_required]** MaintenanceDvir (response) missing REQUIRED property `authorSignature` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("authorSignature")] public object AuthorSignature { get; init; }` to response record `MaintenanceDvir` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MaintenanceDvir (response) missing REQUIRED property `dvirSubmissionBeginTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`
  - Recommended fix: Add `[JsonPropertyName("dvirSubmissionBeginTime")] public string DvirSubmissionBeginTime { get; init; }` to response record `MaintenanceDvir` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MaintenanceDvir (response) missing REQUIRED property `dvirSubmissionTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`
  - Recommended fix: Add `[JsonPropertyName("dvirSubmissionTime")] public string DvirSubmissionTime { get; init; }` to response record `MaintenanceDvir` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MaintenanceDvir (response) missing REQUIRED property `type` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("type")] public string Type { get; init; }` to response record `MaintenanceDvir` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MaintenanceDvir (response) missing REQUIRED property `updatedAtTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public string UpdatedAtTime { get; init; }` to response record `MaintenanceDvir` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (40)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListDefectTypesAsync (GET /defect-types) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /defect-types`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDvirsStreamAsync (GET /dvirs/stream) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /dvirs/stream`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDvirByIdAsync (GET /dvirs/{id}) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /dvirs/{id}`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDefectsStreamAsync (GET /defects/stream) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /defects/stream`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDefectAsync (GET /defects/{id}) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /defects/{id}`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDefectsStreamAsync (GET /defects/stream) is missing query parameter `isResolved` (spec optional, type=boolean).
  - Endpoints: `GET /defects/stream`
  - Recommended fix: Add an optional parameter `bool? isResolved = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDvirsStreamAsync (GET /dvirs/stream) is missing query parameter `safetyStatus` (spec optional, type=array).
  - Endpoints: `GET /dvirs/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? safetyStatus = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `DefectRecord` (response)

- **[response_drift_optional]** DefectRecord (response) missing property `createdAtTime` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public string? CreatedAtTime { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `defectPhotos` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("defectPhotos")] public IReadOnlyList<object>? DefectPhotos { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `defectTypeId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("defectTypeId")] public string? DefectTypeId { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `mechanicNotes` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("mechanicNotes")] public string? MechanicNotes { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `mechanicNotesUpdatedAtTime` (spec type=string).
  - Endpoints: `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("mechanicNotesUpdatedAtTime")] public string? MechanicNotesUpdatedAtTime { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `resolvedAtTime` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("resolvedAtTime")] public string? ResolvedAtTime { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `resolvedBy` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("resolvedBy")] public object? ResolvedBy { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `trailer` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("trailer")] public object? Trailer { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `updatedAtTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public string? UpdatedAtTime { get; init; }` to response record `DefectRecord`.
- **[response_drift_optional]** DefectRecord (response) missing property `vehicle` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("vehicle")] public object? Vehicle { get; init; }` to response record `DefectRecord`.
- **[response_required_drift]** DefectRecord.comment (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`
  - Recommended fix: Tighten `DefectRecord.Comment` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** DefectRecord.isResolved (response): spec marks REQUIRED but SDK exposes as nullable (`bool?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Tighten `DefectRecord.IsResolved` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `DefectType` (response)

- **[response_drift_optional]** DefectType (response) missing property `severity` (spec type=string).
  - Endpoints: `GET /defect-types`
  - Recommended fix: Add `[JsonPropertyName("severity")] public string? Severity { get; init; }` to response record `DefectType`.

### `MaintenanceDvir` (response)

- **[response_drift_optional]** MaintenanceDvir (response) missing property `defectIds` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`
  - Recommended fix: Add `[JsonPropertyName("defectIds")] public IReadOnlyList<string>? DefectIds { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `endTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("endTime")] public string? EndTime { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `formattedAddress` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`
  - Recommended fix: Add `[JsonPropertyName("formattedAddress")] public string? FormattedAddress { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `licensePlate` (spec type=string). (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("licensePlate")] public string? LicensePlate { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `location` (spec type=string). (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("location")] public string? Location { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `mechanicNotes` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("mechanicNotes")] public string? MechanicNotes { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `odometerMeters` (spec type=integer/int64). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `safetyStatus` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("safetyStatus")] public string? SafetyStatus { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `secondSignature` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("secondSignature")] public object? SecondSignature { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `startTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("startTime")] public string? StartTime { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `thirdSignature` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("thirdSignature")] public object? ThirdSignature { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `trailer` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("trailer")] public object? Trailer { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `trailerDefects` (spec type=array). (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("trailerDefects")] public IReadOnlyList<object>? TrailerDefects { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `trailerName` (spec type=string). (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("trailerName")] public string? TrailerName { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `vehicle` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("vehicle")] public object? Vehicle { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `vehicleDefects` (spec type=array). (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Add `[JsonPropertyName("vehicleDefects")] public IReadOnlyList<object>? VehicleDefects { get; init; }` to response record `MaintenanceDvir`.
- **[response_drift_optional]** MaintenanceDvir (response) missing property `walkaroundPhotos` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`
  - Recommended fix: Add `[JsonPropertyName("walkaroundPhotos")] public IReadOnlyList<object>? WalkaroundPhotos { get; init; }` to response record `MaintenanceDvir`.

### `UpdateDefectRequest` (request)

- **[missing_optional]** UpdateDefectRequest is missing property `mechanicNotes` (spec type=string).
  - Endpoints: `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("mechanicNotes")] public string? MechanicNotes { get; init; }` to `UpdateDefectRequest`.
- **[missing_optional]** UpdateDefectRequest is missing property `resolvedAtTime` (spec type=string).
  - Endpoints: `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("resolvedAtTime")] public string? ResolvedAtTime { get; init; }` to `UpdateDefectRequest`.
- **[missing_optional]** UpdateDefectRequest is missing property `resolvedBy` (spec type=object).
  - Endpoints: `PATCH /fleet/defects/{id}`
  - Recommended fix: Add `[JsonPropertyName("resolvedBy")] public object? ResolvedBy { get; init; }` to `UpdateDefectRequest`.

## LOW (16)

### `DefectRecord` (response)

- **[extra_property]** DefectRecord.createdAt (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Remove `DefectRecord.CreatedAt` (not in spec).
- **[extra_property]** DefectRecord.defectType (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`
  - Recommended fix: Remove `DefectRecord.DefectType` (not in spec).
- **[extra_property]** DefectRecord.driverId (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Remove `DefectRecord.DriverId` (not in spec).
- **[extra_property]** DefectRecord.resolvedAt (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Remove `DefectRecord.ResolvedAt` (not in spec).
- **[extra_property]** DefectRecord.vehicleId (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Remove `DefectRecord.VehicleId` (not in spec).
- **[extra_property]** DefectRecord.vehicleName (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /defects/stream`, `GET /defects/{id}`, `PATCH /fleet/defects/{id}`
  - Recommended fix: Remove `DefectRecord.VehicleName` (not in spec).

### `DefectType` (response)

- **[extra_property]** DefectType.category (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /defect-types`
  - Recommended fix: Remove `DefectType.Category` (not in spec).
- **[extra_property]** DefectType.name (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /defect-types`
  - Recommended fix: Remove `DefectType.Name` (not in spec).

### `MaintenanceDvir` (response)

- **[extra_property]** MaintenanceDvir.defects (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Remove `MaintenanceDvir.Defects` (not in spec).
- **[extra_property]** MaintenanceDvir.inspectionType (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Remove `MaintenanceDvir.InspectionType` (not in spec).
- **[extra_property]** MaintenanceDvir.safeToOperate (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Remove `MaintenanceDvir.SafeToOperate` (not in spec).
- **[extra_property]** MaintenanceDvir.timeMs (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Remove `MaintenanceDvir.TimeMs` (not in spec).
- **[extra_property]** MaintenanceDvir.vehicleId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Remove `MaintenanceDvir.VehicleId` (not in spec).
- **[extra_property]** MaintenanceDvir.vehicleName (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /dvirs/stream`, `GET /dvirs/{id}`, `PATCH /fleet/dvirs/{id}`, `POST /fleet/dvirs`
  - Recommended fix: Remove `MaintenanceDvir.VehicleName` (not in spec).

### `UpdateDefectRequest` (request)

- **[extra_property]** UpdateDefectRequest.comment: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/defects/{id}`
  - Recommended fix: Remove `UpdateDefectRequest.Comment` (not in spec).
- **[extra_property]** UpdateDefectRequest.resolvedAt: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/defects/{id}`
  - Recommended fix: Remove `UpdateDefectRequest.ResolvedAt` (not in spec).

