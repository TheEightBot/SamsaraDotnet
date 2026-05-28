# Speeding Intervals — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/43-speeding-intervals.md`](../43-speeding-intervals.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `85a752c` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. The single endpoint
(`GET /speeding-intervals/stream`, surfaced by `GetSpeedingIntervalsStreamAsync`)
covers both the `SpeedingInterval` response model and the query parameters.

**HIGH (6)**

- **`SpeedingInterval` (response) — 5 spec-REQUIRED fields**: all added as
  `required` non-nullable and ordered ahead of the retained extras. `asset` →
  `required object` (left weakly-typed per the repo convention of not
  schematizing config the plan left as `object`); `intervals` →
  `required IReadOnlyList<object>`; and the three timestamps `createdAtTime`,
  `tripStartTime`, `updatedAtTime` → `required DateTimeOffset` (the repo
  convention for `*AtTime`/time fields, not the plan's literal `string`). Safe
  because `SpeedingInterval` is deserialize-only (no construction sites).
- **`(query)` — `assetIds` spec-REQUIRED**: added as a leading
  `IReadOnlyList<string> assetIds` parameter (no default, before the optional
  params) and comma-joined onto the query string via `QueryBuilder.WithParams`,
  mirroring `ISafetyClient.GetEventsStreamAsync`. This is a **breaking**
  signature change to `GetSpeedingIntervalsStreamAsync`.

**MEDIUM (4)**

- **`(query)` — 4 optional params**: `queryBy` (`string?`), `severityLevels`
  (`IReadOnlyList<string>?`, serialized comma-joined like every query-array in
  this SDK rather than the plan's literal `IReadOnlyList<object>?`),
  `includeAsset` (`bool?`), `includeDriverId` (`bool?`) — all appended
  conditionally.

**LOW (10) — kept as nullable back-compat extras (conservative; not removed)**

- `SpeedingInterval`: `id`, `vehicleId`, `vehicleName`, `driverName`,
  `startTime`, `endTime`, `maxSpeedMph`, `speedLimitMph`, `latitude`,
  `longitude` — flat-scalar conveniences present in the SDK but absent from the
  current spec inner schema, kept (now ordered after the spec props) per the
  precedent established in `08-carrier-proposed-assignments`,
  `29-location-and-speed`, `30-maintenance`, `39-safety`, `40-safety-scores`,
  and `42-settings`. `id` was demoted from `required string` to `string?` (it is
  not in the spec schema, so leaving it `required` would risk deserialization
  failure — the same `required`→nullable demotion applied to `tagGroupId` in
  `40-safety-scores`). `driverId` was not flagged and is left unchanged.

Files touched: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`,
`src/Samsara.Sdk/Clients/Fleet/IVehiclesClient.cs`,
`src/Samsara.Sdk/Clients/Fleet/VehiclesClient.cs`. `SpeedingInterval` is already
registered in `SamsaraJsonContext` and the new fields are weakly-typed `object`
(no new top-level types), so no JsonContext change; there are no
construction/caller sites in `src`/`tools`/`tests`, so no CLI/test changes.

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

