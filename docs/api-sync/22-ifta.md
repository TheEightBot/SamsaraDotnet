# IFTA — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (2/4 endpoints implemented)  
> **SDK Client**: `IIftaClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../IftaClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Compliance/IftaModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/reports/ifta/jurisdiction`
**Operation ID**: `getIftaJurisdictionReports`  
**Summary**: Get IFTA jurisdiction reports.  
**Parameters**: `year`, `month`, `quarter`, `jurisdictions`, `fuelType`, `vehicleIds`, `tagIds`, `parentTagIds`  
**Request Body**: No  

- [x] Method defined in `IIftaClient`
- [x] Method implemented in `IftaClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/reports/ifta/vehicle`
**Operation ID**: `getIftaVehicleReports`  
**Summary**: Get IFTA vehicle reports.  
**Parameters**: `year`, `month`, `quarter`, `jurisdictions`, `fuelType`, `vehicleIds`, `tagIds`, `parentTagIds`, `after`  
**Request Body**: No  

- [x] Method defined in `IIftaClient`
- [x] Method implemented in `IftaClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `POST /ifta-detail/csv`
**Operation ID**: `createIftaDetailJob`  
**Summary**: Create a job to generate csv files of IFTA mileage segments.  
**Request Body**: Yes  

- [x] Method defined in `IIftaClient`
- [x] Method implemented in `IftaClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /ifta-detail/csv/{id}`
**Operation ID**: `getIftaDetailJob`  
**Summary**: Get information about an existing IFTA detail job.  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IIftaClient`
- [x] Method implemented in `IftaClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Compliance/IftaModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
