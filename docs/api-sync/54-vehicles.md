# Vehicles — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/54-vehicles.md`](model-sync-plan-2026-05-27/54-vehicles.md). All 19 findings (0 CRIT / 1 HIGH / 14 MED / 4 LOW) applied across the three vehicle endpoints. The `Vehicle` response record gained the HIGH `createdAtTime` as **`required DateTimeOffset`** (placed after `name`; the shared spec schema marks it required across the vehicle endpoints — verified safe, no `new Vehicle(...)` sites) plus **5 new nullable props** (`updatedAtTime`, `isRemotePrivacyButtonEnabled`, `vehicleWeight`, `vehicleWeightInKilograms`, `vehicleWeightInPounds`). The two `weak_typing` fields (`grossVehicleWeight`, `sensorConfiguration`) were intentionally KEPT as weakly-typed `object?` (effort convention — no fabricated models for un-schematized `type=object`), and the 4 LOW non-spec extras (`engineHours`, `gatewaySerial`, `grossVehicleWeight`, `odometerMeters`) were retained as nullable back-compat props. `UpdateVehicleRequest.attributes` was changed from `System.Text.Json.JsonElement?` to **`IReadOnlyList<object>?`** (spec `type=array`; the only construction site doesn't set it). Six optional query params were added to `ListAsync` (`GET /fleet/vehicles`) via `QueryBuilder.WithParams` — all `string?` except `attributes` (`IReadOnlyList<string>?`, comma-joined): `attributes`, `attributeValueIds`, `tagIds`, `parentTagIds`, `createdAfterTime`, `updatedAfterTime`. **Breaking**: consumers may now rely on a non-null `Vehicle.CreatedAtTime`; the two `VehiclesClientTests` mock fixtures were updated to include `createdAtTime` (precedent: `02-alerts`). The CLI `List All` vehicle action passes the cancellation token by name (the 1st positional slot is now `attributes`). No JsonContext changes (`Vehicle`/`UpdateVehicleRequest` already registered; new props scalar/`DateTimeOffset`/array → no new top-level types).  
> **SDK Client**: `IVehiclesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../VehiclesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/vehicles`
**Operation ID**: `listVehicles`  
**Summary**: List all vehicles.  
**Parameters**: `limit`, `after`, `parentTagIds`, `tagIds`, `attributeValueIds`, `attributes`, `updatedAfterTime`, `createdAfterTime`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/vehicles/{id}`
**Operation ID**: `getVehicle`  
**Summary**: Retrieve a vehicle  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /fleet/vehicles/{id}`
**Operation ID**: `updateVehicle`  
**Summary**: Update a vehicle  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IVehiclesClient`
- [x] Method implemented in `VehiclesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/FleetModels.cs` for model definitions used by this domain.

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

**Model audit (2025-05-13):** `UpdateVehicleRequest` was missing a large number of fields present in the API.

- `UpdateVehicleRequest`: added `auxInputType3` through `auxInputType13`, `engineHours`, `grossVehicleWeight`, `gatewaySerial`, `vehicleType`, `attributes`, `odometerMeters`.
