# Driver-Vehicle Assignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/14-driver-vehicle-assignments.md`](../14-driver-vehicle-assignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

