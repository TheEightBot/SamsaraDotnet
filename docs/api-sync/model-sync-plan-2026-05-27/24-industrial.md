# Industrial — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/24-industrial.md`](../24-industrial.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

