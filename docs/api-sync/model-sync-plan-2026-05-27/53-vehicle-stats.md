# Vehicle Stats — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/53-vehicle-stats.md`](../53-vehicle-stats.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `cab18c2` on 2026-05-27**

## Implementation notes

All 77 MEDIUM findings were applied; the 4 LOW response-side extras were
intentionally retained as nullable back-compat properties per the workflow
precedent (cf. `40-safety-scores`, `49-trainingcourses`, `50-trips`, `51-users`,
`52-vehicle-locations`) rather than removed.

**`VehicleStats` response (65 changes)**

- **58 new `object?` props** added for the spec's `type=object` inner-schema
  fields (each weakly typed per the response convention): the aux-input bank
  (`auxInput1`–`auxInput13`), the EV telemetry set
  (`evAverageBatteryTemperatureMilliCelsius`, `evBatteryCurrentMilliAmp`,
  `evBatteryStateOfHealthMilliPercent`, `evBatteryVoltageMilliVolt`,
  `evChargingCurrentMilliAmp`, `evChargingEnergyMicroWh`, `evChargingStatus`,
  `evChargingVoltageMilliVolt`, `evConsumedEnergyMicroWh`, `evDistanceDrivenMeters`,
  `evRegeneratedEnergyMicroWh`, `evStateOfChargeMilliPercent`), the spreader bank
  (`spreaderActive`, `spreaderAirTemp`, `spreaderBlastState`, `spreaderGranularName`,
  `spreaderGranularRate`, `spreaderLiquidName`, `spreaderLiquidRate`,
  `spreaderOnState`, `spreaderPlowStatus`, `spreaderPrewetName`, `spreaderPrewetRate`,
  `spreaderRoadTemp`), and the remaining scalars (`ambientAirTemperatureMilliC`,
  `barometricPressurePa`, `batteryMilliVolts`, `defLevelMilliPercent`,
  `ecuDoorStatus`, `ecuSpeedMph`, `engineCoolantTemperatureMilliC`,
  `engineImmobilizer`, `engineLoadPercent`, `engineOilPressureKPa`, `engineRpm`,
  `externalIds`, `faultCodes`, `fuelConsumedMilliliters`, `gpsDistanceMeters`,
  `idlingDurationMilliseconds`, `intakeManifoldTemperatureMilliC`, `nfcCardScan`,
  `obdEngineSeconds`, `seatbeltDriver`, `syntheticEngineSeconds`).
- **3 new `IReadOnlyList<object>?` array props**: `engineStates`, `fuelPercents`,
  `nfcCardScans` (feed/history shapes only).
- **`name` (response_required_drift)**: tightened from `string?` to `required
  string`. SAFE — no `new VehicleStats(...)` construction sites exist in
  src/tools/tests. **Breaking**: consumers may now rely on a non-null `Name`.
- **3 `type_mismatch` fields → `object?`** (deviation from the plan's
  `IReadOnlyList<object>` recommendation): `gps`, `gpsOdometerMeters`, and
  `obdOdometerMeters` are each a single object on the snapshot endpoint
  (`GET /fleet/vehicles/stats`) but an array on feed/history, so they were modeled
  as `object?` to accept **either** shape (matches the 58 new `object?` props and
  the dual-shape `EquipmentStats` precedent in this same file). The now-unused
  `GpsData` / `GpsOdometer` / `ObdOdometer` typed records (and their
  `SamsaraJsonContext` registrations) were left in place — removing them is out of
  scope.

**MEDIUM query params (12 across 3 stats methods)**

The three stats methods embedded `?types={…}` directly in the path string; they
were refactored to build the query via `QueryBuilder.WithParams` (cleaner,
URL-encodes), with `types` always included. New optional params (each
`IReadOnlyList<string>? = null`, comma-joined, except `time` which is `string?`):

- `ListStatsAsync`: `vehicleIds`, `tagIds`, `parentTagIds`, `time`.
- `GetStatsFeedAsync`: `vehicleIds`, `tagIds`, `parentTagIds`, `decorations`.
- `GetStatsHistoryAsync`: `vehicleIds`, `tagIds`, `parentTagIds`, `decorations`
  (after the existing `startTime`/`endTime`).

**LOW (4) — response-side extras**

- `time` (`DateTimeOffset?`), `engineState` (`EngineState?`), `fuelPercent`
  (`FuelPercent?`), and `engineSeconds` (`EngineSeconds?`) were RETAINED as-is
  (already nullable) under a `// Not in current spec; retained for back-compat.`
  comment, keeping their typed records.

**Caller updated**

- CLI (`tools/Samsara.Cli/TuiApp.cs`): the `List Stats` vehicle action now passes
  the cancellation token by name (`cancellationToken:` — the 1st positional slot
  after `types` is now the `vehicleIds` filter) and drops the now-redundant `?? ""`
  on `s.Name` (now non-null). The CLI does not call `GetStatsFeed`/`GetStatsHistory`.

Files touched: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`,
`src/Samsara.Sdk/Clients/Fleet/IVehiclesClient.cs`,
`src/Samsara.Sdk/Clients/Fleet/VehiclesClient.cs`,
`tools/Samsara.Cli/TuiApp.cs`. No `SamsaraJsonContext` changes (`VehicleStats`
and the inner typed records already registered; new props weakly-typed
`object`/array → no new top-level types). No test changes (no
`new VehicleStats(...)` construction). Other Vehicles methods untouched.

Verification: `dotnet build` 0 errors / 0 warnings, `dotnet test` 59 passed, and
`check-sdk-sync.py --fail-on-mismatch` exits 0 (323/323 matched).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `VehicleStats` | response | 0 | 0 | 65 | 4 |
| `(no SDK type)` | query | 0 | 0 | 12 | 0 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=77, LOW=4  
**Total deduped findings**: 81

## MEDIUM (77)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/vehicles/stats/feed) is missing query parameter `decorations` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? decorations = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/vehicles/stats/history) is missing query parameter `decorations` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? decorations = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListStatsAsync (GET /fleet/vehicles/stats) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/vehicles/stats/feed) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/vehicles/stats/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListStatsAsync (GET /fleet/vehicles/stats) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/vehicles/stats/feed) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/vehicles/stats/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListStatsAsync (GET /fleet/vehicles/stats) is missing query parameter `time` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles/stats`
  - Recommended fix: Add an optional parameter `string? time = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListStatsAsync (GET /fleet/vehicles/stats) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/vehicles/stats/feed) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/vehicles/stats/history) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `VehicleStats` (response)

- **[response_drift_optional]** VehicleStats (response) missing property `ambientAirTemperatureMilliC` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("ambientAirTemperatureMilliC")] public object? AmbientAirTemperatureMilliC { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput1` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput1")] public object? AuxInput1 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput10` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput10")] public object? AuxInput10 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput11` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput11")] public object? AuxInput11 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput12` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput12")] public object? AuxInput12 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput13` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput13")] public object? AuxInput13 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput2` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput2")] public object? AuxInput2 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput3` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput3")] public object? AuxInput3 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput4` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput4")] public object? AuxInput4 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput5` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput5")] public object? AuxInput5 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput6` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput6")] public object? AuxInput6 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput7` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput7")] public object? AuxInput7 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput8` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput8")] public object? AuxInput8 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `auxInput9` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("auxInput9")] public object? AuxInput9 { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `barometricPressurePa` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("barometricPressurePa")] public object? BarometricPressurePa { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `batteryMilliVolts` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("batteryMilliVolts")] public object? BatteryMilliVolts { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `defLevelMilliPercent` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("defLevelMilliPercent")] public object? DefLevelMilliPercent { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `ecuDoorStatus` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("ecuDoorStatus")] public object? EcuDoorStatus { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `ecuSpeedMph` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("ecuSpeedMph")] public object? EcuSpeedMph { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `engineCoolantTemperatureMilliC` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineCoolantTemperatureMilliC")] public object? EngineCoolantTemperatureMilliC { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `engineImmobilizer` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineImmobilizer")] public object? EngineImmobilizer { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `engineLoadPercent` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineLoadPercent")] public object? EngineLoadPercent { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `engineOilPressureKPa` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineOilPressureKPa")] public object? EngineOilPressureKPa { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `engineRpm` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineRpm")] public object? EngineRpm { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `engineStates` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineStates")] public IReadOnlyList<object>? EngineStates { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evAverageBatteryTemperatureMilliCelsius` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evAverageBatteryTemperatureMilliCelsius")] public object? EvAverageBatteryTemperatureMilliCelsius { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evBatteryCurrentMilliAmp` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evBatteryCurrentMilliAmp")] public object? EvBatteryCurrentMilliAmp { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evBatteryStateOfHealthMilliPercent` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evBatteryStateOfHealthMilliPercent")] public object? EvBatteryStateOfHealthMilliPercent { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evBatteryVoltageMilliVolt` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evBatteryVoltageMilliVolt")] public object? EvBatteryVoltageMilliVolt { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evChargingCurrentMilliAmp` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evChargingCurrentMilliAmp")] public object? EvChargingCurrentMilliAmp { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evChargingEnergyMicroWh` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evChargingEnergyMicroWh")] public object? EvChargingEnergyMicroWh { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evChargingStatus` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evChargingStatus")] public object? EvChargingStatus { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evChargingVoltageMilliVolt` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evChargingVoltageMilliVolt")] public object? EvChargingVoltageMilliVolt { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evConsumedEnergyMicroWh` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evConsumedEnergyMicroWh")] public object? EvConsumedEnergyMicroWh { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evDistanceDrivenMeters` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evDistanceDrivenMeters")] public object? EvDistanceDrivenMeters { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evRegeneratedEnergyMicroWh` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evRegeneratedEnergyMicroWh")] public object? EvRegeneratedEnergyMicroWh { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `evStateOfChargeMilliPercent` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("evStateOfChargeMilliPercent")] public object? EvStateOfChargeMilliPercent { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `externalIds` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("externalIds")] public object? ExternalIds { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `faultCodes` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("faultCodes")] public object? FaultCodes { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `fuelConsumedMilliliters` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("fuelConsumedMilliliters")] public object? FuelConsumedMilliliters { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `fuelPercents` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("fuelPercents")] public IReadOnlyList<object>? FuelPercents { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `gpsDistanceMeters` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("gpsDistanceMeters")] public object? GpsDistanceMeters { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `idlingDurationMilliseconds` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("idlingDurationMilliseconds")] public object? IdlingDurationMilliseconds { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `intakeManifoldTemperatureMilliC` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("intakeManifoldTemperatureMilliC")] public object? IntakeManifoldTemperatureMilliC { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `nfcCardScan` (spec type=object).
  - Endpoints: `GET /fleet/vehicles/stats`
  - Recommended fix: Add `[JsonPropertyName("nfcCardScan")] public object? NfcCardScan { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `nfcCardScans` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("nfcCardScans")] public IReadOnlyList<object>? NfcCardScans { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `obdEngineSeconds` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("obdEngineSeconds")] public object? ObdEngineSeconds { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `seatbeltDriver` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("seatbeltDriver")] public object? SeatbeltDriver { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderActive` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderActive")] public object? SpreaderActive { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderAirTemp` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderAirTemp")] public object? SpreaderAirTemp { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderBlastState` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderBlastState")] public object? SpreaderBlastState { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderGranularName` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderGranularName")] public object? SpreaderGranularName { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderGranularRate` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderGranularRate")] public object? SpreaderGranularRate { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderLiquidName` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderLiquidName")] public object? SpreaderLiquidName { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderLiquidRate` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderLiquidRate")] public object? SpreaderLiquidRate { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderOnState` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderOnState")] public object? SpreaderOnState { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderPlowStatus` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderPlowStatus")] public object? SpreaderPlowStatus { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderPrewetName` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderPrewetName")] public object? SpreaderPrewetName { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderPrewetRate` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderPrewetRate")] public object? SpreaderPrewetRate { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `spreaderRoadTemp` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("spreaderRoadTemp")] public object? SpreaderRoadTemp { get; init; }` to response record `VehicleStats`.
- **[response_drift_optional]** VehicleStats (response) missing property `syntheticEngineSeconds` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Add `[JsonPropertyName("syntheticEngineSeconds")] public object? SyntheticEngineSeconds { get; init; }` to response record `VehicleStats`.
- **[response_required_drift]** VehicleStats.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /fleet/vehicles/stats`
  - Recommended fix: Tighten `VehicleStats.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[type_mismatch]** VehicleStats.gps (response): SDK `GpsData?` vs spec `array`. (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Change `VehicleStats.Gps` from `GpsData?` to `IReadOnlyList<object>`.
- **[type_mismatch]** VehicleStats.gpsOdometerMeters (response): SDK `GpsOdometer?` vs spec `array`. (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Change `VehicleStats.GpsOdometerMeters` from `GpsOdometer?` to `IReadOnlyList<object>`.
- **[type_mismatch]** VehicleStats.obdOdometerMeters (response): SDK `ObdOdometer?` vs spec `array`. (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Change `VehicleStats.ObdOdometerMeters` from `ObdOdometer?` to `IReadOnlyList<object>`.

## LOW (4)

### `VehicleStats` (response)

- **[extra_property]** VehicleStats.engineSeconds (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Remove `VehicleStats.EngineSeconds` (not in spec).
- **[extra_property]** VehicleStats.engineState (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Remove `VehicleStats.EngineState` (not in spec).
- **[extra_property]** VehicleStats.fuelPercent (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Remove `VehicleStats.FuelPercent` (not in spec).
- **[extra_property]** VehicleStats.time (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/stats`, `GET /fleet/vehicles/stats/feed`, `GET /fleet/vehicles/stats/history`
  - Recommended fix: Remove `VehicleStats.Time` (not in spec).

