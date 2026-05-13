# Coaching — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/3 endpoints implemented)  
> **SDK Client**: `ICoachingClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../CoachingClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Safety/CoachingModels.cs`  

---

## Endpoints

### ⚠️ `GET /coaching/driver-coach-assignments`
**Operation ID**: `getDriverCoachAssignment`  
**Summary**: Get driver coach assignments.  
**Parameters**: `driverIds`, `coachIds`, `includeExternalIds`, `after`  
**Request Body**: No  

- [x] Method defined in `ICoachingClient`
- [x] Method implemented in `CoachingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `PUT /coaching/driver-coach-assignments`
**Operation ID**: `putDriverCoachAssignment`  
**Summary**: Put driver coach assignments.  
**Parameters**: `driverId`, `coachId`  
**Request Body**: No  

- [x] Method defined in `ICoachingClient`
- [x] Method implemented in `CoachingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /coaching/sessions/stream`
**Operation ID**: `getCoachingSessions`  
**Summary**: Get coaching sessions.  
**Parameters**: `driverIds`, `coachIds`, `sessionStatuses`, `includeCoachableEvents`, `startTime`, `endTime`, `after`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `ICoachingClient`
- [x] Method implemented in `CoachingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Safety/CoachingModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
