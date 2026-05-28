# Assets — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: Resolved 2026-05-27 (model-sync plan)  
> **⚠️ 2026-05-21 audit**: missing v1 asset location/reefer ops; `GET /assets` gained an optional `includeAttributes` param. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `IAssetsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/Fleet/AssetsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/AssetModels.cs`  

---

## Endpoints

### ⚠️ `DELETE /assets`
**Operation ID**: `deleteAsset`  
**Summary**: Delete an existing asset.  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /assets`
**Operation ID**: `listAssets`  
**Summary**: List all assets.  
**Parameters**: `type`, `after`, `updatedAfterTime`, `includeExternalIds`, `includeTags`, `tagIds`, `parentTagIds`, `ids`, `externalIds`, `attributeValueIds`, `attributes`  
**Request Body**: No  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `PATCH /assets`
**Operation ID**: `updateAsset`  
**Summary**: Update an existing asset.  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `POST /assets`
**Operation ID**: `createAsset`  
**Summary**: Create a new asset.  
**Request Body**: Yes  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /v1/fleet/assets/locations`
**Operation ID**: `V1getAllAssetCurrentLocations`  
**Summary**: List current location for all assets  
**Parameters**: `startingAfter`, `endingBefore`, `limit`  
**Request Body**: No  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined (legacy v1 — returns weakly typed `object`)
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /v1/fleet/assets/reefers`
**Operation ID**: `V1getAssetsReefers`  
**Summary**: List stats for all reefers  
**Parameters**: `startMs`, `endMs`, `startingAfter`, `endingBefore`, `limit`  
**Request Body**: No  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined (legacy v1 — returns weakly typed `object`)
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /v1/fleet/assets/{asset_id}/locations`
**Operation ID**: `V1getAssetLocation`  
**Summary**: List historical locations for a given asset  
**Parameters**: `asset_id`, `startMs`, `endMs`  
**Request Body**: No  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined (legacy v1 — returns weakly typed `object`)
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /v1/fleet/assets/{asset_id}/reefer`
**Operation ID**: `V1getAssetReefer`  
**Summary**: List stats for a given reefer  
**Parameters**: `asset_id`, `startMs`, `endMs`  
**Request Body**: No  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined (legacy v1 — returns weakly typed `object`)
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/AssetModels.cs` for model definitions used by this domain.

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

**Model audit (2025-05-13):** Field name corrections applied.

- `Asset` response: fixed JSON property name `assetType` → `type`; added `vin`, `readingsIngestionEnabled`, `regulationMode`, `createdAtTime`, `updatedAtTime`, `serialNumber`.
- `CreateAssetRequest`: renamed `assetType` → `type`; added `serialNumber`, `vin`, `readingsIngestionEnabled`, `regulationMode`, `attributes`.
- `UpdateAssetRequest`: removed `id` (goes in URL path, not body) and `tagIds` (not in update body); added `type`, `serialNumber`, `vin`, `readingsIngestionEnabled`, `regulationMode`.

**Model-sync plan (2026-05-27):** see [`model-sync-plan-2026-05-27/03-assets.md`](model-sync-plan-2026-05-27/03-assets.md).

- `Asset` response: added `attributes` (`IReadOnlyList<JsonElement>?` — spec inner type `GoaAttributeTinyResponseBody`); tightened `createdAtTime` and `updatedAtTime` to non-nullable (`DateTimeOffset`) — spec marks both REQUIRED.
- `CreateAssetRequest`: dropped `required` from `Name` (spec marks it optional) and made it nullable.
- `IAssetsClient.ListAsync`: exposes all 11 documented optional query params (`type`, `updatedAfterTime`, `includeExternalIds`, `includeTags`, `includeAttributes`, `tagIds`, `parentTagIds`, `ids`, `externalIds`, `attributeValueIds`, `attributes`).
- `IAssetsClient.UpdateAsync(string id, …)` and `DeleteAsync(string id, …)` now thread the spec-required `id` query parameter (previously the SDK shipped both methods with no `id`, and `DeleteAsync` incorrectly accepted a `string[] ids` collection).
- `V1GetAssetsReefersAsync`, `V1GetAssetLocationAsync`, `V1GetAssetReeferAsync` now take required `startMs`/`endMs` (long, Unix epoch ms); `V1GetAllAssetCurrentLocationsAsync` and `V1GetAssetsReefersAsync` also expose the optional `startingAfter`/`endingBefore`/`limit` pagination params.
