# Media — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/31-media.md`](../31-media.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. LOW response-side findings on
`MediaFile` and `MediaRetrieval` were intentionally retained as nullable
back-compat properties per the workflow precedent established in
`08-carrier-proposed-assignments`, `13-driver-trailer-assignments`,
`14-driver-vehicle-assignments`, `28-live-sharing-links`,
`29-location-and-speed`, and `30-maintenance`. The single LOW request-side
finding (`CreateMediaRetrievalRequest.cameraId`) was removed since the spec
`MediaRetrievalPostMediaRetrievalRequestBody` does not include it; the
spec-aligned replacements (`inputs` array of camera input strings,
`mediaType` enum) were added under HIGH.

A typed nested `MediaUrlInfo` record was added (mirrors spec
`UrlInfoObjectResponseBody`) and is referenced by both `MediaFile.UrlInfo`
and `MediaRetrieval.UrlInfo`.

Files touched: `src/Samsara.Sdk/Models/Media/MediaModels.cs`,
`src/Samsara.Sdk/Clients/Media/IMediaClient.cs`,
`src/Samsara.Sdk/Clients/Media/MediaClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs` (register
`MediaUrlInfo`), `tools/Samsara.Cli/TuiApp.cs` (Media menu updated to
prompt for the now-required `vehicleIds`, `startTime`, `endTime`).

**HIGH (14)**

- **`ListAsync` (GET /cameras/media) — required query params (3)**: added
  `string vehicleIds`, `string startTime`, `string endTime` as required
  positional parameters; they append unconditionally via
  `QueryBuilder.WithParams(...)`. This is a breaking signature change for
  direct callers of the previous parameterless `ListAsync`.
- **`CreateMediaRetrievalRequest` request — required props (2)**: added
  non-nullable `required IReadOnlyList<string> Inputs` and
  `required string MediaType`. (Typed as `IReadOnlyList<string>` instead
  of `IReadOnlyList<object>` because the spec items are strings with
  enumerated values.)
- **`MediaFile` response — required props (5)**: added `availableAtTime`,
  `endTime`, `input`, `startTime`, `triggerReason` as non-nullable
  `required` strings. `MediaFile` is used only for `GET /cameras/media`, so
  tightening is safe and matches the spec guarantee.
- **`MediaRetrieval` response — required props (4)**: added `input`,
  `mediaType`, `quotaStatus`, `retrievalId`. All four are modeled as
  nullable `string?` because `MediaRetrieval` is shared between
  `GET /cameras/media/retrieval` (which guarantees `input`, `mediaType`
  but not `quotaStatus`/`retrievalId`) and
  `POST /cameras/media/retrieval` (which guarantees `quotaStatus`,
  `retrievalId` but returns none of the GET fields). Same precedent as
  `MaintenanceDvir` in plan 30.

**MEDIUM (15)**

- **Query parameters (4 missing)**: added `inputs`, `mediaTypes`,
  `triggerReasons` (all as `IReadOnlyList<string>?`, comma-joined) and
  `availableAfterTime` (`string?`) to `ListAsync`. All append conditionally
  via `QueryBuilder.WithParams(...)`.
- **`MediaFile` response optionals (2)**: added `cameraRole` (`string?`)
  and `urlInfo` (typed `MediaUrlInfo?`).
- **`MediaFile` required drift (2)**: tightened `MediaType` and `VehicleId`
  to non-nullable `required` per spec. Safe because `MediaFile` is used
  only for `GET /cameras/media`.
- **`MediaRetrieval` response optionals (3)**: added `availableAtTime`
  (`string?`), `cameraRole` (`string?`), `urlInfo` (typed `MediaUrlInfo?`).
- **`MediaRetrieval` required drift (5)**: `endTime`, `startTime`, `status`,
  `vehicleId` — kept nullable. Although the spec marks these required on
  `GET /cameras/media/retrieval`, `MediaRetrieval` is also used as the
  `POST /cameras/media/retrieval` response type, which does not return
  any of these fields. Tightening would cause runtime deserialization
  failures on POST. Documented as nullable with per-endpoint guarantee
  notes in the XML docs. Same precedent as `MaintenanceDvir`.

**LOW (19)**

- **Response extras retained as nullable back-compat (18)**: `MediaFile`
  (`cameraId`, `capturedAtTime`, `driverId`, `driverName`, `durationMs`,
  `id`, `safetyEventId`, `thumbnailUrl`, `uploadedAtTime`, `url`,
  `vehicleName`), `MediaRetrieval` (`cameraId`, `endTime`, `id`,
  `startTime`, `status`, `url`, `vehicleId`). All carry XML docs marking
  them as "Legacy SDK-only field (not in spec inner schema)".
  Note: `MediaRetrieval.endTime`/`startTime`/`status`/`vehicleId` are
  flagged LOW for the POST endpoint but MEDIUM for the GET endpoint —
  retained as nullable as described above.
- **Request extras removed (1)**: `CreateMediaRetrievalRequest.CameraId`
  was removed because it is not in the spec
  `MediaRetrievalPostMediaRetrievalRequestBody` schema; on a request body,
  sending unknown fields would fail server validation. The spec-aligned
  replacements (`Inputs`, `MediaType`) were added under HIGH.

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `MediaFile` | response | 0 | 5 | 4 | 11 |
| `MediaRetrieval` | response | 0 | 4 | 7 | 7 |
| `(no SDK type)` | query | 0 | 3 | 4 | 0 |
| `CreateMediaRetrievalRequest` | request | 0 | 2 | 0 | 1 |

**Counts**: CRITICAL=0, HIGH=14, MEDIUM=15, LOW=19  
**Total deduped findings**: 48

## HIGH (14)

### `(no SDK type)` (query)

- **[missing_required_query]** ListAsync (GET /cameras/media) is missing query parameter `endTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add a required parameter (e.g. `string endTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endTime", ...)`.
- **[missing_required_query]** ListAsync (GET /cameras/media) is missing query parameter `startTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add a required parameter (e.g. `string startTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startTime", ...)`.
- **[missing_required_query]** ListAsync (GET /cameras/media) is missing query parameter `vehicleIds` (spec REQUIRED, type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add a required parameter (e.g. `string vehicleIds` , no default) to the SDK method and append it via `QueryBuilder.WithParams("vehicleIds", ...)`.

### `CreateMediaRetrievalRequest` (request)

- **[missing_required]** CreateMediaRetrievalRequest is missing REQUIRED property `inputs` (spec type=array).
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("inputs")] public required IReadOnlyList<object> Inputs { get; init; }` to `CreateMediaRetrievalRequest`.
- **[missing_required]** CreateMediaRetrievalRequest is missing REQUIRED property `mediaType` (spec type=string enum=['image', 'videoHighRes', 'videoLowRes', 'hyperlapse']).
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("mediaType")] public required string MediaType { get; init; }` to `CreateMediaRetrievalRequest`.

### `MediaFile` (response)

- **[response_drift_required]** MediaFile (response) missing REQUIRED property `availableAtTime` (spec type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add `[JsonPropertyName("availableAtTime")] public string AvailableAtTime { get; init; }` to response record `MediaFile` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MediaFile (response) missing REQUIRED property `endTime` (spec type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add `[JsonPropertyName("endTime")] public string EndTime { get; init; }` to response record `MediaFile` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MediaFile (response) missing REQUIRED property `input` (spec type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add `[JsonPropertyName("input")] public string Input { get; init; }` to response record `MediaFile` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MediaFile (response) missing REQUIRED property `startTime` (spec type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add `[JsonPropertyName("startTime")] public string StartTime { get; init; }` to response record `MediaFile` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MediaFile (response) missing REQUIRED property `triggerReason` (spec type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add `[JsonPropertyName("triggerReason")] public string TriggerReason { get; init; }` to response record `MediaFile` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `MediaRetrieval` (response)

- **[response_drift_required]** MediaRetrieval (response) missing REQUIRED property `input` (spec type=string).
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("input")] public string Input { get; init; }` to response record `MediaRetrieval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MediaRetrieval (response) missing REQUIRED property `mediaType` (spec type=string).
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("mediaType")] public string MediaType { get; init; }` to response record `MediaRetrieval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MediaRetrieval (response) missing REQUIRED property `quotaStatus` (spec type=string).
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("quotaStatus")] public string QuotaStatus { get; init; }` to response record `MediaRetrieval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** MediaRetrieval (response) missing REQUIRED property `retrievalId` (spec type=string).
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("retrievalId")] public string RetrievalId { get; init; }` to response record `MediaRetrieval` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (15)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /cameras/media) is missing query parameter `availableAfterTime` (spec optional, type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add an optional parameter `string? availableAfterTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /cameras/media) is missing query parameter `inputs` (spec optional, type=array).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? inputs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /cameras/media) is missing query parameter `mediaTypes` (spec optional, type=array).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? mediaTypes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /cameras/media) is missing query parameter `triggerReasons` (spec optional, type=array).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? triggerReasons = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `MediaFile` (response)

- **[response_drift_optional]** MediaFile (response) missing property `cameraRole` (spec type=string).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add `[JsonPropertyName("cameraRole")] public string? CameraRole { get; init; }` to response record `MediaFile`.
- **[response_drift_optional]** MediaFile (response) missing property `urlInfo` (spec type=object).
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Add `[JsonPropertyName("urlInfo")] public object? UrlInfo { get; init; }` to response record `MediaFile`.
- **[response_required_drift]** MediaFile.mediaType (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Tighten `MediaFile.MediaType` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** MediaFile.vehicleId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Tighten `MediaFile.VehicleId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `MediaRetrieval` (response)

- **[response_drift_optional]** MediaRetrieval (response) missing property `availableAtTime` (spec type=string).
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("availableAtTime")] public string? AvailableAtTime { get; init; }` to response record `MediaRetrieval`.
- **[response_drift_optional]** MediaRetrieval (response) missing property `cameraRole` (spec type=string).
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("cameraRole")] public string? CameraRole { get; init; }` to response record `MediaRetrieval`.
- **[response_drift_optional]** MediaRetrieval (response) missing property `urlInfo` (spec type=object).
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Add `[JsonPropertyName("urlInfo")] public object? UrlInfo { get; init; }` to response record `MediaRetrieval`.
- **[response_required_drift]** MediaRetrieval.endTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Tighten `MediaRetrieval.EndTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** MediaRetrieval.startTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Tighten `MediaRetrieval.StartTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** MediaRetrieval.status (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Tighten `MediaRetrieval.Status` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** MediaRetrieval.vehicleId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /cameras/media/retrieval`
  - Recommended fix: Tighten `MediaRetrieval.VehicleId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (19)

### `CreateMediaRetrievalRequest` (request)

- **[extra_property]** CreateMediaRetrievalRequest.cameraId: present in SDK but not in spec inner schema.
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Remove `CreateMediaRetrievalRequest.CameraId` (not in spec).

### `MediaFile` (response)

- **[extra_property]** MediaFile.cameraId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.CameraId` (not in spec).
- **[extra_property]** MediaFile.capturedAtTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.CapturedAtTime` (not in spec).
- **[extra_property]** MediaFile.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.DriverId` (not in spec).
- **[extra_property]** MediaFile.driverName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.DriverName` (not in spec).
- **[extra_property]** MediaFile.durationMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.DurationMs` (not in spec).
- **[extra_property]** MediaFile.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.Id` (not in spec).
- **[extra_property]** MediaFile.safetyEventId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.SafetyEventId` (not in spec).
- **[extra_property]** MediaFile.thumbnailUrl (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.ThumbnailUrl` (not in spec).
- **[extra_property]** MediaFile.uploadedAtTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.UploadedAtTime` (not in spec).
- **[extra_property]** MediaFile.url (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.Url` (not in spec).
- **[extra_property]** MediaFile.vehicleName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /cameras/media`
  - Recommended fix: Remove `MediaFile.VehicleName` (not in spec).

### `MediaRetrieval` (response)

- **[extra_property]** MediaRetrieval.cameraId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /cameras/media/retrieval`, `POST /cameras/media/retrieval`
  - Recommended fix: Remove `MediaRetrieval.CameraId` (not in spec).
- **[extra_property]** MediaRetrieval.endTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Remove `MediaRetrieval.EndTime` (not in spec).
- **[extra_property]** MediaRetrieval.id (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /cameras/media/retrieval`, `POST /cameras/media/retrieval`
  - Recommended fix: Remove `MediaRetrieval.Id` (not in spec).
- **[extra_property]** MediaRetrieval.startTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Remove `MediaRetrieval.StartTime` (not in spec).
- **[extra_property]** MediaRetrieval.status (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Remove `MediaRetrieval.Status` (not in spec).
- **[extra_property]** MediaRetrieval.url (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /cameras/media/retrieval`, `POST /cameras/media/retrieval`
  - Recommended fix: Remove `MediaRetrieval.Url` (not in spec).
- **[extra_property]** MediaRetrieval.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /cameras/media/retrieval`
  - Recommended fix: Remove `MediaRetrieval.VehicleId` (not in spec).

