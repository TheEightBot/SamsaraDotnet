# Gateways — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/19-gateways.md`](../19-gateways.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `Gateway` | response | 0 | 0 | 5 | 8 |
| `(no SDK type)` | query | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=6, LOW=8  
**Total deduped findings**: 14

## MEDIUM (6)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /gateways) is missing query parameter `models` (spec optional, type=array).
  - Endpoints: `GET /gateways`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? models = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `Gateway` (response)

- **[response_drift_optional]** Gateway (response) missing property `accessoryDevices` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Add `[JsonPropertyName("accessoryDevices")] public IReadOnlyList<object>? AccessoryDevices { get; init; }` to response record `Gateway`.
- **[response_drift_optional]** Gateway (response) missing property `connectionStatus` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Add `[JsonPropertyName("connectionStatus")] public object? ConnectionStatus { get; init; }` to response record `Gateway`.
- **[response_drift_optional]** Gateway (response) missing property `dataUsageLast30Days` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Add `[JsonPropertyName("dataUsageLast30Days")] public object? DataUsageLast30Days { get; init; }` to response record `Gateway`.
- **[response_required_drift]** Gateway.model (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Tighten `Gateway.Model` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Gateway.serial (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Tighten `Gateway.Serial` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (8)

### `Gateway` (response)

- **[extra_property]** Gateway.firmwareVersion (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Remove `Gateway.FirmwareVersion` (not in spec).
- **[extra_property]** Gateway.id (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Remove `Gateway.Id` (not in spec).
- **[extra_property]** Gateway.mainBus (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Remove `Gateway.MainBus` (not in spec).
- **[extra_property]** Gateway.name (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Remove `Gateway.Name` (not in spec).
- **[extra_property]** Gateway.simCardId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Remove `Gateway.SimCardId` (not in spec).
- **[extra_property]** Gateway.tags (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Remove `Gateway.Tags` (not in spec).
- **[extra_property]** Gateway.vehicle (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Remove `Gateway.Vehicle` (not in spec).
- **[extra_property]** Gateway.wifiMacAddress (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /gateways`, `POST /gateways`
  - Recommended fix: Remove `Gateway.WifiMacAddress` (not in spec).

