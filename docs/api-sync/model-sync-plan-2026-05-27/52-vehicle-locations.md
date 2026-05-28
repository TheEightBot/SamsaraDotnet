# Vehicle Locations — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/52-vehicle-locations.md`](../52-vehicle-locations.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `5015190` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied; the LOW response-side extras were
intentionally retained as nullable back-compat properties per the workflow
precedent (cf. `40-safety-scores`, `49-trainingcourses`, `50-trips`, `51-users`)
rather than removed.

**Tri-shape record caveat (verified).** The single `VehicleLocation` record
deserializes THREE endpoints with mutually exclusive top-level shapes:
`ListLocationsAsync` (`GET /fleet/vehicles/locations`) returns a snapshot with a
top-level `location` object, while `GetLocationsFeedAsync` (`.../feed`) and
`GetLocationsHistoryAsync` (`.../history`) return a top-level `locations` array.
Because `location` and `locations` never co-occur, both HIGH `response_drift_required`
props were modeled **nullable** (`object? Location`, `IReadOnlyList<object>? Locations`)
rather than `required` — marking either `required` would throw on deserialization
of the other shape. This is the "spec marks REQUIRED → else nullable when not
guaranteed across all shapes" branch of the convention.

**HIGH (2)**

- `VehicleLocation.location` (object) and `VehicleLocation.locations` (array) added
  as nullable (`object?` / `IReadOnlyList<object>?`) — weakly typed per the
  query/response-array convention; see tri-shape caveat above.

**MEDIUM (11)**

- **`VehicleLocation.name` (response_required_drift)**: tightened from `string?` to
  `required string`. Present in all 3 shapes; SAFE — no `new VehicleLocation(...)`
  construction sites exist in src/tools/tests. **Breaking**: consumers may now rely
  on non-null `Name`.
- **10 optional query params** added across the 3 location methods (each
  `IReadOnlyList<string>? = null`, comma-joined and appended via
  `QueryBuilder.WithParams`, except `time` which is `string?`):
  - `ListLocationsAsync`: `vehicleIds`, `tagIds`, `parentTagIds`, `time`.
  - `GetLocationsFeedAsync`: `vehicleIds`, `tagIds`, `parentTagIds`.
  - `GetLocationsHistoryAsync`: `vehicleIds`, `tagIds`, `parentTagIds` (after the
    existing `startTime`/`endTime`).

**LOW (7) — response-side extras**

- `latitude`, `longitude`, and `time` were **demoted** from `required` to nullable
  (`double?` / `double?` / `DateTimeOffset?`). These are non-spec SDK flat-scalar
  conveniences absent from the wrapper schemas; leaving them `required` would break
  deserialization of the real `location`/`locations` shapes (precedent:
  `SpeedingInterval.Id`, `Trip.Id`). **Breaking**: `Latitude`/`Longitude`/`Time` are
  now nullable.
- `heading`, `speed`, `formattedAddress`, `reverseGeo` were already nullable and were
  RETAINED as-is (`reverseGeo` keeps its `ReverseGeo?` type and XML doc). The
  `ReverseGeo` record was not touched.

**Caller updated**

- CLI (`tools/Samsara.Cli/TuiApp.cs`): the `List Locations` vehicle action now passes
  the cancellation token by name (`cancellationToken:` — the 1st positional slot is now
  the `vehicleIds` filter), drops the now-redundant `?? ""` on `l.Name`, and renders
  the now-nullable `Latitude`/`Longitude` via `?.ToString() ?? ""` (the BCL annotates
  `Nullable<double>.ToString()` as returning `string?`, so a bare `.ToString()` would
  not satisfy the `Func<T, string[]>` selector under nullable reference types — the
  plan's note that bare `.ToString()` compiles cleanly was incorrect). The
  `Equipment.ListLocationsAsync` call is a different client and was left untouched.

Files touched: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`,
`src/Samsara.Sdk/Clients/Fleet/IVehiclesClient.cs`,
`src/Samsara.Sdk/Clients/Fleet/VehiclesClient.cs`,
`tools/Samsara.Cli/TuiApp.cs`. No `SamsaraJsonContext` changes
(`VehicleLocation`/`ReverseGeo` already registered; new props weakly-typed
`object`/array → no new top-level types). No test changes (no
`new VehicleLocation(...)` construction). Other Vehicles methods untouched.

Verification: `dotnet build` 0 errors / 0 warnings, `dotnet test` 59 passed, and
`check-sdk-sync.py --fail-on-mismatch` exits 0 (323/323 matched).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `VehicleLocation` | response | 0 | 2 | 1 | 7 |
| `(no SDK type)` | query | 0 | 0 | 10 | 0 |

**Counts**: CRITICAL=0, HIGH=2, MEDIUM=11, LOW=7  
**Total deduped findings**: 20

## HIGH (2)

### `VehicleLocation` (response)

- **[response_drift_required]** VehicleLocation (response) missing REQUIRED property `location` (spec type=object).
  - Endpoints: `GET /fleet/vehicles/locations`
  - Recommended fix: Add `[JsonPropertyName("location")] public object Location { get; init; }` to response record `VehicleLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** VehicleLocation (response) missing REQUIRED property `locations` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Add `[JsonPropertyName("locations")] public IReadOnlyList<object> Locations { get; init; }` to response record `VehicleLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (11)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListLocationsAsync (GET /fleet/vehicles/locations) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsFeedAsync (GET /fleet/vehicles/locations/feed) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsHistoryAsync (GET /fleet/vehicles/locations/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /fleet/vehicles/locations) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsFeedAsync (GET /fleet/vehicles/locations/feed) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsHistoryAsync (GET /fleet/vehicles/locations/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /fleet/vehicles/locations) is missing query parameter `time` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles/locations`
  - Recommended fix: Add an optional parameter `string? time = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /fleet/vehicles/locations) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsFeedAsync (GET /fleet/vehicles/locations/feed) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationsHistoryAsync (GET /fleet/vehicles/locations/history) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/locations/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `VehicleLocation` (response)

- **[response_required_drift]** VehicleLocation.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations`, `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Tighten `VehicleLocation.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (7)

### `VehicleLocation` (response)

- **[extra_property]** VehicleLocation.formattedAddress (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations`, `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Remove `VehicleLocation.FormattedAddress` (not in spec).
- **[extra_property]** VehicleLocation.heading (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations`, `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Remove `VehicleLocation.Heading` (not in spec).
- **[extra_property]** VehicleLocation.latitude (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations`, `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Remove `VehicleLocation.Latitude` (not in spec).
- **[extra_property]** VehicleLocation.longitude (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations`, `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Remove `VehicleLocation.Longitude` (not in spec).
- **[extra_property]** VehicleLocation.reverseGeo (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations`, `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Remove `VehicleLocation.ReverseGeo` (not in spec).
- **[extra_property]** VehicleLocation.speed (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations`, `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Remove `VehicleLocation.Speed` (not in spec).
- **[extra_property]** VehicleLocation.time (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles/locations`, `GET /fleet/vehicles/locations/feed`, `GET /fleet/vehicles/locations/history`
  - Recommended fix: Remove `VehicleLocation.Time` (not in spec).

