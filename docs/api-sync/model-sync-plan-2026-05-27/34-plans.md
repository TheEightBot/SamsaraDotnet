# Plans — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/34-plans.md`](../34-plans.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `CreateHubPlanOrdersRequest` | request | 1 | 2 | 6 | 1 |
| `HubPlan` | response | 0 | 4 | 1 | 2 |
| `(no SDK type)` | query | 0 | 1 | 3 | 0 |
| `CreateHubPlanRequest` | request | 0 | 1 | 2 | 1 |

**Counts**: CRITICAL=1, HIGH=8, MEDIUM=12, LOW=4  
**Total deduped findings**: 25

## CRITICAL (1)

### `CreateHubPlanOrdersRequest` (request)

- **[wrapper_drift]** SDK posts CreateHubPlanOrdersRequest as the body, but spec expects array wrapped in `{ data }`. Inner schema requires: ['customerOrderId', 'hubId', 'planId'].
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Wrap the post body in `{ data: T[] }`. Introduce an envelope record (e.g. `CreateHubPlanOrdersRequest { data: IReadOnlyList<CreateHubPlanOrdersInput> }`) and rename the current `CreateHubPlanOrdersRequest` to `CreateHubPlanOrdersInput`. Each item must include `customerOrderId`, `hubId`, `planId` as `required`.

## HIGH (8)

### `(no SDK type)` (query)

- **[missing_required_query]** ListPlansAsync (GET /hub/plans) is missing query parameter `hubId` (spec REQUIRED, type=string).
  - Endpoints: `GET /hub/plans`
  - Recommended fix: Add a required parameter (e.g. `string hubId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("hubId", ...)`.

### `CreateHubPlanOrdersRequest` (request)

- **[missing_required]** CreateHubPlanOrdersRequest is missing REQUIRED property `customerOrderId` (spec type=string).
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("customerOrderId")] public required string CustomerOrderId { get; init; }` to `CreateHubPlanOrdersRequest`.
- **[missing_required]** CreateHubPlanOrdersRequest is missing REQUIRED property `hubId` (spec type=string).
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("hubId")] public required string HubId { get; init; }` to `CreateHubPlanOrdersRequest`.

### `CreateHubPlanRequest` (request)

- **[missing_required]** CreateHubPlanRequest is missing REQUIRED property `hubId` (spec type=string).
  - Endpoints: `POST /hub/plan`
  - Recommended fix: Add `[JsonPropertyName("hubId")] public required string HubId { get; init; }` to `CreateHubPlanRequest`.

### `HubPlan` (response)

- **[response_drift_required]** HubPlan (response) missing REQUIRED property `createdAt` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /hub/plans`, `POST /hub/plan`
  - Recommended fix: Add `[JsonPropertyName("createdAt")] public DateTimeOffset CreatedAt { get; init; }` to response record `HubPlan` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlan (response) missing REQUIRED property `hubId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /hub/plans`, `POST /hub/plan`
  - Recommended fix: Add `[JsonPropertyName("hubId")] public string HubId { get; init; }` to response record `HubPlan` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlan (response) missing REQUIRED property `shiftStartTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /hub/plans`, `POST /hub/plan`
  - Recommended fix: Add `[JsonPropertyName("shiftStartTime")] public DateTimeOffset ShiftStartTime { get; init; }` to response record `HubPlan` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlan (response) missing REQUIRED property `updatedAt` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /hub/plans`, `POST /hub/plan`
  - Recommended fix: Add `[JsonPropertyName("updatedAt")] public DateTimeOffset UpdatedAt { get; init; }` to response record `HubPlan` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (12)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListPlansAsync (GET /hub/plans) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /hub/plans`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListPlansAsync (GET /hub/plans) is missing query parameter `planIds` (spec optional, type=string).
  - Endpoints: `GET /hub/plans`
  - Recommended fix: Add an optional parameter `string? planIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListPlansAsync (GET /hub/plans) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /hub/plans`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateHubPlanOrdersRequest` (request)

- **[missing_optional]** CreateHubPlanOrdersRequest is missing property `customProperties` (spec type=array).
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("customProperties")] public IReadOnlyList<object>? CustomProperties { get; init; }` to `CreateHubPlanOrdersRequest`.
- **[missing_optional]** CreateHubPlanOrdersRequest is missing property `delivery` (spec type=object).
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("delivery")] public object? Delivery { get; init; }` to `CreateHubPlanOrdersRequest`.
- **[missing_optional]** CreateHubPlanOrdersRequest is missing property `pickup` (spec type=object).
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("pickup")] public object? Pickup { get; init; }` to `CreateHubPlanOrdersRequest`.
- **[missing_optional]** CreateHubPlanOrdersRequest is missing property `priority` (spec type=integer/int64).
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("priority")] public long? Priority { get; init; }` to `CreateHubPlanOrdersRequest`.
- **[missing_optional]** CreateHubPlanOrdersRequest is missing property `quantities` (spec type=array).
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("quantities")] public IReadOnlyList<object>? Quantities { get; init; }` to `CreateHubPlanOrdersRequest`.
- **[missing_optional]** CreateHubPlanOrdersRequest is missing property `skillsRequired` (spec type=array).
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("skillsRequired")] public IReadOnlyList<object>? SkillsRequired { get; init; }` to `CreateHubPlanOrdersRequest`.

### `CreateHubPlanRequest` (request)

- **[missing_optional]** CreateHubPlanRequest is missing property `sessionConfigurationId` (spec type=string).
  - Endpoints: `POST /hub/plan`
  - Recommended fix: Add `[JsonPropertyName("sessionConfigurationId")] public string? SessionConfigurationId { get; init; }` to `CreateHubPlanRequest`.
- **[missing_optional]** CreateHubPlanRequest is missing property `shiftStartTime` (spec type=string/date-time).
  - Endpoints: `POST /hub/plan`
  - Recommended fix: Add `[JsonPropertyName("shiftStartTime")] public DateTimeOffset? ShiftStartTime { get; init; }` to `CreateHubPlanRequest`.

### `HubPlan` (response)

- **[response_required_drift]** HubPlan.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /hub/plans`, `POST /hub/plan`
  - Recommended fix: Tighten `HubPlan.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (4)

### `CreateHubPlanOrdersRequest` (request)

- **[extra_property]** CreateHubPlanOrdersRequest.orderIds: present in SDK but not in spec inner schema.
  - Endpoints: `POST /hub/plan/orders`
  - Recommended fix: Remove `CreateHubPlanOrdersRequest.OrderIds` (not in spec).

### `CreateHubPlanRequest` (request)

- **[extra_property]** CreateHubPlanRequest.date: present in SDK but not in spec inner schema.
  - Endpoints: `POST /hub/plan`
  - Recommended fix: Remove `CreateHubPlanRequest.Date` (not in spec).

### `HubPlan` (response)

- **[extra_property]** HubPlan.date (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /hub/plans`, `POST /hub/plan`
  - Recommended fix: Remove `HubPlan.Date` (not in spec).
- **[extra_property]** HubPlan.status (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /hub/plans`, `POST /hub/plan`
  - Recommended fix: Remove `HubPlan.Status` (not in spec).

