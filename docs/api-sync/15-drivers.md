# Drivers — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (4/5 endpoints implemented)  
> **SDK Client**: `IDriversClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../DriversClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Drivers/DriverModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/drivers`
**Operation ID**: `listDrivers`  
**Summary**: List all drivers  
**Parameters**: `driverActivationStatus`, `limit`, `after`, `parentTagIds`, `tagIds`, `attributeValueIds`, `attributes`, `updatedAfterTime`, `createdAfterTime`  
**Request Body**: No  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /fleet/drivers`
**Operation ID**: `createDriver`  
**Summary**: Create a driver  
**Request Body**: Yes  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `POST /fleet/drivers/remote-sign-out`
**Operation ID**: `postDriverRemoteSignout`  
**Summary**: Sign out a driver  
**Request Body**: Yes  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /fleet/drivers/{id}`
**Operation ID**: `getDriver`  
**Summary**: Retrieve a driver  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /fleet/drivers/{id}`
**Operation ID**: `updateDriver`  
**Summary**: Update a driver  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Drivers/DriverModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
