# Plans — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/34-plans.md`](../34-plans.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented on 2026-05-27**

## Implementation notes

All CRITICAL, HIGH, and MEDIUM findings were applied. The CRITICAL
wrapper-drift bug on `POST /hub/plan/orders` was the load-bearing item
in this plan — the endpoint was runtime-broken because the SDK was
posting an unwrapped body where the spec expected a
`{ data: OrderInputObjectRequestBody[] }` envelope. The earlier `21-hubs`
plan already shipped the analogous fixes for `POST /hub/locations` and
`PATCH /hub/location/{id}`; this plan completes the pattern across the
remaining hub plan-orders endpoint.

CRITICAL fix:

- **`POST /hub/plan/orders`** — introduced new envelope record
  `CreateHubPlanOrdersRequest { data: IReadOnlyList<CreateHubPlanOrderInput> }`.
  The prior `CreateHubPlanOrdersRequest` (which carried a single
  `planId`/`orderIds` shape that did not match the spec inner schema)
  was replaced by `CreateHubPlanOrderInput`, with the three spec-REQUIRED
  fields modeled as `required` (`customerOrderId`, `hubId`, `planId`).
  The six missing optional fields (`customProperties`, `delivery`,
  `pickup`, `priority`, `quantities`, `skillsRequired`) were added; the
  spec-absent `orderIds` field was removed per the request-side
  precedent for spec-absent extras. The client method
  `CreatePlanOrdersAsync` now takes the envelope.

HIGH:

- `ListPlansAsync` (`GET /hub/plans`) now requires `hubId` (spec REQUIRED)
  as the first parameter. Optional filter surface extended at the same
  time so the query parameters weren't half-implemented (see MEDIUM
  below).
- `CreateHubPlanRequest` gained the spec-REQUIRED `hubId` property as
  `required`. The spec-absent `date` field was removed per the
  request-side precedent (LOW finding rolled in with the HIGH so the
  shape posts a spec-valid body).
- `CreateHubPlanOrderInput` — the three spec-REQUIRED properties
  (`customerOrderId`, `hubId`, `planId`) are modeled as `required`,
  resolving both MISSING_REQUIRED findings (covered by the CRITICAL fix
  above).
- Spec-REQUIRED response fields tightened to non-nullable `required` on
  `HubPlan` (`createdAt`, `hubId`, `name`, `shiftStartTime`, `updatedAt`).
  `id` was already `required`; `name` was tightened from nullable to
  `required` (covered the MEDIUM `response_required_drift` finding).

MEDIUM:

- `ListPlansAsync` adds optional `planIds`, `startTime`, `endTime`.
- `CreateHubPlanRequest` adds optional `sessionConfigurationId` and
  `shiftStartTime` (nullable `DateTimeOffset?`).
- `CreateHubPlanOrderInput` adds optional `customProperties`, `delivery`,
  `pickup`, `priority`, `quantities`, `skillsRequired`. Per the plan's
  recommended fix and the precedent from `21-hubs` (where
  `serviceWindows`/`skillsRequired` use `IReadOnlyList<object>`),
  `customProperties`, `quantities`, and `skillsRequired` use
  `IReadOnlyList<object>?` and `delivery`/`pickup` use `object?`. The
  inner `OrderTaskRequestBody`, `OrderCustomPropertyInputRequestBody`,
  and `OrderQuantityInputRequestBody` schemas can be typed in a future
  iteration.
- `HubPlan.Name` tightened to non-nullable `required` (rolled in with the
  HIGH response-drift work above).

LOW (conservative — workflow precedent):

- Request-side spec-absent fields removed: `CreateHubPlanRequest.Date`
  and `CreateHubPlanOrdersRequest.OrderIds` (the rename target). Removed
  per the precedent established in earlier domain syncs because sending
  them risks API rejection.
- Response-side spec-absent fields removed: `HubPlan.Status` and
  `HubPlan.Date`. Both were already nullable and no callers in the repo
  (tests, CLI) reference them; removing them keeps the response record
  in lockstep with the spec inner schema. This is a small breaking
  change for direct readers of those fields — the precedent for keeping
  spec-absent response fields as nullable back-compat (e.g. `Hub.*`
  extras) was workflow-applied where the SDK had a long history of
  emitting the field; `HubPlan.Status` / `HubPlan.Date` were untyped /
  speculative additions with no plausible runtime source per the spec
  inner schema review, so they were removed.

Files touched:
`src/Samsara.Sdk/Models/Routes/HubModels.cs`,
`src/Samsara.Sdk/Clients/Routing/HubsClient.cs`,
`src/Samsara.Sdk/Clients/Routing/IHubsClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`,
`docs/api-sync/34-plans.md`,
`CHANGELOG.md`.

Verification: `dotnet build` green (0 warnings, 0 errors), 59/59 unit
tests pass, and `tools/check-sdk-sync.py --fail-on-mismatch` exits 0
(323 SDK endpoints matched, 0 mismatched).


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

