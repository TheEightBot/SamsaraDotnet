# Fuel and Energy — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (5/5 endpoints implemented)  
> **SDK Client**: `IFuelClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../FuelClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fuel/FuelModels.cs`  

---

## Endpoints

### ✅ `GET /driver-efficiency/drivers`
**Operation ID**: `getDriverEfficiencyByDrivers`  
**Summary**: Get Driver efficiency data grouped by drivers.  
**Parameters**: `startTime`, `endTime`, `driverIds`, `dataFormats`, `tagIds`, `parentTagIds`, `after`  
**Request Body**: No  

- [x] Method defined in `IFuelClient`
- [x] Method implemented in `FuelClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /driver-efficiency/vehicles`
**Operation ID**: `getDriverEfficiencyByVehicles`  
**Summary**: Get Driver efficiency data grouped by vehicles.  
**Parameters**: `startTime`, `endTime`, `vehicleIds`, `dataFormats`, `tagIds`, `parentTagIds`, `after`  
**Request Body**: No  

- [x] Method defined in `IFuelClient`
- [x] Method implemented in `FuelClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/reports/drivers/fuel-energy`
**Operation ID**: `getFuelEnergyDriverReports`  
**Summary**: Get fuel and energy efficiency driver reports.  
**Parameters**: `startDate`, `endDate`, `driverIds`, `tagIds`, `parentTagIds`, `after`  
**Request Body**: No  

- [x] Method defined in `IFuelClient`
- [x] Method implemented in `FuelClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/reports/vehicles/fuel-energy`
**Operation ID**: `getFuelEnergyVehicleReports`  
**Summary**: Get fuel and energy efficiency vehicle reports.  
**Parameters**: `startDate`, `endDate`, `vehicleIds`, `energyType`, `tagIds`, `parentTagIds`, `after`  
**Request Body**: No  

- [x] Method defined in `IFuelClient`
- [x] Method implemented in `FuelClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /fuel-purchase`
**Operation ID**: `postFuelPurchase`  
**Summary**: Create a fuel purchase transaction.  
**Request Body**: Yes  

- [x] Method defined in `IFuelClient`
- [x] Method implemented in `FuelClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fuel/FuelModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
