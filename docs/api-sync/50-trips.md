# Trips — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/50-trips.md`](model-sync-plan-2026-05-27/50-trips.md). The single `Trip` record is a DUAL-SHAPE unified record deserializing both `GET /v1/fleet/trips` (v1 flat) and `GET /trips/stream` (modern), so the "required" stream-only props are modeled NULLABLE to avoid breaking v1 deserialization. **Breaking**: `GetStreamAsync` gained a spec-required `IReadOnlyList<string> ids` first param (comma-joined via `QueryBuilder.WithParams`) plus 3 optional query params (`completionStatus`/`queryBy` as `string?`, `includeAsset` as `bool?`); the CLI does not call `GetStreamAsync`. `Trip` gained 5 stream props as nullable (`asset` as `object?`, `completionStatus` as `string?`, `createdAtTime`/`tripStartTime`/`updatedAtTime` as `DateTimeOffset?`) and 2 optional props (`tripEndTime` as `DateTimeOffset?`, `trips` as `IReadOnlyList<object>?`). `Trip.startLocation` was NOT tightened (it's an `extra_property` on the v1 shape). **Breaking**: `Trip.Id` demoted from `required string` to `string?` (a non-spec extra absent from both shapes); CLI render updated to `t.Id ?? ""`. All 13 LOW non-spec extras retained as nullable back-compat props. `ListAsync` unchanged. No JsonContext/test changes.  
> **⚠️ 2026-05-21 audit**: `ListAsync` hits `/fleet/vehicles/trips` (not in spec)→`/v1/fleet/trips`; `GetStreamAsync` (`/trips/stream`) is correct. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `ITripsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../TripsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Routes/TripModels.cs`  

---

## Endpoints

### ✅ `GET /trips/stream`
**Operation ID**: `getTrips`  
**Summary**: Get Trips Stream  
**Parameters**: `includeAsset`, `completionStatus`, `startTime`, `endTime`, `queryBy`, `after`, `ids`  
**Request Body**: No  

- [x] Method defined in `ITripsClient`
- [x] Method implemented in `TripsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /v1/fleet/trips`
**Operation ID**: `V1getFleetTrips`  
**Summary**: Get vehicle trips  
**Parameters**: `vehicleId`, `startMs`, `endMs`  
**Request Body**: No  

- [x] Method defined in `ITripsClient`
- [x] Method implemented in `TripsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Routes/TripModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
