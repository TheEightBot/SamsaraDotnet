# Live Sharing Links — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/28-live-sharing-links.md`](../28-live-sharing-links.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `LiveSharingLink` | response | 0 | 1 | 7 | 4 |
| `(no SDK type)` | query | 0 | 1 | 2 | 0 |
| `UpdateLiveSharingLinkRequest` | request | 0 | 1 | 2 | 2 |
| `CreateLiveSharingLinkRequest` | request | 0 | 0 | 5 | 2 |

**Counts**: CRITICAL=0, HIGH=3, MEDIUM=16, LOW=8  
**Total deduped findings**: 27

## HIGH (3)

### `(no SDK type)` (query)

- **[missing_required_query]** UpdateAsync (PATCH /live-shares) is missing query parameter `id` (spec REQUIRED, type=string).
  - Endpoints: `PATCH /live-shares`
  - Recommended fix: Add a required parameter (e.g. `string id` , no default) to the SDK method and append it via `QueryBuilder.WithParams("id", ...)`.

### `LiveSharingLink` (response)

- **[response_drift_required]** LiveSharingLink (response) missing REQUIRED property `liveSharingUrl` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("liveSharingUrl")] public string LiveSharingUrl { get; init; }` to response record `LiveSharingLink` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `UpdateLiveSharingLinkRequest` (request)

- **[required_drift]** UpdateLiveSharingLinkRequest.name: spec marks REQUIRED but SDK property is not `required`.
  - Endpoints: `PATCH /live-shares`
  - Recommended fix: Mark `UpdateLiveSharingLinkRequest.Name` as `required` (drop the `?` nullable marker).

## MEDIUM (16)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /live-shares) is missing query parameter `ids` (spec optional, type=array).
  - Endpoints: `GET /live-shares`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /live-shares) is missing query parameter `type` (spec optional, type=string).
  - Endpoints: `GET /live-shares`
  - Recommended fix: Add an optional parameter `string? type = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateLiveSharingLinkRequest` (request)

- **[missing_optional]** CreateLiveSharingLinkRequest is missing property `assetsLocationLinkConfig` (spec type=object).
  - Endpoints: `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("assetsLocationLinkConfig")] public object? AssetsLocationLinkConfig { get; init; }` to `CreateLiveSharingLinkRequest`.
- **[missing_optional]** CreateLiveSharingLinkRequest is missing property `assetsNearLocationLinkConfig` (spec type=object).
  - Endpoints: `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("assetsNearLocationLinkConfig")] public object? AssetsNearLocationLinkConfig { get; init; }` to `CreateLiveSharingLinkRequest`.
- **[missing_optional]** CreateLiveSharingLinkRequest is missing property `assetsOnRouteLinkConfig` (spec type=object).
  - Endpoints: `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("assetsOnRouteLinkConfig")] public object? AssetsOnRouteLinkConfig { get; init; }` to `CreateLiveSharingLinkRequest`.
- **[missing_optional]** CreateLiveSharingLinkRequest is missing property `description` (spec type=string).
  - Endpoints: `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("description")] public string? Description { get; init; }` to `CreateLiveSharingLinkRequest`.
- **[missing_optional]** CreateLiveSharingLinkRequest is missing property `expiresAtTime` (spec type=string).
  - Endpoints: `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("expiresAtTime")] public string? ExpiresAtTime { get; init; }` to `CreateLiveSharingLinkRequest`.

### `LiveSharingLink` (response)

- **[response_drift_optional]** LiveSharingLink (response) missing property `assetsLocationLinkConfig` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("assetsLocationLinkConfig")] public object? AssetsLocationLinkConfig { get; init; }` to response record `LiveSharingLink`.
- **[response_drift_optional]** LiveSharingLink (response) missing property `assetsNearLocationLinkConfig` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("assetsNearLocationLinkConfig")] public object? AssetsNearLocationLinkConfig { get; init; }` to response record `LiveSharingLink`.
- **[response_drift_optional]** LiveSharingLink (response) missing property `assetsOnRouteLinkConfig` (spec type=object). (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("assetsOnRouteLinkConfig")] public object? AssetsOnRouteLinkConfig { get; init; }` to response record `LiveSharingLink`.
- **[response_drift_optional]** LiveSharingLink (response) missing property `description` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("description")] public string? Description { get; init; }` to response record `LiveSharingLink`.
- **[response_drift_optional]** LiveSharingLink (response) missing property `expiresAtTime` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Add `[JsonPropertyName("expiresAtTime")] public string? ExpiresAtTime { get; init; }` to response record `LiveSharingLink`.
- **[response_required_drift]** LiveSharingLink.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Tighten `LiveSharingLink.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** LiveSharingLink.type (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Tighten `LiveSharingLink.Type` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateLiveSharingLinkRequest` (request)

- **[missing_optional]** UpdateLiveSharingLinkRequest is missing property `description` (spec type=string).
  - Endpoints: `PATCH /live-shares`
  - Recommended fix: Add `[JsonPropertyName("description")] public string? Description { get; init; }` to `UpdateLiveSharingLinkRequest`.
- **[missing_optional]** UpdateLiveSharingLinkRequest is missing property `expiresAtTime` (spec type=string).
  - Endpoints: `PATCH /live-shares`
  - Recommended fix: Add `[JsonPropertyName("expiresAtTime")] public string? ExpiresAtTime { get; init; }` to `UpdateLiveSharingLinkRequest`.

## LOW (8)

### `CreateLiveSharingLinkRequest` (request)

- **[extra_property]** CreateLiveSharingLinkRequest.entityId: present in SDK but not in spec inner schema.
  - Endpoints: `POST /live-shares`
  - Recommended fix: Remove `CreateLiveSharingLinkRequest.EntityId` (not in spec).
- **[extra_property]** CreateLiveSharingLinkRequest.expiresAt: present in SDK but not in spec inner schema.
  - Endpoints: `POST /live-shares`
  - Recommended fix: Remove `CreateLiveSharingLinkRequest.ExpiresAt` (not in spec).

### `LiveSharingLink` (response)

- **[extra_property]** LiveSharingLink.entityId (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Remove `LiveSharingLink.EntityId` (not in spec).
- **[extra_property]** LiveSharingLink.entityType (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Remove `LiveSharingLink.EntityType` (not in spec).
- **[extra_property]** LiveSharingLink.expiresAt (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Remove `LiveSharingLink.ExpiresAt` (not in spec).
- **[extra_property]** LiveSharingLink.url (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /live-shares`, `PATCH /live-shares`, `POST /live-shares`
  - Recommended fix: Remove `LiveSharingLink.Url` (not in spec).

### `UpdateLiveSharingLinkRequest` (request)

- **[extra_property]** UpdateLiveSharingLinkRequest.expiresAt: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /live-shares`
  - Recommended fix: Remove `UpdateLiveSharingLinkRequest.ExpiresAt` (not in spec).
- **[extra_property]** UpdateLiveSharingLinkRequest.id: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /live-shares`
  - Recommended fix: Remove `UpdateLiveSharingLinkRequest.Id` (not in spec).

