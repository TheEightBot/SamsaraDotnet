# Safety — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/39-safety.md`](model-sync-plan-2026-05-27/39-safety.md). `SafetyEvent` rebuilt against `SafetyEventV2ObjectResponseBody` (typed `asset`/`driver`/`behaviorLabels`/`contextLabels`/`location`/`media`/`detectedStreams`/`dismissalReason`/`speedingMetadata`, plus `startMs`/`endMs`/`eventState`/`createdAtTime`/`updatedAtTime`/`inboxEventUrl`/`incidentReportUrl`/`maxAccelerationGForce`/`assignedCoach`/`tripStartTime`/`tripEndTime`/`updatedByUserId`; `id`/`driver`/`behaviorLabels` tightened to required; back-compat `vehicle`/`time` scalars kept). Query params wired: `getSafetyEventsV2` (`safetyEventIds` required + `includeAsset`/`includeDriver`/`includeVgOnlyEvents`), `getSafetyEventsStream` (`startTime` required + `endTime`/`queryByTimeField`/`assetIds`/`driverIds`/`tagIds`/`assignedCoaches`/`behaviorLabels`/`eventStates`/`include*`), and v1 driver/vehicle score (`startMs`/`endMs` required).  
> **⚠️ 2026-05-21 audit**: `GetEventAsync` by-id is fabricated. `SafetyEvent` is a v2 stub — real schema `SafetyEventV2ObjectResponseBody` (asset not vehicle, object `behaviorLabels`, `eventState`, `location`, `maxAccelerationGForce`, `media`…). See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `ISafetyClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../SafetyClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Safety/SafetyModels.cs`  

---

## Endpoints

### ❌ `GET /safety-events`
**Operation ID**: `getSafetyEventsV2`  
**Summary**: Get Safety Events  
**Parameters**: `safetyEventIds`, `includeAsset`, `includeDriver`, `includeVgOnlyEvents`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /safety-events/stream`
**Operation ID**: `getSafetyEventsV2Stream`  
**Summary**: Get Safety Events Stream  
**Parameters**: `startTime`, `endTime`, `queryByTimeField`, `assetIds`, `driverIds`, `tagIds`, `assignedCoaches`, `behaviorLabels`, `eventStates`, `includeAsset`, `includeDriver`, `includeVgOnlyEvents`, `after`  
**Request Body**: No  

- [x] Method defined in `ISafetyClient`
- [x] Method implemented in `SafetyClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /v1/fleet/drivers/{driverId}/safety/score`
**Operation ID**: `V1getDriverSafetyScore`  
**Summary**: Fetch driver safety score  
**Parameters**: `driverId`, `startMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `ISafetyClient`
- [ ] Method implemented in `SafetyClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /v1/fleet/vehicles/{vehicleId}/safety/score`
**Operation ID**: `V1getVehicleSafetyScore`  
**Summary**: Fetch vehicle safety scores  
**Parameters**: `vehicleId`, `startMs`, `endMs`  
**Request Body**: No  

- [ ] Method defined in `ISafetyClient`
- [ ] Method implemented in `SafetyClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Safety/SafetyModels.cs` for model definitions used by this domain.

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

**Model audit (2025-05-13):** `SafetyEvent` was using v1 fields that no longer exist in the v2 API.

- `SafetyEvent`: replaced `behaviorLabel` (singular string) with `behaviorLabels` (array); removed v1-only fields `maxGForce`, `location`, `coachingState`, `incidentReportUrl`, `downloadForwardVideoUrl`, `downloadInwardVideoUrl`.
- `SafetyEventLocation` class removed entirely (v1 only; v2 API does not surface location on the event object).
