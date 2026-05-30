# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- **Model-parity checker (2026-05-29)** — new `tools/check-model-sync.py` codifies the
  one-time 2026-05-27 property-level audit into a reproducible, CI-gated tool. It compares
  SDK record *shapes* against the live spec request/response bodies **property by property,
  matched by endpoint** (unwrapping the `{ data: … }` envelope; resolving the schema the
  endpoint actually uses, not by type name). Severity: CRITICAL = wrapper-shape mismatch,
  HIGH = missing/required-drift on a required field or required query param, MEDIUM/LOW =
  optional fields / extras / intentional flattening / weak `object?` typing (Beta capped at
  MEDIUM). Gated per-PR in `ci.yml` at `--fail-on-severity HIGH`; full MEDIUM/LOW backlog
  reported weekly. Current state on `main`: **0 CRITICAL, 0 HIGH**, 162 MEDIUM, 448 LOW
  (the deferred extra-property / optional-field backlog). See
  `docs/api-sync/full-sync-completion-plan-2026-05-29.md` (Phase 2).
- **Fabrication / mis-homing checker (2026-05-29)** — new `tools/check-sdk-fabrication.py`
  closes the blind spot that let the Hubs bug ship: `check-sdk-sync.py` dedups SDK endpoints
  by `(verb, path)`, so a method mis-homed to another domain's *real* path is counted as
  coverage and reported as `0 mismatches`. The new checker verifies the reverse property —
  every SDK method maps to a **distinct, correctly-homed** spec op — via two signals:
  **duplicate coverage** (one spec op reached from >1 client file) and **client↔tag drift**
  (a method reaching a spec tag outside its client's committed allow-set in
  `tools/sdk-client-tags.json`). Proven against the real pre-fix Hubs code (flags all 5
  duplicate + 5 tag-drift instances); clean on `main`. Gated per-PR in `ci.yml` and reported
  in the weekly `api-sync-check.yml`. A repo-wide sweep confirmed **Hubs was the only
  offender**. See `docs/api-sync/full-sync-completion-plan-2026-05-29.md` (Phase 1).
- **Places: `GET /places/deletions` (2026-05-29)** — added `IPlacesClient.GetDeletionsAsync()`
  (operationId `getPlaceDeletions`, beta) to poll soft-deleted places, closing the last
  endpoint-coverage gap (`check-sdk-sync.py` now reports `missing=0` against the live spec).
  Introduces a typed `Samsara.Sdk.Models.Beta.PlaceDeletionMarker` (`id` + `deletedAtTime`
  required, optional `externalIds`) — the first typed model on the otherwise weakly-typed Beta
  surface — registered in `SamsaraJsonContext`. Additive (no breaking changes). Refreshed the
  cached spec baseline to `2025-10-23` (absorbing the +13 Places-deletion schemas) so
  `diff-report.md` reads clean. See `docs/api-sync/full-sync-completion-plan-2026-05-29.md`
  (Phase 0).

### Changed

- **Phase 3 model-quality sweep (2026-05-30, targeting v0.3.0)** — driving the
  `check-model-sync.py` MEDIUM/LOW backlog to zero, domain by domain. Each domain aligns
  SDK record shapes to the live `2025-10-23` spec: relaxing response fields the spec marks
  optional (the over-tightening that caused the Hubs `missing required properties`
  deserialization throw), typing `object?`/`JsonElement` fields where the spec has a concrete
  schema, adding missing optional fields, fixing type mismatches, and **removing** SDK
  properties absent from the spec (back-compat extras — **breaking**, batched into v0.3.0).
  Per-domain detail in the commits; net finding deltas tracked against `check-model-sync.py`.
  - **Drivers** — added `dateOfBirth` (string) to `Driver`/`CreateDriverRequest`/
    `UpdateDriverRequest`; relaxed `Driver.Id` and `Driver.Name` to nullable (spec lists them
    optional on `GET /fleet/drivers`). **Breaking**: consumers can no longer assume non-null
    `Driver.Id`/`Name`.
  - **Attributes** — relaxed `AttributeDefinition.Id` to nullable (spec lists `id` optional on
    `GET /attributes`). Kept `AttributeDefinition.Entities` (flagged extra on the list response,
    but present and `required` on `GET /attributes/{id}`, `POST /attributes`,
    `PATCH /attributes/{id}` — one record serves all four; it defaults to an empty list so the
    list response still deserializes). **Breaking**: consumers can no longer assume non-null
    `AttributeDefinition.Id`.
  - **Users** — relaxed `UserRole.Id` to nullable (the spec `UserRole` schema lists `id`
    optional on `GET /user-roles` and inside `User.roles`). Kept `UserRole.TagId` (flagged extra
    against `GET /user-roles`, but it is a real spec field on the user create/update role schema
    `CreateUserRequest_roles.tagId`, since `UserRole` doubles as the request role element);
    corrected its stale "not in current spec" comment. **Breaking**: consumers can no longer
    assume non-null `UserRole.Id`.
  - **Documents** — relaxed `DocumentType.Id` and `DocumentPdfJob.Id` to nullable (spec lists
    both optional on `GET /fleet/document-types` and `POST /fleet/documents/pdfs`). Kept
    `DocumentPdfJob.JobStatus`/`RequestedAtTime`/`CompletedAtTime`/`DownloadDocumentPdfUrl`
    (flagged extra against the POST create response, but all are real fields on
    `GET /fleet/documents/pdfs/{id}` — `DocumentPdfJob` is the shared response record for both
    the create and the status-query endpoints). **Breaking**: consumers can no longer assume
    non-null `DocumentType.Id`/`DocumentPdfJob.Id`.
  - **Organization Info** — relaxed `OrganizationInfo.Id` to nullable (spec lists `id` optional
    on `GET /me`) and **removed** `OrganizationInfo.Address`/`City`/`State`/`Zip`/`Country`,
    which are absent from the `GET /me` response schema (the only endpoint using this record);
    the canonical office address is `CarrierSettings.MainOfficeAddress`. **Breaking**: those five
    properties no longer exist and `OrganizationInfo.Id` is now nullable.
  - **Webhooks** — **removed** `UpdateWebhookRequest.EventTypes`: the `PATCH /webhooks/{id}`
    request schema (the only consumer of this record) does not accept `eventTypes`, unlike
    `POST /webhooks` (`CreateWebhookRequest` keeps it). Event subscriptions can only be set at
    creation. **Breaking**: `UpdateWebhookRequest.EventTypes` no longer exists.
  - **Safety** — **removed** the legacy flat `SafetyEvent.Vehicle` and `SafetyEvent.Time`
    properties (plus the now-orphaned `SafetyEventVehicle` record and its `SamsaraJsonContext`
    registration). Neither appears in the `SafetyEventV2ObjectResponseBody` schema returned by
    `GET /safety-events` or `GET /safety-events/stream`; the canonical data is already exposed via
    `SafetyEvent.Asset` (typed) and `SafetyEvent.StartMs`/`EndMs`/`CreatedAtTime`. **Breaking**:
    `SafetyEvent.Vehicle`/`Time` and the `SafetyEventVehicle` type no longer exist.
  - **Location and Speed** — **removed** the legacy flat `AssetLocationAndSpeed.Id`/`Name`/`Time`
    properties. None appears in `LocationAndSpeedResponseResponseBody`
    (`GET /assets/location-and-speed/stream`, the only consumer): `Id`/`Time` are already exposed
    canonically via `Asset.Id` and `HappenedAtTime`, and the response asset object carries no
    `name` (so the hoisted `Name` was always null). **Breaking**: those three properties no longer
    exist — use `Asset.Id` and `HappenedAtTime`.
  - **Carrier Proposed Assignments** — **removed** the legacy flat
    `CarrierProposedAssignment.DriverId`/`DriverName`/`VehicleId`/`VehicleName` scalars. The spec
    `CarrierProposedAssignment` schema (both `GET` and `POST /fleet/carrier-proposed-assignments`)
    models these only under the nested `driver`/`vehicle` objects, which the SDK already exposes as
    the typed `Driver`/`Vehicle` records (with `Id`/`Name`) — so no data is lost. Updated the CLI
    list view to read `Driver?.Id`. **Breaking**: use `Driver.Id`/`Driver.Name`/`Vehicle.Id`/
    `Vehicle.Name` instead of the removed flat scalars.
  - **Live Sharing Links** — **removed** the legacy `LiveSharingLink.Url`/`ExpiresAt`/`EntityId`/
    `EntityType` aliases. None appears in `LiveSharingLinkFullResponseObjectResponseBody` (the
    shared response schema for `GET`/`POST`/`PATCH /live-shares`); each already has a canonical
    spec-backed equivalent the SDK exposes — `LiveSharingUrl`, `ExpiresAtTime`, the typed
    `*LinkConfig` objects, and `Type`. **Breaking**: those four aliases no longer exist.
  - **Sensors** — no SDK change: `V1SensorReadingsResponse<T>` was already correctly typed to the
    v1 `{ groupId, sensors[] }` response shape. The four `weak-typing` findings were a
    `check-model-sync.py` false positive — its record-resolution helper did not strip the
    closed-generic argument, so `V1SensorReadingsResponse<V1CargoReading>` failed to match the
    declared `V1SensorReadingsResponse` record. Added a dedicated `_record_key` for top-level
    record lookups (property-type comparison still preserves element info). Verified the only
    finding delta is the 4 sensor entries clearing; no other domain moved. Not breaking.
  - **Training Courses** — typed `TrainingCourse.Category` (was `object`) as the new
    `TrainingCourseCategory` record (`id`/`name`) and `TrainingCourse.Labels` (was
    `IReadOnlyList<object>`) as `TrainingCourseLabel` (`name`/`type`), per the spec
    `TrainingCategoryObjectResponseBody`/`TrainingCourseLabelObjectResponseBody`; registered both in
    `SamsaraJsonContext`. **Removed** the spec-absent `Name`/`IsActive`/`CreatedAtTime`/
    `UpdatedAtTime` properties (`GET /training-courses` is the only consumer; the canonical fields
    are `Title` and the `Status` enum). Updated the CLI list view to show `Title`. **Breaking**:
    those four properties no longer exist and `Category`/`Labels` change type.
- **`CreateTagRequest.Name` is now `required` (2026-05-29)** — the live 2025-10-23 spec marks
  `name` required on `POST /tags` (`CreateTagRequest.required = ["name"]`), but the SDK had it
  as `string?` (the 45-tags model sync had dropped `required` on a spec read the current spec
  contradicts). Surfaced by the new `check-model-sync.py` as the sole HIGH finding and verified
  against the authoritative spec. **Breaking**: callers of `TagsClient.CreateAsync`/`ReplaceAsync`
  must now set `Name`. `ReplaceAsync` (`PUT /tags/{id}`) shares this record, where the spec lists
  `name` optional — requiring it there is a deliberate, benign over-tightening (a tag replace
  should carry a name). No call sites affected (the one test already sets `Name`).
- **Hubs `ListAsync` fix + read-only cleanup (2026-05-29)** — `IHubsClient.ListAsync()`
  was wired to `GET /addresses` and threw `JSON deserialization for type 'Hub' was
  missing required properties: timeZone, createdAt, updatedAt` after the 21-hubs sync
  tightened `Hub` to the `GET /hubs` schema; `ListAsync()` now lists hubs via `GET /hubs`
  (delegates to `ListHubsAsync`). **Breaking**: the spec exposes no hub
  get-by-id/create/update/delete endpoint, so the address-overlay methods `GetAsync`/
  `CreateAsync`/`UpdateAsync`/`DeleteAsync` and the `CreateHubRequest`/`UpdateHubRequest`
  models were removed (they duplicated the `Addresses` client) — use `client.Addresses`
  for `/addresses` CRUD. CLI Hubs menu reduced to read-only `List All`; dropped the
  `CreateHubRequest`/`UpdateHubRequest` registrations from `SamsaraJsonContext`. See
  `docs/api-sync/21-hubs.md`.
- **Model sync 56-work-orders (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 2 HIGH / 20 MED / 4 LOW — 26 total) across the work-order,
  service-task, and invoice-scan endpoints. **Breaking**: `DeleteWorkOrdersAsync`
  was re-signatured from `string[] ids` to a single **`string id`** — the spec's
  `DELETE /maintenance/work-orders` takes one required `id` (verified safe, no
  callers in src/tools/tests). `PostInvoiceScanRequest` now exposes the HIGH
  **`required object File`** plus the nullable `assetId`, and its non-spec
  `imageBase64` extra was **demoted** from `required` to nullable and retained as
  back-compat (so `file` is the only mandatory field — **breaking** for callers
  that set `imageBase64`). `ServiceTask` gained four nullable props (`category`,
  `estimatedLaborTimeMinutes` as `int?`, `estimatedPartsCost` as `object?`,
  `subcategory`) and tightens `name` to **`required string`**; its non-spec
  `laborCostCents` extra was retained. `InvoiceScan` tightens `workOrderId` to
  **`required string`** and **demotes** its non-spec `id` extra from `required` to
  nullable (retained — leaving it required would break deserialization of the
  spec-shaped response); `status` retained. `WorkOrder` gained the nullable
  `maintenanceSite` (`object?`), and `CreateWorkOrderRequest`/
  `UpdateWorkOrderRequest` each gained nullable `placeExternalId`/`placeId`. Eight
  optional query params were added across the three list/stream methods
  (`ListServiceTasksAsync`: `ids`/`includeArchived`; `ListWorkOrdersAsync`:
  `ids`/`includeExternalIds`; `GetWorkOrdersStreamAsync`: `assetIds`/
  `assignedUserIds`/`workOrderStatuses`/`includeExternalIds`). **Breaking**:
  consumers may now rely on non-null `InvoiceScan.WorkOrderId` and
  `ServiceTask.Name`. No JsonContext changes (all types already registered; new
  props are weakly-typed `object`/scalar/array, no new top-level types); no CLI or
  test changes (no construction sites, fixtures, or callers). See
  `docs/api-sync/model-sync-plan-2026-05-27/56-work-orders.md`.
- **Model sync 55-webhooks (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 1 HIGH / 5 MED / 1 LOW — 7 total) across the webhook endpoints.
  The `Webhook` response record now exposes the HIGH `secretKey` as
  **`required string`** (the spec marks it required across all four webhook
  endpoints — verified safe, no `new Webhook(...)` construction sites and no
  `Webhook` deserialization fixtures) and tightens three previously-nullable
  props to **non-null `required`** (`name`, `url`, `version`).
  `UpdateWebhookRequest` gained the nullable `version` (`string?`), and its
  non-spec `eventTypes` extra was retained as a nullable back-compat property.
  A new optional query param `ids` (`string?`) was added to `ListAsync`
  (`GET /webhooks`) via `QueryBuilder.WithParams`. **Breaking**: consumers may
  now rely on non-null `Webhook.Name`/`Url`/`Version`/`SecretKey`; the CLI
  `List All` webhook action passes the cancellation token by name (the 1st
  positional slot is now `ids`). No JsonContext changes
  (`Webhook`/`UpdateWebhookRequest` already registered; new props are scalar
  `string`/`string?`, no new top-level types). See
  `docs/api-sync/model-sync-plan-2026-05-27/55-webhooks.md`.
- **Model sync 54-vehicles (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 1 HIGH / 14 MED / 4 LOW — 19 total) across the three vehicle
  endpoints. The `Vehicle` response record gained the HIGH `createdAtTime` as
  **`required DateTimeOffset`** (the shared spec schema marks it required across the
  vehicle endpoints — verified safe, no `new Vehicle(...)` construction sites) plus
  five new nullable props (`updatedAtTime`, `isRemotePrivacyButtonEnabled`,
  `vehicleWeight`, `vehicleWeightInKilograms`, `vehicleWeightInPounds`). The two
  `weak_typing` fields (`grossVehicleWeight`, `sensorConfiguration`) were
  intentionally kept as weakly-typed `object?` (effort convention — no fabricated
  models for un-schematized `type=object`, cf. the 58 `object?` props in
  `53-vehicle-stats`), and the 4 LOW non-spec extras (`engineHours`,
  `gatewaySerial`, `grossVehicleWeight`, `odometerMeters`) were retained as nullable
  back-compat props. `UpdateVehicleRequest.attributes` was changed from
  `System.Text.Json.JsonElement?` to **`IReadOnlyList<object>?`** (spec `type=array`;
  the only construction site does not set it). Six optional query params were added
  to `ListAsync` (`GET /fleet/vehicles`) via `QueryBuilder.WithParams` — all
  `string?` except `attributes` which is `IReadOnlyList<string>?` (comma-joined):
  `attributes`, `attributeValueIds`, `tagIds`, `parentTagIds`, `createdAfterTime`,
  `updatedAfterTime`. **Breaking**: consumers may now rely on a non-null
  `Vehicle.CreatedAtTime`; the two `VehiclesClientTests` mock fixtures were updated
  to include `createdAtTime` (precedent: `02-alerts` `Alert.createdAtTime`). The CLI
  `List All` vehicle action passes the cancellation token by name (the 1st
  positional slot is now `attributes`). No JsonContext changes
  (`Vehicle`/`UpdateVehicleRequest` already registered; new props are
  scalar/`DateTimeOffset`/array, no new top-level types). See
  `docs/api-sync/model-sync-plan-2026-05-27/54-vehicles.md`.
- **Model sync 53-vehicle-stats (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 0 HIGH / 77 MED / 4 LOW — 81 total) across the three
  vehicle-stats endpoints. The `VehicleStats` response record gained 58 weakly-typed
  `object?` props (the `auxInput1`–`auxInput13` bank, the EV-telemetry set, the
  spreader bank, and the remaining `type=object` scalars such as
  `engineRpm`/`faultCodes`/`externalIds`) plus three `IReadOnlyList<object>?` array
  props (`engineStates`, `fuelPercents`, `nfcCardScans`). The three `type_mismatch`
  fields `gps`, `gpsOdometerMeters`, and `obdOdometerMeters` were changed from their
  typed records (`GpsData?`/`GpsOdometer?`/`ObdOdometer?`) to **`object?`** — a
  deliberate deviation from the plan's `IReadOnlyList<object>` recommendation: each is
  a single object on the snapshot endpoint (`GET /fleet/vehicles/stats`) but an array
  on feed/history, so `object?` accepts either shape (matches the new `object?` props
  and the dual-shape `EquipmentStats` precedent). The now-unused `GpsData`/`GpsOdometer`/
  `ObdOdometer` records remain registered (out of scope to remove). **Breaking**:
  `VehicleStats.name` tightened from `string?` to `required string` (verified safe — no
  `new VehicleStats(...)` construction sites). Twelve optional query params were added
  across the three methods (each `IReadOnlyList<string>? = null`, comma-joined via
  `QueryBuilder.WithParams`, except `time` which is `string?`): `vehicleIds`/`tagIds`/
  `parentTagIds` on all three, plus `time` on the snapshot and `decorations` on
  feed/history — the previously path-embedded `?types=` query was refactored to
  `QueryBuilder.WithParams`. The 4 LOW non-spec extras (`time`, `engineState`,
  `fuelPercent`, `engineSeconds`) were retained as nullable back-compat props. The CLI
  `List Stats` vehicle action passes the cancellation token by name (the 1st positional
  slot after `types` is now `vehicleIds`) and drops the redundant `?? ""` on `s.Name`.
  No JsonContext changes (`VehicleStats` and the inner typed records already registered;
  new props weakly-typed, no new types) and no test changes (no construction sites). See
  `docs/api-sync/model-sync-plan-2026-05-27/53-vehicle-stats.md`.
- **Model sync 52-vehicle-locations (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 2 HIGH / 11 MED / 7 LOW — 20 total) across the three
  vehicle-location endpoints. The SDK's single `VehicleLocation` record deserializes
  three mutually-exclusive shapes — `ListLocationsAsync` (snapshot, top-level
  `location` object) vs. `GetLocationsFeedAsync` / `GetLocationsHistoryAsync`
  (top-level `locations` array) — so the two HIGH `response_drift_required` props
  (`location`, `locations`) were modeled **nullable** (weakly-typed `object?` /
  `IReadOnlyList<object>?`) rather than `required`; marking either required would
  throw when deserializing the other shape. **Breaking**: `VehicleLocation.name`
  tightened from `string?` to `required string` (present in all three shapes;
  verified safe — no `new VehicleLocation(...)` construction sites). **Breaking**:
  the non-spec flat-scalar extras `latitude`, `longitude`, and `time` were demoted
  from `required` to nullable (`double?` / `double?` / `DateTimeOffset?`) so the real
  wrapper shapes still deserialize (precedent: `SpeedingInterval.Id`, `Trip.Id`); the
  other four LOW extras (`heading`, `speed`, `formattedAddress`, `reverseGeo`) were
  already nullable and retained as-is. Ten optional query params were added across the
  three methods (each `IReadOnlyList<string>? = null`, comma-joined via
  `QueryBuilder.WithParams`, except `time` which is `string?`): `vehicleIds`/`tagIds`/
  `parentTagIds` on all three, plus `time` on the snapshot. The CLI `List Locations`
  vehicle action passes the cancellation token by name (the 1st positional slot is now
  `vehicleIds`), drops the redundant `?? ""` on `l.Name`, and renders the now-nullable
  `Latitude`/`Longitude` via `?.ToString() ?? ""`. No JsonContext changes
  (`VehicleLocation`/`ReverseGeo` already registered; new props weakly-typed, no new
  types) and no test changes (no construction sites). See
  `docs/api-sync/model-sync-plan-2026-05-27/52-vehicle-locations.md`.
- **Model sync 51-users (2026-05-27)** — applied the per-domain remediation plan
  (0 CRIT / 0 HIGH / 4 MED / 1 LOW — 5 total) across the user endpoints. The 4
  MEDIUM `response_required_drift` findings on the `User` response record were
  applied: `name`, `email`, `authType`, and `roles` were tightened from nullable
  to non-nullable via `required` (repo convention for "tighten to non-nullable",
  cf. `Address.FormattedAddress`; verified safe — no `new User(...)` construction
  sites exist anywhere in src/tools/tests). **Breaking**: consumers may now rely
  on non-null `name`/`email`/`authType`/`roles`. The 1 LOW non-spec extra
  (`UserRole.tagId`) is retained as a nullable back-compat prop rather than
  removed. The CLI `List All` users render site in `TuiApp.cs` was simplified to
  drop the now-redundant `?? ""` on `u.Name`/`u.Email`. `ListAsync` and the
  `IUsersClient`/`UsersClient` methods are unchanged; `CreateUserRequest`/
  `UpdateUserRequest` are out of scope. No JsonContext/test changes (`User`/
  `UserRole` already registered, only nullability tightened, no construction
  sites). See `docs/api-sync/model-sync-plan-2026-05-27/51-users.md`.
- **Model sync 50-trips (2026-05-27)** — applied the per-domain remediation plan
  (0 CRIT / 6 HIGH / 6 MED / 13 LOW — 25 total) across the two trip endpoints. The
  SDK's single `Trip` record is a dual-shape unified record deserializing both
  `GET /v1/fleet/trips` (v1 flat) and `GET /trips/stream` (modern), so the
  stream-only "required" props are modeled nullable to avoid breaking v1
  deserialization. **Breaking**: `GetStreamAsync` gained a spec-required
  `IReadOnlyList<string> ids` first param (no default, comma-joined via
  `QueryBuilder.WithParams`) plus 3 optional query params — `completionStatus` and
  `queryBy` (`string?`) and `includeAsset` (`bool?`, lower-cased); the CLI does not
  call `GetStreamAsync`, so no CLI caller fix was needed. **Breaking**: `Trip.Id`
  demoted from `required string` to `string?` (a non-spec extra absent from both
  shapes; leaving it required would break deserialization) — the CLI `List All`
  trips render site was updated to `t.Id ?? ""`. `Trip` (response) gained 5
  stream-shape props modeled nullable — `asset` (weakly-typed `object?`),
  `completionStatus` (`string?`), and `createdAtTime`/`tripStartTime`/`updatedAtTime`
  (`DateTimeOffset?` per repo convention rather than the plan's literal `string`) —
  plus 2 optional props, `tripEndTime` (`DateTimeOffset?`) and `trips`
  (`IReadOnlyList<object>?`). `Trip.startLocation` was intentionally NOT tightened
  (the plan flags it both required-drift and as an `extra_property` on the v1 shape,
  which omits it). The 13 LOW non-spec extras (`id`, `driverId`, `driverName`,
  `vehicleId`, `vehicleName`, `startTime`, `endTime`, `startLocation`,
  `endLocation`, `distanceMeters`, `durationMs`, `fuelConsumedMl`, `coDriver`) are
  retained as nullable back-compat props. `ListAsync` is unchanged. No
  JsonContext/test changes (`Trip`/`TripLocation` already registered, new props
  weakly-typed / scalar / array, no construction sites). See
  `docs/api-sync/model-sync-plan-2026-05-27/50-trips.md`.
- **Model sync 49-trainingcourses (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 5 HIGH / 4 MED / 4 LOW — 13 total) across the
  training-courses list endpoint (`GET /training-courses`). `ListCoursesAsync`
  gained 3 optional query params — `categoryIds`, `courseIds`, and `status`
  (`IReadOnlyList<string>?`, comma-joined, appended via
  `QueryBuilder.WithParams`; `status` modeled as a string array per the
  query-array convention rather than the plan's literal `object`).
  `TrainingCourse` (response) gained 5 required props — `title`, `status`, and
  `revisionId` (`string`), `category` (weakly-typed `object`), and
  `estimatedTimeToCompleteMinutes` (`long`, int64) — plus 1 optional prop,
  `labels` (`IReadOnlyList<object>?`). The 4 LOW non-spec extras (`name`,
  `isActive`, `createdAtTime`, `updatedAtTime`) are retained as nullable
  back-compat props; `name` in particular is kept because the spec's
  course-title field is the newly added `title`. The CLI `List Courses` call
  site in `TuiApp.cs` was updated for the new signature (named
  `cancellationToken:` argument). No JsonContext/test changes (record already
  registered, new props weakly-typed / scalar / array, no construction sites).
  See `docs/api-sync/model-sync-plan-2026-05-27/49-trainingcourses.md`.
- **Model sync 48-trainingassignments (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 8 HIGH / 12 MED / 6 LOW — 26 total) across the
  training-assignments stream endpoint (`GET /training-assignments/stream`).
  **Breaking**: `ListAssignmentsAsync` gained a required `DateTimeOffset startTime`
  query param (spec REQUIRED, placed first with no default, appended via
  `QueryBuilder.WithTimeRange`; modeled as `DateTimeOffset` per repo stream-param
  convention rather than the spec's literal `string`). Also **breaking**:
  `TrainingAssignment.Status` tightened from `string?` to `required string` (spec
  marks it REQUIRED). Added 6 optional query params: `endTime` (`DateTimeOffset?`),
  `categoryIds`/`courseIds`/`learnerIds`/`status` (`IReadOnlyList<string>?`,
  comma-joined), and `isOverdue` (`bool?`). `TrainingAssignment` (response) gained
  7 required props — `course` and `learner` (weakly-typed `object`), `createdById`
  and `updatedById` (`string`), `createdAtTime` and `updatedAtTime`
  (`DateTimeOffset`), and `durationMinutes` (`long`, int64) — plus 5 optional props
  (`startedAtTime`, `deletedAtTime` as `DateTimeOffset?`; `isOverdue`,
  `isCompletedLate` as `bool?`; `scorePercent` as `double?`). The 6 LOW non-spec
  extras (`driverId`, `driverName`, `courseId`, `courseName`, `assignedAtTime`,
  `score`) are retained as nullable back-compat props. The CLI `List Assignments`
  call site in `TuiApp.cs` was updated for the new signature (default 7-day window,
  named `cancellationToken:` argument, non-nullable `Status` deref). No
  JsonContext/test changes (record already registered, new props weakly-typed /
  scalar / `DateTimeOffset`, no construction sites). See
  `docs/api-sync/model-sync-plan-2026-05-27/48-trainingassignments.md`.
- **Model sync 47-trailers (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 3 HIGH / 48 MED / 21 LOW — 72 total) across the trailers
  endpoints (`GET`/`POST` `/fleet/trailers`, `GET`/`PATCH`/`DELETE`
  `/fleet/trailers/{id}`, and the three stats endpoints `/fleet/trailers/stats`,
  `/stats/feed`, `/stats/history`). **Breaking**: `GetStatsSnapshotAsync`,
  `GetStatsFeedAsync`, and `GetStatsHistoryAsync` each gained a required
  `string types` query param (spec REQUIRED, placed first with no default,
  appended via `QueryBuilder.WithParams`). Also **breaking**: `TrailerStats.Name`
  tightened from `string?` to `required string` (spec marks it REQUIRED). Added 14
  optional `string?` query params (all spec `type=string`): `ListAsync` gained
  `parentTagIds`/`tagIds`; `GetStatsSnapshotAsync` gained
  `parentTagIds`/`tagIds`/`time`/`trailerIds`; `GetStatsFeedAsync` gained
  `decorations`/`parentTagIds`/`tagIds`/`trailerIds`; `GetStatsHistoryAsync` gained
  `decorations`/`parentTagIds`/`tagIds`/`trailerIds`. `TrailerStats` gained 23
  weakly-typed `object?` reefer/gps props (`carrierReeferState`, `gps`,
  `gpsOdometerMeters`, the `reefer*` zone fields, etc.). `Trailer` (response),
  `CreateTrailerRequest`, and `UpdateTrailerRequest` each gained `attributes`
  (`IReadOnlyList<object>?`), `enabledForMobile` (`bool?`), and
  `trailerSerialNumber` (`string?`); `UpdateTrailerRequest` additionally gained
  `odometerMeters` (`long?`, int64). All 21 LOW non-spec extras
  (`make`/`model`/`serial`/`vin`/`year` on the three request/response records, plus
  `enabledForCommunication` on `Trailer` and `engineHours`/`location`/`odometer`/
  `temperature`/`time` on `TrailerStats`) are retained as nullable back-compat
  props. The CLI `ListAsync` call site in `TuiApp.cs` was updated for the new
  signature (named `cancellationToken:` argument). No JsonContext/test changes
  (records already registered, new props weakly-typed, no construction sites). See
  `docs/api-sync/model-sync-plan-2026-05-27/47-trailers.md`.
- **Model sync 46-trailer-assignments (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 1 HIGH / 8 MED / 8 LOW — 17 total) across the two v1
  trailer-assignment endpoints (`GET /v1/fleet/trailers/assignments`,
  `GET /v1/fleet/trailers/{trailerId}/assignments`). The single `TrailerAssignment`
  record deserializes BOTH v1 wrapper shapes (list `{ pagination, trailers }` and
  per-trailer `{ id, name, trailerAssignments }`), so shape-specific fields are
  nullable: added `name` (`string?` — kept nullable, not `required`, since it is
  absent on the list shape), `pagination` (`object?`), and
  `trailerAssignments`/`trailers` (`IReadOnlyList<object>?`). **Breaking**:
  `TrailerAssignment.Id` changed from `required string` to `long?` (applies the
  spec int64 type and stays nullable because the list shape has no top-level id;
  also subsumes the conflicting LOW "id is an extra" finding). Both `ListAsync` and
  `GetByTrailerAsync` gained optional `startMs`/`endMs` query params, typed `long?`
  rather than the plan's `int?` (ms-epoch values overflow `Int32`; matches the
  `*Ms` repo convention, cf. `SafetyClient`), appended via
  `QueryBuilder.WithParams`. The 7 flat non-spec scalars (`trailerId`,
  `trailerName`, `vehicleId`, `vehicleName`, `driverId`, `startTime`, `endTime`)
  are retained as nullable back-compat extras. The CLI call site in `TuiApp.cs` was
  updated for the new signature (named `cancellationToken:` argument) and `long?`
  id (`Id?.ToString()`). No JsonContext/test changes (record already registered,
  new props weakly-typed, no construction sites). See
  `docs/api-sync/model-sync-plan-2026-05-27/46-trailer-assignments.md`.
- **Model sync 45-tags (2026-05-27)** — applied the per-domain remediation plan
  (0 CRIT / 0 HIGH / 2 MED / 2 LOW — 4 total) across the tags endpoints
  (`GET`/`POST` `/tags`, +4 more). `Tag` (response) gained the flat `parentTagId`
  (`string?`), which coexists with the existing `parentTag` `EntityReference?`
  object (tiny/abbreviated tag shapes return the flat id; the full `GET /tags`
  response uses the object). `UpdateTagRequest` gained `externalIds`, typed
  `IReadOnlyDictionary<string, string>?` to match its siblings
  `CreateTagRequest.ExternalIds` and `Tag.ExternalIds` rather than the plan's
  generic `object?` placeholder. `CreateTagRequest.name` had `required` dropped
  (now nullable `string?`) since the spec marks it optional, and
  `CreateTagRequest.externalIds` is retained as a nullable back-compat extra. All
  changes are confined to `TagModels.cs`; no client/interface/JsonContext/CLI/
  test changes. See `docs/api-sync/model-sync-plan-2026-05-27/45-tags.md`.
- **Model sync 44-tachograph-eu-only (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 0 HIGH / 14 MED / 21 LOW — 35 total) across the
  three tachograph history endpoints
  (`GET /fleet/drivers/tachograph-activity/history`,
  `GET /fleet/drivers/tachograph-files/history`,
  `GET /fleet/vehicles/tachograph-files/history`). Added 9 optional query
  params: `ListActivitiesAsync` and `ListFilesAsync` each gained `driverIds`,
  `tagIds`, `parentTagIds`; `ListVehicleFilesAsync` gained `vehicleIds`,
  `tagIds`, `parentTagIds` (all trailing `IReadOnlyList<string>?`, comma-joined
  via `QueryBuilder.WithParams`). Added 5 optional response props left
  weakly-typed per the plan: `TachographActivity.activity`
  (`IReadOnlyList<object>?`) and `TachographActivity.driver` (`object?`);
  `TachographFile.driver` (`object?`), `TachographFile.files`
  (`IReadOnlyList<object>?`), and `TachographFile.vehicle` (`object?`). The 21
  non-spec flat scalars (e.g. `driverId`, `vehicleName`, `startTime`,
  `fileType`, `downloadUrl`) are kept as nullable back-compat extras (both
  `TachographActivity.id` and `TachographFile.id` demoted from `required` to
  nullable). No JsonContext/test changes (deserialize-through models already
  registered, no construction sites); two CLI call sites in `TuiApp.cs` switched
  to the named `cancellationToken:` argument. See
  `docs/api-sync/model-sync-plan-2026-05-27/44-tachograph-eu-only.md`.
- **Model sync 43-speeding-intervals (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 6 HIGH / 4 MED / 10 LOW — 20 total) across the
  `GET /speeding-intervals/stream` endpoint. `SpeedingInterval` (response) gained
  its 5 spec-REQUIRED fields as `required` non-nullable: `asset` (`object`),
  `intervals` (`IReadOnlyList<object>`), and the `createdAtTime`, `tripStartTime`,
  and `updatedAtTime` timestamps as `DateTimeOffset`. **Breaking**:
  `GetSpeedingIntervalsStreamAsync` now takes a leading spec-REQUIRED
  `IReadOnlyList<string> assetIds` parameter, plus 4 optional query params
  (`queryBy`, `severityLevels`, `includeAsset`, `includeDriverId`). The 10
  non-spec flat scalars (`id`, `vehicleId`, `vehicleName`, `driverName`,
  `startTime`, `endTime`, `maxSpeedMph`, `speedLimitMph`, `latitude`,
  `longitude`) are kept as nullable back-compat extras (`id` demoted from
  `required` to nullable). No JsonContext/CLI/test changes (deserialize-through
  model already registered, no construction/caller sites). See
  `docs/api-sync/model-sync-plan-2026-05-27/43-speeding-intervals.md`.
- **Model sync 42-settings (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 12 HIGH / 32 MED / 25 LOW — 69 total) across the five settings
  models in `SettingsModels.cs`. `SafetySettings` (response) gained its 12
  spec-REQUIRED fields as `required` non-nullable: `defaultVehicleType`
  (`string`), `safetyScoreTarget` (`long`), and ten deeply-nested config blobs
  (`distractedDrivingDetectionAlerts`, `followingDistanceDetectionAlerts`,
  `forwardCollisionDetectionAlerts`, `harshEventSensitivity`,
  `harshEventSensitivityV2`, `policyViolationsDetectionAlerts`,
  `rollingStopDetectionAlerts`, `safetyScoreConfiguration`, `speedingSettings`,
  `voiceCoaching`) left weakly-typed as `object` per the plan. The MED findings
  added optional fields: 10 each to `ComplianceSettings` /
  `UpdateComplianceSettingsRequest` (`allowUnregulatedVehiclesEnabled`,
  `canadaHosEnabled`, `carrierName`, `dotNumber`, `driverAutoDutyEnabled`,
  `editCertifiedLogsEnabled`, `forceManualLocationForDutyStatusChangesEnabled`,
  `forceReviewUnassignedHosEnabled`, `mainOfficeFormattedAddress`,
  `persistentDutyStatusEnabled`) and 6 each to `DriverAppSettings` /
  `UpdateDriverAppSettingsRequest` (`driverFleetId`, `gamification`,
  `gamificationConfig`, `orgVehicleSearch`, `trailerSelection`,
  `trailerSelectionConfig`). The 25 non-spec flat scalars (e.g. `hosEnabled`,
  `messageEnabled`, `forwardCollisionWarningEnabled`) are kept as nullable
  back-compat extras. No client/JsonContext/CLI/test changes (deserialize-/
  serialize-through models, no new top-level types). See
  `docs/api-sync/model-sync-plan-2026-05-27/42-settings.md`.
- **Model sync 41-sensors (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 1 HIGH / 0 MED / 0 LOW — 1 total). Added the spec-REQUIRED
  `stepMs` (`int`) property to the `V1SensorHistoryRequest` request body for
  `POST /v1/sensors/history`. See
  `docs/api-sync/model-sync-plan-2026-05-27/41-sensors.md`.
- **Model sync 40-safety-scores (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 22 HIGH / 4 MED / 23 LOW — 49 total). The four
  safety-score response models (`VehicleSafetyScore`, `DriverSafetyScore`,
  `TagSafetyScore`, `TagGroupSafetyScore`) were realigned to their real spec
  schemas: added the spec-REQUIRED fields `driveDistanceMeters`,
  `driveTimeMilliseconds`, `behaviors`, `speeding`, and the per-entity score
  (`vehicleScore`/`driverScore`/`tagScore`/`combinedScore`), with `behaviors`
  and `speeding` modeled as strongly-typed `IReadOnlyList<SafetyScoreBehavior>`
  / `IReadOnlyList<SafetyScoreSpeeding>` (two new nested records registered in
  `SamsaraJsonContext`). `scoreType` was made spec-REQUIRED on
  `ListTagSafetyScoresAsync`/`ListTagGroupSafetyScoresAsync` (positional, valid
  values `driver`/`vehicle`), and optional `vehicleIds`/`driverIds`/`tagIds`
  list filters were added across the four list methods. The non-spec flat
  scalars (`safetyScore`, `timeRange`, `totalHarshEventCount`,
  `totalDistanceDrivenMeters`, `totalTimeDrivenMs`, `crashCount`,
  `harshAccelCount`, `harshBrakingCount`, `harshTurningCount`, `tagName`,
  `tagGroupName`, `tagGroupId`) are kept as nullable back-compat extras with
  XML doc pointers to the canonical spec fields, per the conservative
  flat-scalar precedent. CLI safety-score actions and the two existing unit
  tests were updated for the new signatures/required fields. See
  `docs/api-sync/model-sync-plan-2026-05-27/40-safety-scores.md`.
- **Model sync 39-safety (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 16 HIGH / 24 MED / 2 LOW — 42 total). The long-flagged
  `SafetyEvent` v2 stub was rebuilt against its real schema
  `SafetyEventV2ObjectResponseBody` (returned by `getSafetyEventsV2` and
  `getSafetyEventsStream`): added the 14 spec-REQUIRED fields (`asset`,
  `behaviorLabels`, `contextLabels`, `createdAtTime`, `updatedAtTime`, `startMs`,
  `endMs`, `eventState`, `inboxEventUrl`, `incidentReportUrl`, `location`,
  `maxAccelerationGForce`, plus `driver`/`id`) and 8 optional fields
  (`assignedCoach`, `detectedStreams`, `dismissalReason`, `media`,
  `speedingMetadata`, `tripStartTime`, `tripEndTime`, `updatedByUserId`), with
  strongly-typed nested records (`SafetyEventAsset`, `SafetyEventDriver`,
  `SafetyEventBehaviorLabel`, `SafetyEventContextLabel`, `SafetyEventMedia`,
  `SafetyEventDismissalReason`, `SafetyEventSpeedingMetadata`,
  `SafetyEventLocation`, `SafetyEventAddress`, `SafetyEventGeofence`,
  `SafetyEventAttribute`, `SafetyEventTag`). `Id`/`Driver`/`BehaviorLabels`
  tightened to `required` (the last also retyped to the object-typed
  `IReadOnlyList<SafetyEventBehaviorLabel>`). The non-spec `Vehicle` scalar is
  kept alongside the new `asset` object and `Time` is retained, per the
  conservative flat-scalar back-compat precedent. `ISafetyClient.ListEventsAsync`
  gained the spec-REQUIRED `safetyEventIds` plus
  `includeAsset`/`includeDriver`/`includeVgOnlyEvents` (and dropped the
  spec-absent `startTime`/`endTime`); `GetEventsStreamAsync` made `startTime`
  required and added `endTime`/`queryByTimeField`/`assetIds`/`driverIds`/
  `tagIds`/`assignedCoaches`/`behaviorLabels`/`eventStates`/`include*`; the v1
  driver/vehicle safety-score methods gained the spec-REQUIRED `startMs`/`endMs`.
  CLI safety "List Events" rewired to the stream endpoint. See
  `docs/api-sync/model-sync-plan-2026-05-27/39-safety.md`.
- **Model sync 38-routes (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 5 HIGH / 10 MED / 16 LOW — 31 total). `RouteAuditEvent`
  response record realigned to the spec's `RouteFeedObjectResponseBody`: added
  the four spec-REQUIRED fields `changes`/`route` (opaque `JsonElement`),
  `source`/`type` (`string`), plus optional `operation`, and tightened `time`
  to a non-nullable `DateTimeOffset`. The non-spec back-compat scalars
  (`routeId`, `userId`, `eventType`, `description`) are retained; `id` is
  retained but downgraded from `required` to nullable (the spec schema omits
  it, so `required` would break deserialization). `IRoutesClient.ListAsync`
  gained optional `include`/`tagIds`/`parentTagIds`, `GetAsync` gained optional
  `include`, and `GetAuditLogFeedAsync` gained optional `expand`.
  `IHubsClient.ListPlanRoutesAsync` (`GET /hub/plan/routes`) gained the
  spec-REQUIRED `planId` plus optional `routeIds`/`startTime`/`endTime`. The 11
  spec-absent `Route` response scalars were kept per the conservative
  flat-scalar back-compat precedent (plans 08, 13, 14, 28–31). See
  `docs/api-sync/model-sync-plan-2026-05-27/38-routes.md`.
- **Model sync 37-route-events (2026-05-27)** — added the spec's optional
  `includeExternalIds` query parameter to `IRouteEventsClient.GetStreamAsync` /
  `RouteEventsClient.GetStreamAsync`. See
  `docs/api-sync/model-sync-plan-2026-05-27/37-route-events.md`.
- **Model sync 36-readings (2026-05-27)** — applied the per-domain remediation
  plan (0 CRIT / 9 HIGH / 19 MED / 9 LOW — 37 total). `ReadingDefinition`
  response record rebuilt to match the spec inner schema: five new
  spec-REQUIRED fields (`category`, `ingestionEnabled`, `label`, `readingId`,
  `type`) added as `required`, plus the optional `enumValues` array typed
  through a new `EnumValue` nested record mirroring the spec's
  `EnumValueResponseBody`. `Description` / `EntityType` tightened to
  non-nullable per spec guarantee. `ReadingHistory` and `ReadingSnapshot`
  records gained `externalIds` (`IReadOnlyDictionary<string, string>?`) and
  `happenedAtTime` (`DateTimeOffset?`) and tightened `EntityId` to
  non-nullable; `ReadingSnapshot` further gained the spec-REQUIRED
  `ReadingId`. `IReadingsClient.ListDefinitionsAsync` gained two optional
  query parameters (`ids`, `entityTypes`). `IReadingsClient.GetHistoryAsync`
  gained the spec-REQUIRED `entityType` positional parameter and four
  optional parameters (`entityIds`, `externalIds`, `feed`,
  `includeExternalIds`). `IReadingsClient.GetSnapshotAsync` gained the
  spec-REQUIRED `readingIds` and `entityType` positional parameters and four
  optional parameters (`entityIds`, `externalIds`, `asOfTime`,
  `includeExternalIds`). Breaking signature changes for direct callers of
  `GetHistoryAsync` and `GetSnapshotAsync`. Spec-absent SDK-only extras
  (`Id`, `Name`, `DataType`, `Units` on `ReadingDefinition`; `Id`, `Time` on
  `ReadingHistory`; `Id`, `EntityName`, `Time` on `ReadingSnapshot`) retained
  as nullable back-compat properties per the precedent in plans 08, 13, 14,
  28, 29, 30. The previous `required` modifier on `Id` was dropped to
  nullable since the spec response never emits an `id` field and `required`
  would otherwise prevent deserialization. See
  `docs/api-sync/model-sync-plan-2026-05-27/36-readings.md`.
- **Model sync 34-plans (2026-05-27)** — applied the per-domain remediation
  plan (1 CRIT / 8 HIGH / 12 MED / 4 LOW — 25 total). The load-bearing fix
  was a wrapper-drift bug on `POST /hub/plan/orders`: the SDK previously
  posted an unwrapped `{ planId, orderIds }` body that did not match the
  spec inner schema. The request body is now correctly wrapped as
  `CreateHubPlanOrdersRequest { data: IReadOnlyList<CreateHubPlanOrderInput> }`,
  and the inner input shape carries the three spec-REQUIRED fields
  (`customerOrderId`, `hubId`, `planId`) as `required` plus six optional
  fields (`customProperties`, `delivery`, `pickup`, `priority`,
  `quantities`, `skillsRequired`). This mirrors the wrapper fixes shipped
  for `POST /hub/locations` and `PATCH /hub/location/{id}` in the
  `21-hubs` plan. `HubPlan` response record rebuilt to match the spec
  `PlanObjectResponseBody`: five new `required` fields (`createdAt`,
  `hubId`, `name`, `shiftStartTime`, `updatedAt`) and the spec-absent
  `status`/`date` extras were removed. `CreateHubPlanRequest` gained the
  spec-REQUIRED `hubId` property plus the two optional fields
  (`sessionConfigurationId`, `shiftStartTime`), and the spec-absent
  `date` was removed. `IHubsClient.ListPlansAsync` now requires `hubId`
  (spec REQUIRED) as the first positional parameter and gained three
  optional query parameters (`planIds`, `startTime`, `endTime`). Breaking
  signature changes for direct callers of `CreatePlanOrdersAsync`,
  `CreatePlanAsync`, and `ListPlansAsync`. See
  `docs/api-sync/model-sync-plan-2026-05-27/34-plans.md`.
- **Model sync 33-organization-info (2026-05-27)** — applied the per-domain
  remediation plan (0 CRIT / 0 HIGH / 0 MED / 5 LOW). All five LOW findings
  were SDK-only extras on the `OrganizationInfo` `GET /me` response
  (`address`, `city`, `state`, `zip`, `country`). Per the precedent set by
  plans `08`, `13`, `14`, `28`, and `29`, the fields were retained as
  nullable back-compat properties and annotated with XML doc comments
  noting they are not part of the spec inner schema and pointing callers
  to the canonical replacement where one exists
  (`Address` → `CarrierSettings.MainOfficeAddress`). No client signature
  changes; no breaking changes for existing callers. See
  `docs/api-sync/model-sync-plan-2026-05-27/33-organization-info.md`.
- **Model sync 32-messages (2026-05-27)** — applied the per-domain remediation
  plan (5 HIGH, 5 MEDIUM, 6 LOW — 16 total). `DriverMessage` response rebuilt
  to match spec `V1MessageResponse`: added required `isRead`, `text`, and a
  typed `V1MessageSender` (with required `name`/`type`); tightened
  `driverId` from `string?` to required `long` (spec `integer/int64` type
  fix) and `sentAtMs` from `long?` to required `long`; removed SDK-only
  `id`, `senderType`, `body`, `readAtMs` (not present in the spec inner
  schema). `SendDriverMessageRequest` rebuilt to match the spec request
  body: required `IReadOnlyList<string> DriverIds` and `string Text`
  replace the legacy single-driver `DriverId`/`Body` fields. Both shapes
  are breaking signature changes for direct callers of the previous
  records. `IMessagesClient.ListAsync` gained the spec's two optional
  query parameters (`long? endMs`, `long? durationMs`). See
  `docs/api-sync/model-sync-plan-2026-05-27/32-messages.md`.
- **Model sync 31-media (2026-05-27)** — applied the per-domain remediation
  plan (14 HIGH, 15 MEDIUM, 19 LOW — 48 total). `MediaFile` response rebuilt
  to match the spec `UploadedMediaObjectResponseBody`: seven spec-REQUIRED
  fields added as non-nullable `required` (`availableAtTime`, `endTime`,
  `input`, `startTime`, `triggerReason` are new; `mediaType` and
  `vehicleId` tightened from nullable), plus optional `cameraRole` and a
  typed nested `MediaUrlInfo? UrlInfo` (mirrors spec
  `UrlInfoObjectResponseBody`). `MediaRetrieval` response gained the spec
  `MediaObjectResponseBody` fields (`input`, `mediaType`, `quotaStatus`,
  `retrievalId`, `availableAtTime`, `cameraRole`, `urlInfo`); all are
  modeled as nullable because the type is shared between
  `GET /cameras/media/retrieval` and `POST /cameras/media/retrieval`,
  whose response shapes are disjoint (same precedent as `MaintenanceDvir`
  in plan 30). `CreateMediaRetrievalRequest` rebuilt to match the spec
  `MediaRetrievalPostMediaRetrievalRequestBody`: added required
  `IReadOnlyList<string> Inputs` and `string MediaType`; removed spec-absent
  `CameraId`. `IMediaClient.ListAsync` gained the full spec query surface:
  three required positional parameters (`vehicleIds`, `startTime`,
  `endTime`) and four optional ones (`inputs`, `mediaTypes`,
  `triggerReasons`, `availableAfterTime`). This is a breaking signature
  change for direct callers of the previous parameterless `ListAsync`. SDK-
  only response flat-scalars across both response records retained as
  nullable back-compat conveniences per the established workflow precedent
  (08, 13, 14, 28, 29, 30). See
  `docs/api-sync/model-sync-plan-2026-05-27/31-media.md`.
- **Model sync 30-maintenance (2026-05-27)** — applied the per-domain
  remediation plan (9 HIGH, 40 MEDIUM, 16 LOW — 65 total). `MaintenanceDvir`
  response rebuilt to match the spec `DvirStreamResponseDataResponseBody` /
  `Dvir` schemas: required `authorSignature` (typed `JsonElement?`),
  `dvirSubmissionBeginTime`, `dvirSubmissionTime`, `type`, and
  `updatedAtTime` added, plus 16 optional spec fields (`defectIds`,
  `endTime`, `formattedAddress`, `licensePlate`, `location`, `mechanicNotes`,
  `odometerMeters`, `safetyStatus`, `secondSignature`, `startTime`,
  `thirdSignature`, `trailer`, `trailerDefects`, `trailerName`, `vehicle`,
  `vehicleDefects`, `walkaroundPhotos`). `DefectRecord` response rebuilt to
  match `DefectsResponseDataResponseBody` / `Defect`: required `dvirId`
  added; `comment` and `isResolved` tightened from nullable to non-nullable
  `required`; 10 optional spec fields added (`createdAtTime`, `defectPhotos`,
  `defectTypeId`, `mechanicNotes`, `mechanicNotesUpdatedAtTime`,
  `resolvedAtTime`, `resolvedBy`, `trailer`, `updatedAtTime`, `vehicle`).
  `DefectType` response rebuilt to match `DefectTypesResponseDataResponseBody`:
  three required spec fields added as non-nullable (`createdAtTime`, `label`,
  `sectionType`) plus optional `severity`. `UpdateDefectRequest` request body
  rebuilt to match the spec `DefectPatch` schema: added `mechanicNotes`,
  `resolvedAtTime`, `resolvedBy`; removed spec-absent `comment` and
  `resolvedAt`. `IMaintenanceClient` gained the full spec query surface:
  `includeExternalIds` (boolean) added to `GetDvirsStreamAsync`,
  `GetDvirByIdAsync`, `GetDefectsStreamAsync`, `GetDefectAsync`; `isResolved`
  (boolean) added to `GetDefectsStreamAsync`; `safetyStatus` (array,
  comma-joined) added to `GetDvirsStreamAsync`; `ids` (array) added to
  `ListDefectTypesAsync`. This is a breaking signature change for direct
  positional callers of the previous `GetDvirsStreamAsync`,
  `GetDvirByIdAsync`, `GetDefectsStreamAsync`, `GetDefectAsync`, and
  `ListDefectTypesAsync` methods (all new parameters carry defaults, so
  source compatibility is preserved for callers that name only the
  cancellation token). SDK-only response flat-scalars across all three
  response records retained as nullable back-compat conveniences per the
  established workflow precedent (08, 13, 14, 28, 29). See
  `docs/api-sync/model-sync-plan-2026-05-27/30-maintenance.md`.
- **Model sync 29-location-and-speed (2026-05-27)** — applied the per-domain
  remediation plan (2 HIGH, 10 MEDIUM, 3 LOW — 15 total). `AssetLocationAndSpeed`
  rebuilt to match the spec `LocationAndSpeedResponseResponseBody` inner schema:
  two spec-REQUIRED fields (`asset`, `happenedAtTime`) added as non-nullable
  `required`, and `location` tightened from nullable to `required`. Two new
  typed nested response records replace the previous `double? speed` mismatch
  and untyped placeholders — `AssetLocationAndSpeedAsset` mirrors the spec's
  `AssetResponseResponseBody` (required `id`, optional `externalIds`), and
  `AssetLocationAndSpeedSpeed` mirrors `SpeedResponseResponseBody`
  (`ecuSpeedMetersPerSecond`, `gpsSpeedMetersPerSecond`). The pre-existing
  SDK-only flat scalars (`id`, `name`, `time`) are retained as nullable
  back-compat conveniences with XML doc pointers to the canonical spec
  fields, matching the precedent set in `08-carrier-proposed-assignments`,
  `13-driver-trailer-assignments`, `14-driver-vehicle-assignments`, and
  `28-live-sharing-links`. `IAssetsClient.GetLocationAndSpeedStreamAsync`
  gained the full spec query surface: `startTime`/`endTime` (RFC 3339 via the
  shared `WithTimeRange` helper), comma-joined `ids`, and five optional
  booleans (`includeSpeed`, `includeReverseGeo`, `includeGeofenceLookup`,
  `includeHighFrequencyLocations`, `includeExternalIds`). This is a breaking
  signature change for direct callers of the previous parameterless
  `GetLocationAndSpeedStreamAsync(CancellationToken)` (all eight new
  parameters carry defaults, so source compatibility is preserved for callers
  that named only the cancellation token). See
  `docs/api-sync/model-sync-plan-2026-05-27/29-location-and-speed.md`.
- **Model sync 28-live-sharing-links (2026-05-27)** — applied the per-domain
  remediation plan (3 HIGH, 16 MEDIUM, 8 LOW — 27 total). `LiveSharingLink`
  response rebuilt to match the spec
  `LiveSharingLinkFullResponseObjectResponseBody`: `liveSharingUrl` (REQUIRED)
  added, `name` and `type` tightened from nullable to `required`, and four
  optional spec fields added — `description`, `expiresAtTime`, plus typed
  nested config records `LiveSharingLinkAssetsLocationLinkConfig`,
  `LiveSharingLinkAssetsNearLocationLinkConfig`, and
  `LiveSharingLinkAssetsOnRouteLinkConfig` (with supporting
  `LiveSharingLinkLocation` / `LiveSharingLinkTag`). SDK-only response
  flat-scalars (`url`, `expiresAt`, `entityId`, `entityType`) retained as
  nullable back-compat conveniences per the established workflow precedent.
  `CreateLiveSharingLinkRequest` gained `description`, `expiresAtTime`, and
  the three typed `*LinkConfig` request shapes (`CreateAssetsLocationLinkConfig`
  with `assetId`/`location`/`tagIds`); SDK-only `entityId` (previously
  REQUIRED) and `expiresAt` removed (spec-absent body fields silently
  ignored by the API). `UpdateLiveSharingLinkRequest` gained `description`
  and `expiresAtTime`, tightened `name` to `required`, and dropped SDK-only
  body fields `id` and `expiresAt` (the `id` is now passed as the
  spec-required query parameter on `UpdateAsync(string id, ...)`).
  `ILiveSharingLinksClient.ListAsync` gained optional `ids` / `type` filter
  parameters; `UpdateAsync` signature changed to take the `id` query
  parameter explicitly (breaking for direct callers of the previous body-only
  signature). See
  `docs/api-sync/model-sync-plan-2026-05-27/28-live-sharing-links.md`.
- **Model sync 27-legacy-apis (2026-05-27)** — applied the per-domain
  remediation plan (3 HIGH, 24 MEDIUM, 0 LOW — 27 total). `ILegacyApisClient`
  query surface expanded for seven endpoints. `GetVehicleIdlingReportAsync`
  converted from `Task<object>` to paginated `IAsyncEnumerable<object>` and
  gained required `startTime`/`endTime` plus five optional filters
  (`vehicleIds`, `tagIds`, `parentTagIds`, `isPtoActive`,
  `minIdlingDurationMinutes`); `after`/`limit` are handled transparently by
  the shared `PaginateAsync` helper. `GetDriversVehicleAssignmentsAsync` and
  `GetVehiclesDriverAssignmentsAsync` each gained five optional filter
  parameters; `GetDvirHistoryAsync` gained `parentTagIds`/`tagIds` (array);
  `GetDvirDefectsHistoryAsync` gained `isResolved`; `GetSafetyEventsAsync`
  gained `tagIds`/`parentTagIds`/`vehicleIds` (arrays). On
  `V1GetVehicleHarshEventAsync` the parameter previously named `timestampMs`
  was renamed to `timestamp` to match the spec query key (wire format
  unchanged). See `docs/api-sync/model-sync-plan-2026-05-27/27-legacy-apis.md`.
- **Model sync 25-issues (2026-05-27)** — applied the per-domain remediation
  plan (3 HIGH, 16 MEDIUM, 6 LOW). `Issue` rebuilt to match the spec
  `IssueResponseObjectResponseBody` inner schema: three spec-REQUIRED fields
  (`issueSource`, `submittedAtTime`, `submittedBy`) added as non-nullable
  `required`; four existing fields (`title`, `status`, `createdAtTime`,
  `updatedAtTime`) tightened from nullable to `required`; four optional
  spec fields (`asset`, `assignedTo`, `dueDate`, `mediaList`) added. Four
  new nested response records (`IssueAsset`, `IssueUser`, `IssueSource`,
  `IssueMedia`) replace prior `object`/missing placeholders, mirroring
  the spec's `FormsAssetObjectResponseBody`,
  `FormsPolymorphicUserObjectResponseBody`, `IssueSourceObjectResponseBody`,
  and `FormsMediaRecordObjectResponseBody`. Six SDK-only flat scalars
  (`assigneeId`, `assigneeName`, `vehicleId`, `vehicleName`,
  `resolvedAtTime`, `type`) absent from the spec inner schema were removed.
  `CreateIssueRequest.Asset`/`AssignedTo` and `UpdateIssueRequest.AssignedTo`
  swapped from weak `object` to typed `IssueAssetRequest` /
  `IssueAssigneeRequest`; `Media` collections typed as
  `IReadOnlyList<IssueMediaItemRequest>`. `IIssuesClient.GetStreamAsync`
  gained five missing optional query parameters (`status`, `assetIds`,
  `assetExternalIds`, `include`, `assignedToRouteStopIds`) for the full
  spec query surface. See
  `docs/api-sync/model-sync-plan-2026-05-27/25-issues.md`.
- **Model sync 24-industrial (2026-05-27)** — applied the per-domain
  remediation plan (4 HIGH, 52 MEDIUM, 6 LOW). `IndustrialAsset` rebuilt to
  match the spec `AssetResponse` inner schema: three spec-REQUIRED fields
  (`id`, `name`, `isRunning`) are now non-nullable `required`, plus
  optional `statusCode`/`errorMessage` for the data-outputs endpoint;
  eight new nested records (`IndustrialAssetDataOutput`,
  `IndustrialAssetDataInput`, `IndustrialAssetDataInputLastPoint`,
  `IndustrialAssetLocation`, `IndustrialAssetLocationDataInput`,
  `IndustrialAssetParent`, `IndustrialAssetRunningStatusDataInput`,
  `IndustrialAssetTag`) replace prior placeholders. `DataInput` and
  `DataInputDataPoint` rebuilt to mirror `DataInputTinyResponse` +
  `DataInputResponse_allOf` / `DataInputSnapshot_allOf`; nine new nested
  data-point records (`NumberDataPoint`, `StringDataPoint`,
  `LocationDataPoint`, `LocationDataPointGpsLocation`,
  `LocationDataPointPlace`, `FftSpectraDataPoint`, `FftSpectraValue`,
  `J1939D1StatusDataPoint`, `J1939D1Status`) capture the per-type payload
  variants. Six SDK-only flat scalars (`DataInput.Points`,
  `DataInputDataPoint.Time`/`Value`, `IndustrialAsset.DataInputs`/`MacAddress`,
  and the legacy `DataPoint` record) absent from the spec inner schemas
  were removed. `IIndustrialClient` gained 27 optional query parameters
  across `ListAssetsAsync`, `ListDataInputsAsync`, `GetDataInputAsync`,
  the three data-point endpoints, and the v1 vision endpoints; the v1
  vision-runs endpoints now require a `long durationMs` argument.
  See `docs/api-sync/model-sync-plan-2026-05-27/24-industrial.md`.
- **Model sync 23-idling (2026-05-27)** — applied the per-domain remediation
  plan. `IdlingEvent` rebuilt to match the spec
  `IdlingEventObject_V2025_10_23ResponseBody` inner schema: nine spec-REQUIRED
  fields (`asset`, `durationMilliseconds`, `eventUuid`,
  `fuelConsumedMilliliters`, `fuelCost`, `gaseousFuelConsumedGrams`,
  `gaseousFuelCost`, `ptoState`, `startTime`) are now non-nullable `required`;
  five new nested records (`IdlingEventAsset`, `IdlingEventAddress`,
  `IdlingEventOperator`, `IdlingEventFuelCost`, `IdlingEventGaseousFuelCost`)
  replace prior flat `string?` / `object` placeholders and are registered in
  `SamsaraJsonContext`; the optional `airTemperatureMillicelsius` field was
  added. Eight SDK-only flat scalars (`id`, `vehicleId`, `vehicleName`,
  `driverId`, `driverName`, `endTime`, `durationMs`, `fuelConsumedMl`) absent
  from the spec inner schema were removed. `IIdlingClient.ListEventsAsync`
  gained 11 optional query parameters (`assetIds`, `operatorIds`, `ptoState`,
  `min/maxAirTemperatureMillicelsius`, `excludeEventsWithUnknownAirTemperature`,
  `min/maxDurationMilliseconds`, `tagIds`, `parentTagIds`, `includeExternalIds`)
  for the full spec query surface. See
  `docs/api-sync/model-sync-plan-2026-05-27/23-idling.md`.
- **Model sync 22-ifta (2026-05-27)** — applied the per-domain remediation
  plan. `IftaJurisdictionReportsResponse.Year` /
  `.JurisdictionReports`, `IftaVehicleReportsResponse.Year` /
  `.VehicleReports`, and `IftaDetailJob.Args` / `.JobStatus` /
  `.RequestedAtTime` tightened from nullable to non-nullable `required`
  (all spec-REQUIRED on their response wrappers). `IIftaClient.ListVehicleReportsAsync`
  gains the missing optional `after` query parameter for spec pagination.
  See `docs/api-sync/model-sync-plan-2026-05-27/22-ifta.md`.
- **Model sync 21-hubs (2026-05-27)** — applied the per-domain remediation
  plan, including two CRITICAL wrapper-drift fixes that previously broke the
  hub-location request bodies at runtime. **`POST /hub/locations`**: prior
  `CreateHubLocationRequest` is renamed to `CreateHubLocationInput`, gains the
  five spec-REQUIRED fields (`address`, `customerLocationId`, `hubId`,
  `isDepot`, `name` — all `required`) plus the missing optional fields
  (`driverInstructions`, `plannerNotes`, `serviceTimeSeconds`, `serviceWindows`,
  `skillsRequired`), and is now posted inside the new
  `CreateHubLocationsRequest { data: IReadOnlyList<CreateHubLocationInput> }`
  envelope per the spec. **`PATCH /hub/location/{id}`**: `UpdateLocationAsync`
  now takes the new `UpdateHubLocationEnvelopeRequest { data:
  UpdateHubLocationRequest }` so the inner update payload is wrapped in
  `{ data: T }` per the spec; `UpdateHubLocationRequest` gains nine missing
  optional fields (`customerLocationId`, `driverInstructions`, `isDepot`,
  `latitude`, `longitude`, `plannerNotes`, `serviceTimeSeconds`,
  `serviceWindows`, `skillsRequired`) and drops the spec-absent `notes` field.
  Query surface expanded: `ListCapacitiesAsync`, `ListCustomPropertiesAsync`,
  `ListLocationsAsync`, and `ListSkillsAsync` now require `hubId` (spec
  REQUIRED) and accept their full optional surface (`*Ids`, `*Names`,
  `startTime`, `endTime`); `ListHubsAsync` accepts `hubIds`, `startTime`, and
  `endTime`. Response records tightened: spec-REQUIRED fields on `Hub`
  (`createdAt`, `timeZone`, `updatedAt`), `HubLocation`
  (`address`, `name`, `customerLocationId`, `hubId`, `driverInstructions`,
  `plannerNotes`, `isDepot`, `latitude`, `longitude`, `serviceTimeSeconds`,
  `serviceWindows`, `skillsRequired`, `createdAt`, `updatedAt`), `HubCapacity`
  (`id`, `name`, `unit`, `createdAt`, `updatedAt`), `HubCustomProperty`
  (`hubId`, `name`, `csvColumns`, `createdAt`, `updatedAt`), and `HubSkill`
  (`hubId`, `name`, `createdAt`, `updatedAt`) are now non-nullable `required`.
  LOW response-side extras (`Hub.latitude/longitude/formattedAddress/geofence/tags/externalIds`,
  `HubCapacity.capacity/usedCapacity/timeSlot`, `HubCustomProperty.type`,
  `HubLocation.notes`) retained as nullable back-compat per the workflow
  precedent; request-side spec-absent `notes` removed from both
  `CreateHubLocationInput` and `UpdateHubLocationRequest`. See
  `docs/api-sync/model-sync-plan-2026-05-27/21-hubs.md`.
- **Model sync 20-hours-of-service (2026-05-27)** — applied the per-domain
  remediation plan. `HosLog` (response of `GET /fleet/hos/logs`) re-shaped to
  the spec's per-driver shape (`driver` + `hosLogs[]`), backed by new typed
  nested records `HosLogEntry` (with `logStartTime` required, codriver list,
  vehicle ref) and `HosLogLocation` (latitude/longitude). `HosViolation`
  (response of `GET /fleet/hos/violations`) re-shaped to spec's
  `{ violations[]: HosViolationEntry }` form with required `violations`; new
  `HosViolationEntry`, `HosViolationDay` (required start/end times), and
  `HosViolationDriver` (required `id`). `HosDailyLog` (response of
  `GET /fleet/hos/daily-logs`) tightens spec-required `driver`,
  `startTime`, `endTime` to non-nullable `required`, and gains five new
  typed nested records: `HosDailyLogDriver` (with timezone + ELD settings),
  `HosDailyLogEldSettings`, `HosDailyLogDriverRuleset`,
  `HosDailyLogDistanceTraveled` (drive/PC/yard-move distance meters),
  `HosDailyLogDutyStatusDurations` (also used for pending durations),
  `HosDailyLogMetaData` (carrier, home terminal, certification, shipping
  docs, trailers, vehicles), and `HosDailyLogVehicle`. LOW extras
  (`id`/`driverId`/`vehicleId`/etc. on `HosLog`, `HosViolation`,
  `HosDailyLog`) are retained as nullable back-compat per the workflow
  precedent. `IComplianceClient` query surfaces expanded:
  `ListHosLogsAsync` gains `driverIds`/`tagIds`/`parentTagIds`;
  `ListHosViolationsAsync` gains `driverIds`/`tagIds`/`parentTagIds`/`types`;
  `GetHosClocksAsync` gains `tagIds`/`parentTagIds`/`after`/`limit`;
  `ListHosDailyLogsAsync` gains
  `driverIds`/`startDate`/`endDate`/`tagIds`/`parentTagIds`/`driverActivationStatus`/`expand`
  (legacy `startTime`/`endTime` now fall through to `startDate`/`endDate`
  when those are null); `V1ListHosAuthenticationLogsAsync` now requires
  `long driverId` (spec REQUIRED) and converts the existing
  `startTime`/`endTime` `DateTimeOffset`s to the v1 endpoint's `startMs`/`endMs`
  query parameters. See `docs/api-sync/model-sync-plan-2026-05-27/20-hours-of-service.md`.
- **Model sync 19-gateways (2026-05-27)** — applied the per-domain
  remediation plan. `Gateway` (response) tightens spec-required `model` and
  `serial` to non-nullable `required string`, and gains three typed nested
  records: `GatewayAccessoryDevice` (`accessoryDevices`),
  `GatewayConnectionStatus` (`connectionStatus` — `healthStatus`,
  `lastConnected`), and `GatewayDataUsage` (`dataUsageLast30Days` —
  `cellularDataUsageBytes`, `hotspotUsageBytes`). `IGatewaysClient.ListAsync`
  adds an optional `models` (`IReadOnlyList<string>?`) query filter for
  `GET /gateways`, joined with `,`. LOW extras (`id`, `name`, `mainBus`,
  `firmwareVersion`, `wifiMacAddress`, `simCardId`, `vehicle`, `tags`) are
  retained as nullable back-compat per the workflow precedent. See
  `docs/api-sync/model-sync-plan-2026-05-27/19-gateways.md`.
- **Model sync 18-fuel-and-energy (2026-05-27)** — applied the per-domain
  remediation plan. `FuelEnergyVehicleReport` and `FuelEnergyDriverReport`
  (response rows) tighten `distanceTraveledMeters`, `efficiencyMpge`,
  `estFuelEnergyCost`, and `vehicle`/`driver` to non-nullable `required`
  per the spec. `DriverEfficiencyByDriver` and `DriverEfficiencyByVehicle`
  tighten `driverId` / `vehicleId` to `required string` and replace the four
  `object?` weak typings with four new typed nested records:
  `DriverEfficiencyDifficultyScore`, `DriverEfficiencyPercentageData`,
  `DriverEfficiencyRawData`, and `DriverEfficiencyScoreData`. New
  `FuelPurchaseMoney` (required `amount` + `currency`) replaces the
  previously-untyped `object` on `CreateFuelPurchaseRequest.TransactionPrice`
  (now required) and `Discount` (optional). `FuelPurchase` (response of
  `POST /fuel-purchase`) gains required `uuid`; legacy `id`, `driverId`,
  `vehicleId`, and other echoed fields are retained as nullable back-compat.
  `IFuelClient.GetDriverEfficiencyByDriverAsync` and
  `GetDriverEfficiencyByVehicleAsync` now require `startTime` / `endTime`
  (previously took no parameters) and expose optional
  `driverIds`/`vehicleIds`, `dataFormats`, `tagIds`, and `parentTagIds`.
  `ListVehicleFuelEnergyReportsAsync` and `ListDriverFuelEnergyReportsAsync`
  gain an optional `after` cursor. See
  `docs/api-sync/model-sync-plan-2026-05-27/18-fuel-and-energy.md`.
- **Model sync 17-forms (2026-05-27)** — applied the per-domain remediation
  plan. `FormSubmission` (response) now mirrors the spec's nested shape with
  required `createdAtTime`, `updatedAtTime`, `submittedAtTime`, `status`,
  `isRequired`, `fields`, `formTemplate` (object), and `submittedBy` (object),
  plus optional `title`, `approvalDetails`, `asset`, `geofence`, `location`,
  `score`, `externalIds`, `assignedTo`, `assignedAtTime`, `dueAtTime`,
  `routeId`, and `routeStopId`. Legacy flat-scalar fields (`formTemplateId`,
  `formTemplateName`, `driverId`, `driverName`, `vehicleId`, `vehicleName`,
  `state`, `fieldValues`) are retained as nullable back-compat. `FormTemplate`
  (response) gains required `title` (paired with the legacy `name` for
  back-compat per the rename guidance), `revisionId`, `fields`, `createdBy`,
  and `updatedBy`; `createdAtTime`/`updatedAtTime`/`sections` tightened from
  nullable to non-nullable; optional `approvalConfig` and `formCategory`
  added. `FormPdfExport` (response) tightened with required `pdfId`,
  `jobStatus`, `requestedAtTime`, and `expiresAtTime`, plus optional
  `completedAtTime`, `errorMessage`, and `pdfUrlExpiresAtTime`; the legacy
  `status`, `formSubmissionId`, and `createdAt` fields are retained as
  nullable back-compat. `CreateFormSubmissionRequest` now exposes the
  spec-required `formTemplate` (object) and `status` plus optional `title`,
  `assignedTo`, `dueAtTime`, `fields`, `isRequired`, and `routeStopId`;
  legacy `formTemplateId`, `driver`, `vehicle`, and `fieldValues` are
  retained as nullable back-compat. `UpdateFormSubmissionRequest` gains
  optional `title`, `status`, `isRequired`, `approvalDetails`, `assignedTo`,
  `dueAtTime`, and `routeStopId`; legacy `fieldValues` retained.
  `IFormsClient.ListTemplatesAsync` now accepts optional `ids` (array,
  joined by `,`). `GetSubmissionsStreamAsync` adds optional
  `formTemplateIds`, `userIds`, `driverIds`, `assignedToRouteStopIds`, and
  `include` array parameters. `GetPdfExportsAsync(string pdfId)` and
  `CreatePdfExportAsync(string id)` now accept the spec-required query
  parameters (breaking: `CreatePdfExportAsync` previously took
  `CreateFormPdfExportRequest`, which has been removed since the spec
  endpoint has no request body). See
  `docs/api-sync/model-sync-plan-2026-05-27/17-forms.md`.
- **Model sync 16-equipment (2026-05-27)** — applied the per-domain remediation
  plan. `IEquipmentClient` now exposes the spec's optional `parentTagIds`,
  `tagIds`, and `equipmentIds` query parameters across `ListAsync`,
  `ListLocationsAsync`, `GetLocationsFeedAsync`, `GetLocationsHistoryAsync`,
  `GetStatsAsync`, `GetStatsFeedAsync`, and `GetStatsHistoryAsync` (array
  params joined by `,` per the spec's `style=form,explode=false`). `Equipment`
  (response) now carries spec-defined `assetSerial` and a nested
  `installedGateway` (new `EquipmentInstalledGateway`); the legacy
  `equipmentSerialNumber` property is retained as nullable back-compat.
  `EquipmentLocation` now exposes the required spec shape: `location` for
  `GET /fleet/equipment/locations` and `locations` (array) for the
  `/locations/feed` and `/locations/history` endpoints, both backed by a new
  `EquipmentLocationPoint` record (required `latitude`/`longitude`/`time`,
  optional `heading`/`speed`); `Name` is tightened to required, and the legacy
  flat `latitude`/`longitude`/`time` properties are retained as nullable
  back-compat. `EquipmentStats` (response) gains the 12 spec-defined nested
  properties: `engineRpm`, `engineSeconds`, `engineTotalIdleTimeMinutes`,
  `gatewayEngineSeconds`, `gatewayEngineState` (singular object on `/stats`),
  `gatewayEngineStates` (array on `/feed`/`/history`),
  `gatewayJ1939EngineSeconds`, `gps`, `gpsOdometerMeters`, `obdEngineSeconds`,
  `obdEngineState` (singular), and `obdEngineStates` (array), plus
  `engineStates` and `fuelPercents`. Because the spec serializes
  `engineRpm`/`engineSeconds`/`engineTotalIdleTimeMinutes`/`gatewayEngineSeconds`/`gps`/`gpsOdometerMeters`/`obdEngineSeconds`
  as a single object on the snapshot endpoint and as an array on the
  feed/history endpoints, those fields are exposed as
  `System.Text.Json.JsonElement?` so both shapes deserialize without error.
  `Name` is tightened to required, and the legacy `engineState`,
  `fuelPercent`, `obdOdometer`, and `time` flat-scalar conveniences are
  retained as nullable back-compat. See
  `docs/api-sync/model-sync-plan-2026-05-27/16-equipment.md`.
- **Model sync 15-drivers (2026-05-27)** — applied the per-domain remediation
  plan. `IDriversClient.ListAsync` now accepts the 7 spec-defined optional
  query parameters (`driverActivationStatus`, `parentTagIds`, `tagIds`,
  `attributeValueIds`, `attributes`, `updatedAfterTime`, `createdAfterTime`),
  with array params joined by `,` per the spec's `style=form,explode=false`.
  `Driver` (response), `CreateDriverRequest`, and `UpdateDriverRequest` now
  carry typed properties for fields previously modeled as
  `System.Text.Json.JsonElement?`: `attributes` (`IReadOnlyList<object>?`
  matching the spec's `attributeTiny`/`CreateDriverRequest_attributes` inner
  schemas; precedent: `Equipment`, `Vehicle`, `Attributes` domains),
  `hasDrivingFeaturesHidden` and `hasVehicleUnpinningEnabled` (`bool?`), and
  `profileImageBase64` / `profileImageUrl` (`string?`). The remaining
  `JsonElement?` properties (`eldSettings`, `carrierSettings`, `hosSetting`,
  `peerGroupTag`, `trailerGroupTag`, `vehicleGroupTag`,
  `usDriverRulesetOverride`) are left intentionally untyped pending separate
  spec-shaped plans. See
  `docs/api-sync/model-sync-plan-2026-05-27/15-drivers.md`.
- **Model sync 14-driver-vehicle-assignments (2026-05-27)** — applied the
  per-domain remediation plan. `DriverVehicleAssignment` (response) now mirrors
  the spec's nested shape: required `driver` (new
  `DriverVehicleAssignmentDriver`, mirrors `GoaDriverTinyResponseResponseBody`),
  required `vehicle` (new `DriverVehicleAssignmentVehicle`, mirrors
  `GoaVehicleTinyResponseResponseBody`), required `isPassenger`, and required
  `startTime`, plus optional `assignedAtTime`, `endTime`, `assignmentType`,
  `metadata` (new typed `DriverVehicleAssignmentMetadata` with `sourceName`),
  and `message` (POST/PATCH outcome). The legacy flat-scalar conveniences
  `id`, `driverId`, `driverName`, `vehicleId`, and `vehicleName` are retained
  as nullable, documented back-compat properties (not in spec inner schema).
  `IDriverVehicleAssignmentsClient.ListAsync` now accepts optional
  `driverTagIds` and `vehicleTagIds` query parameters per spec; the `filterBy`
  doc string was corrected to the spec's `drivers`/`vehicles` valid values.
  See `docs/api-sync/model-sync-plan-2026-05-27/14-driver-vehicle-assignments.md`.
- **Model sync 13-driver-trailer-assignments (2026-05-27)** — applied the
  per-domain remediation plan. `DriverTrailerAssignment` (response) now mirrors
  the spec's nested shape: required `id`, `driver` (new
  `DriverTrailerAssignmentDriver` with required `driverId` and optional
  `externalIds`), `trailer` (new `DriverTrailerAssignmentTrailer` with required
  `trailerId`), and `startTime` (RFC 3339 string), plus optional
  `createdAtTime`, `endTime`, and `updatedAtTime`. The legacy flat-scalar
  conveniences `driverId`, `driverName`, `trailerId`, `trailerName`, and `time`
  are retained as nullable, documented back-compat properties (not in spec
  inner schema). `CreateDriverTrailerAssignmentRequest` gains optional
  `startTime` (RFC 3339 string). `UpdateDriverTrailerAssignmentRequest` now
  carries only `required string EndTime` (spec marks `endTime` REQUIRED) — the
  SDK-only `driverId`/`trailerId` body fields were dropped (not in spec body).
  `IDriverTrailerAssignmentsClient.ListAsync` now requires
  `IReadOnlyList<string> driverIds` (spec REQUIRED on
  `GET /driver-trailer-assignments`) and accepts optional
  `bool? includeExternalIds`. `UpdateAsync` now takes the assignment `id`
  separately and appends it via `QueryBuilder.WithParams("id", ...)` to match
  the spec's required `id` query parameter on `PATCH /driver-trailer-assignments`.
  See `docs/api-sync/model-sync-plan-2026-05-27/13-driver-trailer-assignments.md`.
- **Model sync 12-driver-qr-codes (2026-05-27)** — applied the per-domain
  remediation plan. `IDriversClient.ListQrCodesAsync` now requires
  `IReadOnlyList<string> driverIds` (spec REQUIRED on `GET /drivers/qr-codes`)
  and appends it via `QueryBuilder.WithParams`. `DriverQrCode` now uses
  `long DriverId` (spec `integer/int64`) and exposes optional `qrCodeLink`;
  removed SDK-only `qrCodeUrl` and `expiresAt` (not in spec inner schema).
  `CreateDriverQrCodeRequest.DriverId` is now `required long` to match the
  spec's required `integer/int64`. See
  `docs/api-sync/model-sync-plan-2026-05-27/12-driver-qr-codes.md`.
- **Model sync 11-documents (2026-05-27)** — applied the per-domain
  remediation plan. `Document` now exposes the spec-required `createdAtTime`,
  `documentType`, `driver`, `state`, and `fields` plus optional `vehicle`,
  `route`, `routeStop`, `updatedAtTime`, and `conditionalFieldSections`. The
  nested objects are backed by new typed records — `DocumentTypeRef`,
  `DriverRef`, `VehicleRef`, `RouteRef`, `RouteStopRef`, and
  `ConditionalFieldSection` — mirroring the spec's `Goa*TinyResponse` shapes.
  Removed the SDK-only flat scalars `documentTypeId`, `driverId`, `vehicleId`,
  `createdAtMs`, and `updatedAtMs` (not in spec; superseded by the nested
  references). `CreateDocumentRequest.DriverId` is now `required` (spec
  marks it REQUIRED) and gains optional `name`, `vehicleId`, `routeStopId`,
  `state`. `DocumentPdfJob` gains optional `jobStatus`, `requestedAtTime`,
  `completedAtTime`, `downloadDocumentPdfUrl` and drops SDK-only `status` and
  `pdfUrl`. `DocumentType` gains optional `conditionalFieldSections`.
  `IDocumentsClient.ListAsync` now requires `startTime` and `endTime`
  (spec REQUIRED) and accepts optional `documentTypeId` and `queryBy`. See
  `docs/api-sync/model-sync-plan-2026-05-27/11-documents.md`.
- **Model sync 10-contacts (2026-05-27)** — applied the per-domain
  remediation plan. Relaxed `CreateContactRequest.FirstName`, `LastName`,
  `Email`, and `Phone` from `required` to optional (`string?`) — the spec
  declares no required properties on `CreateContactRequest`, so the prior
  `required` modifier blocked otherwise valid partial requests. The
  `Contact` response (which DOES require all five fields per spec) and
  `UpdateContactRequest` (already nullable) are unchanged. See
  `docs/api-sync/model-sync-plan-2026-05-27/10-contacts.md`.
- **Model sync 09-coaching (2026-05-27)** — applied the per-domain
  remediation plan. `CoachingSession` now exposes spec-required `behaviors`,
  `coachingType`, `driver`, `dueAtTime`, `sessionStatus`, `updatedAtTime` plus
  optional `assignedCoachId`, `completedCoachId`, `sessionNote`.
  `DriverCoachAssignment` now exposes spec-required nested `driver`,
  `createdAtTime`, `updatedAtTime`, and tightens `coachId` to non-nullable
  (spec marks REQUIRED). Two new shared records, `CoachingDriver`
  (`DriverWithExternalIdObjectResponseBody`) and `CoachingBehavior`
  (`behaviorResponseBody`), back the nested `driver` and `behaviors`
  properties. Legacy flat scalars (`CoachingSession.DriverId/CoachId/Status/
  ScheduledAtTime/SessionType`, `DriverCoachAssignment.DriverId/DriverName/
  CoachName`) retained for back-compat. `ICoachingClient.ListAssignmentsAsync`
  gains optional `driverIds`, `coachIds`, `includeExternalIds`;
  `GetSessionsStreamAsync` gains optional `driverIds`, `coachIds`,
  `sessionStatuses`, `includeCoachableEvents`, `includeExternalIds`.
  `SetAssignmentAsync` gains a primary `(string driverId, string? coachId)`
  overload that sends both as query parameters (spec-compliant — no JSON
  body); the legacy `(SetDriverCoachAssignmentRequest)` overload is preserved
  and now forwards to the primary overload. See
  `docs/api-sync/model-sync-plan-2026-05-27/09-coaching.md`.
- **Model sync 08-carrier-proposed-assignments (2026-05-27)** — applied the
  per-domain remediation plan. `CarrierProposedAssignment` now exposes the
  spec-required `activeTime` plus optional `acceptedTime`, `firstSeenTime`,
  `rejectedTime`, `shippingDocs`, and nested `driver` / `vehicle` / `trailers`
  objects (new `CarrierProposedAssignmentDriver`,
  `CarrierProposedAssignmentVehicle`, `CarrierProposedAssignmentTrailer`
  records mirroring the spec's tiny-response composition). Removed SDK-only
  `endTime`, `startTime`, `status`, `shippingId` (not in spec); retained the
  flat-scalar `driverId`, `driverName`, `vehicleId`, `vehicleName` properties
  alongside the new nested objects for back-compat.
  `CreateCarrierProposedAssignmentRequest` gains optional `activeTime`,
  `shippingDocs`, `trailerIds`, `trailerNames`; removed SDK-only `endTime`,
  `startTime`, `shippingId`. `ICarrierProposedAssignmentsClient.ListAsync`
  gains optional `driverIds` and `activeTime` query parameters. See
  `docs/api-sync/model-sync-plan-2026-05-27/08-carrier-proposed-assignments.md`.
- **Model sync 07-carb-ctc (2026-05-27)** — applied the per-domain
  remediation plan. `CarbCtcVehicle` now exposes spec-required
  `enrollmentId`, `enrollmentVin`, `testStatus` plus optional
  `testStatusDetails`, `lastCollectionAtTime`, `nextCollectionAtTime`;
  removed SDK-only `name`, `vin`, `licensePlate`, `complianceStatus`,
  `modelYear`, `fuelType` (not in spec). `CarbCtcVehicleHistory` now
  exposes spec-required `enrollmentId`, `enrollmentVin`, `happenedAtTime`,
  `testResult` plus optional `testResultDetails`; removed SDK-only
  `vehicleId`, `time`, `event`, `details`. `ICarbCtcClient.ListVehiclesAsync`
  gains optional `tagIds`, `parentTagIds`, `testStatus` query parameters;
  `ListVehicleHistoryAsync` promotes `vehicleIds` to a required parameter
  (spec marks `required: true`). See
  `docs/api-sync/model-sync-plan-2026-05-27/07-carb-ctc.md`.
- **Model sync 06-beta-apis (2026-05-27)** — applied the per-domain
  remediation plan. Added spec-required and spec-optional query parameters
  across all Beta clients (`BetaClient`, `FunctionsClient`, `PlacesClient`,
  `PreferredStationsClient`, `QualificationRecordsClient`, `ReportsClient`,
  `RidershipClient`) plus the Beta operations on `ComplianceClient` (HOS
  ELD events filters, `UpdateShippingDocsAsync` now requires `driverID` and
  `hosDate`), `TachographClient` (live-data filters), `VehiclesClient`
  (`vehicleIds` required for immobilizer stream), `AssetsClient`
  (depreciation `assetIds`/time-range; inputs stream `ids` + `type`
  required), `DriversClient` (`workflowType` for workflow listing),
  `HubsClient` (`planId` required for plan orders, `hubId` required for
  route templates), and `MaintenanceClient` (vendor `ids`/`includeExternalIds`).
  `HubPlanOrder` response now exposes all 8 spec-required fields
  (`hubId`, `customerOrderId`, `priority`, `createdAtTime`, `updatedAtTime`,
  `customProperties`, `quantities`, `skillsRequired`) and tightens `planId`
  to non-nullable; adds optional `routeId`, `pickup`, `delivery` (typed
  as `object?` per Beta posture). `UpdateEquipmentRequest` adds `id`,
  `equipmentSerialNumber`, `engineHours`, `odometerMeters`, `tagIds`, and
  `attributes`. `Equipment` response adds optional `attributes`.
  `HosEldEvent` adds `name` and `driverActivationStatus`. Deeply nested
  `object?` payloads on Beta records (e.g., `HosEldEvent.eldEvents`,
  `HosEldEvent.externalIds`) intentionally remain weakly-typed per the
  documented Beta subject-to-change posture; 2 MEDIUM weak-typing items
  deferred. See
  `docs/api-sync/model-sync-plan-2026-05-27/06-beta-apis.md`.
- **Model sync 05-auth-token-for-driver (2026-05-27)** — applied the
  per-domain remediation plan. `CreateDriverAuthTokenRequest` now exposes
  the spec-required `code` (`required string`), plus optional `externalId`
  and `username` (`string?`); `driverId` switches from `required string`
  to `long?` (spec `integer/int64`, optional — one of `driverId`,
  `externalId`, or `username` is required). `DriverAuthToken` response
  drops the non-spec `driverId` and `expiresAt` and adds the spec-required
  `expirationTime` (`required long`, Unix milliseconds). See
  `docs/api-sync/model-sync-plan-2026-05-27/05-auth-token-for-driver.md`.
- **Model sync 04-attributes (2026-05-27)** — applied the per-domain
  remediation plan. `IAttributesClient.ListAsync`, `GetAsync`, and `DeleteAsync`
  now take the spec-required `entityType` query parameter. `AttributeDefinition`
  response adds `unit` (string) and `values` (`IReadOnlyList<object>?`) and
  tightens `entities` to non-nullable (defaults to an empty list). Request
  bodies gain `entities` (`CreateAttributeRequest` and `UpdateAttributeRequest`)
  and `unit` (`CreateAttributeRequest`). CLI menu updated to prompt for entity
  type on list/get/delete. See
  `docs/api-sync/model-sync-plan-2026-05-27/04-attributes.md`.
- **Model sync 03-assets (2026-05-27)** — applied the per-domain remediation
  plan. `Asset` response now exposes `attributes` (raw `JsonElement` list,
  matching the spec inner `GoaAttributeTinyResponseBody`) and tightens
  `createdAtTime`/`updatedAtTime` to non-nullable `DateTimeOffset` (both marked
  REQUIRED by the spec). `CreateAssetRequest.Name` is no longer `required`
  (spec marks it optional). `IAssetsClient.ListAsync` now exposes all 11
  documented optional query parameters (`type`, `updatedAfterTime`,
  `includeExternalIds`, `includeTags`, `includeAttributes`, `tagIds`,
  `parentTagIds`, `ids`, `externalIds`, `attributeValueIds`, `attributes`).
  `UpdateAsync(string id, ...)` and `DeleteAsync(string id, ...)` now thread
  the spec-required `id` query parameter (the previous `DeleteAsync` accepted
  `string[] ids` and hand-built a non-spec `ids[]=` query string). Legacy v1
  reefer/location operations now take their spec-required `startMs`/`endMs`
  (Unix epoch milliseconds, `long`); reefer-list and current-locations also
  surface the optional `startingAfter`/`endingBefore`/`limit` pagination
  parameters. See `docs/api-sync/model-sync-plan-2026-05-27/03-assets.md`.
- **Model sync 02-alerts (2026-05-27)** — applied the per-domain remediation
  plan. `AlertConfiguration` and `AlertIncident` response records now expose
  every spec-required field with the correct `required`/non-nullable typing
  (`actions`/`triggers`/`scope`/`createdAtTime`/`lastModifiedAtTime`/`isEnabled`
  on configs; `conditions`/`happenedAtTime`/`incidentUrl`/`isResolved`/
  `updatedAtTime` on incidents). SDK-only extras absent from the spec inner
  schema were removed (`AlertConfiguration.ConditionType`;
  `AlertIncident.{Id,AlertId,Driver,Vehicle,TriggeredAtTime}`). Weak `object?`
  on `scope`/`operationalSettings`/`triggers`/`actions` replaced with new typed
  wrappers `AlertScope`, `AlertTrigger`, `AlertAction`,
  `AlertOperationalSettings`, and `AlertIncidentCondition`. CLI rendering
  updated to surface incident `ConfigurationId`/`HappenedAtTime`/`IncidentUrl`
  (the prior `Id` column is not in the spec). See
  `docs/api-sync/model-sync-plan-2026-05-27/02-alerts.md`.
- **Model sync 01-addresses (2026-05-27)** — applied the per-domain remediation
  plan: `Address.formattedAddress` and `Address.geofence` are now non-nullable
  on the response (spec marks both REQUIRED); `IAddressesClient.ListAsync` now
  exposes the spec's `parentTagIds`, `tagIds`, and `createdAfterTime` query
  parameters. Remaining plan findings (`addressTypes`/`contacts`/`notes`/
  `createdAtTime` on `Address`; `addressTypes`/`contactIds`/`notes`/`geofence`
  on the create/update requests) were already present in the SDK and required
  no edits. See `docs/api-sync/model-sync-plan-2026-05-27/01-addresses.md`.
- **Broken-domain reworks (API sync)** — five domains rebuilt to call the real spec endpoints:
  - **Issues** — `getIssues` requires the `ids` query parameter (no true get-by-id);
    `patchIssue` and `postIssue` exist; SDK now takes `ListAsync(IReadOnlyList<string> ids)`,
    `UpdateAsync(UpdateIssueRequest)` with id in body, `CreateAsync(CreateIssueRequest)`.
  - **Driver-Vehicle Assignments** — `Update` takes `UpdateDriverVehicleAssignmentRequest`
    (driverId/vehicleId/startTime in body); `Delete` takes `DeleteDriverVehicleAssignmentsRequest`
    in the body (DELETE with body); `List` requires `filterBy`.
  - **Sensors** — rebuilt as the v1 POST API (`/v1/sensors/{list,history,cargo,door,humidity,temperature}`)
    with proper long-int models and a new `PostAsync<T>` helper for v1's no-envelope responses.
  - **Fuel** — replaced non-existent `/fleet/vehicles/fuel/*` with `ListVehicleFuelEnergyReports`,
    `ListDriverFuelEnergyReports`, `GetDriverEfficiencyByDriver/ByVehicle`, and
    `CreateFuelPurchase` (POST `/fuel-purchase`).
  - **IFTA** — replaced non-existent detail/summary with `ListJurisdictionReports` and
    `ListVehicleReports` (year-required); CSV jobs moved to `/ifta-detail/csv` with
    spec-correct startHour/endHour/vehicleIds/vehicleTagIds/vehicleParentTagIds.
  - **Alerts** — top-level `/alerts` CRUD removed (not in spec); `UpdateConfiguration` and
    `DeleteConfiguration` move id to body/query respectively; `GetIncidentsStream` now requires
    `startTime` and `configurationIds`. `CreateAlertConfigurationRequest` gains required
    `isEnabled`/`scope`/`triggers`/`actions`.
  - **Compliance** — `GetHosClocks` now returns `IReadOnlyList<HosClocksForDriver>` with the
    correct nested `break`/`cycle`/`drive`/`shift` model.
- **Request-body required fields tightened** (per spec): `CreateDriverRequest.username`,
  `CreateUserRequest.authType`/`roles`, `CreateAddressRequest.formattedAddress`/`geofence`,
  `CreateAttributeRequest.attributeType`, `UpdateAttributeRequest.entityType`.
- **Response models corrected:**
  - `Tag` — `parentTagId: string` → `parentTag: EntityReference`; added `externalIds`.
  - `Address` — added `contacts: EntityReference[]` and `createdAtTime` (the v1-style
    `contactIds: string[]` is not in the spec).
  - `VehicleLocation` — `latitude`/`longitude`/`time` are now required; added `reverseGeo`.
  - `Vehicle` — added `auxInputType3..13`, `attributes`, `vehicleType`, `esn`, `cameraSerial`,
    `grossVehicleWeight`, `sensorConfiguration`, `engineHours`, `odometerMeters`, `gatewaySerial`.
  - `AttributeEntity` — added `entityId` and `values`.

### Status (as of these changes)

**SDK ↔ spec parity is complete and independently verified** — all 317 spec operations
have at least one matched SDK method. `tools/check-sdk-sync.py` reports
`matched=323, mismatched=0, missing=0` against the 2025-10-23 spec; a separate
independent agent verification on 2026-05-27 reached the same conclusion.
`dotnet build` and the 59-test suite pass. The cached baseline has been refreshed.

### Fixed (post-parity)

- **`IFormsClient.ListSubmissionsAsync` / `GetSubmissionAsync`** — the spec's
  `getFormSubmissions` requires an `ids[]` query parameter (plural, array). The
  prior signatures (no-arg list, `?id=` get) would have 400'd at runtime. List now
  takes `IReadOnlyList<string> ids`; `GetSubmissionAsync` is a convenience wrapper
  that calls list with a single id and returns the first match (mirrors
  `Issues.GetAsync`). Caught by independent agent verification.

### Added

- **10 new domain clients** for full coverage of the spec:
  - `ILegacyApisClient` (v1 fleet/reports endpoints — `/fleet/defects/history`,
    `/fleet/dvirs/history`, legacy safety events feed, vehicle harsh-event lookup, etc.)
  - `IPreviewApisClient` (`/preview/*` — vehicle lock/unlock, gateway pairing,
    driver auth-token preview)
  - `IRouteEventsClient` (`/route-events/stream`)
  - `IPlacesClient` (Beta — `/places` CRUD)
  - `IPreferredStationsClient` (Beta — `/preferred-stations` CRUD)
  - `IQualificationRecordsClient` (Beta — records CRUD + stream + types + archive/unarchive)
  - `IRidershipClient` (Beta — passengers + route-setups CRUD)
  - `IFunctionsClient` (Beta — Samsara Functions + Functions Storage)
  - `IReportsClient` (Beta — `/reports/configs`, `/reports/datasets`, `/reports/runs*`)
  - `IBetaClient` (Beta misc — industrial jobs, devices, detections stream, AEMP equipment,
    driver-efficiency)
- **Extensions to existing clients** covering the rest of the gaps:
  - `IContactsClient` — Create / Update / Delete
  - `IFormsClient` — Create / Update form submissions
  - `IGatewaysClient` — Create / Delete
  - `IHubsClient` — `ListHubsAsync` (the canonical `GET /hubs`), `ListPlanOrdersAsync`,
    `ListPlanRoutesAsync`, `DeletePlanOrdersAsync`, `ListRouteTemplatesAsync`,
    `DeleteRouteTemplatesAsync`
  - `IEquipmentClient` — `GetStatsAsync` (snapshot)
  - `ITachographClient` — `ListVehicleFilesAsync`, `ListLiveDataAsync`
  - `ITrailerAssignmentsClient` — `GetByTrailerAsync`
  - `ITrainingClient` — `CreateAssignmentsAsync` / `UpdateAssignmentsAsync` / `DeleteAssignmentsAsync`
  - `IRoutesClient` — `V1DeleteDispatchRouteAsync`
  - `IComplianceClient` — `V1ListHosAuthenticationLogsAsync`, `V1SetCurrentDutyStatusAsync`,
    `UpdateShippingDocsAsync`
  - `ISafetyClient` — `V1GetDriverSafetyScoreAsync`, `V1GetVehicleSafetyScoreAsync`,
    `PatchEventsBatchAsync`
  - `IMaintenanceClient` — `V1ListMaintenanceAsync`, `ListVendorsAsync`, `ListVendorCategoriesAsync`
  - `IAssetsClient` — `V1GetAllAssetsAsync`, `V1GetAllAssetCurrentLocationsAsync`,
    `V1GetAssetsReefersAsync`, `V1GetAssetLocationAsync`, `V1GetAssetReeferAsync`,
    `GetDepreciationTransactionsAsync`, `GetInputsStreamAsync`,
    `ListDeviceRecoveryMissingAsync`, `MarkAssetMissingAsync`, `RecoverAssetAsync`
  - `IIndustrialClient` — `CreateAssetAsync` / `UpdateAssetAsync` / `UpdateAssetDataOutputsAsync` /
    `DeleteAssetAsync` plus v1 Vision (`V1ListCamerasAsync`, runs, programs) and
    v1 Machines (`V1ListMachinesAsync`, `V1GetMachineHistoryAsync`)
  - `IReadingsClient` — `CreateAsync` (POST `/readings`)
  - `IDriversClient` — `ListWorkflowsAsync`, `CreateWorkflowAssignmentAsync`,
    `ResolveVoiceSignInAssignmentAsync`
  - `IVehiclesClient` — `GetImmobilizerStreamAsync`, `UpdateImmobilizerStateAsync`
- New request/response models: `CreateContactRequest`/`UpdateContactRequest`,
  `CreateFormSubmissionRequest`/`UpdateFormSubmissionRequest`, `CreateGatewayRequest`.
- **`tools/check-sdk-sync.py`** — extended to follow class-level `const string` aliases
  inside interpolated strings, and to scan files that combine interface + impl declarations.

- **Endpoint path corrections (API sync)** — fixed wrong URL paths that returned 404:
  Tags `/fleet/tags`→`/tags`; Messages→`/v1/fleet/messages`; Forms→`/form-templates`,
  `/form-submissions`; Tachograph +`/history`; Training→`/training-assignments/stream`,
  `/training-courses`; Trailer Assignments→`/v1/fleet/trailers/assignments`; Routes audit
  log→`/fleet/routes/audit-logs/feed`; Industrial→`/industrial/data-inputs`; Gateways→
  `/gateways`; Trips list→`/v1/fleet/trips`; Maintenance DVIR/defect paths→`/dvirs/*`,
  `/defects/*`, `/defect-types`; Compliance ELD events→`/beta/fleet/hos/drivers/eld-events`;
  Equipment update→`/beta/fleet/equipment/{id}`; Media→`/cameras/media` + `/cameras/media/retrieval`.
- **`IComplianceClient.GetHosClocksAsync`** — now takes `IReadOnlyList<string> driverIds` and
  returns `IReadOnlyList<HosClocksForDriver>` (was single driver / flat `HosClocks`); path
  corrected to `/fleet/hos/clocks`. `HosClocks` is now the nested break/cycle/drive/shift object.

### Removed

- **Fabricated operations** that have no Samsara API endpoint (breaking; all previously 404'd):
  `Drivers.DeleteAsync`, `Vehicles.CreateAsync`/`DeleteAsync`, `Equipment.CreateAsync`/`DeleteAsync`,
  `CarrierProposedAssignments.UpdateAsync`, `Gateways.GetAsync` (no get-by-id),
  `Safety.GetEventAsync` (no get-by-id), `Media.GetAsync` (no get-by-id),
  `Maintenance.ListDtcsAsync`. Duplicate DVIR methods removed from `IComplianceClient`
  (DVIRs live on `IMaintenanceClient`).

### Added

- **API sync documentation** — `docs/api-sync/` folder with 56 per-domain Markdown checklists
  tracking coverage of every endpoint in the Samsara OpenAPI spec (version `2025-10-23`)
- **`tools/check-api-sync.py`** — CLI script that fetches the live Samsara spec, diffs it against
  a cached baseline, and emits a `docs/api-sync/diff-report.md` with new/removed/changed endpoints
- **`.github/workflows/api-sync-check.yml`** — Weekly GitHub Actions workflow (Mondays 08:00 UTC)
  that runs the sync check and opens a labeled `api-sync` issue when API drift is detected
- **`IAssetsClient` / `AssetsClient`** — CRUD for fleet assets (`/assets`), plus location-and-speed
  stream (`/assets/location-and-speed/stream`)
- **`ICarbCtcClient` / `CarbCtcClient`** — CARB CTC compliance vehicle list and history
  (`/fleet/carb-ctc/vehicles`, `/fleet/carb-ctc/vehicles/history`)
- **`ICoachingClient` / `CoachingClient`** — Driver coach assignments and coaching sessions stream
  (`/coaching/driver-coach-assignments`, `/coaching/sessions/stream`)
- **`IDriverTrailerAssignmentsClient` / `DriverTrailerAssignmentsClient`** — Driver-trailer pairing
  CRUD (`/driver-trailer-assignments`)
- **`IIdlingClient` / `IdlingClient`** — Idling events stream (`/idling/events`)
- **`ILiveSharingLinksClient` / `LiveSharingLinksClient`** — Live sharing link management
  (`/live-shares`)
- **`IReadingsClient` / `ReadingsClient`** — Readings definitions, history, and latest snapshot
  (`/readings/definitions`, `/readings/history`, `/readings/latest`)
- **`ISettingsClient` / `SettingsClient`** — Compliance, driver-app, and safety fleet settings
  (`/fleet/settings/compliance`, `/fleet/settings/driver-app`, `/fleet/settings/safety`)
- **`IWorkOrdersClient` / `WorkOrdersClient`** — Work orders CRUD, stream, service tasks, and
  invoice scans (`/maintenance/work-orders`, `/maintenance/service-tasks`, `/maintenance/invoice-scans`)
- **`IVehiclesClient`** — Added `GetLocationsFeedAsync`, `GetLocationsHistoryAsync`,
  `GetStatsFeedAsync`, `GetStatsHistoryAsync`, `GetSpeedingIntervalsStreamAsync`
- **`ITrailersClient`** — Added `GetStatsSnapshotAsync`, `GetStatsFeedAsync`, `GetStatsHistoryAsync`
- **`IEquipmentClient`** — Added `GetLocationsFeedAsync`, `GetLocationsHistoryAsync`,
  `GetStatsFeedAsync`, `GetStatsHistoryAsync`
- **`IDocumentsClient`** — Added `GeneratePdfAsync`, `GetPdfAsync`, `DeleteAsync`
- **`IDriversClient`** — Added `RemoteSignOutAsync`, `CreateAuthTokenAsync`, `ListQrCodesAsync`,
  `CreateQrCodeAsync`, `DeleteQrCodeAsync`
- **`IFormsClient`** — Added `GetSubmissionsStreamAsync`, `GetPdfExportsAsync`, `CreatePdfExportAsync`
- **`IHubsClient`** — Added `ListCapacitiesAsync`, `ListCustomPropertiesAsync`, `ListLocationsAsync`,
  `CreateLocationAsync`, `UpdateLocationAsync`, `ListSkillsAsync`, `CreatePlanAsync`,
  `ListPlansAsync`, `CreatePlanOrdersAsync`
- **`IIftaClient`** — Added `CreateDetailJobAsync`, `GetDetailJobAsync`
- **`IIndustrialClient`** — Added `ListDataInputsAsync`, `GetDataInputSnapshotAsync`,
  `GetDataInputFeedAsync`, `GetDataInputHistoryAsync`
- **`IIssuesClient`** — Added `GetStreamAsync`
- **`IMaintenanceClient`** — Added `GetDvirsStreamAsync`, `GetDvirByIdAsync`, `CreateDvirAsync`,
  `UpdateDvirAsync`, `GetDefectsStreamAsync`, `GetDefectAsync`, `UpdateDefectAsync`,
  `ListDefectTypesAsync`
- **`IMediaClient`** — Added `GetRetrievalAsync`, `CreateRetrievalAsync`
- **`IRoutesClient`** — Added `GetAuditLogFeedAsync`
- **`ISafetyClient`** — Fixed all endpoint paths to current API (safety-events, safety-scores/*);
  added `GetEventsStreamAsync`, `ListDriverSafetyScoresAsync` (v2), `ListVehicleSafetyScoresAsync` (v2),
  `ListTagSafetyScoresAsync` (v2), `ListTagGroupSafetyScoresAsync` (v2)
- **`ITagsClient`** — Added `ReplaceAsync` (PUT /tags/{id})
- **`ITripsClient`** — Added `GetStreamAsync` (GET /trips/stream)
- **`IAlertsClient`** — Added `GetIncidentsStreamAsync`
- **`ISamsaraClient`** — Added 9 new domain client properties: `Assets`, `CarbCtc`, `Coaching`,
  `DriverTrailerAssignments`, `Idling`, `LiveSharingLinks`, `Readings`, `Settings`, `WorkOrders`
- New model types: `Asset`, `AssetLocationAndSpeed`, `CarbCtcVehicle`, `DriverCoachAssignment`,
  `CoachingSession`, `DriverTrailerAssignment`, `IdlingEvent`, `LiveSharingLink`,
  `ReadingDefinition`, `ReadingHistory`, `ReadingSnapshot`, `ComplianceSettings`,
  `DriverAppSettings`, `SafetySettings`, `WorkOrder`, `ServiceTask`, `InvoiceScan`,
  `TrailerStats`, `EquipmentStats`, `SpeedingInterval`, `RouteAuditEvent`, `DocumentPdfJob`,
  `FormPdfExport`, `MediaRetrieval`, `IftaDetailJob`, `DataInputDataPoint`, `HubCapacity`,
  `HubLocation`, `HubSkill`, `HubPlan`, `MaintenanceDefect`, `DefectType`, `CreateDvirRequest`,
  `DriverAuthToken`, `DriverQrCode`, and more

### Fixed

- **`CreateRouteRequest`** — removed non-existent `scheduledStartMs`/`scheduledEndMs` fields; added
  `externalIds`, `recomputeScheduledTimes`, `tagIds`; made `driverId` optional; `stops` is now required
- **`UpdateRouteRequest`** — same field corrections as above; added `stops` (was missing entirely)
- **`Route`** response — replaced deprecated ms-epoch time fields with ISO string equivalents
  (`scheduledRouteStartTime`, `scheduledRouteEndTime`, `actualRouteStartTime`, `actualRouteEndTime`);
  added `externalIds`, `tagIds`, `orgLocalTimezone`, `createdAt`, `updatedAt`, `dispatchRouteId`,
  `distanceMeters`, `durationSeconds`, `hubId`, `isEdited`, `isPinned`, `planId`, `type`, `quantities`
- **`RouteStop`** response — removed `addressId`, `latitude`, `longitude`; added `sequenceNumber`,
  `ontimeWindowAfterArrivalMs`, `ontimeWindowBeforeArrivalMs`, `enRouteTime`, `eta`, `skippedTime`,
  `actualDistanceMeters`, `plannedDistanceMeters`, `liveSharingUrl`, `address`, `hubLocationId`, `orders`
- **`RouteSettings`** — added `sequencingMethod`
- **`CreateRouteStopRequest`** — removed `latitude`/`longitude`; added `sequenceNumber`, ontime window fields
- **`UpdateRouteStopRequest`** — new class (was missing from the SDK entirely)
- **`Asset`** response — fixed `assetType` → `type`; added `createdAtTime`, `updatedAtTime`, `vin`,
  `readingsIngestionEnabled`, `regulationMode`, `serialNumber`
- **`CreateAssetRequest`** — renamed `assetType` → `type`; added `serialNumber`, `vin`,
  `readingsIngestionEnabled`, `regulationMode`, `attributes`
- **`UpdateAssetRequest`** — removed `id` and `tagIds` (neither belongs in the PATCH body);
  added `type`, `serialNumber`, `vin`, `readingsIngestionEnabled`, `regulationMode`
- **`Driver`** response — removed fabricated fields (`password`, `status`, `vehicleId`, `currentVehicleId`);
  added `isDeactivated`, `currentIdCardCode`, `profileImageUrl`, `attributes`, `eldSettings`,
  `hasDrivingFeaturesHidden`, `hasVehicleUnpinningEnabled`, `peerGroupTag`, `trailerGroupTag`,
  `vehicleGroupTag`, `usDriverRulesetOverride`, `waitingTimeDutyStatusEnabled`
- **`CreateDriverRequest`** / **`UpdateDriverRequest`** — removed non-existent `vehicleId`; added 14+
  missing fields including `staticAssignedVehicleId`, `currentIdCardCode`, `carrierSettings`, `hosSetting`,
  `usDriverRulesetOverride`, `peerGroupTagId`, `trailerGroupTagId`, `vehicleGroupTagId`, and more
- **`CreateDvirRequest`** — completely replaced v1 fields (`inspectorName`, `odometer`, `safeToOperate`,
  `trailerIds`) with correct v2 fields: `authorId` (required), `safetyStatus` (required), `type` (required),
  `vehicleId`, `trailerId`, `licensePlate`, `location`, `mechanicNotes`, `odometerMeters`, `resolvedDefectIds`
- **`UpdateDvirRequest`** — completely replaced v1 fields (`authorizedSignatoryId`, `safeToOperate`) with
  correct v2 fields: `authorId` (required), `isResolved` (required), `mechanicNotes`, `signedAtTime`
- **`SafetyEvent`** — replaced `behaviorLabel` (singular) with `behaviorLabels` (array); removed v1-only
  fields (`maxGForce`, `location`, `coachingState`, video download URLs); removed `SafetyEventLocation`
- **`CreateTagRequest`** / **`UpdateTagRequest`** — added `assets`, `machines`, `sensors`, `externalIds`
- **`CreateUserRequest`** — added `expireAt`
- **`UpdateUserRequest`** — added `authType`, `expireAt`
- **`UpdateVehicleRequest`** — added `auxInputType3`–`auxInputType13`, `engineHours`,
  `grossVehicleWeight`, `gatewaySerial`, `vehicleType`, `attributes`, `odometerMeters`
- **`WorkOrder`** / **`CreateWorkOrderRequest`** / **`UpdateWorkOrderRequest`** — completely rebuilt
  all three with correct API fields (`assetId`, `serviceTaskInstances`, `items`, `discount`, `tax`, etc.)

### Documentation

- **Full SDK-vs-spec audit (2026-05-21)** — added `docs/api-sync/full-sync-review-2026-05-21.md`,
  a mechanical comparison of every wired SDK endpoint and model against the live OpenAPI spec.
  Key finding: roughly **1 in 3 wired endpoints does not match the spec** (wrong URL path or
  fabricated operation). Domains previously marked "Complete" — **Tags, Gateways, Media,
  Messages, Sensors, Tachograph, Fuel and Energy, Training** — are mis-pathed and return 404.
  Corrected the status table in `docs/api-sync/README.md` and added a per-domain audit banner to
  33 checklist files. **No SDK runtime code has been changed yet** — fixes are pending review.
- **Correction to the `SafetyEvent` note above** — the SDK's `SafetyEvent` targets the v2
  endpoint (`getSafetyEventsV2`), whose schema *does* include `location` and
  `maxAccelerationGForce` (these are not "v1-only" as the note implies). The current model is a
  minimal v2 stub also missing `asset`, `eventState`, `media`, object-typed `behaviorLabels`, and
  timestamps — see the full review, Part 2.

## [0.1.0] - 2025-04-06

### Added

- Initial release of `Samsara.Sdk`
- Typed `ISamsaraClient` facade exposing 33 domain service clients
- `IAsyncEnumerable<T>` automatic cursor-based pagination across all list endpoints
- Source-generated `System.Text.Json` serialization (zero-reflection)
- `IHttpClientFactory` integration via `AddSamsaraClient()` extension method
- Built-in resilience via `Microsoft.Extensions.Http.Resilience` — exponential backoff with jitter
- Bearer token authentication via `SamsaraAuthenticationHandler`
- Dynamic `TokenProvider` delegate for OAuth 2.0 / rotating-token scenarios
- EU region support via `SamsaraClientOptions.EuBaseUrl`
- Typed exception hierarchy: `SamsaraBadRequestException`, `SamsaraAuthenticationException`,
  `SamsaraNotFoundException`, `SamsaraRateLimitException`, `SamsaraServerException`
- Configurable `RetryCount`, `Timeout`, `DefaultPageSize` options
- Immutable `record` request/response models for all API domains
- Domain clients: Tags, Addresses, Vehicles, Drivers, Safety, Routes, Compliance,
  Maintenance, Documents, Alerts, Fuel, Webhooks, Organization, Users, Contacts,
  Equipment, Industrial, Messages, Trailers, Gateways, UserRoles, Tachograph,
  IFTA, Hubs, Trips, Forms, Attributes, DriverVehicleAssignments,
  TrailerAssignments, CarrierProposedAssignments, Training, Sensors, Issues, Media
- `Samsara.Cli` interactive terminal tool (Spectre.Console TUI)
- XML documentation on all public API surface

[Unreleased]: https://github.com/TheEightBot/SamsaraDotnet/compare/v0.1.0...HEAD
[0.1.0]: https://github.com/TheEightBot/SamsaraDotnet/releases/tag/v0.1.0
