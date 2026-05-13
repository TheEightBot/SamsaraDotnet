# Location and Speed — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/1 endpoints implemented)  
> **SDK Client**: `IAssetsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../AssetsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Assets/AssetModels.cs`  

---

## Endpoints

### ⚠️ `GET /assets/location-and-speed/stream`
**Operation ID**: `getLocationAndSpeed`  
**Summary**: List asset location and speed data in an organization.  
**Parameters**: `after`, `limit`, `startTime`, `endTime`, `ids`, `includeSpeed`, `includeReverseGeo`, `includeGeofenceLookup`, `includeHighFrequencyLocations`, `includeExternalIds`  
**Request Body**: No  

- [x] Method defined in `IAssetsClient`
- [x] Method implemented in `AssetsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Assets/AssetModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
