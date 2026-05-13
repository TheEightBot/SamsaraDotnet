# Trips — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (1/2 endpoints implemented)  
> **SDK Client**: `ITripsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../TripsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Routes/TripModels.cs`  

---

## Endpoints

### ❌ `GET /trips/stream`
**Operation ID**: `getTrips`  
**Summary**: Get Trips Stream  
**Parameters**: `includeAsset`, `completionStatus`, `startTime`, `endTime`, `queryBy`, `after`, `ids`  
**Request Body**: No  

- [x] Method defined in `ITripsClient`
- [x] Method implemented in `TripsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /v1/fleet/trips`
**Operation ID**: `V1getFleetTrips`  
**Summary**: Get vehicle trips  
**Parameters**: `vehicleId`, `startMs`, `endMs`  
**Request Body**: No  

- [x] Method defined in `ITripsClient`
- [x] Method implemented in `TripsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Routes/TripModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
