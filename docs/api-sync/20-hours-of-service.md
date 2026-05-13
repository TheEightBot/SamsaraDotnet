# Hours of Service — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (4/6 endpoints implemented)  
> **SDK Client**: `IComplianceClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../ComplianceClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Compliance/ComplianceModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/hos/clocks`
**Operation ID**: `getHosClocks`  
**Summary**: Get HOS clocks  
**Parameters**: `tagIds`, `parentTagIds`, `driverIds`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IComplianceClient`
- [x] Method implemented in `ComplianceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/hos/daily-logs`
**Operation ID**: `getHosDailyLogs`  
**Summary**: Get all driver HOS daily logs  
**Parameters**: `driverIds`, `startDate`, `endDate`, `tagIds`, `parentTagIds`, `driverActivationStatus`, `after`, `expand`  
**Request Body**: No  

- [x] Method defined in `IComplianceClient`
- [x] Method implemented in `ComplianceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/hos/logs`
**Operation ID**: `getHosLogs`  
**Summary**: Get HOS logs  
**Parameters**: `tagIds`, `parentTagIds`, `driverIds`, `startTime`, `endTime`, `after`  
**Request Body**: No  

- [x] Method defined in `IComplianceClient`
- [x] Method implemented in `ComplianceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/hos/violations`
**Operation ID**: `getHosViolations`  
**Summary**: Get all driver HOS violations  
**Parameters**: `driverIds`, `startTime`, `endTime`, `tagIds`, `parentTagIds`, `types`, `after`  
**Request Body**: No  

- [x] Method defined in `IComplianceClient`
- [x] Method implemented in `ComplianceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `POST /v1/fleet/drivers/{driver_id}/hos/duty_status`
**Operation ID**: `setCurrentDutyStatus`  
**Summary**: Set a duty status for a specific driver  
**Parameters**: `driver_id`  
**Request Body**: Yes  

- [ ] Method defined in `IComplianceClient`
- [ ] Method implemented in `ComplianceClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /v1/fleet/hos_authentication_logs`
**Operation ID**: `V1getFleetHosAuthenticationLogs`  
**Summary**: Get HOS signin and signout  
**Parameters**: `driverId`, `startMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `IComplianceClient`
- [ ] Method implemented in `ComplianceClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Compliance/ComplianceModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
