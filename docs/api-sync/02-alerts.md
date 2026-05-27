# Alerts — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (5/5 endpoints match the spec) — Resolved 2026-05-27 (model-sync plan)  
> **2026-05-27 model-sync**: `AlertConfiguration` and `AlertIncident` response records now expose every spec-required field (`actions`, `triggers`, `scope`, `createdAtTime`, `lastModifiedAtTime`, `isEnabled` on configs; `conditions`, `happenedAtTime`, `incidentUrl`, `isResolved`, `updatedAtTime` on incidents) with `required`/non-nullable typing. Removed SDK-only extras (`AlertConfiguration.ConditionType`, `AlertIncident.{Id,AlertId,Driver,Vehicle,TriggeredAtTime}`). Introduced typed nested models (`AlertScope`, `AlertTrigger`, `AlertAction`, `AlertOperationalSettings`, `AlertIncidentCondition`) in place of weak `object?` on requests. See [model-sync-plan-2026-05-27/02-alerts.md](model-sync-plan-2026-05-27/02-alerts.md).  
> **2026-05-21 audit (resolved)**: top-level `/alerts` CRUD and `/alerts/incidents{/id}` are fabricated — spec exposes only `/alerts/configurations` (+ `/alerts/incidents/stream`). `UpdateConfiguration` must drop `/{id}`; add `DELETE /alerts/configurations`. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `IAlertsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../AlertsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Communication/CommunicationModels.cs`  

---

## Endpoints

### ✅ `DELETE /alerts/configurations`
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

### ✅ `GET /alerts/configurations`
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

### ✅ `PATCH /alerts/configurations`
**Operation ID**: `patchConfigurations`  
**Summary**: Update alert configurations.  
**Request Body**: Yes  

- [x] Method defined in `IAlertsClient`
- [x] Method implemented in `AlertsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `POST /alerts/configurations`
**Operation ID**: `postConfigurations`  
**Summary**: Create alert configurations.  
**Request Body**: Yes  

- [x] Method defined in `IAlertsClient`
- [x] Method implemented in `AlertsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /alerts/incidents/stream`
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

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [x] All models have XML documentation
- [x] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
