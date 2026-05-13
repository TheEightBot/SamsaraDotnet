# TrainingAssignments — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (4/4 endpoints implemented)  
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
