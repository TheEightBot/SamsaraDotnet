# Safety — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ❌ Not Started (0/4 endpoints implemented)  
> **SDK Client**: `ISafetyClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../SafetyClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Safety/SafetyModels.cs`  

---

## Endpoints

### ❌ `GET /safety-events`
**Operation ID**: `getSafetyEventsV2`  
**Summary**: Get Safety Events  
**Parameters**: `safetyEventIds`, `includeAsset`, `includeDriver`, `includeVgOnlyEvents`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /safety-events/stream`
**Operation ID**: `getSafetyEventsV2Stream`  
**Summary**: Get Safety Events Stream  
**Parameters**: `startTime`, `endTime`, `queryByTimeField`, `assetIds`, `driverIds`, `tagIds`, `assignedCoaches`, `behaviorLabels`, `eventStates`, `includeAsset`, `includeDriver`, `includeVgOnlyEvents`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /v1/fleet/drivers/{driverId}/safety/score`
**Operation ID**: `V1getDriverSafetyScore`  
**Summary**: Fetch driver safety score  
**Parameters**: `driverId`, `startMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `ISafetyClient`
- [ ] Method implemented in `SafetyClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /v1/fleet/vehicles/{vehicleId}/safety/score`
**Operation ID**: `V1getVehicleSafetyScore`  
**Summary**: Fetch vehicle safety scores  
**Parameters**: `vehicleId`, `startMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `ISafetyClient`
- [ ] Method implemented in `SafetyClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Safety/SafetyModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
