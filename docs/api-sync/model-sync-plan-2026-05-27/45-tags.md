# Tags — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/45-tags.md`](../45-tags.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `<pending>` on 2026-05-27**

## Implementation notes

All four findings were applied within the single file
`src/Samsara.Sdk/Models/Tags/TagModels.cs`; no client/interface/CLI/test/
JsonContext changes were needed (the affected types are already registered and
the methods carry no new query params).

**MEDIUM (2)**

- **`Tag` (response) — `parentTagId`**: added as `string?`. It coexists with the
  existing `parentTag` `EntityReference?` object — the full `GET /tags` response
  uses the `parentTag` object while the tiny/abbreviated tag response shapes
  return the flat `parentTagId` string, so both are kept.
- **`UpdateTagRequest` (request) — `externalIds`**: added as
  `IReadOnlyDictionary<string, string>?` rather than the plan's generic `object?`
  placeholder, to match its siblings `CreateTagRequest.ExternalIds` and
  `Tag.ExternalIds` in the same file.

**LOW (2)**

- **`CreateTagRequest.name` (`required_drift_over`)**: dropped `required` and made
  it nullable (`string?`) since the spec marks `name` optional on this request —
  the established handling for `required_drift_over` on request props (cf.
  `CreateHubRequest` in `01-addresses`). Backward-compatible: existing
  initializer usages still compile.
- **`CreateTagRequest.externalIds` (`extra_property`)**: retained as a nullable
  back-compat extra per the precedent in `40-safety-scores` rather than removed;
  annotated with a brief `// retained for back-compat` comment.

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

