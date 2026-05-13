# Assets — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/8 endpoints implemented)  
> **SDK Client**: `IAssetsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../AssetsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Assets/AssetModels.cs`  

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

- [ ] Method defined in `IAssetsClient`
- [ ] Method implemented in `AssetsClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/fleet/assets/reefers`
**Operation ID**: `V1getAssetsReefers`  
**Summary**: List stats for all reefers  
**Parameters**: `startMs`, `endMs`, `startingAfter`, `endingBefore`, `limit`  
**Request Body**: No  

- [ ] Method defined in `IAssetsClient`
- [ ] Method implemented in `AssetsClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/fleet/assets/{asset_id}/locations`
**Operation ID**: `V1getAssetLocation`  
**Summary**: List historical locations for a given asset  
**Parameters**: `asset_id`, `startMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `IAssetsClient`
- [ ] Method implemented in `AssetsClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/fleet/assets/{asset_id}/reefer`
**Operation ID**: `V1getAssetReefer`  
**Summary**: List stats for a given reefer  
**Parameters**: `asset_id`, `startMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `IAssetsClient`
- [ ] Method implemented in `AssetsClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Assets/AssetModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
