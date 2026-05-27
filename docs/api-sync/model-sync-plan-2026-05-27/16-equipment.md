# Equipment — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/16-equipment.md`](../16-equipment.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `EquipmentLocation` | response | 0 | 2 | 1 | 3 |
| `(no SDK type)` | query | 0 | 0 | 20 | 0 |
| `EquipmentStats` | response | 0 | 0 | 15 | 4 |
| `Equipment` | response | 0 | 0 | 2 | 1 |

**Counts**: CRITICAL=0, HIGH=2, MEDIUM=38, LOW=8  
**Total deduped findings**: 48

## HIGH (2)

### `EquipmentLocation` (response)

- **[response_drift_required]** EquipmentLocation (response) missing REQUIRED property `location` (spec type=object).
  - Endpoints: `GET /fleet/equipment/locations`
  - Recommended fix: Add `[JsonPropertyName("location")] public object Location { get; init; }` to response record `EquipmentLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** EquipmentLocation (response) missing REQUIRED property `locations` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment/locations/feed`, `GET /fleet/equipment/locations/history`
  - Recommended fix: Add `[JsonPropertyName("locations")] public IReadOnlyList<object> Locations { get; init; }` to response record `EquipmentLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (38)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListLocationsAsync (GET /fleet/equipment/locations) is missing query parameter `equipmentIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? equipmentIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsFeedAsync (GET /fleet/equipment/locations/feed) is missing query parameter `equipmentIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? equipmentIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsHistoryAsync (GET /fleet/equipment/locations/history) is missing query parameter `equipmentIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? equipmentIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/equipment/stats/feed) is missing query parameter `equipmentIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? equipmentIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/equipment/stats/history) is missing query parameter `equipmentIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? equipmentIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsAsync (GET /fleet/equipment/stats) is missing query parameter `equipmentIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? equipmentIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/equipment) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /fleet/equipment/locations) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsFeedAsync (GET /fleet/equipment/locations/feed) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsHistoryAsync (GET /fleet/equipment/locations/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/equipment/stats/feed) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/equipment/stats/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsAsync (GET /fleet/equipment/stats) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/equipment) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /fleet/equipment/locations) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsFeedAsync (GET /fleet/equipment/locations/feed) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsHistoryAsync (GET /fleet/equipment/locations/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/locations/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/equipment/stats/feed) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/equipment/stats/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsAsync (GET /fleet/equipment/stats) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/equipment/stats`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `Equipment` (response)

- **[response_drift_optional]** Equipment (response) missing property `assetSerial` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment`, `GET /fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("assetSerial")] public string? AssetSerial { get; init; }` to response record `Equipment`.
- **[response_drift_optional]** Equipment (response) missing property `installedGateway` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment`, `GET /fleet/equipment/{id}`, `PATCH /beta/fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("installedGateway")] public object? InstalledGateway { get; init; }` to response record `Equipment`.

### `EquipmentLocation` (response)

- **[response_required_drift]** EquipmentLocation.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/locations`, `GET /fleet/equipment/locations/feed`, `GET /fleet/equipment/locations/history`
  - Recommended fix: Tighten `EquipmentLocation.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `EquipmentStats` (response)

- **[response_drift_optional]** EquipmentStats (response) missing property `engineRpm` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineRpm")] public IReadOnlyList<object>? EngineRpm { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `engineSeconds` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineSeconds")] public IReadOnlyList<object>? EngineSeconds { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `engineStates` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineStates")] public IReadOnlyList<object>? EngineStates { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `engineTotalIdleTimeMinutes` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("engineTotalIdleTimeMinutes")] public IReadOnlyList<object>? EngineTotalIdleTimeMinutes { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `fuelPercents` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("fuelPercents")] public IReadOnlyList<object>? FuelPercents { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `gatewayEngineSeconds` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("gatewayEngineSeconds")] public IReadOnlyList<object>? GatewayEngineSeconds { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `gatewayEngineState` (spec type=object).
  - Endpoints: `GET /fleet/equipment/stats`
  - Recommended fix: Add `[JsonPropertyName("gatewayEngineState")] public object? GatewayEngineState { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `gatewayEngineStates` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("gatewayEngineStates")] public IReadOnlyList<object>? GatewayEngineStates { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `gatewayJ1939EngineSeconds` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("gatewayJ1939EngineSeconds")] public IReadOnlyList<object>? GatewayJ1939EngineSeconds { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `gps` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("gps")] public IReadOnlyList<object>? Gps { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `gpsOdometerMeters` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("gpsOdometerMeters")] public IReadOnlyList<object>? GpsOdometerMeters { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `obdEngineSeconds` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("obdEngineSeconds")] public IReadOnlyList<object>? ObdEngineSeconds { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `obdEngineState` (spec type=object).
  - Endpoints: `GET /fleet/equipment/stats`
  - Recommended fix: Add `[JsonPropertyName("obdEngineState")] public object? ObdEngineState { get; init; }` to response record `EquipmentStats`.
- **[response_drift_optional]** EquipmentStats (response) missing property `obdEngineStates` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Add `[JsonPropertyName("obdEngineStates")] public IReadOnlyList<object>? ObdEngineStates { get; init; }` to response record `EquipmentStats`.
- **[response_required_drift]** EquipmentStats.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Tighten `EquipmentStats.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (8)

### `Equipment` (response)

- **[extra_property]** Equipment.equipmentSerialNumber (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment`, `GET /fleet/equipment/{id}`
  - Recommended fix: Remove `Equipment.EquipmentSerialNumber` (not in spec).

### `EquipmentLocation` (response)

- **[extra_property]** EquipmentLocation.latitude (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/locations`, `GET /fleet/equipment/locations/feed`, `GET /fleet/equipment/locations/history`
  - Recommended fix: Remove `EquipmentLocation.Latitude` (not in spec).
- **[extra_property]** EquipmentLocation.longitude (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/locations`, `GET /fleet/equipment/locations/feed`, `GET /fleet/equipment/locations/history`
  - Recommended fix: Remove `EquipmentLocation.Longitude` (not in spec).
- **[extra_property]** EquipmentLocation.time (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/locations`, `GET /fleet/equipment/locations/feed`, `GET /fleet/equipment/locations/history`
  - Recommended fix: Remove `EquipmentLocation.Time` (not in spec).

### `EquipmentStats` (response)

- **[extra_property]** EquipmentStats.engineState (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Remove `EquipmentStats.EngineState` (not in spec).
- **[extra_property]** EquipmentStats.fuelPercent (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Remove `EquipmentStats.FuelPercent` (not in spec).
- **[extra_property]** EquipmentStats.obdOdometer (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Remove `EquipmentStats.ObdOdometer` (not in spec).
- **[extra_property]** EquipmentStats.time (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/equipment/stats`, `GET /fleet/equipment/stats/feed`, `GET /fleet/equipment/stats/history`
  - Recommended fix: Remove `EquipmentStats.Time` (not in spec).

