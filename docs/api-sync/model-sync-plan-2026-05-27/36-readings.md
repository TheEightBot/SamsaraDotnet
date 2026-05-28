# Readings — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/36-readings.md`](../36-readings.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `49caf9e` on 2026-05-27**

## Implementation notes

All HIGH and MEDIUM findings were applied. LOW response-side findings were
intentionally retained as nullable back-compat properties per the workflow
precedent established in `08-carrier-proposed-assignments`,
`13-driver-trailer-assignments`, `14-driver-vehicle-assignments`,
`28-live-sharing-links`, `29-location-and-speed`, and `30-maintenance` —
response-side flat-scalar conveniences kept with XML doc pointers to the
canonical spec fields rather than removed outright.

Files touched: `src/Samsara.Sdk/Models/Industrial/ReadingModels.cs`,
`src/Samsara.Sdk/Clients/Industrial/IReadingsClient.cs`,
`src/Samsara.Sdk/Clients/Industrial/ReadingsClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`.

**HIGH (9)**

- **`(no SDK type)` query — `GetHistoryAsync` required `entityType`**: added
  as a required positional `string entityType` parameter (no default),
  appended via `QueryBuilder.WithParams("entityType", entityType)`. Breaking
  signature change.
- **`(no SDK type)` query — `GetSnapshotAsync` required `entityType` and
  `readingIds`**: both added as required positional parameters
  (`string readingIds`, `string entityType` — readings ids first to match the
  spec parameter order). `GetSnapshotAsync()` was previously a no-argument
  call; this is a breaking signature change but necessary because the spec
  marks both query parameters REQUIRED.
- **`ReadingDefinition` response — required `category`, `ingestionEnabled`,
  `label`, `readingId`, `type`**: all five added as `required` non-nullable
  properties. `type` is modeled as `JsonElement` per the spec (which
  describes it as a free-form object containing `dataType`, `unit`,
  `enumValues`, `fields`, etc.). This matches the precedent in
  `29-location-and-speed` of preferring typed shapes for the well-known
  required fields while keeping a `JsonElement` escape hatch for documented
  free-form schemas.
- **`ReadingSnapshot` response — required `readingId`**: added as `required
  string ReadingId`. Spec marks REQUIRED on the inner schema.

**MEDIUM (19)**

- **Query parameters (10 missing across three endpoints)**:
  - `ListDefinitionsAsync`: added optional `ids`, `entityTypes`.
  - `GetHistoryAsync`: added optional `entityIds`, `externalIds`, `feed`
    (bool, lowercased), `includeExternalIds` (bool, lowercased).
  - `GetSnapshotAsync`: added optional `entityIds`, `externalIds`,
    `asOfTime` (RFC 3339 string), `includeExternalIds` (bool, lowercased).
  All append conditionally via `QueryBuilder.WithParams(...)`. Booleans use
  the established `?.ToString().ToLowerInvariant()` pattern from
  `AssetsClient.ListAsync`.
- **`ReadingDefinition` response — required drift `description`,
  `entityType`**: tightened both to non-nullable `required` per spec
  guarantee.
- **`ReadingDefinition` response — optional `enumValues`**: added as
  `IReadOnlyList<EnumValue>?` with a new nested `EnumValue` record
  (mirrors the spec's `EnumValueResponseBody` — required `label`,
  `symbol`). Preferred a typed record over the plan's literal
  `IReadOnlyList<object>?` recommendation to match the precedent in
  `14-driver-vehicle-assignments` and `29-location-and-speed`.
- **`ReadingHistory` response — required drift `entityId`**: tightened to
  non-nullable `required` per spec guarantee.
- **`ReadingHistory` response — optional `externalIds`, `happenedAtTime`**:
  added as `IReadOnlyDictionary<string, string>?` and `DateTimeOffset?`
  respectively. The spec describes `externalIds` as a free-form
  `additionalProperties: string` map, which matches the established
  `IReadOnlyDictionary<string, string>?` shape used across the SDK.
  `happenedAtTime` is the canonical RFC 3339 timestamp; modeled as
  `DateTimeOffset?` per the SDK convention.
- **`ReadingSnapshot` response — required drift `entityId`**: tightened to
  non-nullable `required` per spec guarantee.
- **`ReadingSnapshot` response — optional `externalIds`, `happenedAtTime`**:
  added with the same shapes as on `ReadingHistory`.

**LOW (9)**

- **`ReadingDefinition.id`, `ReadingDefinition.name`,
  `ReadingDefinition.dataType`, `ReadingDefinition.units` (response)**: kept
  as nullable back-compat properties with XML doc comments noting they are
  not in the spec inner schema and pointing callers to the canonical spec
  fields (`ReadingId` / `Label` / `Type`). The previous `required string Id`
  was dropped to nullable since the spec response never emits an `id` field
  (only `readingId`) and the `required` modifier would otherwise prevent
  deserialization.
- **`ReadingHistory.id`, `ReadingHistory.time` (response)**: kept as nullable
  back-compat with XML doc pointing callers to `HappenedAtTime` for the
  canonical timestamp.
- **`ReadingSnapshot.id`, `ReadingSnapshot.entityName`,
  `ReadingSnapshot.time` (response)**: kept as nullable back-compat with XML
  doc pointing callers to `ReadingId` / a separate entity lookup for the
  name / `HappenedAtTime`.

Verification: `dotnet build` green (0 warnings, 0 errors); all 59 unit
tests pass; `python3 tools/check-sdk-sync.py` exits 0 (matched=323/323,
mismatched=0, unresolved=0, not implemented=0).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `ReadingDefinition` | response | 0 | 5 | 3 | 4 |
| `(no SDK type)` | query | 0 | 3 | 10 | 0 |
| `ReadingSnapshot` | response | 0 | 1 | 3 | 3 |
| `ReadingHistory` | response | 0 | 0 | 3 | 2 |

**Counts**: CRITICAL=0, HIGH=9, MEDIUM=19, LOW=9  
**Total deduped findings**: 37

## HIGH (9)

### `(no SDK type)` (query)

- **[missing_required_query]** GetHistoryAsync (GET /readings/history) is missing query parameter `entityType` (spec REQUIRED, type=string).
  - Endpoints: `GET /readings/history`
  - Recommended fix: Add a required parameter (e.g. `string entityType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("entityType", ...)`.
- **[missing_required_query]** GetSnapshotAsync (GET /readings/latest) is missing query parameter `entityType` (spec REQUIRED, type=string).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add a required parameter (e.g. `string entityType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("entityType", ...)`.
- **[missing_required_query]** GetSnapshotAsync (GET /readings/latest) is missing query parameter `readingIds` (spec REQUIRED, type=string).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add a required parameter (e.g. `string readingIds` , no default) to the SDK method and append it via `QueryBuilder.WithParams("readingIds", ...)`.

### `ReadingDefinition` (response)

- **[response_drift_required]** ReadingDefinition (response) missing REQUIRED property `category` (spec type=string).
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Add `[JsonPropertyName("category")] public string Category { get; init; }` to response record `ReadingDefinition` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** ReadingDefinition (response) missing REQUIRED property `ingestionEnabled` (spec type=boolean).
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Add `[JsonPropertyName("ingestionEnabled")] public bool IngestionEnabled { get; init; }` to response record `ReadingDefinition` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** ReadingDefinition (response) missing REQUIRED property `label` (spec type=string).
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Add `[JsonPropertyName("label")] public string Label { get; init; }` to response record `ReadingDefinition` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** ReadingDefinition (response) missing REQUIRED property `readingId` (spec type=string).
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Add `[JsonPropertyName("readingId")] public string ReadingId { get; init; }` to response record `ReadingDefinition` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** ReadingDefinition (response) missing REQUIRED property `type` (spec type=object).
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Add `[JsonPropertyName("type")] public object Type { get; init; }` to response record `ReadingDefinition` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `ReadingSnapshot` (response)

- **[response_drift_required]** ReadingSnapshot (response) missing REQUIRED property `readingId` (spec type=string).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add `[JsonPropertyName("readingId")] public string ReadingId { get; init; }` to response record `ReadingSnapshot` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (19)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetSnapshotAsync (GET /readings/latest) is missing query parameter `asOfTime` (spec optional, type=string).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add an optional parameter `string? asOfTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetHistoryAsync (GET /readings/history) is missing query parameter `entityIds` (spec optional, type=string).
  - Endpoints: `GET /readings/history`
  - Recommended fix: Add an optional parameter `string? entityIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSnapshotAsync (GET /readings/latest) is missing query parameter `entityIds` (spec optional, type=string).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add an optional parameter `string? entityIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDefinitionsAsync (GET /readings/definitions) is missing query parameter `entityTypes` (spec optional, type=string).
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Add an optional parameter `string? entityTypes = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetHistoryAsync (GET /readings/history) is missing query parameter `externalIds` (spec optional, type=string).
  - Endpoints: `GET /readings/history`
  - Recommended fix: Add an optional parameter `string? externalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSnapshotAsync (GET /readings/latest) is missing query parameter `externalIds` (spec optional, type=string).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add an optional parameter `string? externalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetHistoryAsync (GET /readings/history) is missing query parameter `feed` (spec optional, type=boolean).
  - Endpoints: `GET /readings/history`
  - Recommended fix: Add an optional parameter `bool? feed = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListDefinitionsAsync (GET /readings/definitions) is missing query parameter `ids` (spec optional, type=string).
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Add an optional parameter `string? ids = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetHistoryAsync (GET /readings/history) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /readings/history`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetSnapshotAsync (GET /readings/latest) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `ReadingDefinition` (response)

- **[response_drift_optional]** ReadingDefinition (response) missing property `enumValues` (spec type=array).
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Add `[JsonPropertyName("enumValues")] public IReadOnlyList<object>? EnumValues { get; init; }` to response record `ReadingDefinition`.
- **[response_required_drift]** ReadingDefinition.description (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Tighten `ReadingDefinition.Description` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** ReadingDefinition.entityType (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Tighten `ReadingDefinition.EntityType` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `ReadingHistory` (response)

- **[response_drift_optional]** ReadingHistory (response) missing property `externalIds` (spec type=object).
  - Endpoints: `GET /readings/history`
  - Recommended fix: Add `[JsonPropertyName("externalIds")] public object? ExternalIds { get; init; }` to response record `ReadingHistory`.
- **[response_drift_optional]** ReadingHistory (response) missing property `happenedAtTime` (spec type=string).
  - Endpoints: `GET /readings/history`
  - Recommended fix: Add `[JsonPropertyName("happenedAtTime")] public string? HappenedAtTime { get; init; }` to response record `ReadingHistory`.
- **[response_required_drift]** ReadingHistory.entityId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /readings/history`
  - Recommended fix: Tighten `ReadingHistory.EntityId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `ReadingSnapshot` (response)

- **[response_drift_optional]** ReadingSnapshot (response) missing property `externalIds` (spec type=object).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add `[JsonPropertyName("externalIds")] public object? ExternalIds { get; init; }` to response record `ReadingSnapshot`.
- **[response_drift_optional]** ReadingSnapshot (response) missing property `happenedAtTime` (spec type=string).
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Add `[JsonPropertyName("happenedAtTime")] public string? HappenedAtTime { get; init; }` to response record `ReadingSnapshot`.
- **[response_required_drift]** ReadingSnapshot.entityId (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Tighten `ReadingSnapshot.EntityId` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (9)

### `ReadingDefinition` (response)

- **[extra_property]** ReadingDefinition.dataType (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Remove `ReadingDefinition.DataType` (not in spec).
- **[extra_property]** ReadingDefinition.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Remove `ReadingDefinition.Id` (not in spec).
- **[extra_property]** ReadingDefinition.name (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Remove `ReadingDefinition.Name` (not in spec).
- **[extra_property]** ReadingDefinition.units (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/definitions`
  - Recommended fix: Remove `ReadingDefinition.Units` (not in spec).

### `ReadingHistory` (response)

- **[extra_property]** ReadingHistory.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/history`
  - Recommended fix: Remove `ReadingHistory.Id` (not in spec).
- **[extra_property]** ReadingHistory.time (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/history`
  - Recommended fix: Remove `ReadingHistory.Time` (not in spec).

### `ReadingSnapshot` (response)

- **[extra_property]** ReadingSnapshot.entityName (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Remove `ReadingSnapshot.EntityName` (not in spec).
- **[extra_property]** ReadingSnapshot.id (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Remove `ReadingSnapshot.Id` (not in spec).
- **[extra_property]** ReadingSnapshot.time (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /readings/latest`
  - Recommended fix: Remove `ReadingSnapshot.Time` (not in spec).

