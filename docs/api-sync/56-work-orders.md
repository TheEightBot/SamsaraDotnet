# Work Orders — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/7 endpoints implemented)  
> **SDK Client**: `IMaintenanceClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../MaintenanceClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Maintenance/MaintenanceModels.cs`  

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

**Model audit (2025-05-13):** All three Work Order models were rebuilt from scratch with correct API fields.

- `WorkOrder`, `CreateWorkOrderRequest`, `UpdateWorkOrderRequest`: all replaced with correct schema fields including `assetId` (required on create), `serviceTaskInstances`, `items`, `discount`, `tax`, `assignedUserIds`, `dueDate`, `notes`, `status`, and related fields. Previous implementation did not match the API schema.
