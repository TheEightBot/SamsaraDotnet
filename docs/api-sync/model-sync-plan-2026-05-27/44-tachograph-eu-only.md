# Tachograph (EU Only) — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/44-tachograph-eu-only.md`](../44-tachograph-eu-only.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `5f20235` on 2026-05-27**

## Implementation notes

All MEDIUM findings were applied; the LOW `extra_property` findings were
intentionally retained as nullable back-compat properties per the workflow
precedent established in `40-safety-scores`, `42-settings`, and
`43-speeding-intervals` — flat-scalar conveniences present in the SDK but absent
from the current spec inner schema kept (now ordered after the spec props)
rather than removed outright. All three records/methods are deserialize- /
query-only with no construction sites in `src`/`tools`/`tests`.

**MEDIUM (14)**

- **`(query)` — 9 optional params across 3 methods**: `ListActivitiesAsync` and
  `ListFilesAsync` each gained `driverIds`, `tagIds`, `parentTagIds`;
  `ListVehicleFilesAsync` gained `vehicleIds`, `tagIds`, `parentTagIds`. All
  added as trailing `IReadOnlyList<string>? = null` parameters (entity-id filter
  first, then `tagIds`, then `parentTagIds`, before `cancellationToken`) and
  comma-joined onto the query string via `QueryBuilder.WithParams` wrapping the
  existing `QueryBuilder.WithTimeRange`, mirroring
  `ISafetyClient.GetEventsStreamAsync`. Non-breaking (all optional, appended
  after `startTime`/`endTime`).
- **`TachographActivity` (response) — 2 optional props**: `activity`
  (`IReadOnlyList<object>?`) and `driver` (`object?`), left weakly-typed per the
  plan and ordered ahead of the retained extras.
- **`TachographFile` (response) — 3 optional props**: `driver` (`object?`),
  `files` (`IReadOnlyList<object>?`), and `vehicle` (`object?`), left
  weakly-typed per the plan and ordered ahead of the retained extras.

**LOW (21) — kept as nullable back-compat extras (conservative; not removed)**

- `TachographActivity`: `id`, `driverId`, `driverName`, `vehicleId`,
  `vehicleName`, `activityType`, `startTime`, `endTime`, `durationMs`,
  `country`, `region` (`id` demoted from `required` to nullable).
- `TachographFile`: `id`, `driverId`, `driverName`, `vehicleId`, `vehicleName`,
  `fileType`, `downloadUrl`, `createdAtTime`, `startTime`, `endTime` (`id`
  demoted from `required` to nullable).

The new response props are weakly-typed `object` / `IReadOnlyList<object>`, so
no new top-level types are introduced and `SamsaraJsonContext` (both records
already registered) needed no change. Two CLI call sites in `TuiApp.cs` that
passed the `CancellationToken` positionally as the 3rd argument were switched to
the named `cancellationToken:` form (the 3rd positional slot is now `driverIds`).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 0 | 9 | 0 |
| `TachographFile` | response | 0 | 0 | 3 | 10 |
| `TachographActivity` | response | 0 | 0 | 2 | 11 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=14, LOW=21  
**Total deduped findings**: 35

## MEDIUM (14)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListActivitiesAsync (GET /fleet/drivers/tachograph-activity/history) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListFilesAsync (GET /fleet/drivers/tachograph-files/history) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListActivitiesAsync (GET /fleet/drivers/tachograph-activity/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListFilesAsync (GET /fleet/drivers/tachograph-files/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListVehicleFilesAsync (GET /fleet/vehicles/tachograph-files/history) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListActivitiesAsync (GET /fleet/drivers/tachograph-activity/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListFilesAsync (GET /fleet/drivers/tachograph-files/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListVehicleFilesAsync (GET /fleet/vehicles/tachograph-files/history) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListVehicleFilesAsync (GET /fleet/vehicles/tachograph-files/history) is missing query parameter `vehicleIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `TachographActivity` (response)

- **[response_drift_optional]** TachographActivity (response) missing property `activity` (spec type=array).
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Add `[JsonPropertyName("activity")] public IReadOnlyList<object>? Activity { get; init; }` to response record `TachographActivity`.
- **[response_drift_optional]** TachographActivity (response) missing property `driver` (spec type=object).
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object? Driver { get; init; }` to response record `TachographActivity`.

### `TachographFile` (response)

- **[response_drift_optional]** TachographFile (response) missing property `driver` (spec type=object).
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object? Driver { get; init; }` to response record `TachographFile`.
- **[response_drift_optional]** TachographFile (response) missing property `files` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Add `[JsonPropertyName("files")] public IReadOnlyList<object>? Files { get; init; }` to response record `TachographFile`.
- **[response_drift_optional]** TachographFile (response) missing property `vehicle` (spec type=object).
  - Endpoints: `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Add `[JsonPropertyName("vehicle")] public object? Vehicle { get; init; }` to response record `TachographFile`.

## LOW (21)

### `TachographActivity` (response)

- **[extra_property]** TachographActivity.activityType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.ActivityType` (not in spec).
- **[extra_property]** TachographActivity.country (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.Country` (not in spec).
- **[extra_property]** TachographActivity.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.DriverId` (not in spec).
- **[extra_property]** TachographActivity.driverName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.DriverName` (not in spec).
- **[extra_property]** TachographActivity.durationMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.DurationMs` (not in spec).
- **[extra_property]** TachographActivity.endTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.EndTime` (not in spec).
- **[extra_property]** TachographActivity.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.Id` (not in spec).
- **[extra_property]** TachographActivity.region (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.Region` (not in spec).
- **[extra_property]** TachographActivity.startTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.StartTime` (not in spec).
- **[extra_property]** TachographActivity.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.VehicleId` (not in spec).
- **[extra_property]** TachographActivity.vehicleName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/drivers/tachograph-activity/history`
  - Recommended fix: Remove `TachographActivity.VehicleName` (not in spec).

### `TachographFile` (response)

- **[extra_property]** TachographFile.createdAtTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.CreatedAtTime` (not in spec).
- **[extra_property]** TachographFile.downloadUrl (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.DownloadUrl` (not in spec).
- **[extra_property]** TachographFile.driverId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.DriverId` (not in spec).
- **[extra_property]** TachographFile.driverName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.DriverName` (not in spec).
- **[extra_property]** TachographFile.endTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.EndTime` (not in spec).
- **[extra_property]** TachographFile.fileType (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.FileType` (not in spec).
- **[extra_property]** TachographFile.id (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.Id` (not in spec).
- **[extra_property]** TachographFile.startTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.StartTime` (not in spec).
- **[extra_property]** TachographFile.vehicleId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.VehicleId` (not in spec).
- **[extra_property]** TachographFile.vehicleName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/drivers/tachograph-files/history`, `GET /fleet/vehicles/tachograph-files/history`
  - Recommended fix: Remove `TachographFile.VehicleName` (not in spec).

