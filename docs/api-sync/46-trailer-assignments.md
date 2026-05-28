# Trailer Assignments — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/46-trailer-assignments.md`](model-sync-plan-2026-05-27/46-trailer-assignments.md). The single `TrailerAssignment` record now deserializes BOTH v1 wrapper shapes (list `{ pagination, trailers }` and per-trailer `{ id, name, trailerAssignments }`); it gained `name` (`string?`), `pagination` (`object?`), `trailerAssignments`/`trailers` (`IReadOnlyList<object>?`), and its `id` changed from `required string` to `long?` (int64 + nullable, since the list shape has no top-level id — breaking). `name` is kept nullable (not `required`) because it is absent on the list shape. Both methods gained optional `startMs`/`endMs` query params, typed `long?` (ms-epoch values overflow `Int32`; matches the `*Ms` repo convention). The 7 flat non-spec fields are retained as nullable back-compat extras. CLI call site updated (named `cancellationToken:`, `Id?.ToString()`). No JsonContext/test changes.  
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

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [x] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
