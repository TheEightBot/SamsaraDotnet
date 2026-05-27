# Attributes — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/04-attributes.md`](../04-attributes.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 3 | 0 | 0 |
| `AttributeDefinition` | response | 0 | 0 | 3 | 1 |
| `CreateAttributeRequest` | request | 0 | 0 | 2 | 0 |
| `UpdateAttributeRequest` | request | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=3, MEDIUM=6, LOW=1  
**Total deduped findings**: 10

## HIGH (3)

### `(no SDK type)` (query)

- **[missing_required_query]** ListAsync (GET /attributes) is missing query parameter `entityType` (spec REQUIRED, type=string).
  - Endpoints: `GET /attributes`
  - Recommended fix: Add a required parameter (e.g. `string entityType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("entityType", ...)`.
- **[missing_required_query]** GetAsync (GET /attributes/{id}) is missing query parameter `entityType` (spec REQUIRED, type=string).
  - Endpoints: `GET /attributes/{id}`
  - Recommended fix: Add a required parameter (e.g. `string entityType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("entityType", ...)`.
- **[missing_required_query]** DeleteAsync (DELETE /attributes/{id}) is missing query parameter `entityType` (spec REQUIRED, type=string).
  - Endpoints: `DELETE /attributes/{id}`
  - Recommended fix: Add a required parameter (e.g. `string entityType` , no default) to the SDK method and append it via `QueryBuilder.WithParams("entityType", ...)`.

## MEDIUM (6)

### `AttributeDefinition` (response)

- **[response_drift_optional]** AttributeDefinition (response) missing property `unit` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /attributes`, `GET /attributes/{id}`, `PATCH /attributes/{id}`, `POST /attributes`
  - Recommended fix: Add `[JsonPropertyName("unit")] public string? Unit { get; init; }` to response record `AttributeDefinition`.
- **[response_drift_optional]** AttributeDefinition (response) missing property `values` (spec type=array). (affects 4 endpoints)
  - Endpoints: `GET /attributes`, `GET /attributes/{id}`, `PATCH /attributes/{id}`, `POST /attributes`
  - Recommended fix: Add `[JsonPropertyName("values")] public IReadOnlyList<object>? Values { get; init; }` to response record `AttributeDefinition`.
- **[response_required_drift]** AttributeDefinition.entities (response): spec marks REQUIRED but SDK exposes as nullable (`IReadOnlyList<AttributeEntity>?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /attributes/{id}`, `PATCH /attributes/{id}`, `POST /attributes`
  - Recommended fix: Tighten `AttributeDefinition.Entities` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `CreateAttributeRequest` (request)

- **[missing_optional]** CreateAttributeRequest is missing property `entities` (spec type=array).
  - Endpoints: `POST /attributes`
  - Recommended fix: Add `[JsonPropertyName("entities")] public IReadOnlyList<object>? Entities { get; init; }` to `CreateAttributeRequest`.
- **[missing_optional]** CreateAttributeRequest is missing property `unit` (spec type=string enum=['NO_UNIT', 'METER', 'POUND', 'TON', 'KILOGRAM', 'INCH', 'FOOT', 'GALLON', 'LITER', 'BARREL', 'POUND_PER_SQUARE_INCH', 'BAR', 'KILOPASCAL', 'FAHRENHEIT', 'CELSIUS', 'USD', 'CAD', 'EUR', 'GBP', 'MXN', 'HOUR', 'MINUTE', 'DAY', 'MILE_PER_HOUR', 'KILOMETER_PER_HOUR', 'HORSEPOWER', 'KILOWATT', 'BTU_PER_HOUR', 'KILOWATT_HOUR', 'BTU', 'HERTZ', 'REVOLUTION_PER_MINUTE', 'DECIBEL']).
  - Endpoints: `POST /attributes`
  - Recommended fix: Add `[JsonPropertyName("unit")] public string? Unit { get; init; }` to `CreateAttributeRequest`.

### `UpdateAttributeRequest` (request)

- **[missing_optional]** UpdateAttributeRequest is missing property `entities` (spec type=array).
  - Endpoints: `PATCH /attributes/{id}`
  - Recommended fix: Add `[JsonPropertyName("entities")] public IReadOnlyList<object>? Entities { get; init; }` to `UpdateAttributeRequest`.

## LOW (1)

### `AttributeDefinition` (response)

- **[extra_property]** AttributeDefinition.entities (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /attributes`
  - Recommended fix: Remove `AttributeDefinition.Entities` (not in spec).

