# TrainingAssignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/48-trainingassignments.md`](../48-trainingassignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `TrainingAssignment` | response | 0 | 7 | 6 | 6 |
| `(no SDK type)` | query | 0 | 1 | 6 | 0 |

**Counts**: CRITICAL=0, HIGH=8, MEDIUM=12, LOW=6  
**Total deduped findings**: 26

## HIGH (8)

### `(no SDK type)` (query)

- **[missing_required_query]** ListAssignmentsAsync (GET /training-assignments/stream) is missing query parameter `startTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add a required parameter (e.g. `string startTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startTime", ...)`.

### `TrainingAssignment` (response)

- **[response_drift_required]** TrainingAssignment (response) missing REQUIRED property `course` (spec type=object).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("course")] public object Course { get; init; }` to response record `TrainingAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingAssignment (response) missing REQUIRED property `createdAtTime` (spec type=string/date-time).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public DateTimeOffset CreatedAtTime { get; init; }` to response record `TrainingAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingAssignment (response) missing REQUIRED property `createdById` (spec type=string).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("createdById")] public string CreatedById { get; init; }` to response record `TrainingAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingAssignment (response) missing REQUIRED property `durationMinutes` (spec type=integer/int64).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("durationMinutes")] public long DurationMinutes { get; init; }` to response record `TrainingAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingAssignment (response) missing REQUIRED property `learner` (spec type=object).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("learner")] public object Learner { get; init; }` to response record `TrainingAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingAssignment (response) missing REQUIRED property `updatedAtTime` (spec type=string/date-time).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public DateTimeOffset UpdatedAtTime { get; init; }` to response record `TrainingAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingAssignment (response) missing REQUIRED property `updatedById` (spec type=string).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("updatedById")] public string UpdatedById { get; init; }` to response record `TrainingAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (12)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAssignmentsAsync (GET /training-assignments/stream) is missing query parameter `categoryIds` (spec optional, type=array).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? categoryIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssignmentsAsync (GET /training-assignments/stream) is missing query parameter `courseIds` (spec optional, type=array).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? courseIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssignmentsAsync (GET /training-assignments/stream) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssignmentsAsync (GET /training-assignments/stream) is missing query parameter `isOverdue` (spec optional, type=boolean).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add an optional parameter `bool? isOverdue = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssignmentsAsync (GET /training-assignments/stream) is missing query parameter `learnerIds` (spec optional, type=array).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? learnerIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAssignmentsAsync (GET /training-assignments/stream) is missing query parameter `status` (spec optional, type=array).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? status = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `TrainingAssignment` (response)

- **[response_drift_optional]** TrainingAssignment (response) missing property `deletedAtTime` (spec type=string/date-time).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("deletedAtTime")] public DateTimeOffset? DeletedAtTime { get; init; }` to response record `TrainingAssignment`.
- **[response_drift_optional]** TrainingAssignment (response) missing property `isCompletedLate` (spec type=boolean).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("isCompletedLate")] public bool? IsCompletedLate { get; init; }` to response record `TrainingAssignment`.
- **[response_drift_optional]** TrainingAssignment (response) missing property `isOverdue` (spec type=boolean).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("isOverdue")] public bool? IsOverdue { get; init; }` to response record `TrainingAssignment`.
- **[response_drift_optional]** TrainingAssignment (response) missing property `scorePercent` (spec type=number/double).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("scorePercent")] public double? ScorePercent { get; init; }` to response record `TrainingAssignment`.
- **[response_drift_optional]** TrainingAssignment (response) missing property `startedAtTime` (spec type=string/date-time).
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Add `[JsonPropertyName("startedAtTime")] public DateTimeOffset? StartedAtTime { get; init; }` to response record `TrainingAssignment`.
- **[response_required_drift]** TrainingAssignment.status (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Tighten `TrainingAssignment.Status` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (6)

### `TrainingAssignment` (response)

- **[extra_property]** TrainingAssignment.assignedAtTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Remove `TrainingAssignment.AssignedAtTime` (not in spec).
- **[extra_property]** TrainingAssignment.courseId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Remove `TrainingAssignment.CourseId` (not in spec).
- **[extra_property]** TrainingAssignment.courseName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Remove `TrainingAssignment.CourseName` (not in spec).
- **[extra_property]** TrainingAssignment.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Remove `TrainingAssignment.DriverId` (not in spec).
- **[extra_property]** TrainingAssignment.driverName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Remove `TrainingAssignment.DriverName` (not in spec).
- **[extra_property]** TrainingAssignment.score (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-assignments/stream`
  - Recommended fix: Remove `TrainingAssignment.Score` (not in spec).

