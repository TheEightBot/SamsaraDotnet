# Driver-Vehicle Assignments — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (4/4 endpoints implemented)  
> **SDK Client**: `IDriverVehicleAssignmentsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../DriverVehicleAssignmentsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Assignments/AssignmentModels.cs`  

---

## Endpoints

### ✅ `DELETE /fleet/driver-vehicle-assignments`
**Operation ID**: `deleteDriverVehicleAssignments`  
**Summary**: Delete API generated driver-vehicle assignments  
**Request Body**: Yes  

- [x] Method defined in `IDriverVehicleAssignmentsClient`
- [x] Method implemented in `DriverVehicleAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/driver-vehicle-assignments`
**Operation ID**: `getDriverVehicleAssignments`  
**Summary**: Get all driver-vehicle assignments  
**Parameters**: `filterBy`, `startTime`, `endTime`, `driverIds`, `vehicleIds`, `driverTagIds`, `vehicleTagIds`, `after`, `assignmentType`  
**Request Body**: No  

- [x] Method defined in `IDriverVehicleAssignmentsClient`
- [x] Method implemented in `DriverVehicleAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /fleet/driver-vehicle-assignments`
**Operation ID**: `updateDriverVehicleAssignment`  
**Summary**: Update API generated driver-vehicle assignments  
**Request Body**: Yes  

- [x] Method defined in `IDriverVehicleAssignmentsClient`
- [x] Method implemented in `DriverVehicleAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /fleet/driver-vehicle-assignments`
**Operation ID**: `createDriverVehicleAssignment`  
**Summary**: Create a new driver-vehicle assignment  
**Request Body**: Yes  

- [x] Method defined in `IDriverVehicleAssignmentsClient`
- [x] Method implemented in `DriverVehicleAssignmentsClient.cs`
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
