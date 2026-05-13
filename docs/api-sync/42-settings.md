# Settings — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/5 endpoints implemented)  
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
