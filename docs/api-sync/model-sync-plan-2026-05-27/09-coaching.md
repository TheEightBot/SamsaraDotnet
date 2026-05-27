# Coaching — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/09-coaching.md`](../09-coaching.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `CoachingSession` | response | 0 | 6 | 3 | 5 |
| `DriverCoachAssignment` | response | 0 | 3 | 1 | 3 |
| `(no SDK type)` | query | 0 | 1 | 9 | 0 |

**Counts**: CRITICAL=0, HIGH=10, MEDIUM=13, LOW=8  
**Total deduped findings**: 31

## HIGH (10)

### `(no SDK type)` (query)

- **[missing_required_query]** SetAssignmentAsync (PUT /coaching/driver-coach-assignments) is missing query parameter `driverId` (spec REQUIRED, type=string).
  - Endpoints: `PUT /coaching/driver-coach-assignments`
  - Recommended fix: Add a required parameter (e.g. `string driverId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("driverId", ...)`.

### `CoachingSession` (response)

- **[response_drift_required]** CoachingSession (response) missing REQUIRED property `behaviors` (spec type=array).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("behaviors")] public IReadOnlyList<object> Behaviors { get; init; }` to response record `CoachingSession` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CoachingSession (response) missing REQUIRED property `coachingType` (spec type=string).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("coachingType")] public string CoachingType { get; init; }` to response record `CoachingSession` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CoachingSession (response) missing REQUIRED property `driver` (spec type=object).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object Driver { get; init; }` to response record `CoachingSession` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CoachingSession (response) missing REQUIRED property `dueAtTime` (spec type=string/date-time).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("dueAtTime")] public DateTimeOffset DueAtTime { get; init; }` to response record `CoachingSession` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CoachingSession (response) missing REQUIRED property `sessionStatus` (spec type=string).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("sessionStatus")] public string SessionStatus { get; init; }` to response record `CoachingSession` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** CoachingSession (response) missing REQUIRED property `updatedAtTime` (spec type=string/date-time).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public DateTimeOffset UpdatedAtTime { get; init; }` to response record `CoachingSession` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `DriverCoachAssignment` (response)

- **[response_drift_required]** DriverCoachAssignment (response) missing REQUIRED property `createdAtTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /coaching/driver-coach-assignments`, `PUT /coaching/driver-coach-assignments`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public DateTimeOffset CreatedAtTime { get; init; }` to response record `DriverCoachAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverCoachAssignment (response) missing REQUIRED property `driver` (spec type=object).
  - Endpoints: `GET /coaching/driver-coach-assignments`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object Driver { get; init; }` to response record `DriverCoachAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverCoachAssignment (response) missing REQUIRED property `updatedAtTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /coaching/driver-coach-assignments`, `PUT /coaching/driver-coach-assignments`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public DateTimeOffset UpdatedAtTime { get; init; }` to response record `DriverCoachAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (13)

### `(no SDK type)` (query)

- **[missing_optional_query]** SetAssignmentAsync (PUT /coaching/driver-coach-assignments) is missing query parameter `coachId` (spec optional, type=string).
  - Endpoints: `PUT /coaching/driver-coach-assignments`
  - Recommended fix: Add an optional parameter `string? coachId = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssignmentsAsync (GET /coaching/driver-coach-assignments) is missing query parameter `coachIds` (spec optional, type=array).
  - Endpoints: `GET /coaching/driver-coach-assignments`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? coachIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSessionsStreamAsync (GET /coaching/sessions/stream) is missing query parameter `coachIds` (spec optional, type=array).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? coachIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssignmentsAsync (GET /coaching/driver-coach-assignments) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /coaching/driver-coach-assignments`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSessionsStreamAsync (GET /coaching/sessions/stream) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSessionsStreamAsync (GET /coaching/sessions/stream) is missing query parameter `includeCoachableEvents` (spec optional, type=boolean).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add an optional parameter `bool? includeCoachableEvents = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssignmentsAsync (GET /coaching/driver-coach-assignments) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /coaching/driver-coach-assignments`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSessionsStreamAsync (GET /coaching/sessions/stream) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSessionsStreamAsync (GET /coaching/sessions/stream) is missing query parameter `sessionStatuses` (spec optional, type=array).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? sessionStatuses = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CoachingSession` (response)

- **[response_drift_optional]** CoachingSession (response) missing property `assignedCoachId` (spec type=string).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("assignedCoachId")] public string? AssignedCoachId { get; init; }` to response record `CoachingSession`.
- **[response_drift_optional]** CoachingSession (response) missing property `completedCoachId` (spec type=string).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("completedCoachId")] public string? CompletedCoachId { get; init; }` to response record `CoachingSession`.
- **[response_drift_optional]** CoachingSession (response) missing property `sessionNote` (spec type=string).
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Add `[JsonPropertyName("sessionNote")] public string? SessionNote { get; init; }` to response record `CoachingSession`.

### `DriverCoachAssignment` (response)

- **[response_required_drift]** DriverCoachAssignment.coachId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /coaching/driver-coach-assignments`
  - Recommended fix: Tighten `DriverCoachAssignment.CoachId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (8)

### `CoachingSession` (response)

- **[extra_property]** CoachingSession.coachId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Remove `CoachingSession.CoachId` (not in spec).
- **[extra_property]** CoachingSession.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Remove `CoachingSession.DriverId` (not in spec).
- **[extra_property]** CoachingSession.scheduledAtTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Remove `CoachingSession.ScheduledAtTime` (not in spec).
- **[extra_property]** CoachingSession.sessionType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Remove `CoachingSession.SessionType` (not in spec).
- **[extra_property]** CoachingSession.status (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /coaching/sessions/stream`
  - Recommended fix: Remove `CoachingSession.Status` (not in spec).

### `DriverCoachAssignment` (response)

- **[extra_property]** DriverCoachAssignment.coachName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /coaching/driver-coach-assignments`, `PUT /coaching/driver-coach-assignments`
  - Recommended fix: Remove `DriverCoachAssignment.CoachName` (not in spec).
- **[extra_property]** DriverCoachAssignment.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /coaching/driver-coach-assignments`
  - Recommended fix: Remove `DriverCoachAssignment.DriverId` (not in spec).
- **[extra_property]** DriverCoachAssignment.driverName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /coaching/driver-coach-assignments`, `PUT /coaching/driver-coach-assignments`
  - Recommended fix: Remove `DriverCoachAssignment.DriverName` (not in spec).

