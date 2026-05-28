# Messages — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🔴 Broken (0/2)  
> **⚠️ 2026-05-21 audit**: `fleet/messages`→`/v1/fleet/messages` (`V1getMessages`/`V1createMessages`). See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **Resolved 2026-05-27 (model-sync plan)**: applied [`model-sync-plan-2026-05-27/32-messages.md`](model-sync-plan-2026-05-27/32-messages.md) — 5 HIGH, 5 MEDIUM, 6 LOW findings implemented. `DriverMessage` response rebuilt to match spec `V1MessageResponse`: added required `isRead`, `text`, and a typed `V1MessageSender` (`name`, `type`); tightened `driverId` to non-nullable `long` (type fix), `sentAtMs` to non-nullable `long`; removed SDK-only `id`, `senderType`, `body`, `readAtMs` (not in spec inner schema). `SendDriverMessageRequest` rebuilt to match spec request body: required `driverIds: IReadOnlyList<string>` and `text: string`; removed legacy `driverId`/`body`. `IMessagesClient.ListAsync` gained spec `endMs`/`durationMs` query params.  
> **SDK Client**: `IMessagesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../MessagesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Communication/CommunicationModels.cs`  

---

## Endpoints

### ✅ `GET /v1/fleet/messages`
**Operation ID**: `V1getMessages`  
**Summary**: Get all messages.  
**Parameters**: `endMs`, `durationMs`  
**Request Body**: No  

- [x] Method defined in `IMessagesClient`
- [x] Method implemented in `MessagesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /v1/fleet/messages`
**Operation ID**: `V1createMessages`  
**Summary**: Send a message to a list of driver ids.  
**Request Body**: Yes  

- [x] Method defined in `IMessagesClient`
- [x] Method implemented in `MessagesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Communication/CommunicationModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
