# Sensors — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Complete (6/6 endpoints implemented)  
> **SDK Client**: `ISensorsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../SensorsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Industrial/SensorModels.cs`  

---

## Endpoints

### ✅ `POST /v1/sensors/cargo`
**Operation ID**: `V1getSensorsCargo`  
**Summary**: Get cargo status  
**Request Body**: Yes  

- [x] Method defined in `ISensorsClient`
- [x] Method implemented in `SensorsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /v1/sensors/door`
**Operation ID**: `V1getSensorsDoor`  
**Summary**: Get door status  
**Request Body**: Yes  

- [x] Method defined in `ISensorsClient`
- [x] Method implemented in `SensorsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /v1/sensors/history`
**Operation ID**: `V1getSensorsHistory`  
**Summary**: Get sensor history  
**Request Body**: Yes  

- [x] Method defined in `ISensorsClient`
- [x] Method implemented in `SensorsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /v1/sensors/humidity`
**Operation ID**: `V1getSensorsHumidity`  
**Summary**: Get humidity  
**Request Body**: Yes  

- [x] Method defined in `ISensorsClient`
- [x] Method implemented in `SensorsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /v1/sensors/list`
**Operation ID**: `V1getSensors`  
**Summary**: Get all sensors  
**Request Body**: No  

- [x] Method defined in `ISensorsClient`
- [x] Method implemented in `SensorsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /v1/sensors/temperature`
**Operation ID**: `V1getSensorsTemperature`  
**Summary**: Get temperature  
**Request Body**: Yes  

- [x] Method defined in `ISensorsClient`
- [x] Method implemented in `SensorsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Industrial/SensorModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
