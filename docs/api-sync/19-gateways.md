# Gateways — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🔴 Broken (0/3)  
> **⚠️ 2026-05-21 audit**: `fleet/gateways`→`/gateways`; `GetAsync` by-id has no spec op; missing `POST /gateways`, `DELETE /gateways/{id}`. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/19-gateways.md`](model-sync-plan-2026-05-27/19-gateways.md). `Gateway` (response) now exposes spec-required `model` and `serial` as non-nullable, plus typed `accessoryDevices`, `connectionStatus`, and `dataUsageLast30Days` nested records. `ListAsync` adds the optional `models` query filter. LOW extras (`id`, `name`, `mainBus`, `firmwareVersion`, `wifiMacAddress`, `simCardId`, `vehicle`, `tags`) are retained as nullable back-compat per the established workflow.  
> **SDK Client**: `IGatewaysClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../GatewaysClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Fleet/GatewayModels.cs`  

---

## Endpoints

### ✅ `GET /gateways`
**Operation ID**: `getGateways`  
**Summary**: List all gateways  
**Parameters**: `models`, `after`  
**Request Body**: No  

- [x] Method defined in `IGatewaysClient`
- [x] Method implemented in `GatewaysClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /gateways`
**Operation ID**: `postGateway`  
**Summary**: Activate a new gateway  
**Request Body**: Yes  

- [x] Method defined in `IGatewaysClient`
- [x] Method implemented in `GatewaysClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /gateways/{id}`
**Operation ID**: `deleteGateway`  
**Summary**: Deactivate a gateway  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IGatewaysClient`
- [x] Method implemented in `GatewaysClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /gateways/pair`
**Operation ID**: `pairGateways`  
**Summary**: [beta] Pair gateways to devices (relocated from `POST /preview/gateways/pair` on the
2026-06-22 sync)  
**Request Body**: Yes  

- [x] Method defined in `IGatewaysClient` (`PairGatewaysAsync`)
- [x] Method implemented in `GatewaysClient.cs`
- [x] Request model(s) defined (if applicable) — loosely typed `object` (beta)
- [x] Response model(s) defined — loosely typed `object` (beta)
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`) — n/a for `object`
- [x] Unit/integration test coverage (`GatewaysClientTests.PairGatewaysAsync_PostsToGatewaysPair`)

---

## Models

See `src/Samsara.Sdk/Models/Fleet/GatewayModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
