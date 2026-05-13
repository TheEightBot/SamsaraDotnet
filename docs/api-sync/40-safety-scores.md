# Safety Scores — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ❌ Not Started (0/4 endpoints implemented)  
> **SDK Client**: `ISafetyClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../SafetyClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Safety/SafetyModels.cs`  

---

## Endpoints

### ❌ `GET /safety-scores/drivers`
**Operation ID**: `getDriverSafetyScores`  
**Summary**: Get driver scores  
**Parameters**: `endTime`, `startTime`, `driverIds`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /safety-scores/tag-group`
**Operation ID**: `getTagGroupSafetyScores`  
**Summary**: Get tags combined score  
**Parameters**: `endTime`, `startTime`, `scoreType`, `tagIds`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /safety-scores/tags`
**Operation ID**: `getTagSafetyScores`  
**Summary**: Get tag scores  
**Parameters**: `endTime`, `startTime`, `scoreType`, `tagIds`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /safety-scores/vehicles`
**Operation ID**: `getVehicleSafetyScores`  
**Summary**: Get vehicle scores  
**Parameters**: `endTime`, `startTime`, `vehicleIds`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

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
