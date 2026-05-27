# Auth Token for Driver — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/05-auth-token-for-driver.md`](../05-auth-token-for-driver.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `CreateDriverAuthTokenRequest` | request | 0 | 1 | 3 | 1 |
| `DriverAuthToken` | response | 0 | 1 | 0 | 2 |

**Counts**: CRITICAL=0, HIGH=2, MEDIUM=3, LOW=3  
**Total deduped findings**: 8

## HIGH (2)

### `CreateDriverAuthTokenRequest` (request)

- **[missing_required]** CreateDriverAuthTokenRequest is missing REQUIRED property `code` (spec type=string).
  - Endpoints: `POST /fleet/drivers/auth-token`
  - Recommended fix: Add `[JsonPropertyName("code")] public required string Code { get; init; }` to `CreateDriverAuthTokenRequest`.

### `DriverAuthToken` (response)

- **[response_drift_required]** DriverAuthToken (response) missing REQUIRED property `expirationTime` (spec type=integer/int64).
  - Endpoints: `POST /fleet/drivers/auth-token`
  - Recommended fix: Add `[JsonPropertyName("expirationTime")] public long ExpirationTime { get; init; }` to response record `DriverAuthToken` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (3)

### `CreateDriverAuthTokenRequest` (request)

- **[missing_optional]** CreateDriverAuthTokenRequest is missing property `externalId` (spec type=string).
  - Endpoints: `POST /fleet/drivers/auth-token`
  - Recommended fix: Add `[JsonPropertyName("externalId")] public string? ExternalId { get; init; }` to `CreateDriverAuthTokenRequest`.
- **[missing_optional]** CreateDriverAuthTokenRequest is missing property `username` (spec type=string).
  - Endpoints: `POST /fleet/drivers/auth-token`
  - Recommended fix: Add `[JsonPropertyName("username")] public string? Username { get; init; }` to `CreateDriverAuthTokenRequest`.
- **[type_mismatch]** CreateDriverAuthTokenRequest.driverId: SDK type `string` does not match spec type `integer/int64`.
  - Endpoints: `POST /fleet/drivers/auth-token`
  - Recommended fix: Change `CreateDriverAuthTokenRequest.DriverId` from `string` to `long?`.

## LOW (3)

### `CreateDriverAuthTokenRequest` (request)

- **[required_drift_over]** CreateDriverAuthTokenRequest.driverId: SDK marks `required` but spec is optional.
  - Endpoints: `POST /fleet/drivers/auth-token`
  - Recommended fix: Drop `required` on `CreateDriverAuthTokenRequest.DriverId` (spec marks it optional) — make nullable.

### `DriverAuthToken` (response)

- **[extra_property]** DriverAuthToken.driverId (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/drivers/auth-token`
  - Recommended fix: Remove `DriverAuthToken.DriverId` (not in spec).
- **[extra_property]** DriverAuthToken.expiresAt (response): present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/drivers/auth-token`
  - Recommended fix: Remove `DriverAuthToken.ExpiresAt` (not in spec).

