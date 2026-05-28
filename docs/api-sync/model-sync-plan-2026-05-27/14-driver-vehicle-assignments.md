# Driver-Vehicle Assignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/14-driver-vehicle-assignments.md`](../14-driver-vehicle-assignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `9ecf5d4` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied; LOW findings on the response were
intentionally retained as nullable back-compat properties per the workflow
precedent established in `08-carrier-proposed-assignments` and
`13-driver-trailer-assignments` (response-side flat-scalar conveniences kept;
request-side spec-absent fields are left untouched because this domain's
existing request DTOs already match the spec body shape).

Files touched: `src/Samsara.Sdk/Models/Assignments/AssignmentModels.cs`,
`src/Samsara.Sdk/Clients/Assignments/DriverVehicleAssignmentsClient.cs`,
`src/Samsara.Sdk/Clients/Assignments/IDriverVehicleAssignmentsClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`, plus a one-line
adjustment in `tools/Samsara.Cli/TuiApp.cs` to account for the now-nullable
back-compat `Id` and to surface the new nested `Driver`/`Vehicle` IDs.

**HIGH (2)**

- **`DriverVehicleAssignment` response — required `driver`**: added as
  `required DriverVehicleAssignmentDriver Driver` with a new nested record
  mirroring the spec's `GoaDriverTinyResponseResponseBody` (required `id`,
  optional `name` / `externalIds`).
- **`DriverVehicleAssignment` response — required `vehicle`**: added as
  `required DriverVehicleAssignmentVehicle Vehicle` with a new nested record
  mirroring the spec's `GoaVehicleTinyResponseResponseBody` (optional `id` /
  `name` / `externalIds` — the spec does not mark `id` required on this
  sub-schema).

**MEDIUM (7)**

- **`(no SDK type)` query — optional `driverTagIds`**: added as
  `string? driverTagIds = null` on `ListAsync` and appended via
  `QueryBuilder.WithParams("driverTagIds", driverTagIds)`.
- **`(no SDK type)` query — optional `vehicleTagIds`**: added as
  `string? vehicleTagIds = null` on `ListAsync` and appended via
  `QueryBuilder.WithParams("vehicleTagIds", vehicleTagIds)`.
- **`DriverVehicleAssignment` response — optional `assignedAtTime`**: added
  as `string? AssignedAtTime` (RFC 3339 string per spec — no `format`).
- **`DriverVehicleAssignment` response — optional `message`**: added as
  `string? Message`. POST and PATCH responses return only `{ "data": {
  "message": "Driver assignment was successfully ..." } }`; the field is
  reused on the shared SDK record.
- **`DriverVehicleAssignment` response — optional `metadata`**: added as
  `DriverVehicleAssignmentMetadata? Metadata`, a new nested record mirroring
  the spec's `DriverAssignmentMetadataTinyObjectResponseBody` (optional
  `sourceName`).
- **`DriverVehicleAssignment.isPassenger` (response)**: tightened to
  `required bool IsPassenger` (spec marks REQUIRED for the GET inner schema).
- **`DriverVehicleAssignment.startTime` (response)**: tightened to
  `required DateTimeOffset StartTime` (spec marks REQUIRED for the GET inner
  schema). Type kept as `DateTimeOffset` per the plan's "drop the `?`"
  instruction.

**LOW (9)**

- **`DriverVehicleAssignment.id/driverId/driverName/vehicleId/vehicleName`
  (response)**: kept as nullable back-compat scalars (now relaxed to
  `string? Id`) with XML doc comments noting they are not in the spec inner
  schema. Same approach as the `13-driver-trailer-assignments` precedent.
- **`DriverVehicleAssignment.assignmentType/endTime/isPassenger/startTime`
  (response, "PATCH/POST only" findings)**: not removed. The shared SDK
  record is reused across GET (where these are spec properties) and POST /
  PATCH (whose response bodies only carry `message`). GET is the dominant
  consumer; the fields stay so callers can deserialize list responses.

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `DriverVehicleAssignment` | response | 0 | 2 | 5 | 9 |
| `(no SDK type)` | query | 0 | 0 | 2 | 0 |

**Counts**: CRITICAL=0, HIGH=2, MEDIUM=7, LOW=9  
**Total deduped findings**: 18

## HIGH (2)

### `DriverVehicleAssignment` (response)

- **[response_drift_required]** DriverVehicleAssignment (response) missing REQUIRED property `driver` (spec type=object).
  - Endpoints: `GET /fleet/driver-vehicle-assignments`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object Driver { get; init; }` to response record `DriverVehicleAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverVehicleAssignment (response) missing REQUIRED property `vehicle` (spec type=object).
  - Endpoints: `GET /fleet/driver-vehicle-assignments`
  - Recommended fix: Add `[JsonPropertyName("vehicle")] public object Vehicle { get; init; }` to response record `DriverVehicleAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (7)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /fleet/driver-vehicle-assignments) is missing query parameter `driverTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/driver-vehicle-assignments`
  - Recommended fix: Add an optional parameter `string? driverTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/driver-vehicle-assignments) is missing query parameter `vehicleTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/driver-vehicle-assignments`
  - Recommended fix: Add an optional parameter `string? vehicleTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `DriverVehicleAssignment` (response)

- **[response_drift_optional]** DriverVehicleAssignment (response) missing property `assignedAtTime` (spec type=string).
  - Endpoints: `GET /fleet/driver-vehicle-assignments`
  - Recommended fix: Add `[JsonPropertyName("assignedAtTime")] public string? AssignedAtTime { get; init; }` to response record `DriverVehicleAssignment`.
- **[response_drift_optional]** DriverVehicleAssignment (response) missing property `message` (spec type=string). (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Add `[JsonPropertyName("message")] public string? Message { get; init; }` to response record `DriverVehicleAssignment`.
- **[response_drift_optional]** DriverVehicleAssignment (response) missing property `metadata` (spec type=object).
  - Endpoints: `GET /fleet/driver-vehicle-assignments`
  - Recommended fix: Add `[JsonPropertyName("metadata")] public object? Metadata { get; init; }` to response record `DriverVehicleAssignment`.
- **[response_required_drift]** DriverVehicleAssignment.isPassenger (response): spec marks REQUIRED but SDK exposes as nullable (`bool?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /fleet/driver-vehicle-assignments`
  - Recommended fix: Tighten `DriverVehicleAssignment.IsPassenger` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** DriverVehicleAssignment.startTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /fleet/driver-vehicle-assignments`
  - Recommended fix: Tighten `DriverVehicleAssignment.StartTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (9)

### `DriverVehicleAssignment` (response)

- **[extra_property]** DriverVehicleAssignment.assignmentType (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.AssignmentType` (not in spec).
- **[extra_property]** DriverVehicleAssignment.driverId (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/driver-vehicle-assignments`, `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.DriverId` (not in spec).
- **[extra_property]** DriverVehicleAssignment.driverName (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/driver-vehicle-assignments`, `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.DriverName` (not in spec).
- **[extra_property]** DriverVehicleAssignment.endTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.EndTime` (not in spec).
- **[extra_property]** DriverVehicleAssignment.id (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/driver-vehicle-assignments`, `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.Id` (not in spec).
- **[extra_property]** DriverVehicleAssignment.isPassenger (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.IsPassenger` (not in spec).
- **[extra_property]** DriverVehicleAssignment.startTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.StartTime` (not in spec).
- **[extra_property]** DriverVehicleAssignment.vehicleId (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/driver-vehicle-assignments`, `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.VehicleId` (not in spec).
- **[extra_property]** DriverVehicleAssignment.vehicleName (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/driver-vehicle-assignments`, `PATCH /fleet/driver-vehicle-assignments`, `POST /fleet/driver-vehicle-assignments`
  - Recommended fix: Remove `DriverVehicleAssignment.VehicleName` (not in spec).

