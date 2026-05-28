# Safety Scores — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/40-safety-scores.md`](../40-safety-scores.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `3e5a59c` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. LOW response-side extras were
intentionally retained as nullable back-compat properties per the workflow
precedent established in `08-carrier-proposed-assignments`,
`13-driver-trailer-assignments`, `14-driver-vehicle-assignments`,
`28-live-sharing-links`, `29-location-and-speed`, `30-maintenance`,
`36-readings`, and `39-safety` — response-side flat-scalar conveniences kept
with XML doc pointers to the canonical spec fields rather than removed outright.
The four score response models were realigned to their real spec schemas
(`VehicleSafetyScoreResponseBody`, `DriverSafetyScoreResponseBody`,
`TagSafetyScoreResponseBody`, `TagGroupSafetyScoreResponseBody`).

Files touched: `src/Samsara.Sdk/Models/Safety/SafetyModels.cs`,
`src/Samsara.Sdk/Clients/Safety/ISafetyClient.cs`,
`src/Samsara.Sdk/Clients/Safety/SafetyClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`,
`tools/Samsara.Cli/TuiApp.cs`,
`tests/Samsara.Sdk.Tests/SafetyClientExtensionTests.cs`.

**HIGH (22)**

- **`(no SDK type)` query — `ListTagSafetyScoresAsync` / `ListTagGroupSafetyScoresAsync`
  required `scoreType`**: added as a required positional `string scoreType` (no
  default, placed first so it precedes the existing defaulted `startTime`/`endTime`),
  appended via `QueryBuilder.WithParams("scoreType", scoreType)`. Valid spec values
  are `driver`/`vehicle`. Breaking signature change.
- **`DriverSafetyScore` (response) — required `behaviors`, `driveDistanceMeters`,
  `driveTimeMilliseconds`, `driverScore`, `speeding`**: all five added as `required`
  non-nullable. `behaviors`/`speeding` modeled as strongly-typed
  `IReadOnlyList<SafetyScoreBehavior>` / `IReadOnlyList<SafetyScoreSpeeding>` rather
  than the plan's `IReadOnlyList<object>` placeholders (repo convention, per `39-safety`).
- **`TagSafetyScore` (response) — required `behaviors`, `driveDistanceMeters`,
  `driveTimeMilliseconds`, `speeding`, `tagScore`**: all five added as `required`
  non-nullable (same typed-array treatment).
- **`TagGroupSafetyScore` (response) — required `behaviors`, `combinedScore`,
  `driveDistanceMeters`, `driveTimeMilliseconds`, `speeding`**: all five added as
  `required` non-nullable (same typed-array treatment). Note: the spec's
  `tag-group` schema does **not** include `tagGroupId`, so `tagGroupId` was demoted
  from `required` to a nullable back-compat extra (see LOW).
- **`VehicleSafetyScore` (response) — required `behaviors`, `driveDistanceMeters`,
  `driveTimeMilliseconds`, `speeding`, `vehicleScore`**: all five added as `required`
  non-nullable (same typed-array treatment).

**New typed nested records** (mirroring spec sub-schemas), both registered in
`SamsaraJsonContext`:

- `SafetyScoreBehavior` (`SafetyScoreBehaviorObjectResponseBody`): required
  `behaviorType` (string enum), `count` (`long`), `scoreImpact` (`double`).
- `SafetyScoreSpeeding` (`SafetyScoreSpeedingObjectResponseBody`): required
  `speedingType` (string enum), `durationMilliseconds` (`long`), `scoreImpact` (`double`).

**MEDIUM (4)**

- Added optional list filters to all four list methods: `vehicleIds`
  (`ListVehicleSafetyScoresAsync`), `driverIds` (`ListDriverSafetyScoresAsync`),
  `tagIds` (`ListTagSafetyScoresAsync` and `ListTagGroupSafetyScoresAsync`) — each
  `IReadOnlyList<string>? = null`, appended conditionally as a comma-joined value
  (matching the existing `assetIds`/`driverIds` pattern in `GetEventsStreamAsync`).

**LOW (23) — kept as nullable back-compat extras (conservative; not removed)**

- `VehicleSafetyScore`: `safetyScore`, `timeRange`, `totalDistanceDrivenMeters`,
  `totalHarshEventCount`, `totalTimeDrivenMs`, `crashCount`, `harshAccelCount`,
  `harshBrakingCount`, `harshTurningCount`.
- `DriverSafetyScore`: `safetyScore`, `timeRange`, `totalDistanceDrivenMeters`,
  `totalHarshEventCount`, `totalTimeDrivenMs`.
- `TagSafetyScore`: `safetyScore`, `tagName`, `timeRange`, `totalHarshEventCount`.
- `TagGroupSafetyScore`: `safetyScore`, `tagGroupId`, `tagGroupName`, `timeRange`,
  `totalHarshEventCount`.

  Each carries an XML doc pointer to the canonical spec field where applicable
  (e.g. `safetyScore` → `vehicleScore`/`driverScore`/`tagScore`/`combinedScore`,
  `totalTimeDrivenMs` → `driveTimeMilliseconds`). Retained to avoid breaking
  existing consumers (and the CLI/tests) for LOW findings.

**Callers updated**

- CLI (`tools/Samsara.Cli/TuiApp.cs`): the vehicle/driver safety-score actions now
  pass the cancellation token by name (the 3rd positional slot is now the
  `vehicleIds`/`driverIds` filter) and render the spec `VehicleScore`/`DriverScore`
  ints instead of the back-compat nullable `SafetyScore`.
- Tests (`SafetyClientExtensionTests.cs`): the two existing tag/tag-group tests now
  pass `scoreType` and include the new spec-REQUIRED fields in their mock JSON
  (required members throw on deserialization if absent), and assert
  `TagScore`/`CombinedScore` plus the `scoreType` query value.

Verification: `dotnet build` 0 errors/0 warnings, `dotnet test` 59 passed, and
`check-sdk-sync.py --fail-on-mismatch` exits 0 (323/323 matched).

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

