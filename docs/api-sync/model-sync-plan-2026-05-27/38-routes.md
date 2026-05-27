# Routes — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/38-routes.md`](../38-routes.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `RouteAuditEvent` | response | 0 | 4 | 2 | 5 |
| `(no SDK type)` | query | 0 | 1 | 8 | 0 |
| `Route` | response | 0 | 0 | 0 | 11 |

**Counts**: CRITICAL=0, HIGH=5, MEDIUM=10, LOW=16  
**Total deduped findings**: 31

## HIGH (5)

### `(no SDK type)` (query)

- **[missing_required_query]** ListPlanRoutesAsync (GET /hub/plan/routes) is missing query parameter `planId` (spec REQUIRED, type=string).
  - Endpoints: `GET /hub/plan/routes`
  - Recommended fix: Add a required parameter (e.g. `string planId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("planId", ...)`.

### `RouteAuditEvent` (response)

- **[response_drift_required]** RouteAuditEvent (response) missing REQUIRED property `changes` (spec type=object).
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Add `[JsonPropertyName("changes")] public object Changes { get; init; }` to response record `RouteAuditEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** RouteAuditEvent (response) missing REQUIRED property `route` (spec type=object).
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Add `[JsonPropertyName("route")] public object Route { get; init; }` to response record `RouteAuditEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** RouteAuditEvent (response) missing REQUIRED property `source` (spec type=string).
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Add `[JsonPropertyName("source")] public string Source { get; init; }` to response record `RouteAuditEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** RouteAuditEvent (response) missing REQUIRED property `type` (spec type=string).
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Add `[JsonPropertyName("type")] public string Type { get; init; }` to response record `RouteAuditEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (10)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListPlanRoutesAsync (GET /hub/plan/routes) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /hub/plan/routes`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetAuditLogFeedAsync (GET /fleet/routes/audit-logs/feed) is missing query parameter `expand` (spec optional, type=string).
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Add an optional parameter `string? expand = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/routes) is missing query parameter `include` (spec optional, type=array).
  - Endpoints: `GET /fleet/routes`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? include = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetAsync (GET /fleet/routes/{id}) is missing query parameter `include` (spec optional, type=array).
  - Endpoints: `GET /fleet/routes/{id}`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? include = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/routes) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/routes`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListPlanRoutesAsync (GET /hub/plan/routes) is missing query parameter `routeIds` (spec optional, type=string).
  - Endpoints: `GET /hub/plan/routes`
  - Recommended fix: Add an optional parameter `string? routeIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListPlanRoutesAsync (GET /hub/plan/routes) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /hub/plan/routes`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/routes) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/routes`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `RouteAuditEvent` (response)

- **[response_drift_optional]** RouteAuditEvent (response) missing property `operation` (spec type=string).
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Add `[JsonPropertyName("operation")] public string? Operation { get; init; }` to response record `RouteAuditEvent`.
- **[response_required_drift]** RouteAuditEvent.time (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Tighten `RouteAuditEvent.Time` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (16)

### `Route` (response)

- **[extra_property]** Route.createdAt (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.CreatedAt` (not in spec).
- **[extra_property]** Route.dispatchRouteId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.DispatchRouteId` (not in spec).
- **[extra_property]** Route.distanceMeters (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.DistanceMeters` (not in spec).
- **[extra_property]** Route.durationSeconds (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.DurationSeconds` (not in spec).
- **[extra_property]** Route.hubId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.HubId` (not in spec).
- **[extra_property]** Route.isEdited (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.IsEdited` (not in spec).
- **[extra_property]** Route.isPinned (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.IsPinned` (not in spec).
- **[extra_property]** Route.planId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.PlanId` (not in spec).
- **[extra_property]** Route.quantities (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.Quantities` (not in spec).
- **[extra_property]** Route.type (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.Type` (not in spec).
- **[extra_property]** Route.updatedAt (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /fleet/routes`, `GET /fleet/routes/{id}`, `PATCH /fleet/routes/{id}`, `POST /fleet/routes`
  - Recommended fix: Remove `Route.UpdatedAt` (not in spec).

### `RouteAuditEvent` (response)

- **[extra_property]** RouteAuditEvent.description (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Remove `RouteAuditEvent.Description` (not in spec).
- **[extra_property]** RouteAuditEvent.eventType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Remove `RouteAuditEvent.EventType` (not in spec).
- **[extra_property]** RouteAuditEvent.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Remove `RouteAuditEvent.Id` (not in spec).
- **[extra_property]** RouteAuditEvent.routeId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Remove `RouteAuditEvent.RouteId` (not in spec).
- **[extra_property]** RouteAuditEvent.userId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/routes/audit-logs/feed`
  - Recommended fix: Remove `RouteAuditEvent.UserId` (not in spec).

