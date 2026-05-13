# Maintenance — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (2/9 endpoints implemented)  
> **SDK Client**: `IMaintenanceClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../MaintenanceClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Maintenance/MaintenanceModels.cs`  

---

## Endpoints

### ❌ `GET /defect-types`
**Operation ID**: `getDefectTypes`  
**Summary**: Get DVIR defect types.  
**Parameters**: `after`, `limit`, `ids`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /defects/stream`
**Operation ID**: `streamDefects`  
**Summary**: Stream DVIR defects.  
**Parameters**: `after`, `limit`, `startTime`, `endTime`, `includeExternalIds`, `isResolved`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /defects/{id}`
**Operation ID**: `getDefect`  
**Summary**: Get a single DVIR defect by ID.  
**Parameters**: `id`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /dvirs/stream`
**Operation ID**: `getDvirs`  
**Summary**: Stream DVIRs  
**Parameters**: `after`, `limit`, `includeExternalIds`, `startTime`, `endTime`, `safetyStatus`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /dvirs/{id}`
**Operation ID**: `getDvir`  
**Summary**: Get a single DVIR by ID.  
**Parameters**: `id`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `PATCH /fleet/defects/{id}`
**Operation ID**: `updateDvirDefect`  
**Summary**: Update a defect  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `POST /fleet/dvirs`
**Operation ID**: `createDvir`  
**Summary**: Create a mechanic DVIR  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `PATCH /fleet/dvirs/{id}`
**Operation ID**: `updateDvir`  
**Summary**: Resolve a DVIR  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /v1/fleet/maintenance/list`
**Operation ID**: `V1getFleetMaintenanceList`  
**Summary**: Get vehicles with engine faults or check lights  
**Request Body**: No  

- [ ] Method defined in `IMaintenanceClient`
- [ ] Method implemented in `MaintenanceClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Maintenance/MaintenanceModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
