# Route Events — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/1 endpoints implemented)  
> **SDK Client**: `IRoutesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../RoutesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Routes/RouteModels.cs`  

---

## Endpoints

### ⚠️ `GET /route-events/stream`
**Operation ID**: `getRouteEventsStream`  
**Summary**: Get route events stream  
**Parameters**: `after`, `startTime`, `limit`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IRoutesClient`
- [x] Method implemented in `RoutesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

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
