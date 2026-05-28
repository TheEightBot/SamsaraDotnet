# Assets — Model Sync Plan (2026-05-27)

> **✅ Implemented in commit `30a26e6` on 2026-05-27**  
> Companion to [`docs/api-sync/03-assets.md`](../03-assets.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 8 | 17 | 0 |
| `Asset` | response | 0 | 0 | 3 | 0 |
| `CreateAssetRequest` | request | 0 | 0 | 0 | 1 |

**Counts**: CRITICAL=0, HIGH=8, MEDIUM=20, LOW=1  
**Total deduped findings**: 29

## HIGH (8)

### `(no SDK type)` (query)

- **[missing_required_query]** V1GetAssetsReefersAsync (GET /v1/fleet/assets/reefers) is missing query parameter `endMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/assets/reefers`
  - Recommended fix: Add a required parameter (e.g. `int endMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endMs", ...)`.
- **[missing_required_query]** V1GetAssetLocationAsync (GET /v1/fleet/assets/{asset_id}/locations) is missing query parameter `endMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/assets/{asset_id}/locations`
  - Recommended fix: Add a required parameter (e.g. `int endMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endMs", ...)`.
- **[missing_required_query]** V1GetAssetReeferAsync (GET /v1/fleet/assets/{asset_id}/reefer) is missing query parameter `endMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/assets/{asset_id}/reefer`
  - Recommended fix: Add a required parameter (e.g. `int endMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endMs", ...)`.
- **[missing_required_query]** UpdateAsync (PATCH /assets) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `PATCH /assets`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.
- **[missing_required_query]** DeleteAsync (DELETE /assets) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `DELETE /assets`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.
- **[missing_required_query]** V1GetAssetsReefersAsync (GET /v1/fleet/assets/reefers) is missing query parameter `startMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/assets/reefers`
  - Recommended fix: Add a required parameter (e.g. `int startMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startMs", ...)`.
- **[missing_required_query]** V1GetAssetLocationAsync (GET /v1/fleet/assets/{asset_id}/locations) is missing query parameter `startMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/assets/{asset_id}/locations`
  - Recommended fix: Add a required parameter (e.g. `int startMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startMs", ...)`.
- **[missing_required_query]** V1GetAssetReeferAsync (GET /v1/fleet/assets/{asset_id}/reefer) is missing query parameter `startMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/assets/{asset_id}/reefer`
  - Recommended fix: Add a required parameter (e.g. `int startMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startMs", ...)`.

## MEDIUM (20)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `attributeValueIds` (spec optional, type=string).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `string? attributeValueIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `attributes` (spec optional, type=array).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? attributes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetAllAssetCurrentLocationsAsync (GET /v1/fleet/assets/locations) is missing query parameter `endingBefore` (spec optional, type=string).
  - Endpoints: `GET /v1/fleet/assets/locations`
  - Recommended fix: Add an optional parameter `string? endingBefore = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetAssetsReefersAsync (GET /v1/fleet/assets/reefers) is missing query parameter `endingBefore` (spec optional, type=string).
  - Endpoints: `GET /v1/fleet/assets/reefers`
  - Recommended fix: Add an optional parameter `string? endingBefore = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `externalIds` (spec optional, type=array).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? externalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `includeAttributes` (spec optional, type=boolean).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `bool? includeAttributes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `includeTags` (spec optional, type=boolean).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `bool? includeTags = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetAllAssetCurrentLocationsAsync (GET /v1/fleet/assets/locations) is missing query parameter `limit` (spec optional, type=number).
  - Endpoints: `GET /v1/fleet/assets/locations`
  - Recommended fix: Add an optional parameter `double? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetAssetsReefersAsync (GET /v1/fleet/assets/reefers) is missing query parameter `limit` (spec optional, type=number).
  - Endpoints: `GET /v1/fleet/assets/reefers`
  - Recommended fix: Add an optional parameter `double? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetAllAssetCurrentLocationsAsync (GET /v1/fleet/assets/locations) is missing query parameter `startingAfter` (spec optional, type=string).
  - Endpoints: `GET /v1/fleet/assets/locations`
  - Recommended fix: Add an optional parameter `string? startingAfter = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** V1GetAssetsReefersAsync (GET /v1/fleet/assets/reefers) is missing query parameter `startingAfter` (spec optional, type=string).
  - Endpoints: `GET /v1/fleet/assets/reefers`
  - Recommended fix: Add an optional parameter `string? startingAfter = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `type` (spec optional, type=string).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `string? type = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /assets) is missing query parameter `updatedAfterTime` (spec optional, type=string).
  - Endpoints: `GET /assets`
  - Recommended fix: Add an optional parameter `string? updatedAfterTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `Asset` (response)

- **[response_drift_optional]** Asset (response) missing property `attributes` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /assets`, `PATCH /assets`, `POST /assets`
  - Recommended fix: Add `[JsonPropertyName("attributes")] public IReadOnlyList<object>? Attributes { get; init; }` to response record `Asset`.
- **[response_required_drift]** Asset.createdAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /assets`, `PATCH /assets`, `POST /assets`
  - Recommended fix: Tighten `Asset.CreatedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Asset.updatedAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /assets`, `PATCH /assets`, `POST /assets`
  - Recommended fix: Tighten `Asset.UpdatedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (1)

### `CreateAssetRequest` (request)

- **[required_drift_over]** CreateAssetRequest.name: SDK marks `required` but spec is optional.
  - Endpoints: `POST /assets`
  - Recommended fix: Drop `required` on `CreateAssetRequest.Name` (spec marks it optional) — make nullable.

## Implementation notes

All 29 findings landed in `src/Samsara.Sdk/Models/Fleet/AssetModels.cs`,
`src/Samsara.Sdk/Clients/Fleet/IAssetsClient.cs`, and
`src/Samsara.Sdk/Clients/Fleet/AssetsClient.cs`. Highlights:

- **`Asset.attributes`**: typed as `IReadOnlyList<JsonElement>?` to match the
  existing `CreateAssetRequest.Attributes` shape rather than the
  `IReadOnlyList<object>?` suggested in the recommendation. Spec inner type is
  `GoaAttributeTinyResponseBody` (free-form `dateValues`/`stringValues`/
  `numberValues`); preserving the raw JSON avoids cascading a typed model that
  is not used elsewhere in the SDK.
- **`Asset.createdAtTime` / `updatedAtTime`**: tightened to non-nullable
  `DateTimeOffset` (dropped `?`). Source-compatible — there are no SDK call
  sites that construct `Asset` directly; deserialization populates the fields
  for every spec-conformant response.
- **`CreateAssetRequest.Name`**: dropped `required` and made nullable
  (`string?`). The 2025-05-13 model audit had added `required` on this field
  even though the spec marks it optional. No callers in tests or CLI rely on
  it.
- **`IAssetsClient.ListAsync(...)`**: signature change adds 11 optional query
  parameters (defaulted to `null`), so existing call sites (none in this repo
  beyond the facade test) continue to compile.
- **`UpdateAsync` and `DeleteAsync`**: now require a `string id` parameter
  (passed via `QueryBuilder.WithParams`). The previous `DeleteAsync` signature
  accepted `string[] ids` and hand-built an `ids[]` query string — this never
  matched the spec, which only documents a singular `id` query parameter.
  This is a binary-breaking change at the SDK surface, but there are no
  callers in tests or the CLI.
- **`V1GetAssetLocationAsync` / `V1GetAssetReeferAsync` / `V1GetAssetsReefersAsync`**:
  added the spec-required `startMs` and `endMs` (typed as `long` since the
  spec calls them integers and they are Unix epoch milliseconds). Plus the
  three optional pagination parameters (`startingAfter`, `endingBefore`,
  `limit`) on `V1GetAssetsReefersAsync` and `V1GetAllAssetCurrentLocationsAsync`.

No findings were skipped or downgraded.

