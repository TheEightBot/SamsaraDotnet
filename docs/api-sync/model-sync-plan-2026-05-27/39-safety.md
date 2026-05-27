# Safety — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/39-safety.md`](../39-safety.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `SafetyEvent` | response | 0 | 11 | 11 | 2 |
| `(no SDK type)` | query | 0 | 5 | 13 | 0 |

**Counts**: CRITICAL=0, HIGH=16, MEDIUM=24, LOW=2  
**Total deduped findings**: 42

## HIGH (16)

### `(no SDK type)` (query)

- **[missing_required_query]** V1GetDriverSafetyScoreAsync (GET /v1/fleet/drivers/{driverId}/safety/score) is missing query parameter `endMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/drivers/{driverId}/safety/score`
  - Recommended fix: Add a required parameter (e.g. `int endMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endMs", ...)`.
- **[missing_required_query]** V1GetVehicleSafetyScoreAsync (GET /v1/fleet/vehicles/{vehicleId}/safety/score) is missing query parameter `endMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/vehicles/{vehicleId}/safety/score`
  - Recommended fix: Add a required parameter (e.g. `int endMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endMs", ...)`.
- **[missing_required_query]** ListEventsAsync (GET /safety-events) is missing query parameter `safetyEventIds` (spec REQUIRED, type=array).
  - Endpoints: `GET /safety-events`
  - Recommended fix: Add a required parameter (e.g. `IReadOnlyList<string> safetyEventIds` , no default) to the SDK method and append it via `QueryBuilder.WithParams("safetyEventIds", ...)`.
- **[missing_required_query]** V1GetDriverSafetyScoreAsync (GET /v1/fleet/drivers/{driverId}/safety/score) is missing query parameter `startMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/drivers/{driverId}/safety/score`
  - Recommended fix: Add a required parameter (e.g. `int startMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startMs", ...)`.
- **[missing_required_query]** V1GetVehicleSafetyScoreAsync (GET /v1/fleet/vehicles/{vehicleId}/safety/score) is missing query parameter `startMs` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/vehicles/{vehicleId}/safety/score`
  - Recommended fix: Add a required parameter (e.g. `int startMs` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startMs", ...)`.

### `SafetyEvent` (response)

- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `asset` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("asset")] public object Asset { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `contextLabels` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("contextLabels")] public IReadOnlyList<object> ContextLabels { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `createdAtTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public string CreatedAtTime { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `endMs` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("endMs")] public string EndMs { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `eventState` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("eventState")] public string EventState { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `inboxEventUrl` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("inboxEventUrl")] public string InboxEventUrl { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `incidentReportUrl` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("incidentReportUrl")] public string IncidentReportUrl { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `location` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("location")] public object Location { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `maxAccelerationGForce` (spec type=number/double). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("maxAccelerationGForce")] public double MaxAccelerationGForce { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `startMs` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("startMs")] public string StartMs { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** SafetyEvent (response) missing REQUIRED property `updatedAtTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public string UpdatedAtTime { get; init; }` to response record `SafetyEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (24)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `assignedCoaches` (spec optional, type=array).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? assignedCoaches = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `behaviorLabels` (spec optional, type=array).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? behaviorLabels = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `eventStates` (spec optional, type=array).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? eventStates = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /safety-events) is missing query parameter `includeAsset` (spec optional, type=boolean).
  - Endpoints: `GET /safety-events`
  - Recommended fix: Add an optional parameter `bool? includeAsset = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `includeAsset` (spec optional, type=boolean).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `bool? includeAsset = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /safety-events) is missing query parameter `includeDriver` (spec optional, type=boolean).
  - Endpoints: `GET /safety-events`
  - Recommended fix: Add an optional parameter `bool? includeDriver = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `includeDriver` (spec optional, type=boolean).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `bool? includeDriver = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListEventsAsync (GET /safety-events) is missing query parameter `includeVgOnlyEvents` (spec optional, type=boolean).
  - Endpoints: `GET /safety-events`
  - Recommended fix: Add an optional parameter `bool? includeVgOnlyEvents = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `includeVgOnlyEvents` (spec optional, type=boolean).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `bool? includeVgOnlyEvents = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `queryByTimeField` (spec optional, type=string).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `string? queryByTimeField = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetEventsStreamAsync (GET /safety-events/stream) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /safety-events/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `SafetyEvent` (response)

- **[response_drift_optional]** SafetyEvent (response) missing property `assignedCoach` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("assignedCoach")] public string? AssignedCoach { get; init; }` to response record `SafetyEvent`.
- **[response_drift_optional]** SafetyEvent (response) missing property `detectedStreams` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("detectedStreams")] public IReadOnlyList<object>? DetectedStreams { get; init; }` to response record `SafetyEvent`.
- **[response_drift_optional]** SafetyEvent (response) missing property `dismissalReason` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("dismissalReason")] public object? DismissalReason { get; init; }` to response record `SafetyEvent`.
- **[response_drift_optional]** SafetyEvent (response) missing property `media` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("media")] public IReadOnlyList<object>? Media { get; init; }` to response record `SafetyEvent`.
- **[response_drift_optional]** SafetyEvent (response) missing property `speedingMetadata` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("speedingMetadata")] public object? SpeedingMetadata { get; init; }` to response record `SafetyEvent`.
- **[response_drift_optional]** SafetyEvent (response) missing property `tripEndTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("tripEndTime")] public string? TripEndTime { get; init; }` to response record `SafetyEvent`.
- **[response_drift_optional]** SafetyEvent (response) missing property `tripStartTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("tripStartTime")] public string? TripStartTime { get; init; }` to response record `SafetyEvent`.
- **[response_drift_optional]** SafetyEvent (response) missing property `updatedByUserId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Add `[JsonPropertyName("updatedByUserId")] public string? UpdatedByUserId { get; init; }` to response record `SafetyEvent`.
- **[response_required_drift]** SafetyEvent.behaviorLabels (response): spec marks REQUIRED but SDK exposes as nullable (`IReadOnlyList<string>?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Tighten `SafetyEvent.BehaviorLabels` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** SafetyEvent.driver (response): spec marks REQUIRED but SDK exposes as nullable (`SafetyEventDriver?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Tighten `SafetyEvent.Driver` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** SafetyEvent.id (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Tighten `SafetyEvent.Id` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (2)

### `SafetyEvent` (response)

- **[extra_property]** SafetyEvent.time (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Remove `SafetyEvent.Time` (not in spec).
- **[extra_property]** SafetyEvent.vehicle (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /safety-events`, `GET /safety-events/stream`
  - Recommended fix: Remove `SafetyEvent.Vehicle` (not in spec).

