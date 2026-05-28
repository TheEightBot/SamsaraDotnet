# Samsara .NET SDK — Model-Level Sync Plan (2026-05-27)

> Plan documents only. No SDK code, tests, or CLI files were touched while producing this audit.  
> Spec audited: local `samsara-api.json` version `2025-10-23` (paths: 223, components.schemas: 3921).  
> SDK audited: `src/Samsara.Sdk/` on branch `feature/api-sync-full-2026-05`.

## Background

Endpoint-level parity is already complete (`tools/check-sdk-sync.py` reports 0 mismatches / 0 missing). This audit goes one level deeper — it parses every `*Client.cs` method, resolves the spec operation by (verb, normalized path), then compares the C# request / response / query types against the spec schema **property by property**. Findings are deduplicated by `(sdk_type, property, context)` so a single missing field on a record returned by 12 endpoints counts once but lists all affected endpoints.

## Methodology

1. **Parse SDK clients** (`src/Samsara.Sdk/Clients/**/*Client.cs`):
   - Extract every `public *Async` method and walk its body to find the terminal `HttpClient.{Get|Post|Patch|Put|Delete}{,Data}Async` or `Paginate{,Data}Async` call.
   - Resolve the path argument by substituting `{BasePath}`, class-level `const string` aliases, and local `var` assignments; strip query strings and `{...}` placeholders.
   - Collect the method's C# parameter list (name, type, has-default).
   - Extract the request body argument identifier (second positional arg) and look up its C# type from the parameter list.
   - Extract the generic type argument of the helper (response record).
2. **Parse SDK models** (`src/Samsara.Sdk/Models/**/*.cs`): for each `public sealed record TypeName { ... }`, parse properties — JsonPropertyName, C# type string, `required` modifier, nullability.
3. **Match SDK ↔ spec by endpoint, not by name.** This handles cases where the SDK record name differs from the spec schema name (e.g. SDK `SafetyEvent` for v2 endpoint actually maps to `SafetyEventV2ObjectResponseBody`).
4. **Unwrap spec envelopes.** Most spec request/response bodies wrap the payload in `{ data: T }` (single) or `{ data: T[] }` (list, often with `pagination`). The audit unwraps this one layer (and handles double-wraps like `{ data: { vehicleReports: [...] } }`) so the SDK record is compared against the inner T.
5. **Compare properties.** For each spec property, check that the SDK record has a matching `JsonPropertyName`. Compare C# types against spec `type`/`format` (`string` → `string`, `integer` → `int`/`long`, `number` → `double`, `boolean` → `bool`, `date-time` → `DateTimeOffset`, arrays → `IReadOnlyList<>`). Check `required` modifier vs spec `required`.
6. **Compare query parameters.** For each spec query parameter, look for a matching SDK method parameter by name (camel/Pascal). Pagination params (`limit`, `after`, etc.) are skipped if the endpoint uses `PaginateAsync`. Time-range params (`startTime`/`endTime`) are skipped if the endpoint uses `QueryBuilder.WithTimeRange`. Methods that take a `Filter`/`QueryBuilder` parameter are considered to delegate query construction and are skipped.

### Severity scheme

| Severity | What it means |
|---|---|
| **CRITICAL** | Request will fail at runtime as written (wrapper-shape mismatch — SDK posts raw record where spec expects `{ data: T }` or `{ data: T[] }`). |
| **HIGH** | Missing spec-required field in a request body, missing spec-required query parameter, or required-ness drift on a request property. Response shapes that drop spec-required fields are also HIGH (incoming JSON has the field but the SDK ignores or fails to deserialize a guaranteed value). |
| **MEDIUM** | Missing optional property, type mismatch (e.g. spec `number` but SDK `string?`), weak `object?` typing where a concrete model exists in spec, response-shape drift on optional fields. Beta-tagged endpoints are downgraded to MEDIUM at most (known weakly-typed posture). |
| **LOW** | Extra property in SDK that isn't in spec (additive / lossy-tolerant), or required-ness over-tightening (SDK marks `required` but spec says optional). |

### Caveats and limitations

- The audit only resolves **statically-literal** paths. Methods whose path argument is built by a helper call (e.g. some pagination wrappers) are skipped if `_resolve_expr` cannot reduce the expression to a literal. These show up as no-finding rows; they are handled by the existing `check-sdk-sync.py`.
- Enum-value drift is **not** flagged when both sides are nominally `string`. If the spec restricts a string to an `enum` list and the SDK exposes it as `string?`, the audit marks the type as `ok` but the per-domain doc still calls out enum candidates worth modeling as a C# enum.
- **Nested object schemas** are compared shallowly. If the spec has `parent.child.nestedField` and the SDK has a `Parent` record with no `Child`, the audit will report `Parent` is missing `child` but will not recursively diff `Child` against `nestedField`. The plan docs call out these nested types separately under each affected `sdk_type` heading.
- **CLI under `tools/Samsara.Cli/`** is excluded — the audit reads only `src/Samsara.Sdk/`.
- **Beta clients** (`Clients/Beta/*`) intentionally use `object?` everywhere and are known to be weakly typed; findings there are capped at MEDIUM severity.
- The audit does **not** validate response status codes. Only 200 / 201 / 202 are inspected; error envelopes are ignored.

## Findings totals

- **CRITICAL**: 3
- **HIGH**: 329
- **MEDIUM**: 956
- **LOW**: 431
- **Total deduped findings**: 1719

By finding type:

| Type | Count |
|---|---:|
| `extra_property` | 422 |
| `missing_optional_query` | 421 |
| `response_drift_optional` | 309 |
| `response_drift_required` | 226 |
| `missing_optional` | 102 |
| `missing_required_query` | 82 |
| `response_required_drift` | 81 |
| `type_mismatch` | 24 |
| `weak_typing` | 19 |
| `missing_required` | 17 |
| `required_drift_over` | 9 |
| `required_drift` | 4 |
| `wrapper_drift` | 3 |

## Domain breakdown

| # | Domain | CRIT | HIGH | MED | LOW | Total | Plan doc |
|---|---|---:|---:|---:|---:|---:|---|
| 21 | Hubs | 2 | 33 | 39 | 13 | 87 | [`21-hubs.md`](21-hubs.md) |
| 34 | Plans | 1 | 8 | 12 | 4 | 25 | [`34-plans.md`](34-plans.md) |
| 06 | Beta APIs | 0 | 33 | 106 | 10 | 149 | [`06-beta-apis.md`](06-beta-apis.md) |
| 40 | Safety Scores | 0 | 22 | 4 | 23 | 49 | [`40-safety-scores.md`](40-safety-scores.md) |
| 17 | Forms | 0 | 20 | 40 | 18 | 78 | [`17-forms.md`](17-forms.md) |
| 39 | Safety | 0 | 16 | 24 | 2 | 42 | [`39-safety.md`](39-safety.md) |
| 31 | Media | 0 | 14 | 15 | 19 | 48 | [`31-media.md`](31-media.md) |
| 18 | Fuel and Energy | 0 | 13 | 32 | 11 | 56 | [`18-fuel-and-energy.md`](18-fuel-and-energy.md) |
| 42 | Settings | 0 | 12 | 32 | 25 | 69 | [`42-settings.md`](42-settings.md) |
| 02 | Alerts | 0 | 11 | 8 | 6 | 25 | [`02-alerts.md`](02-alerts.md) |
| 09 | Coaching | 0 | 10 | 13 | 8 | 31 | [`09-coaching.md`](09-coaching.md) |
| 13 | Driver-Trailer Assignments | 0 | 10 | 2 | 7 | 19 | [`13-driver-trailer-assignments.md`](13-driver-trailer-assignments.md) |
| 30 | Maintenance | 0 | 9 | 40 | 16 | 65 | [`30-maintenance.md`](30-maintenance.md) |
| 36 | Readings | 0 | 9 | 19 | 9 | 37 | [`36-readings.md`](36-readings.md) |
| 03 | Assets | 0 | 8 | 20 | 1 | 29 | [`03-assets.md`](03-assets.md) |
| 23 | Idling | 0 | 8 | 15 | 8 | 31 | [`23-idling.md`](23-idling.md) |
| 48 | TrainingAssignments | 0 | 8 | 12 | 6 | 26 | [`48-trainingassignments.md`](48-trainingassignments.md) |
| 07 | CARB CTC | 0 | 8 | 7 | 10 | 25 | [`07-carb-ctc.md`](07-carb-ctc.md) |
| 11 | Documents | 0 | 6 | 18 | 7 | 31 | [`11-documents.md`](11-documents.md) |
| 50 | Trips | 0 | 6 | 6 | 13 | 25 | [`50-trips.md`](50-trips.md) |
| 43 | Speeding Intervals | 0 | 6 | 4 | 10 | 20 | [`43-speeding-intervals.md`](43-speeding-intervals.md) |
| 20 | Hours of Service | 0 | 5 | 24 | 30 | 59 | [`20-hours-of-service.md`](20-hours-of-service.md) |
| 38 | Routes | 0 | 5 | 10 | 16 | 31 | [`38-routes.md`](38-routes.md) |
| 32 | Messages | 0 | 5 | 5 | 6 | 16 | [`32-messages.md`](32-messages.md) |
| 49 | TrainingCourses | 0 | 5 | 4 | 4 | 13 | [`49-trainingcourses.md`](49-trainingcourses.md) |
| 24 | Industrial | 0 | 4 | 52 | 6 | 62 | [`24-industrial.md`](24-industrial.md) |
| 47 | Trailers | 0 | 3 | 48 | 21 | 72 | [`47-trailers.md`](47-trailers.md) |
| 27 | Legacy APIs | 0 | 3 | 24 | 0 | 27 | [`27-legacy-apis.md`](27-legacy-apis.md) |
| 25 | Issues | 0 | 3 | 16 | 6 | 25 | [`25-issues.md`](25-issues.md) |
| 28 | Live Sharing Links | 0 | 3 | 16 | 8 | 27 | [`28-live-sharing-links.md`](28-live-sharing-links.md) |
| 04 | Attributes | 0 | 3 | 6 | 1 | 10 | [`04-attributes.md`](04-attributes.md) |
| 16 | Equipment | 0 | 2 | 38 | 8 | 48 | [`16-equipment.md`](16-equipment.md) |
| 56 | Work Orders | 0 | 2 | 20 | 4 | 26 | [`56-work-orders.md`](56-work-orders.md) |
| 01 | Addresses | 0 | 2 | 19 | 2 | 23 | [`01-addresses.md`](01-addresses.md) |
| 52 | Vehicle Locations | 0 | 2 | 11 | 7 | 20 | [`52-vehicle-locations.md`](52-vehicle-locations.md) |
| 29 | Location and Speed | 0 | 2 | 10 | 3 | 15 | [`29-location-and-speed.md`](29-location-and-speed.md) |
| 14 | Driver-Vehicle Assignments | 0 | 2 | 7 | 9 | 18 | [`14-driver-vehicle-assignments.md`](14-driver-vehicle-assignments.md) |
| 05 | Auth Token for Driver | 0 | 2 | 3 | 3 | 8 | [`05-auth-token-for-driver.md`](05-auth-token-for-driver.md) |
| 54 | Vehicles | 0 | 1 | 14 | 4 | 19 | [`54-vehicles.md`](54-vehicles.md) |
| 08 | Carrier Proposed Assignments | 0 | 1 | 13 | 11 | 25 | [`08-carrier-proposed-assignments.md`](08-carrier-proposed-assignments.md) |
| 46 | Trailer Assignments | 0 | 1 | 8 | 8 | 17 | [`46-trailer-assignments.md`](46-trailer-assignments.md) |
| 55 | Webhooks | 0 | 1 | 5 | 1 | 7 | [`55-webhooks.md`](55-webhooks.md) |
| 12 | Driver QR Codes | 0 | 1 | 3 | 2 | 6 | [`12-driver-qr-codes.md`](12-driver-qr-codes.md) |
| 41 | Sensors | 0 | 1 | 0 | 0 | 1 | [`41-sensors.md`](41-sensors.md) |
| 53 | Vehicle Stats | 0 | 0 | 77 | 4 | 81 | [`53-vehicle-stats.md`](53-vehicle-stats.md) |
| 15 | Drivers | 0 | 0 | 20 | 0 | 20 | [`15-drivers.md`](15-drivers.md) |
| 44 | Tachograph (EU Only) | 0 | 0 | 14 | 21 | 35 | [`44-tachograph-eu-only.md`](44-tachograph-eu-only.md) |
| 22 | IFTA | 0 | 0 | 8 | 0 | 8 | [`22-ifta.md`](22-ifta.md) |
| 19 | Gateways | 0 | 0 | 6 | 8 | 14 | [`19-gateways.md`](19-gateways.md) |
| 51 | Users | 0 | 0 | 4 | 1 | 5 | [`51-users.md`](51-users.md) |
| 45 | Tags | 0 | 0 | 2 | 2 | 4 | [`45-tags.md`](45-tags.md) |
| 37 | Route Events | 0 | 0 | 1 | 0 | 1 | [`37-route-events.md`](37-route-events.md) |
| 10 | Contacts | 0 | 0 | 0 | 4 | 4 | [`10-contacts.md`](10-contacts.md) |
| 33 | Organization Info | 0 | 0 | 0 | 5 | 5 | [`33-organization-info.md`](33-organization-info.md) |

## Top 20 worst offenders by SDK type

These models / requests account for the bulk of findings. Fixing them first will close more of the audit than any other ordering.

| Rank | SDK Type | CRIT | HIGH | MED | LOW | Total |
|---:|---|---:|---:|---:|---:|---:|
| 1 | `CreateHubLocationRequest` | 1 | 4 | 5 | 1 | 11 |
| 2 | `CreateHubPlanOrdersRequest` | 1 | 2 | 6 | 1 | 10 |
| 3 | `UpdateHubLocationRequest` | 1 | 0 | 9 | 1 | 11 |
| 4 | `SafetySettings` | 0 | 12 | 0 | 6 | 18 |
| 5 | `SafetyEvent` | 0 | 11 | 11 | 2 | 24 |
| 6 | `HubLocation` | 0 | 10 | 4 | 1 | 15 |
| 7 | `IdlingEvent` | 0 | 8 | 4 | 8 | 20 |
| 8 | `HubPlanOrder` | 0 | 8 | 4 | 1 | 13 |
| 9 | `FormSubmission` | 0 | 7 | 13 | 8 | 28 |
| 10 | `TrainingAssignment` | 0 | 7 | 6 | 6 | 19 |
| 11 | `DriverTrailerAssignment` | 0 | 7 | 0 | 5 | 12 |
| 12 | `CoachingSession` | 0 | 6 | 3 | 5 | 14 |
| 13 | `AlertConfiguration` | 0 | 6 | 3 | 1 | 10 |
| 14 | `MaintenanceDvir` | 0 | 5 | 17 | 6 | 28 |
| 15 | `FormTemplate` | 0 | 5 | 5 | 2 | 12 |
| 16 | `MediaFile` | 0 | 5 | 4 | 11 | 20 |
| 17 | `Trip` | 0 | 5 | 3 | 13 | 21 |
| 18 | `ReadingDefinition` | 0 | 5 | 3 | 4 | 12 |
| 19 | `AlertIncident` | 0 | 5 | 1 | 5 | 11 |
| 20 | `TrainingCourse` | 0 | 5 | 1 | 4 | 10 |

## Cross-cutting patterns

- **Bare records vs spec data-envelopes (3 known cases).** `POST /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/plan/orders` — the SDK posts a bare record where the spec expects `{ data: T[] }` (or `{ data: T }` for the PATCH). Only three CRITICAL cases exist because most Samsara spec POST/PATCH bodies are flat — the data-envelope convention is concentrated in the Hubs domain.
- **Missing required fields on creators.** Many `Create*Request` records under `src/Samsara.Sdk/Models/**` are missing fields the spec marks as required: `CreateHubLocationRequest` (5 missing), `AssetsCreateAssetRequestBody` (request fields like `make`, `model`, `name`), `Forms` create / submission flow records, `CreateAttributeRequest`, `CreateContactRequest`, `CreateDriverQrCodeRequest`, etc. These will produce HTTP 400 responses from Samsara when the missing field is genuinely required.
- **Response records dropping `required` payload fields.** `Hub`, `HubCapacity`, `FormTemplate`, `FormSubmission`, `WorkOrder`, `Vehicle`, `Trailer`, `VehicleStats`, `Issue`, `Asset`, `Driver`, `Trip`, `Coaching*`, `SafetyEvent`, etc. — every one of these records the SDK exposes is **missing fields that the spec marks `required`**. Customers who construct dashboards from these records get nulls where there should be values.
- **Weak `object?` typing in Beta clients.** ~100 properties under `Clients/Beta/*` are typed as `object?` where the spec gives a concrete schema. Recommend a Phase D pass to introduce typed models for `IndustrialJob`, `Detection`, `Device`, `AempEquipment`, `Place`, `PreferredStation`, `QualificationRecord`, `Ridership*`, `FunctionLog`, `FunctionFile` etc.
- **Missing optional query parameters across List endpoints.** Many `List*` methods omit filtering parameters: `tagIds`, `parentTagIds`, `driverActivationStatus`, `attributeValueIds`, `createdAfterTime`, `updatedAfterTime`, etc. These reduce SDK utility but don't cause failures.
- **VehicleStats / TrailerStats `gps` type drift.** On the `/feed` and `/history` endpoints, `gps` is `VehicleStatsListGps[]` (array of points) but the SDK exposes it as a singular `GpsData?` object. The same record is reused for `/stats` (snapshot) which **is** object-shaped, so the SDK silently loses data on the time-series endpoints. Two records are needed: one for the snapshot view and one for the history/feed view.
- **Forms title vs name.** SDK `FormTemplate.name` does not exist in spec; spec uses `title`. The SDK also lacks `createdBy`, `updatedBy`, `revisionId`, `fields`, `formCategory`, `approvalConfig`. Forms is one of the worst-affected domains.
- **HOS log shape divergence.** Spec returns `data: [ HosLogsForDriver { driver, hosLogs[] } ]`; SDK pages over a flat `HosLog` record. As a result every SDK property on `HosLog` (`id`, `driverId`, `driverName`, `vehicleId`, `vehicleName`, `hosStatusType`, `logStartMs`, etc.) is flagged as an `extra_property`, while the spec's `driver` and `hosLogs[]` payloads are missing. The same pattern applies to `Trip`, `TachographActivity`, `Route` and other domains where the SDK flattens nested spec objects into denormalized scalars.
- **Response nullability over-loosening.** 81 `response_required_drift` findings — properties the spec guarantees as non-null are exposed by the SDK as nullable (`string?`, `int?`, etc.). Consumers must null-check fields that the spec promises will never be null. Tightening these is a low-risk binary-compatible quality-of-life improvement for downstream code.
- **Extra properties on SDK records (422 LOW).** Most of these aren't garbage — they reflect *older* shapes of spec models that the SDK kept after the spec changed (e.g. `Vehicle.serial` vs spec's `gateway.serial`, `Trip.driverId` vs spec's `Trip.driver.id`). Removing these wholesale is a binary-breaking exercise; each should be assessed individually.

