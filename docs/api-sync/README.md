# Samsara API Sync — Documentation & Process

This folder contains a checklist per API domain for tracking the sync state between the
[Samsara OpenAPI spec](https://developers.samsara.com/openapi/samsara-api.json) and this
.NET SDK (`Samsara.Sdk`).

## Current Status

> **✅ Full spec parity (resolved on `feature/api-sync-full-2026-05`)**: every operation in
> the **317**-operation 2025-10-23 spec is covered by at least one SDK method.
> `tools/check-sdk-sync.py` reports `matched=323, mismatched=0, missing=0`; an independent
> agent verification on 2026-05-27 reached the same conclusion. Every audit finding in
> [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md) has been resolved.

> **API Version**: `2025-10-23`  
> **Last Sync**: 2026-05-27 (full parity achieved)  
> **Overall Progress**: **317 / 317 spec operations covered (100%)**

> The `Status` and `Matched` columns below were corrected at the 2026-05-21 audit and now
> reflect the post-rework state. Some Beta domains (Places, PreferredStations, Qualification
> Records, Ridership, Functions, Reports, misc Beta) are wired to the correct endpoints but
> use `object?` for their request/response payloads pending typed schemas — this is called
> out in the per-domain notes.

| # | Domain | Status | Notes |
|---|--------|--------|-------|
| 01 | [Addresses](01-addresses.md) | ✅ Complete | — |
| 02 | [Alerts](02-alerts.md) | ✅ Complete | reworked to spec-correct configurations-only API + incidents stream |
| 03 | [Assets](03-assets.md) | ✅ Complete | adds v1 location/reefer + device-recovery + depreciation |
| 04 | [Attributes](04-attributes.md) | ✅ Complete | — |
| 05 | [Auth Token for Driver](05-auth-token-for-driver.md) | ✅ Complete | — |
| 06 | [Beta APIs](06-beta-apis.md) | ✅ Complete | Places, PreferredStations, QualificationRecords, Ridership, Functions, Reports, misc Beta — request/response models use `object?` pending typed schemas |
| 07 | [CARB CTC](07-carb-ctc.md) | ✅ Complete | — |
| 08 | [Carrier Proposed Assignments](08-carrier-proposed-assignments.md) | ✅ Complete | fabricated `Update` removed; model still flattened (follow-up) |
| 09 | [Coaching](09-coaching.md) | ✅ Complete | — |
| 10 | [Contacts](10-contacts.md) | ✅ Complete | added Create/Update/Delete |
| 11 | [Documents](11-documents.md) | ✅ Complete | — |
| 12 | [Driver QR Codes](12-driver-qr-codes.md) | ✅ Complete | — |
| 13 | [Driver-Trailer Assignments](13-driver-trailer-assignments.md) | ✅ Complete | — |
| 14 | [Driver-Vehicle Assignments](14-driver-vehicle-assignments.md) | ✅ Complete | Update/Delete reworked to body-based signatures |
| 15 | [Drivers](15-drivers.md) | ✅ Complete | fabricated `Delete` removed; workflows + voice sign-in added |
| 16 | [Equipment](16-equipment.md) | ✅ Complete | fabricated `Create/Delete` removed; stats snapshot added |
| 17 | [Forms](17-forms.md) | ✅ Complete | paths fixed; POST/PATCH submission + `ids[]` required filter |
| 18 | [Fuel and Energy](18-fuel-and-energy.md) | ✅ Complete | reworked to fuel-energy reports + driver-efficiency + `/fuel-purchase` |
| 19 | [Gateways](19-gateways.md) | ✅ Complete | reworked to `/gateways`; create/delete added |
| 20 | [Hours of Service](20-hours-of-service.md) | ✅ Complete | clocks reworked to nested shape; eld-events to beta path; v1 auth-logs + duty_status |
| 21 | [Hubs](21-hubs.md) | ✅ Complete | added `ListHubsAsync`, plan-orders/plan-routes/route-templates |
| 22 | [IFTA](22-ifta.md) | ✅ Complete | reworked to jurisdiction/vehicle reports + `/ifta-detail/csv` jobs |
| 23 | [Idling](23-idling.md) | ✅ Complete | — |
| 24 | [Industrial](24-industrial.md) | ✅ Complete | data-inputs path; assets CRUD; v1 vision + machines |
| 25 | [Issues](25-issues.md) | ✅ Complete | reworked to spec-correct `ids[]` filter; create/update use body |
| 26 | [Legacy](26-legacy.md) | ✅ Complete | `V1GetAllAssetsAsync` lives on `IAssetsClient` |
| 27 | [Legacy APIs](27-legacy-apis.md) | ✅ Complete | new `ILegacyApisClient` covers all 8 ops |
| 28 | [Live Sharing Links](28-live-sharing-links.md) | ✅ Complete | — |
| 29 | [Location and Speed](29-location-and-speed.md) | ✅ Complete | — |
| 30 | [Maintenance](30-maintenance.md) | ✅ Complete | DVIR/defect paths fixed; duplicate Compliance DVIRs removed; vendors added |
| 31 | [Media](31-media.md) | ✅ Complete | reworked to `/cameras/media` + `/cameras/media/retrieval` |
| 32 | [Messages](32-messages.md) | ✅ Complete | reworked to `/v1/fleet/messages` |
| 33 | [Organization Info](33-organization-info.md) | ✅ Complete | extra non-spec fields kept for back-compat |
| 34 | [Plans](34-plans.md) | ✅ Complete | — |
| 35 | [Preview APIs](35-preview-apis.md) | ✅ Complete | new `IPreviewApisClient` (vehicle lock/unlock, gateway pair, auth-token preview) |
| 36 | [Readings](36-readings.md) | ✅ Complete | POST `/readings` added |
| 37 | [Route Events](37-route-events.md) | ✅ Complete | new `IRouteEventsClient` |
| 38 | [Routes](38-routes.md) | ✅ Complete | audit-log path fixed; v1 dispatch delete added |
| 39 | [Safety](39-safety.md) | ✅ Complete | fabricated `GetEvent` removed; v1 driver/vehicle scores + batch patch added; SafetyEvent still a v2 stub (follow-up) |
| 40 | [Safety Scores](40-safety-scores.md) | ✅ Complete | — |
| 41 | [Sensors](41-sensors.md) | ✅ Complete | reworked to v1 POST endpoints + proper long-id models |
| 42 | [Settings](42-settings.md) | ✅ Complete | — |
| 43 | [Speeding Intervals](43-speeding-intervals.md) | ✅ Complete | — |
| 44 | [Tachograph (EU Only)](44-tachograph-eu-only.md) | ✅ Complete | `/history` suffix added; vehicle files + live data added |
| 45 | [Tags](45-tags.md) | ✅ Complete | BasePath `/tags`; `parentTag`/`externalIds` added |
| 46 | [Trailer Assignments](46-trailer-assignments.md) | ✅ Complete | path fixed to `/v1/fleet/trailers/assignments`; per-trailer added |
| 47 | [Trailers](47-trailers.md) | ✅ Complete | — |
| 48 | [Training Assignments](48-trainingassignments.md) | ✅ Complete | reworked path + Create/Update/Delete added |
| 49 | [Training Courses](49-trainingcourses.md) | ✅ Complete | reworked path |
| 50 | [Trips](50-trips.md) | ✅ Complete | List path fixed to `/v1/fleet/trips` |
| 51 | [Users](51-users.md) | ✅ Complete | required fields tightened |
| 52 | [Vehicle Locations](52-vehicle-locations.md) | ✅ Complete | `reverseGeo` + required lat/long/time |
| 53 | [Vehicle Stats](53-vehicle-stats.md) | ✅ Complete | — |
| 54 | [Vehicles](54-vehicles.md) | ✅ Complete | fabricated `Create/Delete` removed; +13 model fields; immobilizer added |
| 55 | [Webhooks](55-webhooks.md) | ✅ Complete | — |
| 56 | [Work Orders](56-work-orders.md) | ✅ Complete | — |

**Legend**:
- ✅ Complete — every spec operation in this domain is wired to an SDK method (verified against the live spec)

---

## How to Use This Process

### Running the Sync Check Locally

Three complementary checkers guard three different failure modes. **All three must be
green** — each is blind to what the others catch:

```bash
# 1. SPEC DRIFT — does the live spec differ from our cached baseline?
#    (new/removed/changed endpoints + schema additions/removals)
python3 tools/check-api-sync.py            # writes docs/api-sync/diff-report.md

# 2. COVERAGE — does every SDK path exist in the spec, and is every spec op implemented?
python3 tools/check-sdk-sync.py --fail-on-mismatch

# 3. FABRICATION / MIS-HOMING — does every SDK method map to a DISTINCT, correctly-homed
#    spec op? Catches the Hubs-class bug (a method pointed at another domain's path, e.g.
#    HubsClient CRUD secretly hitting /addresses) that COVERAGE reports as "0 mismatches".
python3 tools/check-sdk-fabrication.py --fail-on-issues
```

Why three? `check-sdk-sync.py` dedups SDK endpoints by `(verb, path)`, so a method
mis-homed to a *real* path in another domain is counted as coverage and never flagged.
`check-sdk-fabrication.py` adds the reverse check: **duplicate coverage** (one spec op
reached from >1 client file) and **client↔tag drift** (a method reaching a spec tag outside
its client's committed allow-set in `tools/sdk-client-tags.json`). After an intentional new
cross-domain method, refresh the allow-set with `--update-tags` and review the diff.

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
