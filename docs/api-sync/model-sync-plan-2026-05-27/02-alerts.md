# Alerts — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/02-alerts.md`](../02-alerts.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `AlertConfiguration` | response | 0 | 6 | 3 | 1 |
| `AlertIncident` | response | 0 | 5 | 1 | 5 |
| `CreateAlertConfigurationRequest` | request | 0 | 0 | 2 | 0 |
| `UpdateAlertConfigurationRequest` | request | 0 | 0 | 2 | 0 |

**Counts**: CRITICAL=0, HIGH=11, MEDIUM=8, LOW=6  
**Total deduped findings**: 25

## HIGH (11)

### `AlertConfiguration` (response)

- **[response_drift_required]** AlertConfiguration (response) missing REQUIRED property `actions` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Add `[JsonPropertyName("actions")] public IReadOnlyList<object> Actions { get; init; }` to response record `AlertConfiguration` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertConfiguration (response) missing REQUIRED property `createdAtTime` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public string CreatedAtTime { get; init; }` to response record `AlertConfiguration` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertConfiguration (response) missing REQUIRED property `isEnabled` (spec type=boolean). (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Add `[JsonPropertyName("isEnabled")] public bool IsEnabled { get; init; }` to response record `AlertConfiguration` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertConfiguration (response) missing REQUIRED property `lastModifiedAtTime` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Add `[JsonPropertyName("lastModifiedAtTime")] public string LastModifiedAtTime { get; init; }` to response record `AlertConfiguration` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertConfiguration (response) missing REQUIRED property `scope` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Add `[JsonPropertyName("scope")] public object Scope { get; init; }` to response record `AlertConfiguration` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertConfiguration (response) missing REQUIRED property `triggers` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Add `[JsonPropertyName("triggers")] public IReadOnlyList<object> Triggers { get; init; }` to response record `AlertConfiguration` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `AlertIncident` (response)

- **[response_drift_required]** AlertIncident (response) missing REQUIRED property `conditions` (spec type=array).
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Add `[JsonPropertyName("conditions")] public IReadOnlyList<object> Conditions { get; init; }` to response record `AlertIncident` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertIncident (response) missing REQUIRED property `happenedAtTime` (spec type=string).
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Add `[JsonPropertyName("happenedAtTime")] public string HappenedAtTime { get; init; }` to response record `AlertIncident` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertIncident (response) missing REQUIRED property `incidentUrl` (spec type=string).
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Add `[JsonPropertyName("incidentUrl")] public string IncidentUrl { get; init; }` to response record `AlertIncident` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertIncident (response) missing REQUIRED property `isResolved` (spec type=boolean).
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Add `[JsonPropertyName("isResolved")] public bool IsResolved { get; init; }` to response record `AlertIncident` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** AlertIncident (response) missing REQUIRED property `updatedAtTime` (spec type=string).
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public string UpdatedAtTime { get; init; }` to response record `AlertIncident` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (8)

### `AlertConfiguration` (response)

- **[response_drift_optional]** AlertConfiguration (response) missing property `externalIds` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Add `[JsonPropertyName("externalIds")] public object? ExternalIds { get; init; }` to response record `AlertConfiguration`.
- **[response_drift_optional]** AlertConfiguration (response) missing property `operationalSettings` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Add `[JsonPropertyName("operationalSettings")] public object? OperationalSettings { get; init; }` to response record `AlertConfiguration`.
- **[response_required_drift]** AlertConfiguration.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Tighten `AlertConfiguration.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `AlertIncident` (response)

- **[response_required_drift]** AlertIncident.configurationId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Tighten `AlertIncident.ConfigurationId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `CreateAlertConfigurationRequest` (request)

- **[weak_typing]** CreateAlertConfigurationRequest.operationalSettings: SDK uses weak `object` for spec type `object`.
  - Endpoints: `POST /alerts/configurations`
  - Recommended fix: Replace weak `object?` with a typed model on `CreateAlertConfigurationRequest.OperationalSettings` (spec type=`object`).
- **[weak_typing]** CreateAlertConfigurationRequest.scope: SDK uses weak `object` for spec type `object`.
  - Endpoints: `POST /alerts/configurations`
  - Recommended fix: Replace weak `object?` with a typed model on `CreateAlertConfigurationRequest.Scope` (spec type=`object`).

### `UpdateAlertConfigurationRequest` (request)

- **[weak_typing]** UpdateAlertConfigurationRequest.operationalSettings: SDK uses weak `object` for spec type `object`.
  - Endpoints: `PATCH /alerts/configurations`
  - Recommended fix: Replace weak `object?` with a typed model on `UpdateAlertConfigurationRequest.OperationalSettings` (spec type=`object`).
- **[weak_typing]** UpdateAlertConfigurationRequest.scope: SDK uses weak `object` for spec type `object`.
  - Endpoints: `PATCH /alerts/configurations`
  - Recommended fix: Replace weak `object?` with a typed model on `UpdateAlertConfigurationRequest.Scope` (spec type=`object`).

## LOW (6)

### `AlertConfiguration` (response)

- **[extra_property]** AlertConfiguration.conditionType (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /alerts/configurations`, `PATCH /alerts/configurations`, `POST /alerts/configurations`
  - Recommended fix: Remove `AlertConfiguration.ConditionType` (not in spec).

### `AlertIncident` (response)

- **[extra_property]** AlertIncident.alertId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Remove `AlertIncident.AlertId` (not in spec).
- **[extra_property]** AlertIncident.driver (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Remove `AlertIncident.Driver` (not in spec).
- **[extra_property]** AlertIncident.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Remove `AlertIncident.Id` (not in spec).
- **[extra_property]** AlertIncident.triggeredAtTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Remove `AlertIncident.TriggeredAtTime` (not in spec).
- **[extra_property]** AlertIncident.vehicle (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /alerts/incidents/stream`
  - Recommended fix: Remove `AlertIncident.Vehicle` (not in spec).

