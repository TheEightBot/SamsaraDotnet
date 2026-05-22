# Trailer Assignments — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🔴 Broken (0/2)  
> **⚠️ 2026-05-21 audit**: `fleet/trailer-assignments`→`/v1/fleet/trailers/assignments` (+ per-trailer `/v1/fleet/trailers/{id}/assignments`). See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `ITrailerAssignmentsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../TrailerAssignmentsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Assignments/AssignmentModels.cs`  

---

## Endpoints

### ✅ `GET /v1/fleet/trailers/assignments`
**Operation ID**: `V1getAllTrailerAssignments`  
**Summary**: List trailer assignments for all trailers  
**Parameters**: `startMs`, `endMs`, `limit`, `startingAfter`, `endingBefore`  
**Request Body**: No  

- [x] Method defined in `ITrailerAssignmentsClient`
- [x] Method implemented in `TrailerAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /v1/fleet/trailers/{trailerId}/assignments`
**Operation ID**: `V1getFleetTrailerAssignments`  
**Summary**: List trailer assignments for a given trailer  
**Parameters**: `trailerId`, `startMs`, `endMs`  
**Request Body**: No  

- [x] Method defined in `ITrailerAssignmentsClient`
- [x] Method implemented in `TrailerAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Assignments/AssignmentModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
