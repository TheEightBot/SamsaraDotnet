# Settings — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/42-settings.md`](model-sync-plan-2026-05-27/42-settings.md). Aligned the five settings request/response models to spec: `SafetySettings` gained its 12 spec-REQUIRED fields (`defaultVehicleType` string, `safetyScoreTarget` long, plus ten weakly-typed `object` config blobs — `distractedDrivingDetectionAlerts`, `followingDistanceDetectionAlerts`, `forwardCollisionDetectionAlerts`, `harshEventSensitivity`, `harshEventSensitivityV2`, `policyViolationsDetectionAlerts`, `rollingStopDetectionAlerts`, `safetyScoreConfiguration`, `speedingSettings`, `voiceCoaching`). `ComplianceSettings`/`UpdateComplianceSettingsRequest` each gained 10 optional fields and `DriverAppSettings`/`UpdateDriverAppSettingsRequest` each gained 6 optional fields. The 25 non-spec flat scalars (e.g. `hosEnabled`, `messageEnabled`, `forwardCollisionWarningEnabled`) are kept as nullable back-compat extras.  
> **SDK Client**: `ISettingsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../SettingsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Organization/SettingsModels.cs`  

---

## Endpoints

### ⚠️ `GET /fleet/settings/compliance`
**Operation ID**: `getComplianceSettings`  
**Summary**: Get compliance settings  
**Request Body**: No  

- [x] Method defined in `ISettingsClient`
- [x] Method implemented in `SettingsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `PATCH /fleet/settings/compliance`
**Operation ID**: `patchComplianceSettings`  
**Summary**: Update compliance settings  
**Request Body**: Yes  

- [x] Method defined in `ISettingsClient`
- [x] Method implemented in `SettingsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /fleet/settings/driver-app`
**Operation ID**: `getDriverAppSettings`  
**Summary**: Get driver app settings  
**Request Body**: No  

- [x] Method defined in `ISettingsClient`
- [x] Method implemented in `SettingsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `PATCH /fleet/settings/driver-app`
**Operation ID**: `patchDriverAppSettings`  
**Summary**: Update driver app settings  
**Request Body**: Yes  

- [x] Method defined in `ISettingsClient`
- [x] Method implemented in `SettingsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /fleet/settings/safety`
**Operation ID**: `getSafetySettings`  
**Summary**: Get safety settings  
**Request Body**: No  

- [x] Method defined in `ISettingsClient`
- [x] Method implemented in `SettingsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Organization/SettingsModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
