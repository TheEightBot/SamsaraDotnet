# Carrier Proposed Assignments — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/08-carrier-proposed-assignments.md`](../08-carrier-proposed-assignments.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `CarrierProposedAssignment` | response | 0 | 1 | 7 | 8 |
| `CreateCarrierProposedAssignmentRequest` | request | 0 | 0 | 4 | 3 |
| `(no SDK type)` | query | 0 | 0 | 2 | 0 |

**Counts**: CRITICAL=0, HIGH=1, MEDIUM=13, LOW=11  
**Total deduped findings**: 25

## HIGH (1)

### `CarrierProposedAssignment` (response)

- **[response_drift_required]** CarrierProposedAssignment (response) missing REQUIRED property `activeTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("activeTime")] public string ActiveTime { get; init; }` to response record `CarrierProposedAssignment` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (13)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListAsync (GET /fleet/carrier-proposed-assignments) is missing query parameter `activeTime` (spec optional, type=string).
  - Endpoints: `GET /fleet/carrier-proposed-assignments`
  - Recommended fix: Add an optional parameter `string? activeTime = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** ListAsync (GET /fleet/carrier-proposed-assignments) is missing query parameter `driverIds` (spec optional, type=array).
  - Endpoints: `GET /fleet/carrier-proposed-assignments`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? driverIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CarrierProposedAssignment` (response)

- **[response_drift_optional]** CarrierProposedAssignment (response) missing property `acceptedTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("acceptedTime")] public string? AcceptedTime { get; init; }` to response record `CarrierProposedAssignment`.
- **[response_drift_optional]** CarrierProposedAssignment (response) missing property `driver` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("driver")] public object? Driver { get; init; }` to response record `CarrierProposedAssignment`.
- **[response_drift_optional]** CarrierProposedAssignment (response) missing property `firstSeenTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("firstSeenTime")] public string? FirstSeenTime { get; init; }` to response record `CarrierProposedAssignment`.
- **[response_drift_optional]** CarrierProposedAssignment (response) missing property `rejectedTime` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("rejectedTime")] public string? RejectedTime { get; init; }` to response record `CarrierProposedAssignment`.
- **[response_drift_optional]** CarrierProposedAssignment (response) missing property `shippingDocs` (spec type=string). (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("shippingDocs")] public string? ShippingDocs { get; init; }` to response record `CarrierProposedAssignment`.
- **[response_drift_optional]** CarrierProposedAssignment (response) missing property `trailers` (spec type=array). (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("trailers")] public IReadOnlyList<object>? Trailers { get; init; }` to response record `CarrierProposedAssignment`.
- **[response_drift_optional]** CarrierProposedAssignment (response) missing property `vehicle` (spec type=object). (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("vehicle")] public object? Vehicle { get; init; }` to response record `CarrierProposedAssignment`.

### `CreateCarrierProposedAssignmentRequest` (request)

- **[missing_optional]** CreateCarrierProposedAssignmentRequest is missing property `activeTime` (spec type=string).
  - Endpoints: `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("activeTime")] public string? ActiveTime { get; init; }` to `CreateCarrierProposedAssignmentRequest`.
- **[missing_optional]** CreateCarrierProposedAssignmentRequest is missing property `shippingDocs` (spec type=string).
  - Endpoints: `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("shippingDocs")] public string? ShippingDocs { get; init; }` to `CreateCarrierProposedAssignmentRequest`.
- **[missing_optional]** CreateCarrierProposedAssignmentRequest is missing property `trailerIds` (spec type=array).
  - Endpoints: `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("trailerIds")] public IReadOnlyList<string>? TrailerIds { get; init; }` to `CreateCarrierProposedAssignmentRequest`.
- **[missing_optional]** CreateCarrierProposedAssignmentRequest is missing property `trailerNames` (spec type=array).
  - Endpoints: `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Add `[JsonPropertyName("trailerNames")] public IReadOnlyList<object>? TrailerNames { get; init; }` to `CreateCarrierProposedAssignmentRequest`.

## LOW (11)

### `CarrierProposedAssignment` (response)

- **[extra_property]** CarrierProposedAssignment.driverId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CarrierProposedAssignment.DriverId` (not in spec).
- **[extra_property]** CarrierProposedAssignment.driverName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CarrierProposedAssignment.DriverName` (not in spec).
- **[extra_property]** CarrierProposedAssignment.endTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CarrierProposedAssignment.EndTime` (not in spec).
- **[extra_property]** CarrierProposedAssignment.shippingId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CarrierProposedAssignment.ShippingId` (not in spec).
- **[extra_property]** CarrierProposedAssignment.startTime (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CarrierProposedAssignment.StartTime` (not in spec).
- **[extra_property]** CarrierProposedAssignment.status (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CarrierProposedAssignment.Status` (not in spec).
- **[extra_property]** CarrierProposedAssignment.vehicleId (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CarrierProposedAssignment.VehicleId` (not in spec).
- **[extra_property]** CarrierProposedAssignment.vehicleName (response): present in SDK but not in spec inner schema. (affects 2 endpoints)
  - Endpoints: `GET /fleet/carrier-proposed-assignments`, `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CarrierProposedAssignment.VehicleName` (not in spec).

### `CreateCarrierProposedAssignmentRequest` (request)

- **[extra_property]** CreateCarrierProposedAssignmentRequest.endTime: present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CreateCarrierProposedAssignmentRequest.EndTime` (not in spec).
- **[extra_property]** CreateCarrierProposedAssignmentRequest.shippingId: present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CreateCarrierProposedAssignmentRequest.ShippingId` (not in spec).
- **[extra_property]** CreateCarrierProposedAssignmentRequest.startTime: present in SDK but not in spec inner schema.
  - Endpoints: `POST /fleet/carrier-proposed-assignments`
  - Recommended fix: Remove `CreateCarrierProposedAssignmentRequest.StartTime` (not in spec).

