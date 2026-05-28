# Trailers — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/47-trailers.md`](../47-trailers.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `e36edb9` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. LOW response/request-side extras were
intentionally retained as nullable back-compat properties per the workflow
precedent (cf. `40-safety-scores`, `44-tachograph-eu-only`, `45-tags`,
`46-trailer-assignments`) — grouped under a
`// Not in current spec; retained for back-compat.` comment rather than removed.

Files touched: `src/Samsara.Sdk/Models/Fleet/TrailerModels.cs`,
`src/Samsara.Sdk/Clients/Fleet/ITrailersClient.cs`,
`src/Samsara.Sdk/Clients/Fleet/TrailersClient.cs`,
`tools/Samsara.Cli/TuiApp.cs`.

**HIGH (3) — required `types` query param (breaking)**

- **`(no SDK type)` query — `GetStatsSnapshotAsync` / `GetStatsFeedAsync` /
  `GetStatsHistoryAsync` required `types`**: added to each as a required positional
  `string types` (no default, placed first so it precedes the existing defaulted
  params), appended via `QueryBuilder.WithParams("types", types)`. Breaking
  signature change on all three stats methods. Spec type is `string` (comma-joined
  stat-type list), so the param is `string`, NOT an array.

**MEDIUM (48)**

- **Query params (14)**: all spec query params are `type=string`, so each is a
  trailing `string?` passed straight through `QueryBuilder.WithParams` (callers
  pass already comma-separated strings). `ListAsync` gained `parentTagIds`,
  `tagIds`. `GetStatsSnapshotAsync` gained `parentTagIds`, `tagIds`, `time`,
  `trailerIds`. `GetStatsFeedAsync` gained `decorations`, `parentTagIds`, `tagIds`,
  `trailerIds`. `GetStatsHistoryAsync` gained `decorations`, `parentTagIds`,
  `tagIds`, `trailerIds` (keeping its existing `startTime`/`endTime` time range).
- **`Trailer` (response) — 3 props**: `attributes` (`IReadOnlyList<object>?`),
  `enabledForMobile` (`bool?`), `trailerSerialNumber` (`string?`).
- **`CreateTrailerRequest` — 3 props**: `attributes` (`IReadOnlyList<object>?`),
  `enabledForMobile` (`bool?`), `trailerSerialNumber` (`string?`).
- **`UpdateTrailerRequest` — 4 props**: `attributes` (`IReadOnlyList<object>?`),
  `enabledForMobile` (`bool?`), `odometerMeters` (`long?`, int64),
  `trailerSerialNumber` (`string?`).
- **`TrailerStats` (response) — 24 findings**: `name` tightened from `string?` to
  **`required string`** (`response_required_drift`; spec marks it REQUIRED — safe,
  no construction sites and the CLI does not read `TrailerStats`), plus 23 new
  `object?` props left weakly-typed per the plan: `carrierReeferState`, `gps`,
  `gpsOdometerMeters`, `reeferAlarms`, `reeferAmbientAirTemperatureMilliC`,
  `reeferDoorStateZone1/2/3`, `reeferFuelPercent`, `reeferObdEngineSeconds`,
  `reeferReturnAirTemperatureMilliCZone1/2/3`, `reeferRunMode`,
  `reeferSetPointTemperatureMilliCZone1/2/3`, `reeferStateZone1/2/3`,
  `reeferSupplyAirTemperatureMilliCZone1/2/3`.

**LOW (21) — all retained**

- All 21 `extra_property` findings (already present as nullable props) are
  RETAINED, not removed, grouped under a back-compat comment:
  - `Trailer` (6): `make`, `model`, `serial`, `vin`, `year`, `enabledForCommunication`.
  - `CreateTrailerRequest` (5): `make`, `model`, `serial`, `vin`, `year`.
  - `UpdateTrailerRequest` (5): `make`, `model`, `serial`, `vin`, `year`.
  - `TrailerStats` (5): `engineHours`, `location` (kept as its typed
    `TrailerLocation?`), `odometer`, `temperature`, `time`.

**Other files**: the CLI call site in `tools/Samsara.Cli/TuiApp.cs` was fixed for
one compile-breaker — `ListAsync(Timeout60s())` now passes the token by name
(`cancellationToken:`) so it no longer binds to the new `string? parentTagIds`.
The other Trailers CLI calls use `GetAsync`/`CreateAsync`/`UpdateAsync`/
`DeleteAsync` (unchanged) and the CLI calls no `GetStats*` method, so the breaking
`types` param needs no CLI fix. No `SamsaraJsonContext` change (`Trailer`,
`CreateTrailerRequest`, `UpdateTrailerRequest`, `TrailerStats`, `TrailerLocation`
already registered; new props are weakly-typed `object`/scalar/array, introducing
no new top-level types) and no test change (no construction sites; the facade test
only substitutes the interface).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 3 | 14 | 0 |
| `TrailerStats` | response | 0 | 0 | 24 | 5 |
| `UpdateTrailerRequest` | request | 0 | 0 | 4 | 5 |
| `CreateTrailerRequest` | request | 0 | 0 | 3 | 5 |
| `Trailer` | response | 0 | 0 | 3 | 6 |

**Counts**: CRITICAL=0, HIGH=3, MEDIUM=48, LOW=21  
**Total deduped findings**: 72

## HIGH (3)

### `(no SDK type)` (query)

- **[missing_required_query]** GetStatsSnapshotAsync (GET /fleet/trailers/stats) is missing query parameter `types` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/trailers/stats`
  - Recommended fix: Add a required parameter (e.g. `string types` , no default) to the SDK method and append it via `QueryBuilder.WithParams("types", ...)`.
- **[missing_required_query]** GetStatsFeedAsync (GET /fleet/trailers/stats/feed) is missing query parameter `types` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/trailers/stats/feed`
  - Recommended fix: Add a required parameter (e.g. `string types` , no default) to the SDK method and append it via `QueryBuilder.WithParams("types", ...)`.
- **[missing_required_query]** GetStatsHistoryAsync (GET /fleet/trailers/stats/history) is missing query parameter `types` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/trailers/stats/history`
  - Recommended fix: Add a required parameter (e.g. `string types` , no default) to the SDK method and append it via `QueryBuilder.WithParams("types", ...)`.

## MEDIUM (48)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/trailers/stats/feed) is missing query parameter `decorations` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats/feed`
  - Recommended fix: Add an optional parameter `string? decorations = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/trailers/stats/history) is missing query parameter `decorations` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats/history`
  - Recommended fix: Add an optional parameter `string? decorations = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/trailers) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsSnapshotAsync (GET /fleet/trailers/stats) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/trailers/stats/feed) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats/feed`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/trailers/stats/history) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats/history`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/trailers) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsSnapshotAsync (GET /fleet/trailers/stats) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/trailers/stats/feed) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats/feed`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/trailers/stats/history) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats/history`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsSnapshotAsync (GET /fleet/trailers/stats) is missing query parameter `time` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats`
  - Recommended fix: Add an optional parameter `string? time = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsSnapshotAsync (GET /fleet/trailers/stats) is missing query parameter `trailerIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats`
  - Recommended fix: Add an optional parameter `string? trailerIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsFeedAsync (GET /fleet/trailers/stats/feed) is missing query parameter `trailerIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats/feed`
  - Recommended fix: Add an optional parameter `string? trailerIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStatsHistoryAsync (GET /fleet/trailers/stats/history) is missing query parameter `trailerIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/trailers/stats/history`
  - Recommended fix: Add an optional parameter `string? trailerIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateTrailerRequest` (request)

- **[missing_optional]** CreateTrailerRequest is missing property `attributes` (spec type=array).
  - Endpoints: `POST /fleet/trailers`
  - Recommended fix: Add `[JsonPropertyName("attributes")] public IReadOnlyList<object>? Attributes { get; init; }` to `CreateTrailerRequest`.
- **[missing_optional]** CreateTrailerRequest is missing property `enabledForMobile` (spec type=boolean).
  - Endpoints: `POST /fleet/trailers`
  - Recommended fix: Add `[JsonPropertyName("enabledForMobile")] public bool? EnabledForMobile { get; init; }` to `CreateTrailerRequest`.
- **[missing_optional]** CreateTrailerRequest is missing property `trailerSerialNumber` (spec type=string).
  - Endpoints: `POST /fleet/trailers`
  - Recommended fix: Add `[JsonPropertyName("trailerSerialNumber")] public string? TrailerSerialNumber { get; init; }` to `CreateTrailerRequest`.

### `Trailer` (response)

- **[response_drift_optional]** Trailer (response) missing property `attributes` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Add `[JsonPropertyName("attributes")] public IReadOnlyList<object>? Attributes { get; init; }` to response record `Trailer`.
- **[response_drift_optional]** Trailer (response) missing property `enabledForMobile` (spec type=boolean). (affects 4 endpoints)
  - Endpoints: `GET /fleet/trailers`, `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Add `[JsonPropertyName("enabledForMobile")] public bool? EnabledForMobile { get; init; }` to response record `Trailer`.
- **[response_drift_optional]** Trailer (response) missing property `trailerSerialNumber` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /fleet/trailers`, `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Add `[JsonPropertyName("trailerSerialNumber")] public string? TrailerSerialNumber { get; init; }` to response record `Trailer`.

### `TrailerStats` (response)

- **[response_drift_optional]** TrailerStats (response) missing property `carrierReeferState` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("carrierReeferState")] public object? CarrierReeferState { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `gps` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("gps")] public object? Gps { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `gpsOdometerMeters` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("gpsOdometerMeters")] public object? GpsOdometerMeters { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferAlarms` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferAlarms")] public object? ReeferAlarms { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferAmbientAirTemperatureMilliC` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferAmbientAirTemperatureMilliC")] public object? ReeferAmbientAirTemperatureMilliC { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferDoorStateZone1` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferDoorStateZone1")] public object? ReeferDoorStateZone1 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferDoorStateZone2` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferDoorStateZone2")] public object? ReeferDoorStateZone2 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferDoorStateZone3` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferDoorStateZone3")] public object? ReeferDoorStateZone3 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferFuelPercent` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferFuelPercent")] public object? ReeferFuelPercent { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferObdEngineSeconds` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferObdEngineSeconds")] public object? ReeferObdEngineSeconds { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferReturnAirTemperatureMilliCZone1` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferReturnAirTemperatureMilliCZone1")] public object? ReeferReturnAirTemperatureMilliCZone1 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferReturnAirTemperatureMilliCZone2` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferReturnAirTemperatureMilliCZone2")] public object? ReeferReturnAirTemperatureMilliCZone2 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferReturnAirTemperatureMilliCZone3` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferReturnAirTemperatureMilliCZone3")] public object? ReeferReturnAirTemperatureMilliCZone3 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferRunMode` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferRunMode")] public object? ReeferRunMode { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferSetPointTemperatureMilliCZone1` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferSetPointTemperatureMilliCZone1")] public object? ReeferSetPointTemperatureMilliCZone1 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferSetPointTemperatureMilliCZone2` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferSetPointTemperatureMilliCZone2")] public object? ReeferSetPointTemperatureMilliCZone2 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferSetPointTemperatureMilliCZone3` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferSetPointTemperatureMilliCZone3")] public object? ReeferSetPointTemperatureMilliCZone3 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferStateZone1` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferStateZone1")] public object? ReeferStateZone1 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferStateZone2` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferStateZone2")] public object? ReeferStateZone2 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferStateZone3` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferStateZone3")] public object? ReeferStateZone3 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferSupplyAirTemperatureMilliCZone1` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferSupplyAirTemperatureMilliCZone1")] public object? ReeferSupplyAirTemperatureMilliCZone1 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferSupplyAirTemperatureMilliCZone2` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferSupplyAirTemperatureMilliCZone2")] public object? ReeferSupplyAirTemperatureMilliCZone2 { get; init; }` to response record `TrailerStats`.
- **[response_drift_optional]** TrailerStats (response) missing property `reeferSupplyAirTemperatureMilliCZone3` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Add `[JsonPropertyName("reeferSupplyAirTemperatureMilliCZone3")] public object? ReeferSupplyAirTemperatureMilliCZone3 { get; init; }` to response record `TrailerStats`.
- **[response_required_drift]** TrailerStats.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Tighten `TrailerStats.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateTrailerRequest` (request)

- **[missing_optional]** UpdateTrailerRequest is missing property `attributes` (spec type=array).
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Add `[JsonPropertyName("attributes")] public IReadOnlyList<object>? Attributes { get; init; }` to `UpdateTrailerRequest`.
- **[missing_optional]** UpdateTrailerRequest is missing property `enabledForMobile` (spec type=boolean).
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Add `[JsonPropertyName("enabledForMobile")] public bool? EnabledForMobile { get; init; }` to `UpdateTrailerRequest`.
- **[missing_optional]** UpdateTrailerRequest is missing property `odometerMeters` (spec type=integer/int64).
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Add `[JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }` to `UpdateTrailerRequest`.
- **[missing_optional]** UpdateTrailerRequest is missing property `trailerSerialNumber` (spec type=string).
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Add `[JsonPropertyName("trailerSerialNumber")] public string? TrailerSerialNumber { get; init; }` to `UpdateTrailerRequest`.

## LOW (21)

### `CreateTrailerRequest` (request)

- **[extra_property]** CreateTrailerRequest.make: present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/trailers`
  - Recommended fix: Remove `CreateTrailerRequest.Make` (not in spec).
- **[extra_property]** CreateTrailerRequest.model: present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/trailers`
  - Recommended fix: Remove `CreateTrailerRequest.Model` (not in spec).
- **[extra_property]** CreateTrailerRequest.serial: present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/trailers`
  - Recommended fix: Remove `CreateTrailerRequest.Serial` (not in spec).
- **[extra_property]** CreateTrailerRequest.vin: present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/trailers`
  - Recommended fix: Remove `CreateTrailerRequest.Vin` (not in spec).
- **[extra_property]** CreateTrailerRequest.year: present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/trailers`
  - Recommended fix: Remove `CreateTrailerRequest.Year` (not in spec).

### `Trailer` (response)

- **[extra_property]** Trailer.enabledForCommunication (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/trailers`, `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Remove `Trailer.EnabledForCommunication` (not in spec).
- **[extra_property]** Trailer.make (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/trailers`, `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Remove `Trailer.Make` (not in spec).
- **[extra_property]** Trailer.model (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/trailers`, `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Remove `Trailer.Model` (not in spec).
- **[extra_property]** Trailer.serial (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/trailers`, `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Remove `Trailer.Serial` (not in spec).
- **[extra_property]** Trailer.vin (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/trailers`, `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Remove `Trailer.Vin` (not in spec).
- **[extra_property]** Trailer.year (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/trailers`, `GET /fleet/trailers/{id}`, `PATCH /fleet/trailers/{id}`, `POST /fleet/trailers`
  - Recommended fix: Remove `Trailer.Year` (not in spec).

### `TrailerStats` (response)

- **[extra_property]** TrailerStats.engineHours (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Remove `TrailerStats.EngineHours` (not in spec).
- **[extra_property]** TrailerStats.location (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Remove `TrailerStats.Location` (not in spec).
- **[extra_property]** TrailerStats.odometer (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Remove `TrailerStats.Odometer` (not in spec).
- **[extra_property]** TrailerStats.temperature (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Remove `TrailerStats.Temperature` (not in spec).
- **[extra_property]** TrailerStats.time (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/trailers/stats`, `GET /fleet/trailers/stats/feed`, `GET /fleet/trailers/stats/history`
  - Recommended fix: Remove `TrailerStats.Time` (not in spec).

### `UpdateTrailerRequest` (request)

- **[extra_property]** UpdateTrailerRequest.make: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Remove `UpdateTrailerRequest.Make` (not in spec).
- **[extra_property]** UpdateTrailerRequest.model: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Remove `UpdateTrailerRequest.Model` (not in spec).
- **[extra_property]** UpdateTrailerRequest.serial: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Remove `UpdateTrailerRequest.Serial` (not in spec).
- **[extra_property]** UpdateTrailerRequest.vin: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Remove `UpdateTrailerRequest.Vin` (not in spec).
- **[extra_property]** UpdateTrailerRequest.year: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/trailers/{id}`
  - Recommended fix: Remove `UpdateTrailerRequest.Year` (not in spec).

