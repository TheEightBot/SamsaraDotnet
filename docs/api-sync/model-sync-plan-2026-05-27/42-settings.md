# Settings — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/42-settings.md`](../42-settings.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `3b60184` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. LOW response/request-side extras were
intentionally retained as nullable back-compat properties per the workflow
precedent established in `08-carrier-proposed-assignments`,
`13-driver-trailer-assignments`, `14-driver-vehicle-assignments`,
`28-live-sharing-links`, `29-location-and-speed`, `30-maintenance`,
`36-readings`, `39-safety`, and `40-safety-scores` — flat-scalar conveniences
present in the SDK but absent from the current spec inner schema kept (now
ordered after the spec props) rather than removed outright.

Files touched: `src/Samsara.Sdk/Models/Organization/SettingsModels.cs` only.
The five types are deserialize-/serialize-through models with no construction
sites in `src`/`tools`/`tests`, so the `SettingsClient` pass-through, the
`SamsaraJsonContext` registrations (no new top-level types — the new complex
fields are weakly-typed `object`), and the CLI/tests required no changes.

**HIGH (12)**

- **`SafetySettings` (response) — 12 spec-REQUIRED fields**: all added as
  `required` non-nullable. `defaultVehicleType` → `required string`,
  `safetyScoreTarget` → `required long`, and the ten deeply-nested config blobs
  (`distractedDrivingDetectionAlerts`, `followingDistanceDetectionAlerts`,
  `forwardCollisionDetectionAlerts`, `harshEventSensitivity`,
  `harshEventSensitivityV2`, `policyViolationsDetectionAlerts`,
  `rollingStopDetectionAlerts`, `safetyScoreConfiguration`, `speedingSettings`,
  `voiceCoaching`) → `required object`, left weakly-typed per the plan (the
  repo convention of not schematizing config the plan left as `object`).
  Safe because `SafetySettings` is deserialize-only (no construction sites).

**MEDIUM (32)**

- **`ComplianceSettings` (response) — 10 fields** and
  **`UpdateComplianceSettingsRequest` (request) — 10 fields**: added as nullable
  (`allowUnregulatedVehiclesEnabled`, `canadaHosEnabled`, `carrierName`,
  `dotNumber` (`long?`), `driverAutoDutyEnabled`, `editCertifiedLogsEnabled`,
  `forceManualLocationForDutyStatusChangesEnabled`,
  `forceReviewUnassignedHosEnabled`, `mainOfficeFormattedAddress`,
  `persistentDutyStatusEnabled`).
- **`DriverAppSettings` (response) — 6 fields** and
  **`UpdateDriverAppSettingsRequest` (request) — 6 fields**: added as nullable
  (`driverFleetId`, `gamification`, `gamificationConfig` (`object?`),
  `orgVehicleSearch`, `trailerSelection`, `trailerSelectionConfig` (`object?`)).

**LOW (25) — kept as nullable back-compat extras (conservative; not removed)**

- `ComplianceSettings` / `UpdateComplianceSettingsRequest`: `hosEnabled`,
  `dvirEnabled`, `eldExemptEnabled`, `defaultCycleRule`, `defaultHosRule` (5 each).
- `DriverAppSettings`: `messageEnabled`, `navigationEnabled`,
  `driverRewardsEnabled`, `vehiclePreviewEnabled`, `coachingAlertsEnabled` (5).
- `UpdateDriverAppSettingsRequest`: `messageEnabled`, `navigationEnabled`,
  `driverRewardsEnabled`, `vehiclePreviewEnabled` (4).
- `SafetySettings`: `forwardCollisionWarningEnabled`, `laneDepartureWarningEnabled`,
  `speedingEnabled`, `harshAccelerationEnabled`, `harshBrakingEnabled`,
  `harshCorneringEnabled` (6).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `SafetySettings` | response | 0 | 12 | 0 | 6 |
| `ComplianceSettings` | response | 0 | 0 | 10 | 5 |
| `UpdateComplianceSettingsRequest` | request | 0 | 0 | 10 | 5 |
| `DriverAppSettings` | response | 0 | 0 | 6 | 5 |
| `UpdateDriverAppSettingsRequest` | request | 0 | 0 | 6 | 4 |

**Counts**: CRITICAL=0, HIGH=12, MEDIUM=32, LOW=25  
**Total deduped findings**: 69

## HIGH (12)

### `SafetySettings` (response)

- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `defaultVehicleType` (spec type=string).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("defaultVehicleType")] public string DefaultVehicleType { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `distractedDrivingDetectionAlerts` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("distractedDrivingDetectionAlerts")] public object DistractedDrivingDetectionAlerts { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `followingDistanceDetectionAlerts` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("followingDistanceDetectionAlerts")] public object FollowingDistanceDetectionAlerts { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `forwardCollisionDetectionAlerts` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("forwardCollisionDetectionAlerts")] public object ForwardCollisionDetectionAlerts { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `harshEventSensitivity` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("harshEventSensitivity")] public object HarshEventSensitivity { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `harshEventSensitivityV2` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("harshEventSensitivityV2")] public object HarshEventSensitivityV2 { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `policyViolationsDetectionAlerts` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("policyViolationsDetectionAlerts")] public object PolicyViolationsDetectionAlerts { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `rollingStopDetectionAlerts` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("rollingStopDetectionAlerts")] public object RollingStopDetectionAlerts { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `safetyScoreConfiguration` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("safetyScoreConfiguration")] public object SafetyScoreConfiguration { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `safetyScoreTarget` (spec type=integer/int64).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("safetyScoreTarget")] public long SafetyScoreTarget { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `speedingSettings` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("speedingSettings")] public object SpeedingSettings { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetySettings (response) missing REQUIRED property `voiceCoaching` (spec type=object).
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Add `[JsonPropertyName("voiceCoaching")] public object VoiceCoaching { get; init; }` to response record `SafetySettings` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (32)

### `ComplianceSettings` (response)

- **[response_drift_optional]** ComplianceSettings (response) missing property `allowUnregulatedVehiclesEnabled` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("allowUnregulatedVehiclesEnabled")] public bool? AllowUnregulatedVehiclesEnabled { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `canadaHosEnabled` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("canadaHosEnabled")] public bool? CanadaHosEnabled { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `carrierName` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("carrierName")] public string? CarrierName { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `dotNumber` (spec type=integer/int64). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("dotNumber")] public long? DotNumber { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `driverAutoDutyEnabled` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("driverAutoDutyEnabled")] public bool? DriverAutoDutyEnabled { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `editCertifiedLogsEnabled` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("editCertifiedLogsEnabled")] public bool? EditCertifiedLogsEnabled { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `forceManualLocationForDutyStatusChangesEnabled` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("forceManualLocationForDutyStatusChangesEnabled")] public bool? ForceManualLocationForDutyStatusChangesEnabled { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `forceReviewUnassignedHosEnabled` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("forceReviewUnassignedHosEnabled")] public bool? ForceReviewUnassignedHosEnabled { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `mainOfficeFormattedAddress` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("mainOfficeFormattedAddress")] public string? MainOfficeFormattedAddress { get; init; }` to response record `ComplianceSettings`.
- **[response_drift_optional]** ComplianceSettings (response) missing property `persistentDutyStatusEnabled` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("persistentDutyStatusEnabled")] public bool? PersistentDutyStatusEnabled { get; init; }` to response record `ComplianceSettings`.

### `DriverAppSettings` (response)

- **[response_drift_optional]** DriverAppSettings (response) missing property `driverFleetId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("driverFleetId")] public string? DriverFleetId { get; init; }` to response record `DriverAppSettings`.
- **[response_drift_optional]** DriverAppSettings (response) missing property `gamification` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("gamification")] public bool? Gamification { get; init; }` to response record `DriverAppSettings`.
- **[response_drift_optional]** DriverAppSettings (response) missing property `gamificationConfig` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("gamificationConfig")] public object? GamificationConfig { get; init; }` to response record `DriverAppSettings`.
- **[response_drift_optional]** DriverAppSettings (response) missing property `orgVehicleSearch` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("orgVehicleSearch")] public bool? OrgVehicleSearch { get; init; }` to response record `DriverAppSettings`.
- **[response_drift_optional]** DriverAppSettings (response) missing property `trailerSelection` (spec type=boolean). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("trailerSelection")] public bool? TrailerSelection { get; init; }` to response record `DriverAppSettings`.
- **[response_drift_optional]** DriverAppSettings (response) missing property `trailerSelectionConfig` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("trailerSelectionConfig")] public object? TrailerSelectionConfig { get; init; }` to response record `DriverAppSettings`.

### `UpdateComplianceSettingsRequest` (request)

- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `allowUnregulatedVehiclesEnabled` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("allowUnregulatedVehiclesEnabled")] public bool? AllowUnregulatedVehiclesEnabled { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `canadaHosEnabled` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("canadaHosEnabled")] public bool? CanadaHosEnabled { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `carrierName` (spec type=string).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("carrierName")] public string? CarrierName { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `dotNumber` (spec type=integer/int64).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("dotNumber")] public long? DotNumber { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `driverAutoDutyEnabled` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("driverAutoDutyEnabled")] public bool? DriverAutoDutyEnabled { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `editCertifiedLogsEnabled` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("editCertifiedLogsEnabled")] public bool? EditCertifiedLogsEnabled { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `forceManualLocationForDutyStatusChangesEnabled` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("forceManualLocationForDutyStatusChangesEnabled")] public bool? ForceManualLocationForDutyStatusChangesEnabled { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `forceReviewUnassignedHosEnabled` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("forceReviewUnassignedHosEnabled")] public bool? ForceReviewUnassignedHosEnabled { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `mainOfficeFormattedAddress` (spec type=string).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("mainOfficeFormattedAddress")] public string? MainOfficeFormattedAddress { get; init; }` to `UpdateComplianceSettingsRequest`.
- **[missing_optional]** UpdateComplianceSettingsRequest is missing property `persistentDutyStatusEnabled` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Add `[JsonPropertyName("persistentDutyStatusEnabled")] public bool? PersistentDutyStatusEnabled { get; init; }` to `UpdateComplianceSettingsRequest`.

### `UpdateDriverAppSettingsRequest` (request)

- **[missing_optional]** UpdateDriverAppSettingsRequest is missing property `driverFleetId` (spec type=string).
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("driverFleetId")] public string? DriverFleetId { get; init; }` to `UpdateDriverAppSettingsRequest`.
- **[missing_optional]** UpdateDriverAppSettingsRequest is missing property `gamification` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("gamification")] public bool? Gamification { get; init; }` to `UpdateDriverAppSettingsRequest`.
- **[missing_optional]** UpdateDriverAppSettingsRequest is missing property `gamificationConfig` (spec type=object).
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("gamificationConfig")] public object? GamificationConfig { get; init; }` to `UpdateDriverAppSettingsRequest`.
- **[missing_optional]** UpdateDriverAppSettingsRequest is missing property `orgVehicleSearch` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("orgVehicleSearch")] public bool? OrgVehicleSearch { get; init; }` to `UpdateDriverAppSettingsRequest`.
- **[missing_optional]** UpdateDriverAppSettingsRequest is missing property `trailerSelection` (spec type=boolean).
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("trailerSelection")] public bool? TrailerSelection { get; init; }` to `UpdateDriverAppSettingsRequest`.
- **[missing_optional]** UpdateDriverAppSettingsRequest is missing property `trailerSelectionConfig` (spec type=object).
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Add `[JsonPropertyName("trailerSelectionConfig")] public object? TrailerSelectionConfig { get; init; }` to `UpdateDriverAppSettingsRequest`.

## LOW (25)

### `ComplianceSettings` (response)

- **[extra_property]** ComplianceSettings.defaultCycleRule (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `ComplianceSettings.DefaultCycleRule` (not in spec).
- **[extra_property]** ComplianceSettings.defaultHosRule (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `ComplianceSettings.DefaultHosRule` (not in spec).
- **[extra_property]** ComplianceSettings.dvirEnabled (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `ComplianceSettings.DvirEnabled` (not in spec).
- **[extra_property]** ComplianceSettings.eldExemptEnabled (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `ComplianceSettings.EldExemptEnabled` (not in spec).
- **[extra_property]** ComplianceSettings.hosEnabled (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/compliance`, `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `ComplianceSettings.HosEnabled` (not in spec).

### `DriverAppSettings` (response)

- **[extra_property]** DriverAppSettings.coachingAlertsEnabled (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `DriverAppSettings.CoachingAlertsEnabled` (not in spec).
- **[extra_property]** DriverAppSettings.driverRewardsEnabled (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `DriverAppSettings.DriverRewardsEnabled` (not in spec).
- **[extra_property]** DriverAppSettings.messageEnabled (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `DriverAppSettings.MessageEnabled` (not in spec).
- **[extra_property]** DriverAppSettings.navigationEnabled (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `DriverAppSettings.NavigationEnabled` (not in spec).
- **[extra_property]** DriverAppSettings.vehiclePreviewEnabled (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/settings/driver-app`, `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `DriverAppSettings.VehiclePreviewEnabled` (not in spec).

### `SafetySettings` (response)

- **[extra_property]** SafetySettings.forwardCollisionWarningEnabled (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Remove `SafetySettings.ForwardCollisionWarningEnabled` (not in spec).
- **[extra_property]** SafetySettings.harshAccelerationEnabled (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Remove `SafetySettings.HarshAccelerationEnabled` (not in spec).
- **[extra_property]** SafetySettings.harshBrakingEnabled (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Remove `SafetySettings.HarshBrakingEnabled` (not in spec).
- **[extra_property]** SafetySettings.harshCorneringEnabled (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Remove `SafetySettings.HarshCorneringEnabled` (not in spec).
- **[extra_property]** SafetySettings.laneDepartureWarningEnabled (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Remove `SafetySettings.LaneDepartureWarningEnabled` (not in spec).
- **[extra_property]** SafetySettings.speedingEnabled (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/settings/safety`
  - Recommended fix: Remove `SafetySettings.SpeedingEnabled` (not in spec).

### `UpdateComplianceSettingsRequest` (request)

- **[extra_property]** UpdateComplianceSettingsRequest.defaultCycleRule: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `UpdateComplianceSettingsRequest.DefaultCycleRule` (not in spec).
- **[extra_property]** UpdateComplianceSettingsRequest.defaultHosRule: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `UpdateComplianceSettingsRequest.DefaultHosRule` (not in spec).
- **[extra_property]** UpdateComplianceSettingsRequest.dvirEnabled: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `UpdateComplianceSettingsRequest.DvirEnabled` (not in spec).
- **[extra_property]** UpdateComplianceSettingsRequest.eldExemptEnabled: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `UpdateComplianceSettingsRequest.EldExemptEnabled` (not in spec).
- **[extra_property]** UpdateComplianceSettingsRequest.hosEnabled: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/compliance`
  - Recommended fix: Remove `UpdateComplianceSettingsRequest.HosEnabled` (not in spec).

### `UpdateDriverAppSettingsRequest` (request)

- **[extra_property]** UpdateDriverAppSettingsRequest.driverRewardsEnabled: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `UpdateDriverAppSettingsRequest.DriverRewardsEnabled` (not in spec).
- **[extra_property]** UpdateDriverAppSettingsRequest.messageEnabled: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `UpdateDriverAppSettingsRequest.MessageEnabled` (not in spec).
- **[extra_property]** UpdateDriverAppSettingsRequest.navigationEnabled: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `UpdateDriverAppSettingsRequest.NavigationEnabled` (not in spec).
- **[extra_property]** UpdateDriverAppSettingsRequest.vehiclePreviewEnabled: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /fleet/settings/driver-app`
  - Recommended fix: Remove `UpdateDriverAppSettingsRequest.VehiclePreviewEnabled` (not in spec).

