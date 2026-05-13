# Organization Info — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (1/1 endpoints implemented)  
> **SDK Client**: `IOrganizationClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../OrganizationClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Organization/OrganizationModels.cs`  

---

## Endpoints

### ✅ `GET /me`
**Operation ID**: `getOrganizationInfo`  
**Summary**: Get information about your organization  
**Request Body**: No  

- [x] Method defined in `IOrganizationClient`
- [x] Method implemented in `OrganizationClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Organization/OrganizationModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
