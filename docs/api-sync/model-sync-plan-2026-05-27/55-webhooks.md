# Webhooks — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/55-webhooks.md`](../55-webhooks.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

