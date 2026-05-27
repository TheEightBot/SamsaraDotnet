# Documents — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/11-documents.md`](../11-documents.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `Document` | response | 0 | 3 | 7 | 5 |
| `(no SDK type)` | query | 0 | 2 | 2 | 0 |
| `CreateDocumentRequest` | request | 0 | 1 | 4 | 0 |
| `DocumentPdfJob` | response | 0 | 0 | 4 | 2 |
| `DocumentType` | response | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=6, MEDIUM=18, LOW=7  
**Total deduped findings**: 31

## HIGH (6)

### `(no SDK type)` (query)

- **[missing_required_query]** ListAsync (GET /fleet/documents) is missing query parameter `endTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/documents`
  - Recommended fix: Add a required parameter (e.g. `string endTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endTime", ...)`.
- **[missing_required_query]** ListAsync (GET /fleet/documents) is missing query parameter `startTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /fleet/documents`
  - Recommended fix: Add a required parameter (e.g. `string startTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startTime", ...)`.

### `CreateDocumentRequest` (request)

- **[required_drift]** CreateDocumentRequest.driverId: spec marks REQUIRED but SDK property is not `required`.
  - Endpoints: `POST /fleet/documents`
  - Recommended fix: Mark `CreateDocumentRequest.DriverId` as `required` (drop the `?` nullable marker).

### `Document` (response)

- **[response_drift_required]** Document (response) missing REQUIRED property `createdAtTime` (spec type=string/date-time). (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public DateTimeOffset CreatedAtTime { get; init; }` to response record `Document` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Document (response) missing REQUIRED property `documentType` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("documentType")] public object DocumentType { get; init; }` to response record `Document` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Document (response) missing REQUIRED property `driver` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object Driver { get; init; }` to response record `Document` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (18)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /fleet/documents) is missing query parameter `documentTypeId` (spec optional, type=string).
  - Endpoints: `GET /fleet/documents`
  - Recommended fix: Add an optional parameter `string? documentTypeId = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/documents) is missing query parameter `queryBy` (spec optional, type=string).
  - Endpoints: `GET /fleet/documents`
  - Recommended fix: Add an optional parameter `string? queryBy = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateDocumentRequest` (request)

- **[missing_optional]** CreateDocumentRequest is missing property `name` (spec type=string).
  - Endpoints: `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("name")] public string? Name { get; init; }` to `CreateDocumentRequest`.
- **[missing_optional]** CreateDocumentRequest is missing property `routeStopId` (spec type=string).
  - Endpoints: `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("routeStopId")] public string? RouteStopId { get; init; }` to `CreateDocumentRequest`.
- **[missing_optional]** CreateDocumentRequest is missing property `state` (spec type=string enum=['submitted', 'required']).
  - Endpoints: `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("state")] public string? State { get; init; }` to `CreateDocumentRequest`.
- **[missing_optional]** CreateDocumentRequest is missing property `vehicleId` (spec type=string).
  - Endpoints: `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }` to `CreateDocumentRequest`.

### `Document` (response)

- **[response_drift_optional]** Document (response) missing property `conditionalFieldSections` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("conditionalFieldSections")] public IReadOnlyList<object>? ConditionalFieldSections { get; init; }` to response record `Document`.
- **[response_drift_optional]** Document (response) missing property `route` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("route")] public object? Route { get; init; }` to response record `Document`.
- **[response_drift_optional]** Document (response) missing property `routeStop` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("routeStop")] public object? RouteStop { get; init; }` to response record `Document`.
- **[response_drift_optional]** Document (response) missing property `updatedAtTime` (spec type=string/date-time). (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public DateTimeOffset? UpdatedAtTime { get; init; }` to response record `Document`.
- **[response_drift_optional]** Document (response) missing property `vehicle` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Add `[JsonPropertyName("vehicle")] public object? Vehicle { get; init; }` to response record `Document`.
- **[response_required_drift]** Document.fields (response): spec marks REQUIRED but SDK exposes as nullable (`IReadOnlyList<DocumentField>?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Tighten `Document.Fields` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Document.state (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Tighten `Document.State` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `DocumentPdfJob` (response)

- **[response_drift_optional]** DocumentPdfJob (response) missing property `completedAtTime` (spec type=string).
  - Endpoints: `GET /fleet/documents/pdfs/{id}`
  - Recommended fix: Add `[JsonPropertyName("completedAtTime")] public string? CompletedAtTime { get; init; }` to response record `DocumentPdfJob`.
- **[response_drift_optional]** DocumentPdfJob (response) missing property `downloadDocumentPdfUrl` (spec type=string).
  - Endpoints: `GET /fleet/documents/pdfs/{id}`
  - Recommended fix: Add `[JsonPropertyName("downloadDocumentPdfUrl")] public string? DownloadDocumentPdfUrl { get; init; }` to response record `DocumentPdfJob`.
- **[response_drift_optional]** DocumentPdfJob (response) missing property `jobStatus` (spec type=string).
  - Endpoints: `GET /fleet/documents/pdfs/{id}`
  - Recommended fix: Add `[JsonPropertyName("jobStatus")] public string? JobStatus { get; init; }` to response record `DocumentPdfJob`.
- **[response_drift_optional]** DocumentPdfJob (response) missing property `requestedAtTime` (spec type=string).
  - Endpoints: `GET /fleet/documents/pdfs/{id}`
  - Recommended fix: Add `[JsonPropertyName("requestedAtTime")] public string? RequestedAtTime { get; init; }` to response record `DocumentPdfJob`.

### `DocumentType` (response)

- **[response_drift_optional]** DocumentType (response) missing property `conditionalFieldSections` (spec type=array).
  - Endpoints: `GET /fleet/document-types`
  - Recommended fix: Add `[JsonPropertyName("conditionalFieldSections")] public IReadOnlyList<object>? ConditionalFieldSections { get; init; }` to response record `DocumentType`.

## LOW (7)

### `Document` (response)

- **[extra_property]** Document.createdAtMs (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Remove `Document.CreatedAtMs` (not in spec).
- **[extra_property]** Document.documentTypeId (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Remove `Document.DocumentTypeId` (not in spec).
- **[extra_property]** Document.driverId (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Remove `Document.DriverId` (not in spec).
- **[extra_property]** Document.updatedAtMs (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Remove `Document.UpdatedAtMs` (not in spec).
- **[extra_property]** Document.vehicleId (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/documents`, `GET /fleet/documents/{id}`, `POST /fleet/documents`
  - Recommended fix: Remove `Document.VehicleId` (not in spec).

### `DocumentPdfJob` (response)

- **[extra_property]** DocumentPdfJob.pdfUrl (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/documents/pdfs/{id}`, `POST /fleet/documents/pdfs`
  - Recommended fix: Remove `DocumentPdfJob.PdfUrl` (not in spec).
- **[extra_property]** DocumentPdfJob.status (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/documents/pdfs/{id}`, `POST /fleet/documents/pdfs`
  - Recommended fix: Remove `DocumentPdfJob.Status` (not in spec).

