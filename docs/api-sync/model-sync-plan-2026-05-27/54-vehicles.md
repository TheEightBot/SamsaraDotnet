# Vehicles — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/54-vehicles.md`](../54-vehicles.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `Vehicle` | response | 0 | 1 | 7 | 4 |
| `(no SDK type)` | query | 0 | 0 | 6 | 0 |
| `UpdateVehicleRequest` | request | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=1, MEDIUM=14, LOW=4  
**Total deduped findings**: 19

## HIGH (1)

### `Vehicle` (response)

- **[response_drift_required]** Vehicle (response) missing REQUIRED property `createdAtTime` (spec type=string/date-time).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public DateTimeOffset CreatedAtTime { get; init; }` to response record `Vehicle` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (14)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /fleet/vehicles) is missing query parameter `attributeValueIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add an optional parameter `string? attributeValueIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/vehicles) is missing query parameter `attributes` (spec optional, type=array).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? attributes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/vehicles) is missing query parameter `createdAfterTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add an optional parameter `string? createdAfterTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/vehicles) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/vehicles) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/vehicles) is missing query parameter `updatedAfterTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add an optional parameter `string? updatedAfterTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `UpdateVehicleRequest` (request)

- **[type_mismatch]** UpdateVehicleRequest.attributes: SDK type `System.Text.Json.JsonElement?` does not match spec type `array`.
  - Endpoints: `PATCH /fleet/vehicles/{id}`
  - Recommended fix: Change `UpdateVehicleRequest.Attributes` from `System.Text.Json.JsonElement?` to `IReadOnlyList<object>?`.

### `Vehicle` (response)

- **[response_drift_optional]** Vehicle (response) missing property `isRemotePrivacyButtonEnabled` (spec type=boolean).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add `[JsonPropertyName("isRemotePrivacyButtonEnabled")] public bool? IsRemotePrivacyButtonEnabled { get; init; }` to response record `Vehicle`.
- **[response_drift_optional]** Vehicle (response) missing property `updatedAtTime` (spec type=string/date-time).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add `[JsonPropertyName("updatedAtTime")] public DateTimeOffset? UpdatedAtTime { get; init; }` to response record `Vehicle`.
- **[response_drift_optional]** Vehicle (response) missing property `vehicleWeight` (spec type=integer/int64).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add `[JsonPropertyName("vehicleWeight")] public long? VehicleWeight { get; init; }` to response record `Vehicle`.
- **[response_drift_optional]** Vehicle (response) missing property `vehicleWeightInKilograms` (spec type=integer/int64).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add `[JsonPropertyName("vehicleWeightInKilograms")] public long? VehicleWeightInKilograms { get; init; }` to response record `Vehicle`.
- **[response_drift_optional]** Vehicle (response) missing property `vehicleWeightInPounds` (spec type=integer/int64).
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Add `[JsonPropertyName("vehicleWeightInPounds")] public long? VehicleWeightInPounds { get; init; }` to response record `Vehicle`.
- **[weak_typing]** Vehicle.grossVehicleWeight (response): SDK uses weak `object` for spec type `object`. (affects 2 endpoints)
  - Endpoints: `GET /fleet/vehicles/{id}`, `PATCH /fleet/vehicles/{id}`
  - Recommended fix: Replace weak `object?` with a typed model on `Vehicle.GrossVehicleWeight` (spec type=`object`).
- **[weak_typing]** Vehicle.sensorConfiguration (response): SDK uses weak `object` for spec type `object`. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles`, `GET /fleet/vehicles/{id}`, `PATCH /fleet/vehicles/{id}`
  - Recommended fix: Replace weak `object?` with a typed model on `Vehicle.SensorConfiguration` (spec type=`object`).

## LOW (4)

### `Vehicle` (response)

- **[extra_property]** Vehicle.engineHours (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles`, `GET /fleet/vehicles/{id}`, `PATCH /fleet/vehicles/{id}`
  - Recommended fix: Remove `Vehicle.EngineHours` (not in spec).
- **[extra_property]** Vehicle.gatewaySerial (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles`, `GET /fleet/vehicles/{id}`, `PATCH /fleet/vehicles/{id}`
  - Recommended fix: Remove `Vehicle.GatewaySerial` (not in spec).
- **[extra_property]** Vehicle.grossVehicleWeight (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/vehicles`
  - Recommended fix: Remove `Vehicle.GrossVehicleWeight` (not in spec).
- **[extra_property]** Vehicle.odometerMeters (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /fleet/vehicles`, `GET /fleet/vehicles/{id}`, `PATCH /fleet/vehicles/{id}`
  - Recommended fix: Remove `Vehicle.OdometerMeters` (not in spec).

