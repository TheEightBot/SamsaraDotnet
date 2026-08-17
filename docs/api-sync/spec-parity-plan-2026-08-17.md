# Spec Parity Plan — 2026-08-17 (target: v0.5.0)

> **Status: PROPOSAL — awaiting review. No SDK source has been modified.**
> Per the repo's document-first convention, this enumerates every proposed change with its spec
> target so it can be approved (or trimmed) before implementation begins.

## Why this exists

The SDK was declared at 100% parity after the v0.3.0 sweep, yet weakly-typed properties and missing
endpoints keep surfacing in normal use. This document establishes *why* the existing gates missed
them, and specifies the work to close the gap and to keep it closed automatically.

**Two root causes, both confirmed by measurement:**

1. **`check-model-sync.py` had a blanket suppression.** A single allowlist entry —
   `("object", "*", "weak-typing"): "intentional: Beta/preview/volatile-v1 endpoints weakly typed
   (object) by design"` — collapsed **every** weakly-typed body into one line and marked it
   accepted. Additionally `is_weak_type()` never recognised namespace-qualified
   (`System.Text.Json.JsonElement?`) or collection-wrapped (`IReadOnlyList<JsonElement>`) weak
   types, so a further ~54 properties were invisible to the checker regardless of the allowlist.
2. **Samsara mutates the spec in place under a frozen `info.version`.** The live spec still reports
   `2025-10-23`, identical to the committed baseline, while having grown from 224→**263 paths**,
   318→**382 operations**, and 3,934→**5,345 schemas**. Version-keyed drift detection cannot see
   this; only the content fingerprint can, and the weekly job that computes it only files an
   advisory issue.

**Measured state of the repo against the current live spec:**

| Signal | Value |
|---|---|
| Spec operations | 382 (checker-normalized 380) |
| SDK endpoints | 325 — matched 324, **mismatched 1** |
| **Unimplemented spec operations** | **57** (Beta 42, Preview 14, stable 1) |
| Weakly-typed model properties | **96** — only **3** justified by the spec |
| Weakly-typed whole endpoint bodies | **94** endpoints (**101** method signatures — overloads merge) |
| `check-model-sync` active findings today | 13 (0 CRIT / 0 HIGH / 12 MED / 1 LOW) — gate is HIGH, so green |
| `check-model-sync` findings after fixing the checker | **62 MEDIUM / 7 LOW** |
| Fabrication check | clean (0 duplicate coverage, 0 tag drift) |

A third fact shapes sequencing: **the committed baseline is already behind the SDK.** Seven shipped
SDK methods target paths that exist only in the newer live spec (voice-sessions ×2, gateways/pair,
preventive-maintenance ×2, work-order-templates, tachograph file-uploads). `ci.yml` is green today
only because its checkers fetch the **live** spec rather than the baseline.

---

## Agreed scope (from the 2026-08-17 design review)

| # | Decision |
|---|---|
| D1 | One batched **breaking** release, **v0.5.0**. No `[Obsolete]` shims except where noted in D6. |
| D2 | Implement **all 57** unimplemented operations, including Beta and Preview, **fully typed**. |
| D3 | Retype all **93** weakly-typed properties that have a concrete spec schema. Keep `JsonElement` only for the **3** properties the spec itself leaves free-form, each allowlisted with a spec pointer. |
| D4 | Type the **94** whole-body `object` endpoints in `Clients/Beta/*` and the legacy v1 clients. |
| D5 | **Unflatten** existing flattened records — strict 1:1 mirror of the Samsara schema (one record per spec schema; `ObjectResponseBody` / `ResponseBody` / `RequestBody` suffixes stripped). |
| D6 | Endpoints graduated out of `/preview` move to their proper client, carrying `[Experimental("SAMSARA001")]` plus an `[Obsolete]` forwarding shim in `PreviewApisClient` for one release. |
| D7 | Checkers: delete the blanket allowlist; `weak-typing` and `flattened` become **MEDIUM** findings; CI gate moves to `--fail-on-severity MEDIUM`; `check-sdk-sync` gains `--fail-on-unimplemented`; `check-api-sync` gains `--spec-file` and a machine-readable summary. |
| D8 | Drift workflow polls **daily**; structural drift opens/updates a single GitHub Issue containing a complete implementation spec and assigns it to `copilot-swe-agent` via a user-owned PAT (`COPILOT_ASSIGN_TOKEN`), falling back to an unassigned issue that a local agent can be pointed at. Cosmetic-only drift auto-commits a refreshed baseline. |
| D9 | One canonical spec file — `.github/cache/samsara-api-baseline.json`, refreshed as part of this work. `docs/open-api-spec.json` is **not** committed as a second copy. |
| D10 | `docs/api-sync/README.md` status block and the per-domain checklists are regenerated from checker output this pass, so they cannot rot again. |

## Open decisions — these need your call

Eight items surfaced during measurement that were not settled in the design review. Each has a
recommendation; none is assumed in the work below without your approval. **OD7 is the one that
materially changes the size of this release** — please read that one carefully.

**OD1 — Pin CI to the committed baseline instead of the live spec.** Today `ci.yml`'s checkers fetch
the live Samsara spec, which makes CI non-hermetic: an upstream edit can turn a PR red without any
code change, and — as measured above — it is currently *masking* a stale baseline. Recommendation:
CI runs `--spec-file .github/cache/samsara-api-baseline.json`; the daily drift workflow becomes the
only consumer of the live spec. **Consequence:** the baseline must be refreshed in the same PR that
pins CI, because seven already-shipped methods target ops the stale baseline lacks.

**OD2 — Untrack `docs/api-sync/diff-report.md`.** It is a generated artifact whose default output
path overwrites a tracked file, so any local checker run dirties the tree. Recommendation: `git rm`
it, add to `.gitignore`; drift reports live in the workflow artifact and issue body.

**OD3 — `main` ruleset and making `sdk-sync` a required check.** There is currently no required
status check on `main` (only the org-level automatic Copilot code review). To make "green means
compliant" enforceable, `sdk-sync` must be required, which needs three mechanical changes to
`ci.yml`: rename the job from `SDK ↔ Spec endpoint check` to `sdk-sync` (stable ASCII check name),
remove `paths-ignore` from the `pull_request` trigger (a required check skipped by a path filter
leaves docs-only PRs stuck at "Expected"), and add a `github-actions` bypass so the cosmetic-drift
baseline commit can still push to `main`. If your org disallows app bypass, the cosmetic path must
open an auto-merged PR instead — which needs `allow_auto_merge` enabled (currently `false`).

**OD4 — `COPILOT_ASSIGN_TOKEN` is yours to create; I cannot.** Copilot ignores assignments made by
`GITHUB_TOKEN`, so L3 requires a human-owned fine-grained PAT. Scope: this repository only, **Issues:
read & write** (required for the `replaceActorsForAssignable` mutation), Metadata: read; Pull
requests: read & write and Contents: read recommended so the resulting PR is attributed to you.
`copilot-swe-agent` is confirmed assignable on this repo today. Without the secret the workflow still
files the issue and comments that it is ready for a local agent — so nothing blocks on this.

**OD5 — Whether to split `PreviewApisClient`.** The new Preview surface is 14 ops across two coherent
domains (5 orders, 9 warranties/warranty-claims). `tools/sdk-client-tags.json` maps both to
`PreviewApisClient`. Recommendation: split into `OrdersClient` and `WarrantiesClient` under
`Clients/Preview/` — a 14-method grab-bag client is the shape that produced the original Hubs
mis-homing. Cost: `sdk-client-tags.json` needs two new entries.

**OD6 — D6 applies to one endpoint, not two.** `/preview/gateways/pair` → `/gateways/pair` was
**already migrated**: `GatewaysClient.cs:27-28` calls the correct path and only the doc comment in
`IGatewaysClient.cs:27-29` is stale. So the `[Experimental]` + forwarding-shim treatment is needed
only for `POST /fleet/tachograph/file-uploads` (currently `PreviewApisClient.cs:20-22`, `:38-39`).
Recommendation: fix the stale comment, apply D6 to tachograph alone.

**OD7 — How deep does 1:1 typing go? (the scope question)** The 142 records in Part 2 cover only the
schemas the weak properties point at *directly*. Typing their children 1:1 as well pulls in far more:
`TriggerParams` alone has 35 child `*Details` objects (×2 for request/response twins) and
`WorkflowIncidentDetails` has 68 — the full object-schema closure is **~579 spec names** (roughly
halving after twin merges; some children already exist in the SDK).

- **A. Full transitive typing.** The only option that actually satisfies "no `JsonElement` anywhere" —
  typing a parent but leaving its children `JsonElement` just moves the problem down one level, which
  is how the SDK arrived here.
- **B. Direct records now (142), Alerts/Workflow children as a follow-on.** Roughly halves this
  release. Requires leaving `AlertTrigger.TriggerParams` and `AlertIncidentCondition.Details` weakly
  typed, allowlisted under a **new `deferred` category with a tracking issue** — explicitly *not* the
  `intentional` category, since no spec pointer justifies it.

➡️ **A**, with the Alerts/Workflow children landing as their own commits so review stays tractable.
My reservation about B is not the work split — it is that a "deferred" allowlist category is the same
mechanism as the blanket suppression this whole effort exists to remove, and deferred entries have a
poor track record of coming back. If the release size is the binding constraint, B is legitimate, but
then the tracking issue should be filed the same day and the category should expire loudly.

**OD8 — Are the 44 non-Beta weak methods in scope?** You approved typing the Beta method-level
`object` signatures. Measurement then found the same pattern in **44 additional methods on non-Beta
clients** (§2b, 47 more schemas) — the `V1*`/legacy methods on `AssetsClient`, `IndustrialClient`,
`MaintenanceClient`, `SafetyClient`, `ComplianceClient`, `DriversClient`, `VehiclesClient`,
`HubsClient`, `RouteEventsClient`, `TrainingClient`. Recommendation: **in scope** — it is the same
defect class, these are on *stable* clients where users are most likely to hit it, and the MEDIUM gate
will fail on them anyway once the blanket allowlist is deleted.

## Part 1 — The 57 unimplemented operations (D2)

13 domains; **270** schemas in the transitive closure, **~148** structurally distinct after
de-duplicating the `Create*/Update*/List*` copies the spec emits of identical Money/Ref shapes. That
~148 is the realistic new-record count.

| Domain | Ops | Closure | Distinct | Proposed client | Models file |
|---|---|---|---|---|---|
| Asset Sharing | 9 | 16 | 12 | **new** `AssetSharingClient` (`Clients/Fleet/`) | **new** `Models/Fleet/AssetSharingModels.cs` |
| Asset Assignments & Associations | 4 | 10 | 10 | `AssetsClient` | `Models/Fleet/AssetModels.cs` (append) |
| Installer Photo Uploads | 3 | 8 | 8 | `BetaClient` or **new** `InstallerClient` | **new** `Models/Beta/InstallerPhotoUploadModels.cs` |
| Ground Intelligence | 4 | 16 | 13 | **new** `GroundIntelligenceClient` (`Clients/Beta/`) | **new** `Models/Beta/GroundIntelligenceModels.cs` |
| Maintenance Parts & Purchase Orders | 13 | 87 | 32 | **new** `PartsClient` (`Clients/Maintenance/`) | **new** `PartsModels.cs` + `PurchaseOrderModels.cs` |
| Maintenance Preventive & Time Entries | 3 | 14 | 12 | `MaintenanceClient` | `Models/Maintenance/MaintenanceModels.cs` (append) |
| Places (geocode/geofence) | 2 | 9 | 8 | `PlacesClient` | `Models/Beta/PlaceModels.cs` (append) |
| Equipment | 1 | 3 | 3 | `EquipmentClient` | `Models/Fleet/FleetModels.cs` (append) |
| Hub Route Templates | 2 | 9 | 7 | `HubsClient` | `Models/Routes/HubModels.cs` (append) |
| Tachograph | 1 | 4 | 4 | `TachographClient` (re-homed — see OD6) | `Models/Compliance/TachographModels.cs` (append) |
| Legacy v1 Fleet | 1 | 3 | 3 | `VehiclesClient.V1GetFleetLocationsAsync` | `Models/Fleet/FleetModels.cs` (append) |
| Preview Orders | 5 | 22 | 21 | `PreviewApisClient` or **new** `OrdersClient` (OD5) | **new** `Models/Routes/OrderModels.cs` |
| Preview Warranties | 9 | 77 | 32 | `PreviewApisClient` or **new** `WarrantiesClient` (OD5) | **new** `Models/Maintenance/WarrantyModels.cs` |
| **Total** | **57** | **270** | **~148** | | |

Notes that affect shape, not just volume:

- **21 operations are paginated** (`pagination.endCursor`) → `IAsyncEnumerable<T>` via `PaginateAsync<T>`.
  **`GET /preview/fleet/orders` is not** — it returns `data[]` with no pagination, so it must be
  `Task<IReadOnlyList<T>>`. `getPlaceGeocode`/`getPlaceGeofence` *are* paginated in the spec despite
  being semantically lookups.
- 5 operations return **201** (`createPart`, `createPartInventoryLocation`, `createPurchaseOrder`,
  `createWarranty`, `createWarrantyClaim`); 7 return **204** with no body.
- Almost every Beta operation identifies its resource by **query string**, not path segment
  (`?id=`, `?dsaId=`, `?partSamsaraId=&placeId=`). Only `PATCH /fleet/equipment/{id}/digital-output`
  uses a path parameter. Method signatures must follow the spec here rather than REST habit.
- `/ground-intelligence/issues` must **not** go on `IssuesClient` — it is a different resource from
  `/issues`; the `listIssues` operationId collides in name only. This is exactly the mis-homing class
  that produced the fabricated Hubs CRUD.
- `tools/sdk-client-tags.json` needs the tag **`Fleet`** added (used only by `GET /v1/fleet/locations`),
  plus entries for whichever new clients are approved.

<details>
<summary><strong>All 57 operations</strong> (click to expand)</summary>

`req*` marks required query params. Pag = paginated. Closure = distinct schemas reachable from
request + success response.

| # | Verb | Path | operationId | Status | Proposed client · method | Query params | Success | Pag | Closure |
|---|---|---|---|---|---|---|---|---|---|
| 1 | GET | `/fleet/asset-sharing/agreements` | `listAssetSharingAgreements` | Beta | AssetSharingClient · `ListAgreementsAsync` | ids, statusIn, roleIn, after | 200 | yes | 3 |
| 2 | POST | `/fleet/asset-sharing/agreements` | `createAssetSharingAgreement` | Beta | AssetSharingClient · `CreateAgreementAsync` | — | 200 | no | 3 |
| 3 | DELETE | `/fleet/asset-sharing/agreements` | `deleteAssetSharingAgreement` | Beta | AssetSharingClient · `DeleteAgreementAsync` | id\* | 204 | no | 0 |
| 4 | POST | `/fleet/asset-sharing/agreements/accept` | `acceptAssetSharingAgreement` | Beta | AssetSharingClient · `AcceptAgreementAsync` | id\* | 200 | no | 2 |
| 5 | GET | `/fleet/asset-sharing/agreements/assets` | `listSharedAssets` | Beta | AssetSharingClient · `ListSharedAssetsAsync` | dsaId\*, after | 200 | yes | 3 |
| 6 | POST | `/fleet/asset-sharing/agreements/assets/batch` | `createSharedAssetsBatch` | Beta | AssetSharingClient · `CreateSharedAssetsBatchAsync` | dsaId\* | 200 | no | 4 |
| 7 | PATCH | `/fleet/asset-sharing/agreements/assets/batch` | `updateSharedAssetsBatch` | Beta | AssetSharingClient · `UpdateSharedAssetsBatchAsync` | — | 200 | no | 4 |
| 8 | POST | `/fleet/asset-sharing/agreements/cancel` | `cancelAssetSharingAgreement` | Beta | AssetSharingClient · `CancelAgreementAsync` | id\* | 200 | no | 2 |
| 9 | POST | `/fleet/asset-sharing/agreements/reject` | `rejectAssetSharingAgreement` | Beta | AssetSharingClient · `RejectAgreementAsync` | id\* | 200 | no | 2 |
| 10 | GET | `/fleet/assets/assignments` | `listAssetAssignments` | Beta | AssetsClient · `ListAssignmentsAsync` | includeExternalIds, assetIds, assigneeIds, after | 200 | yes | 5 |
| 11 | POST | `/fleet/assets/assignments` | `createAssetAssignment` | Beta | AssetsClient · `CreateAssignmentAsync` | — | 200 | no | 5 |
| 12 | POST | `/fleet/assets/assignments/unassign` | `unassignAssetAssignment` | Beta | AssetsClient · `UnassignAsync` | — | 204 | no | 1 |
| 13 | GET | `/fleet/assets/associations` | `listAssociations` | Beta | AssetsClient · `ListAssociationsAsync` | peripheralIds\*, startTime\*, endTime, after | 200 | yes | 3 |
| 14 | GET | `/fleet/installer/photo-uploads` | `getFleetInstallerPhotoUploads` | Beta | InstallerClient · `GetInstallerPhotoUploadsAsync` | ids, startTime, endTime, after | 200 | yes | 3 |
| 15 | POST | `/fleet/installer/photo-uploads` | `postFleetInstallerPhotoUpload` | Beta | InstallerClient · `CreateInstallerPhotoUploadAsync` | — | 200 | no | 4 |
| 16 | POST | `/fleet/installer/photo-uploads/complete` | `postFleetInstallerPhotoUploadComplete` | Beta | InstallerClient · `CompleteInstallerPhotoUploadAsync` | id\* | 200 | no | 2 |
| 17 | GET | `/ground-intelligence/issues` | `listIssues` | Beta | GroundIntelligenceClient · `ListIssuesAsync` | ids, types, statuses, severities, startTime, endTime, queryByTimeField, after, limit | 200 | yes | 6 |
| 18 | PATCH | `/ground-intelligence/issues` | `updateGroundIntelligenceIssue` | Beta | GroundIntelligenceClient · `UpdateIssueAsync` | id\* | 200 | no | 6 |
| 19 | POST | `/ground-intelligence/watchpoints` | `createWatchpoint` | Beta | GroundIntelligenceClient · `CreateWatchpointAsync` | — | 200 | no | 5 |
| 20 | PATCH | `/ground-intelligence/watchpoints` | `updateWatchpoint` | Beta | GroundIntelligenceClient · `UpdateWatchpointAsync` | id\* | 200 | no | 4 |
| 21 | GET | `/maintenance/parts` | `listParts` | Beta | PartsClient · `ListPartsAsync` | idIn, partIds, partStatus, includeDeleted, after, limit | 200 | yes | 4 |
| 22 | POST | `/maintenance/parts` | `createPart` | Beta | PartsClient · `CreatePartAsync` | — | 201 | no | 5 |
| 23 | PATCH | `/maintenance/parts` | `updatePart` | Beta | PartsClient · `UpdatePartAsync` | id\* | 200 | no | 5 |
| 24 | DELETE | `/maintenance/parts` | `deletePart` | Beta | PartsClient · `DeletePartAsync` | id\* | 204 | no | 0 |
| 25 | GET | `/maintenance/parts/inventory-location` | `listPartInventory` | Beta | PartsClient · `ListPartInventoryAsync` | placeIds, isLowStock, partSamsaraIds, after, limit | 200 | yes | 6 |
| 26 | POST | `/maintenance/parts/inventory-location` | `createPartInventoryLocation` | Beta | PartsClient · `CreatePartInventoryLocationAsync` | partSamsaraId, placeId | 201 | no | 7 |
| 27 | PATCH | `/maintenance/parts/inventory-location` | `updatePartInventoryLocation` | Beta | PartsClient · `UpdatePartInventoryLocationAsync` | partSamsaraId, placeId | 200 | no | 7 |
| 28 | POST | `/maintenance/parts/stock-movements` | `createStockMovement` | Beta | PartsClient · `CreateStockMovementAsync` | — | 200 | no | 6 |
| 29 | GET | `/maintenance/parts/transactions` | `listPartTransactions` | Beta | PartsClient · `ListPartTransactionsAsync` | happenedAtTimeStart\*, happenedAtTimeEnd, partSamsaraIds, placeIds, transactionTypeIn, after, limit | 200 | yes | 5 |
| 30 | GET | `/maintenance/purchase-orders` | `listPurchaseOrders` | Beta | PartsClient · `ListPurchaseOrdersAsync` | ids, poNumbers, vendorIds, startTime\*, endTime, after, limit | 200 | yes | 11 |
| 31 | POST | `/maintenance/purchase-orders` | `createPurchaseOrder` | Beta | PartsClient · `CreatePurchaseOrderAsync` | — | 201 | no | 17 |
| 32 | PATCH | `/maintenance/purchase-orders` | `updatePurchaseOrder` | Beta | PartsClient · `UpdatePurchaseOrderAsync` | id\* | 200 | no | 17 |
| 33 | DELETE | `/maintenance/purchase-orders` | `deletePurchaseOrder` | Beta | PartsClient · `DeletePurchaseOrderAsync` | id\* | 204 | no | 0 |
| 34 | POST | `/maintenance/preventive/resolve` | `resolvePreventiveMaintenance` | Beta | MaintenanceClient · `ResolvePreventiveMaintenanceAsync` | assetId, scheduleId | 200 | no | 3 |
| 35 | PATCH | `/maintenance/preventive/upcoming` | `updateUpcomingPreventiveMaintenance` | Beta | MaintenanceClient · `UpdateUpcomingPreventiveMaintenanceAsync` | assetId, scheduleId | 200 | no | 6 |
| 36 | GET | `/maintenance/time-entries/stream` | `listTimeEntries` | Beta | MaintenanceClient · `GetTimeEntriesStreamAsync` | startTime\*, endTime, after, limit | 200 | yes | 5 |
| 37 | GET | `/places/geocode` | `getPlaceGeocode` | Beta | PlacesClient · `GetGeocodeAsync` | address\*, after, limit | 200 | yes | 3 |
| 38 | GET | `/places/geofence` | `getPlaceGeofence` | Beta | PlacesClient · `GetGeofenceAsync` | latitude\*, longitude\*, suggestionTypes, sizeOrder, min/max lat-lng, maxAreaSquareMeters, maxSourceVertices, maxVertices, maxResults, after | 200 | yes | 7 |
| 39 | PATCH | `/fleet/equipment/{id}/digital-output` | `setEquipmentDigitalOutput` | Beta | EquipmentClient · `SetDigitalOutputAsync` | — (path `id`) | 200 | no | 3 |
| 40 | POST | `/hub/route-templates` | `createHubRouteTemplate` | Beta | HubsClient · `CreateRouteTemplateAsync` | — | 200 | no | 7 |
| 41 | PATCH | `/hub/route-templates` | `updateHubRouteTemplate` | Beta | HubsClient · `UpdateRouteTemplateAsync` | id\* | 200 | no | 5 |
| 42 | POST | `/fleet/tachograph/file-uploads` | `postTachographFileUpload` | Beta | TachographClient · `CreateFileUploadAsync` (re-homed) | — | 200 | no | 4 |
| 43 | GET | `/v1/fleet/locations` | `getFleetLocations` | **Stable** (v1) | VehiclesClient · `V1GetFleetLocationsAsync` | after, limit, vehicleIds, tagIds | 200 | yes | 3 |
| 44 | GET | `/preview/fleet/orders` | `getOrders` | Preview | OrdersClient · `GetOrdersAsync` | orderIds\*, includeExternalIds | 200 | **no** | 8 |
| 45 | DELETE | `/preview/fleet/orders` | `deleteOrder` | Preview | OrdersClient · `DeleteOrderAsync` | orderId\* | 204 | no | 0 |
| 46 | POST | `/preview/fleet/orders/batch` | `postOrdersBatch` | Preview | OrdersClient · `PostOrdersBatchAsync` | — | 200 | no | 17 |
| 47 | GET | `/preview/fleet/orders/deletions` | `getOrderDeletions` | Preview | OrdersClient · `GetOrderDeletionsAsync` | startTime, endTime, after, limit | 200 | yes | 3 |
| 48 | GET | `/preview/fleet/orders/stream` | `getOrdersStream` | Preview | OrdersClient · `GetOrdersStreamAsync` | startTime\*, endTime, routeId, includeExternalIds, after | 200 | yes | 9 |
| 49 | GET | `/preview/maintenance/warranties` | `listWarranties` | Preview | WarrantiesClient · `ListWarrantiesAsync` | warrantyIds, name, after, limit, includeExternalIds | 200 | yes | 7 |
| 50 | POST | `/preview/maintenance/warranties` | `createWarranty` | Preview | WarrantiesClient · `CreateWarrantyAsync` | — | 201 | no | 10 |
| 51 | PATCH | `/preview/maintenance/warranties` | `updateWarranty` | Preview | WarrantiesClient · `UpdateWarrantyAsync` | id\* | 200 | no | 10 |
| 52 | DELETE | `/preview/maintenance/warranties` | `deleteWarranty` | Preview | WarrantiesClient · `DeleteWarrantyAsync` | id\* | 204 | no | 0 |
| 53 | POST | `/preview/maintenance/warranties/assets/replace` | `replaceWarrantyAssetAssignments` | Preview | WarrantiesClient · `ReplaceWarrantyAssetAssignmentsAsync` | warrantyId | 200 | no | 5 |
| 54 | GET | `/preview/maintenance/warranty-claims` | `listWarrantyClaims` | Preview | WarrantiesClient · `ListWarrantyClaimsAsync` | warrantyClaimIds, assetIds, claimStatus, warrantyIds, after, limit, includeExternalIds | 200 | yes | 12 |
| 55 | POST | `/preview/maintenance/warranty-claims` | `createWarrantyClaim` | Preview | WarrantiesClient · `CreateWarrantyClaimAsync` | — | 201 | no | 17 |
| 56 | PATCH | `/preview/maintenance/warranty-claims` | `updateWarrantyClaim` | Preview | WarrantiesClient · `UpdateWarrantyClaimAsync` | id\* | 200 | no | 17 |
| 57 | DELETE | `/preview/maintenance/warranty-claims` | `deleteWarrantyClaim` | Preview | WarrantiesClient · `DeleteWarrantyClaimAsync` | id\* | 204 | no | 0 |

</details>

### Path moves and new parameters

| Change | Current location | Action |
|---|---|---|
| `POST /preview/fleet/tachograph/file-uploads` → `/fleet/tachograph/file-uploads` | `Clients/Preview/PreviewApisClient.cs:20-22` (interface), `:38-39` (impl) | Re-home to `TachographClient.CreateFileUploadAsync`; response shape is identical, so the record is reused, not re-modelled. D6 treatment. |
| `POST /preview/gateways/pair` → `/gateways/pair` | `Clients/Fleet/GatewaysClient.cs:27-28` | **Already correct**; only the doc comment at `IGatewaysClient.cs:27-29` is stale (OD6). |
| `sourceName` param added | `DriverVehicleAssignmentsClient.cs:16-35`, `IDriverVehicleAssignmentsClient.cs:14-23` | Add optional param |
| `assetTypes` param added | `ReadingsClient.cs:20-39` + `:41-57`, `IReadingsClient.cs:18-37` + `:39-54` | Add optional param to `GetHistoryAsync` and `GetSnapshotAsync` |

## Part 2 — Weak typing and flattening (D3, D4, D5)

> Every `$ref` below was resolved by replaying `check-model-sync.py`'s own endpoint→record mapping
> against the spec — i.e. the schema the record's *actual endpoint* returns or accepts, not a
> name-based guess. That matters here: this repo has known v1/v2 schema-name collisions where matching
> by name produces false findings.

### 2.1 The 93 property retypings (D3) — 83 distinct spec schemas

| Models file | Props | Notable targets |
|---|---|---|
| `Communication/CommunicationModels.cs` | 8 | `TriggerParamsObjectResponseBody` (**35 props**), `WorkflowIncidentDetailsObjectResponseBody` (**68 props**), `ActionParams`, `TimeRange`, tiny asset/driver/tag/widget refs |
| `Drivers/DriverModels.cs` | 14 | `DriverEldSettings`, `DriverCarrierSettings` (**record already exists** — needs 2 props, not a new type), `DriverHosSetting`, `UsDriverRulesetOverride`, attribute + tag tinies |
| `Maintenance/WorkOrderModels.cs` | 14 | `ServiceTaskInstance` (9), `WorkOrderAttachment` (4), `WorkOrderDiscount`/`Item`/`Tax`/`UnallocatedLabor` (2 each) |
| `Routes/HubModels.cs` | 12 | `ServiceWindow`, `OrderCustomProperty`, `QuantityObject`; **4 of these need no record at all** — `skillsRequired` is `string[]` in the request shapes → `IReadOnlyList<string>` |
| `Fleet/FleetModels.cs` + `AssetModels.cs` + `TrailerModels.cs` | 11 | `GrossVehicleWeight`, `VehicleStatsFaultCodesOBDIITroubleCode` (7), `SpeedingIntervalResponseBody` (7), attribute tinies |
| `Organization/SettingsModels.cs` | 9 | 6 harsh-sensitivity settings all `{heavyDuty,lightDuty,passenger}`, `InattentiveDrivingDetectionAlertSettings`, `speedingSeverityLevel`. The XML docs claiming these stay `JsonElement` "to preserve their full nested payloads" are simply wrong against this spec. |
| `Routes/TripModels.cs` | 5 | `FleetTripAddress`, `FleetTripCoordinates`, trip geofence |
| `Documents/FormModels.cs` | 4 | `FormsFieldDefinition` (17), `FormsFieldInput` (18), `FormSubmissionRequestFieldInput` (13) |
| `Maintenance/MaintenanceModels.cs` | 4 | `dvirTrailerDefectsItems`, `WalkaroundPhoto`, `DefectPhoto` |
| `Tags/AttributeModels.cs` | 4 | `attributeValueTiny`, `AttributeEntity` |
| `Assignments/AssignmentModels.cs` | 3 | `V1TrailerAssignmentResponse`, `V1TrailerAssignmentsResponse`, `V1Pagination` |
| `Routes/RouteModels.cs` | 3 | `LiveSharingLinkResponseObject`, route-stop address + `RouteStopOrderTaskReference` |
| `Documents/DocumentModels.cs`, `Safety/CoachingModels.cs` | 2 | `fieldObjectValue`, `coachableEvent` |

**Heavy sharing keeps the record count well below the property count:** the `{id,name,parentTagId}`
tag-tiny family collapses 5 spec names → 1 `TagTiny`; the attribute-tiny family collapses 5 spec
names → 1 `AttributeTiny` serving 10 properties.

**Name collisions to resolve when stripping suffixes:** `SpeedingInterval` (new item type vs existing
wrapper → `SpeedingIntervalDetail`), `Geofence` (trip-location tiny vs `Addresses.Geofence` →
`TripGeofence`), `SafetyEvent` (legacy v1 list shape vs existing → `LegacySafetyEvent`). Existing
stubs `FormFieldDefinition` (4 props vs the spec's 17), `DiagnosticTroubleCode`, and `DvirDefect`
must be **replaced**, not duplicated alongside.

### 2.2 Method-level weak signatures (D4) — correction to the earlier figure

The "~293 `object` generic args" I reported earlier was a raw token count, not methods. The real
figures:

| Group | Weak methods | Distinct schemas |
|---|---|---|
| `Clients/Beta/*` + `LegacyApisClient` + `PreviewApisClient` (§2 of the inventory) | **57** | 57 |
| Other, non-Beta clients (§2b) | **44** | 47 |
| **Total** | **101** | **104** |

Per-client: `FunctionsClient` 11, `BetaClient` 9, `RidershipClient` 8, `LegacyApisClient` 8,
`QualificationRecordsClient` 7, `ReportsClient` 7, `ReportsClient`/`PreferredStationsClient` 5/4,
`PlacesClient` 3, `PreviewApisClient` 2.

Two anomalies worth deciding on rather than mechanically typing:

- `FunctionsClient.DeployAsync` and `UpdateStorageFileAsync` accept an `object request`, but the spec
  defines **no `requestBody`** for those operations. Either the parameter is wrong, or the spec is
  incomplete — needs a live-API check before typing.
- `PreviewApisClient.CreateTachographFileUploadAsync` calls a path that is not in the spec at all —
  resolved by the OD6 re-homing.

### 2.3 Flattening (D5) — much smaller than the docs imply

Only **one** record is genuinely still flattened, and it is a **live bug**:

**`UserRole`** (`Models/Organization/OrganizationModels.cs:56`) — a single 3-prop record
(`id`, `name`, `tagId`) is used for three divergent spec shapes:

| Where | Spec schema | Shape |
|---|---|---|
| `GET /user-roles` item | `UserRole` | `id`, `name` — matches |
| `User.roles[]` | `UserRoleAssignment` | `role` → `UserRole`, `tag` → `tagTinyResponse`, `expireAt` |
| `CreateUserRequest.roles[]` / `Update…` | `CreateUserRequest_roles` | `roleId`, `tagId` |

Consequences today: deserializing `User.roles` **populates nothing** (the wire nests under `role`),
`expireAt` is silently dropped, and on create/update the SDK serializes `id` where the spec expects
`roleId`. Fix: `UserRoleAssignment { Role, Tag, ExpireAt }` for responses and `UserRoleInput { RoleId,
TagId }` for requests; `UserRole` itself stays as the `/user-roles` shape. **This is precisely the
blind spot documented in the checker notes** — the union of the three shapes contains every SDK
property, so no `extra-property` finding ever fired.

Also: **`DvirEntry` / `DvirVehicle` / `DvirSignature` / `DvirDefect`** are orphaned legacy records —
registered in `SamsaraJsonContext` but referenced by **no client method**, superseded by
`MaintenanceDvir`. The correct fix is deletion, not retyping.

Two docs corrections fall out: `CarrierProposedAssignment` is **already un-flattened** (commit
`b22a2ca`), so the README row-08 note "model still flattened (follow-up)" is stale; and
`DriverCoachAssignment.driverId`, `DriverTrailerAssignment.driverId/trailerId`, and
`MaintenanceDvir.trailerName` *look* flattened but are verified spec-accurate dual shapes — not
defects, and they must not be "fixed".

> **⚠️ Reversed 2026-08-17.** The paragraph above is superseded for the **DVIR/Defect** records.
> The dual v1/v2 shapes of `MaintenanceDvir`, `DefectRecord`, `MaintenanceDvirAssetRef`,
> `MaintenanceDvirSignature` and `MaintenanceSignatoryUser` are **no longer accepted** — they are
> split into `V1`-prefixed v1 records and unprefixed v2 records, one record per spec shape, on the
> approved rule that *where the spec defines two versions with different objects, the SDK should be
> representative of that*. `MaintenanceDvir.trailerName` therefore moves to `V1MaintenanceDvir`
> rather than staying on a record that also answers the v2 endpoints. The full design — record
> inventory, schema resolution, naming scheme, and the list of public API breaks — is the
> **2026-08-17b design note** in [`30-maintenance.md`](30-maintenance.md). The other three records
> named above (`DriverCoachAssignment`, `DriverTrailerAssignment`, `CarrierProposedAssignment`) are
> unaffected and their guidance stands. The original text is kept, not deleted, so the reasoning
> trail survives.

### 2.4 Record-count totals

| Bucket | Distinct spec schemas |
|---|---|
| §2.1 property retyping | 83 |
| §2.2 Beta/Legacy/Preview method-level | 57 |
| §2.3 flattening | 2 |
| **Direct total** | **142** |
| After merging 12 identical request/response twins | **130** |
| After merging identical-shape families (tag-tiny, attribute-tiny, harsh-sensitivity ×6, …) | **106** |
| If §2b's 44 non-Beta weak methods are also in scope (OD8) | **189 direct** |

Add Part 1's ~148 for the new endpoints, and the total new-record work is roughly **250–340
records**, depending on OD7 and OD8 below.

## Part 3 — Checker changes (the part that keeps this from recurring)

### 3.1 `tools/check-model-sync.py`

The allowlist entry to **delete**, verbatim:

```python
("object", "*", "weak-typing"): "intentional: Beta/preview/volatile-v1 endpoints weakly typed (object) by design",
```

The 12 remaining entries are **kept** (7 `TrailerAssignment` v1 back-compat extras, 1 v1 nested
envelope cursor, 4 deliberate over-tightenings). Three new entries are **added**, each citing the
spec pointer that proves the schema is genuinely free-form:

```python
("ReadingDefinition", "type", "weak-typing"):
    "spec components.schemas.ReadingDefinitionResponseBody.properties.type is a free-form {type: object} (no properties) — JsonElement is the honest type",
("ReadingHistory", "value", "weak-typing"):
    "spec components.schemas.ReadingHistoryResponseBody.properties.value is free-form {type: object} — value shape depends on the reading's dataType",
("ReadingSnapshot", "value", "weak-typing"):
    "spec components.schemas.ReadingSnapshotResponseBody.properties.value is free-form {type: object} — value shape depends on the reading's dataType",
```

| # | Site | Change |
|---|---|---|
| M1 | `is_weak_type()` | Recognise namespace-qualified (`System.Text.Json.JsonElement?`) and collection-wrapped (`IReadOnlyList<JsonElement>`, `T[]`) weak types — strip nullable, peel one collection wrapper, take the leaf after the last `.`. Treat `Dictionary<string, object>` as weak **only** when the spec property is an object with `properties` (otherwise it is a legitimate map). |
| M2 | `compare_record()` weak branch | Replace the current "only when inner has `properties`" test with a `_spec_is_concrete()` predicate: concrete = has `properties`, or `enum`, or `oneOf/anyOf/allOf`, or a scalar `type`, or `type: object` with a non-trivial `additionalProperties`. Emit `weak-typing` at **MEDIUM** when concrete, **LOW** when free-form (same ftype, so the three Reading allowlist entries still match and stay reviewable). |
| M3 | `type_mismatch()` | Early-return `None` for weak types so they never double-report as `type-mismatch`. |
| M4 | Whole-body weak typing (request + response sites) | Change the finding key from the literal `("object", "*")` to `(f"{file}::{method}", "<request>"/"<response>")`. **This is the fix that matters most** — today all 94 weak endpoint bodies dedup into one line, which is exactly what one blanket allowlist entry could hide. |
| M5 | Flattened heuristic | Rename ftype `flattened-nested` → `flattened`; severity LOW → **MEDIUM**; widen matching to `<name><Child>`, `<name>_<child>`, and bare `<child>` when `<child>` is not itself a top-level spec property. |
| M6 | `ALLOWLIST` | As above, plus a module comment requiring a spec pointer on every `weak-typing` entry. |
| M7 | `report_json()` | Add `gate` and `by_type` blocks and a `--json-file PATH` flag so one invocation serves both the human log and the issue builder. |

**Measured effect** (probe run, not committed): active findings go from `0/0/0/4` to
`0 CRIT / 0 HIGH / 62 MEDIUM / 7 LOW` — 61 per-property weak-typing sites plus the whole-body group
that M4 expands into 94 separate findings. That backlog is precisely the work in Part 2.

**Known limits, to be documented in the README rather than silently accepted:** records not reachable
from any client method are never compared (coverage is endpoint-reachable records only), and the beta
severity cap now only prevents CRITICAL/HIGH labels — it no longer exempts anything from the gate.

### 3.2 Other checkers

| Checker | Change |
|---|---|
| `check-sdk-sync.py` | Add `--fail-on-unimplemented`. Fix `missing_count` double-counting multi-tag ops; expose a unique `missing` list in JSON (today the workflow would have to scrape `--show-missing` text). Add `spec_source` to JSON so a silent baseline fallback is visible. Add `--json-file`. |
| `check-sdk-fabrication.py` | No functional change; add `--json-file`. Document that against a *newer* spec, a method reaching a new tag reports tag-drift until `--update-tags` is run in the implementation PR. |
| `check-api-sync.py` | Add `--spec-file`, `--no-report`, `--summary-json PATH`, `--fail-on-structural`. New `classify()` → `none` / `cosmetic` / `structural`. Move endpoint `summary` text changes out of `changes` into a `summary_only` list — doc churn must not read as a contract change. |

Two new stdlib-only scripts: **`tools/render-drift-report.py`** (four JSON files → full Markdown
report + size-capped issue body) and **`tools/render-sync-status.py`** (`--write` / `--check` /
`--stdout`) which generates the README status block between
`<!-- sync-status:start -->` / `<!-- sync-status:end -->` markers, so the hand-maintained counts that
went stale cannot go stale again.

## Part 4 — The drift workflow (replaces `api-sync-check.yml`)

**Shape:** daily cron 06:00 UTC + `workflow_dispatch` (`dry_run`, `force_issue`). Fetch the live spec
**once**; every checker receives it via `--spec-file`. `check-api-sync` classifies the drift, and the
classification picks the branch:

| Classification | Meaning | Action |
|---|---|---|
| `none` | content hash unchanged | nothing |
| `cosmetic` | hash moved, zero endpoint/schema-property deltas (description & example churn — this is common) | `--update-baseline` + bot commit to `main`. No issue, no noise. |
| `structural` | any op added/removed/changed, or any schema property/required delta | run all three SDK checkers against the live spec → render a full report → create/update **one** `api-sync` issue whose body is a complete implementation spec → assign `copilot-swe-agent` |

**Why an issue and not a PR:** the issue body *is* the hand-off artifact, and it works identically
whether Copilot picks it up or you point a local agent at it. A bot-authored PR would also have to
push a baseline refresh to a branch before anyone had reviewed whether the drift is real.

**Anti-spam:** the body carries a hidden `<!-- api-sync-fp:<hash> -->` marker. If an open `api-sync`
issue already reflects the current live hash, the run is a no-op; if the hash moved, the body is
replaced in place and a dated comment notes the refresh. One issue, always current.

**Copilot assignment** resolves the bot's node id via `suggestedActors(capabilities:[CAN_BE_ASSIGNED])`,
reads the current assignees (because `replaceActorsForAssignable` replaces the whole set — a naive
call would silently unassign humans), then assigns. The step is `continue-on-error`, and a following
step comments the actual outcome on the issue: assigned to Copilot / not assigned because the secret
is missing / assignment failed. In the latter two cases the issue is explicitly labelled ready for a
local agent.

**Reports are artifact-only** (90-day retention) rather than committed to `docs/api-sync/drift/`.
Committing would mean pushing to `main` outside a PR, which would force a broader bot bypass on the
branch ruleset and weaken the guarantee that only *cosmetic* drift ever lands unreviewed.

<details>
<summary><strong>Full proposed <code>.github/workflows/api-sync-check.yml</code></strong> (click to expand)</summary>

```yaml
name: Samsara API Sync Check

on:
  schedule:
    - cron: '0 6 * * *'          # daily 06:00 UTC
  workflow_dispatch:
    inputs:
      dry_run:
        description: 'Run all checks but do not open/update issues or commit the baseline'
        type: boolean
        default: false
      force_issue:
        description: 'Open/update the issue even if drift is only cosmetic (or none)'
        type: boolean
        default: false

permissions:
  contents: write     # cosmetic-drift baseline commit to main
  issues: write       # create/update the api-sync issue

concurrency:
  group: api-sync-check
  cancel-in-progress: false

env:
  SPEC_URL: https://developers.samsara.com/openapi/samsara-api.json
  BASELINE: .github/cache/samsara-api-baseline.json
  # secrets are not usable in `if:`; mirror presence into env
  HAS_COPILOT_TOKEN: ${{ secrets.COPILOT_ASSIGN_TOKEN != '' }}

jobs:
  drift:
    name: Detect Samsara spec drift
    runs-on: ubuntu-latest
    outputs:
      classification: ${{ steps.classify.outputs.classification }}
      new_hash: ${{ steps.classify.outputs.new_hash }}
      old_hash: ${{ steps.classify.outputs.old_hash }}
      new_version: ${{ steps.classify.outputs.new_version }}
      old_version: ${{ steps.classify.outputs.old_version }}

    steps:
      - name: Checkout
        uses: actions/checkout@v4
        with:
          fetch-depth: 1

      - name: Set up Python
        uses: actions/setup-python@v5
        with:
          python-version: '3.12'

      - name: Fetch live spec (once)
        run: |
          mkdir -p "$RUNNER_TEMP/sync"
          curl -fsSL --retry 3 --retry-delay 10 \
            -A "SamsaraApiSyncChecker/2.0 (github.com/${{ github.repository }})" \
            -o "$RUNNER_TEMP/sync/live-spec.json" "$SPEC_URL"
          python3 -c "import json,sys; json.load(open(sys.argv[1]))" "$RUNNER_TEMP/sync/live-spec.json"
          echo "LIVE_SPEC=$RUNNER_TEMP/sync/live-spec.json" >> "$GITHUB_ENV"
          echo "SYNC_DIR=$RUNNER_TEMP/sync" >> "$GITHUB_ENV"

      - name: Diff live spec against baseline
        id: apisync
        run: |
          python3 tools/check-api-sync.py \
            --spec-file "$LIVE_SPEC" \
            --baseline "$BASELINE" \
            --no-report \
            --summary-json "$SYNC_DIR/api-sync-summary.json"

      - name: Classify drift
        id: classify
        run: |
          python3 - "$SYNC_DIR/api-sync-summary.json" >> "$GITHUB_OUTPUT" <<'PY'
          import json, sys
          s = json.load(open(sys.argv[1]))
          print(f"classification={s['classification']}")
          print(f"old_hash={s['old_fingerprint']['hash']}")
          print(f"new_hash={s['new_fingerprint']['hash']}")
          print(f"old_version={s['old_version']}")
          print(f"new_version={s['new_version']}")
          PY

      # ---------- STRUCTURAL: run every SDK checker against the LIVE spec ----------
      - name: SDK checkers vs live spec (non-gating; produce findings)
        if: steps.classify.outputs.classification == 'structural' || inputs.force_issue == true
        run: |
          set +e
          python3 tools/check-sdk-sync.py --spec-file "$LIVE_SPEC" --show-missing \
            --json-file "$SYNC_DIR/sdk-sync.json" > "$SYNC_DIR/sdk-sync.txt" 2>&1
          python3 tools/check-sdk-fabrication.py --spec-file "$LIVE_SPEC" \
            --json-file "$SYNC_DIR/fabrication.json" > "$SYNC_DIR/fabrication.txt" 2>&1
          python3 tools/check-model-sync.py --spec-file "$LIVE_SPEC" --by-domain \
            --json-file "$SYNC_DIR/model-sync.json" > "$SYNC_DIR/model-sync.txt" 2>&1
          set -e

      - name: Build full drift report + issue body
        if: steps.classify.outputs.classification == 'structural' || inputs.force_issue == true
        run: |
          python3 tools/render-drift-report.py \
            --summary "$SYNC_DIR/api-sync-summary.json" \
            --sdk-sync "$SYNC_DIR/sdk-sync.json" \
            --fabrication "$SYNC_DIR/fabrication.json" \
            --model-sync "$SYNC_DIR/model-sync.json" \
            --run-url "${{ github.server_url }}/${{ github.repository }}/actions/runs/${{ github.run_id }}" \
            --full-out "$SYNC_DIR/drift-report.md" \
            --issue-out "$SYNC_DIR/issue-body.md" \
            --issue-max-bytes 60000

      - name: Upload drift report artifact
        if: steps.classify.outputs.classification != 'none' || inputs.force_issue == true
        uses: actions/upload-artifact@v4
        with:
          name: api-sync-drift-${{ github.run_id }}
          path: |
            ${{ env.SYNC_DIR }}/*.json
            ${{ env.SYNC_DIR }}/*.md
            ${{ env.SYNC_DIR }}/*.txt
          retention-days: 90

      - name: Create or update the api-sync issue
        if: (steps.classify.outputs.classification == 'structural' || inputs.force_issue == true) && inputs.dry_run != true
        id: issue
        uses: actions/github-script@v7
        env:
          ISSUE_BODY_PATH: ${{ env.SYNC_DIR }}/issue-body.md
          NEW_HASH: ${{ steps.classify.outputs.new_hash }}
          OLD_HASH: ${{ steps.classify.outputs.old_hash }}
          NEW_VERSION: ${{ steps.classify.outputs.new_version }}
          OLD_VERSION: ${{ steps.classify.outputs.old_version }}
        with:
          script: |
            const fs = require('fs');
            const body = fs.readFileSync(process.env.ISSUE_BODY_PATH, 'utf8');
            const marker = `<!-- api-sync-fp:${process.env.NEW_HASH} -->`;
            const title = process.env.OLD_VERSION !== process.env.NEW_VERSION
              ? `[api-sync] Samsara API ${process.env.OLD_VERSION} → ${process.env.NEW_VERSION}: implement spec drift`
              : `[api-sync] Samsara spec drift (${process.env.NEW_VERSION}, content ${process.env.OLD_HASH} → ${process.env.NEW_HASH}): implement`;
            const {owner, repo} = context.repo;

            const open = await github.paginate(github.rest.issues.listForRepo, {
              owner, repo, labels: 'api-sync', state: 'open', per_page: 100,
            });
            const existing = open.find(i => !i.pull_request);

            let issue;
            if (!existing) {
              const res = await github.rest.issues.create({
                owner, repo, title, body: `${marker}\n${body}`, labels: ['api-sync'],
              });
              issue = res.data;
            } else if ((existing.body || '').includes(marker)) {
              issue = existing;
              core.info(`#${issue.number} already reflects live hash; no update`);
              core.setOutput('changed', 'false');
            } else {
              const res = await github.rest.issues.update({
                owner, repo, issue_number: existing.number, title, body: `${marker}\n${body}`,
              });
              issue = res.data;
              await github.rest.issues.createComment({
                owner, repo, issue_number: issue.number,
                body: `Spec drift refreshed on ${new Date().toISOString().slice(0,10)} — live content hash is now \`${process.env.NEW_HASH}\`.`,
              });
            }
            core.setOutput('number', String(issue.number));
            core.setOutput('node_id', issue.node_id);
            core.setOutput('assigned_copilot', String((issue.assignees || []).some(a => a.login === 'copilot-swe-agent')));
            core.setOutput('changed', 'true');

      - name: Assign issue to Copilot coding agent
        if: steps.issue.outputs.number != '' && steps.issue.outputs.assigned_copilot != 'true' && env.HAS_COPILOT_TOKEN == 'true' && inputs.dry_run != true
        id: assign
        uses: actions/github-script@v7
        continue-on-error: true
        with:
          github-token: ${{ secrets.COPILOT_ASSIGN_TOKEN }}
          script: |
            const {owner, repo} = context.repo;
            const issueId = '${{ steps.issue.outputs.node_id }}';
            const issueNumber = Number('${{ steps.issue.outputs.number }}');

            // 1) resolve the Copilot bot's node id for THIS repo
            const q = await github.graphql(`
              query($owner:String!, $name:String!) {
                repository(owner:$owner, name:$name) {
                  suggestedActors(capabilities:[CAN_BE_ASSIGNED], first:100) {
                    nodes { login __typename ... on Bot { id } ... on User { id } }
                  }
                }
              }`, { owner, name: repo });
            const bot = q.repository.suggestedActors.nodes.find(n => n.login === 'copilot-swe-agent');
            if (!bot) {
              core.setFailed('copilot-swe-agent is not assignable on this repo');
              return;
            }

            // 2) keep existing human assignees; the mutation REPLACES the whole set
            const cur = await github.graphql(`
              query($owner:String!, $name:String!, $n:Int!) {
                repository(owner:$owner, name:$name) { issue(number:$n) { assignees(first:20) { nodes { id login } } } }
              }`, { owner, name: repo, n: issueNumber });
            const keep = cur.repository.issue.assignees.nodes.map(a => a.id);

            // 3) assign
            await github.graphql(`
              mutation($assignableId:ID!, $actorIds:[ID!]!) {
                replaceActorsForAssignable(input:{assignableId:$assignableId, actorIds:$actorIds}) {
                  assignable { ... on Issue { number assignees(first:20) { nodes { login } } } }
                }
              }`, { assignableId: issueId, actorIds: [...new Set([...keep, bot.id])] });

      - name: Note assignment outcome on the issue
        if: steps.issue.outputs.number != '' && steps.issue.outputs.changed == 'true' && inputs.dry_run != true
        uses: actions/github-script@v7
        env:
          ASSIGN_OUTCOME: ${{ steps.assign.outcome }}          # success | failure | skipped
        with:
          script: |
            const n = Number('${{ steps.issue.outputs.number }}');
            const o = process.env.ASSIGN_OUTCOME;
            let msg;
            if (o === 'success') msg = 'Assigned to **@copilot-swe-agent** — the Copilot coding agent will open a PR from this issue.';
            else if (o === 'skipped' && process.env.HAS_COPILOT_TOKEN !== 'true') msg = 'Not assigned to Copilot: secret `COPILOT_ASSIGN_TOKEN` is not configured. This issue is ready for a local agent / human — follow the steps in the body.';
            else if (o === 'skipped') msg = 'Copilot already assigned or nothing to do.';
            else msg = 'Copilot assignment **failed** (see run log). This issue is ready for a local agent / human — follow the steps in the body.';
            await github.rest.issues.createComment({ ...context.repo, issue_number: n, body: msg });

      # ---------- COSMETIC: refresh baseline directly on main ----------
      - name: Refresh baseline (cosmetic drift only)
        if: steps.classify.outputs.classification == 'cosmetic' && inputs.dry_run != true && github.ref == 'refs/heads/main'
        run: |
          python3 tools/check-api-sync.py --spec-file "$LIVE_SPEC" --baseline "$BASELINE" --no-report --update-baseline
          git config user.name  "github-actions[bot]"
          git config user.email "41898282+github-actions[bot]@users.noreply.github.com"
          git add "$BASELINE"
          if git diff --cached --quiet; then echo "baseline unchanged"; exit 0; fi
          git commit -m "chore(api-sync): refresh baseline (cosmetic drift ${{ steps.classify.outputs.old_hash }} → ${{ steps.classify.outputs.new_hash }})" \
                     -m "No structural change (0 endpoint / schema-property deltas). Description/example churn only. Auto-committed by api-sync-check."
          git push origin HEAD:main

      - name: Summary
        if: always()
        run: |
          {
            echo "## Samsara API sync — ${{ steps.classify.outputs.classification || 'error' }}"
            echo "- baseline: \`${{ steps.classify.outputs.old_hash }}\` (v${{ steps.classify.outputs.old_version }})"
            echo "- live:     \`${{ steps.classify.outputs.new_hash }}\` (v${{ steps.classify.outputs.new_version }})"
            echo "- issue:    ${{ steps.issue.outputs.number && format('#{0}', steps.issue.outputs.number) || 'n/a' }}"
            echo "- copilot:  ${{ steps.assign.outcome || 'skipped' }}"
          } >> "$GITHUB_STEP_SUMMARY"
```

</details>

### The issue body — what Copilot (or a local agent) receives

```markdown
<!-- api-sync-fp:{new_hash} -->
## Samsara API drift — implementation spec

**Baseline** `{old_version}` · content `{old_hash}` → **Live** `{new_version}` · content `{new_hash}`
**Detected** {date} by [api-sync-check run]({run_url}) · full report: run artifact `api-sync-drift-{run_id}`
**Classification:** structural — {added} new / {removed} removed / {changed} changed ops;
{schemas_added} schemas added, {schemas_removed} removed, {schemas_changed} with property/required deltas

### 1. Endpoint diff
#### New operations ({added})        ← grouped by spec tag; `VERB /path` — summary (`operationId`) [deprecated]
#### Removed operations ({removed})
#### Changed operations ({changed})  ← param/body/deprecation deltas per op
#### Schema property deltas          ← `SchemaName: +props a, b; −props c; +required d`

### 2. SDK state vs the LIVE spec (checkers run with `--spec-file <live>`)
- `check-sdk-sync`: matched {matched}/{spec_ops}; **unimplemented {missing}**; mismatched {mismatched}
- `check-sdk-fabrication`: duplicates {dups}, tag-drift {drift}
- `check-model-sync`: CRIT {c} / HIGH {h} / MED {m} / LOW {l} active (allowlisted {a})
#### Unimplemented spec operations   ← by tag: `VERB /path` `operationId` [DEP]
#### SDK endpoint mismatches         ← `File::Method` → `VERB /path` no longer in the live spec
#### Model findings                  ← `[SEV] type :: Record.prop — detail` (+ first 3 endpoints)

### 3. Steps to close this issue (all in ONE PR)
1. `python3 tools/check-api-sync.py --update-baseline` FIRST — the baseline diff IS the reviewable spec change.
2. Implement every item in §2: client methods + interfaces, records under `src/Samsara.Sdk/Models/**`,
   register every new type in `SamsaraJsonContext.cs`. Follow "Adding a New Domain" in `docs/api-sync/README.md`.
3. If a method legitimately reaches a new tag, run `check-sdk-fabrication.py --update-tags` and review the diff.
4. All four checkers green against the refreshed baseline (exact commands listed).
5. `dotnet build && dotnet test` green; add contract tests for new records.
6. Update `docs/api-sync/NN-*.md`, regenerate the README status block, add a `CHANGELOG.md` entry.
7. Open the PR with `Closes #{n}`; the `sdk-sync` required check must pass.

**Do not** hand-edit the baseline or allowlist a finding to get green — allowlist entries require a
spec pointer proving the deviation is intentional.

_Assignment: {assigned to @copilot-swe-agent | ready for a local agent — COPILOT_ASSIGN_TOKEN not configured | assignment failed}._
```

That last paragraph is deliberate: the most likely failure mode of an automated loop like this is an
agent silencing a checker to turn the build green. The instruction is stated in the artifact the
agent actually reads, and the "spec pointer required" rule makes a silenced finding visible in review.

## Part 5 — Documentation reconciliation (D10)

`docs/api-sync/README.md` currently claims "317 / 317 spec operations covered (100%)" and
`matched=323, mismatched=0, missing=0`. Against the live spec the truth is 324 matched, 1 mismatched,
57 missing. Sections to rewrite:

| Section | Problem | Rewrite to |
|---|---|---|
| Current Status | Stale, hand-maintained | Generated `<!-- sync-status:start/end -->` block; `ci.yml` runs `render-sync-status.py --check` so it cannot rot |
| Domain table Notes | Free text partly obsolete (Beta "`object?` pending typed schemas", CPA "model still flattened", Safety "SafetyEvent still a v2 stub") | Keep the table as the human index; strip every status claim a checker now proves; keep only pointers and quirks |
| Running the check locally | Flags out of date | New canonical commands; state the rule **CI = baseline (hermetic), daily workflow = live** |
| "Why four checkers?" | Says MEDIUM/LOW are "the deferred backlog" | MEDIUM now gates; `weak-typing` and `flattened` defined; allowlist entries require a spec pointer |
| Baseline discipline | Says refresh is "never automatic" — no longer strictly true | State the three classifications explicitly: cosmetic is absorbed automatically by the daily workflow (auditable via `git log -- .github/cache/`); **structural is never auto-absorbed** — it produces an issue, and the baseline moves only inside the implementation PR, as its first commit |
| Weekly Automated Check | Weekly, issue-only | Rename **Daily**; document the classification branches, Copilot assignment, `dry_run`/`force_issue`, and the required repo setup |

The invariant you asked to preserve survives intact: `main`'s baseline still only advances alongside
reconciled code, *except* for provably-cosmetic churn, which by definition changes no contract.

## Part 6 — Sequencing

Two PRs, matching the agreed shape (workflow first, then one integration branch):

**PR A — tooling + workflow** (small, independent, no gate flips)
All checker changes M1–M7 / S1–S4 / F1 / A1–A9, the two new render scripts, the new workflow, the
README rewrite, and **the baseline refresh** (required in this PR: pinning CI to the baseline without
refreshing it would fail `--fail-on-mismatch` on the seven already-shipped methods). Gates stay at
their current thresholds so this PR is green on merge. The new workflow is safe to enable here — on
the structural path it only reads and files an issue.

**PR B — `feature/spec-parity-2026-08` → v0.5.0** (the breaking sweep)
Per-domain commits in this order, each independently reviewable:
1. `PreviewApisClient` tachograph move + `[Experimental]` + shim (D6/OD6); stale `IGatewaysClient` comment.
2. The 3 new query params (`sourceName`, `assetTypes` ×2).
3. **The `UserRole` flattening bug** (D5, §2.3) + delete the orphaned `Dvir*` quartet. This is a real
   data-loss defect, not cleanup, so it lands early and separately with its own regression test.
4. Retype the 93 weak properties (D3), grouped by Models file. Alerts/Workflow child objects as their
   own commits (OD7).
5. Type the 57 Beta/Legacy/Preview weak method signatures (D4), plus the 44 non-Beta ones if OD8 is approved.
6. The 57 new endpoints (D2), grouped by the 13 domains, each with its models + `SamsaraJsonContext` registration.
7. Contract tests, `docs/api-sync/NN-*.md` + regenerated status block, CHANGELOG with a consolidated
   "Migration to v0.5.0" section.
8. **Final commit:** flip the gates — `check-model-sync --fail-on-severity MEDIUM`,
   `check-sdk-sync --fail-on-unimplemented`. Flipping earlier turns CI red for the whole series.

Then: tag `v0.5.0`, and enable the `main` ruleset with `sdk-sync` required (OD3) once it is green.

## Verification — what "done" means

Every one of these must hold at the end of PR B, against the refreshed baseline:

- `check-sdk-sync --fail-on-mismatch --fail-on-unimplemented` → 382/382, 0 mismatched, 0 unimplemented
- `check-sdk-fabrication --fail-on-issues` → 0 duplicate coverage, 0 tag drift
- `check-model-sync --fail-on-severity MEDIUM` → 0 CRIT / 0 HIGH / 0 MEDIUM; allowlist = 15 entries, each with a spec pointer
- `check-api-sync --fail-on-diff` → no-op
- Zero `JsonElement` / `object` in `src/Samsara.Sdk/Models/**` except the 3 free-form Reading
  properties (under OD7-B, plus the two Alerts properties under a `deferred` allowlist entry)
- Zero `object` generic arguments in `src/Samsara.Sdk/Clients/Beta/**`, `LegacyApisClient`,
  `PreviewApisClient` — and in every other client if OD8 is approved
- `User.roles` round-trips correctly (the `UserRole` flattening bug in §2.3 is fixed and covered by a test)
- `dotnet build` (net8.0 + netstandard2.0) and `dotnet test` green, with contract tests covering the new records
- `render-sync-status.py --check` clean

## Risks

| Risk | Mitigation |
|---|---|
| **Scale.** ~148 distinct new schemas + 93 retypings + 94 endpoint bodies is the largest change this SDK has taken. | Per-domain commits; checkers verify each mechanically rather than by eye. |
| **The spec will drift mid-flight** (it changed under a frozen version already). | Re-fetch and re-verify all four checkers immediately before merge — the same discipline used for v0.3.0. |
| **Beta/Preview instability.** Typing beta surfaces means breaking when Samsara changes them. | `[Experimental]` + namespace separation communicate the risk; the daily workflow catches the change the day it ships. |
| **Source-gen trap.** STJ source generation does not honour C# property initializers the way reflection did (this bit the SDK before, in `MediaClient`). | Every new record must be registered in `SamsaraJsonContext`, and null-coalescing must be explicit; the existing `EveryModelType_IsRegisteredInSourceGenContext` test enforces registration. |
| **Deserialization must stay lenient.** The live API omits fields its own spec marks `required`. | Unchanged: `SamsaraSerializerOptions.Default` stays lenient; `Strict` remains opt-in conformance validation only. Typing more fields does not change this. |
| **An agent silences a checker to go green.** | Allowlist entries require a spec pointer; the issue template says so explicitly; review the allowlist diff on every api-sync PR. |
