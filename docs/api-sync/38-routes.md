# Routes — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (5/8 endpoints implemented)  
> **SDK Client**: `IRoutesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../RoutesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Routes/RouteModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/routes`
**Operation ID**: `fetchRoutes`  
**Summary**: Fetch all routes  
**Parameters**: `startTime`, `endTime`, `limit`, `after`, `include`, `tagIds`, `parentTagIds`  
**Request Body**: No  

- [x] Method defined in `IRoutesClient`
- [x] Method implemented in `RoutesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /fleet/routes`
**Operation ID**: `createRoute`  
**Summary**: Create a route  
**Request Body**: Yes  

- [x] Method defined in `IRoutesClient`
- [x] Method implemented in `RoutesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /fleet/routes/audit-logs/feed`
**Operation ID**: `getRoutesFeed`  
**Summary**: Get route updates  
**Parameters**: `after`, `expand`  
**Request Body**: No  

- [x] Method defined in `IRoutesClient`
- [x] Method implemented in `RoutesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `DELETE /fleet/routes/{id}`
**Operation ID**: `deleteRoute`  
**Summary**: Delete a route.  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IRoutesClient`
- [x] Method implemented in `RoutesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/routes/{id}`
**Operation ID**: `fetchRoute`  
**Summary**: Fetch a route  
**Parameters**: `include`, `id`  
**Request Body**: No  

- [x] Method defined in `IRoutesClient`
- [x] Method implemented in `RoutesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /fleet/routes/{id}`
**Operation ID**: `patchRoute`  
**Summary**: Update a route  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IRoutesClient`
- [x] Method implemented in `RoutesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /hub/plan/routes`
**Operation ID**: `listHubPlanRoutes`  
**Summary**: List routes for a specific plan  
**Parameters**: `planId`, `routeIds`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `IRoutesClient`
- [ ] Method implemented in `RoutesClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `DELETE /v1/fleet/dispatch/routes/{route_id_or_external_id}`
**Operation ID**: `V1deleteDispatchRouteById`  
**Summary**: Delete a route  
**Parameters**: `route_id_or_external_id`  
**Request Body**: Yes  

- [ ] Method defined in `IRoutesClient`
- [ ] Method implemented in `RoutesClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Routes/RouteModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
