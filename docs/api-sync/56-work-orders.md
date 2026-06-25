# Work Orders — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/56-work-orders.md`](model-sync-plan-2026-05-27/56-work-orders.md). All 26 findings (0 CRIT / 2 HIGH / 20 MED / 4 LOW) applied across the work-order, service-task, and invoice-scan endpoints. `DeleteWorkOrdersAsync` was re-signatured from `string[] ids` to a single required **`string id`** (the spec's `DELETE /maintenance/work-orders` takes one `id` — **breaking**, but SAFE: no callers in src/tools/tests). `PostInvoiceScanRequest` gained the HIGH **`required object File`**, the MED `assetId` (`string?`), and its non-spec `imageBase64` extra was DEMOTED from `required` to nullable and RETAINED. `ServiceTask` gained four nullable props (`category`, `estimatedLaborTimeMinutes` as `int?`, `estimatedPartsCost` as `object?`, `subcategory`) and tightened `name` to **`required string`**; its non-spec `laborCostCents` extra was RETAINED. `InvoiceScan` tightened `workOrderId` to **`required string`** and DEMOTED its non-spec `id` extra from `required` to nullable (retained, else deserialization breaks); `status` retained. `WorkOrder` gained the MED `maintenanceSite` (`object?`); `CreateWorkOrderRequest`/`UpdateWorkOrderRequest` each gained `placeExternalId`/`placeId` (`string?`). Eight optional query params were added across the three list/stream methods. **Breaking**: consumers may now rely on non-null `InvoiceScan.WorkOrderId`/`ServiceTask.Name`, and `PostInvoiceScanRequest` now requires `file` instead of `imageBase64`. No JsonContext changes (all types already registered; new props are weakly-typed `object`/scalar/array → no new top-level types). No CLI or test changes (no construction sites, no fixtures, no callers).  
> **SDK Client**: `IWorkOrdersClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/Maintenance/WorkOrdersClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Maintenance/WorkOrderModels.cs`  

---

## Endpoints

### ⚠️ `POST /maintenance/invoice-scans`
**Operation ID**: `postInvoiceScan`  
**Summary**: Process an invoice scan.  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /maintenance/service-tasks`
**Operation ID**: `getServiceTasks`  
**Summary**: Gets service tasks.  
**Parameters**: `ids`, `includeArchived`, `after`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `DELETE /maintenance/work-orders`
**Operation ID**: `deleteWorkOrders`  
**Summary**: Deletes a work order.  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /maintenance/work-orders`
**Operation ID**: `getWorkOrders`  
**Summary**: Gets work orders.  
**Parameters**: `ids`, `after`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `PATCH /maintenance/work-orders`
**Operation ID**: `patchWorkOrders`  
**Summary**: Updates a work order.  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `POST /maintenance/work-orders`
**Operation ID**: `postWorkOrders`  
**Summary**: Creates a work order.  
**Request Body**: Yes  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /maintenance/work-orders/stream`
**Operation ID**: `streamWorkOrders`  
**Summary**: Stream work orders.  
**Parameters**: `after`, `startTime`, `endTime`, `workOrderStatuses`, `assetIds`, `assignedUserIds`  
**Request Body**: No  

- [x] Method defined in `IMaintenanceClient`
- [x] Method implemented in `MaintenanceClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Maintenance/WorkOrderModels.cs` for model definitions used by this domain.

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

**Model sync (2026-05-27):** applied the per-domain remediation plan (0 CRIT / 2 HIGH / 20 MED / 4 LOW — 26 total). See [`model-sync-plan-2026-05-27/56-work-orders.md`](model-sync-plan-2026-05-27/56-work-orders.md) for the full breakdown.

- `DeleteWorkOrdersAsync`: re-signatured from `string[] ids` to a single required `string id` (spec is singular — **breaking**, but SAFE: no callers).
- `PostInvoiceScanRequest`: added HIGH `file` (`required object`) and MED `assetId` (`string?`); DEMOTED non-spec `imageBase64` from `required` to nullable (retained).
- `ServiceTask`: added `category`, `estimatedLaborTimeMinutes` (`int?`), `estimatedPartsCost` (`object?`), `subcategory`; tightened `name` to `required string`; retained non-spec `laborCostCents` (LOW).
- `InvoiceScan`: tightened `workOrderId` to `required string`; DEMOTED non-spec `id` from `required` to nullable (retained, else deserialization breaks); retained `status` (LOW).
- `WorkOrder`: added `maintenanceSite` (`object?`). `CreateWorkOrderRequest`/`UpdateWorkOrderRequest`: each added `placeExternalId`/`placeId` (`string?`).
- Query params: `ListServiceTasksAsync` +`ids`/`includeArchived`; `ListWorkOrdersAsync` +`ids`/`includeExternalIds`; `GetWorkOrdersStreamAsync` +`assetIds`/`assignedUserIds`/`workOrderStatuses`/`includeExternalIds`.
- **Breaking**: consumers may now rely on non-null `InvoiceScan.WorkOrderId`/`ServiceTask.Name`; `PostInvoiceScanRequest` requires `file` instead of `imageBase64`.

**2026-06-22 sync:** `GET /maintenance/work-order-templates` added (beta) →
`IWorkOrdersClient.GetWorkOrderTemplatesAsync` (paginated, loosely typed `object` pending a stable
template schema).

**Model audit (2025-05-13):** All three Work Order models were rebuilt from scratch with correct API fields.

- `WorkOrder`, `CreateWorkOrderRequest`, `UpdateWorkOrderRequest`: all replaced with correct schema fields including `assetId` (required on create), `serviceTaskInstances`, `items`, `discount`, `tax`, `assignedUserIds`, `dueDate`, `notes`, `status`, and related fields. Previous implementation did not match the API schema.
