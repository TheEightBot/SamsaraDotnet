# Trailers — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/47-trailers.md`](model-sync-plan-2026-05-27/47-trailers.md). The three stats methods (`GetStatsSnapshotAsync`, `GetStatsFeedAsync`, `GetStatsHistoryAsync`) gained a required `string types` query param (placed first, no default — **breaking**) plus 14 optional `string?` query params across List/stats methods. `TrailerStats` had `name` tightened from `string?` to `required string` (spec REQUIRED) and gained 23 weakly-typed `object?` reefer/gps props (24 findings). `Trailer` gained `attributes`/`enabledForMobile`/`trailerSerialNumber`; `CreateTrailerRequest` gained the same three; `UpdateTrailerRequest` additionally gained `odometerMeters` (`long?`). All 21 LOW non-spec extras (make/model/serial/vin/year and friends) are retained as nullable back-compat props. CLI `ListAsync` call site updated (named `cancellationToken:`). No JsonContext/test changes.  
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
