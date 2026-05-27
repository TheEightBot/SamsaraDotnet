# Readings — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/36-readings.md`](../36-readings.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

