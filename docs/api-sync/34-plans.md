# Plans — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/3 endpoints implemented)  
> **SDK Client**: `IHubsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../HubsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Routes/HubModels.cs`  

---

## Endpoints

### ⚠️ `POST /hub/plan`
**Operation ID**: `createHubPlan`  
**Summary**: Create a new plan  
**Request Body**: Yes  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `POST /hub/plan/orders`
**Operation ID**: `createPlanOrders`  
**Summary**: Create orders in bulk  
**Request Body**: Yes  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /hub/plans`
**Operation ID**: `listHubPlans`  
**Summary**: List plans for a specific hub  
**Parameters**: `hubId`, `planIds`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

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
