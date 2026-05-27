# Location and Speed — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/29-location-and-speed.md`](../29-location-and-speed.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `AssetLocationAndSpeed` | response | 0 | 2 | 2 | 3 |
| `(no SDK type)` | query | 0 | 0 | 8 | 0 |

**Counts**: CRITICAL=0, HIGH=2, MEDIUM=10, LOW=3  
**Total deduped findings**: 15

## HIGH (2)

### `AssetLocationAndSpeed` (response)

- **[response_drift_required]** AssetLocationAndSpeed (response) missing REQUIRED property `asset` (spec type=object).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add `[JsonPropertyName("asset")] public object Asset { get; init; }` to response record `AssetLocationAndSpeed` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AssetLocationAndSpeed (response) missing REQUIRED property `happenedAtTime` (spec type=string).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add `[JsonPropertyName("happenedAtTime")] public string HappenedAtTime { get; init; }` to response record `AssetLocationAndSpeed` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (10)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetLocationAndSpeedStreamAsync (GET /assets/location-and-speed/stream) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationAndSpeedStreamAsync (GET /assets/location-and-speed/stream) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationAndSpeedStreamAsync (GET /assets/location-and-speed/stream) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationAndSpeedStreamAsync (GET /assets/location-and-speed/stream) is missing query parameter `includeGeofenceLookup` (spec optional, type=boolean).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add an optional parameter `bool? includeGeofenceLookup = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationAndSpeedStreamAsync (GET /assets/location-and-speed/stream) is missing query parameter `includeHighFrequencyLocations` (spec optional, type=boolean).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add an optional parameter `bool? includeHighFrequencyLocations = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationAndSpeedStreamAsync (GET /assets/location-and-speed/stream) is missing query parameter `includeReverseGeo` (spec optional, type=boolean).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add an optional parameter `bool? includeReverseGeo = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationAndSpeedStreamAsync (GET /assets/location-and-speed/stream) is missing query parameter `includeSpeed` (spec optional, type=boolean).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add an optional parameter `bool? includeSpeed = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLocationAndSpeedStreamAsync (GET /assets/location-and-speed/stream) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `AssetLocationAndSpeed` (response)

- **[response_required_drift]** AssetLocationAndSpeed.location (response): spec marks REQUIRED but SDK exposes as nullable (`AssetLocation?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Tighten `AssetLocationAndSpeed.Location` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[type_mismatch]** AssetLocationAndSpeed.speed (response): SDK `double?` vs spec `object`.
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Change `AssetLocationAndSpeed.Speed` from `double?` to `object`.

## LOW (3)

### `AssetLocationAndSpeed` (response)

- **[extra_property]** AssetLocationAndSpeed.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Remove `AssetLocationAndSpeed.Id` (not in spec).
- **[extra_property]** AssetLocationAndSpeed.name (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Remove `AssetLocationAndSpeed.Name` (not in spec).
- **[extra_property]** AssetLocationAndSpeed.time (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /assets/location-and-speed/stream`
  - Recommended fix: Remove `AssetLocationAndSpeed.Time` (not in spec).

