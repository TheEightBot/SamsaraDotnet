# Safety Scores — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/40-safety-scores.md`](../40-safety-scores.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `DriverSafetyScore` | response | 0 | 5 | 0 | 5 |
| `TagGroupSafetyScore` | response | 0 | 5 | 0 | 5 |
| `TagSafetyScore` | response | 0 | 5 | 0 | 4 |
| `VehicleSafetyScore` | response | 0 | 5 | 0 | 9 |
| `(no SDK type)` | query | 0 | 2 | 4 | 0 |

**Counts**: CRITICAL=0, HIGH=22, MEDIUM=4, LOW=23  
**Total deduped findings**: 49

## HIGH (22)

### `(no SDK type)` (query)

- **[missing_required_query]** ListTagSafetyScoresAsync (GET /safety-scores/tags) is missing query parameter `scoreType` (spec REQUIRED, type=string).
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Add a required parameter (e.g. `string scoreType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("scoreType", ...)`.
- **[missing_required_query]** ListTagGroupSafetyScoresAsync (GET /safety-scores/tag-group) is missing query parameter `scoreType` (spec REQUIRED, type=string).
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Add a required parameter (e.g. `string scoreType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("scoreType", ...)`.

### `DriverSafetyScore` (response)

- **[response_drift_required]** DriverSafetyScore (response) missing REQUIRED property `behaviors` (spec type=array).
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Add `[JsonPropertyName("behaviors")] public IReadOnlyList<object> Behaviors { get; init; }` to response record `DriverSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverSafetyScore (response) missing REQUIRED property `driveDistanceMeters` (spec type=integer/int64).
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Add `[JsonPropertyName("driveDistanceMeters")] public long DriveDistanceMeters { get; init; }` to response record `DriverSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverSafetyScore (response) missing REQUIRED property `driveTimeMilliseconds` (spec type=integer/int64).
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Add `[JsonPropertyName("driveTimeMilliseconds")] public long DriveTimeMilliseconds { get; init; }` to response record `DriverSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverSafetyScore (response) missing REQUIRED property `driverScore` (spec type=integer/int32).
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Add `[JsonPropertyName("driverScore")] public int DriverScore { get; init; }` to response record `DriverSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverSafetyScore (response) missing REQUIRED property `speeding` (spec type=array).
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Add `[JsonPropertyName("speeding")] public IReadOnlyList<object> Speeding { get; init; }` to response record `DriverSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `TagGroupSafetyScore` (response)

- **[response_drift_required]** TagGroupSafetyScore (response) missing REQUIRED property `behaviors` (spec type=array).
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Add `[JsonPropertyName("behaviors")] public IReadOnlyList<object> Behaviors { get; init; }` to response record `TagGroupSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TagGroupSafetyScore (response) missing REQUIRED property `combinedScore` (spec type=integer/int32).
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Add `[JsonPropertyName("combinedScore")] public int CombinedScore { get; init; }` to response record `TagGroupSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TagGroupSafetyScore (response) missing REQUIRED property `driveDistanceMeters` (spec type=integer/int64).
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Add `[JsonPropertyName("driveDistanceMeters")] public long DriveDistanceMeters { get; init; }` to response record `TagGroupSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TagGroupSafetyScore (response) missing REQUIRED property `driveTimeMilliseconds` (spec type=integer/int64).
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Add `[JsonPropertyName("driveTimeMilliseconds")] public long DriveTimeMilliseconds { get; init; }` to response record `TagGroupSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TagGroupSafetyScore (response) missing REQUIRED property `speeding` (spec type=array).
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Add `[JsonPropertyName("speeding")] public IReadOnlyList<object> Speeding { get; init; }` to response record `TagGroupSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `TagSafetyScore` (response)

- **[response_drift_required]** TagSafetyScore (response) missing REQUIRED property `behaviors` (spec type=array).
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Add `[JsonPropertyName("behaviors")] public IReadOnlyList<object> Behaviors { get; init; }` to response record `TagSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TagSafetyScore (response) missing REQUIRED property `driveDistanceMeters` (spec type=integer/int64).
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Add `[JsonPropertyName("driveDistanceMeters")] public long DriveDistanceMeters { get; init; }` to response record `TagSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TagSafetyScore (response) missing REQUIRED property `driveTimeMilliseconds` (spec type=integer/int64).
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Add `[JsonPropertyName("driveTimeMilliseconds")] public long DriveTimeMilliseconds { get; init; }` to response record `TagSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TagSafetyScore (response) missing REQUIRED property `speeding` (spec type=array).
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Add `[JsonPropertyName("speeding")] public IReadOnlyList<object> Speeding { get; init; }` to response record `TagSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TagSafetyScore (response) missing REQUIRED property `tagScore` (spec type=integer/int32).
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Add `[JsonPropertyName("tagScore")] public int TagScore { get; init; }` to response record `TagSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `VehicleSafetyScore` (response)

- **[response_drift_required]** VehicleSafetyScore (response) missing REQUIRED property `behaviors` (spec type=array).
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Add `[JsonPropertyName("behaviors")] public IReadOnlyList<object> Behaviors { get; init; }` to response record `VehicleSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** VehicleSafetyScore (response) missing REQUIRED property `driveDistanceMeters` (spec type=integer/int64).
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Add `[JsonPropertyName("driveDistanceMeters")] public long DriveDistanceMeters { get; init; }` to response record `VehicleSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** VehicleSafetyScore (response) missing REQUIRED property `driveTimeMilliseconds` (spec type=integer/int64).
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Add `[JsonPropertyName("driveTimeMilliseconds")] public long DriveTimeMilliseconds { get; init; }` to response record `VehicleSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** VehicleSafetyScore (response) missing REQUIRED property `speeding` (spec type=array).
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Add `[JsonPropertyName("speeding")] public IReadOnlyList<object> Speeding { get; init; }` to response record `VehicleSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** VehicleSafetyScore (response) missing REQUIRED property `vehicleScore` (spec type=integer/int32).
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Add `[JsonPropertyName("vehicleScore")] public int VehicleScore { get; init; }` to response record `VehicleSafetyScore` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (4)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListDriverSafetyScoresAsync (GET /safety-scores/drivers) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListTagSafetyScoresAsync (GET /safety-scores/tags) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListTagGroupSafetyScoresAsync (GET /safety-scores/tag-group) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListVehicleSafetyScoresAsync (GET /safety-scores/vehicles) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

## LOW (23)

### `DriverSafetyScore` (response)

- **[extra_property]** DriverSafetyScore.safetyScore (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Remove `DriverSafetyScore.SafetyScore` (not in spec).
- **[extra_property]** DriverSafetyScore.timeRange (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Remove `DriverSafetyScore.TimeRange` (not in spec).
- **[extra_property]** DriverSafetyScore.totalDistanceDrivenMeters (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Remove `DriverSafetyScore.TotalDistanceDrivenMeters` (not in spec).
- **[extra_property]** DriverSafetyScore.totalHarshEventCount (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Remove `DriverSafetyScore.TotalHarshEventCount` (not in spec).
- **[extra_property]** DriverSafetyScore.totalTimeDrivenMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/drivers`
  - Recommended fix: Remove `DriverSafetyScore.TotalTimeDrivenMs` (not in spec).

### `TagGroupSafetyScore` (response)

- **[extra_property]** TagGroupSafetyScore.safetyScore (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Remove `TagGroupSafetyScore.SafetyScore` (not in spec).
- **[extra_property]** TagGroupSafetyScore.tagGroupId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Remove `TagGroupSafetyScore.TagGroupId` (not in spec).
- **[extra_property]** TagGroupSafetyScore.tagGroupName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Remove `TagGroupSafetyScore.TagGroupName` (not in spec).
- **[extra_property]** TagGroupSafetyScore.timeRange (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Remove `TagGroupSafetyScore.TimeRange` (not in spec).
- **[extra_property]** TagGroupSafetyScore.totalHarshEventCount (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tag-group`
  - Recommended fix: Remove `TagGroupSafetyScore.TotalHarshEventCount` (not in spec).

### `TagSafetyScore` (response)

- **[extra_property]** TagSafetyScore.safetyScore (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Remove `TagSafetyScore.SafetyScore` (not in spec).
- **[extra_property]** TagSafetyScore.tagName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Remove `TagSafetyScore.TagName` (not in spec).
- **[extra_property]** TagSafetyScore.timeRange (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Remove `TagSafetyScore.TimeRange` (not in spec).
- **[extra_property]** TagSafetyScore.totalHarshEventCount (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/tags`
  - Recommended fix: Remove `TagSafetyScore.TotalHarshEventCount` (not in spec).

### `VehicleSafetyScore` (response)

- **[extra_property]** VehicleSafetyScore.crashCount (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.CrashCount` (not in spec).
- **[extra_property]** VehicleSafetyScore.harshAccelCount (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.HarshAccelCount` (not in spec).
- **[extra_property]** VehicleSafetyScore.harshBrakingCount (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.HarshBrakingCount` (not in spec).
- **[extra_property]** VehicleSafetyScore.harshTurningCount (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.HarshTurningCount` (not in spec).
- **[extra_property]** VehicleSafetyScore.safetyScore (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.SafetyScore` (not in spec).
- **[extra_property]** VehicleSafetyScore.timeRange (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.TimeRange` (not in spec).
- **[extra_property]** VehicleSafetyScore.totalDistanceDrivenMeters (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.TotalDistanceDrivenMeters` (not in spec).
- **[extra_property]** VehicleSafetyScore.totalHarshEventCount (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.TotalHarshEventCount` (not in spec).
- **[extra_property]** VehicleSafetyScore.totalTimeDrivenMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /safety-scores/vehicles`
  - Recommended fix: Remove `VehicleSafetyScore.TotalTimeDrivenMs` (not in spec).

