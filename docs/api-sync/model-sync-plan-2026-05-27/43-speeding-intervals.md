# Speeding Intervals — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/43-speeding-intervals.md`](../43-speeding-intervals.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `SpeedingInterval` | response | 0 | 5 | 0 | 10 |
| `(no SDK type)` | query | 0 | 1 | 4 | 0 |

**Counts**: CRITICAL=0, HIGH=6, MEDIUM=4, LOW=10  
**Total deduped findings**: 20

## HIGH (6)

### `(no SDK type)` (query)

- **[missing_required_query]** GetSpeedingIntervalsStreamAsync (GET /speeding-intervals/stream) is missing query parameter `assetIds` (spec REQUIRED, type=array).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add a required parameter (e.g. `IReadOnlyList<string> assetIds` , no default) to the SDK method and append it via `QueryBuilder.WithParams("assetIds", ...)`.

### `SpeedingInterval` (response)

- **[response_drift_required]** SpeedingInterval (response) missing REQUIRED property `asset` (spec type=object).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add `[JsonPropertyName("asset")] public object Asset { get; init; }` to response record `SpeedingInterval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SpeedingInterval (response) missing REQUIRED property `createdAtTime` (spec type=string).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public string CreatedAtTime { get; init; }` to response record `SpeedingInterval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SpeedingInterval (response) missing REQUIRED property `intervals` (spec type=array).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add `[JsonPropertyName("intervals")] public IReadOnlyList<object> Intervals { get; init; }` to response record `SpeedingInterval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SpeedingInterval (response) missing REQUIRED property `tripStartTime` (spec type=string).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add `[JsonPropertyName("tripStartTime")] public string TripStartTime { get; init; }` to response record `SpeedingInterval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SpeedingInterval (response) missing REQUIRED property `updatedAtTime` (spec type=string).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public string UpdatedAtTime { get; init; }` to response record `SpeedingInterval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (4)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetSpeedingIntervalsStreamAsync (GET /speeding-intervals/stream) is missing query parameter `includeAsset` (spec optional, type=boolean).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add an optional parameter `bool? includeAsset = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSpeedingIntervalsStreamAsync (GET /speeding-intervals/stream) is missing query parameter `includeDriverId` (spec optional, type=boolean).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add an optional parameter `bool? includeDriverId = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSpeedingIntervalsStreamAsync (GET /speeding-intervals/stream) is missing query parameter `queryBy` (spec optional, type=string).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add an optional parameter `string? queryBy = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSpeedingIntervalsStreamAsync (GET /speeding-intervals/stream) is missing query parameter `severityLevels` (spec optional, type=array).
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? severityLevels = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

## LOW (10)

### `SpeedingInterval` (response)

- **[extra_property]** SpeedingInterval.driverName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.DriverName` (not in spec).
- **[extra_property]** SpeedingInterval.endTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.EndTime` (not in spec).
- **[extra_property]** SpeedingInterval.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.Id` (not in spec).
- **[extra_property]** SpeedingInterval.latitude (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.Latitude` (not in spec).
- **[extra_property]** SpeedingInterval.longitude (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.Longitude` (not in spec).
- **[extra_property]** SpeedingInterval.maxSpeedMph (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.MaxSpeedMph` (not in spec).
- **[extra_property]** SpeedingInterval.speedLimitMph (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.SpeedLimitMph` (not in spec).
- **[extra_property]** SpeedingInterval.startTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.StartTime` (not in spec).
- **[extra_property]** SpeedingInterval.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.VehicleId` (not in spec).
- **[extra_property]** SpeedingInterval.vehicleName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /speeding-intervals/stream`
  - Recommended fix: Remove `SpeedingInterval.VehicleName` (not in spec).

