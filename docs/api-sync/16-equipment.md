# Equipment — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (4/8 endpoints implemented)  
> **SDK Client**: `IEquipmentClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../EquipmentClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`  

---

## Endpoints

### ✅ `GET /fleet/equipment`
**Operation ID**: `listEquipment`  
**Summary**: List all equipment  
**Parameters**: `limit`, `after`, `parentTagIds`, `tagIds`  
**Request Body**: No  

- [x] Method defined in `IEquipmentClient`
- [x] Method implemented in `EquipmentClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /fleet/equipment/locations`
**Operation ID**: `getEquipmentLocations`  
**Summary**: Get most recent locations for all equipment  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `equipmentIds`  
**Request Body**: No  

- [x] Method defined in `IEquipmentClient`
- [x] Method implemented in `EquipmentClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /fleet/equipment/locations/feed`
**Operation ID**: `getEquipmentLocationsFeed`  
**Summary**: Follow feed of equipment locations  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `equipmentIds`  
**Request Body**: No  

- [x] Method defined in `IEquipmentClient`
- [x] Method implemented in `EquipmentClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /fleet/equipment/locations/history`
**Operation ID**: `getEquipmentLocationsHistory`  
**Summary**: Get historical equipment locations  
**Parameters**: `after`, `startTime`, `endTime`, `parentTagIds`, `tagIds`, `equipmentIds`  
**Request Body**: No  

- [x] Method defined in `IEquipmentClient`
- [x] Method implemented in `EquipmentClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /fleet/equipment/stats`
**Operation ID**: `getEquipmentStats`  
**Summary**: Get most recent stats for all equipment  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `equipmentIds`, `types`  
**Request Body**: No  

- [x] Method defined in `IEquipmentClient`
- [x] Method implemented in `EquipmentClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /fleet/equipment/stats/feed`
**Operation ID**: `getEquipmentStatsFeed`  
**Summary**: Follow a feed of equipment stats  
**Parameters**: `after`, `parentTagIds`, `tagIds`, `equipmentIds`, `types`  
**Request Body**: No  

- [x] Method defined in `IEquipmentClient`
- [x] Method implemented in `EquipmentClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /fleet/equipment/stats/history`
**Operation ID**: `getEquipmentStatsHistory`  
**Summary**: Get historical equipment stats  
**Parameters**: `after`, `startTime`, `endTime`, `parentTagIds`, `tagIds`, `equipmentIds`, `types`  
**Request Body**: No  

- [x] Method defined in `IEquipmentClient`
- [x] Method implemented in `EquipmentClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /fleet/equipment/{id}`
**Operation ID**: `getEquipment`  
**Summary**: Retrieve a unit of equipment  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IEquipmentClient`
- [x] Method implemented in `EquipmentClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/FleetModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
