# Fuel and Energy — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/18-fuel-and-energy.md`](../18-fuel-and-energy.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `f297ca4` on 2026-05-27**

## Implementation notes

All HIGH (13) and MEDIUM (32) findings were applied. LOW findings on the
response records were intentionally retained as nullable back-compat
properties per the workflow precedent established in
`08-carrier-proposed-assignments`, `13-driver-trailer-assignments`,
`14-driver-vehicle-assignments`, `16-equipment`, and `17-forms` (flat-scalar
conveniences kept so existing consumers don't silently break).

The audit tool's `*ReportsResponse` findings were applied to the actual SDK
row records — `FuelEnergyDriverReport` and `FuelEnergyVehicleReport` — since
those are what carry the spec's per-row schema. The `FuelEnergyDriverReportsResponse`
/ `FuelEnergyVehicleReportsResponse` wrappers (which carry `driverReports`
/ `vehicleReports` arrays) are unchanged: the LOW `extra_property` findings
on those wrappers are conservative retentions of the existing `data` shape.

Files touched:
- `src/Samsara.Sdk/Models/Fuel/FuelModels.cs` — tightened required fields
  on `FuelEnergyVehicleReport` and `FuelEnergyDriverReport`
  (`distanceTraveledMeters`, `efficiencyMpge`, `estFuelEnergyCost`, plus
  `vehicle`/`driver`) to non-nullable `required`; introduced typed
  `DriverEfficiencyDifficultyScore`, `DriverEfficiencyPercentageData`,
  `DriverEfficiencyRawData`, and `DriverEfficiencyScoreData` records to
  replace the four `object?` weak typings on `DriverEfficiencyByDriver`
  and `DriverEfficiencyByVehicle`; tightened `driverId`/`vehicleId` to
  `required string`; introduced `FuelPurchaseMoney` (required `amount`
  + `currency`) and used it to type the previously-`object` `transactionPrice`
  and `discount` on `CreateFuelPurchaseRequest`; added `required string Uuid`
  to `FuelPurchase`.
- `src/Samsara.Sdk/Clients/Fuel/IFuelClient.cs` and
  `src/Samsara.Sdk/Clients/Fuel/FuelClient.cs` — added required
  `startTime` / `endTime` plus optional `driverIds` / `vehicleIds` /
  `dataFormats` / `tagIds` / `parentTagIds` to the two driver-efficiency
  methods (previously took no parameters at all); added optional `after`
  cursor to `ListVehicleFuelEnergyReportsAsync` and
  `ListDriverFuelEnergyReportsAsync`.
- `src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs` — registered the
  five new fuel/efficiency records (`FuelPurchaseMoney`,
  `DriverEfficiencyDifficultyScore`, `DriverEfficiencyPercentageData`,
  `DriverEfficiencyRawData`, `DriverEfficiencyScoreData`).

Two minor spec gaps the audit did not flag and which were not changed:

- `FuelEnergyCost.currency` keeps the existing SDK property name even
  though the spec calls it `currencyCode`. The plan did not flag this and
  changing it is out of scope.
- The driver-efficiency `dataFormats` array is typed as
  `IReadOnlyList<string>?` (rather than the audit's suggested
  `IReadOnlyList<object>?`), matching the spec's
  `items: { type: "string" }` declaration and the established pattern for
  comma-joined enum arrays elsewhere in the SDK.

Verification:
- `dotnet build Samsara.Dotnet.sln`: 0 errors, 0 warnings.
- `dotnet test tests/Samsara.Sdk.Tests`: 59 passed.
- `python3 tools/check-sdk-sync.py --fail-on-mismatch`: exit 0.

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 4 | 10 | 0 |
| `FuelEnergyDriverReportsResponse` | response | 0 | 4 | 5 | 1 |
| `FuelEnergyVehicleReportsResponse` | response | 0 | 4 | 5 | 1 |
| `FuelPurchase` | response | 0 | 1 | 0 | 9 |
| `DriverEfficiencyByDriver` | response | 0 | 0 | 5 | 0 |
| `DriverEfficiencyByVehicle` | response | 0 | 0 | 5 | 0 |
| `CreateFuelPurchaseRequest` | request | 0 | 0 | 2 | 0 |

**Counts**: CRITICAL=0, HIGH=13, MEDIUM=32, LOW=11  
**Total deduped findings**: 56

## HIGH (13)

### `(no SDK type)` (query)

- **[missing_required_query]** GetDriverEfficiencyByDriverAsync (GET /driver-efficiency/drivers) is missing query parameter `endTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Add a required parameter (e.g. `string endTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endTime", ...)`.
- **[missing_required_query]** GetDriverEfficiencyByVehicleAsync (GET /driver-efficiency/vehicles) is missing query parameter `endTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Add a required parameter (e.g. `string endTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("endTime", ...)`.
- **[missing_required_query]** GetDriverEfficiencyByDriverAsync (GET /driver-efficiency/drivers) is missing query parameter `startTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Add a required parameter (e.g. `string startTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startTime", ...)`.
- **[missing_required_query]** GetDriverEfficiencyByVehicleAsync (GET /driver-efficiency/vehicles) is missing query parameter `startTime` (spec REQUIRED, type=string).
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Add a required parameter (e.g. `string startTime` , no default) to the SDK method and append it via `QueryBuilder.WithParams("startTime", ...)`.

### `FuelEnergyDriverReportsResponse` (response)

- **[response_drift_required]** FuelEnergyDriverReportsResponse (response) missing REQUIRED property `distanceTraveledMeters` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("distanceTraveledMeters")] public double DistanceTraveledMeters { get; init; }` to response record `FuelEnergyDriverReportsResponse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FuelEnergyDriverReportsResponse (response) missing REQUIRED property `driver` (spec type=object).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object Driver { get; init; }` to response record `FuelEnergyDriverReportsResponse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FuelEnergyDriverReportsResponse (response) missing REQUIRED property `efficiencyMpge` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("efficiencyMpge")] public double EfficiencyMpge { get; init; }` to response record `FuelEnergyDriverReportsResponse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FuelEnergyDriverReportsResponse (response) missing REQUIRED property `estFuelEnergyCost` (spec type=object).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("estFuelEnergyCost")] public object EstFuelEnergyCost { get; init; }` to response record `FuelEnergyDriverReportsResponse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `FuelEnergyVehicleReportsResponse` (response)

- **[response_drift_required]** FuelEnergyVehicleReportsResponse (response) missing REQUIRED property `distanceTraveledMeters` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("distanceTraveledMeters")] public double DistanceTraveledMeters { get; init; }` to response record `FuelEnergyVehicleReportsResponse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FuelEnergyVehicleReportsResponse (response) missing REQUIRED property `efficiencyMpge` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("efficiencyMpge")] public double EfficiencyMpge { get; init; }` to response record `FuelEnergyVehicleReportsResponse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FuelEnergyVehicleReportsResponse (response) missing REQUIRED property `estFuelEnergyCost` (spec type=object).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("estFuelEnergyCost")] public object EstFuelEnergyCost { get; init; }` to response record `FuelEnergyVehicleReportsResponse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** FuelEnergyVehicleReportsResponse (response) missing REQUIRED property `vehicle` (spec type=object).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("vehicle")] public object Vehicle { get; init; }` to response record `FuelEnergyVehicleReportsResponse` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `FuelPurchase` (response)

- **[response_drift_required]** FuelPurchase (response) missing REQUIRED property `uuid` (spec type=string).
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Add `[JsonPropertyName("uuid")] public string Uuid { get; init; }` to response record `FuelPurchase` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (32)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListVehicleFuelEnergyReportsAsync (GET /fleet/reports/vehicles/fuel-energy) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDriverFuelEnergyReportsAsync (GET /fleet/reports/drivers/fuel-energy) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyByDriverAsync (GET /driver-efficiency/drivers) is missing query parameter `dataFormats` (spec optional, type=array).
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? dataFormats = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyByVehicleAsync (GET /driver-efficiency/vehicles) is missing query parameter `dataFormats` (spec optional, type=array).
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? dataFormats = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyByDriverAsync (GET /driver-efficiency/drivers) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyByDriverAsync (GET /driver-efficiency/drivers) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyByVehicleAsync (GET /driver-efficiency/vehicles) is missing query parameter `parentTagIds` (spec optional, type=string).
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Add an optional parameter `string? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyByDriverAsync (GET /driver-efficiency/drivers) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyByVehicleAsync (GET /driver-efficiency/vehicles) is missing query parameter `tagIds` (spec optional, type=string).
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Add an optional parameter `string? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetDriverEfficiencyByVehicleAsync (GET /driver-efficiency/vehicles) is missing query parameter `vehicleIds` (spec optional, type=string).
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Add an optional parameter `string? vehicleIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateFuelPurchaseRequest` (request)

- **[weak_typing]** CreateFuelPurchaseRequest.discount: SDK uses weak `object` for spec type `object`.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Replace weak `object?` with a typed model on `CreateFuelPurchaseRequest.Discount` (spec type=`object`).
- **[weak_typing]** CreateFuelPurchaseRequest.transactionPrice: SDK uses weak `object` for spec type `object`.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Replace weak `object?` with a typed model on `CreateFuelPurchaseRequest.TransactionPrice` (spec type=`object`).

### `DriverEfficiencyByDriver` (response)

- **[response_required_drift]** DriverEfficiencyByDriver.driverId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Tighten `DriverEfficiencyByDriver.DriverId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[weak_typing]** DriverEfficiencyByDriver.difficultyScore (response): SDK uses weak `object` for spec type `object`.
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Replace weak `object?` with a typed model on `DriverEfficiencyByDriver.DifficultyScore` (spec type=`object`).
- **[weak_typing]** DriverEfficiencyByDriver.percentageData (response): SDK uses weak `object` for spec type `object`.
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Replace weak `object?` with a typed model on `DriverEfficiencyByDriver.PercentageData` (spec type=`object`).
- **[weak_typing]** DriverEfficiencyByDriver.rawData (response): SDK uses weak `object` for spec type `object`.
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Replace weak `object?` with a typed model on `DriverEfficiencyByDriver.RawData` (spec type=`object`).
- **[weak_typing]** DriverEfficiencyByDriver.scoreData (response): SDK uses weak `object` for spec type `object`.
  - Endpoints: `GET /driver-efficiency/drivers`
  - Recommended fix: Replace weak `object?` with a typed model on `DriverEfficiencyByDriver.ScoreData` (spec type=`object`).

### `DriverEfficiencyByVehicle` (response)

- **[response_required_drift]** DriverEfficiencyByVehicle.vehicleId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Tighten `DriverEfficiencyByVehicle.VehicleId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[weak_typing]** DriverEfficiencyByVehicle.difficultyScore (response): SDK uses weak `object` for spec type `object`.
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Replace weak `object?` with a typed model on `DriverEfficiencyByVehicle.DifficultyScore` (spec type=`object`).
- **[weak_typing]** DriverEfficiencyByVehicle.percentageData (response): SDK uses weak `object` for spec type `object`.
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Replace weak `object?` with a typed model on `DriverEfficiencyByVehicle.PercentageData` (spec type=`object`).
- **[weak_typing]** DriverEfficiencyByVehicle.rawData (response): SDK uses weak `object` for spec type `object`.
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Replace weak `object?` with a typed model on `DriverEfficiencyByVehicle.RawData` (spec type=`object`).
- **[weak_typing]** DriverEfficiencyByVehicle.scoreData (response): SDK uses weak `object` for spec type `object`.
  - Endpoints: `GET /driver-efficiency/vehicles`
  - Recommended fix: Replace weak `object?` with a typed model on `DriverEfficiencyByVehicle.ScoreData` (spec type=`object`).

### `FuelEnergyDriverReportsResponse` (response)

- **[response_drift_optional]** FuelEnergyDriverReportsResponse (response) missing property `energyUsedKwh` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("energyUsedKwh")] public double? EnergyUsedKwh { get; init; }` to response record `FuelEnergyDriverReportsResponse`.
- **[response_drift_optional]** FuelEnergyDriverReportsResponse (response) missing property `engineIdleTimeDurationMs` (spec type=integer/int64).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("engineIdleTimeDurationMs")] public long? EngineIdleTimeDurationMs { get; init; }` to response record `FuelEnergyDriverReportsResponse`.
- **[response_drift_optional]** FuelEnergyDriverReportsResponse (response) missing property `engineRunTimeDurationMs` (spec type=integer/int64).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("engineRunTimeDurationMs")] public long? EngineRunTimeDurationMs { get; init; }` to response record `FuelEnergyDriverReportsResponse`.
- **[response_drift_optional]** FuelEnergyDriverReportsResponse (response) missing property `estCarbonEmissionsKg` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("estCarbonEmissionsKg")] public double? EstCarbonEmissionsKg { get; init; }` to response record `FuelEnergyDriverReportsResponse`.
- **[response_drift_optional]** FuelEnergyDriverReportsResponse (response) missing property `fuelConsumedMl` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("fuelConsumedMl")] public double? FuelConsumedMl { get; init; }` to response record `FuelEnergyDriverReportsResponse`.

### `FuelEnergyVehicleReportsResponse` (response)

- **[response_drift_optional]** FuelEnergyVehicleReportsResponse (response) missing property `energyUsedKwh` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("energyUsedKwh")] public double? EnergyUsedKwh { get; init; }` to response record `FuelEnergyVehicleReportsResponse`.
- **[response_drift_optional]** FuelEnergyVehicleReportsResponse (response) missing property `engineIdleTimeDurationMs` (spec type=integer/int64).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("engineIdleTimeDurationMs")] public long? EngineIdleTimeDurationMs { get; init; }` to response record `FuelEnergyVehicleReportsResponse`.
- **[response_drift_optional]** FuelEnergyVehicleReportsResponse (response) missing property `engineRunTimeDurationMs` (spec type=integer/int64).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("engineRunTimeDurationMs")] public long? EngineRunTimeDurationMs { get; init; }` to response record `FuelEnergyVehicleReportsResponse`.
- **[response_drift_optional]** FuelEnergyVehicleReportsResponse (response) missing property `estCarbonEmissionsKg` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("estCarbonEmissionsKg")] public double? EstCarbonEmissionsKg { get; init; }` to response record `FuelEnergyVehicleReportsResponse`.
- **[response_drift_optional]** FuelEnergyVehicleReportsResponse (response) missing property `fuelConsumedMl` (spec type=number/double).
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Add `[JsonPropertyName("fuelConsumedMl")] public double? FuelConsumedMl { get; init; }` to response record `FuelEnergyVehicleReportsResponse`.

## LOW (11)

### `FuelEnergyDriverReportsResponse` (response)

- **[extra_property]** FuelEnergyDriverReportsResponse.driverReports (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/reports/drivers/fuel-energy`
  - Recommended fix: Remove `FuelEnergyDriverReportsResponse.DriverReports` (not in spec).

### `FuelEnergyVehicleReportsResponse` (response)

- **[extra_property]** FuelEnergyVehicleReportsResponse.vehicleReports (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /fleet/reports/vehicles/fuel-energy`
  - Recommended fix: Remove `FuelEnergyVehicleReportsResponse.VehicleReports` (not in spec).

### `FuelPurchase` (response)

- **[extra_property]** FuelPurchase.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.DriverId` (not in spec).
- **[extra_property]** FuelPurchase.fuelGrade (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.FuelGrade` (not in spec).
- **[extra_property]** FuelPurchase.fuelQuantityLiters (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.FuelQuantityLiters` (not in spec).
- **[extra_property]** FuelPurchase.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.Id` (not in spec).
- **[extra_property]** FuelPurchase.iftaFuelType (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.IftaFuelType` (not in spec).
- **[extra_property]** FuelPurchase.transactionLocation (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.TransactionLocation` (not in spec).
- **[extra_property]** FuelPurchase.transactionReference (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.TransactionReference` (not in spec).
- **[extra_property]** FuelPurchase.transactionTime (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.TransactionTime` (not in spec).
- **[extra_property]** FuelPurchase.vehicleId (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fuel-purchase`
  - Recommended fix: Remove `FuelPurchase.VehicleId` (not in spec).

