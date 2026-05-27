# Messages — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/32-messages.md`](../32-messages.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `DriverMessage` | response | 0 | 3 | 3 | 4 |
| `SendDriverMessageRequest` | request | 0 | 2 | 0 | 2 |
| `(no SDK type)` | query | 0 | 0 | 2 | 0 |

**Counts**: CRITICAL=0, HIGH=5, MEDIUM=5, LOW=6  
**Total deduped findings**: 16

## HIGH (5)

### `DriverMessage` (response)

- **[response_drift_required]** DriverMessage (response) missing REQUIRED property `isRead` (spec type=boolean).
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Add `[JsonPropertyName("isRead")] public bool IsRead { get; init; }` to response record `DriverMessage` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverMessage (response) missing REQUIRED property `sender` (spec type=object).
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Add `[JsonPropertyName("sender")] public object Sender { get; init; }` to response record `DriverMessage` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** DriverMessage (response) missing REQUIRED property `text` (spec type=string).
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Add `[JsonPropertyName("text")] public string Text { get; init; }` to response record `DriverMessage` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `SendDriverMessageRequest` (request)

- **[missing_required]** SendDriverMessageRequest is missing REQUIRED property `driverIds` (spec type=array).
  - Endpoints: `POST /v1/fleet/messages`
  - Recommended fix: Add `[JsonPropertyName("driverIds")] public required IReadOnlyList<string> DriverIds { get; init; }` to `SendDriverMessageRequest`.
- **[missing_required]** SendDriverMessageRequest is missing REQUIRED property `text` (spec type=string).
  - Endpoints: `POST /v1/fleet/messages`
  - Recommended fix: Add `[JsonPropertyName("text")] public required string Text { get; init; }` to `SendDriverMessageRequest`.

## MEDIUM (5)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /v1/fleet/messages) is missing query parameter `durationMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Add an optional parameter `int? durationMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /v1/fleet/messages) is missing query parameter `endMs` (spec optional, type=integer).
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Add an optional parameter `int? endMs = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `DriverMessage` (response)

- **[response_required_drift]** DriverMessage.driverId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Tighten `DriverMessage.DriverId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** DriverMessage.sentAtMs (response): spec marks REQUIRED but SDK exposes as nullable (`long?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Tighten `DriverMessage.SentAtMs` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[type_mismatch]** DriverMessage.driverId (response): SDK `string?` vs spec `integer/int64`.
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Change `DriverMessage.DriverId` from `string?` to `long`.

## LOW (6)

### `DriverMessage` (response)

- **[extra_property]** DriverMessage.body (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Remove `DriverMessage.Body` (not in spec).
- **[extra_property]** DriverMessage.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Remove `DriverMessage.Id` (not in spec).
- **[extra_property]** DriverMessage.readAtMs (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Remove `DriverMessage.ReadAtMs` (not in spec).
- **[extra_property]** DriverMessage.senderType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /v1/fleet/messages`
  - Recommended fix: Remove `DriverMessage.SenderType` (not in spec).

### `SendDriverMessageRequest` (request)

- **[extra_property]** SendDriverMessageRequest.body: present in SDK but not in spec inner schema.
  - Endpoints: `POST /v1/fleet/messages`
  - Recommended fix: Remove `SendDriverMessageRequest.Body` (not in spec).
- **[extra_property]** SendDriverMessageRequest.driverId: present in SDK but not in spec inner schema.
  - Endpoints: `POST /v1/fleet/messages`
  - Recommended fix: Remove `SendDriverMessageRequest.DriverId` (not in spec).

