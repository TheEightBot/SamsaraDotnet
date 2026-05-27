# Addresses — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/01-addresses.md`](../01-addresses.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `CreateHubRequest` | request | 0 | 2 | 3 | 2 |
| `Hub` | response | 0 | 0 | 7 | 0 |
| `UpdateHubRequest` | request | 0 | 0 | 4 | 0 |
| `(no SDK type)` | query | 0 | 0 | 3 | 0 |
| `Address` | response | 0 | 0 | 2 | 0 |

**Counts**: CRITICAL=0, HIGH=2, MEDIUM=19, LOW=2  
**Total deduped findings**: 23

## HIGH (2)

### `CreateHubRequest` (request)

- **[missing_required]** CreateHubRequest is missing REQUIRED property `geofence` (spec type=object).
  - Endpoints: `POST /addresses`
  - Recommended fix: Add `[JsonPropertyName("geofence")] public required object Geofence { get; init; }` to `CreateHubRequest`.
- **[required_drift]** CreateHubRequest.formattedAddress: spec marks REQUIRED but SDK property is not `required`.
  - Endpoints: `POST /addresses`
  - Recommended fix: Mark `CreateHubRequest.FormattedAddress` as `required` (drop the `?` nullable marker).

## MEDIUM (19)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /addresses) is missing query parameter `createdAfterTime` (spec optional, type=string). (affects 2 endpoints)
  - Endpoints: `GET /addresses`
  - Recommended fix: Add an optional parameter `string? createdAfterTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /addresses) is missing query parameter `parentTagIds` (spec optional, type=array). (affects 2 endpoints)
  - Endpoints: `GET /addresses`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? parentTagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /addresses) is missing query parameter `tagIds` (spec optional, type=array). (affects 2 endpoints)
  - Endpoints: `GET /addresses`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? tagIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `Address` (response)

- **[response_required_drift]** Address.formattedAddress (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /addresses`, `GET /addresses/{id}`, `PATCH /addresses/{id}`, `POST /addresses`
  - Recommended fix: Tighten `Address.FormattedAddress` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Address.geofence (response): spec marks REQUIRED but SDK exposes as nullable (`Geofence?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /addresses`, `GET /addresses/{id}`, `PATCH /addresses/{id}`, `POST /addresses`
  - Recommended fix: Tighten `Address.Geofence` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `CreateHubRequest` (request)

- **[missing_optional]** CreateHubRequest is missing property `addressTypes` (spec type=array).
  - Endpoints: `POST /addresses`
  - Recommended fix: Add `[JsonPropertyName("addressTypes")] public IReadOnlyList<object>? AddressTypes { get; init; }` to `CreateHubRequest`.
- **[missing_optional]** CreateHubRequest is missing property `contactIds` (spec type=array).
  - Endpoints: `POST /addresses`
  - Recommended fix: Add `[JsonPropertyName("contactIds")] public IReadOnlyList<string>? ContactIds { get; init; }` to `CreateHubRequest`.
- **[missing_optional]** CreateHubRequest is missing property `notes` (spec type=string).
  - Endpoints: `POST /addresses`
  - Recommended fix: Add `[JsonPropertyName("notes")] public string? Notes { get; init; }` to `CreateHubRequest`.

### `Hub` (response)

- **[response_drift_optional]** Hub (response) missing property `addressTypes` (spec type=array). (affects 4 endpoints)
  - Endpoints: `GET /addresses`, `GET /addresses/{id}`, `PATCH /addresses/{id}`, `POST /addresses`
  - Recommended fix: Add `[JsonPropertyName("addressTypes")] public IReadOnlyList<object>? AddressTypes { get; init; }` to response record `Hub`.
- **[response_drift_optional]** Hub (response) missing property `contacts` (spec type=array). (affects 4 endpoints)
  - Endpoints: `GET /addresses`, `GET /addresses/{id}`, `PATCH /addresses/{id}`, `POST /addresses`
  - Recommended fix: Add `[JsonPropertyName("contacts")] public IReadOnlyList<object>? Contacts { get; init; }` to response record `Hub`.
- **[response_drift_optional]** Hub (response) missing property `createdAtTime` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /addresses`, `GET /addresses/{id}`, `PATCH /addresses/{id}`, `POST /addresses`
  - Recommended fix: Add `[JsonPropertyName("createdAtTime")] public DateTimeOffset? CreatedAtTime { get; init; }` to response record `Hub`.
- **[response_drift_optional]** Hub (response) missing property `notes` (spec type=string). (affects 4 endpoints)
  - Endpoints: `GET /addresses`, `GET /addresses/{id}`, `PATCH /addresses/{id}`, `POST /addresses`
  - Recommended fix: Add `[JsonPropertyName("notes")] public string? Notes { get; init; }` to response record `Hub`.
- **[response_required_drift]** Hub.formattedAddress (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /addresses`, `GET /addresses/{id}`, `PATCH /addresses/{id}`, `POST /addresses`
  - Recommended fix: Tighten `Hub.FormattedAddress` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Hub.geofence (response): spec marks REQUIRED but SDK exposes as nullable (`Addresses.Geofence?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /addresses`, `GET /addresses/{id}`, `PATCH /addresses/{id}`, `POST /addresses`
  - Recommended fix: Tighten `Hub.Geofence` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Hub.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 5 endpoints)
  - Endpoints: `GET /addresses` (+4 more)
  - Recommended fix: Tighten `Hub.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateHubRequest` (request)

- **[missing_optional]** UpdateHubRequest is missing property `addressTypes` (spec type=array).
  - Endpoints: `PATCH /addresses/{id}`
  - Recommended fix: Add `[JsonPropertyName("addressTypes")] public IReadOnlyList<object>? AddressTypes { get; init; }` to `UpdateHubRequest`.
- **[missing_optional]** UpdateHubRequest is missing property `contactIds` (spec type=array).
  - Endpoints: `PATCH /addresses/{id}`
  - Recommended fix: Add `[JsonPropertyName("contactIds")] public IReadOnlyList<string>? ContactIds { get; init; }` to `UpdateHubRequest`.
- **[missing_optional]** UpdateHubRequest is missing property `geofence` (spec type=object).
  - Endpoints: `PATCH /addresses/{id}`
  - Recommended fix: Add `[JsonPropertyName("geofence")] public object? Geofence { get; init; }` to `UpdateHubRequest`.
- **[missing_optional]** UpdateHubRequest is missing property `notes` (spec type=string).
  - Endpoints: `PATCH /addresses/{id}`
  - Recommended fix: Add `[JsonPropertyName("notes")] public string? Notes { get; init; }` to `UpdateHubRequest`.

## LOW (2)

### `CreateHubRequest` (request)

- **[required_drift_over]** CreateHubRequest.latitude: SDK marks `required` but spec is optional.
  - Endpoints: `POST /addresses`
  - Recommended fix: Drop `required` on `CreateHubRequest.Latitude` (spec marks it optional) — make nullable.
- **[required_drift_over]** CreateHubRequest.longitude: SDK marks `required` but spec is optional.
  - Endpoints: `POST /addresses`
  - Recommended fix: Drop `required` on `CreateHubRequest.Longitude` (spec marks it optional) — make nullable.

