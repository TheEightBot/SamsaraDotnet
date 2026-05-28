# Idling — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: Resolved 2026-05-27 (model-sync plan)  
> **SDK Client**: `IIdlingClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../IdlingClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/IdlingModels.cs`  

---

## Endpoints

### ⚠️ `GET /idling/events`
**Operation ID**: `getIdlingEvents`  
**Summary**: Get idling events.  
**Parameters**: `startTime`, `endTime`, `assetIds`, `operatorIds`, `ptoState`, `minAirTemperatureMillicelsius`, `maxAirTemperatureMillicelsius`, `excludeEventsWithUnknownAirTemperature`, `minDurationMilliseconds`, `maxDurationMilliseconds`, `tagIds`, `parentTagIds`, `includeExternalIds`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IIdlingClient`
- [x] Method implemented in `IdlingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/IdlingModels.cs` for model definitions used by this domain.

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [x] All models have XML documentation
- [x] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

- **2026-05-27 — Model sync plan applied.** See
  [`docs/api-sync/model-sync-plan-2026-05-27/23-idling.md`](model-sync-plan-2026-05-27/23-idling.md)
  for full details. `IdlingEvent` was rebuilt to match
  `IdlingEventObject_V2025_10_23ResponseBody`: nine spec-REQUIRED fields
  (`asset`, `durationMilliseconds`, `eventUuid`, `fuelConsumedMilliliters`,
  `fuelCost`, `gaseousFuelConsumedGrams`, `gaseousFuelCost`, `ptoState`,
  `startTime`) are now non-nullable `required`; nested records were added
  for `asset`, `address`, `operator`, `fuelCost`, and `gaseousFuelCost`;
  the optional `airTemperatureMillicelsius` field was added. Eight SDK-only
  flat scalars (`id`, `vehicleId/Name`, `driverId/Name`, `endTime`,
  `durationMs`, `fuelConsumedMl`) absent from the spec inner schema were
  removed. `ListEventsAsync` gained 11 optional query parameters
  (`assetIds`, `operatorIds`, `ptoState`, `min/maxAirTemperatureMillicelsius`,
  `excludeEventsWithUnknownAirTemperature`, `min/maxDurationMilliseconds`,
  `tagIds`, `parentTagIds`, `includeExternalIds`) to cover the full spec
  query surface.
