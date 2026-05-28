# Driver QR Codes — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/12-driver-qr-codes.md`](../12-driver-qr-codes.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `d3100e0` on 2026-05-27**

## Implementation notes

All 6 findings (HIGH=1, MEDIUM=3, LOW=2) were applied across
`src/Samsara.Sdk/Clients/Fleet/IDriversClient.cs`,
`src/Samsara.Sdk/Clients/Fleet/DriversClient.cs`, and
`src/Samsara.Sdk/Models/Drivers/DriverModels.cs`:

- **HIGH — missing required `driverIds` query**: `IDriversClient.ListQrCodesAsync`
  now takes `IReadOnlyList<string> driverIds` (no default), and the
  implementation appends it via
  `QueryBuilder.WithParams("drivers/qr-codes", ("driverIds", string.Join(",", driverIds)))`
  — same pattern used elsewhere for required list-style query params (e.g.
  `QualificationRecordsClient.ListAsync`).
- **MEDIUM — `CreateDriverQrCodeRequest.DriverId` type**: changed from
  `required string` to `required long` (spec `integer/int64`, required).
- **MEDIUM — `DriverQrCode.driverId` type**: changed from
  `required string` to `required long` (spec `integer/int64`, required).
- **MEDIUM — add `DriverQrCode.qrCodeLink`**: added
  `[JsonPropertyName("qrCodeLink")] public string? QrCodeLink { get; init; }`
  per `QrCodeResponseObjectResponseBody` inner schema.
- **LOW — drop SDK-only `qrCodeUrl`**: removed (not in spec inner schema).
- **LOW — drop SDK-only `expiresAt`**: removed (not in spec inner schema).

No JSON-context registrations needed updating — `DriverQrCode` and
`CreateDriverQrCodeRequest` were already listed in
`SamsaraJsonContext.cs`. No tests referenced these types, so none needed
updating. Build is green and the SDK↔spec endpoint check passes
(`mismatched=0`, `not implemented=0`).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 1 | 0 | 0 |
| `DriverQrCode` | response | 0 | 0 | 2 | 2 |
| `CreateDriverQrCodeRequest` | request | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=1, MEDIUM=3, LOW=2  
**Total deduped findings**: 6

## HIGH (1)

### `(no SDK type)` (query)

- **[missing_required_query]** ListQrCodesAsync (GET /drivers/qr-codes) is missing query parameter `driverIds` (spec REQUIRED, type=array).
  - Endpoints: `GET /drivers/qr-codes`
  - Recommended fix: Add a required parameter (e.g. `IReadOnlyList<string> driverIds` , no default) to the SDK method and append it via `QueryBuilder.WithParams("driverIds", ...)`.

## MEDIUM (3)

### `CreateDriverQrCodeRequest` (request)

- **[type_mismatch]** CreateDriverQrCodeRequest.driverId: SDK type `string` does not match spec type `integer/int64`.
  - Endpoints: `POST /drivers/qr-codes`
  - Recommended fix: Change `CreateDriverQrCodeRequest.DriverId` from `string` to `long?`.

### `DriverQrCode` (response)

- **[response_drift_optional]** DriverQrCode (response) missing property `qrCodeLink` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /drivers/qr-codes`, `POST /drivers/qr-codes`
  - Recommended fix: Add `[JsonPropertyName("qrCodeLink")] public string? QrCodeLink { get; init; }` to response record `DriverQrCode`.
- **[type_mismatch]** DriverQrCode.driverId (response): SDK `string` vs spec `integer/int64`. (affects 2 endpoints)
  - Endpoints: `GET /drivers/qr-codes`, `POST /drivers/qr-codes`
  - Recommended fix: Change `DriverQrCode.DriverId` from `string` to `long`.

## LOW (2)

### `DriverQrCode` (response)

- **[extra_property]** DriverQrCode.expiresAt (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /drivers/qr-codes`, `POST /drivers/qr-codes`
  - Recommended fix: Remove `DriverQrCode.ExpiresAt` (not in spec).
- **[extra_property]** DriverQrCode.qrCodeUrl (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /drivers/qr-codes`, `POST /drivers/qr-codes`
  - Recommended fix: Remove `DriverQrCode.QrCodeUrl` (not in spec).

