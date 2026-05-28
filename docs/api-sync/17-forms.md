# Forms — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (7/7 endpoints match the spec)  
> **✅ Resolved 2026-05-27 (model-sync plan)** — Implemented HIGH (20) and MEDIUM (40) findings from `docs/api-sync/model-sync-plan-2026-05-27/17-forms.md`: aligned `FormSubmission`, `FormTemplate`, `FormPdfExport`, `CreateFormSubmissionRequest`, and `UpdateFormSubmissionRequest` with the spec's required/optional shape (nested objects modeled as `object`); added `pdfId` (required) to `GetPdfExportsAsync` and `id` (required, query) to `CreatePdfExportAsync` (and removed the unused `CreateFormPdfExportRequest` body type since the spec endpoint has no body); added optional `ids` to `ListTemplatesAsync` and optional `formTemplateIds`/`userIds`/`driverIds`/`assignedToRouteStopIds`/`include` to `GetSubmissionsStreamAsync`. Per the task guidance, the legacy `FormTemplate.Name` was kept alongside the new spec-correct `Title` (rename — both deserialize via `JsonPropertyName`). LOW back-compat fields retained per workflow precedent.  
> **⚠️ 2026-05-21 audit**: `fleet/forms/templates`→`/form-templates`; `fleet/forms/submissions[/{id}]`→`/form-submissions`. Missing `POST`/`PATCH /form-submissions`. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `IFormsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../FormsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Documents/FormModels.cs`  

---

## Endpoints

### ✅ `GET /form-submissions`
**Operation ID**: `getFormSubmissions`  
**Summary**: Get a list of specified form submissions.  
**Parameters**: `ids`, `include`  
**Request Body**: No  

- [x] Method defined in `IFormsClient`
- [x] Method implemented in `FormsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /form-submissions`
**Operation ID**: `patchFormSubmission`  
**Summary**: Update a single form submission.  
**Request Body**: Yes  

- [x] Method defined in `IFormsClient`
- [x] Method implemented in `FormsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /form-submissions`
**Operation ID**: `postFormSubmission`  
**Summary**: Create a form submission.  
**Request Body**: Yes  

- [x] Method defined in `IFormsClient`
- [x] Method implemented in `FormsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /form-submissions/pdf-exports`
**Operation ID**: `getFormSubmissionsPdfExports`  
**Summary**: Return a PDF export that has already been generated for a form submission.  
**Parameters**: `pdfId`  
**Request Body**: No  

- [x] Method defined in `IFormsClient`
- [x] Method implemented in `FormsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `POST /form-submissions/pdf-exports`
**Operation ID**: `postFormSubmissionsPdfExports`  
**Summary**: Create a PDF export for an existing form submission.  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IFormsClient`
- [x] Method implemented in `FormsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /form-submissions/stream`
**Operation ID**: `getFormSubmissionsStream`  
**Summary**: Get a stream of filtered form submissions.  
**Parameters**: `startTime`, `endTime`, `after`, `formTemplateIds`, `userIds`, `driverIds`, `include`, `assignedToRouteStopIds`  
**Request Body**: No  

- [x] Method defined in `IFormsClient`
- [x] Method implemented in `FormsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /form-templates`
**Operation ID**: `getFormTemplates`  
**Summary**: Get a list of form templates.  
**Parameters**: `ids`, `after`  
**Request Body**: No  

- [x] Method defined in `IFormsClient`
- [x] Method implemented in `FormsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Documents/FormModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
