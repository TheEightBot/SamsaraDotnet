# Vehicle Locations — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/52-vehicle-locations.md`](model-sync-plan-2026-05-27/52-vehicle-locations.md). All 2 HIGH + 11 MEDIUM findings applied across the 3 location endpoints. The single `VehicleLocation` record deserializes 3 mutually-exclusive shapes (snapshot `location` object vs. feed/history `locations` array), so the 2 HIGH `response_drift_required` props (`location`, `locations`) were modeled **nullable** (weakly-typed `object?`/`IReadOnlyList<object>?`) rather than `required` — marking either required would throw on the other shape. **Breaking**: `VehicleLocation.name` tightened from `string?` to `required string` (present in all 3 shapes; verified safe — no `new VehicleLocation(...)` sites). 10 optional query params added across the 3 methods (`vehicleIds`/`tagIds`/`parentTagIds` on all three, plus `time` on the snapshot). The 7 LOW non-spec extras were retained as nullable back-compat props — and `latitude`/`longitude`/`time` were **demoted** from `required` to nullable (`double?`/`double?`/`DateTimeOffset?`) so the real wrapper shapes still deserialize (precedent: `SpeedingInterval.Id`, `Trip.Id`). CLI `List Locations` fixed (named `cancellationToken:`, `l.Name` un-coalesced, `Latitude`/`Longitude` rendered via `?.ToString() ?? ""`). No JsonContext/test changes.  
> **⚠️ 2026-05-21 audit**: model — `VehicleLocation` missing `reverseGeo`; `latitude`/`longitude`/`time` should be required. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `IVehiclesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../VehiclesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/vehicles/locations`
**Operation ID**: `getVehicleLocations`  
**Summary**: Locations snapshot  
**Parameters**: `after`, `time`, `parentTagIds`, `tagIds`, `vehicleIds`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /fleet/vehicles/locations/feed`
**Operation ID**: `getVehicleLocationsFeed`  
**Summary**: Locations feed  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `vehicleIds`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /fleet/vehicles/locations/history`
**Operation ID**: `getVehicleLocationsHistory`  
**Summary**: Historical locations  
**Parameters**: `after`, `startTime`, `endTime`, `parentTagIds`, `tagIds`, `vehicleIds`  
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
