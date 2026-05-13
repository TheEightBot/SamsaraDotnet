# Alerts — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ❌ Not Started (0/5 endpoints implemented)  
> **SDK Client**: `IAlertsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../AlertsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Communication/CommunicationModels.cs`  

---

## Endpoints

### ❌ `DELETE /alerts/configurations`
**Operation ID**: `deleteConfigurations`  
**Summary**: Delete alert configurations.  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IAlertsClient`
- [x] Method implemented in `AlertsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /alerts/configurations`
**Operation ID**: `getConfigurations`  
**Summary**: Get Alert Configurations.  
**Parameters**: `ids`, `status`, `after`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IAlertsClient`
- [x] Method implemented in `AlertsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `PATCH /alerts/configurations`
**Operation ID**: `patchConfigurations`  
**Summary**: Update alert configurations.  
**Request Body**: Yes  

- [x] Method defined in `IAlertsClient`
- [x] Method implemented in `AlertsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `POST /alerts/configurations`
**Operation ID**: `postConfigurations`  
**Summary**: Create alert configurations.  
**Request Body**: Yes  

- [x] Method defined in `IAlertsClient`
- [x] Method implemented in `AlertsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /alerts/incidents/stream`
**Operation ID**: `getIncidents`  
**Summary**: Get Alert Incidents.  
**Parameters**: `startTime`, `configurationIds`, `endTime`, `after`  
**Request Body**: No  

- [x] Method defined in `IAlertsClient`
- [x] Method implemented in `AlertsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Communication/CommunicationModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
