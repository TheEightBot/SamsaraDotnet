# Samsara API Sync — Documentation & Process

This folder contains a checklist per API domain for tracking the sync state between the
[Samsara OpenAPI spec](https://developers.samsara.com/openapi/samsara-api.json) and this
.NET SDK (`Samsara.Sdk`).

## Current Status

> **⚠️ 2026-05-21 full-spec audit**: a mechanical SDK-vs-spec comparison found that the
> statuses below were filled by *intent*, not verified against live URL paths. Roughly **1 in
> 3 wired endpoints does not match the spec** (wrong path or fabricated). The table below has
> been corrected; see **[full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md)** for
> the authoritative findings and proposed changes. Per-endpoint checkboxes in the individual
> files are *not* yet updated — they will be reconciled as fixes land.

> **API Version**: `2025-10-23`  
> **Last Sync**: 2026-05-21 (full audit; spec content drifted since the cached baseline — see [full review](full-sync-review-2026-05-21.md) Part 4)  
> **Overall Progress**: ~142 / 311 operations correctly wired (46%) — the spec has 311 operations (was counted as 307)

> Status/`Matched` columns below are corrected as of the 2026-05-21 audit. `Matched` = SDK
> endpoints whose verb+path exactly match a spec operation. "Notes" flags wrong-path,
> fabricated, or model issues; see [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).

| # | Domain | Status | Matched | Notes (2026-05-21 audit) |
|---|--------|--------|---------|--------------------------|
| 01 | [Addresses](01-addresses.md) | ✅ Complete | 5/5 | model: `CreateAddressRequest` required fields, missing `contacts` |
| 02 | [Alerts](02-alerts.md) | 🔴 Broken | 3/5 | top-level `/alerts` CRUD + incidents list/get fabricated; spec is configurations-only |
| 03 | [Assets](03-assets.md) | 🟡 Partial | 4/8 | missing v1 location/reefer ops; new `includeAttributes` param |
| 04 | [Attributes](04-attributes.md) | ✅ Complete | 5/5 | model: required `attributeType`/`entityType`, missing `entities`/`unit` |
| 05 | [Auth Token for Driver](05-auth-token-for-driver.md) | ✅ Complete | 1/1 | — |
| 06 | [Beta APIs](06-beta-apis.md) | ❌ Not Started | 0/70 | incl. new **Places** domain |
| 07 | [CARB CTC](07-carb-ctc.md) | ✅ Complete | 2/2 | — |
| 08 | [Carrier Proposed Assignments](08-carrier-proposed-assignments.md) | 🔴 Broken | 3/3 | `UpdateAsync` fabricated; model flattened vs nested spec shape |
| 09 | [Coaching](09-coaching.md) | ✅ Complete | 3/3 | — |
| 10 | [Contacts](10-contacts.md) | 🟡 Partial | 2/5 | model: `Contact` required fields modelled optional |
| 11 | [Documents](11-documents.md) | ✅ Complete | 7/7 | — |
| 12 | [Driver QR Codes](12-driver-qr-codes.md) | ✅ Complete | 3/3 | — |
| 13 | [Driver-Trailer Assignments](13-driver-trailer-assignments.md) | ✅ Complete | 3/3 | — |
| 14 | [Driver-Vehicle Assignments](14-driver-vehicle-assignments.md) | 🔴 Broken | 2/4 | `Update`/`Delete` put id in path (spec: id in query/body); 2 ops missing |
| 15 | [Drivers](15-drivers.md) | ✅ Complete | 5/5 | `DeleteAsync` fabricated; `CreateDriverRequest.username` required |
| 16 | [Equipment](16-equipment.md) | 🟡 Partial | 7/8 | `Create`/`Delete` fabricated; `Update`→`/beta/...`; missing stats snapshot |
| 17 | [Forms](17-forms.md) | 🟡 Partial | 3/7 | `fleet/forms/*`→`/form-*`; missing POST/PATCH submission |
| 18 | [Fuel and Energy](18-fuel-and-energy.md) | 🔴 Broken | 0/5 | client hits non-existent `/fleet/vehicles/fuel/*`; needs rework |
| 19 | [Gateways](19-gateways.md) | 🔴 Broken | 0/3 | `fleet/gateways`→`/gateways`; get-by-id fabricated; missing create/delete |
| 20 | [Hours of Service](20-hours-of-service.md) | 🟡 Partial | 3/6 | `clocks`/`eld-events` wrong path; missing v1 duty_status/auth-logs |
| 21 | [Hubs](21-hubs.md) | 🟡 Partial | 6/7 | 1 op missing |
| 22 | [IFTA](22-ifta.md) | 🔴 Broken | 0/4 | detail/summary not in spec; jobs→`/ifta-detail/csv`; needs rework |
| 23 | [Idling](23-idling.md) | ✅ Complete | 1/1 | — |
| 24 | [Industrial](24-industrial.md) | 🟡 Partial | 4/17 | `industrial/data`→`/industrial/data-inputs`; many gaps |
| 25 | [Issues](25-issues.md) | 🔴 Broken | 2/3 | `Get`/`Update` use `/issues/{id}`; spec is `/issues` (id as query/body) |
| 26 | [Legacy](26-legacy.md) | ❌ Not Started | 0/1 | — |
| 27 | [Legacy APIs](27-legacy-apis.md) | ❌ Not Started | 0/8 | — |
| 28 | [Live Sharing Links](28-live-sharing-links.md) | ✅ Complete | 4/4 | — |
| 29 | [Location and Speed](29-location-and-speed.md) | ✅ Complete | 1/1 | — |
| 30 | [Maintenance](30-maintenance.md) | 🔴 Broken | 3/9 | most DVIR/defect paths wrong; DVIRs duplicated w/ Compliance; DTCs fabricated |
| 31 | [Media](31-media.md) | 🔴 Broken | 0/3 | spec is `/cameras/media`; get-by-id fabricated |
| 32 | [Messages](32-messages.md) | 🔴 Broken | 0/2 | `fleet/messages`→`/v1/fleet/messages` |
| 33 | [Organization Info](33-organization-info.md) | ✅ Complete | 1/1 | model: extra address fields not in spec |
| 34 | [Plans](34-plans.md) | ✅ Complete | 3/3 | — |
| 35 | [Preview APIs](35-preview-apis.md) | ❌ Not Started | 0/4 | — |
| 36 | [Readings](36-readings.md) | ✅ Complete | 3/3 | already correct (index previously understated) |
| 37 | [Route Events](37-route-events.md) | ❌ Not Started | 0/1 | — |
| 38 | [Routes](38-routes.md) | 🟡 Partial | 5/8 | `GetAuditLog`→`/fleet/routes/audit-logs/feed` |
| 39 | [Safety](39-safety.md) | 🟡 Partial | 2/4 | `GetEvent` by-id fabricated; `SafetyEvent` model is a v2 stub |
| 40 | [Safety Scores](40-safety-scores.md) | ✅ Complete | 4/4 | — |
| 41 | [Sensors](41-sensors.md) | 🔴 Broken | 0/6 | spec sensors are v1 POST endpoints; client mis-pathed; needs rework |
| 42 | [Settings](42-settings.md) | ✅ Complete | 5/5 | — |
| 43 | [Speeding Intervals](43-speeding-intervals.md) | ✅ Complete | 1/1 | — |
| 44 | [Tachograph (EU Only)](44-tachograph-eu-only.md) | 🔴 Broken | 0/3 | missing `/history` suffix; missing vehicle files |
| 45 | [Tags](45-tags.md) | 🔴 Broken | 0/6 | all use `fleet/tags`; spec is `/tags`; model missing externalIds/parentTag |
| 46 | [Trailer Assignments](46-trailer-assignments.md) | 🔴 Broken | 0/2 | `fleet/trailer-assignments`→`/v1/fleet/trailers/assignments` |
| 47 | [Trailers](47-trailers.md) | ✅ Complete | 8/8 | — |
| 48 | [Training Assignments](48-trainingassignments.md) | 🔴 Broken | 0/4 | `fleet/training/assignments`→`/training-assignments/stream`; 3 ops missing |
| 49 | [Training Courses](49-trainingcourses.md) | 🔴 Broken | 0/1 | `fleet/training/courses`→`/training-courses` |
| 50 | [Trips](50-trips.md) | 🟡 Partial | 1/2 | `ListAsync` hits `/fleet/vehicles/trips` (not in spec)→`/v1/fleet/trips` |
| 51 | [Users](51-users.md) | ✅ Complete | 6/6 | model: required fields modelled optional |
| 52 | [Vehicle Locations](52-vehicle-locations.md) | ✅ Complete | 3/3 | model: `VehicleLocation` missing `reverseGeo`, required lat/long/time |
| 53 | [Vehicle Stats](53-vehicle-stats.md) | ✅ Complete | 3/3 | — |
| 54 | [Vehicles](54-vehicles.md) | ✅ Complete | 3/3 | `Create`/`Delete` fabricated; `Vehicle` model missing ~17 fields |
| 55 | [Webhooks](55-webhooks.md) | ✅ Complete | 5/5 | — |
| 56 | [Work Orders](56-work-orders.md) | ✅ Complete | 7/7 | — |

**Legend**:
- ✅ Complete — all spec operations in this domain are correctly wired (may still have model drift; see Notes)
- 🟡 Partial — some operations wired correctly, some missing
- 🔴 Broken — endpoints are wired but the URL path is wrong or the method is fabricated (runtime 404/405); see full review
- ❌ Not Started — no current spec endpoints implemented

---

## How to Use This Process

### Running the Sync Check Locally

```bash
# Fetch the latest spec and compare against checklists
python3 tools/check-api-sync.py

# Check against a specific spec version
python3 tools/check-api-sync.py --spec-url https://developers.samsara.com/openapi/samsara-api.json
```

The script outputs:
- New endpoints added since last check
- Removed/deprecated endpoints
- Changed parameter signatures
- A summary diff report at `docs/api-sync/diff-report.md`

### Updating Checklists After Implementation

When implementing an endpoint, update its checklist file by checking off the relevant items:

```markdown
- [x] Method defined in `IFooClient`
- [x] Method implemented in `FooClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage
```

Also update the status line at the top of the file and the table in this README.

### Adding a New Domain

1. Identify the API tag(s) that map to the new domain
2. Create `I<Domain>Client.cs` interface
3. Create `<Domain>Client.cs` implementation
4. Create `Models/<Domain>/<Domain>Models.cs`
5. Register in `ISamsaraClient` and `SamsaraClient`
6. Register in `SamsaraJsonContext.cs`
7. Create or update the checklist file in `docs/api-sync/`
8. Update this README index table

### Weekly Automated Check

A GitHub Actions workflow (`.github/workflows/api-sync-check.yml`) runs every Monday at 08:00 UTC:

1. Downloads the latest Samsara OpenAPI spec
2. Compares it against the last-known spec (cached in the workflow)
3. If differences are found, opens a GitHub Issue with the diff summary
4. The issue is labeled `api-sync` and assigned for review

---

## Versioning

The Samsara API version is embedded in the spec's `info.version` field. When a new version
is detected by the workflow, the issue title includes the version change (e.g.,
`[api-sync] Samsara API updated: 2025-10-23 → 2026-04-01`).

After implementing changes for a new API version, update:
1. The `API Version` in this README
2. The `Last Sync` date
3. The `Overall Progress` count
4. Each affected checklist file's status
5. `CHANGELOG.md` with a new entry describing the changes

---

## File Structure

```
docs/api-sync/
├── README.md                     ← This file (index + process guide)
├── 01-addresses.md
├── 02-alerts.md
├── ...
└── 56-work-orders.md
```

Each checklist file follows this structure:
- **Header** — API version, status, SDK client mappings
- **Endpoints** — Per-endpoint checklist (interface, impl, models, serialization, tests)
- **Models** — Domain model quality checklist
- **Notes** — Implementation notes, breaking changes, special considerations
