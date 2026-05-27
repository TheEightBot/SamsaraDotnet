# Vehicle Locations — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/52-vehicle-locations.md`](../52-vehicle-locations.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

