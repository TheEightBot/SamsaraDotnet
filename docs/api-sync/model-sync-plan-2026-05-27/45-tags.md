# Tags — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/45-tags.md`](../45-tags.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `Tag` | response | 0 | 0 | 1 | 0 |
| `UpdateTagRequest` | request | 0 | 0 | 1 | 0 |
| `CreateTagRequest` | request | 0 | 0 | 0 | 2 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=2, LOW=2  
**Total deduped findings**: 4

## MEDIUM (2)

### `Tag` (response)

- **[response_drift_optional]** Tag (response) missing property `parentTagId` (spec type=string). (affects 5 endpoints)
  - Endpoints: `GET /tags` (+4 more)
  - Recommended fix: Add `[JsonPropertyName("parentTagId")] public string? ParentTagId { get; init; }` to response record `Tag`.

### `UpdateTagRequest` (request)

- **[missing_optional]** UpdateTagRequest is missing property `externalIds` (spec type=object).
  - Endpoints: `PATCH /tags/{id}`
  - Recommended fix: Add `[JsonPropertyName("externalIds")] public object? ExternalIds { get; init; }` to `UpdateTagRequest`.

## LOW (2)

### `CreateTagRequest` (request)

- **[extra_property]** CreateTagRequest.externalIds: present in SDK but not in spec inner schema.
  - Endpoints: `PUT /tags/{id}`
  - Recommended fix: Remove `CreateTagRequest.ExternalIds` (not in spec).
- **[required_drift_over]** CreateTagRequest.name: SDK marks `required` but spec is optional.
  - Endpoints: `PUT /tags/{id}`
  - Recommended fix: Drop `required` on `CreateTagRequest.Name` (spec marks it optional) — make nullable.

