# Webhooks — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/55-webhooks.md`](model-sync-plan-2026-05-27/55-webhooks.md). All 7 findings (0 CRIT / 1 HIGH / 5 MED / 1 LOW) applied across the webhook endpoints. The `Webhook` response record gained the HIGH `secretKey` as **`required string`** (placed after `version`; the spec marks it required across all four webhook endpoints — verified safe, no `new Webhook(...)` sites and no `Webhook` deserialization fixtures, only facade/DI tests) and tightened three previously-nullable props to **`required`** (`name`, `url`, `version`). `UpdateWebhookRequest` gained the MED nullable `version` (`string?`, placed after `url`), and its non-spec `eventTypes` extra was RETAINED as a nullable back-compat prop. One optional query param `ids` (`string?`) was added to `ListAsync` (`GET /webhooks`) via `QueryBuilder.WithParams`. **Breaking**: consumers may now rely on non-null `Webhook.Name`/`Url`/`Version`/`SecretKey`. The CLI `List All` webhook action passes the cancellation token by name (the 1st positional slot is now `ids`) and drops the now-redundant null-coalescing on `Name`/`Url`. No JsonContext changes (`Webhook`/`UpdateWebhookRequest` already registered; new props scalar `string`/`string?` → no new top-level types).  
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

**Model sync (2026-05-27):** applied the per-domain remediation plan (0 CRIT / 1 HIGH / 5 MED / 1 LOW — 7 total). See [`model-sync-plan-2026-05-27/55-webhooks.md`](model-sync-plan-2026-05-27/55-webhooks.md) for the full breakdown.

- `Webhook`: added HIGH `secretKey` (`required string`); tightened `name`, `url`, `version` from `string?` to `required`.
- `UpdateWebhookRequest`: added `version` (`string?`); retained non-spec `eventTypes` (LOW) as back-compat.
- `ListAsync` (`GET /webhooks`): added optional `ids` query param (`string?`).
- **Breaking**: consumers may now rely on non-null `Webhook.Name`/`Url`/`Version`/`SecretKey`.
