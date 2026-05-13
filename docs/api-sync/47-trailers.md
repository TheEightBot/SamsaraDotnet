# Trailers — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (5/8 endpoints implemented)  
> **SDK Client**: `ITrailersClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../TrailersClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/TrailerModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/trailers`
**Operation ID**: `listTrailers`  
**Summary**: List all trailers  
**Parameters**: `tagIds`, `parentTagIds`, `limit`, `after`  
**Request Body**: No  

- [x] Method defined in `ITrailersClient`
- [x] Method implemented in `TrailersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /fleet/trailers`
**Operation ID**: `createTrailer`  
**Summary**: Creates a new trailer asset  
**Request Body**: Yes  

- [x] Method defined in `ITrailersClient`
- [x] Method implemented in `TrailersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /fleet/trailers/stats`
**Operation ID**: `getTrailerStatsSnapshot`  
**Summary**: Get trailer stats  
**Parameters**: `types`, `tagIds`, `parentTagIds`, `after`, `trailerIds`, `time`  
**Request Body**: No  

- [x] Method defined in `ITrailersClient`
- [x] Method implemented in `TrailersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /fleet/trailers/stats/feed`
**Operation ID**: `getTrailerStatsFeed`  
**Summary**: Get trailer stats feed  
**Parameters**: `types`, `tagIds`, `parentTagIds`, `after`, `trailerIds`, `decorations`  
**Request Body**: No  

- [x] Method defined in `ITrailersClient`
- [x] Method implemented in `TrailersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /fleet/trailers/stats/history`
**Operation ID**: `getTrailerStatsHistory`  
**Summary**: Get trailer stats history  
**Parameters**: `startTime`, `endTime`, `types`, `tagIds`, `parentTagIds`, `after`, `trailerIds`, `decorations`  
**Request Body**: No  

- [x] Method defined in `ITrailersClient`
- [x] Method implemented in `TrailersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `DELETE /fleet/trailers/{id}`
**Operation ID**: `deleteTrailer`  
**Summary**: Delete a trailer  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `ITrailersClient`
- [x] Method implemented in `TrailersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/trailers/{id}`
**Operation ID**: `getTrailer`  
**Summary**: Retrieve a trailer  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `ITrailersClient`
- [x] Method implemented in `TrailersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /fleet/trailers/{id}`
**Operation ID**: `updateTrailer`  
**Summary**: Update a trailer  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `ITrailersClient`
- [x] Method implemented in `TrailersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/TrailerModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
