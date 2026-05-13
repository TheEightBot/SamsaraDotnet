# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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

- `SafetyClient` — corrected all API endpoint paths (were using old/incorrect paths)

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
