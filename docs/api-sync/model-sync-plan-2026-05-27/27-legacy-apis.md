# Legacy APIs — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/27-legacy-apis.md`](../27-legacy-apis.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `4088176` on 2026-05-27**

## Implementation notes

All HIGH (3) and MEDIUM (24) findings were applied — 27 total. There were no
LOW findings for this domain.

Files touched:

- `src/Samsara.Sdk/Clients/Legacy/LegacyApisClient.cs` — interface + impl,
  query surface expanded across seven endpoints.
- `CHANGELOG.md`, `docs/api-sync/27-legacy-apis.md` — banners.

The legacy v1 schemas are loosely defined and the SDK keeps `object` /
`object?` for response payloads (per the file-level guidance). No new model
records were introduced — the entire change is in method signatures and
query-string composition.

**Endpoint-by-endpoint summary**

- **`GET /fleet/reports/vehicle/idling` (`GetVehicleIdlingReportAsync`)** —
  Converted from a single-page `Task<object>` to paginated
  `IAsyncEnumerable<object>` using `PaginateAsync`. This pattern matches
  every other paginated legacy endpoint on the client (DVIRs, defects,
  driver/vehicle assignments, safety events) and lets `after` and `limit`
  be absorbed by the shared pagination helper instead of being exposed as
  extra method parameters. Two HIGH spec-REQUIRED params (`startTime`,
  `endTime`) added as non-nullable `DateTimeOffset` and pushed through
  `QueryBuilder.WithTimeRange`. Five MEDIUM optional filters
  (`vehicleIds`, `tagIds`, `parentTagIds`, `isPtoActive`,
  `minIdlingDurationMinutes`) added as nullable scalars matching the
  spec parameter types. `isPtoActive` is lowercased via
  `ToString(CultureInfo.InvariantCulture).ToLowerInvariant()`;
  `minIdlingDurationMinutes` is rendered with invariant culture.

- **`GET /v1/fleet/vehicles/{vehicleId}/safety/harsh_event`
  (`V1GetVehicleHarshEventAsync`)** — The HIGH finding flagged
  `timestamp` (required, int64) as missing. The SDK already forwarded the
  value to the wire as `timestamp` via `QueryBuilder.WithParams`, but the
  C# parameter was named `timestampMs`. The analyzer matches by parameter
  name — renamed to `timestamp` and switched the string conversion to
  `CultureInfo.InvariantCulture` (was using the default
  `long.ToString()`). Wire format is unchanged.

- **`GET /fleet/drivers/vehicle-assignments`
  (`GetDriversVehicleAssignmentsAsync`)** — Added six optional filters per
  the plan: `driverIds` (array, joined with `,`), `startTime`/`endTime`
  (RFC 3339 via `WithTimeRange`), `tagIds`, `parentTagIds`,
  `driverActivationStatus`. Pagination cursor `after` continues to be
  handled by `PaginateAsync`.

- **`GET /fleet/vehicles/driver-assignments`
  (`GetVehiclesDriverAssignmentsAsync`)** — Added five optional filters:
  `startTime`/`endTime`, `vehicleIds`, `tagIds`, `parentTagIds`. Per the
  spec these are all plain `string` (comma-joined IDs), not arrays.

- **`GET /fleet/dvirs/history` (`GetDvirHistoryAsync`)** — Added
  `parentTagIds` and `tagIds` (both arrays in the spec, joined with `,`).
  Existing `startTime`/`endTime` retained; the spec marks them REQUIRED
  but they remain nullable in the SDK signature for backward
  compatibility with existing callers — surfaced in the doc-comment.

- **`GET /fleet/defects/history` (`GetDvirDefectsHistoryAsync`)** — Added
  `isResolved` (bool, lowercased). `startTime`/`endTime` similarly remain
  nullable for backward compatibility.

- **`GET /fleet/safety-events` (`GetSafetyEventsAsync`)** — Added
  `tagIds`, `parentTagIds`, `vehicleIds` (all arrays per the spec).

- **`GET /fleet/safety-events/audit-logs/feed`
  (`GetSafetyEventsAuditFeedAsync`)** — No plan findings; signature
  unchanged. (Note: the SDK exposes an `endTime` parameter that is not in
  the spec — kept as-is to avoid an out-of-scope removal.)

**Conventions followed**

- Array query parameters joined with `,` via
  `string.Join(",", parameters)` per `FuelClient` /
  `QualificationRecordsClient` precedent.
- Boolean parameters rendered as `true` / `false` via
  `ToString(CultureInfo.InvariantCulture).ToLowerInvariant()`.
- Integer parameters rendered with `CultureInfo.InvariantCulture`.
- Date/time parameters: `DateTimeOffset` (or `DateTimeOffset?`) routed
  through `QueryBuilder.WithTimeRange` (RFC 3339 round-trip format `"O"`).

**Verification**

- `dotnet build Samsara.Dotnet.sln` — `0 Warning(s) 0 Error(s)`.
- `dotnet test tests/Samsara.Sdk.Tests` — `Passed: 59, Failed: 0`.
- `python3 tools/check-sdk-sync.py --spec-file samsara-api.json
  --fail-on-mismatch` — `MISMATCHED: 0`,
  `Spec ops not implemented: 0`.

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

