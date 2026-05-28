# Work Orders — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/56-work-orders.md`](../56-work-orders.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `PENDING` on 2026-05-27**

## Implementation notes

All 26 findings were addressed. The HIGH items and the two `response_required_drift`
tightenings were applied as `required` non-nullable; the MEDIUM request/response/query
items were applied; the four LOW non-spec extras were intentionally RETAINED per the
workflow precedent (cf. `40-safety-scores`, `54-vehicles`, `55-webhooks`) rather than
removed — where an extra was previously `required` but is not in the spec, it was
DEMOTED to nullable so deserialization cannot break.

**`WorkOrder` response**

- **`maintenanceSite` (response_drift_optional, MED)** — added as weakly-typed
  `object?` (spec `type=object`, un-schematized — no fabricated model, effort
  convention). Present on `GET /maintenance/work-orders`,
  `GET /maintenance/work-orders/stream`, `PATCH /maintenance/work-orders`,
  `POST /maintenance/work-orders`.

**`CreateWorkOrderRequest` / `UpdateWorkOrderRequest` request**

- **`placeExternalId`, `placeId` (missing_optional, MED ×2 each)** — added as
  `string?` to both records (placed alphabetically after `odometerMeters`).

**`ServiceTask` response**

- **`name` (response_required_drift, MED)** — tightened from `string?` to
  `required string`. The spec marks it REQUIRED in the response.
- **`category`, `estimatedLaborTimeMinutes`, `estimatedPartsCost`, `subcategory`
  (response_drift_optional, MED ×4)** — added.
  `estimatedLaborTimeMinutes` is `int?` (spec int32); `estimatedPartsCost` is
  weakly-typed `object?` (spec `type=object`); the other two are `string?`.
- **`laborCostCents` (extra_property, LOW)** — RETAINED as `long?` back-compat
  property (not in spec inner schema).

**`InvoiceScan` response**

- **`workOrderId` (response_required_drift, MED)** — tightened from `string?` to
  `required string`. The spec marks it REQUIRED in the response, so consumers may
  now rely on a non-null value.
- **`id` (extra_property, LOW)** — DEMOTED from `required string` to `string?`
  and RETAINED. It is not in the spec inner schema; leaving it `required` would
  break deserialization of the spec-shaped response.
- **`status` (extra_property, LOW)** — RETAINED as `string?` back-compat property.

**`PostInvoiceScanRequest` request**

- **`file` (missing_required, HIGH)** — added as `required object` (spec
  `type=object`; the endpoint is `POST /maintenance/invoice-scans`).
- **`assetId` (missing_optional, MED)** — added as `string?`.
- **`imageBase64` (extra_property, LOW)** — DEMOTED from `required string` to
  `string?` and RETAINED as back-compat (not in spec inner schema; demoting keeps
  the now-required `file` the only mandatory field).

**Query**

- **`DeleteWorkOrdersAsync` — `id` (missing_required_query, HIGH)** — the spec's
  `DELETE /maintenance/work-orders` takes a SINGLE required `id` (string), not the
  SDK's prior `ids` array. The method signature was changed from
  `DeleteWorkOrdersAsync(string[] ids, …)` to
  `DeleteWorkOrdersAsync(string id, …)` and the value appended via
  `QueryBuilder.WithParams(BasePath, ("id", id))`. **Breaking** — but SAFE (no
  callers in src/tools/tests).
- **`ListServiceTasksAsync` (`GET /maintenance/service-tasks`)** — added optional
  `ids` (`IReadOnlyList<string>?`, comma-joined) and `includeArchived` (`bool?`).
- **`ListWorkOrdersAsync` (`GET /maintenance/work-orders`)** — added optional
  `ids` (`IReadOnlyList<string>?`, comma-joined) and `includeExternalIds`
  (`bool?`).
- **`GetWorkOrdersStreamAsync` (`GET /maintenance/work-orders/stream`)** — added
  optional `assetIds`, `assignedUserIds`, `workOrderStatuses` (each
  `IReadOnlyList<string>?`, comma-joined) and `includeExternalIds` (`bool?`),
  appended after the existing `startTime`/`endTime` via `QueryBuilder.WithParams`
  wrapping `QueryBuilder.WithTimeRange`.

**Breaking / collateral**

- `DeleteWorkOrdersAsync` signature changed from `string[] ids` to `string id`
  (spec is singular). Consumers may now rely on a non-null `InvoiceScan.WorkOrderId`
  and `ServiceTask.Name`; `PostInvoiceScanRequest` now requires `file` instead of
  `imageBase64`. `System.Text.Json`'s `required` check throws on deserialization
  if a required response field is absent.
- No JsonContext changes (`WorkOrder`/`ServiceTask`/`InvoiceScan`/
  `PostInvoiceScanRequest` and the request records already registered; new props
  are weakly-typed `object`/scalar/array, no new top-level types).
- No CLI or test changes (no `new WorkOrder/ServiceTask/InvoiceScan/
  PostInvoiceScanRequest(...)` construction sites, no deserialization fixtures, and
  the CLI does not call any of these methods — only a facade substitute exists).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 1 | 8 | 0 |
| `PostInvoiceScanRequest` | request | 0 | 1 | 1 | 1 |
| `ServiceTask` | response | 0 | 0 | 5 | 1 |
| `CreateWorkOrderRequest` | request | 0 | 0 | 2 | 0 |
| `UpdateWorkOrderRequest` | request | 0 | 0 | 2 | 0 |
| `InvoiceScan` | response | 0 | 0 | 1 | 2 |
| `WorkOrder` | response | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=2, MEDIUM=20, LOW=4  
**Total deduped findings**: 26

## HIGH (2)

### `(no SDK type)` (query)

- **[missing_required_query]** DeleteWorkOrdersAsync (DELETE /maintenance/work-orders) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `DELETE /maintenance/work-orders`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.

### `PostInvoiceScanRequest` (request)

- **[missing_required]** PostInvoiceScanRequest is missing REQUIRED property `file` (spec type=object).
  - Endpoints: `POST /maintenance/invoice-scans`
  - Recommended fix: Add `[JsonPropertyName("file")] public required object File { get; init; }` to `PostInvoiceScanRequest`.

## MEDIUM (20)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetWorkOrdersStreamAsync (GET /maintenance/work-orders/stream) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /maintenance/work-orders/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetWorkOrdersStreamAsync (GET /maintenance/work-orders/stream) is missing query parameter `assignedUserIds` (spec optional, type=array).
  - Endpoints: `GET /maintenance/work-orders/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assignedUserIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListServiceTasksAsync (GET /maintenance/service-tasks) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /maintenance/service-tasks`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListWorkOrdersAsync (GET /maintenance/work-orders) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /maintenance/work-orders`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListServiceTasksAsync (GET /maintenance/service-tasks) is missing query parameter `includeArchived` (spec optional, type=boolean).
  - Endpoints: `GET /maintenance/service-tasks`
  - Recommended fix: Add an optional parameter `bool? includeArchived = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListWorkOrdersAsync (GET /maintenance/work-orders) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /maintenance/work-orders`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetWorkOrdersStreamAsync (GET /maintenance/work-orders/stream) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /maintenance/work-orders/stream`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetWorkOrdersStreamAsync (GET /maintenance/work-orders/stream) is missing query parameter `workOrderStatuses` (spec optional, type=array).
  - Endpoints: `GET /maintenance/work-orders/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? workOrderStatuses = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateWorkOrderRequest` (request)

- **[missing_optional]** CreateWorkOrderRequest is missing property `placeExternalId` (spec type=string).
  - Endpoints: `POST /maintenance/work-orders`
  - Recommended fix: Add `[JsonPropertyName("placeExternalId")] public string? PlaceExternalId { get; init; }` to `CreateWorkOrderRequest`.
- **[missing_optional]** CreateWorkOrderRequest is missing property `placeId` (spec type=string).
  - Endpoints: `POST /maintenance/work-orders`
  - Recommended fix: Add `[JsonPropertyName("placeId")] public string? PlaceId { get; init; }` to `CreateWorkOrderRequest`.

### `InvoiceScan` (response)

- **[response_required_drift]** InvoiceScan.workOrderId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `POST /maintenance/invoice-scans`
  - Recommended fix: Tighten `InvoiceScan.WorkOrderId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `PostInvoiceScanRequest` (request)

- **[missing_optional]** PostInvoiceScanRequest is missing property `assetId` (spec type=string).
  - Endpoints: `POST /maintenance/invoice-scans`
  - Recommended fix: Add `[JsonPropertyName("assetId")] public string? AssetId { get; init; }` to `PostInvoiceScanRequest`.

### `ServiceTask` (response)

- **[response_drift_optional]** ServiceTask (response) missing property `category` (spec type=string).
  - Endpoints: `GET /maintenance/service-tasks`
  - Recommended fix: Add `[JsonPropertyName("category")] public string? Category { get; init; }` to response record `ServiceTask`.
- **[response_drift_optional]** ServiceTask (response) missing property `estimatedLaborTimeMinutes` (spec type=integer/int32).
  - Endpoints: `GET /maintenance/service-tasks`
  - Recommended fix: Add `[JsonPropertyName("estimatedLaborTimeMinutes")] public int? EstimatedLaborTimeMinutes { get; init; }` to response record `ServiceTask`.
- **[response_drift_optional]** ServiceTask (response) missing property `estimatedPartsCost` (spec type=object).
  - Endpoints: `GET /maintenance/service-tasks`
  - Recommended fix: Add `[JsonPropertyName("estimatedPartsCost")] public object? EstimatedPartsCost { get; init; }` to response record `ServiceTask`.
- **[response_drift_optional]** ServiceTask (response) missing property `subcategory` (spec type=string).
  - Endpoints: `GET /maintenance/service-tasks`
  - Recommended fix: Add `[JsonPropertyName("subcategory")] public string? Subcategory { get; init; }` to response record `ServiceTask`.
- **[response_required_drift]** ServiceTask.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /maintenance/service-tasks`
  - Recommended fix: Tighten `ServiceTask.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateWorkOrderRequest` (request)

- **[missing_optional]** UpdateWorkOrderRequest is missing property `placeExternalId` (spec type=string).
  - Endpoints: `PATCH /maintenance/work-orders`
  - Recommended fix: Add `[JsonPropertyName("placeExternalId")] public string? PlaceExternalId { get; init; }` to `UpdateWorkOrderRequest`.
- **[missing_optional]** UpdateWorkOrderRequest is missing property `placeId` (spec type=string).
  - Endpoints: `PATCH /maintenance/work-orders`
  - Recommended fix: Add `[JsonPropertyName("placeId")] public string? PlaceId { get; init; }` to `UpdateWorkOrderRequest`.

### `WorkOrder` (response)

- **[response_drift_optional]** WorkOrder (response) missing property `maintenanceSite` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /maintenance/work-orders`, `GET /maintenance/work-orders/stream`, `PATCH /maintenance/work-orders`, `POST /maintenance/work-orders`
  - Recommended fix: Add `[JsonPropertyName("maintenanceSite")] public object? MaintenanceSite { get; init; }` to response record `WorkOrder`.

## LOW (4)

### `InvoiceScan` (response)

- **[extra_property]** InvoiceScan.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /maintenance/invoice-scans`
  - Recommended fix: Remove `InvoiceScan.Id` (not in spec).
- **[extra_property]** InvoiceScan.status (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /maintenance/invoice-scans`
  - Recommended fix: Remove `InvoiceScan.Status` (not in spec).

### `PostInvoiceScanRequest` (request)

- **[extra_property]** PostInvoiceScanRequest.imageBase64: present in SDK but not in spec inner schema.
  - Endpoints: `POST /maintenance/invoice-scans`
  - Recommended fix: Remove `PostInvoiceScanRequest.ImageBase64` (not in spec).

### `ServiceTask` (response)

- **[extra_property]** ServiceTask.laborCostCents (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /maintenance/service-tasks`
  - Recommended fix: Remove `ServiceTask.LaborCostCents` (not in spec).

