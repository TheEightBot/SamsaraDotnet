# Idling — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/23-idling.md`](../23-idling.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

