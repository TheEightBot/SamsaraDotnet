# Driver-Trailer Assignments — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/3 endpoints implemented)  
> **SDK Client**: `IDriverTrailerAssignmentsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../DriverTrailerAssignmentsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Assignments/AssignmentModels.cs`  

---

## Endpoints

### ⚠️ `GET /driver-trailer-assignments`
**Operation ID**: `getDriverTrailerAssignments`  
**Summary**: Get currently active driver-trailer assignments for driver.  
**Parameters**: `driverIds`, `after`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IDriverTrailerAssignmentsClient`
- [x] Method implemented in `DriverTrailerAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `PATCH /driver-trailer-assignments`
**Operation ID**: `updateDriverTrailerAssignment`  
**Summary**: Update an existing driver-trailer assignment.  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IDriverTrailerAssignmentsClient`
- [x] Method implemented in `DriverTrailerAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `POST /driver-trailer-assignments`
**Operation ID**: `createDriverTrailerAssignment`  
**Summary**: Create a new driver-trailer assignment  
**Request Body**: Yes  

- [x] Method defined in `IDriverTrailerAssignmentsClient`
- [x] Method implemented in `DriverTrailerAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

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
