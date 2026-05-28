# Live Sharing Links — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/28-live-sharing-links.md`](../28-live-sharing-links.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `8bcde86` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. LOW findings were treated
consistently with the workflow precedent established in
`08-carrier-proposed-assignments`, `13-driver-trailer-assignments`, and
`14-driver-vehicle-assignments`: response-side flat-scalar conveniences
preserved as nullable back-compat; request-side spec-absent body fields
removed because the API silently ignores them and they mislead callers.

Files touched: `src/Samsara.Sdk/Models/Fleet/LiveSharingModels.cs`,
`src/Samsara.Sdk/Clients/Fleet/ILiveSharingLinksClient.cs`,
`src/Samsara.Sdk/Clients/Fleet/LiveSharingLinksClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`.

**HIGH (3)**

- **`(no SDK type)` query — `id` REQUIRED on PATCH**:
  `ILiveSharingLinksClient.UpdateAsync` now takes `string id` separately and
  appends it via `QueryBuilder.WithParams(BasePath, ("id", id))` — same
  pattern as the prior `13-driver-trailer-assignments` and
  `14-driver-vehicle-assignments` implementations. This is a breaking
  signature change for direct callers of the previous body-only
  `UpdateAsync(UpdateLiveSharingLinkRequest, ...)`.
- **`LiveSharingLink` response — REQUIRED `liveSharingUrl`**: added as
  `required string LiveSharingUrl` with `[JsonPropertyName("liveSharingUrl")]`.
  This is the canonical spec name; the legacy `Url` flat scalar remains as
  nullable back-compat.
- **`UpdateLiveSharingLinkRequest.name` REQUIRED**: tightened from
  `string?` to `required string` matching the spec body schema.

**MEDIUM (16)**

- **`(no SDK type)` query — optional `ids`**: added as
  `IReadOnlyList<string>? ids = null` on `ListAsync`, serialized via
  `string.Join(",", ids)` per the precedent in `AssetsClient.ListAsync`.
- **`(no SDK type)` query — optional `type`**: added as `string? type = null`
  on `ListAsync` and appended via `QueryBuilder.WithParams("type", type)`.
- **`CreateLiveSharingLinkRequest` — optional `description`,
  `expiresAtTime`, `assetsLocationLinkConfig`,
  `assetsNearLocationLinkConfig`, `assetsOnRouteLinkConfig`**: added as
  typed properties. `description`/`expiresAtTime` are nullable `string?` per
  spec (RFC 3339 string with no `format` keyword). The three `*LinkConfig`
  properties are typed via dedicated request records to match the spec's
  request-side schemas — `CreateAssetsLocationLinkConfig` mirrors
  `AssetsLocationLinkRequestConfigObject` (with `tagIds` instead of the
  response-side resolved `tags`); the near-location and on-route configs
  reuse the same nested records as the response side (the request and
  response shapes are identical for those two).
- **`LiveSharingLink` response — optional `description`, `expiresAtTime`,
  `assetsLocationLinkConfig`, `assetsNearLocationLinkConfig`,
  `assetsOnRouteLinkConfig`**: added as nullable typed properties.
  `assetsLocationLinkConfig` is typed via
  `LiveSharingLinkAssetsLocationLinkConfig`, which mirrors the spec's
  `AssetsLocationLinkResponseConfigObjectResponseBody` (with the resolved
  `tags` array typed as `LiveSharingLinkTag` — a minified tag record that
  mirrors `GoaTagTinyResponseResponseBody`). The `location` sub-object is
  typed as `LiveSharingLinkLocation` mirroring the spec's address-details
  schema.
- **`LiveSharingLink.name` / `LiveSharingLink.type` REQUIRED**: tightened
  from `string?` to `required string` for each — both are spec REQUIRED on
  the response inner schema.
- **`UpdateLiveSharingLinkRequest` — optional `description`,
  `expiresAtTime`**: added as nullable `string?` per spec.

**LOW (8)**

- **`LiveSharingLink.url`, `LiveSharingLink.expiresAt`,
  `LiveSharingLink.entityId`, `LiveSharingLink.entityType` (response)**:
  kept as nullable back-compat properties with XML doc comments noting they
  are not in the spec inner schema and pointing callers to the canonical
  spec fields (`liveSharingUrl`, `expiresAtTime`) or typed
  `*LinkConfig` records (for `entityId`/`entityType`). Same approach as
  `08-carrier-proposed-assignments`, `13-driver-trailer-assignments`, and
  `14-driver-vehicle-assignments`.
- **`CreateLiveSharingLinkRequest.entityId`,
  `CreateLiveSharingLinkRequest.expiresAt` (request)**: REMOVED. The spec
  request body does not declare these. `entityId` was previously declared
  `required` in the SDK — its removal is a breaking change for direct
  callers, but the body field was never honored by the API (the typed
  `*LinkConfig.assetId` / `addressId` / `recurringRouteId` paths are the
  real wire shape).
- **`UpdateLiveSharingLinkRequest.id`,
  `UpdateLiveSharingLinkRequest.expiresAt` (request)**: REMOVED. The `id`
  is now passed as the spec-required query parameter on
  `UpdateAsync(string id, ...)`; placing it in the body was incorrect.
  `expiresAt` is not in the spec body — the legacy alias was unused.

Verification: `dotnet build` green (0 warnings, 0 errors); all 59 unit
tests pass; `python3 tools/check-sdk-sync.py` exits 0 (matched=323/323,
mismatched=0, unresolved=0, not implemented=0).

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

