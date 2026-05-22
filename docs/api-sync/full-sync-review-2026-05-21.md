# Samsara .NET SDK — Full API Sync Review

> **Generated**: 2026-05-21
> **Spec**: https://developers.samsara.com/openapi/samsara-api.json
> **Spec `info.version`**: `2025-10-23` (note: spec content has drifted since the cached baseline of the same version label — see Part 4)
> **Status**: PROPOSED CHANGES — for review. **No SDK code has been modified.**

This document is the result of a full mechanical comparison of `Samsara.Sdk` against the
current OpenAPI spec at the endpoint level (HTTP verb + URL path + parameters) and the
model level (record properties vs. schema properties). It supersedes the per-domain
status in `docs/api-sync/README.md`, several entries of which are inaccurate (see Part 5).

---

## Executive Summary

| Dimension | Spec | SDK | Notes |
|---|---|---|---|
| Operations (verb+path) | **311** | — | across 221 paths |
| Distinct SDK endpoints wired | — | **209** | — |
| SDK endpoints that **match** the spec | — | **~142** | correctly wired |
| SDK endpoints **wrong path** (fixable by URL change) | — | **~40** | currently return 404 |
| SDK endpoints **fabricated / needs rework** (no spec op) | — | **~24** | return 404 / 405 |
| Spec operations **not implemented** | **~170** | — | coverage gaps |
| Name-matched models with property drift | — | **29 / 36** | lower bound (see Part 2) |

**Headline:** roughly **one in three wired SDK endpoints does not match the current spec**
and will fail at runtime (404 Not Found, or 400/silent-null for the body/shape mismatches).
Several domains marked "✅ Complete" in `README.md` — **Tags, Gateways, Media, Messages,
Sensors, Tachograph, Fuel and Energy** — are in fact almost entirely mis-wired. The
prior "model audit" (commits `dd6678f`, `3ccea15`) corrected model *properties* but never
verified *URL paths*, which is where most of the breakage is.

**Severity legend used below**
- 🔴 **P1 — Broken**: endpoint/path/required-field is wrong; guaranteed runtime failure.
- 🟠 **P2 — Silent data loss / shape**: deserializes but drops or mis-maps data.
- 🟡 **P3 — Coverage gap**: spec endpoint not implemented (no regression, just missing).
- ⚪ **P4 — Cosmetic**: stricter-than-spec nullability, enum-as-string, docs.

---

## Methodology & Caveats

- Endpoints were extracted from `src/Samsara.Sdk/Clients/**/*Client.cs` by parsing every
  `HttpClient.{Verb}Async(...)` and `Paginate*Async(...)` call, resolving `BasePath`
  constants and string interpolation to a normalized `/path/{}` form, then matched against
  the spec's `paths`. The base address is `https://api.samsara.com/` (from
  `SamsaraClientOptions.DefaultBaseUrl`) and paths are sent **relative with no rewriting**
  (`SamsaraHttpClient.SendAndValidateAsync`), so the relative path *is* the spec path.
- Models were extracted from `src/Samsara.Sdk/Models/**/*.cs` (record name + every
  `[JsonPropertyName]`), and compared to the like-named spec schema.
- **Caveat 1 (models, lower bound):** only **36 of 212** SDK records share an exact name
  with a spec schema, so the property audit in Part 2 is a *lower bound*. Request/response
  records whose names don't match a schema (e.g. `VehicleStats`, `CreateEquipmentRequest`)
  were not auto-diffed and need a manual pass during implementation.
- **Caveat 2 (deliberate flattening):** some "EXTRA in SDK" properties are the SDK author
  intentionally flattening a nested spec object (e.g. `driver{id,name}` → `driverId`,
  `driverName`). These still **diverge from the wire shape** and cause silent nulls, so
  they are reported, but the fix is a design decision (flatten via converter vs. model the
  nested object).
- **Caveat 3 (v1/v2 name collision — important):** a few SDK record names collide with a
  *legacy v1* spec schema of the same name while the SDK actually calls a *v2* endpoint that
  returns a differently-named schema. Each Part 2 row below was re-verified against the
  schema the SDK's endpoint *actually* returns:
  - `SafetyEvent` → real v2 schema `SafetyEventV2ObjectResponseBody` (not v1 `SafetyEvent`).
  - `HosClocks` → wrapped as `HosClocksForDriver[]` (each `{ driver, clocks, currentDutyStatus, currentVehicle, violations }`, `clocks` = the nested object).
  - `TachographActivity` → wrapped as `{ driver, activity: TachographActivity[] }`.
  Models that name-match their endpoint's real schema **exactly** (Driver, Tag, Address,
  Contact, Equipment, Vehicle, CarrierProposedAssignment, User) and all request-body
  schemas (verified) are reliable.
- The raw machine output backing this review is reproducible; the comparison was done with
  ad-hoc scripts (not committed). `tools/check-api-sync.py` remains the spec-vs-baseline
  drift tool (Part 4); it does **not** check SDK paths and should be extended (Part 6).

---

## Part 1 — Endpoint Defects (P1 / P2)

### 1a. 🔴 Wrong path — fixable by changing the URL only

These methods exist and have correct verbs/models; only the URL string is wrong. Each row
gives the current SDK path and the correct spec path + `operationId`.

#### Tags — `TagsClient` (`BasePath` `fleet/tags` → `tags`)
README says "🟡 Partial 5/6"; actual matched **0/6**. Remove the `fleet/` prefix on all six.

| Method | SDK path (wrong) | Correct spec path | operationId |
|---|---|---|---|
| `ListAsync` | `GET fleet/tags` | `GET /tags` | `listTags` |
| `GetAsync` | `GET fleet/tags/{id}` | `GET /tags/{id}` | `getTag` |
| `CreateAsync` | `POST fleet/tags` | `POST /tags` | `createTag` |
| `UpdateAsync` | `PATCH fleet/tags/{id}` | `PATCH /tags/{id}` | `patchTag` |
| `ReplaceAsync` | `PUT fleet/tags/{id}` | `PUT /tags/{id}` | `replaceTag` |
| `DeleteAsync` | `DELETE fleet/tags/{id}` | `DELETE /tags/{id}` | `deleteTag` |

#### Gateways — `GatewaysClient` (`fleet/gateways` → `gateways`)
README "✅ Complete 3/3"; actual **0/3**.

| Method | SDK path | Correct spec path | Note |
|---|---|---|---|
| `ListAsync` | `GET fleet/gateways` | `GET /gateways` (`getGateways`) | fix prefix |
| `GetAsync` | `GET fleet/gateways/{id}` | — | **no get-by-id in spec** → see 1b |

Also **missing**: `POST /gateways` (`postGateway`), `DELETE /gateways/{id}` (`deleteGateway`).

#### Media — `MediaClient`
README "🟡 Partial 1/3"; actual **0/3**. Spec uses `/cameras/media`.

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `ListAsync` | `GET fleet/media` | `GET /cameras/media` | `listUploadedMedia` |
| `GetRetrievalAsync` | `GET media/retrievals/{id}` | `GET /cameras/media/retrieval` (retrievalId as query) | `getMediaRetrieval` |
| `CreateRetrievalAsync` | `POST media/retrievals` | `POST /cameras/media/retrieval` | `postMediaRetrieval` |
| `GetAsync` | `GET fleet/media/{id}` | — | **no get-by-id** → see 1b |

#### Messages — `MessagesClient` (`fleet/messages` → `v1/fleet/messages`)
README "✅ Complete 2/2"; actual **0/2**. These are v1 endpoints.

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `ListAsync` | `GET fleet/messages` | `GET /v1/fleet/messages` | `V1getMessages` |
| `SendAsync` | `POST fleet/messages` | `POST /v1/fleet/messages` | `V1createMessages` |

#### Forms — `FormsClient` (mixed; 3 of 6 wrong)
README "🟡 Partial 4/7". The newer methods are correct; the older three are not.

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `ListTemplatesAsync` | `GET fleet/forms/templates` | `GET /form-templates` | `getFormTemplates` |
| `ListSubmissionsAsync` | `GET fleet/forms/submissions` | `GET /form-submissions` | `getFormSubmissions` |
| `GetSubmissionAsync` | `GET fleet/forms/submissions/{id}` | `GET /form-submissions` (id as query) | `getFormSubmissions` |
| `GetSubmissionsStreamAsync` | `form-submissions/stream` | ✅ correct | `getFormSubmissionsStream` |
| `GetPdfExportsAsync` | `form-submissions/pdf-exports` | ✅ correct | `getFormSubmissionsPdfExports` |
| `CreatePdfExportAsync` | `POST form-submissions/pdf-exports` | ✅ correct | `postFormSubmissionsPdfExports` |

Missing: `PATCH /form-submissions` (`patchFormSubmission`), `POST /form-submissions` (`postFormSubmission`).

#### Tachograph — `TachographClient` (missing `/history` suffix)
README "✅ Complete 3/3"; actual **0/3**.

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `ListActivitiesAsync` | `GET fleet/drivers/tachograph-activity` | `GET /fleet/drivers/tachograph-activity/history` | `getDriverTachographActivity` |
| `ListFilesAsync` | `GET fleet/drivers/tachograph-files` | `GET /fleet/drivers/tachograph-files/history` | `getDriverTachographFiles` |

Missing: `GET /fleet/vehicles/tachograph-files/history` (`getVehicleTachographFiles`).

#### Training — `TrainingClient` (`fleet/training/*` → `training-*`)
README "✅ Complete" for both; actual **0/4** and **0/1**.

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `ListAssignmentsAsync` | `GET fleet/training/assignments` | `GET /training-assignments/stream` | `getTrainingAssignmentsStream` |
| `ListCoursesAsync` | `GET fleet/training/courses` | `GET /training-courses` | `getTrainingCourses` |

Missing (TrainingAssignments): `POST`, `PATCH`, `DELETE /training-assignments`.

#### Industrial — `IndustrialClient` (`industrial/data` → `industrial/data-inputs`)

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `ListDataInputsAsync` | `GET industrial/data` | `GET /industrial/data-inputs` | `getDataInputs` |
| `GetDataInputAsync` | `GET industrial/data/{id}` | `GET /industrial/data-inputs` (id as query) | `getDataInputs` |

(`ListAssetsAsync`, `GetDataInput{Snapshot,Feed,History}Async` are correct.)

#### Compliance — `ComplianceClient`

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `GetHosClocksAsync` | `GET fleet/drivers/{id}/hos/clocks` | `GET /fleet/hos/clocks` (driverIds as query) | `getHosClocks` |
| `ListHosEldEventsAsync` | `GET fleet/hos/eld-events` | `GET /beta/fleet/hos/drivers/eld-events` | `getHosEldEvents` |
| `ListDvirsAsync` | `GET fleet/dvirs` | `GET /dvirs/stream` | `getDvirs` |
| `GetDvirAsync` | `GET fleet/dvirs/{id}` | `GET /dvirs/{id}` | `getDvir` |

> **Note — DVIR duplication:** DVIRs are implemented in **both** `ComplianceClient` and
> `MaintenanceClient`. Recommend consolidating into one (Maintenance) during implementation.

#### Maintenance — `MaintenanceClient`

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `ListDefectTypesAsync` | `GET fleet/defect-types` | `GET /defect-types` | `getDefectTypes` |
| `GetDefectsStreamAsync` | `GET fleet/defects/stream` | `GET /defects/stream` | `streamDefects` |
| `GetDefectAsync` | `GET fleet/defects/{id}` | `GET /defects/{id}` | `getDefect` |
| `GetDvirsStreamAsync` | `GET fleet/dvirs/stream` | `GET /dvirs/stream` | `getDvirs` |
| `GetDvirByIdAsync` | `GET fleet/dvirs/{id}` | `GET /dvirs/{id}` | `getDvir` |
| `ListDvirsAsync` | `GET fleet/maintenance/dvirs` | `GET /dvirs/stream` | `getDvirs` (duplicate) |
| `GetDvirAsync` | `GET fleet/maintenance/dvirs/{id}` | `GET /dvirs/{id}` | `getDvir` (duplicate) |

(`CreateDvirAsync` `POST /fleet/dvirs`, `UpdateDvirAsync` `PATCH /fleet/dvirs/{id}`,
`UpdateDefectAsync` `PATCH /fleet/defects/{id}` are correct.)

#### IFTA — `IftaClient`

| Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| `CreateDetailJobAsync` | `POST fleet/reports/ifta/detail/jobs` | `POST /ifta-detail/csv` | `createIftaDetailJob` |
| `GetDetailJobAsync` | `GET fleet/reports/ifta/detail/jobs/{id}` | `GET /ifta-detail/csv/{id}` | `getIftaDetailJob` |
| `ListDetailsAsync` | `GET fleet/reports/ifta/detail` | — rework → `GET /fleet/reports/ifta/vehicle` | `getIftaVehicleReports` |
| `GetSummaryAsync` | `GET fleet/reports/ifta/summary` | — **no equivalent** → see 1b | — |

Missing: `GET /fleet/reports/ifta/jurisdiction` (`getIftaJurisdictionReports`).

#### Other single-method path fixes

| Domain / Method | SDK path | Correct spec path | operationId |
|---|---|---|---|
| Routes `GetAuditLogAsync` | `GET routes/audit-log` | `GET /fleet/routes/audit-logs/feed` | `getRoutesFeed` |
| Trips `ListAsync` | `GET fleet/vehicles/trips` | `GET /v1/fleet/trips` | `V1getFleetTrips` |
| TrailerAssignments `ListAsync` | `GET fleet/trailer-assignments` | `GET /v1/fleet/trailers/assignments` | `V1getAllTrailerAssignments` |
| Equipment `UpdateAsync` | `PATCH fleet/equipment/{id}` | `PATCH /beta/fleet/equipment/{id}` | `patchEquipment` |
| Issues `GetAsync` | `GET issues/{id}` | `GET /issues` (id as query) | `getIssues` |
| Issues `UpdateAsync` | `PATCH issues/{id}` | `PATCH /issues` (id in body) | `patchIssue` |
| Alerts `UpdateConfigurationAsync` | `PATCH alerts/configurations/{id}` | `PATCH /alerts/configurations` (no id in path) | `patchConfigurations` |
| DriverVehicleAssignments `UpdateAsync` | `PATCH fleet/driver-vehicle-assignments/{id}` | `PATCH /fleet/driver-vehicle-assignments` (no id in path) | `updateDriverVehicleAssignment` |
| DriverVehicleAssignments `DeleteAsync` | `DELETE fleet/driver-vehicle-assignments/{id}` | `DELETE /fleet/driver-vehicle-assignments` (no id in path) | `deleteDriverVehicleAssignments` |

### 1b. 🔴 Fabricated / needs-rework operations (no matching spec op)

These methods call endpoints that **do not exist** in the spec. Recommended action per row.

| Domain / Method | SDK call | Recommendation |
|---|---|---|
| Drivers `DeleteAsync` | `DELETE /fleet/drivers/{id}` | **Remove.** No `deleteDriver`; deactivate via `PATCH /fleet/drivers/{id}` with `driverActivationStatus`. |
| Vehicles `CreateAsync` | `POST /fleet/vehicles` | **Remove.** Spec has no `createVehicle` (vehicles provisioned via gateway). |
| Vehicles `DeleteAsync` | `DELETE /fleet/vehicles/{id}` | **Remove.** No `deleteVehicle`. |
| Equipment `CreateAsync` | `POST /fleet/equipment` | **Remove.** No `createEquipment`. |
| Equipment `DeleteAsync` | `DELETE /fleet/equipment/{id}` | **Remove.** No `deleteEquipment`. |
| CarrierProposedAssignments `UpdateAsync` | `PATCH /fleet/carrier-proposed-assignments/{id}` | **Remove.** Spec only has list/create/delete. |
| Gateways `GetAsync` | `GET /fleet/gateways/{id}` | **Remove** (no get-by-id) and add create/delete instead. |
| Media `GetAsync` | `GET /fleet/media/{id}` | **Remove** (no get-by-id; use `listUploadedMedia`). |
| Maintenance `ListDtcsAsync` | `GET /fleet/vehicles/diagnostics` | **Rework.** No diagnostics path; DTCs come via `GET /fleet/vehicles/stats?types=faultCodes...`. |
| Safety `GetEventAsync` | `GET /safety-events/{id}` | **Remove** (no get-by-id; use `getSafetyEventsV2` list/stream). |
| IFTA `GetSummaryAsync` | `GET /fleet/reports/ifta/summary` | **Remove / rework** to jurisdiction + vehicle reports. |
| **Alerts (top-level CRUD)** | `GET/POST /alerts`, `GET/PATCH/DELETE /alerts/{id}` | **Remove all.** Spec only exposes `/alerts/configurations` (+ `/alerts/incidents/stream`). |
| Alerts `ListIncidentsAsync` | `GET /alerts/incidents` | **Rework** → `GET /alerts/incidents/stream` (`getIncidents`). |
| Alerts `GetIncidentAsync` | `GET /alerts/incidents/{id}` | **Remove** (no get-by-id). |
| **Sensors (entire client)** | `GET /sensors`, `GET /sensors/{id}`, `POST /sensors/history` | **Rework.** Spec sensors are v1 POST endpoints: `POST /v1/sensors/list`, `/cargo`, `/door`, `/humidity`, `/temperature`, `/history`. There is no list/get-by-id. |
| **Fuel (entire client)** | `GET /fleet/vehicles/fuel/purchases`, `/energy-levels` | **Rework.** Spec: `GET /fleet/reports/vehicles/fuel-energy` (`getFuelEnergyVehicleReports`), `GET /fleet/reports/drivers/fuel-energy`, `POST /fuel-purchase`, plus driver-efficiency reports. |

Also missing alerts op: `DELETE /alerts/configurations` (`deleteConfigurations`).

### 1c. New optional query params on implemented endpoints (P3, additive)

- `GET /assets` gained **`includeAttributes`** (query, optional) — `AssetsClient.ListAsync` should expose it.
- `GET /issues/stream` gained **`assetExternalIds`** (query, optional) — `IssuesClient.GetStreamAsync` should expose it.

---

## Part 2 — Model / Property Drift

Auto-diff of the **36 name-matched** records (lower bound — see Methodology). Full machine
output is large; the high-value clusters:

### 2a. 🔴 Request bodies: `required` fields modelled as optional (cause 400s)

The SDK lets callers omit fields the API requires. Add `required` (or make non-nullable).

| Record (file) | Field(s) the spec requires but SDK marks optional |
|---|---|
| `CreateDriverRequest` (Drivers) | `username` |
| `CreateUserRequest` (Organization) | `authType`, `roles` |
| `CreateAddressRequest` (Addresses) | `formattedAddress`, `geofence` |
| `CreateAttributeRequest` (Tags) | `attributeType` |
| `UpdateAttributeRequest` (Tags) | `entityType` |

### 2b. 🟠 Response models with divergent JSON shape (deserialize to silent nulls)

The SDK flattened a nested spec object into scalar fields; the wire shape differs, so the
flattened fields stay `null`. Fix by modelling the nested object (or a custom converter).

| Record | Spec shape | SDK shape (wrong) |
|---|---|---|
| `HosClocks` (Compliance) | response is `HosClocksForDriver[]`; each item `{ driver, clocks{ break, cycle, drive, shift }, currentDutyStatus, currentVehicle, violations }` | single flat record with `drivingTimeMs`, `dutyTimeMs`, `cycleTimeMs`, `shift*Ms`, `driverId` — wrong cardinality *and* shape |
| `CarrierProposedAssignment` (Assignments) | nested `driver`, `vehicle`, `trailers[]`, `activeTime`, `shippingDocs`, `*Time` | flat `driverId/driverName/vehicleId/vehicleName/shippingId/startTime/endTime/status` |
| `CreateCarrierProposedAssignmentRequest` | `activeTime`, `shippingDocs`, `trailerIds[]`, `trailerNames[]` | flat `shippingId/startTime/endTime` (won't be accepted) |
| `TachographActivity` (Compliance) | response wrapped `{ driver, activity: TachographActivity[] }`; activity item has `state` (enum), `isManualEntry` | flat record with `driverId/vehicleId/activityType/durationMs/country/region` baked in (driver belongs on the wrapper) |
| `DvirSignature` (Compliance) | `signatoryUser` (object), `signedAtTime` | flat `driverId/name/email/signedAtMs` |
| `SafetyEvent` (Safety) | v2 `SafetyEventV2ObjectResponseBody`: `asset` (not `vehicle`), `behaviorLabels[]` (objects), `startMs`/`endMs`/`createdAtTime`, `eventState`, `location`, `maxAccelerationGForce`, `media[]`, `contextLabels[]`, `inboxEventUrl`, … | minimal stub `{ id, time, behaviorLabels: string[], vehicle{id,name}, driver{id,name} }` — `time` not in v2, `behaviorLabels` should be objects, `vehicle` should be `asset`, ~15 fields missing |
| `Sensor` (Industrial) | v1 sensors return an inline (`inline_response_200_*`) schema, not a `data` envelope | SDK `Sensor` (`macAddress/model/serialNumber/tags`) is moot — the whole client is mis-pathed (see 1b); rebuild models with the v1 endpoints |

### 2c. 🟠 Missing response properties (data the API returns but the SDK drops)

| Record | Missing properties (spec) |
|---|---|
| `Vehicle` (Fleet) | `attributes`, `vehicleType`, `esn`, `cameraSerial`, `grossVehicleWeight`, `sensorConfiguration`, `auxInputType3..13` |
| `Address` (Addresses) | `contacts[]`, `createdAtTime`; **extra** `contactIds` not in spec |
| `Tag` (Tags) | `externalIds`, `parentTag` |
| `Equipment` (Fleet) | `assetSerial`, `installedGateway`; **extra** `equipmentSerialNumber` not in spec |
| `EquipmentLocation` / `VehicleLocation` | `reverseGeo`, `heading`, `speed`; `latitude`/`longitude`/`time` should be required |
| `AttributeEntity` (Tags) | `entityId`, `values[]`, `dateValues[]`; **extra** `id` not in spec |
| `DriverCarrierSettings` (Drivers) | `homeTerminalAddress`, `homeTerminalName` |

### 2d. ⚪ Stricter-than-spec nullability on response IDs (low risk, optional)

`Driver`, `Tag`, `User`, `UserRole`, `OrganizationInfo` mark `id`/`name` as `required`
while the spec lists them optional. Harmless for responses in practice; align only if you
want strict spec parity. `Contact` / `User` mark `firstName/lastName/email/phone` /
`name/email/authType/roles` optional while spec requires them (matters for the *create*
paths). `OrganizationInfo` has extra `address/city/state/zip/country` not in the spec.

---

## Part 3 — Coverage Gaps (P3, unimplemented spec operations)

~170 of 311 operations are unimplemented. Net coverage after correcting the Part 1 mis-wirings
is roughly **142/311 (46%)**, not the 37% in `README.md` (the README counts some
mis-wired endpoints as implemented and undercounts a few helper-built paths). Notable gaps
in already-touched domains:

- **Assets** (4/8): missing `GET /v1/fleet/assets/locations`, `/reefers`, `/{id}/locations`, `/{id}/reefer`.
- **Hours of Service** (3/6): `logs`/`violations`/`daily-logs` are wired; missing
  `POST /v1/fleet/drivers/{id}/hos/duty_status` (`setCurrentDutyStatus`) and
  `GET /v1/fleet/hos_authentication_logs` (`V1getFleetHosAuthenticationLogs`). (`clocks` and
  `eld-events` exist but are wrong-path — see 1a.)
- **Work Orders** is fully wired (7/7, incl. `getServiceTasks`, `postInvoiceScan`).
- **Contacts** (2/5), **Driver-Vehicle Assignments** (2/4), **Routes** (5/8), **Hubs** (6/7), **Safety** (2/4).
- **Entirely unimplemented domains**: Beta APIs (0/70, incl. the new **Places**), Legacy/Legacy APIs (0/9), Preview APIs (0/4), Plans (0/3), Readings… (Readings is actually 3/3 — README wrong), Route Events (0/1).

A full per-operation gap list is derivable from `/tmp/spec-by-tag.txt` (regenerate with the
spec dump in Part 6); the per-domain checklist files should be regenerated from the live spec.

---

## Part 4 — Spec Drift Since Cached Baseline

`.github/cache/samsara-api-baseline.json` (cached 2026-05-13) carries the same
`info.version` (`2025-10-23`) but the live spec has changed: **221 paths (was 220)** and
**3831 schemas (was 3764, +67)**. `tools/check-api-sync.py` reports:

- 🆕 **New "Places" beta domain** (4 ops): `GET/POST/PATCH/DELETE /places` (`getPlaces`,
  `postPlace`, `patchPlace`, `deletePlace`) + ~60 supporting `Place*`/`Hub*` schemas.
- 🔄 `GET /assets` +`includeAttributes`; `GET /issues/stream` +`assetExternalIds` (see 1c).

**Action:** refresh the baseline (`python3 tools/check-api-sync.py --update-baseline`) once
these are reviewed, so future weekly drift checks are accurate.

---

## Part 5 — Documentation / Process Accuracy

`docs/api-sync/README.md` statuses are unreliable because the checklists were filled by
intent, not verified against live paths. Concretely wrong entries to correct after the fix:

| Domain | README says | Reality (matched) |
|---|---|---|
| Tags | 🟡 5/6 | 0/6 (all wrong path) |
| Gateways | ✅ 3/3 | 0/3 |
| Media | 🟡 1/3 | 0/3 |
| Messages | ✅ 2/2 | 0/2 |
| Sensors | ✅ 6/6 | 0/6 (wrong + only 3 modelled) |
| Tachograph | ✅ 3/3 | 0/3 |
| Fuel and Energy | ✅ 5/5 | 0/5 |
| Training Assignments / Courses | ✅ 4/4, 1/1 | 0/4, 0/1 |
| Readings | ⚠️/partial | **3/3 (already correct)** |
| Equipment | 🟡 4/8 | 7/8 wired (+ 2 fabricated, 1 beta-path) |

Recommend regenerating every `NN-*.md` checklist from the live spec as part of the fix so
the checklist files become trustworthy.

---

## Part 6 — Proposed Action Plan (in priority order)

1. **P1 path fixes (1a):** mechanical URL corrections across Tags, Gateways, Media,
   Messages, Forms, Tachograph, Training, Industrial, Compliance, Maintenance, IFTA, Routes,
   Trips, TrailerAssignments, Equipment, Issues, Alerts, DriverVehicleAssignments. Low risk,
   high impact. Add/repair integration tests that assert the request URL per method.
2. **P1 fabricated ops (1b):** remove `Delete/Create` methods with no spec backing
   (Drivers.Delete, Vehicles.Create/Delete, Equipment.Create/Delete,
   CarrierProposedAssignments.Update, Alerts top-level CRUD, get-by-id where unsupported).
   These are **breaking API changes** to the SDK surface — bundle into one minor/major bump
   and note in `CHANGELOG.md`.
3. **Rework domains:** Sensors (→ v1 POST), Fuel (→ reports), Alerts (→ configurations +
   incidents stream), Industrial data-inputs. New models required.
4. **P1 request-required (2a)** then **P2 shape/missing (2b/2c):** correct request bodies,
   then model the nested response objects; verify `SamsaraJsonContext.cs` registrations.
5. **Coverage (Part 3):** prioritize gaps in active domains (Assets v1 locations, HOS,
   Contacts, Routes, DVA) and decide on Places/Beta.
6. **Process:** refresh baseline (Part 4); regenerate checklists from live spec (Part 5);
   extend `tools/check-api-sync.py` (or add a new check) to diff **SDK paths vs spec**, not
   just spec vs baseline, so this class of bug is caught in CI going forward.

> Suggested sequencing: land **Step 1** first as an isolated PR (pure path strings, easy to
> review and verify), then **Step 2** as a clearly-labelled breaking-change PR, then the
> model work per domain.
