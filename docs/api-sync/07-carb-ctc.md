# CARB CTC — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/2 endpoints implemented)  
> **SDK Client**: `ICarbCtcClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../CarbCtcClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Compliance/CarbCtcModels.cs`  

---

## Endpoints

### ⚠️ `GET /fleet/carb-ctc/vehicles`
**Operation ID**: `listCarbCtcVehicles`  
**Summary**: List CARB CTC enrolled vehicles  
**Parameters**: `tagIds`, `parentTagIds`, `testStatus`, `after`  
**Request Body**: No  

- [x] Method defined in `ICarbCtcClient`
- [x] Method implemented in `CarbCtcClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /fleet/carb-ctc/vehicles/history`
**Operation ID**: `listCarbCtcVehicleHistory`  
**Summary**: List CARB CTC vehicle collection history  
**Parameters**: `vehicleIds`, `startTime`, `endTime`, `after`  
**Request Body**: No  

- [x] Method defined in `ICarbCtcClient`
- [x] Method implemented in `CarbCtcClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Compliance/CarbCtcModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
