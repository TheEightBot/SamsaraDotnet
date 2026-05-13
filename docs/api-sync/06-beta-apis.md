# Beta APIs — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/66 endpoints implemented)  
> **SDK Client**: `multiple`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../multiple.cs`  
> **Models**: `src/Samsara.Sdk/various`  

---

## Endpoints

### ⚠️ `GET /assets/depreciation`
**Operation ID**: `getDepreciationTransactions`  
**Summary**: [beta] Get depreciation transactions.  
**Parameters**: `startTime`, `endTime`, `assetIds`, `after`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /assets/inputs/stream`
**Operation ID**: `getAssetsInputs`  
**Summary**: [beta] List asset inputs data in an organization.  
**Parameters**: `ids`, `type`, `after`, `startTime`, `endTime`, `includeExternalIds`, `includeTags`, `includeAttributes`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /beta/aemp/Fleet/{pageNumber}`
**Operation ID**: `getAempEquipmentList`  
**Summary**: [beta] Get a list of AEMP equipment  
**Parameters**: `pageNumber`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /beta/fleet/drivers/efficiency`
**Operation ID**: `getDriverEfficiency`  
**Summary**: [beta] List driver efficiency  
**Parameters**: `driverActivationStatus`, `driverIds`, `after`, `driverTagIds`, `driverParentTagIds`, `startTime`, `endTime`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PATCH /beta/fleet/equipment/{id}`
**Operation ID**: `patchEquipment`  
**Summary**: [beta] Update an equipment  
**Parameters**: `id`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /beta/fleet/hos/drivers/eld-events`
**Operation ID**: `getHosEldEvents`  
**Summary**: [beta] Get driver HOS ELD events  
**Parameters**: `startTime`, `endTime`, `driverIds`, `tagIds`, `parentTagIds`, `driverActivationStatus`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PATCH /beta/fleet/vehicles/{id}/immobilizer`
**Operation ID**: `updateEngineImmobilizerState`  
**Summary**: [beta] Update engine immobilizer state of a vehicle.  
**Parameters**: `id`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /beta/industrial/jobs`
**Operation ID**: `deleteJob`  
**Summary**: [beta] Deletes an existing job  
**Parameters**: `id`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /beta/industrial/jobs`
**Operation ID**: `getJobs`  
**Summary**: [beta] Fetches all jobs  
**Parameters**: `id`, `startDate`, `endDate`, `industrialAssetIds`, `fleetDeviceIds`, `status`, `customerName`, `after`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PATCH /beta/industrial/jobs`
**Operation ID**: `patchJob`  
**Summary**: [beta] Patches a job  
**Parameters**: `id`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /beta/industrial/jobs`
**Operation ID**: `createJob`  
**Summary**: [beta] Create a job  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /detections/stream`
**Operation ID**: `getDetections`  
**Summary**: [beta] Returns a list of all detections in an organization.  
**Parameters**: `driverIds`, `assetIds`, `detectionBehaviorLabels`, `inboxFilterReason`, `inboxEvent`, `inCabAlertPlayed`, `tagIds`, `includeAsset`, `includeDriver`, `startTime`, `endTime`, `after`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /devices`
**Operation ID**: `getDevices`  
**Summary**: [beta] List devices.  
**Parameters**: `models`, `healthStatuses`, `includeHealth`, `after`, `limit`, `includeTags`, `tagIds`, `parentTagIds`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/assets/device-recovery-missing`
**Operation ID**: `listDeviceRecoveryMissingAssets`  
**Summary**: [beta] List missing device recovery assets.  
**Parameters**: `after`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /fleet/assets/device-recovery/{id}/missing`
**Operation ID**: `markAssetMissing`  
**Summary**: [beta] Mark an asset as missing.  
**Parameters**: `id`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /fleet/assets/device-recovery/{id}/recovered`
**Operation ID**: `recoverAsset`  
**Summary**: [beta] Mark an asset as recovered.  
**Parameters**: `id`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /fleet/drivers/voice-sign-in/resolve-assignment`
**Operation ID**: `resolveAssignmentByDetails`  
**Summary**: [beta] Voice sign-in: resolve driver and create assignment  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /fleet/drivers/workflow-assignments`
**Operation ID**: `postDriverWorkflowAssignment`  
**Summary**: [beta] Publish a driver app workflow to drivers  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/drivers/workflows`
**Operation ID**: `listDriverWorkflows`  
**Summary**: [beta] List driver app workflows  
**Parameters**: `after`, `limit`, `workflowType`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/maintenance/vendor-categories`
**Operation ID**: `listVendorCategories`  
**Summary**: [beta] List all vendor categories.  
**Parameters**: `after`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/maintenance/vendors`
**Operation ID**: `listMaintenanceVendors`  
**Summary**: [beta] List all maintenance vendors.  
**Parameters**: `ids`, `includeExternalIds`, `after`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/tachograph-live-data/latest`
**Operation ID**: `listTachographLiveData`  
**Summary**: [beta] List tachograph live data  
**Parameters**: `driverIds`, `vehicleIds`, `startTime`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /fleet/vehicles/immobilizer/stream`
**Operation ID**: `getEngineImmobilizerStates`  
**Summary**: [beta] Get engine immobilizer states  
**Parameters**: `vehicleIds`, `startTime`, `endTime`, `after`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /functions`
**Operation ID**: `createFunction`  
**Summary**: [beta] Create a Function.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /functions-storage/files`
**Operation ID**: `deleteFunctionStorageFile`  
**Summary**: [beta] Delete a Functions storage file.  
**Parameters**: `name`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /functions-storage/files`
**Operation ID**: `getFunctionStorageFile`  
**Summary**: [beta] Get a Functions storage file.  
**Parameters**: `name`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /functions-storage/files`
**Operation ID**: `createFunctionStorageFile`  
**Summary**: [beta] Create a Functions storage file.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PUT /functions-storage/files`
**Operation ID**: `updateFunctionStorageFile`  
**Summary**: [beta] Update a Functions storage file.  
**Parameters**: `name`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /functions-storage/ls`
**Operation ID**: `listFunctionsStorageFiles`  
**Summary**: [beta] List Functions storage files.  
**Parameters**: `after`, `limit`, `includeDownloadUrls`, `includeUploadUrls`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /functions/{name}`
**Operation ID**: `deleteFunction`  
**Summary**: [beta] Delete a Function.  
**Parameters**: `name`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /functions/{name}`
**Operation ID**: `getFunction`  
**Summary**: [beta] Get a Function.  
**Parameters**: `name`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PATCH /functions/{name}`
**Operation ID**: `patchFunction`  
**Summary**: [beta] Update a Function.  
**Parameters**: `name`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /functions/{name}/deploy`
**Operation ID**: `deployFunction`  
**Summary**: [beta] Deploy a Function.  
**Parameters**: `name`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /functions/{name}/logs`
**Operation ID**: `getFunctionLogs`  
**Summary**: [beta] Get Function logs.  
**Parameters**: `startTime`, `endTime`, `after`, `limit`, `filterText`, `name`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /functions/{name}/runs`
**Operation ID**: `startFunctionRun`  
**Summary**: [beta] Start a Function run.  
**Parameters**: `name`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /functions/{name}/runs/{correlationId}`
**Operation ID**: `getFunctionRun`  
**Summary**: [beta] Get a Function run.  
**Parameters**: `name`, `correlationId`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PATCH /hos/daily-logs/log-meta-data`
**Operation ID**: `updateShippingDocs`  
**Summary**: [beta] Update the shippingDocs field of an existing assignment.  
**Parameters**: `hosDate`, `driverID`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /hub/plan/orders`
**Operation ID**: `deletePlanOrders`  
**Summary**: [beta] Delete orders from a plan  
**Parameters**: `planId`, `orderIds`, `deleteAll`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /hub/plan/orders`
**Operation ID**: `listPlanOrders`  
**Summary**: [beta] List orders for a specific plan  
**Parameters**: `planId`, `orderIds`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /hub/route-templates`
**Operation ID**: `deleteHubRouteTemplate`  
**Summary**: [beta] Delete a route template  
**Parameters**: `id`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /hub/route-templates`
**Operation ID**: `listHubRouteTemplates`  
**Summary**: [beta] List route templates for a specific hub  
**Parameters**: `hubId`, `id`, `name`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /qualification-records`
**Operation ID**: `deleteQualificationRecord`  
**Summary**: [beta] Delete a qualification record.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /qualification-records`
**Operation ID**: `getQualificationRecords`  
**Summary**: [beta] Get a list of specified qualification records.  
**Parameters**: `ids`, `includeExternalIds`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PATCH /qualification-records`
**Operation ID**: `patchQualificationRecord`  
**Summary**: [beta] Update a qualification record.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /qualification-records`
**Operation ID**: `postQualificationRecord`  
**Summary**: [beta] Create a qualification record.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /qualification-records/archive`
**Operation ID**: `archiveQualificationRecord`  
**Summary**: [beta] Archive a qualification record.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /qualification-records/stream`
**Operation ID**: `getQualificationRecordsStream`  
**Summary**: [beta] Get a stream of filtered qualification records.  
**Parameters**: `entityType`, `startTime`, `endTime`, `after`, `qualificationTypeIds`, `ownerIds`, `includeDeleted`, `includeExternalIds`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /qualification-records/unarchive`
**Operation ID**: `unarchiveQualificationRecord`  
**Summary**: [beta] Unarchive a qualification record.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /qualification-types`
**Operation ID**: `getQualificationTypes`  
**Summary**: [beta] Get a list of qualification types.  
**Parameters**: `entityType`, `ids`, `after`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /readings`
**Operation ID**: `postReadings`  
**Summary**: [beta] Ingest Readings  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /reports/configs`
**Operation ID**: `getReportConfigs`  
**Summary**: [beta] Get custom report configs  
**Parameters**: `after`, `limit`, `ids`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /reports/datasets`
**Operation ID**: `getDatasets`  
**Summary**: [beta] Get custom report datasets  
**Parameters**: `ids`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /reports/runs`
**Operation ID**: `getReportRuns`  
**Summary**: [beta] Get custom report runs  
**Parameters**: `reportConfigIds`, `ids`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /reports/runs`
**Operation ID**: `createReportRun`  
**Summary**: [beta] Create a custom report run  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /reports/runs/data`
**Operation ID**: `getReportRunData`  
**Summary**: [beta] Get custom report run data  
**Parameters**: `id`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /ridership/passengers`
**Operation ID**: `deleteRidershipPassenger`  
**Summary**: [beta] Delete a ridership passenger.  
**Parameters**: `id`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /ridership/passengers`
**Operation ID**: `listRidershipPassengers`  
**Summary**: [beta] List ridership passengers.  
**Parameters**: `tagId`, `after`, `limit`, `includeExternalIds`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /ridership/passengers`
**Operation ID**: `createRidershipPassenger`  
**Summary**: [beta] Create a ridership passenger.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PUT /ridership/passengers`
**Operation ID**: `updateRidershipPassenger`  
**Summary**: [beta] Update a ridership passenger.  
**Parameters**: `id`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /ridership/passengers/{id}`
**Operation ID**: `getRidershipPassenger`  
**Summary**: [beta] Get a ridership passenger.  
**Parameters**: `includeExternalIds`, `id`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `DELETE /ridership/route-setups`
**Operation ID**: `deleteRidershipRouteSetup`  
**Summary**: [beta] Delete a ridership route setup.  
**Parameters**: `routeId`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /ridership/route-setups`
**Operation ID**: `listRidershipRouteSetups`  
**Summary**: [beta] List ridership route setups.  
**Parameters**: `accountId`, `after`, `limit`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `POST /ridership/route-setups`
**Operation ID**: `createRidershipRouteSetup`  
**Summary**: [beta] Create a ridership route setup.  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PUT /ridership/route-setups`
**Operation ID**: `updateRidershipRouteSetup`  
**Summary**: [beta] Update a ridership route setup.  
**Parameters**: `routeId`  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `GET /ridership/route-setups/{routeId}`
**Operation ID**: `getRidershipRouteSetup`  
**Summary**: [beta] Get a ridership route setup.  
**Parameters**: `routeId`  
**Request Body**: No  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ⚠️ `PATCH /safety-events/batch`
**Operation ID**: `patchSafetyEventsV2Batch`  
**Summary**: [beta] Update Safety Events  
**Request Body**: Yes  

- [ ] Method defined in `multiple`
- [ ] Method implemented in `multiple.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/various` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
