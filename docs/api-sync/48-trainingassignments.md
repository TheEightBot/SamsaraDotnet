# TrainingAssignments — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/48-trainingassignments.md`](model-sync-plan-2026-05-27/48-trainingassignments.md). `ListAssignmentsAsync` (`GET /training-assignments/stream`) gained a required `DateTimeOffset startTime` query param (placed first, no default — **breaking**) plus 6 optional query params (`endTime` as `DateTimeOffset?`; `categoryIds`/`courseIds`/`learnerIds`/`status` as `IReadOnlyList<string>?`; `isOverdue` as `bool?`). `TrainingAssignment` gained 7 required props (`course`/`learner` as weakly-typed `object`, `createdById`/`updatedById` as `string`, `createdAtTime`/`updatedAtTime` as `DateTimeOffset`, `durationMinutes` as `long`) and 5 optional props (`startedAtTime`, `deletedAtTime`, `isOverdue`, `isCompletedLate`, `scorePercent`); `status` was tightened from `string?` to `required string` (spec REQUIRED — **breaking**). The 6 LOW non-spec extras (`driverId`/`driverName`/`courseId`/`courseName`/`assignedAtTime`/`score`) are retained as nullable back-compat props. CLI `List Assignments` call site updated (default 7-day window + `a.Status` deref). No JsonContext/test changes.  
> **⚠️ 2026-05-21 audit**: `fleet/training/assignments`→`/training-assignments/stream`; missing POST/PATCH/DELETE `/training-assignments`. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `ITrainingClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../TrainingClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Training/TrainingModels.cs`  

---

## Endpoints

### ✅ `DELETE /training-assignments`
**Operation ID**: `deleteTrainingAssignments`  
**Summary**: Delete training assignments.  
**Parameters**: `ids`  
**Request Body**: No  

- [x] Method defined in `ITrainingClient`
- [x] Method implemented in `TrainingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /training-assignments`
**Operation ID**: `patchTrainingAssignments`  
**Summary**: Update training assignments.  
**Parameters**: `ids`, `dueAtTime`  
**Request Body**: No  

- [x] Method defined in `ITrainingClient`
- [x] Method implemented in `TrainingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /training-assignments`
**Operation ID**: `postTrainingAssignments`  
**Summary**: Create training assignments.  
**Parameters**: `courseId`, `dueAtTime`, `learnerIds`  
**Request Body**: No  

- [x] Method defined in `ITrainingClient`
- [x] Method implemented in `TrainingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /training-assignments/stream`
**Operation ID**: `getTrainingAssignmentsStream`  
**Summary**: Get a stream of filtered training assignments.  
**Parameters**: `after`, `startTime`, `endTime`, `learnerIds`, `courseIds`, `status`, `isOverdue`, `categoryIds`  
**Request Body**: No  

- [x] Method defined in `ITrainingClient`
- [x] Method implemented in `TrainingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Training/TrainingModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
