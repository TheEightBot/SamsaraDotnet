# Tachograph (EU Only) — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (3/3 endpoints implemented)  
> **SDK Client**: `ITachographClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../TachographClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Compliance/TachographModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/drivers/tachograph-activity/history`
**Operation ID**: `getDriverTachographActivity`  
**Summary**: Get driver tachograph activity  
**Parameters**: `after`, `startTime`, `endTime`, `driverIds`, `parentTagIds`, `tagIds`  
**Request Body**: No  

- [x] Method defined in `ITachographClient`
- [x] Method implemented in `TachographClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/drivers/tachograph-files/history`
**Operation ID**: `getDriverTachographFiles`  
**Summary**: Get tachograph driver files  
**Parameters**: `after`, `startTime`, `endTime`, `driverIds`, `parentTagIds`, `tagIds`  
**Request Body**: No  

- [x] Method defined in `ITachographClient`
- [x] Method implemented in `TachographClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/vehicles/tachograph-files/history`
**Operation ID**: `getVehicleTachographFiles`  
**Summary**: Get tachograph vehicle files  
**Parameters**: `after`, `startTime`, `endTime`, `vehicleIds`, `parentTagIds`, `tagIds`  
**Request Body**: No  

- [x] Method defined in `ITachographClient`
- [x] Method implemented in `TachographClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Compliance/TachographModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
