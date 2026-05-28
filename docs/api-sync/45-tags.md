# Tags — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/45-tags.md`](model-sync-plan-2026-05-27/45-tags.md). Aligned the `Tag` response and the two write requests to spec across `GET/POST /tags` (+4 more): `Tag` gained the flat `parentTagId` `string?` (coexists with the existing `parentTag` object) and `UpdateTagRequest` gained `externalIds` (typed `IReadOnlyDictionary<string, string>?` to match its siblings). `CreateTagRequest.name` had `required` dropped (spec marks it optional), and `CreateTagRequest.externalIds` is kept as a nullable back-compat extra. All changes are in `TagModels.cs`; no client/interface/JsonContext/CLI/test changes.  
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

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

**Model audit (2025-05-13):** Tag request models were missing asset, machine, and sensor membership fields.

- `CreateTagRequest`: added `assets`, `machines`, `sensors`, `externalIds`.
- `UpdateTagRequest`: added `assets`, `machines`, `sensors`.
