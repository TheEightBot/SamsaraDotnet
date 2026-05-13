# Idling — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/1 endpoints implemented)  
> **SDK Client**: `IIdlingClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../IdlingClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/IdlingModels.cs`  

---

## Endpoints

### ⚠️ `GET /idling/events`
**Operation ID**: `getIdlingEvents`  
**Summary**: Get idling events.  
**Parameters**: `startTime`, `endTime`, `assetIds`, `operatorIds`, `ptoState`, `minAirTemperatureMillicelsius`, `maxAirTemperatureMillicelsius`, `excludeEventsWithUnknownAirTemperature`, `minDurationMilliseconds`, `maxDurationMilliseconds`, `tagIds`, `parentTagIds`, `includeExternalIds`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IIdlingClient`
- [x] Method implemented in `IdlingClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Fleet/IdlingModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
