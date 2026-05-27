# Hours of Service — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/20-hours-of-service.md`](../20-hours-of-service.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `HosDailyLog` | response | 0 | 3 | 4 | 8 |
| `(no SDK type)` | query | 0 | 1 | 18 | 0 |
| `HosViolation` | response | 0 | 1 | 0 | 7 |
| `HosLog` | response | 0 | 0 | 2 | 15 |

**Counts**: CRITICAL=0, HIGH=5, MEDIUM=24, LOW=30  
**Total deduped findings**: 59

## HIGH (5)

### `(no SDK type)` (query)

- **[missing_required_query]** V1ListHosAuthenticationLogsAsync (GET /v1/fleet/hos_authentication_logs) is missing query parameter `driverId` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/hos_authentication_logs`
  - Recommended fix: Add a required parameter (e.g. `int driverId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("driverId", ...)`.

### `HosDailyLog` (response)

- **[response_drift_required]** HosDailyLog (response) missing REQUIRED property `driver` (spec type=object).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object Driver { get; init; }` to response record `HosDailyLog` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HosDailyLog (response) missing REQUIRED property `endTime` (spec type=string).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add `[JsonPropertyName("endTime")] public string EndTime { get; init; }` to response record `HosDailyLog` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HosDailyLog (response) missing REQUIRED property `startTime` (spec type=string).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add `[JsonPropertyName("startTime")] public string StartTime { get; init; }` to response record `HosDailyLog` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `HosViolation` (response)

- **[response_drift_required]** HosViolation (response) missing REQUIRED property `violations` (spec type=array).
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Add `[JsonPropertyName("violations")] public IReadOnlyList<object> Violations { get; init; }` to response record `HosViolation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (24)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetHosClocksAsync (GET /fleet/hos/clocks) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/clocks`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosDailyLogsAsync (GET /fleet/hos/daily-logs) is missing query parameter `driverActivationStatus` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add an optional parameter `string? driverActivationStatus = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosLogsAsync (GET /fleet/hos/logs) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosViolationsAsync (GET /fleet/hos/violations) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosDailyLogsAsync (GET /fleet/hos/daily-logs) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosDailyLogsAsync (GET /fleet/hos/daily-logs) is missing query parameter `endDate` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add an optional parameter `string? endDate = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosDailyLogsAsync (GET /fleet/hos/daily-logs) is missing query parameter `expand` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add an optional parameter `string? expand = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetHosClocksAsync (GET /fleet/hos/clocks) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /fleet/hos/clocks`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosLogsAsync (GET /fleet/hos/logs) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosViolationsAsync (GET /fleet/hos/violations) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetHosClocksAsync (GET /fleet/hos/clocks) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/hos/clocks`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosDailyLogsAsync (GET /fleet/hos/daily-logs) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosDailyLogsAsync (GET /fleet/hos/daily-logs) is missing query parameter `startDate` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add an optional parameter `string? startDate = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosLogsAsync (GET /fleet/hos/logs) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosViolationsAsync (GET /fleet/hos/violations) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetHosClocksAsync (GET /fleet/hos/clocks) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/hos/clocks`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosDailyLogsAsync (GET /fleet/hos/daily-logs) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosViolationsAsync (GET /fleet/hos/violations) is missing query parameter `types` (spec optional, type=array).
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? types = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `HosDailyLog` (response)

- **[response_drift_optional]** HosDailyLog (response) missing property `distanceTraveled` (spec type=object).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add `[JsonPropertyName("distanceTraveled")] public object? DistanceTraveled { get; init; }` to response record `HosDailyLog`.
- **[response_drift_optional]** HosDailyLog (response) missing property `dutyStatusDurations` (spec type=object).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add `[JsonPropertyName("dutyStatusDurations")] public object? DutyStatusDurations { get; init; }` to response record `HosDailyLog`.
- **[response_drift_optional]** HosDailyLog (response) missing property `logMetaData` (spec type=object).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add `[JsonPropertyName("logMetaData")] public object? LogMetaData { get; init; }` to response record `HosDailyLog`.
- **[response_drift_optional]** HosDailyLog (response) missing property `pendingDutyStatusDurations` (spec type=object).
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Add `[JsonPropertyName("pendingDutyStatusDurations")] public object? PendingDutyStatusDurations { get; init; }` to response record `HosDailyLog`.

### `HosLog` (response)

- **[response_drift_optional]** HosLog (response) missing property `driver` (spec type=object).
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object? Driver { get; init; }` to response record `HosLog`.
- **[response_drift_optional]** HosLog (response) missing property `hosLogs` (spec type=array).
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Add `[JsonPropertyName("hosLogs")] public IReadOnlyList<object>? HosLogs { get; init; }` to response record `HosLog`.

## LOW (30)

### `HosDailyLog` (response)

- **[extra_property]** HosDailyLog.certificationState (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Remove `HosDailyLog.CertificationState` (not in spec).
- **[extra_property]** HosDailyLog.date (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Remove `HosDailyLog.Date` (not in spec).
- **[extra_property]** HosDailyLog.distanceDrivenMeters (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Remove `HosDailyLog.DistanceDrivenMeters` (not in spec).
- **[extra_property]** HosDailyLog.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Remove `HosDailyLog.DriverId` (not in spec).
- **[extra_property]** HosDailyLog.driverName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Remove `HosDailyLog.DriverName` (not in spec).
- **[extra_property]** HosDailyLog.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Remove `HosDailyLog.Id` (not in spec).
- **[extra_property]** HosDailyLog.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Remove `HosDailyLog.VehicleId` (not in spec).
- **[extra_property]** HosDailyLog.vehicleName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/daily-logs`
  - Recommended fix: Remove `HosDailyLog.VehicleName` (not in spec).

### `HosLog` (response)

- **[extra_property]** HosLog.codriverIds (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.CodriverIds` (not in spec).
- **[extra_property]** HosLog.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.DriverId` (not in spec).
- **[extra_property]** HosLog.driverName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.DriverName` (not in spec).
- **[extra_property]** HosLog.groupId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.GroupId` (not in spec).
- **[extra_property]** HosLog.hosStatusType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.HosStatusType` (not in spec).
- **[extra_property]** HosLog.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.Id` (not in spec).
- **[extra_property]** HosLog.locCity (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.LocCity` (not in spec).
- **[extra_property]** HosLog.locLat (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.LocLat` (not in spec).
- **[extra_property]** HosLog.locLng (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.LocLng` (not in spec).
- **[extra_property]** HosLog.locName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.LocName` (not in spec).
- **[extra_property]** HosLog.locState (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.LocState` (not in spec).
- **[extra_property]** HosLog.logStartMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.LogStartMs` (not in spec).
- **[extra_property]** HosLog.remark (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.Remark` (not in spec).
- **[extra_property]** HosLog.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.VehicleId` (not in spec).
- **[extra_property]** HosLog.vehicleName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/logs`
  - Recommended fix: Remove `HosLog.VehicleName` (not in spec).

### `HosViolation` (response)

- **[extra_property]** HosViolation.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Remove `HosViolation.DriverId` (not in spec).
- **[extra_property]** HosViolation.driverName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Remove `HosViolation.DriverName` (not in spec).
- **[extra_property]** HosViolation.endMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Remove `HosViolation.EndMs` (not in spec).
- **[extra_property]** HosViolation.severityType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Remove `HosViolation.SeverityType` (not in spec).
- **[extra_property]** HosViolation.startMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Remove `HosViolation.StartMs` (not in spec).
- **[extra_property]** HosViolation.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Remove `HosViolation.VehicleId` (not in spec).
- **[extra_property]** HosViolation.violationType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/hos/violations`
  - Recommended fix: Remove `HosViolation.ViolationType` (not in spec).

