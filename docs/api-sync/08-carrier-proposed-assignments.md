# Carrier Proposed Assignments — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (3/3 endpoints implemented)  
> **SDK Client**: `ICarrierProposedAssignmentsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../CarrierProposedAssignmentsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Assignments/AssignmentModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/carrier-proposed-assignments`
**Operation ID**: `listCarrierProposedAssignments`  
**Summary**: Retrieve assignments  
**Parameters**: `limit`, `after`, `driverIds`, `activeTime`  
**Request Body**: No  

- [x] Method defined in `ICarrierProposedAssignmentsClient`
- [x] Method implemented in `CarrierProposedAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /fleet/carrier-proposed-assignments`
**Operation ID**: `createCarrierProposedAssignment`  
**Summary**: Create an assignment  
**Request Body**: Yes  

- [x] Method defined in `ICarrierProposedAssignmentsClient`
- [x] Method implemented in `CarrierProposedAssignmentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /fleet/carrier-proposed-assignments/{id}`
**Operation ID**: `deleteCarrierProposedAssignment`  
**Summary**: Delete an assignment  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `ICarrierProposedAssignmentsClient`
- [x] Method implemented in `CarrierProposedAssignmentsClient.cs`
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
