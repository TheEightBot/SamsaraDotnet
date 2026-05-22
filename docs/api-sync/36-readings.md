# Readings — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (3/3 endpoints match the spec)  
> **⚠️ 2026-05-21 audit**: verified correct against the live spec (the index previously understated this domain). See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `IReadingsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../ReadingsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Industrial/ReadingModels.cs`  

---

## Endpoints

### ⚠️ `GET /readings/definitions`
**Operation ID**: `listReadingsDefinitions`  
**Summary**: Get Readings Definitions  
**Parameters**: `after`, `ids`, `entityTypes`  
**Request Body**: No  

- [x] Method defined in `IReadingsClient`
- [x] Method implemented in `ReadingsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /readings/history`
**Operation ID**: `getReadingsHistory`  
**Summary**: Get Readings History and Feed  
**Parameters**: `after`, `readingId`, `entityIds`, `entityType`, `externalIds`, `startTime`, `endTime`, `feed`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IReadingsClient`
- [x] Method implemented in `ReadingsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /readings/latest`
**Operation ID**: `getReadingsSnapshot`  
**Summary**: Get Readings Snapshot  
**Parameters**: `after`, `readingIds`, `entityIds`, `externalIds`, `asOfTime`, `entityType`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IReadingsClient`
- [x] Method implemented in `ReadingsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Industrial/ReadingModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
