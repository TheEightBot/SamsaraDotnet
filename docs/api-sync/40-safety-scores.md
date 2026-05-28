# Safety Scores — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/40-safety-scores.md`](model-sync-plan-2026-05-27/40-safety-scores.md). All four score response models realigned to their real spec schemas (`VehicleSafetyScoreResponseBody`, `DriverSafetyScoreResponseBody`, `TagSafetyScoreResponseBody`, `TagGroupSafetyScoreResponseBody`): added the spec-REQUIRED fields (`behaviors`/`speeding` as typed `SafetyScoreBehavior`/`SafetyScoreSpeeding` lists, `driveDistanceMeters`, `driveTimeMilliseconds`, plus `vehicleScore`/`driverScore`/`tagScore`/`combinedScore`). Query params wired: `scoreType` made required on `getTagSafetyScores`/`getTagGroupSafetyScores`, and optional `vehicleIds`/`driverIds`/`tagIds` filters added across the four list methods. Back-compat flat scalars (`safetyScore`, `timeRange`, `totalHarshEventCount`, `total*Driven*`, harsh-event counts, `tagName`/`tagGroupName`/`tagGroupId`) kept as nullable extras with XML doc pointers.  
> **SDK Client**: `ISafetyClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../SafetyClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Safety/SafetyModels.cs`  

---

## Endpoints

### ❌ `GET /safety-scores/drivers`
**Operation ID**: `getDriverSafetyScores`  
**Summary**: Get driver scores  
**Parameters**: `endTime`, `startTime`, `driverIds`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /safety-scores/tag-group`
**Operation ID**: `getTagGroupSafetyScores`  
**Summary**: Get tags combined score  
**Parameters**: `endTime`, `startTime`, `scoreType`, `tagIds`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /safety-scores/tags`
**Operation ID**: `getTagSafetyScores`  
**Summary**: Get tag scores  
**Parameters**: `endTime`, `startTime`, `scoreType`, `tagIds`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /safety-scores/vehicles`
**Operation ID**: `getVehicleSafetyScores`  
**Summary**: Get vehicle scores  
**Parameters**: `endTime`, `startTime`, `vehicleIds`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Safety/SafetyModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
