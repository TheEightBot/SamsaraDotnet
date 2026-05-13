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

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

**Model audit (2025-05-13):** Extensive corrections applied to align with the v2 API schema.

- `CreateRouteRequest`: removed `scheduledStartMs`/`scheduledEndMs` (not in API), made `driverId` optional, added `externalIds`, `recomputeScheduledTimes`, `tagIds`; `stops` is required.
- `UpdateRouteRequest`: same field corrections as above; added `stops` (was missing entirely).
- `Route` response: replaced deprecated ms-epoch time fields (`scheduledStartMs`, `scheduledEndMs`, `actualStartMs`, `actualEndMs`) with ISO string equivalents (`scheduledRouteStartTime`, `scheduledRouteEndTime`, `actualRouteStartTime`, `actualRouteEndTime`); added 15+ new response fields.
- `RouteStop` response: removed `addressId`, `latitude`, `longitude`; added `sequenceNumber`, ontime window fields, `enRouteTime`, `eta`, `skippedTime`, distance metrics, `liveSharingUrl`, `address`, `hubLocationId`, `orders`.
- `RouteSettings`: added `sequencingMethod`.
- `CreateRouteStopRequest`: removed `latitude`/`longitude`; added `sequenceNumber`, `ontimeWindowAfterArrivalMs`, `ontimeWindowBeforeArrivalMs`.
- `UpdateRouteStopRequest`: new class — was missing from the SDK entirely.
