# TrainingCourses — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/49-trainingcourses.md`](model-sync-plan-2026-05-27/49-trainingcourses.md). `ListCoursesAsync` (`GET /training-courses`) gained 3 optional query params (`categoryIds`/`courseIds`/`status` as `IReadOnlyList<string>?`), appended via `QueryBuilder.WithParams`. `TrainingCourse` gained 5 required props (`title`/`status`/`revisionId` as `string`, `category` as weakly-typed `object`, `estimatedTimeToCompleteMinutes` as `long`) and 1 optional prop (`labels` as `IReadOnlyList<object>?`). The 4 LOW non-spec extras (`name`/`isActive`/`createdAtTime`/`updatedAtTime`) are retained as nullable back-compat props — `name` kept because the spec's course-title field is `title`. CLI `List Courses` call site updated (named `cancellationToken:` arg). No JsonContext/test changes.  
> **⚠️ 2026-05-21 audit**: `fleet/training/courses`→`/training-courses`. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `ITrainingClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../TrainingClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Training/TrainingModels.cs`  

---

## Endpoints

### ✅ `GET /training-courses`
**Operation ID**: `getTrainingCourses`  
**Summary**: Get a list of filtered training courses.  
**Parameters**: `after`, `courseIds`, `categoryIds`, `status`  
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
