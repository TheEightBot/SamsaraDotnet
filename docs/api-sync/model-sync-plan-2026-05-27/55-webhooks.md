# Webhooks — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/55-webhooks.md`](../55-webhooks.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `PENDING` on 2026-05-27**

## Implementation notes

All 7 findings were addressed. The HIGH and the three response `required` drifts
were applied as `required` non-nullable; the MEDIUM request/query items were
applied; the single LOW non-spec extra was intentionally RETAINED per the
workflow precedent (cf. `40-safety-scores`, `54-vehicles`) rather than removed.

**`Webhook` response**

- **`secretKey` (response_drift_required, HIGH)** — added as
  `required string` (placed after `version`). The spec marks it REQUIRED across
  all four webhook endpoints (`GET /webhooks`, `GET /webhooks/{id}`,
  `PATCH /webhooks/{id}`, `POST /webhooks`), so it is part of the guaranteed
  payload. SAFE — no `new Webhook(...)` construction sites and no `Webhook`
  deserialization fixtures exist in src/tools/tests (only facade/DI substitute
  tests, which check wiring and are unaffected).
- **`name`, `url`, `version` (response_required_drift, MED ×3)** — tightened
  from `string?` to `required string`. The spec marks all three REQUIRED in the
  response, so consumers may now rely on non-null values.

**`UpdateWebhookRequest` request**

- **`version` (missing_optional, MED)** — added as `string?` (placed after
  `url`, matching `CreateWebhookRequest`'s ordering).
- **`eventTypes` (extra_property, LOW)** — RETAINED as `IReadOnlyList<string>?`
  back-compat property (not in spec inner schema; kept per effort convention).

**Query**

- **`ids` (missing_optional_query, MED)** — added `string? ids = null` to
  `ListAsync` (`GET /webhooks`) and appended via `QueryBuilder.WithParams`.

**Breaking / collateral**

- Consumers may now rely on non-null `Webhook.Name`, `Webhook.Url`,
  `Webhook.Version`, and `Webhook.SecretKey`. `System.Text.Json`'s `required`
  check throws on deserialization if any are absent.
- The CLI `List All` webhook action passes the cancellation token by name (the
  1st positional slot is now `ids`) and drops the now-redundant null-coalescing
  on `Name`/`Url`.
- No JsonContext changes (`Webhook`/`UpdateWebhookRequest` already registered;
  new props are scalar `string`/`string?`, no new top-level types).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `Webhook` | response | 0 | 1 | 3 | 0 |
| `(no SDK type)` | query | 0 | 0 | 1 | 0 |
| `UpdateWebhookRequest` | request | 0 | 0 | 1 | 1 |

**Counts**: CRITICAL=0, HIGH=1, MEDIUM=5, LOW=1  
**Total deduped findings**: 7

## HIGH (1)

### `Webhook` (response)

- **[response_drift_required]** Webhook (response) missing REQUIRED property `secretKey` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /webhooks`, `GET /webhooks/{id}`, `PATCH /webhooks/{id}`, `POST /webhooks`
  - Recommended fix: Add `[JsonPropertyName("secretKey")] public string SecretKey { get; init; }` to response record `Webhook` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (5)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /webhooks) is missing query parameter `ids` (spec optional, type=string).
  - Endpoints: `GET /webhooks`
  - Recommended fix: Add an optional parameter `string? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `UpdateWebhookRequest` (request)

- **[missing_optional]** UpdateWebhookRequest is missing property `version` (spec type=string enum=['2018-01-01', '2021-06-09', '2022-09-13', '2024-02-27']).
  - Endpoints: `PATCH /webhooks/{id}`
  - Recommended fix: Add `[JsonPropertyName("version")] public string? Version { get; init; }` to `UpdateWebhookRequest`.

### `Webhook` (response)

- **[response_required_drift]** Webhook.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /webhooks`, `GET /webhooks/{id}`, `PATCH /webhooks/{id}`, `POST /webhooks`
  - Recommended fix: Tighten `Webhook.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Webhook.url (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /webhooks`, `GET /webhooks/{id}`, `PATCH /webhooks/{id}`, `POST /webhooks`
  - Recommended fix: Tighten `Webhook.Url` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Webhook.version (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /webhooks`, `GET /webhooks/{id}`, `PATCH /webhooks/{id}`, `POST /webhooks`
  - Recommended fix: Tighten `Webhook.Version` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (1)

### `UpdateWebhookRequest` (request)

- **[extra_property]** UpdateWebhookRequest.eventTypes: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /webhooks/{id}`
  - Recommended fix: Remove `UpdateWebhookRequest.EventTypes` (not in spec).

