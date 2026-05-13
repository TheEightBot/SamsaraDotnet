# Attributes — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (5/5 endpoints implemented)  
> **SDK Client**: `IAttributesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../AttributesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Tags/AttributeModels.cs`  

---

## Endpoints

### ✅ `GET /attributes`
**Operation ID**: `getAttributesByEntityType`  
**Summary**: List all attributes by entity type  
**Parameters**: `entityType`, `limit`, `after`  
**Request Body**: No  

- [x] Method defined in `IAttributesClient`
- [x] Method implemented in `AttributesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /attributes`
**Operation ID**: `createAttribute`  
**Summary**: Create an attribute  
**Request Body**: Yes  

- [x] Method defined in `IAttributesClient`
- [x] Method implemented in `AttributesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /attributes/{id}`
**Operation ID**: `deleteAttribute`  
**Summary**: Deleting an attribute  
**Parameters**: `id`, `entityType`  
**Request Body**: No  

- [x] Method defined in `IAttributesClient`
- [x] Method implemented in `AttributesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /attributes/{id}`
**Operation ID**: `getAttribute`  
**Summary**: Retrieve an attribute  
**Parameters**: `id`, `entityType`  
**Request Body**: No  

- [x] Method defined in `IAttributesClient`
- [x] Method implemented in `AttributesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /attributes/{id}`
**Operation ID**: `updateAttribute`  
**Summary**: Update an attribute  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IAttributesClient`
- [x] Method implemented in `AttributesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Tags/AttributeModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
