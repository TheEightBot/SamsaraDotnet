# Hubs — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/21-hubs.md`](../21-hubs.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `a48fb39` on 2026-05-27**

## Implementation notes

All CRITICAL, HIGH, and MEDIUM findings were applied. The two CRITICAL
wrapper-drift bugs (`POST /hub/locations` and `PATCH /hub/location/{id}`)
were the load-bearing items in this plan — both endpoints were
runtime-broken because the SDK was posting an unwrapped body where the
spec expected a `{ data: ... }` envelope.

CRITICAL fixes:

- **`POST /hub/locations`** — introduced new envelope record
  `CreateHubLocationsRequest { data: IReadOnlyList<CreateHubLocationInput> }`.
  The prior `CreateHubLocationRequest` was renamed to
  `CreateHubLocationInput` and tightened to mark `address`,
  `customerLocationId`, `hubId`, `isDepot`, and `name` as `required` per the
  spec. The five missing optional fields (`driverInstructions`,
  `plannerNotes`, `serviceTimeSeconds`, `serviceWindows`, `skillsRequired`)
  were added. The spec-absent `notes` field was removed per the request-side
  precedent for spec-absent extras. The client method
  `CreateLocationAsync` now takes the envelope.
- **`PATCH /hub/location/{id}`** — introduced new envelope record
  `UpdateHubLocationEnvelopeRequest { data: UpdateHubLocationRequest }`.
  The inner `UpdateHubLocationRequest` gained the nine missing optional
  fields (`customerLocationId`, `driverInstructions`, `isDepot`, `latitude`,
  `longitude`, `plannerNotes`, `serviceTimeSeconds`, `serviceWindows`,
  `skillsRequired`) and dropped the spec-absent `notes` field. The client
  method `UpdateLocationAsync` now takes the envelope.

HIGH:

- All four list endpoints (`ListCapacitiesAsync`,
  `ListCustomPropertiesAsync`, `ListLocationsAsync`, `ListSkillsAsync`)
  now require `hubId` (spec REQUIRED) as the first parameter. The optional
  filter surface was extended at the same time so the query parameters
  weren't half-implemented (see MEDIUM below).
- `ListHubsAsync` (`GET /hubs`) accepts `hubIds`, `startTime`, and `endTime`
  as optional parameters.
- Spec-REQUIRED response fields tightened to non-nullable `required` on
  `Hub` (`timeZone`, `createdAt`, `updatedAt`), `HubLocation` (`address`,
  `name`, `customerLocationId`, `hubId`, `isDepot`, `latitude`, `longitude`,
  `driverInstructions`, `plannerNotes`, `serviceTimeSeconds`,
  `serviceWindows`, `skillsRequired`, `createdAt`, `updatedAt`),
  `HubCapacity` (`id`, `name`, `unit`, `createdAt`, `updatedAt`),
  `HubCustomProperty` (`hubId`, `name`, `csvColumns`, `createdAt`,
  `updatedAt`), and `HubSkill` (`hubId`, `name`, `createdAt`, `updatedAt`).
  Note: `serviceWindows` and `skillsRequired` use `IReadOnlyList<object>`
  rather than the typed inner records (`ServiceWindowObjectResponseBody` /
  `SkillObjectResponseBody`) to stay aligned with the plan's recommended
  fix; tightening to typed nested records can follow in a future iteration.

MEDIUM:

- `ListCapacitiesAsync` adds optional `capacityIds`, `capacityNames`,
  `startTime`, `endTime`.
- `ListCustomPropertiesAsync` adds optional `customPropertyIds`,
  `customPropertyNames`, `startTime`, `endTime`.
- `ListLocationsAsync` adds optional `locationIds`, `customerLocationIds`,
  `startTime`, `endTime`.
- `ListSkillsAsync` adds optional `skillIds`, `skillNames`, `startTime`,
  `endTime`.
- `HubCustomProperty.Name`, `HubLocation.Address/Latitude/Longitude/Name`,
  and `HubSkill.Name` tightened to non-nullable `required` (already covered
  by the HIGH response-drift work above).

LOW (conservative — workflow precedent):

- Response-side spec-absent fields retained as nullable back-compat
  properties: `Hub.externalIds`, `Hub.formattedAddress`, `Hub.geofence`,
  `Hub.latitude`, `Hub.longitude`, `Hub.tags`, `HubCapacity.capacity`,
  `HubCapacity.timeSlot`, `HubCapacity.usedCapacity`,
  `HubCustomProperty.type`, and `HubLocation.notes`. These do not appear in
  the spec inner schemas but may be returned by the API and removing them
  would be a breaking change for consumers.
- Request-side spec-absent fields removed:
  `CreateHubLocationInput.notes` (the rename target) and
  `UpdateHubLocationRequest.notes`. Request-side spec-absent fields are
  removed per the precedent established in earlier domain syncs because
  sending them risks API rejection.

Files touched:
`src/Samsara.Sdk/Models/Routes/HubModels.cs`,
`src/Samsara.Sdk/Clients/Routing/HubsClient.cs`,
`src/Samsara.Sdk/Clients/Routing/IHubsClient.cs`,
`src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs`,
`docs/api-sync/21-hubs.md`,
`CHANGELOG.md`.

Verification: `dotnet build` green (0 warnings, 0 errors), 59/59 unit
tests pass, and `tools/check-sdk-sync.py --fail-on-mismatch` exits 0
(323 SDK endpoints matched, 0 mismatched).



## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `CreateHubLocationRequest` | request | 1 | 4 | 5 | 1 |
| `UpdateHubLocationRequest` | request | 1 | 0 | 9 | 1 |
| `HubLocation` | response | 0 | 10 | 4 | 1 |
| `HubCapacity` | response | 0 | 5 | 0 | 3 |
| `(no SDK type)` | query | 0 | 4 | 19 | 0 |
| `HubCustomProperty` | response | 0 | 4 | 1 | 1 |
| `HubSkill` | response | 0 | 3 | 1 | 0 |
| `Hub` | response | 0 | 3 | 0 | 6 |

**Counts**: CRITICAL=2, HIGH=33, MEDIUM=39, LOW=13  
**Total deduped findings**: 87

## CRITICAL (2)

### `CreateHubLocationRequest` (request)

- **[wrapper_drift]** SDK posts CreateHubLocationRequest as the body, but spec expects array wrapped in `{ data }`. Inner schema requires: ['address', 'customerLocationId', 'hubId', 'isDepot', 'name'].
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Wrap the post body in `{ data: T[] }`. Introduce an envelope record (e.g. `CreateHubLocationsRequest { data: IReadOnlyList<CreateHubLocationInput> }`) and rename the current `CreateHubLocationRequest` to `CreateHubLocationInput`. Each item must include `address`, `customerLocationId`, `hubId`, `isDepot`, `name` as `required`.

### `UpdateHubLocationRequest` (request)

- **[wrapper_drift]** SDK posts UpdateHubLocationRequest as the body, but spec expects object wrapped in `{ data }`. Inner schema requires: [].
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Wrap the body in `{ data: T }`. Introduce an envelope record `UpdateHubLocationEnvelopeRequest { data: UpdateHubLocationRequest }` and update the client to post the envelope. Required inner fields: (none specified — entire data object optional).

## HIGH (33)

### `(no SDK type)` (query)

- **[missing_required_query]** ListCapacitiesAsync (GET /hub/capacities) is missing query parameter `hubId` (spec REQUIRED, type=string).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add a required parameter (e.g. `string hubId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("hubId", ...)`.
- **[missing_required_query]** ListCustomPropertiesAsync (GET /hub/customProperties) is missing query parameter `hubId` (spec REQUIRED, type=string).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add a required parameter (e.g. `string hubId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("hubId", ...)`.
- **[missing_required_query]** ListLocationsAsync (GET /hub/locations) is missing query parameter `hubId` (spec REQUIRED, type=string).
  - Endpoints: `GET /hub/locations`
  - Recommended fix: Add a required parameter (e.g. `string hubId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("hubId", ...)`.
- **[missing_required_query]** ListSkillsAsync (GET /hub/skills) is missing query parameter `hubId` (spec REQUIRED, type=string).
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Add a required parameter (e.g. `string hubId` , no default) to the SDK method and append it via `QueryBuilder.WithParams("hubId", ...)`.

### `CreateHubLocationRequest` (request)

- **[missing_required]** CreateHubLocationRequest is missing REQUIRED property `customerLocationId` (spec type=string).
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("customerLocationId")] public required string CustomerLocationId { get; init; }` to `CreateHubLocationRequest`.
- **[missing_required]** CreateHubLocationRequest is missing REQUIRED property `hubId` (spec type=string).
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("hubId")] public required string HubId { get; init; }` to `CreateHubLocationRequest`.
- **[missing_required]** CreateHubLocationRequest is missing REQUIRED property `isDepot` (spec type=boolean).
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("isDepot")] public required bool IsDepot { get; init; }` to `CreateHubLocationRequest`.
- **[required_drift]** CreateHubLocationRequest.address: spec marks REQUIRED but SDK property is not `required`.
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Mark `CreateHubLocationRequest.Address` as `required` (drop the `?` nullable marker).

### `Hub` (response)

- **[response_drift_required]** Hub (response) missing REQUIRED property `createdAt` (spec type=string/date-time).
  - Endpoints: `GET /hubs`
  - Recommended fix: Add `[JsonPropertyName("createdAt")] public DateTimeOffset CreatedAt { get; init; }` to response record `Hub` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Hub (response) missing REQUIRED property `timeZone` (spec type=string).
  - Endpoints: `GET /hubs`
  - Recommended fix: Add `[JsonPropertyName("timeZone")] public string TimeZone { get; init; }` to response record `Hub` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Hub (response) missing REQUIRED property `updatedAt` (spec type=string/date-time).
  - Endpoints: `GET /hubs`
  - Recommended fix: Add `[JsonPropertyName("updatedAt")] public DateTimeOffset UpdatedAt { get; init; }` to response record `Hub` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `HubCapacity` (response)

- **[response_drift_required]** HubCapacity (response) missing REQUIRED property `createdAt` (spec type=string/date-time).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add `[JsonPropertyName("createdAt")] public DateTimeOffset CreatedAt { get; init; }` to response record `HubCapacity` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubCapacity (response) missing REQUIRED property `id` (spec type=string).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add `[JsonPropertyName("id")] public string Id { get; init; }` to response record `HubCapacity` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubCapacity (response) missing REQUIRED property `name` (spec type=string).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add `[JsonPropertyName("name")] public string Name { get; init; }` to response record `HubCapacity` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubCapacity (response) missing REQUIRED property `unit` (spec type=string).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add `[JsonPropertyName("unit")] public string Unit { get; init; }` to response record `HubCapacity` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubCapacity (response) missing REQUIRED property `updatedAt` (spec type=string/date-time).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add `[JsonPropertyName("updatedAt")] public DateTimeOffset UpdatedAt { get; init; }` to response record `HubCapacity` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `HubCustomProperty` (response)

- **[response_drift_required]** HubCustomProperty (response) missing REQUIRED property `createdAt` (spec type=string/date-time).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add `[JsonPropertyName("createdAt")] public DateTimeOffset CreatedAt { get; init; }` to response record `HubCustomProperty` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubCustomProperty (response) missing REQUIRED property `csvColumns` (spec type=string).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add `[JsonPropertyName("csvColumns")] public string CsvColumns { get; init; }` to response record `HubCustomProperty` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubCustomProperty (response) missing REQUIRED property `hubId` (spec type=string).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add `[JsonPropertyName("hubId")] public string HubId { get; init; }` to response record `HubCustomProperty` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubCustomProperty (response) missing REQUIRED property `updatedAt` (spec type=string/date-time).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add `[JsonPropertyName("updatedAt")] public DateTimeOffset UpdatedAt { get; init; }` to response record `HubCustomProperty` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `HubLocation` (response)

- **[response_drift_required]** HubLocation (response) missing REQUIRED property `createdAt` (spec type=string/date-time). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("createdAt")] public DateTimeOffset CreatedAt { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `customerLocationId` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("customerLocationId")] public string CustomerLocationId { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `driverInstructions` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("driverInstructions")] public string DriverInstructions { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `hubId` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("hubId")] public string HubId { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `isDepot` (spec type=boolean). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("isDepot")] public bool IsDepot { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `plannerNotes` (spec type=string). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("plannerNotes")] public string PlannerNotes { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `serviceTimeSeconds` (spec type=integer/int32). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("serviceTimeSeconds")] public int ServiceTimeSeconds { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `serviceWindows` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("serviceWindows")] public IReadOnlyList<object> ServiceWindows { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `skillsRequired` (spec type=array). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("skillsRequired")] public IReadOnlyList<object> SkillsRequired { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubLocation (response) missing REQUIRED property `updatedAt` (spec type=string/date-time). (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("updatedAt")] public DateTimeOffset UpdatedAt { get; init; }` to response record `HubLocation` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

### `HubSkill` (response)

- **[response_drift_required]** HubSkill (response) missing REQUIRED property `createdAt` (spec type=string/date-time).
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Add `[JsonPropertyName("createdAt")] public DateTimeOffset CreatedAt { get; init; }` to response record `HubSkill` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubSkill (response) missing REQUIRED property `hubId` (spec type=string).
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Add `[JsonPropertyName("hubId")] public string HubId { get; init; }` to response record `HubSkill` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** HubSkill (response) missing REQUIRED property `updatedAt` (spec type=string/date-time).
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Add `[JsonPropertyName("updatedAt")] public DateTimeOffset UpdatedAt { get; init; }` to response record `HubSkill` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (39)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListCapacitiesAsync (GET /hub/capacities) is missing query parameter `capacityIds` (spec optional, type=string).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add an optional parameter `string? capacityIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCapacitiesAsync (GET /hub/capacities) is missing query parameter `capacityNames` (spec optional, type=string).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add an optional parameter `string? capacityNames = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCustomPropertiesAsync (GET /hub/customProperties) is missing query parameter `customPropertyIds` (spec optional, type=string).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add an optional parameter `string? customPropertyIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCustomPropertiesAsync (GET /hub/customProperties) is missing query parameter `customPropertyNames` (spec optional, type=string).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add an optional parameter `string? customPropertyNames = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /hub/locations) is missing query parameter `customerLocationIds` (spec optional, type=string).
  - Endpoints: `GET /hub/locations`
  - Recommended fix: Add an optional parameter `string? customerLocationIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHubsAsync (GET /hubs) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /hubs`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCapacitiesAsync (GET /hub/capacities) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCustomPropertiesAsync (GET /hub/customProperties) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /hub/locations) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /hub/locations`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListSkillsAsync (GET /hub/skills) is missing query parameter `endTime` (spec optional, type=string).
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Add an optional parameter `string? endTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHubsAsync (GET /hubs) is missing query parameter `hubIds` (spec optional, type=string).
  - Endpoints: `GET /hubs`
  - Recommended fix: Add an optional parameter `string? hubIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /hub/locations) is missing query parameter `locationIds` (spec optional, type=string).
  - Endpoints: `GET /hub/locations`
  - Recommended fix: Add an optional parameter `string? locationIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListSkillsAsync (GET /hub/skills) is missing query parameter `skillIds` (spec optional, type=string).
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Add an optional parameter `string? skillIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListSkillsAsync (GET /hub/skills) is missing query parameter `skillNames` (spec optional, type=string).
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Add an optional parameter `string? skillNames = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListHubsAsync (GET /hubs) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /hubs`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCapacitiesAsync (GET /hub/capacities) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListCustomPropertiesAsync (GET /hub/customProperties) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListLocationsAsync (GET /hub/locations) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /hub/locations`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListSkillsAsync (GET /hub/skills) is missing query parameter `startTime` (spec optional, type=string).
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Add an optional parameter `string? startTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateHubLocationRequest` (request)

- **[missing_optional]** CreateHubLocationRequest is missing property `driverInstructions` (spec type=string).
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("driverInstructions")] public string? DriverInstructions { get; init; }` to `CreateHubLocationRequest`.
- **[missing_optional]** CreateHubLocationRequest is missing property `plannerNotes` (spec type=string).
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("plannerNotes")] public string? PlannerNotes { get; init; }` to `CreateHubLocationRequest`.
- **[missing_optional]** CreateHubLocationRequest is missing property `serviceTimeSeconds` (spec type=integer/int32).
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("serviceTimeSeconds")] public int? ServiceTimeSeconds { get; init; }` to `CreateHubLocationRequest`.
- **[missing_optional]** CreateHubLocationRequest is missing property `serviceWindows` (spec type=array).
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("serviceWindows")] public IReadOnlyList<object>? ServiceWindows { get; init; }` to `CreateHubLocationRequest`.
- **[missing_optional]** CreateHubLocationRequest is missing property `skillsRequired` (spec type=array).
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Add `[JsonPropertyName("skillsRequired")] public IReadOnlyList<object>? SkillsRequired { get; init; }` to `CreateHubLocationRequest`.

### `HubCustomProperty` (response)

- **[response_required_drift]** HubCustomProperty.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Tighten `HubCustomProperty.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `HubLocation` (response)

- **[response_required_drift]** HubLocation.address (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Tighten `HubLocation.Address` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** HubLocation.latitude (response): spec marks REQUIRED but SDK exposes as nullable (`double?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Tighten `HubLocation.Latitude` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** HubLocation.longitude (response): spec marks REQUIRED but SDK exposes as nullable (`double?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Tighten `HubLocation.Longitude` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** HubLocation.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Tighten `HubLocation.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `HubSkill` (response)

- **[response_required_drift]** HubSkill.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /hub/skills`
  - Recommended fix: Tighten `HubSkill.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateHubLocationRequest` (request)

- **[missing_optional]** UpdateHubLocationRequest is missing property `customerLocationId` (spec type=string).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("customerLocationId")] public string? CustomerLocationId { get; init; }` to `UpdateHubLocationRequest`.
- **[missing_optional]** UpdateHubLocationRequest is missing property `driverInstructions` (spec type=string).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("driverInstructions")] public string? DriverInstructions { get; init; }` to `UpdateHubLocationRequest`.
- **[missing_optional]** UpdateHubLocationRequest is missing property `isDepot` (spec type=boolean).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("isDepot")] public bool? IsDepot { get; init; }` to `UpdateHubLocationRequest`.
- **[missing_optional]** UpdateHubLocationRequest is missing property `latitude` (spec type=number/double).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("latitude")] public double? Latitude { get; init; }` to `UpdateHubLocationRequest`.
- **[missing_optional]** UpdateHubLocationRequest is missing property `longitude` (spec type=number/double).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("longitude")] public double? Longitude { get; init; }` to `UpdateHubLocationRequest`.
- **[missing_optional]** UpdateHubLocationRequest is missing property `plannerNotes` (spec type=string).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("plannerNotes")] public string? PlannerNotes { get; init; }` to `UpdateHubLocationRequest`.
- **[missing_optional]** UpdateHubLocationRequest is missing property `serviceTimeSeconds` (spec type=integer/int32).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("serviceTimeSeconds")] public int? ServiceTimeSeconds { get; init; }` to `UpdateHubLocationRequest`.
- **[missing_optional]** UpdateHubLocationRequest is missing property `serviceWindows` (spec type=array).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("serviceWindows")] public IReadOnlyList<object>? ServiceWindows { get; init; }` to `UpdateHubLocationRequest`.
- **[missing_optional]** UpdateHubLocationRequest is missing property `skillsRequired` (spec type=array).
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Add `[JsonPropertyName("skillsRequired")] public IReadOnlyList<object>? SkillsRequired { get; init; }` to `UpdateHubLocationRequest`.

## LOW (13)

### `CreateHubLocationRequest` (request)

- **[extra_property]** CreateHubLocationRequest.notes: present in SDK but not in spec inner schema.
  - Endpoints: `POST /hub/locations`
  - Recommended fix: Remove `CreateHubLocationRequest.Notes` (not in spec).

### `Hub` (response)

- **[extra_property]** Hub.externalIds (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hubs`
  - Recommended fix: Remove `Hub.ExternalIds` (not in spec).
- **[extra_property]** Hub.formattedAddress (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hubs`
  - Recommended fix: Remove `Hub.FormattedAddress` (not in spec).
- **[extra_property]** Hub.geofence (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hubs`
  - Recommended fix: Remove `Hub.Geofence` (not in spec).
- **[extra_property]** Hub.latitude (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hubs`
  - Recommended fix: Remove `Hub.Latitude` (not in spec).
- **[extra_property]** Hub.longitude (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hubs`
  - Recommended fix: Remove `Hub.Longitude` (not in spec).
- **[extra_property]** Hub.tags (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hubs`
  - Recommended fix: Remove `Hub.Tags` (not in spec).

### `HubCapacity` (response)

- **[extra_property]** HubCapacity.capacity (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Remove `HubCapacity.Capacity` (not in spec).
- **[extra_property]** HubCapacity.timeSlot (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Remove `HubCapacity.TimeSlot` (not in spec).
- **[extra_property]** HubCapacity.usedCapacity (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hub/capacities`
  - Recommended fix: Remove `HubCapacity.UsedCapacity` (not in spec).

### `HubCustomProperty` (response)

- **[extra_property]** HubCustomProperty.type (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /hub/customProperties`
  - Recommended fix: Remove `HubCustomProperty.Type` (not in spec).

### `HubLocation` (response)

- **[extra_property]** HubLocation.notes (response): present in SDK but not in spec inner schema. (affects 3 endpoints)
  - Endpoints: `GET /hub/locations`, `PATCH /hub/location/{id}`, `POST /hub/locations`
  - Recommended fix: Remove `HubLocation.Notes` (not in spec).

### `UpdateHubLocationRequest` (request)

- **[extra_property]** UpdateHubLocationRequest.notes: present in SDK but not in spec inner schema.
  - Endpoints: `PATCH /hub/location/{id}`
  - Recommended fix: Remove `UpdateHubLocationRequest.Notes` (not in spec).

