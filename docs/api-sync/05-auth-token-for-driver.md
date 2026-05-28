# Auth Token for Driver — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: Resolved 2026-05-27 (model-sync plan) — all spec drift addressed; see [`model-sync-plan-2026-05-27/05-auth-token-for-driver.md`](model-sync-plan-2026-05-27/05-auth-token-for-driver.md).  
> **SDK Client**: `IDriversClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../DriversClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Drivers/DriverModels.cs`  

---

## Endpoints

### ⚠️ `POST /fleet/drivers/auth-token`
**Operation ID**: `authToken`  
**Summary**: Create auth token for a driver  
**Request Body**: Yes  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Drivers/DriverModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
