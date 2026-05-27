# Beta APIs — Model Sync Plan (2026-05-27)

> **✅ Implemented in commit `pending` on 2026-05-27**

> Companion to [`docs/api-sync/06-beta-apis.md`](../06-beta-apis.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

## Implementation notes

Resolved 2026-05-27. Counts implemented: CRITICAL=0, HIGH=33, MEDIUM=104, LOW=0.

**MEDIUM weak-typing findings explicitly deferred (2):**

- `HosEldEvent.externalIds` (response, `object` per spec) — deferred.
- `HosEldEvent.eldEvents` (response, REQUIRED `array of object` per spec) — deferred.

Beta clients are documented as weakly-typed by design; typing would require modeling
hundreds of nested schemas. Tracked for a future Beta-typing workstream.

**LOW findings (10):** explicitly out of scope for this plan — left untouched per the
"LOW — leave alone" policy. These are SDK-only `HosEldEvent` and `HubPlanOrder` fields
that are present in the SDK but not in the spec inner schema (likely historical or v1
fallthroughs); removing them would be a breaking change for downstream consumers.


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 25 | 91 | 0 |
| `HubPlanOrder` | response | 0 | 8 | 4 | 1 |
| `UpdateEquipmentRequest` | request | 0 | 0 | 6 | 0 |
| `HosEldEvent` | response | 0 | 0 | 4 | 9 |
| `Equipment` | response | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=33, MEDIUM=106, LOW=10  
**Total deduped findings**: 149

## HIGH (33)

### `(no SDK type)` (query)

- **[missing_required_query]** ListRouteSetupsAsync (GET /ridership/route-setups) is missing query parameter `accountId` (spec REQUIRED, type=string).
  - Endpoints: `GET /ridership/route-setups`
  - Recommended fix: Add a required parameter (e.g. `string accountId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("accountId", ...)`.
- **[missing_required_query]** UpdateShippingDocsAsync (PATCH /hos/daily-logs/log-meta-data) is missing query parameter `driverID` (spec REQUIRED, type=string).
  - Endpoints: `PATCH /hos/daily-logs/log-meta-data`
  - Recommended fix: Add a required parameter (e.g. `string driverID` , no default) to the SDK method and append it via `QueryBuilder.WithParams("driverID", ...)`.
- **[missing_required_query]** GetLogsAsync (GET /functions/{name}/logs) is missing query parameter `endTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /functions/{name}/logs`
  - Recommended fix: Add a required parameter (e.g. `string endTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endTime", ...)`.
- **[missing_required_query]** GetStreamAsync (GET /qualification-records/stream) is missing query parameter `entityType` (spec REQUIRED, type=string).
  - Endpoints: `GET /qualification-records/stream`
  - Recommended fix: Add a required parameter (e.g. `string entityType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("entityType", ...)`.
- **[missing_required_query]** ListTypesAsync (GET /qualification-types) is missing query parameter `entityType` (spec REQUIRED, type=string).
  - Endpoints: `GET /qualification-types`
  - Recommended fix: Add a required parameter (e.g. `string entityType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("entityType", ...)`.
- **[missing_required_query]** UpdateShippingDocsAsync (PATCH /hos/daily-logs/log-meta-data) is missing query parameter `hosDate` (spec REQUIRED, type=string).
  - Endpoints: `PATCH /hos/daily-logs/log-meta-data`
  - Recommended fix: Add a required parameter (e.g. `string hosDate` , no default) to the SDK method and append it via `QueryBuilder.WithParams("hosDate", ...)`.
- **[missing_required_query]** ListRouteTemplatesAsync (GET /hub/route-templates) is missing query parameter `hubId` (spec REQUIRED, type=string).
  - Endpoints: `GET /hub/route-templates`
  - Recommended fix: Add a required parameter (e.g. `string hubId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("hubId", ...)`.
- **[missing_required_query]** UpdateIndustrialJobAsync (PATCH /beta/industrial/jobs) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `PATCH /beta/industrial/jobs`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.
- **[missing_required_query]** UpdateAsync (PATCH /preferred-stations) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `PATCH /preferred-stations`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.
- **[missing_required_query]** GetRunDataAsync (GET /reports/runs/data) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `GET /reports/runs/data`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.
- **[missing_required_query]** UpdatePassengerAsync (PUT /ridership/passengers) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `PUT /ridership/passengers`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.
- **[missing_required_query]** DeleteRouteTemplatesAsync (DELETE /hub/route-templates) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `DELETE /hub/route-templates`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.
- **[missing_required_query]** ListAsync (GET /qualification-records) is missing query parameter `ids` (spec REQUIRED, type=array).
  - Endpoints: `GET /qualification-records`
  - Recommended fix: Add a required parameter (e.g. `IReadOnlyList<string> ids` , no default) to the SDK method and append it via `QueryBuilder.WithParams("ids", ...)`.
- **[missing_required_query]** GetInputsStreamAsync (GET /assets/inputs/stream) is missing query parameter `ids` (spec REQUIRED, type=array).
  - Endpoints: `GET /assets/inputs/stream`
  - Recommended fix: Add a required parameter (e.g. `IReadOnlyList<string> ids` , no default) to the SDK method and append it via `QueryBuilder.WithParams("ids", ...)`.
- **[missing_required_query]** GetStorageFileAsync (GET /functions-storage/files) is missing query parameter `name` (spec REQUIRED, type=string).
  - Endpoints: `GET /functions-storage/files`
  - Recommended fix: Add a required parameter (e.g. `string name` , no default) to the SDK method and append it via `QueryBuilder.WithParams("name", ...)`.
- **[missing_required_query]** UpdateStorageFileAsync (PUT /functions-storage/files) is missing query parameter `name` (spec REQUIRED, type=string).
  - Endpoints: `PUT /functions-storage/files`
  - Recommended fix: Add a required parameter (e.g. `string name` , no default) to the SDK method and append it via `QueryBuilder.WithParams("name", ...)`.
- **[missing_required_query]** DeleteStorageFileAsync (DELETE /functions-storage/files) is missing query parameter `name` (spec REQUIRED, type=string).
  - Endpoints: `DELETE /functions-storage/files`
  - Recommended fix: Add a required parameter (e.g. `string name` , no default) to the SDK method and append it via `QueryBuilder.WithParams("name", ...)`.
- **[missing_required_query]** DeleteAsync (DELETE /places) is missing query parameter `placeId` (spec REQUIRED, type=integer).
  - Endpoints: `DELETE /places`
  - Recommended fix: Add a required parameter (e.g. `int placeId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("placeId", ...)`.
- **[missing_required_query]** ListPlanOrdersAsync (GET /hub/plan/orders) is missing query parameter `planId` (spec REQUIRED, type=string).
  - Endpoints: `GET /hub/plan/orders`
  - Recommended fix: Add a required parameter (e.g. `string planId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("planId", ...)`.
- **[missing_required_query]** DeletePlanOrdersAsync (DELETE /hub/plan/orders) is missing query parameter `planId` (spec REQUIRED, type=string).
  - Endpoints: `DELETE /hub/plan/orders`
  - Recommended fix: Add a required parameter (e.g. `string planId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("planId", ...)`.
- **[missing_required_query]** UpdateRouteSetupAsync (PUT /ridership/route-setups) is missing query parameter `routeId` (spec REQUIRED, type=string).
  - Endpoints: `PUT /ridership/route-setups`
  - Recommended fix: Add a required parameter (e.g. `string routeId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("routeId", ...)`.
- **[missing_required_query]** GetLogsAsync (GET /functions/{name}/logs) is missing query parameter `startTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /functions/{name}/logs`
  - Recommended fix: Add a required parameter (e.g. `string startTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startTime", ...)`.
- **[missing_required_query]** ListPassengersAsync (GET /ridership/passengers) is missing query parameter `tagId` (spec REQUIRED, type=string).
  - Endpoints: `GET /ridership/passengers`
  - Recommended fix: Add a required parameter (e.g. `string tagId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("tagId", ...)`.
- **[missing_required_query]** GetInputsStreamAsync (GET /assets/inputs/stream) is missing query parameter `type` (spec REQUIRED, type=string).
  - Endpoints: `GET /assets/inputs/stream`
  - Recommended fix: Add a required parameter (e.g. `string type` , no default) to the SDK method and append it via `QueryBuilder.WithParams("type", ...)`.
- **[missing_required_query]** GetImmobilizerStreamAsync (GET /fleet/vehicles/immobilizer/stream) is missing query parameter `vehicleIds` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/vehicles/immobilizer/stream`
  - Recommended fix: Add a required parameter (e.g. `string vehicleIds` , no default) to the SDK method and append it via `QueryBuilder.WithParams("vehicleIds", ...)`.

### `HubPlanOrder` (response)

- **[response_drift_required]** HubPlanOrder (response) missing REQUIRED property `createdAtTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public DateTimeOffset CreatedAtTime { get; init; }` to response record `HubPlanOrder` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlanOrder (response) missing REQUIRED property `customProperties` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("customProperties")] public IReadOnlyList<object> CustomProperties { get; init; }` to response record `HubPlanOrder` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlanOrder (response) missing REQUIRED property `customerOrderId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("customerOrderId")] public string CustomerOrderId { get; init; }` to response record `HubPlanOrder` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlanOrder (response) missing REQUIRED property `hubId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("hubId")] public string HubId { get; init; }` to response record `HubPlanOrder` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlanOrder (response) missing REQUIRED property `priority` (spec type=integer/int64). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("priority")] public long Priority { get; init; }` to response record `HubPlanOrder` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlanOrder (response) missing REQUIRED property `quantities` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("quantities")] public IReadOnlyList<object> Quantities { get; init; }` to response record `HubPlanOrder` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlanOrder (response) missing REQUIRED property `skillsRequired` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("skillsRequired")] public IReadOnlyList<object> SkillsRequired { get; init; }` to response record `HubPlanOrder` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubPlanOrder (response) missing REQUIRED property `updatedAtTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public DateTimeOffset UpdatedAtTime { get; init; }` to response record `HubPlanOrder` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (106)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListIndustrialJobsAsync (GET /beta/industrial/jobs) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /beta/industrial/jobs`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDevicesAsync (GET /devices) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /devices`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyAsync (GET /beta/fleet/drivers/efficiency) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /beta/fleet/drivers/efficiency`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLogsAsync (GET /functions/{name}/logs) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /functions/{name}/logs`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListStorageFilesAsync (GET /functions-storage/ls) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /functions-storage/ls`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListTypesAsync (GET /qualification-types) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /qualification-types`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListConfigsAsync (GET /reports/configs) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /reports/configs`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDatasetsAsync (GET /reports/datasets) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /reports/datasets`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetRunDataAsync (GET /reports/runs/data) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /reports/runs/data`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDepreciationTransactionsAsync (GET /assets/depreciation) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /assets/depreciation`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListIndustrialJobsAsync (GET /beta/industrial/jobs) is missing query parameter `customerName` (spec optional, type=string).
  - Endpoints: `GET /beta/industrial/jobs`
  - Recommended fix: Add an optional parameter `string? customerName = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** DeletePlanOrdersAsync (DELETE /hub/plan/orders) is missing query parameter `deleteAll` (spec optional, type=boolean).
  - Endpoints: `DELETE /hub/plan/orders`
  - Recommended fix: Add an optional parameter `bool? deleteAll = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `detectionBehaviorLabels` (spec optional, type=array).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? detectionBehaviorLabels = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyAsync (GET /beta/fleet/drivers/efficiency) is missing query parameter `driverActivationStatus` (spec optional, type=string).
  - Endpoints: `GET /beta/fleet/drivers/efficiency`
  - Recommended fix: Add an optional parameter `string? driverActivationStatus = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosEldEventsAsync (GET /beta/fleet/hos/drivers/eld-events) is missing query parameter `driverActivationStatus` (spec optional, type=string).
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Add an optional parameter `string? driverActivationStatus = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyAsync (GET /beta/fleet/drivers/efficiency) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /beta/fleet/drivers/efficiency`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosEldEventsAsync (GET /beta/fleet/hos/drivers/eld-events) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLiveDataAsync (GET /fleet/tachograph-live-data/latest) is missing query parameter `driverIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/tachograph-live-data/latest`
  - Recommended fix: Add an optional parameter `string? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyAsync (GET /beta/fleet/drivers/efficiency) is missing query parameter `driverParentTagIds` (spec optional, type=array).
  - Endpoints: `GET /beta/fleet/drivers/efficiency`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverParentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyAsync (GET /beta/fleet/drivers/efficiency) is missing query parameter `driverTagIds` (spec optional, type=array).
  - Endpoints: `GET /beta/fleet/drivers/efficiency`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListIndustrialJobsAsync (GET /beta/industrial/jobs) is missing query parameter `endDate` (spec optional, type=string).
  - Endpoints: `GET /beta/industrial/jobs`
  - Recommended fix: Add an optional parameter `string? endDate = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyAsync (GET /beta/fleet/drivers/efficiency) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /beta/fleet/drivers/efficiency`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDepreciationTransactionsAsync (GET /assets/depreciation) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /assets/depreciation`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** UpdateAsync (PATCH /places) is missing query parameter `externalId` (spec optional, type=string).
  - Endpoints: `PATCH /places`
  - Recommended fix: Add an optional parameter `string? externalId = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /places) is missing query parameter `externalIds` (spec optional, type=string).
  - Endpoints: `GET /places`
  - Recommended fix: Add an optional parameter `string? externalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLogsAsync (GET /functions/{name}/logs) is missing query parameter `filterText` (spec optional, type=string).
  - Endpoints: `GET /functions/{name}/logs`
  - Recommended fix: Add an optional parameter `string? filterText = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListIndustrialJobsAsync (GET /beta/industrial/jobs) is missing query parameter `fleetDeviceIds` (spec optional, type=array).
  - Endpoints: `GET /beta/industrial/jobs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? fleetDeviceIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDevicesAsync (GET /devices) is missing query parameter `healthStatuses` (spec optional, type=array).
  - Endpoints: `GET /devices`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? healthStatuses = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListIndustrialJobsAsync (GET /beta/industrial/jobs) is missing query parameter `id` (spec optional, type=string).
  - Endpoints: `GET /beta/industrial/jobs`
  - Recommended fix: Add an optional parameter `string? id = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListRouteTemplatesAsync (GET /hub/route-templates) is missing query parameter `id` (spec optional, type=string).
  - Endpoints: `GET /hub/route-templates`
  - Recommended fix: Add an optional parameter `string? id = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListTypesAsync (GET /qualification-types) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /qualification-types`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListConfigsAsync (GET /reports/configs) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /reports/configs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDatasetsAsync (GET /reports/datasets) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /reports/datasets`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListRunsAsync (GET /reports/runs) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /reports/runs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListVendorsAsync (GET /fleet/maintenance/vendors) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /fleet/maintenance/vendors`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `inCabAlertPlayed` (spec optional, type=boolean).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `bool? inCabAlertPlayed = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `inboxEvent` (spec optional, type=boolean).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `bool? inboxEvent = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `inboxFilterReason` (spec optional, type=array).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? inboxFilterReason = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `includeAsset` (spec optional, type=boolean).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `bool? includeAsset = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetInputsStreamAsync (GET /assets/inputs/stream) is missing query parameter `includeAttributes` (spec optional, type=boolean).
  - Endpoints: `GET /assets/inputs/stream`
  - Recommended fix: Add an optional parameter `bool? includeAttributes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /qualification-records/stream) is missing query parameter `includeDeleted` (spec optional, type=boolean).
  - Endpoints: `GET /qualification-records/stream`
  - Recommended fix: Add an optional parameter `bool? includeDeleted = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListStorageFilesAsync (GET /functions-storage/ls) is missing query parameter `includeDownloadUrls` (spec optional, type=boolean).
  - Endpoints: `GET /functions-storage/ls`
  - Recommended fix: Add an optional parameter `bool? includeDownloadUrls = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `includeDriver` (spec optional, type=boolean).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `bool? includeDriver = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /places) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /places`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /preferred-stations) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /preferred-stations`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetAsync (GET /preferred-stations/{id}) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /preferred-stations/{id}`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /qualification-records) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /qualification-records`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /qualification-records/stream) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /qualification-records/stream`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListPassengersAsync (GET /ridership/passengers) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /ridership/passengers`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetPassengerAsync (GET /ridership/passengers/{id}) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /ridership/passengers/{id}`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetInputsStreamAsync (GET /assets/inputs/stream) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /assets/inputs/stream`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListVendorsAsync (GET /fleet/maintenance/vendors) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /fleet/maintenance/vendors`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDevicesAsync (GET /devices) is missing query parameter `includeHealth` (spec optional, type=boolean).
  - Endpoints: `GET /devices`
  - Recommended fix: Add an optional parameter `bool? includeHealth = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDevicesAsync (GET /devices) is missing query parameter `includeTags` (spec optional, type=boolean).
  - Endpoints: `GET /devices`
  - Recommended fix: Add an optional parameter `bool? includeTags = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /places) is missing query parameter `includeTags` (spec optional, type=boolean).
  - Endpoints: `GET /places`
  - Recommended fix: Add an optional parameter `bool? includeTags = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetInputsStreamAsync (GET /assets/inputs/stream) is missing query parameter `includeTags` (spec optional, type=boolean).
  - Endpoints: `GET /assets/inputs/stream`
  - Recommended fix: Add an optional parameter `bool? includeTags = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListStorageFilesAsync (GET /functions-storage/ls) is missing query parameter `includeUploadUrls` (spec optional, type=boolean).
  - Endpoints: `GET /functions-storage/ls`
  - Recommended fix: Add an optional parameter `bool? includeUploadUrls = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListIndustrialJobsAsync (GET /beta/industrial/jobs) is missing query parameter `industrialAssetIds` (spec optional, type=array).
  - Endpoints: `GET /beta/industrial/jobs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? industrialAssetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDevicesAsync (GET /devices) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /devices`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetLogsAsync (GET /functions/{name}/logs) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /functions/{name}/logs`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListStorageFilesAsync (GET /functions-storage/ls) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /functions-storage/ls`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListConfigsAsync (GET /reports/configs) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /reports/configs`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDatasetsAsync (GET /reports/datasets) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /reports/datasets`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetRunDataAsync (GET /reports/runs/data) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /reports/runs/data`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDevicesAsync (GET /devices) is missing query parameter `models` (spec optional, type=array).
  - Endpoints: `GET /devices`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? models = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /places) is missing query parameter `name` (spec optional, type=string).
  - Endpoints: `GET /places`
  - Recommended fix: Add an optional parameter `string? name = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListRouteTemplatesAsync (GET /hub/route-templates) is missing query parameter `name` (spec optional, type=string).
  - Endpoints: `GET /hub/route-templates`
  - Recommended fix: Add an optional parameter `string? name = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListPlanOrdersAsync (GET /hub/plan/orders) is missing query parameter `orderIds` (spec optional, type=string).
  - Endpoints: `GET /hub/plan/orders`
  - Recommended fix: Add an optional parameter `string? orderIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** DeletePlanOrdersAsync (DELETE /hub/plan/orders) is missing query parameter `orderIds` (spec optional, type=string).
  - Endpoints: `DELETE /hub/plan/orders`
  - Recommended fix: Add an optional parameter `string? orderIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /qualification-records/stream) is missing query parameter `ownerIds` (spec optional, type=array).
  - Endpoints: `GET /qualification-records/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ownerIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDevicesAsync (GET /devices) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /devices`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /places) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /places`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosEldEventsAsync (GET /beta/fleet/hos/drivers/eld-events) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** UpdateAsync (PATCH /places) is missing query parameter `placeId` (spec optional, type=integer).
  - Endpoints: `PATCH /places`
  - Recommended fix: Add an optional parameter `int? placeId = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /places) is missing query parameter `placeIds` (spec optional, type=string).
  - Endpoints: `GET /places`
  - Recommended fix: Add an optional parameter `string? placeIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /places) is missing query parameter `placeTypes` (spec optional, type=string).
  - Endpoints: `GET /places`
  - Recommended fix: Add an optional parameter `string? placeTypes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /qualification-records/stream) is missing query parameter `qualificationTypeIds` (spec optional, type=array).
  - Endpoints: `GET /qualification-records/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? qualificationTypeIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListRunsAsync (GET /reports/runs) is missing query parameter `reportConfigIds` (spec optional, type=array).
  - Endpoints: `GET /reports/runs`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? reportConfigIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListIndustrialJobsAsync (GET /beta/industrial/jobs) is missing query parameter `startDate` (spec optional, type=string).
  - Endpoints: `GET /beta/industrial/jobs`
  - Recommended fix: Add an optional parameter `string? startDate = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyAsync (GET /beta/fleet/drivers/efficiency) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /beta/fleet/drivers/efficiency`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLiveDataAsync (GET /fleet/tachograph-live-data/latest) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/tachograph-live-data/latest`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDepreciationTransactionsAsync (GET /assets/depreciation) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /assets/depreciation`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListIndustrialJobsAsync (GET /beta/industrial/jobs) is missing query parameter `status` (spec optional, type=string).
  - Endpoints: `GET /beta/industrial/jobs`
  - Recommended fix: Add an optional parameter `string? status = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDevicesAsync (GET /devices) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /devices`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDetectionsStreamAsync (GET /detections/stream) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /detections/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /places) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /places`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHosEldEventsAsync (GET /beta/fleet/hos/drivers/eld-events) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLiveDataAsync (GET /fleet/tachograph-live-data/latest) is missing query parameter `vehicleIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/tachograph-live-data/latest`
  - Recommended fix: Add an optional parameter `string? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListWorkflowsAsync (GET /fleet/drivers/workflows) is missing query parameter `workflowType` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers/workflows`
  - Recommended fix: Add an optional parameter `string? workflowType = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `Equipment` (response)

- **[response_drift_optional]** Equipment (response) missing property `attributes` (spec type=array).
  - Endpoints: `PATCH /beta/fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("attributes")] public IReadOnlyList<object>? Attributes { get; init; }` to response record `Equipment`.

### `HosEldEvent` (response)

- **[response_drift_optional]** HosEldEvent (response) missing property `externalIds` (spec type=object).
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Add `[JsonPropertyName("externalIds")] public object? ExternalIds { get; init; }` to response record `HosEldEvent`.
- **[response_drift_required]** HosEldEvent (response) missing REQUIRED property `driverActivationStatus` (spec type=string).
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Add `[JsonPropertyName("driverActivationStatus")] public string DriverActivationStatus { get; init; }` to response record `HosEldEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HosEldEvent (response) missing REQUIRED property `eldEvents` (spec type=array).
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Add `[JsonPropertyName("eldEvents")] public IReadOnlyList<object> EldEvents { get; init; }` to response record `HosEldEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HosEldEvent (response) missing REQUIRED property `name` (spec type=string).
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Add `[JsonPropertyName("name")] public string Name { get; init; }` to response record `HosEldEvent` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `HubPlanOrder` (response)

- **[response_drift_optional]** HubPlanOrder (response) missing property `delivery` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("delivery")] public object? Delivery { get; init; }` to response record `HubPlanOrder`.
- **[response_drift_optional]** HubPlanOrder (response) missing property `pickup` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("pickup")] public object? Pickup { get; init; }` to response record `HubPlanOrder`.
- **[response_drift_optional]** HubPlanOrder (response) missing property `routeId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Add `[JsonPropertyName("routeId")] public string? RouteId { get; init; }` to response record `HubPlanOrder`.
- **[response_required_drift]** HubPlanOrder.planId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Tighten `HubPlanOrder.PlanId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateEquipmentRequest` (request)

- **[missing_optional]** UpdateEquipmentRequest is missing property `attributes` (spec type=array).
  - Endpoints: `PATCH /beta/fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("attributes")] public IReadOnlyList<object>? Attributes { get; init; }` to `UpdateEquipmentRequest`.
- **[missing_optional]** UpdateEquipmentRequest is missing property `engineHours` (spec type=integer/int64).
  - Endpoints: `PATCH /beta/fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("engineHours")] public long? EngineHours { get; init; }` to `UpdateEquipmentRequest`.
- **[missing_optional]** UpdateEquipmentRequest is missing property `equipmentSerialNumber` (spec type=string).
  - Endpoints: `PATCH /beta/fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("equipmentSerialNumber")] public string? EquipmentSerialNumber { get; init; }` to `UpdateEquipmentRequest`.
- **[missing_optional]** UpdateEquipmentRequest is missing property `id` (spec type=string).
  - Endpoints: `PATCH /beta/fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("id")] public string? Id { get; init; }` to `UpdateEquipmentRequest`.
- **[missing_optional]** UpdateEquipmentRequest is missing property `odometerMeters` (spec type=integer/int64).
  - Endpoints: `PATCH /beta/fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }` to `UpdateEquipmentRequest`.
- **[missing_optional]** UpdateEquipmentRequest is missing property `tagIds` (spec type=array).
  - Endpoints: `PATCH /beta/fleet/equipment/{id}`
  - Recommended fix: Add `[JsonPropertyName("tagIds")] public IReadOnlyList<string>? TagIds { get; init; }` to `UpdateEquipmentRequest`.

## LOW (10)

### `HosEldEvent` (response)

- **[extra_property]** HosEldEvent.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.DriverId` (not in spec).
- **[extra_property]** HosEldEvent.engineHours (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.EngineHours` (not in spec).
- **[extra_property]** HosEldEvent.eventCode (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.EventCode` (not in spec).
- **[extra_property]** HosEldEvent.eventTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.EventTime` (not in spec).
- **[extra_property]** HosEldEvent.eventType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.EventType` (not in spec).
- **[extra_property]** HosEldEvent.latitude (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.Latitude` (not in spec).
- **[extra_property]** HosEldEvent.longitude (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.Longitude` (not in spec).
- **[extra_property]** HosEldEvent.odometer (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.Odometer` (not in spec).
- **[extra_property]** HosEldEvent.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /beta/fleet/hos/drivers/eld-events`
  - Recommended fix: Remove `HosEldEvent.VehicleId` (not in spec).

### `HubPlanOrder` (response)

- **[extra_property]** HubPlanOrder.status (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /hub/plan/orders`, `POST /hub/plan/orders`
  - Recommended fix: Remove `HubPlanOrder.Status` (not in spec).

