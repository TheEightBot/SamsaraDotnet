# Webhooks — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (5/5 endpoints implemented)  
> **SDK Client**: `IWebhooksClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../WebhooksClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Webhooks/WebhookModels.cs`  

---

## Endpoints

### ✅ `GET /webhooks`
**Operation ID**: `listWebhooks`  
**Summary**: List all webhooks belonging to a specific org.  
**Parameters**: `ids`, `limit`, `after`  
**Request Body**: No  

- [x] Method defined in `IWebhooksClient`
- [x] Method implemented in `WebhooksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /webhooks`
**Operation ID**: `postWebhooks`  
**Summary**: Create a webhook  
**Request Body**: Yes  

- [x] Method defined in `IWebhooksClient`
- [x] Method implemented in `WebhooksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /webhooks/{id}`
**Operation ID**: `deleteWebhook`  
**Summary**: Delete a webhook with the given ID  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IWebhooksClient`
- [x] Method implemented in `WebhooksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /webhooks/{id}`
**Operation ID**: `getWebhook`  
**Summary**: Retrieve a webhook with given ID  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IWebhooksClient`
- [x] Method implemented in `WebhooksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /webhooks/{id}`
**Operation ID**: `patchWebhook`  
**Summary**: Update a specific webhook's information.  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IWebhooksClient`
- [x] Method implemented in `WebhooksClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Webhooks/WebhookModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
