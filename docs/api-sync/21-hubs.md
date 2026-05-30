# Hubs — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (1/7 endpoints implemented)  
> **✅ Resolved 2026-05-27 (model-sync plan, incl. 2 CRITICAL wrapper fixes)**: 87 findings across 8 SDK types addressed — see [model-sync-plan-2026-05-27/21-hubs.md](model-sync-plan-2026-05-27/21-hubs.md). The two CRITICAL wrapper-drift bugs are fixed: `POST /hub/locations` now posts the `{ data: CreateHubLocationInput[] }` envelope (new `CreateHubLocationsRequest`; prior `CreateHubLocationRequest` renamed to `CreateHubLocationInput` with `address`/`customerLocationId`/`hubId`/`isDepot`/`name` all `required`) and `PATCH /hub/location/{id}` now posts `{ data: UpdateHubLocationRequest }` via the new `UpdateHubLocationEnvelopeRequest`. All four list endpoints (`ListCapacitiesAsync`, `ListCustomPropertiesAsync`, `ListLocationsAsync`, `ListSkillsAsync`) now require `hubId` and accept their full optional query surface (`*Ids`, `*Names`, `startTime`, `endTime`); `ListHubsAsync` gains `hubIds`/`startTime`/`endTime`. Spec-REQUIRED response fields on `Hub`, `HubLocation`, `HubCapacity`, `HubCustomProperty`, and `HubSkill` tightened to non-nullable `required` (createdAt/updatedAt, hubId, address/customerLocationId/driverInstructions/isDepot/latitude/longitude/name/plannerNotes/serviceTimeSeconds/serviceWindows/skillsRequired on locations, csvColumns on custom properties, unit/name on capacities). Response-side spec-absent fields (`Hub.latitude/longitude/formattedAddress/geofence/tags/externalIds`, `HubCapacity.capacity/usedCapacity/timeSlot`, `HubCustomProperty.type`, `HubLocation.notes`) retained as nullable back-compat per the workflow precedent.  
> **✅ Fixed 2026-05-29 (`Hubs.ListAsync` regression)**: `ListAsync()` was still wired to `GET /addresses` — a legacy address overlay predating the real Hubs API — and threw `JSON deserialization for type 'Hub' was missing required properties: timeZone, createdAt, updatedAt` once the 2026-05-27 sync tightened `Hub` to the `GET /hubs` schema. `ListAsync()` now lists hubs via `GET /hubs` (delegates to `ListHubsAsync`). The spec exposes no hub get-by-id/create/update/delete endpoint, so the address-overlay methods `GetAsync`/`CreateAsync`/`UpdateAsync`/`DeleteAsync` and the `CreateHubRequest`/`UpdateHubRequest` models were **removed** (they duplicated the `Addresses` client) — breaking; use `client.Addresses` for `/addresses` CRUD.  
> **SDK Client**: `IHubsClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../HubsClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Routes/HubModels.cs`  

---

## Endpoints

### ❌ `GET /hub/capacities`
**Operation ID**: `listHubCapacities`  
**Summary**: List capacities for a specific hub  
**Parameters**: `hubId`, `capacityIds`, `capacityNames`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /hub/customProperties`
**Operation ID**: `listHubCustomProperties`  
**Summary**: List custom properties for a specific hub  
**Parameters**: `hubId`, `customPropertyIds`, `customPropertyNames`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `PATCH /hub/location/{id}`
**Operation ID**: `updateHubLocation`  
**Summary**: Update a location  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /hub/locations`
**Operation ID**: `listHubLocations`  
**Summary**: List locations for a specific hub  
**Parameters**: `hubId`, `locationIds`, `customerLocationIds`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `POST /hub/locations`
**Operation ID**: `createHubLocations`  
**Summary**: Create locations in bulk  
**Request Body**: Yes  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ❌ `GET /hub/skills`
**Operation ID**: `listHubSkills`  
**Summary**: List skills for a specific hub  
**Parameters**: `hubId`, `skillIds`, `skillNames`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ✅ `GET /hubs`
**Operation ID**: `listHubs`  
**Summary**: List all hubs for the organization  
**Parameters**: `hubIds`, `startTime`, `endTime`, `after`, `limit`  
**Request Body**: No  

- [x] Method defined in `IHubsClient`
- [x] Method implemented in `HubsClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Routes/HubModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

- **Hubs are read-only**: `GET /hubs` is the only hub endpoint in the spec — there is
  no get-by-id, create, update, or delete. `Hub` is a response-only model. Address CRUD
  lives on the separate `Addresses` client (`/addresses`).
- **2026-05-29 (breaking)**: the legacy address-overlay methods `GetAsync`/`CreateAsync`/
  `UpdateAsync`/`DeleteAsync` and the `CreateHubRequest`/`UpdateHubRequest` models were
  removed from `IHubsClient` — they targeted `/addresses`, duplicated the `Addresses`
  client, and broke once `Hub` was tightened to the `GET /hubs` schema. `ListAsync()` was
  repointed from `GET /addresses` to `GET /hubs`.
