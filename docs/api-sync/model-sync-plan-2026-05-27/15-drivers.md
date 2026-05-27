# Drivers — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/15-drivers.md`](../15-drivers.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 0 | 7 | 0 |
| `CreateDriverRequest` | request | 0 | 0 | 5 | 0 |
| `UpdateDriverRequest` | request | 0 | 0 | 5 | 0 |
| `Driver` | response | 0 | 0 | 3 | 0 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=20, LOW=0  
**Total deduped findings**: 20

## MEDIUM (20)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /fleet/drivers) is missing query parameter `attributeValueIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? attributeValueIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/drivers) is missing query parameter `attributes` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? attributes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/drivers) is missing query parameter `createdAfterTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers`
  - Recommended fix: Add an optional parameter `string? createdAfterTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/drivers) is missing query parameter `driverActivationStatus` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers`
  - Recommended fix: Add an optional parameter `string? driverActivationStatus = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/drivers) is missing query parameter `parentTagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/drivers) is missing query parameter `tagIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/drivers`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/drivers) is missing query parameter `updatedAfterTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/drivers`
  - Recommended fix: Add an optional parameter `string? updatedAfterTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateDriverRequest` (request)

- **[type_mismatch]** CreateDriverRequest.attributes: SDK type `System.Text.Json.JsonElement?` does not match spec type `array`.
  - Endpoints: `POST /fleet/drivers`
  - Recommended fix: Change `CreateDriverRequest.Attributes` from `System.Text.Json.JsonElement?` to `IReadOnlyList<object>?`.
- **[type_mismatch]** CreateDriverRequest.hasDrivingFeaturesHidden: SDK type `System.Text.Json.JsonElement?` does not match spec type `boolean`.
  - Endpoints: `POST /fleet/drivers`
  - Recommended fix: Change `CreateDriverRequest.HasDrivingFeaturesHidden` from `System.Text.Json.JsonElement?` to `bool?`.
- **[type_mismatch]** CreateDriverRequest.hasVehicleUnpinningEnabled: SDK type `System.Text.Json.JsonElement?` does not match spec type `boolean`.
  - Endpoints: `POST /fleet/drivers`
  - Recommended fix: Change `CreateDriverRequest.HasVehicleUnpinningEnabled` from `System.Text.Json.JsonElement?` to `bool?`.
- **[type_mismatch]** CreateDriverRequest.profileImageBase64: SDK type `System.Text.Json.JsonElement?` does not match spec type `string`.
  - Endpoints: `POST /fleet/drivers`
  - Recommended fix: Change `CreateDriverRequest.ProfileImageBase64` from `System.Text.Json.JsonElement?` to `string?`.
- **[type_mismatch]** CreateDriverRequest.profileImageUrl: SDK type `System.Text.Json.JsonElement?` does not match spec type `string`.
  - Endpoints: `POST /fleet/drivers`
  - Recommended fix: Change `CreateDriverRequest.ProfileImageUrl` from `System.Text.Json.JsonElement?` to `string?`.

### `Driver` (response)

- **[type_mismatch]** Driver.attributes (response): SDK `System.Text.Json.JsonElement?` vs spec `array`. (affects 4 endpoints)
  - Endpoints: `GET /fleet/drivers`, `GET /fleet/drivers/{id}`, `PATCH /fleet/drivers/{id}`, `POST /fleet/drivers`
  - Recommended fix: Change `Driver.Attributes` from `System.Text.Json.JsonElement?` to `IReadOnlyList<object>`.
- **[type_mismatch]** Driver.hasDrivingFeaturesHidden (response): SDK `System.Text.Json.JsonElement?` vs spec `boolean`. (affects 4 endpoints)
  - Endpoints: `GET /fleet/drivers`, `GET /fleet/drivers/{id}`, `PATCH /fleet/drivers/{id}`, `POST /fleet/drivers`
  - Recommended fix: Change `Driver.HasDrivingFeaturesHidden` from `System.Text.Json.JsonElement?` to `bool`.
- **[type_mismatch]** Driver.hasVehicleUnpinningEnabled (response): SDK `System.Text.Json.JsonElement?` vs spec `boolean`. (affects 4 endpoints)
  - Endpoints: `GET /fleet/drivers`, `GET /fleet/drivers/{id}`, `PATCH /fleet/drivers/{id}`, `POST /fleet/drivers`
  - Recommended fix: Change `Driver.HasVehicleUnpinningEnabled` from `System.Text.Json.JsonElement?` to `bool`.

### `UpdateDriverRequest` (request)

- **[type_mismatch]** UpdateDriverRequest.attributes: SDK type `System.Text.Json.JsonElement?` does not match spec type `array`.
  - Endpoints: `PATCH /fleet/drivers/{id}`
  - Recommended fix: Change `UpdateDriverRequest.Attributes` from `System.Text.Json.JsonElement?` to `IReadOnlyList<object>?`.
- **[type_mismatch]** UpdateDriverRequest.hasDrivingFeaturesHidden: SDK type `System.Text.Json.JsonElement?` does not match spec type `boolean`.
  - Endpoints: `PATCH /fleet/drivers/{id}`
  - Recommended fix: Change `UpdateDriverRequest.HasDrivingFeaturesHidden` from `System.Text.Json.JsonElement?` to `bool?`.
- **[type_mismatch]** UpdateDriverRequest.hasVehicleUnpinningEnabled: SDK type `System.Text.Json.JsonElement?` does not match spec type `boolean`.
  - Endpoints: `PATCH /fleet/drivers/{id}`
  - Recommended fix: Change `UpdateDriverRequest.HasVehicleUnpinningEnabled` from `System.Text.Json.JsonElement?` to `bool?`.
- **[type_mismatch]** UpdateDriverRequest.profileImageBase64: SDK type `System.Text.Json.JsonElement?` does not match spec type `string`.
  - Endpoints: `PATCH /fleet/drivers/{id}`
  - Recommended fix: Change `UpdateDriverRequest.ProfileImageBase64` from `System.Text.Json.JsonElement?` to `string?`.
- **[type_mismatch]** UpdateDriverRequest.profileImageUrl: SDK type `System.Text.Json.JsonElement?` does not match spec type `string`.
  - Endpoints: `PATCH /fleet/drivers/{id}`
  - Recommended fix: Change `UpdateDriverRequest.ProfileImageUrl` from `System.Text.Json.JsonElement?` to `string?`.

