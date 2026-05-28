# Speeding Intervals — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/43-speeding-intervals.md`](model-sync-plan-2026-05-27/43-speeding-intervals.md). Aligned `SpeedingInterval` (response) and the `GetSpeedingIntervalsStreamAsync` query to spec for `GET /speeding-intervals/stream`: the response gained its 5 spec-REQUIRED fields as `required` (`asset` `object`, `intervals` `IReadOnlyList<object>`, and the `createdAtTime`/`tripStartTime`/`updatedAtTime` timestamps as `DateTimeOffset`). The method gained a leading spec-REQUIRED `assetIds` parameter (**breaking** signature change) plus 4 optional query params (`queryBy`, `severityLevels`, `includeAsset`, `includeDriverId`). The 10 non-spec flat scalars (e.g. `vehicleId`, `maxSpeedMph`, `latitude`) are kept as nullable back-compat extras (`id` demoted from `required` to nullable).  
> **SDK Client**: `IVehiclesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../VehiclesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`  

---

## Endpoints

### ⚠️ `GET /speeding-intervals/stream`
**Operation ID**: `getSpeedingIntervals`  
**Summary**: Get Speeding Intervals  
**Parameters**: `assetIds`, `startTime`, `endTime`, `queryBy`, `includeAsset`, `includeDriverId`, `after`, `severityLevels`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/FleetModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
