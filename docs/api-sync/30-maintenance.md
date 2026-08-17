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

**2026-08-17 (superseded, see the 2026-08-17b design note below) — `MaintenanceDvirAssetRef` covers four spec schemas; split deferred, casing must not be "fixed":**

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

---

**2026-08-17b — DESIGN NOTE: split the DVIR/Defect response records into v1/v2 pairs.**

**This decision reverses the 2026-08-17 note above and §2.3 of
[`spec-parity-plan-2026-08-17.md`](spec-parity-plan-2026-08-17.md).** Both recorded the DVIR/Defect
v1↔v2 dual shapes as accepted and "must not be 'fixed'". Approved reversal, 2026-08-17: **where the
spec defines two versions with different objects, the SDK is representative of that.** The older
notes are kept rather than deleted so the reasoning trail survives; they are historical, not
current guidance.

### The problem being fixed

Eight spec schemas were served by four C# records. Every one of those records was the *union* of a
v1 and a v2 shape, so no record could be faithful to either: `MaintenanceDvir` carried seven
properties the v2 schema does not define, `DefectRecord` carried two, and
`MaintenanceDvirAssetRef` had to pick one spelling of the external-ID map for two schemas that
spell it differently. That union is also invisible to `check-model-sync`, which compares each
record against both endpoints and sees every property accounted for by *one* of them — the blind
spot recorded in [`project_sync_checker_blindspot`](README.md).

### Schema resolution (verified via `responses.200 -> data/items -> $ref`)

| Endpoint | Envelope | Payload schema |
|---|---|---|
| `GET /dvirs/stream` | `DvirGetDvirsResponseBody` | `data[]` → **`DvirStreamResponseDataResponseBody`** |
| `GET /dvirs/{id}` | **`DvirGetDvirResponseBody`** (no `data` wrapper; property-identical to the stream item) | — |
| `POST /fleet/dvirs` | `DvirResponse` | `data` → **`Dvir`** |
| `PATCH /fleet/dvirs/{id}` | `DvirResponse` | `data` → **`Dvir`** |
| `GET /defects/stream` | `DvirDefectStreamDefectsResponseBody` | `data[]` → **`DefectsResponseDataResponseBody`** |
| `GET /defects/{id}` | **`DvirDefectGetDefectResponseBody`** (no `data` wrapper; property-identical to the stream item) | — |
| `PATCH /fleet/defects/{id}` | `DefectResponse` | `data` → **`Defect`** |
| `GET /defect-types` | `DvirDefectTypeGetDefectTypesResponseBody` | `data[]` → `DefectTypesResponseDataResponseBody` (v2 only, not split) |

### Naming scheme

The v1 record takes a **`V1` prefix** (repo precedent: `V1Trip`, `V1TripAddress`,
`V1PaginationInfo`, `V1TrailerAssignmentEntry`, `V1Sensor`). The **unprefixed name stays with the
v2 shape**, because v2 is the API surface new code should target and keeping the name there means
callers of the modern endpoints do not have to touch their code.

Schemas that are byte-identical are still served by **one** record — the split is per *shape*, not
per *schema name*. Three such families exist here and are called out in the inventory.

### Record inventory

**v2 records — unprefixed name retained, property set narrowed to the v2 schema**

| Record | Mirrors | Returned by | Change |
|---|---|---|---|
| `MaintenanceDvir` | `DvirStreamResponseDataResponseBody` ≡ `DvirGetDvirResponseBody` | `GET /dvirs/stream`, `GET /dvirs/{id}` | **drops** `endTime`, `startTime`, `licensePlate`, `location`, `trailerName`, `trailerDefects`, `vehicleDefects` (all v1-only); `Id` loses `required` |
| `MaintenanceDvirSignature` | `AuthorSignatureObjectResponseBody` | ″ | unchanged shape; `SignatoryUser` retypes to the v2 signatory record |
| `MaintenanceSignatoryUser` | `SignatoryUserObjectResponseBody` | ″ | **drops** `name` (v1-only) |
| `MaintenanceDvirAssetRef` | `TrailerDvirObjectResponseBody` ≡ `VehicleDvirObjectResponseBody` ≡ `DefectTrailerResponseResponseBody` ≡ `DefectVehicleResponseResponseBody` — all four `{externalIds, id}` | `GET /dvirs/stream`, `GET /dvirs/{id}`, `GET /defects/stream`, `GET /defects/{id}` | **drops** `name` (v1-only) |
| `DefectRecord` | `DefectsResponseDataResponseBody` ≡ `DvirDefectGetDefectResponseBody` | `GET /defects/stream`, `GET /defects/{id}` | **drops** `defectType`, `mechanicNotesUpdatedAtTime` (v1-only); `Id`/`IsResolved` lose `required` |
| `DefectPhoto` | `DefectPhotoResponseResponseBody` | `GET /defects/stream`, `GET /defects/{id}` | unchanged (v2-only, was never a union) |
| `WalkaroundPhoto` | `WalkaroundPhotoObjectResponseBody` | `GET /dvirs/stream`, `GET /dvirs/{id}` | unchanged (v2-only, was never a union) |
| `DefectResolvedBy` | `DvirResolvedByObjectResponseBody` **and** `Defect_resolvedBy` | all four defect/DVIR endpoints | **unchanged — not split.** Verified: the two schemas are `{id, name, type}` with identical spellings. Only the `required` list differs, which response records ignore. |

**v1 records — new**

| Record | Mirrors | Returned by | Property set |
|---|---|---|---|
| `V1MaintenanceDvir` | `Dvir` | `POST /fleet/dvirs`, `PATCH /fleet/dvirs/{id}` | `authorSignature`, `endTime`, `id`, `licensePlate`, `location`, `mechanicNotes`, `odometerMeters`, `safetyStatus`, `secondSignature`, `startTime`, `thirdSignature`, `trailer`, `trailerDefects`, `trailerName`, `type`, `vehicle`, `vehicleDefects` |
| `V1MaintenanceDvirSignature` | `DvirAuthorSignature` ≡ `DvirSecondSignature` ≡ `DvirThirdSignature` | ″ | `signatoryUser`, `signedAtTime`, `type` |
| `V1MaintenanceSignatoryUser` | `userTinyResponse` (as reached from `DvirAuthorSignature.signatoryUser`) | ″ | `id`, `name` |
| `V1MaintenanceVehicleRef` | `vehicleTinyResponse` ≡ `DvirVehicle` ≡ the inline `dvirTrailerDefectsItems.vehicle` allOf | `POST /fleet/dvirs`, `PATCH /fleet/dvirs/{id}`, `PATCH /fleet/defects/{id}` | **`ExternalIds`** (capital E — see below), `id`, `name` |
| `V1MaintenanceTrailerRef` | `trailerTinyResponse` ≡ `DvirTrailer` ≡ the inline `dvirTrailerDefectsItems.trailer` allOf | ″ | `id`, `name` — **no external-IDs property at all** |
| `V1DefectRecord` | `Defect` ≡ `dvirTrailerDefectsItems` | `PATCH /fleet/defects/{id}`; also nested at `V1MaintenanceDvir.trailerDefects[]` / `.vehicleDefects[]` | `comment`, `createdAtTime`, `defectType`, `id`, `isResolved`, `mechanicNotes`, `mechanicNotesUpdatedAtTime`, `resolvedAtTime`, `resolvedBy`, `trailer`, `vehicle` |

**Records removed**

| Removed | Superseded by | Why |
|---|---|---|
| `DvirDefect` | `V1DefectRecord` | `dvirTrailerDefectsItems` and `Defect` have the identical 11-property set with identical spellings — verified property by property. One record mirrors both. |
| `DvirDefectVehicle` | `V1MaintenanceVehicleRef` | It existed only because it was the one v1-only site where the capital-E spelling was safe. After the split *every* v1 vehicle site is v1-only, so the general v1 record carries the spelling and this one is redundant. This retires the 2026-08-17 note's claim that it "is not a duplicate to consolidate away". |

### The `ExternalIds` casing — resolved, not worked around

`vehicleTinyResponse` spells its external-ID map **`ExternalIds`**, with a capital E. It is the
**only** one of the 123 spec schemas carrying such a map that does — the other 122 use
`externalIds`, including its own siblings `VehicleDvirObjectResponseBody`,
`GoaVehicleTinyResponseResponseBody` and `VehicleWithGatewayTinyResponseResponseBody`, and
including `trailerTinyResponse`'s v2 counterpart. Within a single `Defect`, the `trailer` and
`vehicle` siblings disagree with each other. This is near-certainly an upstream typo in Samsara's
spec.

The SDK **copies the spec verbatim**: `V1MaintenanceVehicleRef.ExternalIds` carries
`[JsonPropertyName("ExternalIds")]` and `MaintenanceDvirAssetRef.ExternalIds` carries
`[JsonPropertyName("externalIds")]`. Because each record now serves exactly one spelling, this is
possible for the first time — and it is what removes the standing `check-model-sync`
`missing-optional` finding on `MaintenanceDvirAssetRef.ExternalIds`, **resolved rather than
allowlisted**.

**Do not "correct" the capital E.** A `<remarks>` block on the record says so; it is there because
the spelling looks like a bug and a future reader will otherwise silently revert it. Reads would
survive a revert (the serializer sets `PropertyNameCaseInsensitive`), which is exactly what makes
the regression invisible — the checker finding would come back and nothing else would break.

`V1MaintenanceTrailerRef` has **no** external-IDs property, because `trailerTinyResponse` defines
none. Do not add one for symmetry.

### Public API breaks

1. `IMaintenanceClient.CreateDvirAsync` returns `Task<V1MaintenanceDvir>` (was `Task<MaintenanceDvir>`).
2. `IMaintenanceClient.UpdateDvirAsync` returns `Task<V1MaintenanceDvir>` (was `Task<MaintenanceDvir>`).
3. `IMaintenanceClient.UpdateDefectAsync` returns `Task<V1DefectRecord>` (was `Task<DefectRecord>`).
4. `MaintenanceDvir` loses `EndTime`, `StartTime`, `LicensePlate`, `Location`, `TrailerName`, `TrailerDefects`, `VehicleDefects`.
5. `MaintenanceDvir.Id` becomes `string?` (`required` removed).
6. `DefectRecord` loses `DefectType`, `MechanicNotesUpdatedAtTime`.
7. `DefectRecord.Id` becomes `string?` and `DefectRecord.IsResolved` becomes `bool?` (`required` removed).
8. `MaintenanceDvirAssetRef` loses `Name`.
9. `MaintenanceSignatoryUser` loses `Name`.
10. `MaintenanceDvir.Trailer`/`.Vehicle` and `DefectRecord.Trailer`/`.Vehicle` keep their type; the v1 equivalents are typed `V1MaintenanceTrailerRef`/`V1MaintenanceVehicleRef`.
11. Records `DvirDefect` and `DvirDefectVehicle` are removed.

`required` is removed from every response property in this closure per the standing rule: **response
records stay fully nullable.** The live API omits fields its own spec marks required, and `required`
on a response record has crashed deserialization before.

### Test coverage

`tests/Samsara.Sdk.Tests/MaintenanceContractTests.cs` keeps its five asset-ref contract tests,
retargeted at the new types, and gains per-version binding tests: a v1 vehicle payload spelling the
map `ExternalIds` must populate `V1MaintenanceVehicleRef.ExternalIds`, and a v2 payload spelling it
`externalIds` must populate `MaintenanceDvirAssetRef.ExternalIds`.

---

**Model audit (2025-05-13):** DVIR models were completely wrong (v1 API fields). Both models fully replaced.

- `CreateDvirRequest`: replaced v1 fields (`inspectorName`, `odometer`, `safeToOperate`, `trailerIds`) with correct v2 fields: `authorId` (required), `safetyStatus` (required), `type` (required), plus optional `vehicleId`, `trailerId`, `licensePlate`, `location`, `mechanicNotes`, `odometerMeters`, `resolvedDefectIds`.
- `UpdateDvirRequest`: replaced v1 fields (`authorizedSignatoryId`, `safeToOperate`) with correct v2 fields: `authorId` (required), `isResolved` (required), plus optional `mechanicNotes`, `signedAtTime`.
