# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed

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
