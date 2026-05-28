# TrainingAssignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/48-trainingassignments.md`](../48-trainingassignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `f5cb439` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. The 6 LOW response-side extras were
intentionally retained as nullable back-compat properties per the workflow
precedent (cf. `40-safety-scores`, `45-tags`, `46-trailer-assignments`,
`47-trailers`) — grouped under a `// Not in current spec; retained for
back-compat.` comment rather than removed.

Files touched: `src/Samsara.Sdk/Models/Training/TrainingModels.cs`,
`src/Samsara.Sdk/Clients/Training/ITrainingClient.cs`,
`src/Samsara.Sdk/Clients/Training/TrainingClient.cs`,
`tools/Samsara.Cli/TuiApp.cs`.

**HIGH (8)**

- **`(no SDK type)` query — `ListAssignmentsAsync` required `startTime`**: added
  as a required positional `DateTimeOffset startTime` (no default, placed first),
  appended via `QueryBuilder.WithTimeRange`. **Breaking** signature change. The
  plan lists spec `type=string`, but `*Time` stream params are modeled as
  `DateTimeOffset` here, mirroring `ISafetyClient.GetEventsStreamAsync`.
- **`TrainingAssignment` (response) — 7 required props**: `course`
  (`required object`), `learner` (`required object`), `createdById`
  (`required string`), `createdAtTime` (`required DateTimeOffset`), `updatedById`
  (`required string`), `updatedAtTime` (`required DateTimeOffset`),
  `durationMinutes` (`required long`, int64). Timestamps use `DateTimeOffset` per
  repo convention (plan says `string`); `course`/`learner` stay weakly-typed
  `object` per plan. Verified safe to mark `required` — no `new
  TrainingAssignment(...)` construction sites exist.

**MEDIUM (12)**

- **Query params (6)**: `endTime` added as `DateTimeOffset?` (via
  `WithTimeRange`); `categoryIds`, `courseIds`, `learnerIds`, and `status` added
  as `IReadOnlyList<string>?` (comma-joined). `status` uses
  `IReadOnlyList<string>?`, not the plan's literal `IReadOnlyList<object>?`, to
  match the array-query convention. `isOverdue` added as `bool?` (lowercase
  stringified). All appended via `QueryBuilder.WithParams`.
- **`TrainingAssignment` (response) — 5 optional props + 1 tightening**: added
  `startedAtTime` (`DateTimeOffset?`), `deletedAtTime` (`DateTimeOffset?`),
  `isOverdue` (`bool?`), `isCompletedLate` (`bool?`), `scorePercent` (`double?`).
  `status` tightened from `string?` to `required string` (spec REQUIRED) —
  **breaking**.

**LOW (6)**

- Non-spec extras `driverId`, `driverName`, `courseId`, `courseName`,
  `assignedAtTime`, `score` retained as nullable back-compat props.

The CLI `List Assignments` call site in `TuiApp.cs` was updated for the new
signature (default 7-day window `DateTimeOffset.UtcNow.AddDays(-7)`, named
`cancellationToken:` argument) and the now-non-nullable `a.Status` deref. No
JsonContext/test changes (record already registered, new props weakly-typed /
scalar / `DateTimeOffset`, no construction sites).

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

