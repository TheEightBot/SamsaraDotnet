# Media — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (1/3 endpoints implemented)  
> **SDK Client**: `IMediaClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../MediaClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Media/MediaModels.cs`  

---

## Endpoints

### ✅ `GET /cameras/media`
**Operation ID**: `listUploadedMedia`  
**Summary**: List uploaded media by time range.  
**Parameters**: `vehicleIds`, `inputs`, `mediaTypes`, `triggerReasons`, `startTime`, `endTime`, `availableAfterTime`, `after`  
**Request Body**: No  

- [x] Method defined in `IMediaClient`
- [x] Method implemented in `MediaClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /cameras/media/retrieval`
**Operation ID**: `getMediaRetrieval`  
**Summary**: Get details for a media retrieval request  
**Parameters**: `retrievalId`  
**Request Body**: No  

- [x] Method defined in `IMediaClient`
- [x] Method implemented in `MediaClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `POST /cameras/media/retrieval`
**Operation ID**: `postMediaRetrieval`  
**Summary**: Create a media retrieval request  
**Request Body**: Yes  

- [x] Method defined in `IMediaClient`
- [x] Method implemented in `MediaClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Media/MediaModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
