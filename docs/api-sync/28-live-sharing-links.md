# Live Sharing Links — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/4 endpoints implemented)  
> **SDK Client**: `ILiveSharingLinksClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../LiveSharingLinksClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/LiveSharingModels.cs`  

---

## Endpoints

### ⚠️ `DELETE /live-shares`
**Operation ID**: `deleteLiveSharingLink`  
**Summary**: Delete non-expired Live Sharing Link  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `ILiveSharingLinksClient`
- [x] Method implemented in `LiveSharingLinksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /live-shares`
**Operation ID**: `getLiveSharingLinks`  
**Summary**: Get Live Sharing Links  
**Parameters**: `ids`, `type`, `limit`, `after`  
**Request Body**: No  

- [x] Method defined in `ILiveSharingLinksClient`
- [x] Method implemented in `LiveSharingLinksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `PATCH /live-shares`
**Operation ID**: `updateLiveSharingLink`  
**Summary**: Update non-expired Live Sharing Link  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `ILiveSharingLinksClient`
- [x] Method implemented in `LiveSharingLinksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `POST /live-shares`
**Operation ID**: `createLiveSharingLink`  
**Summary**: Create Live Sharing Link  
**Request Body**: Yes  

- [x] Method defined in `ILiveSharingLinksClient`
- [x] Method implemented in `LiveSharingLinksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/LiveSharingModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
