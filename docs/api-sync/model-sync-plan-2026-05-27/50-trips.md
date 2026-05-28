# Trips — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/50-trips.md`](../50-trips.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `e15edee` on 2026-05-27**

## Implementation notes

The SDK's single `Trip` record is a DUAL-SHAPE unified record that has to
deserialize BOTH trip endpoints, which is why the plan's findings appear to
contradict each other. The real shapes are:

- **List** (`GET /v1/fleet/trips`) → v1 flat trip: `id`, `driverId`, `vehicleId`,
  `startTime`, `endTime`, `startLocation`, `endLocation`, `distanceMeters`, … (no
  `startLocation` guarantee — flagged as an `extra_property` for this endpoint).
- **Stream** (`GET /trips/stream`) → modern trip: `asset`, `completionStatus`,
  `createdAtTime`, `tripStartTime`, `tripEndTime`, `updatedAtTime`,
  `startLocation`-required, …

Because one record covers both shapes, fields present in only one shape MUST be
nullable, or deserializing the other endpoint throws. This drives several
deliberate deviations from the plan's verbatim recommendations:

**HIGH (6)**

- **`GetStreamAsync` `ids` (`missing_required_query`)**: added as a required
  `IReadOnlyList<string> ids` first parameter (no default), comma-joined and
  appended via `QueryBuilder.WithParams`. **Breaking** for any caller of
  `GetStreamAsync` (the CLI does not call it, so no CLI fix needed). Mirrors
  `ISafetyClient.GetEventsStreamAsync`.
- **`Trip` (response) — 5 "required" props modeled NULLABLE**: `asset`
  (weakly-typed `object?`), `completionStatus` (`string?`), `createdAtTime`,
  `tripStartTime`, `updatedAtTime` (all `DateTimeOffset?`). Although the spec marks
  these required on the `/trips/stream` shape, they are absent on the
  `/v1/fleet/trips` shape, so the plan's "else nullable" caveat applies — modeling
  them non-nullable would break v1 deserialization. Timestamps use
  `DateTimeOffset?` per repo convention rather than the plan's literal `string`.

**MEDIUM (6)**

- **3 optional query params on `GetStreamAsync`**: `completionStatus` (`string?`),
  `queryBy` (`string?`), `includeAsset` (`bool?`, lower-cased), appended
  conditionally via `QueryBuilder.WithParams`.
- **`Trip.tripEndTime` / `Trip.trips`**: added as `DateTimeOffset?` and
  `IReadOnlyList<object>?` respectively.
- **`Trip.startLocation` (`response_required_drift`) — NOT tightened**: kept as
  nullable `TripLocation?`. The plan flags it both as required-drift (tighten) AND
  as an `extra_property` for `/v1/fleet/trips`; because the v1 shape omits it,
  tightening to non-nullable would break v1 deserialization (same dual-shape
  resolution as `TrailerAssignment` in 46-trailer-assignments).

**LOW (13)**

- All 13 non-spec response extras (`id`, `driverId`, `driverName`, `vehicleId`,
  `vehicleName`, `startTime`, `endTime`, `startLocation`, `endLocation`,
  `distanceMeters`, `durationMs`, `fuelConsumedMl`, `coDriver`) are retained as
  nullable back-compat props per the workflow precedent, grouped under a
  `// Not in current spec; retained for back-compat.` comment rather than removed.
- **`Trip.Id` demoted `required string` → `string?`**: `id` is a LOW extra absent
  from both spec shapes; leaving it `required` would break deserialization of
  either endpoint (precedent: `SpeedingInterval.Id`, `TachographFile.Id`).
  **Breaking** for callers reading `Trip.Id` as non-null. The CLI render site
  (`TuiApp.cs`) was updated to `t.Id ?? ""`.

Files touched: `src/Samsara.Sdk/Models/Routes/TripModels.cs`,
`src/Samsara.Sdk/Clients/Routing/ITripsClient.cs`,
`src/Samsara.Sdk/Clients/Routing/TripsClient.cs`,
`tools/Samsara.Cli/TuiApp.cs`. `ListAsync` is unchanged. No JsonContext/test
changes (`Trip`/`TripLocation` already registered, new props weakly-typed /
scalar / array, no construction sites).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `Trip` | response | 0 | 5 | 3 | 13 |
| `(no SDK type)` | query | 0 | 1 | 3 | 0 |

**Counts**: CRITICAL=0, HIGH=6, MEDIUM=6, LOW=13  
**Total deduped findings**: 25

## HIGH (6)

### `(no SDK type)` (query)

- **[missing_required_query]** GetStreamAsync (GET /trips/stream) is missing query parameter `ids` (spec REQUIRED, type=array).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add a required parameter (e.g. `IReadOnlyList<string> ids` , no default) to the SDK method and append it via `QueryBuilder.WithParams("ids", ...)`.

### `Trip` (response)

- **[response_drift_required]** Trip (response) missing REQUIRED property `asset` (spec type=object).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add `[JsonPropertyName("asset")] public object Asset { get; init; }` to response record `Trip` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Trip (response) missing REQUIRED property `completionStatus` (spec type=string).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add `[JsonPropertyName("completionStatus")] public string CompletionStatus { get; init; }` to response record `Trip` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Trip (response) missing REQUIRED property `createdAtTime` (spec type=string).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public string CreatedAtTime { get; init; }` to response record `Trip` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Trip (response) missing REQUIRED property `tripStartTime` (spec type=string).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add `[JsonPropertyName("tripStartTime")] public string TripStartTime { get; init; }` to response record `Trip` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Trip (response) missing REQUIRED property `updatedAtTime` (spec type=string).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public string UpdatedAtTime { get; init; }` to response record `Trip` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (6)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetStreamAsync (GET /trips/stream) is missing query parameter `completionStatus` (spec optional, type=string).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add an optional parameter `string? completionStatus = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /trips/stream) is missing query parameter `includeAsset` (spec optional, type=boolean).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add an optional parameter `bool? includeAsset = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /trips/stream) is missing query parameter `queryBy` (spec optional, type=string).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add an optional parameter `string? queryBy = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `Trip` (response)

- **[response_drift_optional]** Trip (response) missing property `tripEndTime` (spec type=string).
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Add `[JsonPropertyName("tripEndTime")] public string? TripEndTime { get; init; }` to response record `Trip`.
- **[response_drift_optional]** Trip (response) missing property `trips` (spec type=array).
  - Endpoints: `GET /v1/fleet/trips`
  - Recommended fix: Add `[JsonPropertyName("trips")] public IReadOnlyList<object>? Trips { get; init; }` to response record `Trip`.
- **[response_required_drift]** Trip.startLocation (response): spec marks REQUIRED but SDK exposes as nullable (`TripLocation?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /trips/stream`
  - Recommended fix: Tighten `Trip.StartLocation` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (13)

### `Trip` (response)

- **[extra_property]** Trip.coDriver (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.CoDriver` (not in spec).
- **[extra_property]** Trip.distanceMeters (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.DistanceMeters` (not in spec).
- **[extra_property]** Trip.driverId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.DriverId` (not in spec).
- **[extra_property]** Trip.driverName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.DriverName` (not in spec).
- **[extra_property]** Trip.durationMs (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.DurationMs` (not in spec).
- **[extra_property]** Trip.endLocation (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.EndLocation` (not in spec).
- **[extra_property]** Trip.endTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.EndTime` (not in spec).
- **[extra_property]** Trip.fuelConsumedMl (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.FuelConsumedMl` (not in spec).
- **[extra_property]** Trip.id (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.Id` (not in spec).
- **[extra_property]** Trip.startLocation (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.StartLocation` (not in spec).
- **[extra_property]** Trip.startTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.StartTime` (not in spec).
- **[extra_property]** Trip.vehicleId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.VehicleId` (not in spec).
- **[extra_property]** Trip.vehicleName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /trips/stream`, `GET /v1/fleet/trips`
  - Recommended fix: Remove `Trip.VehicleName` (not in spec).

