# Messages — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (2/2 endpoints implemented)  
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
