# CARB CTC — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/07-carb-ctc.md`](../07-carb-ctc.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `CarbCtcVehicleHistory` | response | 0 | 4 | 1 | 4 |
| `CarbCtcVehicle` | response | 0 | 3 | 3 | 6 |
| `(no SDK type)` | query | 0 | 1 | 3 | 0 |

**Counts**: CRITICAL=0, HIGH=8, MEDIUM=7, LOW=10  
**Total deduped findings**: 25

## HIGH (8)

### `(no SDK type)` (query)

- **[missing_required_query]** ListVehicleHistoryAsync (GET /fleet/carb-ctc/vehicles/history) is missing query parameter `vehicleIds` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Add a required parameter (e.g. `string vehicleIds` , no default) to the SDK method and append it via `QueryBuilder.WithParams("vehicleIds", ...)`.

### `CarbCtcVehicle` (response)

- **[response_drift_required]** CarbCtcVehicle (response) missing REQUIRED property `enrollmentId` (spec type=string/uuid).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add `[JsonPropertyName("enrollmentId")] public string EnrollmentId { get; init; }` to response record `CarbCtcVehicle` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CarbCtcVehicle (response) missing REQUIRED property `enrollmentVin` (spec type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add `[JsonPropertyName("enrollmentVin")] public string EnrollmentVin { get; init; }` to response record `CarbCtcVehicle` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CarbCtcVehicle (response) missing REQUIRED property `testStatus` (spec type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add `[JsonPropertyName("testStatus")] public string TestStatus { get; init; }` to response record `CarbCtcVehicle` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `CarbCtcVehicleHistory` (response)

- **[response_drift_required]** CarbCtcVehicleHistory (response) missing REQUIRED property `enrollmentId` (spec type=string/uuid).
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Add `[JsonPropertyName("enrollmentId")] public string EnrollmentId { get; init; }` to response record `CarbCtcVehicleHistory` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CarbCtcVehicleHistory (response) missing REQUIRED property `enrollmentVin` (spec type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Add `[JsonPropertyName("enrollmentVin")] public string EnrollmentVin { get; init; }` to response record `CarbCtcVehicleHistory` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CarbCtcVehicleHistory (response) missing REQUIRED property `happenedAtTime` (spec type=string/date-time).
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Add `[JsonPropertyName("happenedAtTime")] public DateTimeOffset HappenedAtTime { get; init; }` to response record `CarbCtcVehicleHistory` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CarbCtcVehicleHistory (response) missing REQUIRED property `testResult` (spec type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Add `[JsonPropertyName("testResult")] public string TestResult { get; init; }` to response record `CarbCtcVehicleHistory` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (7)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListVehiclesAsync (GET /fleet/carb-ctc/vehicles) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListVehiclesAsync (GET /fleet/carb-ctc/vehicles) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListVehiclesAsync (GET /fleet/carb-ctc/vehicles) is missing query parameter `testStatus` (spec optional, type=array).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? testStatus = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CarbCtcVehicle` (response)

- **[response_drift_optional]** CarbCtcVehicle (response) missing property `lastCollectionAtTime` (spec type=string/date-time).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add `[JsonPropertyName("lastCollectionAtTime")] public DateTimeOffset? LastCollectionAtTime { get; init; }` to response record `CarbCtcVehicle`.
- **[response_drift_optional]** CarbCtcVehicle (response) missing property `nextCollectionAtTime` (spec type=string/date-time).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add `[JsonPropertyName("nextCollectionAtTime")] public DateTimeOffset? NextCollectionAtTime { get; init; }` to response record `CarbCtcVehicle`.
- **[response_drift_optional]** CarbCtcVehicle (response) missing property `testStatusDetails` (spec type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Add `[JsonPropertyName("testStatusDetails")] public string? TestStatusDetails { get; init; }` to response record `CarbCtcVehicle`.

### `CarbCtcVehicleHistory` (response)

- **[response_drift_optional]** CarbCtcVehicleHistory (response) missing property `testResultDetails` (spec type=string).
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Add `[JsonPropertyName("testResultDetails")] public string? TestResultDetails { get; init; }` to response record `CarbCtcVehicleHistory`.

## LOW (10)

### `CarbCtcVehicle` (response)

- **[extra_property]** CarbCtcVehicle.complianceStatus (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Remove `CarbCtcVehicle.ComplianceStatus` (not in spec).
- **[extra_property]** CarbCtcVehicle.fuelType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Remove `CarbCtcVehicle.FuelType` (not in spec).
- **[extra_property]** CarbCtcVehicle.licensePlate (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Remove `CarbCtcVehicle.LicensePlate` (not in spec).
- **[extra_property]** CarbCtcVehicle.modelYear (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Remove `CarbCtcVehicle.ModelYear` (not in spec).
- **[extra_property]** CarbCtcVehicle.name (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Remove `CarbCtcVehicle.Name` (not in spec).
- **[extra_property]** CarbCtcVehicle.vin (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles`
  - Recommended fix: Remove `CarbCtcVehicle.Vin` (not in spec).

### `CarbCtcVehicleHistory` (response)

- **[extra_property]** CarbCtcVehicleHistory.details (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Remove `CarbCtcVehicleHistory.Details` (not in spec).
- **[extra_property]** CarbCtcVehicleHistory.event (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Remove `CarbCtcVehicleHistory.Event` (not in spec).
- **[extra_property]** CarbCtcVehicleHistory.time (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Remove `CarbCtcVehicleHistory.Time` (not in spec).
- **[extra_property]** CarbCtcVehicleHistory.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/carb-ctc/vehicles/history`
  - Recommended fix: Remove `CarbCtcVehicleHistory.VehicleId` (not in spec).

