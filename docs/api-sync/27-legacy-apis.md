# Legacy APIs — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/8 endpoints implemented)  
> **SDK Client**: `N/A (legacy)`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../N/A.cs`  
> **Models**: `src/Samsara.Sdk/N/A`  

---

## Endpoints

### ⚠️ `GET /fleet/defects/history`
**Operation ID**: `getDvirDefects`  
**Summary**: [legacy] Get all defects  
**Parameters**: `limit`, `after`, `startTime`, `endTime`, `isResolved`  
**Request Body**: No  

- [ ] Method defined in `N/A (legacy)`
- [ ] Method implemented in `N/A.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/drivers/vehicle-assignments`
**Operation ID**: `getDriversVehicleAssignments`  
**Summary**: [legacy] Get all vehicles assigned to a set of drivers  
**Parameters**: `driverIds`, `startTime`, `endTime`, `tagIds`, `parentTagIds`, `driverActivationStatus`, `after`  
**Request Body**: No  

- [ ] Method defined in `N/A (legacy)`
- [ ] Method implemented in `N/A.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/dvirs/history`
**Operation ID**: `getDvirHistory`  
**Summary**: [legacy] Get all DVIRs  
**Parameters**: `limit`, `after`, `parentTagIds`, `tagIds`, `startTime`, `endTime`  
**Request Body**: No  

- [ ] Method defined in `N/A (legacy)`
- [ ] Method implemented in `N/A.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/reports/vehicle/idling`
**Operation ID**: `getVehicleIdlingReports`  
**Summary**: [legacy] Get vehicle idling reports.  
**Parameters**: `after`, `limit`, `startTime`, `endTime`, `vehicleIds`, `tagIds`, `parentTagIds`, `isPtoActive`, `minIdlingDurationMinutes`  
**Request Body**: No  

- [ ] Method defined in `N/A (legacy)`
- [ ] Method implemented in `N/A.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/safety-events`
**Operation ID**: `getSafetyEvents`  
**Summary**: [legacy] List all safety events.  
**Parameters**: `after`, `startTime`, `endTime`, `tagIds`, `parentTagIds`, `vehicleIds`  
**Request Body**: No  

- [ ] Method defined in `N/A (legacy)`
- [ ] Method implemented in `N/A.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/safety-events/audit-logs/feed`
**Operation ID**: `getSafetyActivityEventFeed`  
**Summary**: [legacy] Fetches safety activity event feed  
**Parameters**: `after`, `startTime`  
**Request Body**: No  

- [ ] Method defined in `N/A (legacy)`
- [ ] Method implemented in `N/A.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/vehicles/driver-assignments`
**Operation ID**: `getVehiclesDriverAssignments`  
**Summary**: [legacy] Get all drivers assigned to a set of vehicles  
**Parameters**: `startTime`, `endTime`, `vehicleIds`, `tagIds`, `parentTagIds`, `after`  
**Request Body**: No  

- [ ] Method defined in `N/A (legacy)`
- [ ] Method implemented in `N/A.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /v1/fleet/vehicles/{vehicleId}/safety/harsh_event`
**Operation ID**: `V1getVehicleHarshEvent`  
**Summary**: [legacy] Fetch harsh events  
**Parameters**: `vehicleId`, `timestamp`  
**Request Body**: No  

- [ ] Method defined in `N/A (legacy)`
- [ ] Method implemented in `N/A.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/N/A` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
