---
goal: Expand Samsara SDK API coverage with new endpoints from the latest OpenAPI spec
version: 1.0
date_created: 2026-04-27
last_updated: 2026-04-27
owner: TheEightBot
status: 'In progress'
tags: [feature, api, clients, models]
---

# Introduction

![Status: In progress](https://img.shields.io/badge/status-In%20progress-yellow)

Expand `Samsara.Sdk` v0.1.0 to match the latest Samsara OpenAPI specification. The spec has grown significantly since the initial release: 14+ entirely new resource domains need new client interfaces, and 12 existing clients are missing operations. Beta endpoints are included and annotated with XML doc warnings. This plan covers every client interface, model, DI registration, JSON context entry, and unit test required.

---

## 1. Requirements & Constraints

- **REQ-001**: All new public API surface must follow existing patterns: `sealed record` models, `IAsyncEnumerable<T>` for lists, `Task<T>` for single items, `CancellationToken` optional last parameter.
- **REQ-002**: Beta endpoints must be clearly marked with `<remarks>⚠ Beta API</remarks>` in XML documentation on both the interface and the `ISamsaraClient` property.
- **REQ-003**: Source-generated JSON (`SamsaraJsonContext`) must include `[JsonSerializable]` for every new model type.
- **REQ-004**: Every new client must be registered in `ServiceCollectionExtensions.AddSamsaraClient()` and in `SamsaraClient` constructor/properties.
- **REQ-005**: Unit tests must follow the pattern in `TagsClientTests`: mock HTTP handler, verify path/method/deserialization.
- **REQ-006**: `SamsaraClientFacadeTests` must be updated to include all new clients.
- **CON-001**: Project targets `net8.0` and `netstandard2.0`; no `[Experimental]` attribute (net8+ only). Use XML `<remarks>` for beta warnings instead.
- **CON-002**: No new NuGet dependencies.
- **GUD-001**: Model files are grouped by domain under `src/Samsara.Sdk/Models/<Domain>/`, one file per domain named `<Domain>Models.cs`.
- **GUD-002**: Client implementation files live under `src/Samsara.Sdk/Clients/<Domain>/` and follow `internal sealed class XClient : SamsaraServiceClientBase, IXClient`.
- **PAT-001**: All new model types use `sealed record` with `[JsonPropertyName]` annotations and `required` for mandatory fields.

---

## 2. Implementation Steps

### Implementation Phase 1 — Update Existing Clients

- GOAL-001: Fill gaps in already-shipped client interfaces that have missing CRUD operations or new list endpoints.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-001 | Add `CreateAsync(CreateVehicleRequest, CancellationToken) → Task<Vehicle>` to `IVehiclesClient` and `VehiclesClient` (path `fleet/vehicles`). Add `CreateVehicleRequest` model to `FleetModels.cs`. | ✅ | 2026-04-27 |
| TASK-002 | Add `CreateAsync`, `UpdateAsync(id, UpdateEquipmentRequest)`, `DeleteAsync(id)` to `IEquipmentClient` and `EquipmentClient` (paths `fleet/equipment`, `fleet/equipment/{id}`). Add `CreateEquipmentRequest`, `UpdateEquipmentRequest` to `FleetModels.cs`. | ✅ | 2026-04-27 |
| TASK-003 | Add `ListHosDailyLogsAsync`, `ListHosEldEventsAsync` to `IComplianceClient` and `ComplianceClient` (paths `fleet/hos/daily-logs`, `fleet/hos/eld-events`). Add `HosDailyLog`, `HosEldEvent` to `ComplianceModels.cs`. | ✅ | 2026-04-27 |
| TASK-004 | Add `CreateAsync`, `UpdateAsync`, `DeleteAsync`, `CreateConfigurationAsync`, `UpdateConfigurationAsync`, `DeleteConfigurationAsync`, `ListIncidentsAsync`, `GetIncidentAsync` to `IAlertsClient` and `AlertsClient`. Add `CreateAlertRequest`, `UpdateAlertRequest`, `CreateAlertConfigurationRequest`, `UpdateAlertConfigurationRequest`, `AlertIncident` to `CommunicationModels.cs`. | ✅ | 2026-04-27 |
| TASK-005 | Add `ListTagSafetyScoresAsync`, `ListTagGroupSafetyScoresAsync` to `ISafetyClient` and `SafetyClient`. Add `TagSafetyScore`, `TagGroupSafetyScore` to `SafetyModels.cs`. | ✅ | 2026-04-27 |

### Implementation Phase 2 — New Stable Client Interfaces

- GOAL-002: Implement new client interfaces and implementations for stable (non-beta) API areas.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-006 | Create `IAssetsClient` + `AssetsClient` under `Clients/Fleet/`. Paths: `GET/POST/PATCH fleet/assets`, `GET fleet/assets/inputs`. Models: `FleetAsset`, `CreateFleetAssetRequest`, `UpdateFleetAssetRequest`, `AssetInput` in `Models/Fleet/AssetModels.cs`. | ✅ | 2026-04-27 |
| TASK-007 | Create `ILiveSharingLinksClient` + `LiveSharingLinksClient` under `Clients/Fleet/`. Paths: full CRUD on `fleet/live-sharing-links`. Models: `LiveSharingLink`, `CreateLiveSharingLinkRequest`, `UpdateLiveSharingLinkRequest` in `Models/Fleet/LiveSharingModels.cs`. | ✅ | 2026-04-27 |
| TASK-008 | Create `IQualificationsClient` + `QualificationsClient` under `Clients/Fleet/`. Paths: `GET fleet/qualifications/types`, `GET/POST fleet/qualifications/records`. Models: `QualificationType`, `QualificationRecord`, `CreateQualificationRecordRequest` in `Models/People/QualificationModels.cs`. | ✅ | 2026-04-27 |
| TASK-009 | Create `ISettingsClient` + `SettingsClient` under `Clients/Organization/`. Paths: `GET/PATCH settings/compliance`, `GET/PATCH settings/driver-app`, `GET/PATCH settings/safety`. Models in `Models/Organization/SettingsModels.cs`. | ✅ | 2026-04-27 |
| TASK-010 | Create `IJobsClient` + `JobsClient` under `Clients/Routing/`. Paths: full CRUD on `routing/jobs`. Models: `RoutingJob`, `CreateRoutingJobRequest`, `UpdateRoutingJobRequest` in `Models/Routing/RoutingJobModels.cs`. | ✅ | 2026-04-27 |
| TASK-011 | Create `IPlanOrdersClient` + `PlanOrdersClient` under `Clients/Routing/`. Paths: `GET/POST routing/plan-orders`. Models: `PlanOrder`, `CreatePlanOrderRequest` in `Models/Routing/PlanOrderModels.cs`. | ✅ | 2026-04-27 |

### Implementation Phase 3 — New Beta Client Interfaces

- GOAL-003: Implement new clients for beta API areas. All interface/property XML docs must include the beta warning remark.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-012 | Create `IDriverQrCodesClient` + `DriverQrCodesClient` under `Clients/Fleet/`. Paths: `GET/POST/DELETE beta/fleet/driver-qr-codes`. Models: `DriverQrCode`, `CreateDriverQrCodeRequest` in `Models/Drivers/DriverExtendedModels.cs`. | ✅ | 2026-04-27 |
| TASK-013 | Create `IDriverEfficiencyClient` + `DriverEfficiencyClient` under `Clients/Fleet/`. Path: `GET beta/fleet/driver-efficiency`. Models: `DriverEfficiency`, `DriverEfficiencyEntry` in `Models/Drivers/DriverExtendedModels.cs`. | ✅ | 2026-04-27 |
| TASK-014 | Create `IDriverCoachAssignmentsClient` + `DriverCoachAssignmentsClient` under `Clients/Fleet/`. Path: `GET beta/fleet/driver-coach-assignments`. Models: `DriverCoachAssignment` in `Models/Drivers/DriverExtendedModels.cs`. | ✅ | 2026-04-27 |
| TASK-015 | Create `IEngineImmobilizerClient` + `EngineImmobilizerClient` under `Clients/Fleet/`. Path: `GET beta/fleet/engine-immobilizer/states`. Models: `EngineImmobilizerState` in `Models/Fleet/EngineImmobilizerModels.cs`. | ✅ | 2026-04-27 |
| TASK-016 | Create `ICarbCtcClient` + `CarbCtcClient` under `Clients/Compliance/`. Paths: `GET beta/compliance/carb-ctc/vehicles`, `GET beta/compliance/carb-ctc/vehicles/{id}/history`. Models: `CarbCtcVehicle`, `CarbCtcHistoryEntry` in `Models/Compliance/CarbCtcModels.cs`. | ✅ | 2026-04-27 |
| TASK-017 | Create `IDetectionLogClient` + `DetectionLogClient` under `Clients/Safety/`. Path: `GET beta/safety/detection-logs`. Models: `DetectionLog` in `Models/Safety/DetectionLogModels.cs`. | ✅ | 2026-04-27 |
| TASK-018 | Create `ISpeedingIntervalsClient` + `SpeedingIntervalsClient` under `Clients/Safety/`. Path: `GET beta/fleet/speeding-intervals`. Models: `SpeedingInterval` in `Models/Safety/SpeedingModels.cs`. | ✅ | 2026-04-27 |
| TASK-019 | Create `IRidershipClient` + `RidershipClient` under `Clients/People/`. Paths: full CRUD on `beta/ridership/accounts`, `beta/ridership/passengers`, `beta/ridership/route-setups`. Models in `Models/People/RidershipModels.cs`. | ✅ | 2026-04-27 |

### Implementation Phase 4 — Infrastructure

- GOAL-004: Wire up all new types into the serialization context, DI container, and aggregate facade.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-020 | Add `[JsonSerializable]` entries for all new model types in `SamsaraJsonContext.cs`. | ✅ | 2026-04-27 |
| TASK-021 | Update `ISamsaraClient` with 14 new properties for all new clients. | ✅ | 2026-04-27 |
| TASK-022 | Update `SamsaraClient` constructor and properties to include all 14 new clients. | ✅ | 2026-04-27 |
| TASK-023 | Update `ServiceCollectionExtensions.AddSamsaraClient()` with `TryAddScoped` registrations for all new clients. | ✅ | 2026-04-27 |

### Implementation Phase 5 — Tests

- GOAL-005: Add unit tests for all new and updated clients.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-024 | Add tests for updated `IVehiclesClient` (CreateAsync path/method) and `IEquipmentClient` (full CRUD). | ✅ | 2026-04-27 |
| TASK-025 | Add tests for updated `IAlertsClient` (create/update/delete, incidents) and `ISafetyClient` (tag scores). | ✅ | 2026-04-27 |
| TASK-026 | Add tests for all new stable clients (Assets, LiveSharingLinks, Qualifications, Settings, Jobs, PlanOrders). | ✅ | 2026-04-27 |
| TASK-027 | Add tests for all new beta clients (DriverQrCodes, DriverEfficiency, DriverCoachAssignments, EngineImmobilizer, CarbCtc, DetectionLog, SpeedingIntervals, Ridership). | ✅ | 2026-04-27 |
| TASK-028 | Update `SamsaraClientFacadeTests` to construct `SamsaraClient` with all new clients and assert all new properties. | ✅ | 2026-04-27 |
| TASK-029 | Update `ServiceCollectionExtensionsTests` if it asserts on registered service count. | ✅ | 2026-04-27 |

---

## 3. Alternatives

- **ALT-001**: Use `[Obsolete]` on beta interfaces — rejected; `[Obsolete]` implies deprecation, not beta status. XML doc `<remarks>` is more accurate and cross-framework.
- **ALT-002**: Group beta clients under a `Beta` sub-interface on `ISamsaraClient` (e.g., `client.Beta.DriverQrCodes`) — rejected; adds unnecessary nesting and breaks the flat pattern established by the existing 35 clients.
- **ALT-003**: Skip beta endpoints entirely — rejected per user requirement.

---

## 4. Dependencies

- **DEP-001**: No new NuGet packages required.
- **DEP-002**: All new models depend on `Samsara.Sdk.Models.Common` for `TagReference`, `SamsaraResponse<T>`, `SamsaraListResponse<T>`.

---

## 5. Files

### New Files
- `src/Samsara.Sdk/Models/Fleet/AssetModels.cs`
- `src/Samsara.Sdk/Models/Fleet/LiveSharingModels.cs`
- `src/Samsara.Sdk/Models/Fleet/EngineImmobilizerModels.cs`
- `src/Samsara.Sdk/Models/Drivers/DriverExtendedModels.cs`
- `src/Samsara.Sdk/Models/Compliance/CarbCtcModels.cs`
- `src/Samsara.Sdk/Models/Safety/DetectionLogModels.cs`
- `src/Samsara.Sdk/Models/Safety/SpeedingModels.cs`
- `src/Samsara.Sdk/Models/People/QualificationModels.cs`
- `src/Samsara.Sdk/Models/People/RidershipModels.cs`
- `src/Samsara.Sdk/Models/Organization/SettingsModels.cs`
- `src/Samsara.Sdk/Models/Routing/RoutingJobModels.cs`
- `src/Samsara.Sdk/Models/Routing/PlanOrderModels.cs`
- `src/Samsara.Sdk/Clients/Fleet/IAssetsClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/AssetsClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/ILiveSharingLinksClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/LiveSharingLinksClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/IDriverQrCodesClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/DriverQrCodesClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/IDriverEfficiencyClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/DriverEfficiencyClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/IDriverCoachAssignmentsClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/DriverCoachAssignmentsClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/IEngineImmobilizerClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/EngineImmobilizerClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/IQualificationsClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/QualificationsClient.cs`
- `src/Samsara.Sdk/Clients/Compliance/ICarbCtcClient.cs`
- `src/Samsara.Sdk/Clients/Compliance/CarbCtcClient.cs`
- `src/Samsara.Sdk/Clients/Safety/IDetectionLogClient.cs`
- `src/Samsara.Sdk/Clients/Safety/DetectionLogClient.cs`
- `src/Samsara.Sdk/Clients/Safety/ISpeedingIntervalsClient.cs`
- `src/Samsara.Sdk/Clients/Safety/SpeedingIntervalsClient.cs`
- `src/Samsara.Sdk/Clients/Organization/ISettingsClient.cs`
- `src/Samsara.Sdk/Clients/Organization/SettingsClient.cs`
- `src/Samsara.Sdk/Clients/Routing/IJobsClient.cs`
- `src/Samsara.Sdk/Clients/Routing/JobsClient.cs`
- `src/Samsara.Sdk/Clients/Routing/IPlanOrdersClient.cs`
- `src/Samsara.Sdk/Clients/Routing/PlanOrdersClient.cs`
- `src/Samsara.Sdk/Clients/People/IRidershipClient.cs`
- `src/Samsara.Sdk/Clients/People/RidershipClient.cs`
- `tests/Samsara.Sdk.Tests/AssetsClientTests.cs`
- `tests/Samsara.Sdk.Tests/LiveSharingLinksClientTests.cs`
- `tests/Samsara.Sdk.Tests/QualificationsClientTests.cs`
- `tests/Samsara.Sdk.Tests/SettingsClientTests.cs`
- `tests/Samsara.Sdk.Tests/JobsClientTests.cs`
- `tests/Samsara.Sdk.Tests/PlanOrdersClientTests.cs`
- `tests/Samsara.Sdk.Tests/BetaDriverQrCodesClientTests.cs`
- `tests/Samsara.Sdk.Tests/BetaDriverEfficiencyClientTests.cs`
- `tests/Samsara.Sdk.Tests/BetaEngineImmobilizerClientTests.cs`
- `tests/Samsara.Sdk.Tests/BetaCarbCtcClientTests.cs`
- `tests/Samsara.Sdk.Tests/BetaDetectionLogClientTests.cs`
- `tests/Samsara.Sdk.Tests/BetaSpeedingIntervalsClientTests.cs`
- `tests/Samsara.Sdk.Tests/BetaRidershipClientTests.cs`

### Modified Files
- `src/Samsara.Sdk/Clients/Fleet/IVehiclesClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/VehiclesClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/IEquipmentClient.cs`
- `src/Samsara.Sdk/Clients/Fleet/EquipmentClient.cs`
- `src/Samsara.Sdk/Clients/Compliance/IComplianceClient.cs`
- `src/Samsara.Sdk/Clients/Compliance/ComplianceClient.cs`
- `src/Samsara.Sdk/Clients/Communication/IAlertsClient.cs`
- `src/Samsara.Sdk/Clients/Communication/AlertsClient.cs`
- `src/Samsara.Sdk/Clients/Safety/ISafetyClient.cs`
- `src/Samsara.Sdk/Clients/Safety/SafetyClient.cs`
- `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`
- `src/Samsara.Sdk/Models/Compliance/ComplianceModels.cs`
- `src/Samsara.Sdk/Models/Communication/CommunicationModels.cs`
- `src/Samsara.Sdk/Models/Safety/SafetyModels.cs`
- `src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`
- `src/Samsara.Sdk/Clients/ISamsaraClient.cs`
- `src/Samsara.Sdk/Clients/SamsaraClient.cs`
- `src/Samsara.Sdk/DependencyInjection/ServiceCollectionExtensions.cs`
- `tests/Samsara.Sdk.Tests/SamsaraClientFacadeTests.cs`
- `CHANGELOG.md`

---

## 6. Testing

- **TEST-001**: Each new client has at minimum one test per public method covering: correct HTTP method, correct path, and correct deserialization.
- **TEST-002**: `SamsaraClientFacadeTests.AllProperties_ReturnInjectedInstances` is updated with all 14 new clients.
- **TEST-003**: All tests run via `dotnet test` with zero failures.

---

## 7. Risks & Assumptions

- **RISK-001**: Beta endpoint paths may differ slightly from what the spec preview showed; implementations use the most recent paths from the spec analysis and are easily adjusted.
- **ASSUMPTION-001**: The Samsara API returns `{ "data": T }` envelope for all single-resource and `{ "data": [...], "pagination": {...} }` for paginated responses, consistent with all existing endpoints.
- **ASSUMPTION-002**: Beta endpoints follow the same auth and base URL structure as stable endpoints.

---

## 8. Related Specifications / Further Reading

- [Samsara OpenAPI Spec](https://developers.samsara.com/openapi/samsara-api.json)
- [Samsara Developer Docs](https://developers.samsara.com/docs)
