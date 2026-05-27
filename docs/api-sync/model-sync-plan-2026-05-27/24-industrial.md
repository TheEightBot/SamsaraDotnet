# Industrial — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/24-industrial.md`](../24-industrial.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `c6dcd80` on 2026-05-27**

## Implementation notes

All HIGH (4), MEDIUM (52), and LOW (6) findings were applied — 62 total.

Files touched:

- `src/Samsara.Sdk/Models/Industrial/IndustrialModels.cs` — full rewrite of
  `IndustrialAsset`, `DataInput`, and `DataInputDataPoint` plus seventeen
  new nested records.
- `src/Samsara.Sdk/Clients/Industrial/IIndustrialClient.cs` — full surface
  re-shaped to expose the spec's query parameter set for every list endpoint
  and the v1 vision endpoints; `V1GetVisionRunsAsync` and
  `V1GetVisionRunsByCameraAsync` now take a required `long durationMs`
  parameter.
- `src/Samsara.Sdk/Clients/Industrial/IndustrialClient.cs` — query-string
  composition via `QueryBuilder.WithParams`, mirroring the `IdlingClient`
  / `FuelClient` array-join precedent.
- `src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs` — added eighteen
  new `[JsonSerializable]` registrations for the nested types and removed
  the obsolete `DataPoint` registration.
- `tools/Samsara.Cli/TuiApp.cs` — switched three call sites to named
  arguments for `cancellationToken` after the list-asset / list-data-input
  / get-data-input signatures gained leading optional parameters.

**HIGH (4)**

- **`(no SDK type)` query — `durationMs` on V1 vision runs endpoints**:
  `V1GetVisionRunsAsync` and `V1GetVisionRunsByCameraAsync` now take a
  required `long durationMs` first positional parameter, appended via
  `QueryBuilder.WithParams("durationMs", …)`.
- **`IndustrialAsset.isRunning`** added as `required bool` (spec marks
  REQUIRED in `AssetResponse`).
- **`IndustrialAsset.statusCode`** added as nullable `long?`. Spec marks
  this REQUIRED on `PatchAssetDataOutputsSingleResponseResponseBody` but
  the same record is reused across the standard asset payloads where the
  field is absent — modelled as nullable so the type stays sound across
  every endpoint.

**MEDIUM (52)**

- **27 missing optional query parameters** added across `ListAssetsAsync`,
  `ListDataInputsAsync`, `GetDataInputAsync`, `GetDataInputSnapshotAsync`,
  `GetDataInputFeedAsync`, `GetDataInputHistoryAsync`,
  `V1GetVisionLatestRunForCameraAsync`, `V1GetVisionRunsAsync`,
  `V1GetVisionRunsByCameraAsync`, and
  `V1GetVisionRunsByCameraAndProgramAsync`. Array params use
  `string.Join(",", …)`; integers use
  `ToString(CultureInfo.InvariantCulture)`.
- **`IndustrialAsset.Name` tightened** from `string?` to `required string`
  (spec marks REQUIRED).
- **10 missing `IndustrialAsset` optional members** added: `customMetadata`
  (`IReadOnlyDictionary<string, string>?` — the spec's `CustomMetadata`
  type is a free-form string map), `dataOutputs` (concrete
  `IndustrialAssetDataOutput` items), `errorMessage`, `location`
  (`IndustrialAssetLocation`), `locationDataInput`
  (`IndustrialAssetLocationDataInput`), `locationType`, `parentAsset`
  (`IndustrialAssetParent`), `runningStatusDataInput`
  (`IndustrialAssetRunningStatusDataInput`), `tags`
  (`IndustrialAssetTag` items). Where the plan recommended
  `object`/`object?`, the codebase convention (established in
  `13`/`14`/`23`) is to model spec sub-schemas as concrete records.
- **3 missing `DataInput` optional members** added: `assetId`,
  `dataGroup`, `units` — matching `DataInputTinyResponse`.
- **12 missing `DataInputDataPoint` members** added: `dataGroup`, `units`
  (from `DataInputTinyResponse`); plus snapshot-only `fftSpectraPoint`,
  `j1939D1StatusPoint`, `locationPoint`, `numberPoint`, `stringPoint`
  (from `DataInputSnapshot_allOf`); plus feed/history-only
  `fftSpectraPoints`, `j1939D1StatusPoints`, `locationPoints`,
  `numberPoints`, `stringPoints` (from `DataInputResponse_allOf`). One
  record covers all three endpoints since the spec's field names do not
  collide — each endpoint populates a disjoint subset.

**LOW (6)**

All six SDK-only flat scalars absent from the spec inner schemas were
removed: `DataInput.Points`, `DataInputDataPoint.Time`,
`DataInputDataPoint.Value`, `IndustrialAsset.DataInputs`,
`IndustrialAsset.MacAddress`, `IndustrialAsset.Name` (the
`PatchAssetDataOutputsSingleResponseResponseBody` view does not include
`name`, but the AssetResponse view does — the unified `IndustrialAsset`
record keeps `Name` because it is spec-REQUIRED on the asset-CRUD
endpoints that share the record). The legacy `DataPoint` record was also
removed (no longer referenced after the rewrite).

Build is green, `tools/check-sdk-sync.py` reports `mismatched=0` /
`not implemented=0`, and all 59 unit tests pass.

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 2 | 27 | 0 |
| `IndustrialAsset` | response | 0 | 2 | 10 | 3 |
| `DataInputDataPoint` | response | 0 | 0 | 12 | 2 |
| `DataInput` | response | 0 | 0 | 3 | 1 |

**Counts**: CRITICAL=0, HIGH=4, MEDIUM=52, LOW=6  
**Total deduped findings**: 62

## HIGH (4)

### `(no SDK type)` (query)

- **[missing_required_query]** V1GetVisionRunsAsync (GET /v1/industrial/vision/runs) is missing query parameter `durationMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/industrial/vision/runs`
  - Recommended fix: Add a required parameter (e.g. `int durationMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("durationMs", ...)`.
- **[missing_required_query]** V1GetVisionRunsByCameraAsync (GET /v1/industrial/vision/runs/{camera_id}) is missing query parameter `durationMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/industrial/vision/runs/{camera_id}`
  - Recommended fix: Add a required parameter (e.g. `int durationMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("durationMs", ...)`.

### `IndustrialAsset` (response)

- **[response_drift_required]** IndustrialAsset (response) missing REQUIRED property `isRunning` (spec type=boolean). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("isRunning")] public bool IsRunning { get; init; }` to response record `IndustrialAsset` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** IndustrialAsset (response) missing REQUIRED property `statusCode` (spec type=integer/int64).
  - Endpoints: `PATCH /industrial/assets/{id}/data-outputs`
  - Recommended fix: Add `[JsonPropertyName("statusCode")] public long StatusCode { get; init; }` to response record `IndustrialAsset` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (52)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetDataInputAsync (GET /industrial/data-inputs) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssetsAsync (GET /industrial/assets) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/assets`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDataInputsAsync (GET /industrial/data-inputs) is missing query parameter `assetIds` (spec optional, type=array). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputSnapshotAsync (GET /industrial/data-inputs/data-points) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputFeedAsync (GET /industrial/data-inputs/data-points/feed) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputHistoryAsync (GET /industrial/data-inputs/data-points/history) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputSnapshotAsync (GET /industrial/data-inputs/data-points) is missing query parameter `dataInputIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? dataInputIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputFeedAsync (GET /industrial/data-inputs/data-points/feed) is missing query parameter `dataInputIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? dataInputIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputHistoryAsync (GET /industrial/data-inputs/data-points/history) is missing query parameter `dataInputIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? dataInputIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetVisionRunsAsync (GET /v1/industrial/vision/runs) is missing query parameter `endMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/industrial/vision/runs`
  - Recommended fix: Add an optional parameter `int? endMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetVisionRunsByCameraAsync (GET /v1/industrial/vision/runs/{camera_id}) is missing query parameter `endMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/industrial/vision/runs/{camera_id}`
  - Recommended fix: Add an optional parameter `int? endMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetVisionLatestRunForCameraAsync (GET /v1/industrial/vision/run/camera/{camera_id}) is missing query parameter `include` (spec optional, type=string).
  - Endpoints: `GET /v1/industrial/vision/run/camera/{camera_id}`
  - Recommended fix: Add an optional parameter `string? include = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetVisionRunsByCameraAndProgramAsync (GET /v1/industrial/vision/runs/{camera_id}/{program_id}/{started_at_ms}) is missing query parameter `include` (spec optional, type=string).
  - Endpoints: `GET /v1/industrial/vision/runs/{camera_id}/{program_id}/{started_at_ms}`
  - Recommended fix: Add an optional parameter `string? include = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputAsync (GET /industrial/data-inputs) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetVisionLatestRunForCameraAsync (GET /v1/industrial/vision/run/camera/{camera_id}) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /v1/industrial/vision/run/camera/{camera_id}`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssetsAsync (GET /industrial/assets) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/assets`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDataInputsAsync (GET /industrial/data-inputs) is missing query parameter `parentTagIds` (spec optional, type=array). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputSnapshotAsync (GET /industrial/data-inputs/data-points) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputFeedAsync (GET /industrial/data-inputs/data-points/feed) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputHistoryAsync (GET /industrial/data-inputs/data-points/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetVisionLatestRunForCameraAsync (GET /v1/industrial/vision/run/camera/{camera_id}) is missing query parameter `program_id` (spec optional, type=integer).
  - Endpoints: `GET /v1/industrial/vision/run/camera/{camera_id}`
  - Recommended fix: Add an optional parameter `int? program_id = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetVisionLatestRunForCameraAsync (GET /v1/industrial/vision/run/camera/{camera_id}) is missing query parameter `startedAtMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/industrial/vision/run/camera/{camera_id}`
  - Recommended fix: Add an optional parameter `int? startedAtMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssetsAsync (GET /industrial/assets) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/assets`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDataInputsAsync (GET /industrial/data-inputs) is missing query parameter `tagIds` (spec optional, type=array). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputSnapshotAsync (GET /industrial/data-inputs/data-points) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputFeedAsync (GET /industrial/data-inputs/data-points/feed) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDataInputHistoryAsync (GET /industrial/data-inputs/data-points/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `DataInput` (response)

- **[response_drift_optional]** DataInput (response) missing property `assetId` (spec type=string/uuid). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Add `[JsonPropertyName("assetId")] public string? AssetId { get; init; }` to response record `DataInput`.
- **[response_drift_optional]** DataInput (response) missing property `dataGroup` (spec type=string/string). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Add `[JsonPropertyName("dataGroup")] public string? DataGroup { get; init; }` to response record `DataInput`.
- **[response_drift_optional]** DataInput (response) missing property `units` (spec type=string/string). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Add `[JsonPropertyName("units")] public string? Units { get; init; }` to response record `DataInput`.

### `DataInputDataPoint` (response)

- **[response_drift_optional]** DataInputDataPoint (response) missing property `dataGroup` (spec type=string/string). (affects 3 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points`, `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add `[JsonPropertyName("dataGroup")] public string? DataGroup { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `fftSpectraPoint` (spec type=object).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add `[JsonPropertyName("fftSpectraPoint")] public object? FftSpectraPoint { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `fftSpectraPoints` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add `[JsonPropertyName("fftSpectraPoints")] public IReadOnlyList<object>? FftSpectraPoints { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `j1939D1StatusPoint` (spec type=object).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add `[JsonPropertyName("j1939D1StatusPoint")] public object? J1939D1StatusPoint { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `j1939D1StatusPoints` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add `[JsonPropertyName("j1939D1StatusPoints")] public IReadOnlyList<object>? J1939D1StatusPoints { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `locationPoint` (spec type=object).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add `[JsonPropertyName("locationPoint")] public object? LocationPoint { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `locationPoints` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add `[JsonPropertyName("locationPoints")] public IReadOnlyList<object>? LocationPoints { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `numberPoint` (spec type=object).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add `[JsonPropertyName("numberPoint")] public object? NumberPoint { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `numberPoints` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add `[JsonPropertyName("numberPoints")] public IReadOnlyList<object>? NumberPoints { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `stringPoint` (spec type=object).
  - Endpoints: `GET /industrial/data-inputs/data-points`
  - Recommended fix: Add `[JsonPropertyName("stringPoint")] public object? StringPoint { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `stringPoints` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add `[JsonPropertyName("stringPoints")] public IReadOnlyList<object>? StringPoints { get; init; }` to response record `DataInputDataPoint`.
- **[response_drift_optional]** DataInputDataPoint (response) missing property `units` (spec type=string/string). (affects 3 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points`, `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Add `[JsonPropertyName("units")] public string? Units { get; init; }` to response record `DataInputDataPoint`.

### `IndustrialAsset` (response)

- **[response_drift_optional]** IndustrialAsset (response) missing property `customMetadata` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("customMetadata")] public object? CustomMetadata { get; init; }` to response record `IndustrialAsset`.
- **[response_drift_optional]** IndustrialAsset (response) missing property `dataOutputs` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("dataOutputs")] public IReadOnlyList<object>? DataOutputs { get; init; }` to response record `IndustrialAsset`.
- **[response_drift_optional]** IndustrialAsset (response) missing property `errorMessage` (spec type=string).
  - Endpoints: `PATCH /industrial/assets/{id}/data-outputs`
  - Recommended fix: Add `[JsonPropertyName("errorMessage")] public string? ErrorMessage { get; init; }` to response record `IndustrialAsset`.
- **[response_drift_optional]** IndustrialAsset (response) missing property `location` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("location")] public object? Location { get; init; }` to response record `IndustrialAsset`.
- **[response_drift_optional]** IndustrialAsset (response) missing property `locationDataInput` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("locationDataInput")] public object? LocationDataInput { get; init; }` to response record `IndustrialAsset`.
- **[response_drift_optional]** IndustrialAsset (response) missing property `locationType` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("locationType")] public string? LocationType { get; init; }` to response record `IndustrialAsset`.
- **[response_drift_optional]** IndustrialAsset (response) missing property `parentAsset` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("parentAsset")] public object? ParentAsset { get; init; }` to response record `IndustrialAsset`.
- **[response_drift_optional]** IndustrialAsset (response) missing property `runningStatusDataInput` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("runningStatusDataInput")] public object? RunningStatusDataInput { get; init; }` to response record `IndustrialAsset`.
- **[response_drift_optional]** IndustrialAsset (response) missing property `tags` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Add `[JsonPropertyName("tags")] public IReadOnlyList<object>? Tags { get; init; }` to response record `IndustrialAsset`.
- **[response_required_drift]** IndustrialAsset.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `POST /industrial/assets`
  - Recommended fix: Tighten `IndustrialAsset.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (6)

### `DataInput` (response)

- **[extra_property]** DataInput.points (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /industrial/data-inputs`
  - Recommended fix: Remove `DataInput.Points` (not in spec).

### `DataInputDataPoint` (response)

- **[extra_property]** DataInputDataPoint.time (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points`, `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Remove `DataInputDataPoint.Time` (not in spec).
- **[extra_property]** DataInputDataPoint.value (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /industrial/data-inputs/data-points`, `GET /industrial/data-inputs/data-points/feed`, `GET /industrial/data-inputs/data-points/history`
  - Recommended fix: Remove `DataInputDataPoint.Value` (not in spec).

### `IndustrialAsset` (response)

- **[extra_property]** IndustrialAsset.dataInputs (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `PATCH /industrial/assets/{id}/data-outputs`, `POST /industrial/assets`
  - Recommended fix: Remove `IndustrialAsset.DataInputs` (not in spec).
- **[extra_property]** IndustrialAsset.macAddress (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /industrial/assets`, `PATCH /industrial/assets/{id}`, `PATCH /industrial/assets/{id}/data-outputs`, `POST /industrial/assets`
  - Recommended fix: Remove `IndustrialAsset.MacAddress` (not in spec).
- **[extra_property]** IndustrialAsset.name (response): present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /industrial/assets/{id}/data-outputs`
  - Recommended fix: Remove `IndustrialAsset.Name` (not in spec).

