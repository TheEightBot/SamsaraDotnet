# Location and Speed — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/29-location-and-speed.md`](../29-location-and-speed.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `bbfca63` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. LOW findings on the response were
intentionally retained as nullable back-compat properties per the workflow
precedent established in `08-carrier-proposed-assignments`,
`13-driver-trailer-assignments`, `14-driver-vehicle-assignments`, and
`28-live-sharing-links` — response-side flat-scalar conveniences kept with
XML doc pointers to the canonical spec fields rather than removed outright.

Files touched: `src/Samsara.Sdk/Models/Fleet/AssetModels.cs`,
`src/Samsara.Sdk/Clients/Fleet/IAssetsClient.cs`,
`src/Samsara.Sdk/Clients/Fleet/AssetsClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`.

**HIGH (2)**

- **`AssetLocationAndSpeed` response — required `asset`**: added as
  `required AssetLocationAndSpeedAsset Asset` with a new nested record
  mirroring the spec's `AssetResponseResponseBody` (required `id`, optional
  `externalIds`). Chose a typed nested record over the plan's literal
  `object` recommendation to match the precedent in
  `14-driver-vehicle-assignments`, where `driver` / `vehicle` were similarly
  surfaced as typed nested records rather than bare `object`.
- **`AssetLocationAndSpeed` response — required `happenedAtTime`**: added as
  `required DateTimeOffset HappenedAtTime`. Spec describes the field as an
  RFC 3339 string; `DateTimeOffset` is the established SDK convention for
  RFC 3339 timestamps (e.g. `Asset.CreatedAtTime`, `Asset.UpdatedAtTime`).

**MEDIUM (10)**

- **`(no SDK type)` query — optional `startTime` / `endTime`**: added as
  `DateTimeOffset?` parameters routed through the shared
  `QueryBuilder.WithTimeRange` helper (RFC 3339 round-trip format), matching
  the precedent used by every other paginated stream endpoint in the SDK
  (`assets/depreciation`, `assets/inputs/stream`, `qualification-records/stream`,
  `detections/stream`, etc.).
- **`(no SDK type)` query — optional `ids`**: added as
  `IReadOnlyList<string>? ids = null` and joined with `,` per the
  `AssetsClient.ListAsync` precedent.
- **`(no SDK type)` query — optional `includeSpeed`, `includeReverseGeo`,
  `includeGeofenceLookup`, `includeHighFrequencyLocations`,
  `includeExternalIds`**: added as `bool?` parameters serialized via
  `?.ToString().ToLowerInvariant()` (the established lowercase-boolean
  convention used by `AssetsClient.ListAsync` and
  `AssetsClient.GetInputsStreamAsync`).
- **`AssetLocationAndSpeed.location` (response)**: tightened from
  `AssetLocation?` to `required AssetLocation` per the plan's "drop the `?`"
  instruction. Spec marks this field REQUIRED on the response inner schema.
- **`AssetLocationAndSpeed.speed` (response)**: changed from `double?` to
  the new typed nested record `AssetLocationAndSpeedSpeed?` (mirrors
  `SpeedResponseResponseBody` — optional `ecuSpeedMetersPerSecond` /
  `gpsSpeedMetersPerSecond`). The plan's literal recommendation was
  `object`; a typed nested record is a strictly-better superset and matches
  the precedent in `14-driver-vehicle-assignments`. The field stays nullable
  on the response because the spec only emits the `speed` sub-object when
  `includeSpeed=true` is passed on the request.

**LOW (3)**

- **`AssetLocationAndSpeed.id`, `AssetLocationAndSpeed.name`,
  `AssetLocationAndSpeed.time` (response)**: kept as nullable back-compat
  properties with XML doc comments noting they are not in the spec inner
  schema and pointing callers to the canonical spec fields
  (`Asset.Id` / a separate asset lookup for `name` / `HappenedAtTime`).
  Same approach as the response-side flat-scalar conveniences in
  `08-carrier-proposed-assignments`, `13-driver-trailer-assignments`,
  `14-driver-vehicle-assignments`, and `28-live-sharing-links`.

Verification: `dotnet build` green (0 warnings, 0 errors); all 59 unit
tests pass; `python3 tools/check-sdk-sync.py` exits 0 (matched=323/323,
mismatched=0, unresolved=0, not implemented=0).

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

