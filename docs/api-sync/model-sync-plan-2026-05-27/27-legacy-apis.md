# Legacy APIs — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/27-legacy-apis.md`](../27-legacy-apis.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 3 | 24 | 0 |

**Counts**: CRITICAL=0, HIGH=3, MEDIUM=24, LOW=0  
**Total deduped findings**: 27

## HIGH (3)

### `(no SDK type)` (query)

- **[missing_required_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `endTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add a required parameter (e.g. `string endTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endTime", ...)`.
- **[missing_required_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `startTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add a required parameter (e.g. `string startTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startTime", ...)`.
- **[missing_required_query]** V1GetVehicleHarshEventAsync (GET /v1/fleet/vehicles/{vehicleId}/safety/harsh_event) is missing query parameter `timestamp` (spec REQUIRED, type=integer).
  - Endpoints: `GET /v1/fleet/vehicles/{vehicleId}/safety/harsh_event`
  - Recommended fix: Add a required parameter (e.g. `int timestamp` , no default) to the SDK method and append it via `QueryBuilder.WithParams("timestamp", ...)`.

## MEDIUM (24)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriversVehicleAssignmentsAsync (GET /fleet/drivers/vehicle-assignments) is missing query parameter `driverActivationStatus` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers/vehicle-assignments`
  - Recommended fix: Add an optional parameter `string? driverActivationStatus = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriversVehicleAssignmentsAsync (GET /fleet/drivers/vehicle-assignments) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers/vehicle-assignments`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriversVehicleAssignmentsAsync (GET /fleet/drivers/vehicle-assignments) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers/vehicle-assignments`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehiclesDriverAssignmentsAsync (GET /fleet/vehicles/driver-assignments) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles/driver-assignments`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `isPtoActive` (spec optional, type=boolean).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add an optional parameter `bool? isPtoActive = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDvirDefectsHistoryAsync (GET /fleet/defects/history) is missing query parameter `isResolved` (spec optional, type=boolean).
  - Endpoints: `GET /fleet/defects/history`
  - Recommended fix: Add an optional parameter `bool? isResolved = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `limit` (spec optional, type=integer).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add an optional parameter `int? limit = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `minIdlingDurationMinutes` (spec optional, type=integer).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add an optional parameter `int? minIdlingDurationMinutes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriversVehicleAssignmentsAsync (GET /fleet/drivers/vehicle-assignments) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers/vehicle-assignments`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDvirHistoryAsync (GET /fleet/dvirs/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/dvirs/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSafetyEventsAsync (GET /fleet/safety-events) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/safety-events`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehiclesDriverAssignmentsAsync (GET /fleet/vehicles/driver-assignments) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles/driver-assignments`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriversVehicleAssignmentsAsync (GET /fleet/drivers/vehicle-assignments) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers/vehicle-assignments`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehiclesDriverAssignmentsAsync (GET /fleet/vehicles/driver-assignments) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles/driver-assignments`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriversVehicleAssignmentsAsync (GET /fleet/drivers/vehicle-assignments) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers/vehicle-assignments`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDvirHistoryAsync (GET /fleet/dvirs/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/dvirs/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSafetyEventsAsync (GET /fleet/safety-events) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/safety-events`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehiclesDriverAssignmentsAsync (GET /fleet/vehicles/driver-assignments) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles/driver-assignments`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehicleIdlingReportAsync (GET /fleet/reports/vehicle/idling) is missing query parameter `vehicleIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/reports/vehicle/idling`
  - Recommended fix: Add an optional parameter `string? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSafetyEventsAsync (GET /fleet/safety-events) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/safety-events`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetVehiclesDriverAssignmentsAsync (GET /fleet/vehicles/driver-assignments) is missing query parameter `vehicleIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles/driver-assignments`
  - Recommended fix: Add an optional parameter `string? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

