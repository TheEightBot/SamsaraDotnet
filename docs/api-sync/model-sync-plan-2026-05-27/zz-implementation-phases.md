# Implementation Phases — Model Sync (2026-05-27)

Roll the model-level fixes out in four phases. The split is by severity but each phase has its own breaking-change and rollback profile.

## Phase A — CRITICAL (wrapper-shape fixes)

- **Findings**: 3
- **Affected SDK types**: 3 (`CreateHubLocationRequest`, `CreateHubPlanOrdersRequest`, `UpdateHubLocationRequest`)
- **Affected methods**: 3
- **Breaking?** Yes — these are constructor / signature changes. Any consumer that today calls `CreateLocationAsync(new CreateHubLocationRequest { ... })` will fail to compile when the parameter type changes to an envelope.
- **Test impact**: requires new fixtures for the data-envelope; existing fixtures using the bare record will need updating.
- **CLI impact**: `tools/Samsara.Cli/` is out of scope for this audit, but Phase A signature changes will ripple into the CLI's hub commands; budget a follow-up commit to retarget them.
- **Rollback**: revert the model file plus the affected client method. Low risk to schedule behind a feature flag because there are only 3 endpoints.

### Specific Phase A items

- **`PATCH /hub/location/{id}` (updateHubLocation)** — SDK posts UpdateHubLocationRequest as the body, but spec expects object wrapped in `{ data }`. Inner schema requires: [].
- **`POST /hub/locations` (createHubLocations)** — SDK posts CreateHubLocationRequest as the body, but spec expects array wrapped in `{ data }`. Inner schema requires: ['address', 'customerLocationId', 'hubId', 'isDepot', 'name'].
- **`POST /hub/plan/orders` (createPlanOrders)** — SDK posts CreateHubPlanOrdersRequest as the body, but spec expects array wrapped in `{ data }`. Inner schema requires: ['customerOrderId', 'hubId', 'planId'].

## Phase B — HIGH (required-field tightening, missing required query params, response drift on required fields)

- **Findings**: 329
- **Affected SDK types**: 68
- **Affected methods**: 106
- **Breaking?** Mostly yes. Adding a `required` property to a request record breaks every consumer that instantiates the record without that field. Adding a required parameter to a method is also breaking. Adding a `required` property to a response record is technically a binary break (consumers must now read the value or compile fails) but in practice most consumers use the records as outputs so the impact is smaller.
- **Test impact**: substantial — every fixture/test that constructs `Create*Request` records will need updating to add the new fields. Recommend a fixtures-factory helper to centralize this.
- **CLI impact**: the CLI will need new flags wherever a required method parameter is added (industrial-jobs update, functions storage put, functions logs, etc.).
- **Rollback**: per-type. Each `Create*Request` change is independent — they can ship one at a time over multiple PRs.

### High-traffic items in Phase B

Top 15 SDK types by HIGH-severity finding count (query-param findings have no associated record and are grouped under `(query params)`):

- (query params) — 82 required-field gaps
- `SafetySettings` — 12 required-field gaps
- `SafetyEvent` — 11 required-field gaps
- `HubLocation` — 10 required-field gaps
- `IdlingEvent` — 8 required-field gaps
- `HubPlanOrder` — 8 required-field gaps
- `DriverTrailerAssignment` — 7 required-field gaps
- `FormSubmission` — 7 required-field gaps
- `TrainingAssignment` — 7 required-field gaps
- `AlertConfiguration` — 6 required-field gaps
- `CoachingSession` — 6 required-field gaps
- `AlertIncident` — 5 required-field gaps
- `FormTemplate` — 5 required-field gaps
- `SpeedingInterval` — 5 required-field gaps
- `ReadingDefinition` — 5 required-field gaps

## Phase C — MEDIUM (optional property additions, type mismatches, weak-typing in non-Beta clients, optional query params)

- **Findings**: 956
- **Affected SDK types**: 112
- **Affected methods**: 136
- **Breaking?** No, except for type-mismatch fixes (e.g. switching `string?` to `double?`) which are source-breaking but JSON-compatible.
- **Test impact**: low — additive. Existing tests continue to pass; new tests cover newly-added properties / query params.
- **CLI impact**: additive — new CLI flags for newly-exposed optional parameters.
- **Rollback**: trivial — each property addition is independent.

Top 15 SDK types by MEDIUM-severity finding count (query-param findings grouped under `(query params)`):

- (query params) — 421 medium findings
- `VehicleStats` — 65 medium findings
- `TrailerStats` — 24 medium findings
- `MaintenanceDvir` — 17 medium findings
- `EquipmentStats` — 15 medium findings
- `FormSubmission` — 13 medium findings
- `DataInputDataPoint` — 12 medium findings
- `DefectRecord` — 12 medium findings
- `SafetyEvent` — 11 medium findings
- `IndustrialAsset` — 10 medium findings
- `ComplianceSettings` — 10 medium findings
- `UpdateComplianceSettingsRequest` — 10 medium findings
- `UpdateHubLocationRequest` — 9 medium findings
- `Issue` — 8 medium findings
- `CarrierProposedAssignment` — 7 medium findings

## Phase D — LOW (cleanup, extra-property removal, required-drift over-tightening)

- **Findings**: 431
- **Affected SDK types**: 100
- **Affected methods**: 95
- **Breaking?** Removing extra-property fields IS source-breaking and JSON-incompatible if any caller has been setting them. Audit each removal — most are SDK conveniences (flattened scalar views of nested spec objects) that should be deprecated and replaced with the spec shape, not silently dropped.
- **Test impact**: low; mostly compile-fixes if any removals proceed.
- **CLI impact**: low.
- **Rollback**: trivial.

Top 15 SDK types by LOW-severity finding count:

- `HosLog` — 15 extras / over-tightenings
- `Trip` — 13 extras / over-tightenings
- `TachographActivity` — 11 extras / over-tightenings
- `MediaFile` — 11 extras / over-tightenings
- `Route` — 11 extras / over-tightenings
- `TachographFile` — 10 extras / over-tightenings
- `SpeedingInterval` — 10 extras / over-tightenings
- `DriverVehicleAssignment` — 9 extras / over-tightenings
- `HosEldEvent` — 9 extras / over-tightenings
- `FuelPurchase` — 9 extras / over-tightenings
- `VehicleSafetyScore` — 9 extras / over-tightenings
- `CarrierProposedAssignment` — 8 extras / over-tightenings
- `TrailerAssignment` — 8 extras / over-tightenings
- `HosDailyLog` — 8 extras / over-tightenings
- `FormSubmission` — 8 extras / over-tightenings

**Recommended approach for extra-property findings**:

1. For each LOW `extra_property`, check whether the SDK record's extra scalars are a denormalized view of a nested spec object (e.g. `Trip.driverId`/`Trip.driverName` flatten spec's `Trip.driver: { id, name }`). If so, deprecate the flat property (`[Obsolete]`) and add the nested object as the primary shape.
2. For SDK records whose extras don't match any nested spec object (e.g. fields the API used to return but no longer documents), check the live API to confirm whether the field is still being returned. If yes, file a spec issue; if no, deprecate then remove.
3. For `required_drift_over` (SDK marks `required` but spec optional), drop `required` in the next major release.

## Sequencing notes

- **Phases A and B should land in a single major release** (e.g. v2.x.0). Customers should update once for the new signatures rather than absorbing several signature changes per minor version.
- **Phase C can land incrementally** in patch releases; each individual missing-optional or weak-typing fix is independent and additive.
- **Phase D should land last** and may need a deprecation notice (`[Obsolete]`) on properties before they are removed.
- The `Beta APIs` domain accounts for ~140 findings on its own (mostly MEDIUM). Consider phasing the Beta typings as a separate workstream once the rest of the SDK is clean, since they are explicitly subject to change.

