# Vehicle Stats — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (1/3 endpoints implemented)  
> **SDK Client**: `IVehiclesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../VehiclesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/vehicles/stats`
**Operation ID**: `getVehicleStats`  
**Summary**: Stats snapshot  
**Parameters**: `after`, `time`, `parentTagIds`, `tagIds`, `vehicleIds`, `types`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /fleet/vehicles/stats/feed`
**Operation ID**: `getVehicleStatsFeed`  
**Summary**: Stats feed  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `vehicleIds`, `types`, `decorations`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /fleet/vehicles/stats/history`
**Operation ID**: `getVehicleStatsHistory`  
**Summary**: Historical stats  
**Parameters**: `after`, `startTime`, `endTime`, `parentTagIds`, `tagIds`, `vehicleIds`, `types`, `decorations`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/FleetModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
