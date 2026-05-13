# Documents — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (3/7 endpoints implemented)  
> **SDK Client**: `IDocumentsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../DocumentsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Documents/DocumentModels.cs`  

---

## Endpoints

### ❌ `GET /fleet/document-types`
**Operation ID**: `getDocumentTypes`  
**Summary**: Fetch document types  
**Parameters**: `after`  
**Request Body**: No  

- [ ] Method defined in `IDocumentsClient`
- [ ] Method implemented in `DocumentsClient.cs`
- [ ] Request model(s) defined (if applicable)
- [ ] Response model(s) defined
- [ ] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/documents`
**Operation ID**: `getDocuments`  
**Summary**: Fetch all documents  
**Parameters**: `startTime`, `endTime`, `after`, `documentTypeId`, `queryBy`  
**Request Body**: No  

- [x] Method defined in `IDocumentsClient`
- [x] Method implemented in `DocumentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /fleet/documents`
**Operation ID**: `postDocument`  
**Summary**: Create document  
**Request Body**: Yes  

- [x] Method defined in `IDocumentsClient`
- [x] Method implemented in `DocumentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `POST /fleet/documents/pdfs`
**Operation ID**: `generateDocumentPdf`  
**Summary**: Create a document PDF  
**Request Body**: Yes  

- [x] Method defined in `IDocumentsClient`
- [x] Method implemented in `DocumentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /fleet/documents/pdfs/{id}`
**Operation ID**: `getDocumentPdf`  
**Summary**: Query a document PDF  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IDocumentsClient`
- [x] Method implemented in `DocumentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `DELETE /fleet/documents/{id}`
**Operation ID**: `deleteDocument`  
**Summary**: Delete document  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IDocumentsClient`
- [x] Method implemented in `DocumentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /fleet/documents/{id}`
**Operation ID**: `getDocument`  
**Summary**: Fetch document  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IDocumentsClient`
- [x] Method implemented in `DocumentsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Documents/DocumentModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
