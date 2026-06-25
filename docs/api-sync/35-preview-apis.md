# Preview APIs — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/4 endpoints implemented)  
> **SDK Client**: `multiple`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../multiple.cs`  
> **Models**: `src/Samsara.Sdk/various`  

---

## Endpoints

### ⚠️ `POST /preview/fleet/drivers/create-auth-token`
**Operation ID**: `createDriverAuthToken`  
**Summary**: [preview] Create auth token for a driver  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /preview/fleet/vehicles/{id}/lock`
**Operation ID**: `unlockVehicle`  
**Summary**: [preview] Unlock a vehicle.  
**Parameters**: `id`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PUT /preview/fleet/vehicles/{id}/lock`
**Operation ID**: `lockVehicle`  
**Summary**: [preview] Lock a vehicle.  
**Parameters**: `id`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ➡️ `POST /preview/gateways/pair` — REMOVED (relocated)
**Operation ID**: `pairGateways`  
**Summary**: Removed from the spec on the 2026-06-22 sync; pairing moved to the (beta)
`POST /gateways/pair`. Implemented as `IGatewaysClient.PairGatewaysAsync` — see
[`19-gateways.md`](19-gateways.md).

### ✅ `POST /preview/fleet/tachograph/file-uploads`
**Operation ID**: `postTachographFileUpload`  
**Summary**: [preview] Create a tachograph file upload  
**Request Body**: Yes  

- [x] Method defined in `IPreviewApisClient` (`CreateTachographFileUploadAsync`)
- [x] Method implemented in `PreviewApisClient.cs`
- [x] Request model(s) defined (if applicable) — loosely typed `object` (preview)
- [x] Response model(s) defined — loosely typed `object` (preview)
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`) — n/a for `object`
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/various` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
