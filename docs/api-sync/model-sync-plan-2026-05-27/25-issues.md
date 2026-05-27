# Issues — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/25-issues.md`](../25-issues.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `Issue` | response | 0 | 3 | 8 | 6 |
| `(no SDK type)` | query | 0 | 0 | 5 | 0 |
| `CreateIssueRequest` | request | 0 | 0 | 2 | 0 |
| `UpdateIssueRequest` | request | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=3, MEDIUM=16, LOW=6  
**Total deduped findings**: 25

## HIGH (3)

### `Issue` (response)

- **[response_drift_required]** Issue (response) missing REQUIRED property `issueSource` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("issueSource")] public object IssueSource { get; init; }` to response record `Issue` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Issue (response) missing REQUIRED property `submittedAtTime` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("submittedAtTime")] public DateTimeOffset SubmittedAtTime { get; init; }` to response record `Issue` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Issue (response) missing REQUIRED property `submittedBy` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("submittedBy")] public object SubmittedBy { get; init; }` to response record `Issue` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (16)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `assetExternalIds` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `assignedToRouteStopIds` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assignedToRouteStopIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `include` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? include = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `status` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? status = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateIssueRequest` (request)

- **[weak_typing]** CreateIssueRequest.asset: SDK uses weak `object` for spec type `object`.
  - Endpoints: `POST /issues`
  - Recommended fix: Replace weak `object?` with a typed model on `CreateIssueRequest.Asset` (spec type=`object`).
- **[weak_typing]** CreateIssueRequest.assignedTo: SDK uses weak `object` for spec type `object`.
  - Endpoints: `POST /issues`
  - Recommended fix: Replace weak `object?` with a typed model on `CreateIssueRequest.AssignedTo` (spec type=`object`).

### `Issue` (response)

- **[response_drift_optional]** Issue (response) missing property `asset` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("asset")] public object? Asset { get; init; }` to response record `Issue`.
- **[response_drift_optional]** Issue (response) missing property `assignedTo` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("assignedTo")] public object? AssignedTo { get; init; }` to response record `Issue`.
- **[response_drift_optional]** Issue (response) missing property `dueDate` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("dueDate")] public DateTimeOffset? DueDate { get; init; }` to response record `Issue`.
- **[response_drift_optional]** Issue (response) missing property `mediaList` (spec type=array). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("mediaList")] public IReadOnlyList<object>? MediaList { get; init; }` to response record `Issue`.
- **[response_required_drift]** Issue.createdAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Tighten `Issue.CreatedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Issue.status (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Tighten `Issue.Status` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Issue.title (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Tighten `Issue.Title` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Issue.updatedAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Tighten `Issue.UpdatedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateIssueRequest` (request)

- **[weak_typing]** UpdateIssueRequest.assignedTo: SDK uses weak `object` for spec type `object`.
  - Endpoints: `PATCH /issues`
  - Recommended fix: Replace weak `object?` with a typed model on `UpdateIssueRequest.AssignedTo` (spec type=`object`).

## LOW (6)

### `Issue` (response)

- **[extra_property]** Issue.assigneeId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.AssigneeId` (not in spec).
- **[extra_property]** Issue.assigneeName (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.AssigneeName` (not in spec).
- **[extra_property]** Issue.resolvedAtTime (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.ResolvedAtTime` (not in spec).
- **[extra_property]** Issue.type (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.Type` (not in spec).
- **[extra_property]** Issue.vehicleId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.VehicleId` (not in spec).
- **[extra_property]** Issue.vehicleName (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.VehicleName` (not in spec).

