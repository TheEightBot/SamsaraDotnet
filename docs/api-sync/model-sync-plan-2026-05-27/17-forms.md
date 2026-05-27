# Forms — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/17-forms.md`](../17-forms.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `FormSubmission` | response | 0 | 7 | 13 | 8 |
| `FormTemplate` | response | 0 | 5 | 5 | 2 |
| `FormPdfExport` | response | 0 | 4 | 3 | 3 |
| `(no SDK type)` | query | 0 | 2 | 6 | 0 |
| `CreateFormSubmissionRequest` | request | 0 | 2 | 6 | 4 |
| `UpdateFormSubmissionRequest` | request | 0 | 0 | 7 | 1 |

**Counts**: CRITICAL=0, HIGH=20, MEDIUM=40, LOW=18  
**Total deduped findings**: 78

## HIGH (20)

### `(no SDK type)` (query)

- **[missing_required_query]** CreatePdfExportAsync (POST /form-submissions/pdf-exports) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `POST /form-submissions/pdf-exports`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.
- **[missing_required_query]** GetPdfExportsAsync (GET /form-submissions/pdf-exports) is missing query parameter `pdfId` (spec REQUIRED, type=string).
  - Endpoints: `GET /form-submissions/pdf-exports`
  - Recommended fix: Add a required parameter (e.g. `string pdfId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("pdfId", ...)`.

### `CreateFormSubmissionRequest` (request)

- **[missing_required]** CreateFormSubmissionRequest is missing REQUIRED property `formTemplate` (spec type=object).
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("formTemplate")] public required object FormTemplate { get; init; }` to `CreateFormSubmissionRequest`.
- **[missing_required]** CreateFormSubmissionRequest is missing REQUIRED property `status` (spec type=string enum=['notStarted']).
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("status")] public required string Status { get; init; }` to `CreateFormSubmissionRequest`.

### `FormPdfExport` (response)

- **[response_drift_required]** FormPdfExport (response) missing REQUIRED property `expiresAtTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Add `[JsonPropertyName("expiresAtTime")] public DateTimeOffset ExpiresAtTime { get; init; }` to response record `FormPdfExport` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormPdfExport (response) missing REQUIRED property `jobStatus` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Add `[JsonPropertyName("jobStatus")] public string JobStatus { get; init; }` to response record `FormPdfExport` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormPdfExport (response) missing REQUIRED property `pdfId` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Add `[JsonPropertyName("pdfId")] public string PdfId { get; init; }` to response record `FormPdfExport` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormPdfExport (response) missing REQUIRED property `requestedAtTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Add `[JsonPropertyName("requestedAtTime")] public DateTimeOffset RequestedAtTime { get; init; }` to response record `FormPdfExport` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `FormSubmission` (response)

- **[response_drift_required]** FormSubmission (response) missing REQUIRED property `createdAtTime` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public DateTimeOffset CreatedAtTime { get; init; }` to response record `FormSubmission` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormSubmission (response) missing REQUIRED property `fields` (spec type=array). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("fields")] public IReadOnlyList<object> Fields { get; init; }` to response record `FormSubmission` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormSubmission (response) missing REQUIRED property `formTemplate` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("formTemplate")] public object FormTemplate { get; init; }` to response record `FormSubmission` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormSubmission (response) missing REQUIRED property `isRequired` (spec type=boolean). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("isRequired")] public bool IsRequired { get; init; }` to response record `FormSubmission` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormSubmission (response) missing REQUIRED property `status` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("status")] public string Status { get; init; }` to response record `FormSubmission` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormSubmission (response) missing REQUIRED property `submittedBy` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("submittedBy")] public object SubmittedBy { get; init; }` to response record `FormSubmission` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormSubmission (response) missing REQUIRED property `updatedAtTime` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public DateTimeOffset UpdatedAtTime { get; init; }` to response record `FormSubmission` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `FormTemplate` (response)

- **[response_drift_required]** FormTemplate (response) missing REQUIRED property `createdBy` (spec type=object).
  - Endpoints: `GET /form-templates`
  - Recommended fix: Add `[JsonPropertyName("createdBy")] public object CreatedBy { get; init; }` to response record `FormTemplate` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormTemplate (response) missing REQUIRED property `fields` (spec type=array).
  - Endpoints: `GET /form-templates`
  - Recommended fix: Add `[JsonPropertyName("fields")] public IReadOnlyList<object> Fields { get; init; }` to response record `FormTemplate` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormTemplate (response) missing REQUIRED property `revisionId` (spec type=string/uuid).
  - Endpoints: `GET /form-templates`
  - Recommended fix: Add `[JsonPropertyName("revisionId")] public string RevisionId { get; init; }` to response record `FormTemplate` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormTemplate (response) missing REQUIRED property `title` (spec type=string).
  - Endpoints: `GET /form-templates`
  - Recommended fix: Add `[JsonPropertyName("title")] public string Title { get; init; }` to response record `FormTemplate` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FormTemplate (response) missing REQUIRED property `updatedBy` (spec type=object).
  - Endpoints: `GET /form-templates`
  - Recommended fix: Add `[JsonPropertyName("updatedBy")] public object UpdatedBy { get; init; }` to response record `FormTemplate` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (40)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetSubmissionsStreamAsync (GET /form-submissions/stream) is missing query parameter `assignedToRouteStopIds` (spec optional, type=array).
  - Endpoints: `GET /form-submissions/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assignedToRouteStopIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSubmissionsStreamAsync (GET /form-submissions/stream) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /form-submissions/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSubmissionsStreamAsync (GET /form-submissions/stream) is missing query parameter `formTemplateIds` (spec optional, type=array).
  - Endpoints: `GET /form-submissions/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? formTemplateIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListTemplatesAsync (GET /form-templates) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /form-templates`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSubmissionsStreamAsync (GET /form-submissions/stream) is missing query parameter `include` (spec optional, type=array).
  - Endpoints: `GET /form-submissions/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? include = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSubmissionsStreamAsync (GET /form-submissions/stream) is missing query parameter `userIds` (spec optional, type=array).
  - Endpoints: `GET /form-submissions/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? userIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateFormSubmissionRequest` (request)

- **[missing_optional]** CreateFormSubmissionRequest is missing property `assignedTo` (spec type=object).
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("assignedTo")] public object? AssignedTo { get; init; }` to `CreateFormSubmissionRequest`.
- **[missing_optional]** CreateFormSubmissionRequest is missing property `dueAtTime` (spec type=string/date-time).
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }` to `CreateFormSubmissionRequest`.
- **[missing_optional]** CreateFormSubmissionRequest is missing property `fields` (spec type=array).
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("fields")] public IReadOnlyList<object>? Fields { get; init; }` to `CreateFormSubmissionRequest`.
- **[missing_optional]** CreateFormSubmissionRequest is missing property `isRequired` (spec type=boolean).
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("isRequired")] public bool? IsRequired { get; init; }` to `CreateFormSubmissionRequest`.
- **[missing_optional]** CreateFormSubmissionRequest is missing property `routeStopId` (spec type=string).
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("routeStopId")] public string? RouteStopId { get; init; }` to `CreateFormSubmissionRequest`.
- **[missing_optional]** CreateFormSubmissionRequest is missing property `title` (spec type=string).
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("title")] public string? Title { get; init; }` to `CreateFormSubmissionRequest`.

### `FormPdfExport` (response)

- **[response_drift_optional]** FormPdfExport (response) missing property `completedAtTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Add `[JsonPropertyName("completedAtTime")] public DateTimeOffset? CompletedAtTime { get; init; }` to response record `FormPdfExport`.
- **[response_drift_optional]** FormPdfExport (response) missing property `errorMessage` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Add `[JsonPropertyName("errorMessage")] public string? ErrorMessage { get; init; }` to response record `FormPdfExport`.
- **[response_drift_optional]** FormPdfExport (response) missing property `pdfUrlExpiresAtTime` (spec type=string/date-time). (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Add `[JsonPropertyName("pdfUrlExpiresAtTime")] public DateTimeOffset? PdfUrlExpiresAtTime { get; init; }` to response record `FormPdfExport`.

### `FormSubmission` (response)

- **[response_drift_optional]** FormSubmission (response) missing property `approvalDetails` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("approvalDetails")] public object? ApprovalDetails { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `asset` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("asset")] public object? Asset { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `assignedAtTime` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("assignedAtTime")] public DateTimeOffset? AssignedAtTime { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `assignedTo` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("assignedTo")] public object? AssignedTo { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `dueAtTime` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `externalIds` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("externalIds")] public object? ExternalIds { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `geofence` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("geofence")] public object? Geofence { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `location` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("location")] public object? Location { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `routeId` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("routeId")] public string? RouteId { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `routeStopId` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("routeStopId")] public string? RouteStopId { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `score` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("score")] public object? Score { get; init; }` to response record `FormSubmission`.
- **[response_drift_optional]** FormSubmission (response) missing property `title` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("title")] public string? Title { get; init; }` to response record `FormSubmission`.
- **[response_required_drift]** FormSubmission.submittedAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Tighten `FormSubmission.SubmittedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `FormTemplate` (response)

- **[response_drift_optional]** FormTemplate (response) missing property `approvalConfig` (spec type=object).
  - Endpoints: `GET /form-templates`
  - Recommended fix: Add `[JsonPropertyName("approvalConfig")] public object? ApprovalConfig { get; init; }` to response record `FormTemplate`.
- **[response_drift_optional]** FormTemplate (response) missing property `formCategory` (spec type=string).
  - Endpoints: `GET /form-templates`
  - Recommended fix: Add `[JsonPropertyName("formCategory")] public string? FormCategory { get; init; }` to response record `FormTemplate`.
- **[response_required_drift]** FormTemplate.createdAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /form-templates`
  - Recommended fix: Tighten `FormTemplate.CreatedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** FormTemplate.sections (response): spec marks REQUIRED but SDK exposes as nullable (`IReadOnlyList<FormSection>?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /form-templates`
  - Recommended fix: Tighten `FormTemplate.Sections` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** FormTemplate.updatedAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /form-templates`
  - Recommended fix: Tighten `FormTemplate.UpdatedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateFormSubmissionRequest` (request)

- **[missing_optional]** UpdateFormSubmissionRequest is missing property `approvalDetails` (spec type=object).
  - Endpoints: `PATCH /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("approvalDetails")] public object? ApprovalDetails { get; init; }` to `UpdateFormSubmissionRequest`.
- **[missing_optional]** UpdateFormSubmissionRequest is missing property `assignedTo` (spec type=object).
  - Endpoints: `PATCH /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("assignedTo")] public object? AssignedTo { get; init; }` to `UpdateFormSubmissionRequest`.
- **[missing_optional]** UpdateFormSubmissionRequest is missing property `dueAtTime` (spec type=string/date-time).
  - Endpoints: `PATCH /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }` to `UpdateFormSubmissionRequest`.
- **[missing_optional]** UpdateFormSubmissionRequest is missing property `isRequired` (spec type=boolean).
  - Endpoints: `PATCH /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("isRequired")] public bool? IsRequired { get; init; }` to `UpdateFormSubmissionRequest`.
- **[missing_optional]** UpdateFormSubmissionRequest is missing property `routeStopId` (spec type=string).
  - Endpoints: `PATCH /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("routeStopId")] public string? RouteStopId { get; init; }` to `UpdateFormSubmissionRequest`.
- **[missing_optional]** UpdateFormSubmissionRequest is missing property `status` (spec type=string enum=['notStarted', 'archived', 'inProgress', 'changesRequested', 'approved']).
  - Endpoints: `PATCH /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("status")] public string? Status { get; init; }` to `UpdateFormSubmissionRequest`.
- **[missing_optional]** UpdateFormSubmissionRequest is missing property `title` (spec type=string).
  - Endpoints: `PATCH /form-submissions`
  - Recommended fix: Add `[JsonPropertyName("title")] public string? Title { get; init; }` to `UpdateFormSubmissionRequest`.

## LOW (18)

### `CreateFormSubmissionRequest` (request)

- **[extra_property]** CreateFormSubmissionRequest.driver: present in SDK but not in spec inner schema.
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Remove `CreateFormSubmissionRequest.Driver` (not in spec).
- **[extra_property]** CreateFormSubmissionRequest.fieldValues: present in SDK but not in spec inner schema.
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Remove `CreateFormSubmissionRequest.FieldValues` (not in spec).
- **[extra_property]** CreateFormSubmissionRequest.formTemplateId: present in SDK but not in spec inner schema.
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Remove `CreateFormSubmissionRequest.FormTemplateId` (not in spec).
- **[extra_property]** CreateFormSubmissionRequest.vehicle: present in SDK but not in spec inner schema.
  - Endpoints: `POST /form-submissions`
  - Recommended fix: Remove `CreateFormSubmissionRequest.Vehicle` (not in spec).

### `FormPdfExport` (response)

- **[extra_property]** FormPdfExport.createdAt (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Remove `FormPdfExport.CreatedAt` (not in spec).
- **[extra_property]** FormPdfExport.formSubmissionId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Remove `FormPdfExport.FormSubmissionId` (not in spec).
- **[extra_property]** FormPdfExport.status (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /form-submissions/pdf-exports`, `POST /form-submissions/pdf-exports`
  - Recommended fix: Remove `FormPdfExport.Status` (not in spec).

### `FormSubmission` (response)

- **[extra_property]** FormSubmission.driverId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Remove `FormSubmission.DriverId` (not in spec).
- **[extra_property]** FormSubmission.driverName (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Remove `FormSubmission.DriverName` (not in spec).
- **[extra_property]** FormSubmission.fieldValues (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Remove `FormSubmission.FieldValues` (not in spec).
- **[extra_property]** FormSubmission.formTemplateId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Remove `FormSubmission.FormTemplateId` (not in spec).
- **[extra_property]** FormSubmission.formTemplateName (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Remove `FormSubmission.FormTemplateName` (not in spec).
- **[extra_property]** FormSubmission.state (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Remove `FormSubmission.State` (not in spec).
- **[extra_property]** FormSubmission.vehicleId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Remove `FormSubmission.VehicleId` (not in spec).
- **[extra_property]** FormSubmission.vehicleName (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /form-submissions`, `GET /form-submissions/stream`, `PATCH /form-submissions`, `POST /form-submissions`
  - Recommended fix: Remove `FormSubmission.VehicleName` (not in spec).

### `FormTemplate` (response)

- **[extra_property]** FormTemplate.name (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /form-templates`
  - Recommended fix: Remove `FormTemplate.Name` (not in spec).
- **[extra_property]** FormTemplate.revision (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /form-templates`
  - Recommended fix: Remove `FormTemplate.Revision` (not in spec).

### `UpdateFormSubmissionRequest` (request)

- **[extra_property]** UpdateFormSubmissionRequest.fieldValues: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /form-submissions`
  - Recommended fix: Remove `UpdateFormSubmissionRequest.FieldValues` (not in spec).

