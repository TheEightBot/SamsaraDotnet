# Industrial — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (5/17 endpoints implemented)  
> **SDK Client**: `IIndustrialClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../IndustrialClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Industrial/IndustrialModels.cs`  

---

## Endpoints

### ✅ `GET /industrial/assets`
**Operation ID**: `getIndustrialAssets`  
**Summary**: List all assets  
**Parameters**: `limit`, `after`, `parentTagIds`, `tagIds`, `assetIds`  
**Request Body**: No  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /industrial/assets`
**Operation ID**: `createIndustrialAsset`  
**Summary**: Create an asset  
**Request Body**: Yes  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /industrial/assets/{id}`
**Operation ID**: `deleteIndustrialAsset`  
**Summary**: Delete an existing asset  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /industrial/assets/{id}`
**Operation ID**: `patchIndustrialAsset`  
**Summary**: Update an asset  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /industrial/assets/{id}/data-outputs`
**Operation ID**: `patchAssetDataOutputs`  
**Summary**: Writes to data outputs on an asset  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /industrial/data-inputs`
**Operation ID**: `getDataInputs`  
**Summary**: List all data inputs  
**Parameters**: `limit`, `after`, `parentTagIds`, `tagIds`, `assetIds`  
**Request Body**: No  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /industrial/data-inputs/data-points`
**Operation ID**: `getDataInputDataSnapshot`  
**Summary**: List most recent data points for data inputs  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `dataInputIds`, `assetIds`  
**Request Body**: No  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /industrial/data-inputs/data-points/feed`
**Operation ID**: `getDataInputDataFeed`  
**Summary**: Follow a real-time feed of data points for data inputs  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `dataInputIds`, `assetIds`  
**Request Body**: No  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /industrial/data-inputs/data-points/history`
**Operation ID**: `getDataInputDataHistory`  
**Summary**: List historical data points for data inputs  
**Parameters**: `startTime`, `endTime`, `after`, `parentTagIds`, `tagIds`, `dataInputIds`, `assetIds`  
**Request Body**: No  

- [x] Method defined in `IIndustrialClient`
- [x] Method implemented in `IndustrialClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /v1/industrial/vision/cameras`
**Operation ID**: `V1getCameras`  
**Summary**: Fetch industrial cameras  
**Request Body**: No  

- [ ] Method defined in `IIndustrialClient`
- [ ] Method implemented in `IndustrialClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/industrial/vision/cameras/{camera_id}/programs`
**Operation ID**: `V1getVisionProgramsByCamera`  
**Summary**: Fetch industrial camera programs  
**Parameters**: `camera_id`  
**Request Body**: No  

- [ ] Method defined in `IIndustrialClient`
- [ ] Method implemented in `IndustrialClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/industrial/vision/run/camera/{camera_id}`
**Operation ID**: `V1getVisionLatestRunCamera`  
**Summary**: Fetch the latest run for a camera or program  
**Parameters**: `camera_id`, `program_id`, `startedAtMs`, `include`, `limit`  
**Request Body**: No  

- [ ] Method defined in `IIndustrialClient`
- [ ] Method implemented in `IndustrialClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/industrial/vision/runs`
**Operation ID**: `V1getVisionRuns`  
**Summary**: Fetch runs  
**Parameters**: `durationMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `IIndustrialClient`
- [ ] Method implemented in `IndustrialClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/industrial/vision/runs/{camera_id}`
**Operation ID**: `getVisionRunsByCamera`  
**Summary**: Fetch runs by camera  
**Parameters**: `camera_id`, `durationMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `IIndustrialClient`
- [ ] Method implemented in `IndustrialClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/industrial/vision/runs/{camera_id}/{program_id}/{started_at_ms}`
**Operation ID**: `V1getVisionRunsByCameraAndProgram`  
**Summary**: Fetch runs by camera and program  
**Parameters**: `camera_id`, `program_id`, `started_at_ms`, `include`  
**Request Body**: No  

- [ ] Method defined in `IIndustrialClient`
- [ ] Method implemented in `IndustrialClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /v1/machines/history`
**Operation ID**: `V1getMachinesHistory`  
**Summary**: Get machine history  
**Request Body**: Yes  

- [ ] Method defined in `IIndustrialClient`
- [ ] Method implemented in `IndustrialClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /v1/machines/list`
**Operation ID**: `V1getMachines`  
**Summary**: Get machines  
**Request Body**: No  

- [ ] Method defined in `IIndustrialClient`
- [ ] Method implemented in `IndustrialClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Industrial/IndustrialModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
