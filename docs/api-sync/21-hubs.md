# Hubs — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (1/7 endpoints implemented)  
> **SDK Client**: `IHubsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../HubsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Routes/HubModels.cs`  

---

## Endpoints

### ❌ `GET /hub/capacities`
**Operation ID**: `listHubCapacities`  
**Summary**: List capacities for a specific hub  
**Parameters**: `hubId`, `capacityIds`, `capacityNames`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /hub/customProperties`
**Operation ID**: `listHubCustomProperties`  
**Summary**: List custom properties for a specific hub  
**Parameters**: `hubId`, `customPropertyIds`, `customPropertyNames`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `PATCH /hub/location/{id}`
**Operation ID**: `updateHubLocation`  
**Summary**: Update a location  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /hub/locations`
**Operation ID**: `listHubLocations`  
**Summary**: List locations for a specific hub  
**Parameters**: `hubId`, `locationIds`, `customerLocationIds`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `POST /hub/locations`
**Operation ID**: `createHubLocations`  
**Summary**: Create locations in bulk  
**Request Body**: Yes  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /hub/skills`
**Operation ID**: `listHubSkills`  
**Summary**: List skills for a specific hub  
**Parameters**: `hubId`, `skillIds`, `skillNames`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /hubs`
**Operation ID**: `listHubs`  
**Summary**: List all hubs for the organization  
**Parameters**: `hubIds`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Routes/HubModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
