# Idling — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/23-idling.md`](../23-idling.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

## Implementation notes

All HIGH (8), MEDIUM (15), and LOW (8) findings were applied — 31 total.

Files touched:

- `src/Samsara.Sdk/Models/Fleet/IdlingModels.cs` — full rewrite of
  `IdlingEvent` plus five new nested records.
- `src/Samsara.Sdk/Clients/Fleet/IIdlingClient.cs` — `ListEventsAsync`
  surface expanded from 2 to 13 parameters.
- `src/Samsara.Sdk/Clients/Fleet/IdlingClient.cs` — query-string composition
  via `QueryBuilder.WithParams`, mirroring `FuelClient`'s array-join pattern.
- `src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs` — added five new
  `[JsonSerializable]` registrations for the nested types.

**HIGH (8) — `IdlingEvent` (response) spec-REQUIRED fields**

All eight spec-REQUIRED inner-schema fields were added as non-nullable
`required` members. Where the plan recommended `object`, the codebase
convention (established in `13-driver-trailer-assignments` and
`14-driver-vehicle-assignments`) is to model spec sub-schemas as concrete
nested records. So:

- **`asset`** → `IdlingEventAsset` (required `long Id`, optional
  `IReadOnlyDictionary<string, string>? ExternalIds`) — mirrors
  `IdlingEventAssetObjectResponseBody`.
- **`durationMilliseconds`** → `required long` (spec `integer/int64`).
- **`eventUuid`** → `required string`.
- **`fuelConsumedMilliliters`** → `required double` (spec `number/double`).
- **`fuelCost`** → `IdlingEventFuelCost` (required `Amount` string,
  required `Currency` enum-as-string) — mirrors `FuelCostObjectResponseBody`.
- **`gaseousFuelConsumedGrams`** → `required double` (spec `number/double`).
- **`gaseousFuelCost`** → `IdlingEventGaseousFuelCost` (same shape as
  `IdlingEventFuelCost`) — mirrors `GaseousFuelCostObjectResponseBody`.
- **`ptoState`** → `required string`. The spec declares `enum: ["active, inactive"]`
  with a single comma-joined entry — that is a spec defect, the prose
  description clarifies the two valid values are `active` and `inactive`.
  Modelled as `string` to avoid hard-coding the broken enum.

**MEDIUM (15)**

- **11 missing optional query parameters on `ListEventsAsync`** — added to
  both `IIdlingClient` and `IdlingClient`. Array params use `string.Join(",", …)`
  per the `FuelClient` precedent; integer/boolean params use
  `ToString(CultureInfo.InvariantCulture)` with booleans lowercased to match
  the spec's lowercase form. Order in the method signature: spec-required
  pair first (`startTime`, `endTime` — kept nullable on the SDK side because
  the existing surface accepted them as optional, and the only caller is
  `PaginateAsync`), then the spec-optional surface in spec order.
- **`IdlingEvent.startTime` tightened** from `DateTimeOffset?` to `required
  DateTimeOffset` (spec-REQUIRED). Listed under MEDIUM in the plan
  (`response_required_drift`) but applied together with the HIGH adds.
- **`IdlingEvent.address`** changed from `string?` to
  `IdlingEventAddress?` (spec `object`, optional) — concrete nested record
  rather than the plan's recommended `object` placeholder.
- **`IdlingEvent.airTemperatureMillicelsius`** added as `long?`
  (spec `integer/int64`, optional).
- **`IdlingEvent.operator`** added as `IdlingEventOperator?`
  (spec `object`, optional) — concrete nested record rather than `object`.

**LOW (8) — extra-property removals**

All eight SDK-only flat scalars absent from
`IdlingEventObject_V2025_10_23ResponseBody` were removed: `id`, `vehicleId`,
`vehicleName`, `driverId`, `driverName`, `endTime`, `durationMs`,
`fuelConsumedMl`. There are no SDK consumers (no test/TUI references), so
nothing else needed updating. The flat-scalar back-compat approach used in
`13`/`14` was not applied here because the new nested records use the same
JSON property names as the spec (`asset.id`, `operator.id`, etc.) — keeping
the flat scalars alongside would have produced confusing duplication
without a meaningful migration aid, since the source data flowed only
through the spec-aligned JSON payload.

Build is green and `tools/check-sdk-sync.py` reports `mismatched=0` /
`not implemented=0`. All 59 unit tests pass.

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `IdlingEvent` | response | 0 | 8 | 4 | 8 |
| `(no SDK type)` | query | 0 | 0 | 11 | 0 |

**Counts**: CRITICAL=0, HIGH=8, MEDIUM=15, LOW=8  
**Total deduped findings**: 31

## HIGH (8)

### `IdlingEvent` (response)

- **[response_drift_required]** IdlingEvent (response) missing REQUIRED property `asset` (spec type=object).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("asset")] public object Asset { get; init; }` to response record `IdlingEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** IdlingEvent (response) missing REQUIRED property `durationMilliseconds` (spec type=integer/int64).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("durationMilliseconds")] public long DurationMilliseconds { get; init; }` to response record `IdlingEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** IdlingEvent (response) missing REQUIRED property `eventUuid` (spec type=string).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("eventUuid")] public string EventUuid { get; init; }` to response record `IdlingEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** IdlingEvent (response) missing REQUIRED property `fuelConsumedMilliliters` (spec type=number/double).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("fuelConsumedMilliliters")] public double FuelConsumedMilliliters { get; init; }` to response record `IdlingEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** IdlingEvent (response) missing REQUIRED property `fuelCost` (spec type=object).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("fuelCost")] public object FuelCost { get; init; }` to response record `IdlingEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** IdlingEvent (response) missing REQUIRED property `gaseousFuelConsumedGrams` (spec type=number/double).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("gaseousFuelConsumedGrams")] public double GaseousFuelConsumedGrams { get; init; }` to response record `IdlingEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** IdlingEvent (response) missing REQUIRED property `gaseousFuelCost` (spec type=object).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("gaseousFuelCost")] public object GaseousFuelCost { get; init; }` to response record `IdlingEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** IdlingEvent (response) missing REQUIRED property `ptoState` (spec type=string).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("ptoState")] public string PtoState { get; init; }` to response record `IdlingEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (15)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `excludeEventsWithUnknownAirTemperature` (spec optional, type=boolean).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `bool? excludeEventsWithUnknownAirTemperature = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `maxAirTemperatureMillicelsius` (spec optional, type=integer).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `int? maxAirTemperatureMillicelsius = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `maxDurationMilliseconds` (spec optional, type=integer).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `int? maxDurationMilliseconds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `minAirTemperatureMillicelsius` (spec optional, type=integer).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `int? minAirTemperatureMillicelsius = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `minDurationMilliseconds` (spec optional, type=integer).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `int? minDurationMilliseconds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `operatorIds` (spec optional, type=array).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? operatorIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `ptoState` (spec optional, type=string).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `string? ptoState = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /idling/events) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `IdlingEvent` (response)

- **[response_drift_optional]** IdlingEvent (response) missing property `airTemperatureMillicelsius` (spec type=integer/int64).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("airTemperatureMillicelsius")] public long? AirTemperatureMillicelsius { get; init; }` to response record `IdlingEvent`.
- **[response_drift_optional]** IdlingEvent (response) missing property `operator` (spec type=object).
  - Endpoints: `GET /idling/events`
  - Recommended fix: Add `[JsonPropertyName("operator")] public object? Operator { get; init; }` to response record `IdlingEvent`.
- **[response_required_drift]** IdlingEvent.startTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Tighten `IdlingEvent.StartTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[type_mismatch]** IdlingEvent.address (response): SDK `string?` vs spec `object`.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Change `IdlingEvent.Address` from `string?` to `object`.

## LOW (8)

### `IdlingEvent` (response)

- **[extra_property]** IdlingEvent.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Remove `IdlingEvent.DriverId` (not in spec).
- **[extra_property]** IdlingEvent.driverName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Remove `IdlingEvent.DriverName` (not in spec).
- **[extra_property]** IdlingEvent.durationMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Remove `IdlingEvent.DurationMs` (not in spec).
- **[extra_property]** IdlingEvent.endTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Remove `IdlingEvent.EndTime` (not in spec).
- **[extra_property]** IdlingEvent.fuelConsumedMl (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Remove `IdlingEvent.FuelConsumedMl` (not in spec).
- **[extra_property]** IdlingEvent.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Remove `IdlingEvent.Id` (not in spec).
- **[extra_property]** IdlingEvent.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Remove `IdlingEvent.VehicleId` (not in spec).
- **[extra_property]** IdlingEvent.vehicleName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /idling/events`
  - Recommended fix: Remove `IdlingEvent.VehicleName` (not in spec).

