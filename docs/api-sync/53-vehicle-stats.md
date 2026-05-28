# Vehicle Stats — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/53-vehicle-stats.md`](model-sync-plan-2026-05-27/53-vehicle-stats.md). All 77 MEDIUM findings applied across the 3 stats endpoints. The `VehicleStats` response record gained **58 weakly-typed `object?` props** (aux-input bank, EV telemetry, spreader bank, and remaining `type=object` scalars) plus **3 `IReadOnlyList<object>?` array props** (`engineStates`, `fuelPercents`, `nfcCardScans`). The 3 `type_mismatch` fields `gps`/`gpsOdometerMeters`/`obdOdometerMeters` were changed to **`object?`** (deviation from the plan's `IReadOnlyList<object>`): each is a single object on the snapshot endpoint but an array on feed/history, so `object?` accepts either shape (cf. dual-shape `EquipmentStats`). **Breaking**: `VehicleStats.name` tightened from `string?` to `required string` (verified safe — no `new VehicleStats(...)` sites). 12 optional query params added across the 3 methods (`vehicleIds`/`tagIds`/`parentTagIds` on all three; plus `time` on the snapshot and `decorations` on feed/history) — the `?types=` path-embedded query was refactored to `QueryBuilder.WithParams`. The 4 LOW non-spec extras (`time`, `engineState`, `fuelPercent`, `engineSeconds`) were retained as nullable back-compat props. CLI `List Stats` fixed (named `cancellationToken:`, `s.Name` un-coalesced). No JsonContext/test changes.  
> **SDK Client**: `IVehiclesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../VehiclesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/vehicles/stats`
**Operation ID**: `getVehicleStats`  
**Summary**: Stats snapshot  
**Parameters**: `after`, `time`, `parentTagIds`, `tagIds`, `vehicleIds`, `types`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /fleet/vehicles/stats/feed`
**Operation ID**: `getVehicleStatsFeed`  
**Summary**: Stats feed  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `vehicleIds`, `types`, `decorations`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /fleet/vehicles/stats/history`
**Operation ID**: `getVehicleStatsHistory`  
**Summary**: Historical stats  
**Parameters**: `after`, `startTime`, `endTime`, `parentTagIds`, `tagIds`, `vehicleIds`, `types`, `decorations`  
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
