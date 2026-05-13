# Gateways — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (3/3 endpoints implemented)  
> **SDK Client**: `IGatewaysClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../GatewaysClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/GatewayModels.cs`  

---

## Endpoints

### ✅ `GET /gateways`
**Operation ID**: `getGateways`  
**Summary**: List all gateways  
**Parameters**: `models`, `after`  
**Request Body**: No  

- [x] Method defined in `IGatewaysClient`
- [x] Method implemented in `GatewaysClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /gateways`
**Operation ID**: `postGateway`  
**Summary**: Activate a new gateway  
**Request Body**: Yes  

- [x] Method defined in `IGatewaysClient`
- [x] Method implemented in `GatewaysClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /gateways/{id}`
**Operation ID**: `deleteGateway`  
**Summary**: Deactivate a gateway  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IGatewaysClient`
- [x] Method implemented in `GatewaysClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/GatewayModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
