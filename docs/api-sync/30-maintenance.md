# Maintenance — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🔴 Broken (3/9)  
> **⚠️ 2026-05-21 audit**: most DVIR/defect paths wrong: `/dvirs/stream`, `/dvirs/{id}`, `/defect-types`, `/defects/stream`, `/defects/{id}`. DVIRs are duplicated in `ComplianceClient`. `ListDtcsAsync` (`/fleet/vehicles/diagnostics`) is fabricated. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **Resolved 2026-05-27 (model-sync plan)**: applied [`model-sync-plan-2026-05-27/30-maintenance.md`](model-sync-plan-2026-05-27/30-maintenance.md) — 9 HIGH, 40 MEDIUM findings implemented; LOW spec-absent response extras retained as nullable back-compat per workflow precedent (08, 13, 14); spec-absent request fields on `UpdateDefectRequest` (`comment`, `resolvedAt`) replaced with the spec-aligned `mechanicNotes`, `resolvedAtTime`, `resolvedBy`.  
> **SDK Client**: `IMaintenanceClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../MaintenanceClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Maintenance/MaintenanceModels.cs`  

---

## Endpoints

### ❌ `GET /defect-types`
**Operation ID**: `getDefectTypes`  
**Summary**: Get DVIR defect types.  
**Parameters**: `after`, `limit`, `ids`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /defects/stream`
**Operation ID**: `streamDefects`  
**Summary**: Stream DVIR defects.  
**Parameters**: `after`, `limit`, `startTime`, `endTime`, `includeExternalIds`, `isResolved`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /defects/{id}`
**Operation ID**: `getDefect`  
**Summary**: Get a single DVIR defect by ID.  
**Parameters**: `id`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /dvirs/stream`
**Operation ID**: `getDvirs`  
**Summary**: Stream DVIRs  
**Parameters**: `after`, `limit`, `includeExternalIds`, `startTime`, `endTime`, `safetyStatus`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /dvirs/{id}`
**Operation ID**: `getDvir`  
**Summary**: Get a single DVIR by ID.  
**Parameters**: `id`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `PATCH /fleet/defects/{id}`
**Operation ID**: `updateDvirDefect`  
**Summary**: Update a defect  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `POST /fleet/dvirs`
**Operation ID**: `createDvir`  
**Summary**: Create a mechanic DVIR  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `PATCH /fleet/dvirs/{id}`
**Operation ID**: `updateDvir`  
**Summary**: Resolve a DVIR  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /v1/fleet/maintenance/list`
**Operation ID**: `V1getFleetMaintenanceList`  
**Summary**: Get vehicles with engine faults or check lights  
**Request Body**: No  

- [ ] Method defined in `IMaintenanceClient`
- [ ] Method implemented in `MaintenanceClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Maintenance/MaintenanceModels.cs` for model definitions used by this domain.

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

**2026-06-22 sync — preventive maintenance added (beta, loosely typed `object`):**

- `GET /maintenance/preventive/schedules` → `IMaintenanceClient.ListPreventiveMaintenanceSchedulesAsync` (paginated)
- `GET /maintenance/preventive/upcoming` → `IMaintenanceClient.ListUpcomingPreventiveMaintenanceAsync` (paginated)

`GET /maintenance/work-order-templates` was also added in the same sync — see
[`56-work-orders.md`](56-work-orders.md). All three follow the beta weak-typing convention.

**2026-08-17 — `MaintenanceDvirAssetRef` covers four spec schemas; split deferred, casing must not be "fixed":**

`MaintenanceDvirAssetRef` is one record standing in for four schemas, reached from four
properties — `MaintenanceDvir.Trailer`/`.Vehicle` and `DefectRecord.Trailer`/`.Vehicle`:

| Spec schema | Shape | Reached from |
|---|---|---|
| `trailerTinyResponse` | `{id, name}` — **no external IDs at all** | v1 `Dvir.trailer` (via `DvirTrailer` allOf), `Defect.trailer` |
| `vehicleTinyResponse` | `{ExternalIds, id, name}` — **capital E** | v1 `Dvir.vehicle` (via `DvirVehicle` allOf), `Defect.vehicle` |
| `TrailerDvirObjectResponseBody` = `DefectTrailerResponseResponseBody` | `{externalIds, id}` — **no name** | v2 `trailer` |
| `VehicleDvirObjectResponseBody` = `DefectVehicleResponseResponseBody` | `{externalIds, id}` — **no name** | v2 `vehicle` |

**Upstream typo.** `vehicleTinyResponse` is the **only** one of the 123 spec schemas carrying an
external-ID map that spells it `ExternalIds`; the other 122 use `externalIds` — including its own
siblings `VehicleDvirObjectResponseBody`, `GoaVehicleTinyResponseResponseBody` and
`VehicleWithGatewayTinyResponseResponseBody`. Within a single `Defect`, the `trailer` and `vehicle`
siblings disagree. Near-certainly a Samsara spec typo. It is harmless on reads because
`SamsaraJsonContext` sets `PropertyNameCaseInsensitive`, so either spelling binds.

(If you grep the spec for `ExternalIds` you get three hits, not one: `vehicleTinyResponse.properties`,
its verbatim inline copy under `dvirTrailerDefectsItems.vehicle.allOf[1]`, and a dead
`components.schemas.ExternalIds` alias of `VehicleExternalIds` that nothing `$ref`s. Only the first
two are property spellings.)

**Why the per-schema split was not done.** `MaintenanceDvir` is the response type for both the v1
`Dvir` schema (`POST /fleet/dvirs`, `PATCH /fleet/dvirs/{id}`) and the v2
`DvirStreamResponseDataResponseBody` (`GET /dvirs/stream`, `GET /dvirs/{id}`); `DefectRecord`
likewise covers v1 `Defect` (`PATCH /fleet/defects/{id}`) and v2
`DefectsResponseDataResponseBody`/`DvirDefectGetDefectResponseBody`. So there are four C# properties
for eight schema slots, and one property cannot have two types. Splitting the leaf per-schema
therefore requires first splitting those two parent records into v1/v2 pairs — which the 2026-08-17
spec-parity plan (§2.3) explicitly records as accepted dual shapes that "must not be 'fixed'". That
is a separate, larger decision; **it has not been taken.** Until it is, the union is the only shape
that loses no data: dropping `Name` blanks it on every v1 response, dropping `ExternalIds` blanks it
on every v2 response.

**Do not "correct" the casing.** Measured, not assumed: flipping
`MaintenanceDvirAssetRef.ExternalIds` to the capital-E spelling leaves `check-model-sync` at exactly
184 findings and merely moves the `missing-optional` finding from the v1 endpoints
(`POST /fleet/dvirs`, `PATCH /fleet/defects/{id}`) to the v2 ones (`GET /dvirs/stream`,
`GET /defects/stream`). Because the checker matches property names case-sensitively, **any** single
record serving both spellings produces exactly one such finding; the only way to zero is the parent
split above. The finding is MEDIUM, the gate is HIGH, and it is deliberately **not** allowlisted so
it stays visible.

`DvirDefectVehicle` mirrors the same `vehicleTinyResponse` shape but is reached only from the
v1-only `Dvir.trailerDefects[]`/`vehicleDefects[]` site, so it can and does spell the property
`ExternalIds` verbatim. It is not a duplicate to consolidate away — it is the one place the verbatim
spelling is safe.

Five contract tests in `tests/Samsara.Sdk.Tests/MaintenanceContractTests.cs` pin all four shapes so
a future split cannot silently drop a field.

**Model audit (2025-05-13):** DVIR models were completely wrong (v1 API fields). Both models fully replaced.

- `CreateDvirRequest`: replaced v1 fields (`inspectorName`, `odometer`, `safeToOperate`, `trailerIds`) with correct v2 fields: `authorId` (required), `safetyStatus` (required), `type` (required), plus optional `vehicleId`, `trailerId`, `licensePlate`, `location`, `mechanicNotes`, `odometerMeters`, `resolvedDefectIds`.
- `UpdateDvirRequest`: replaced v1 fields (`authorizedSignatoryId`, `safeToOperate`) with correct v2 fields: `authorId` (required), `isResolved` (required), plus optional `mechanicNotes`, `signedAtTime`.
