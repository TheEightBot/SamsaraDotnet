# Sensors — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Resolved 2026-05-27 (model-sync plan)  
> **⚠️ 2026-05-21 audit**: client targets `/sensors*`; spec sensors are v1 POST endpoints (`/v1/sensors/list|cargo|door|humidity|temperature|history`). Needs rework. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/41-sensors.md`](model-sync-plan-2026-05-27/41-sensors.md). Added the spec-REQUIRED `stepMs` (`int`) property to the `V1SensorHistoryRequest` request body for `POST /v1/sensors/history`.  
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
