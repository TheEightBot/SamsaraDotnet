# Tags — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (5/6 endpoints implemented)  
> **SDK Client**: `ITagsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../TagsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Tags/TagModels.cs`  

---

## Endpoints

### ✅ `GET /tags`
**Operation ID**: `listTags`  
**Summary**: List all tags  
**Parameters**: `limit`, `after`  
**Request Body**: No  

- [x] Method defined in `ITagsClient`
- [x] Method implemented in `TagsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /tags`
**Operation ID**: `createTag`  
**Summary**: Create a tag  
**Request Body**: Yes  

- [x] Method defined in `ITagsClient`
- [x] Method implemented in `TagsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /tags/{id}`
**Operation ID**: `deleteTag`  
**Summary**: Delete a tag  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `ITagsClient`
- [x] Method implemented in `TagsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /tags/{id}`
**Operation ID**: `getTag`  
**Summary**: Retrieve a tag  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `ITagsClient`
- [x] Method implemented in `TagsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /tags/{id}`
**Operation ID**: `patchTag`  
**Summary**: Update a tag  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `ITagsClient`
- [x] Method implemented in `TagsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `PUT /tags/{id}`
**Operation ID**: `replaceTag`  
**Summary**: Update a tag  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `ITagsClient`
- [x] Method implemented in `TagsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Tags/TagModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
