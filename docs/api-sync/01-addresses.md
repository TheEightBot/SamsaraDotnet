# Addresses — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (5/5 endpoints implemented)  
> **SDK Client**: `IAddressesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../AddressesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Addresses/AddressModels.cs`  

---

## Endpoints

### ✅ `GET /addresses`
**Operation ID**: `listAddresses`  
**Summary**: List all addresses  
**Parameters**: `limit`, `after`, `parentTagIds`, `tagIds`, `createdAfterTime`  
**Request Body**: No  

- [x] Method defined in `IAddressesClient`
- [x] Method implemented in `AddressesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /addresses`
**Operation ID**: `createAddress`  
**Summary**: Create an address  
**Request Body**: Yes  

- [x] Method defined in `IAddressesClient`
- [x] Method implemented in `AddressesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /addresses/{id}`
**Operation ID**: `deleteAddress`  
**Summary**: Delete an address  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IAddressesClient`
- [x] Method implemented in `AddressesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /addresses/{id}`
**Operation ID**: `getAddress`  
**Summary**: Retrieve an address  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IAddressesClient`
- [x] Method implemented in `AddressesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /addresses/{id}`
**Operation ID**: `updateAddress`  
**Summary**: Update an address  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IAddressesClient`
- [x] Method implemented in `AddressesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Addresses/AddressModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
