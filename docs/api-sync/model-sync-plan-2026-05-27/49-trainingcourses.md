# TrainingCourses — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/49-trainingcourses.md`](../49-trainingcourses.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `a31f682` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. The 4 LOW response-side extras were
intentionally retained as nullable back-compat properties per the workflow
precedent (cf. `40-safety-scores`, `47-trailers`, `48-trainingassignments`) —
grouped under a `// Not in current spec; retained for back-compat.` comment
rather than removed. Notably `name` is kept because the spec's course-title field
is `title` (added below), so existing callers reading `Name` keep working.

Files touched: `src/Samsara.Sdk/Models/Training/TrainingModels.cs`,
`src/Samsara.Sdk/Clients/Training/ITrainingClient.cs`,
`src/Samsara.Sdk/Clients/Training/TrainingClient.cs`,
`tools/Samsara.Cli/TuiApp.cs`.

**HIGH (5)**

- **`TrainingCourse` (response) — 5 required props**: `title` (`required
  string`), `status` (`required string`), `revisionId` (`required string`),
  `category` (`required object`, weakly-typed per plan), and
  `estimatedTimeToCompleteMinutes` (`required long`, int64). Verified safe to
  mark `required` — no `new TrainingCourse(...)` construction sites exist.

**MEDIUM (4)**

- **Query params (3)**: `categoryIds`, `courseIds`, and `status` added to
  `ListCoursesAsync` (`GET /training-courses`) as optional
  `IReadOnlyList<string>?` params, appended conditionally via
  `QueryBuilder.WithParams` (comma-joined). `status` uses `IReadOnlyList<string>?`
  per the query-array convention (plan suggested `IReadOnlyList<object>?`).
- **`TrainingCourse` (response) — 1 optional prop**: `labels`
  (`IReadOnlyList<object>?`).

**LOW (4)** — `name`, `isActive`, `createdAtTime`, `updatedAtTime` retained as
nullable back-compat props (not removed).

The CLI `List Courses` call site was updated to use a named `cancellationToken:`
argument now that `ListCoursesAsync` has optional params before the token. No
JsonContext changes (new props are weakly-typed `object`/scalar/array — no new
top-level types). No test changes (no construction sites).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `TrainingCourse` | response | 0 | 5 | 1 | 4 |
| `(no SDK type)` | query | 0 | 0 | 3 | 0 |

**Counts**: CRITICAL=0, HIGH=5, MEDIUM=4, LOW=4  
**Total deduped findings**: 13

## HIGH (5)

### `TrainingCourse` (response)

- **[response_drift_required]** TrainingCourse (response) missing REQUIRED property `category` (spec type=object).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add `[JsonPropertyName("category")] public object Category { get; init; }` to response record `TrainingCourse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingCourse (response) missing REQUIRED property `estimatedTimeToCompleteMinutes` (spec type=integer/int64).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add `[JsonPropertyName("estimatedTimeToCompleteMinutes")] public long EstimatedTimeToCompleteMinutes { get; init; }` to response record `TrainingCourse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingCourse (response) missing REQUIRED property `revisionId` (spec type=string).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add `[JsonPropertyName("revisionId")] public string RevisionId { get; init; }` to response record `TrainingCourse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingCourse (response) missing REQUIRED property `status` (spec type=string).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add `[JsonPropertyName("status")] public string Status { get; init; }` to response record `TrainingCourse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** TrainingCourse (response) missing REQUIRED property `title` (spec type=string).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add `[JsonPropertyName("title")] public string Title { get; init; }` to response record `TrainingCourse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (4)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListCoursesAsync (GET /training-courses) is missing query parameter `categoryIds` (spec optional, type=array).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? categoryIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCoursesAsync (GET /training-courses) is missing query parameter `courseIds` (spec optional, type=array).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? courseIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCoursesAsync (GET /training-courses) is missing query parameter `status` (spec optional, type=array).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? status = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `TrainingCourse` (response)

- **[response_drift_optional]** TrainingCourse (response) missing property `labels` (spec type=array).
  - Endpoints: `GET /training-courses`
  - Recommended fix: Add `[JsonPropertyName("labels")] public IReadOnlyList<object>? Labels { get; init; }` to response record `TrainingCourse`.

## LOW (4)

### `TrainingCourse` (response)

- **[extra_property]** TrainingCourse.createdAtTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-courses`
  - Recommended fix: Remove `TrainingCourse.CreatedAtTime` (not in spec).
- **[extra_property]** TrainingCourse.isActive (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-courses`
  - Recommended fix: Remove `TrainingCourse.IsActive` (not in spec).
- **[extra_property]** TrainingCourse.name (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-courses`
  - Recommended fix: Remove `TrainingCourse.Name` (not in spec).
- **[extra_property]** TrainingCourse.updatedAtTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /training-courses`
  - Recommended fix: Remove `TrainingCourse.UpdatedAtTime` (not in spec).

