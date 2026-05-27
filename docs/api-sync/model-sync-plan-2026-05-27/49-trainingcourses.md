# TrainingCourses — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/49-trainingcourses.md`](../49-trainingcourses.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

