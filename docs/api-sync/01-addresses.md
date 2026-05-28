# Addresses — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (5/5 endpoints match the spec) — Resolved 2026-05-27 (model-sync plan)  
> **2026-05-27 model-sync**: `Address.formattedAddress` and `Address.geofence` tightened to non-nullable (spec marks both REQUIRED on the response); `ListAsync` now exposes `parentTagIds`/`tagIds`/`createdAfterTime` query parameters. See [model-sync-plan-2026-05-27/01-addresses.md](model-sync-plan-2026-05-27/01-addresses.md).  
> **2026-05-21 audit (resolved)**: model — `CreateAddressRequest.formattedAddress`/`geofence` should be required; `Address` is missing `contacts`/`createdAtTime` and has an extra `contactIds`. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
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
