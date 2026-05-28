# Vehicles — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/54-vehicles.md`](../54-vehicles.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `115ec3c` on 2026-05-27**

## Implementation notes

All 19 findings were addressed; the HIGH was applied as `required`, the MEDIUM
response/request/query items were applied, the two `weak_typing` items and the
four LOW response-side extras were intentionally KEPT per the workflow precedent
(cf. `40-safety-scores`, `52-vehicle-locations`, `53-vehicle-stats`) rather than
restructured/removed.

**`Vehicle` response (6 new props)**

- **`createdAtTime` (response_drift_required, HIGH)** — added as
  `required DateTimeOffset` (placed after `name`). The shared spec schema
  (`VehicleResponseObjectResponseBody`) marks it REQUIRED across the vehicle
  endpoints, so it is part of the guaranteed payload. SAFE — no
  `new Vehicle(...)` construction sites exist in src/tools/tests.
  **Breaking**: consumers may now rely on a non-null `CreatedAtTime`; the two
  `VehiclesClientTests` mock fixtures (`GetAsync`, `UpdateAsync`) were updated to
  include `createdAtTime` (precedent: `02-alerts` `Alert.createdAtTime`), or
  `System.Text.Json`'s `required` check throws on deserialization.
- **5 new nullable props** (response_drift_optional): `updatedAtTime`
  (`DateTimeOffset?`), `isRemotePrivacyButtonEnabled` (`bool?`), `vehicleWeight`
  (`long?`), `vehicleWeightInKilograms` (`long?`), `vehicleWeightInPounds`
  (`long?`).
- **`weak_typing` (2) — KEPT as `object?`**: `grossVehicleWeight` and
  `sensorConfiguration` remain weakly-typed `object?`. The plan recommends typed
  models, but this effort consistently keeps complex un-schematized nested
  `type=object` fields as `object` (no fabricated models) — cf. the 58 `object?`
  props in `53-vehicle-stats`.
- **LOW (4) — RETAINED**: `engineHours` (`long?`), `gatewaySerial` (`string?`),
  `grossVehicleWeight` (`object?`), `odometerMeters` (`double?`) were left in
  place (all already nullable) rather than removed. `grossVehicleWeight` is both
  a `weak_typing`-keep and a LOW-retain — same outcome: untouched `object?`.

**`UpdateVehicleRequest` request (1 change)**

- **`attributes` (type_mismatch)** — changed from
  `System.Text.Json.JsonElement?` to `IReadOnlyList<object>?` (spec `type=array`).
  SAFE — the only construction site
  (`new UpdateVehicleRequest { Name = "Updated Truck" }` in
  `VehiclesClientTests`) does not set `attributes`. `CreateVehicleRequest` and the
  separate typed `Vehicle.attributes` property were left untouched.

**MEDIUM query params (6 on `ListAsync`)**

`ListAsync` (`GET /fleet/vehicles`) previously took only the cancellation token;
it now appends six optional query params via `QueryBuilder.WithParams`. All are
`string?` EXCEPT `attributes` which is `IReadOnlyList<string>?` (spec
`type=array`, comma-joined per the query-array convention): `attributes`,
`attributeValueIds`, `tagIds`, `parentTagIds`, `createdAfterTime`,
`updatedAfterTime`.

**Caller updated**

- CLI (`tools/Samsara.Cli/TuiApp.cs`): the `List All` vehicle action now passes
  the cancellation token by name (`cancellationToken:` — the 1st positional slot
  is now the `attributes` filter). The render line
  (`v => [v.Id, v.Name ?? "", v.Vin ?? "", v.LicensePlate ?? ""]`) is unchanged
  (those props remain nullable). `Get by ID`/`Update` calls are unchanged.

Files touched: `src/Samsara.Sdk/Models/Fleet/FleetModels.cs`,
`src/Samsara.Sdk/Clients/Fleet/IVehiclesClient.cs`,
`src/Samsara.Sdk/Clients/Fleet/VehiclesClient.cs`,
`tools/Samsara.Cli/TuiApp.cs`, `tests/Samsara.Sdk.Tests/VehiclesClientTests.cs`.
No `SamsaraJsonContext` changes (`Vehicle`/`UpdateVehicleRequest` already
registered; new props are scalar/`DateTimeOffset`/array → no new top-level
types). Other Vehicles methods untouched (stats/locations handled in 52/53).

Verification: `dotnet build` 0 errors / 0 warnings, `dotnet test` 59 passed, and
`check-sdk-sync.py --fail-on-mismatch` exits 0 (323/323 matched).

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

