# Contacts — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (2/5)  
> **⚠️ 2026-05-21 audit**: model — `Contact.firstName/lastName/email/phone` are required in spec but modelled optional; 3 ops missing. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `IContactsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../ContactsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Common/CommonModels.cs`  

---

## Endpoints

### ✅ `GET /contacts`
**Operation ID**: `listContacts`  
**Summary**: List all contacts  
**Parameters**: `limit`, `after`  
**Request Body**: No  

- [x] Method defined in `IContactsClient`
- [x] Method implemented in `ContactsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /contacts`
**Operation ID**: `createContact`  
**Summary**: Create a contact  
**Request Body**: Yes  

- [x] Method defined in `IContactsClient`
- [x] Method implemented in `ContactsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /contacts/{id}`
**Operation ID**: `deleteContact`  
**Summary**: Delete a contact  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IContactsClient`
- [x] Method implemented in `ContactsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /contacts/{id}`
**Operation ID**: `getContact`  
**Summary**: Retrieve a contact  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IContactsClient`
- [x] Method implemented in `ContactsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /contacts/{id}`
**Operation ID**: `updateContact`  
**Summary**: Update a contact  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IContactsClient`
- [x] Method implemented in `ContactsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Common/CommonModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
