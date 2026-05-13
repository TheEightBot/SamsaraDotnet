# Samsara API Sync — Documentation & Process

This folder contains a checklist per API domain for tracking the sync state between the
[Samsara OpenAPI spec](https://developers.samsara.com/openapi/samsara-api.json) and this
.NET SDK (`Samsara.Sdk`).

## Current Status

> **API Version**: `2025-10-23`  
> **Last Sync**: 2026-05-13  
> **Overall Progress**: 113 / 307 endpoints implemented (37%)

| # | Domain | Status | Implemented |
|---|--------|--------|-------------|
| 01 | [Addresses](01-addresses.md) | ✅ Complete | 5/5 |
| 02 | [Alerts](02-alerts.md) | ❌ Not Started | 0/5 |
| 03 | [Assets](03-assets.md) | ⚠️ Unverified | 0/8 |
| 04 | [Attributes](04-attributes.md) | ✅ Complete | 5/5 |
| 05 | [Auth Token for Driver](05-auth-token-for-driver.md) | ⚠️ Unverified | 0/1 |
| 06 | [Beta APIs](06-beta-apis.md) | ⚠️ Unverified | 0/66 |
| 07 | [CARB CTC](07-carb-ctc.md) | ⚠️ Unverified | 0/2 |
| 08 | [Carrier Proposed Assignments](08-carrier-proposed-assignments.md) | ✅ Complete | 3/3 |
| 09 | [Coaching](09-coaching.md) | ⚠️ Unverified | 0/3 |
| 10 | [Contacts](10-contacts.md) | ✅ Complete | 5/5 |
| 11 | [Documents](11-documents.md) | 🟡 Partial | 3/7 |
| 12 | [Driver QR Codes](12-driver-qr-codes.md) | ⚠️ Unverified | 0/3 |
| 13 | [Driver-Trailer Assignments](13-driver-trailer-assignments.md) | ⚠️ Unverified | 0/3 |
| 14 | [Driver-Vehicle Assignments](14-driver-vehicle-assignments.md) | ✅ Complete | 4/4 |
| 15 | [Drivers](15-drivers.md) | 🟡 Partial | 4/5 |
| 16 | [Equipment](16-equipment.md) | 🟡 Partial | 4/8 |
| 17 | [Forms](17-forms.md) | 🟡 Partial | 4/7 |
| 18 | [Fuel and Energy](18-fuel-and-energy.md) | ✅ Complete | 5/5 |
| 19 | [Gateways](19-gateways.md) | ✅ Complete | 3/3 |
| 20 | [Hours of Service](20-hours-of-service.md) | 🟡 Partial | 4/6 |
| 21 | [Hubs](21-hubs.md) | 🟡 Partial | 1/7 |
| 22 | [IFTA](22-ifta.md) | 🟡 Partial | 2/4 |
| 23 | [Idling](23-idling.md) | ⚠️ Unverified | 0/1 |
| 24 | [Industrial](24-industrial.md) | 🟡 Partial | 5/17 |
| 25 | [Issues](25-issues.md) | 🟡 Partial | 2/3 |
| 26 | [Legacy](26-legacy.md) | ⚠️ Unverified | 0/1 |
| 27 | [Legacy APIs](27-legacy-apis.md) | ⚠️ Unverified | 0/8 |
| 28 | [Live Sharing Links](28-live-sharing-links.md) | ⚠️ Unverified | 0/4 |
| 29 | [Location and Speed](29-location-and-speed.md) | ⚠️ Unverified | 0/1 |
| 30 | [Maintenance](30-maintenance.md) | 🟡 Partial | 2/9 |
| 31 | [Media](31-media.md) | 🟡 Partial | 1/3 |
| 32 | [Messages](32-messages.md) | ✅ Complete | 2/2 |
| 33 | [Organization Info](33-organization-info.md) | ✅ Complete | 1/1 |
| 34 | [Plans](34-plans.md) | ⚠️ Unverified | 0/3 |
| 35 | [Preview APIs](35-preview-apis.md) | ⚠️ Unverified | 0/4 |
| 36 | [Readings](36-readings.md) | ⚠️ Unverified | 0/3 |
| 37 | [Route Events](37-route-events.md) | ⚠️ Unverified | 0/1 |
| 38 | [Routes](38-routes.md) | 🟡 Partial | 5/8 |
| 39 | [Safety](39-safety.md) | ❌ Not Started | 0/4 |
| 40 | [Safety Scores](40-safety-scores.md) | ❌ Not Started | 0/4 |
| 41 | [Sensors](41-sensors.md) | ✅ Complete | 6/6 |
| 42 | [Settings](42-settings.md) | ⚠️ Unverified | 0/5 |
| 43 | [Speeding Intervals](43-speeding-intervals.md) | ⚠️ Unverified | 0/1 |
| 44 | [Tachograph (EU Only)](44-tachograph-eu-only.md) | ✅ Complete | 3/3 |
| 45 | [Tags](45-tags.md) | 🟡 Partial | 5/6 |
| 46 | [Trailer Assignments](46-trailer-assignments.md) | ✅ Complete | 2/2 |
| 47 | [Trailers](47-trailers.md) | 🟡 Partial | 5/8 |
| 48 | [Training Assignments](48-trainingassignments.md) | ✅ Complete | 4/4 |
| 49 | [Training Courses](49-trainingcourses.md) | ✅ Complete | 1/1 |
| 50 | [Trips](50-trips.md) | 🟡 Partial | 1/2 |
| 51 | [Users](51-users.md) | ✅ Complete | 6/6 |
| 52 | [Vehicle Locations](52-vehicle-locations.md) | 🟡 Partial | 1/3 |
| 53 | [Vehicle Stats](53-vehicle-stats.md) | 🟡 Partial | 1/3 |
| 54 | [Vehicles](54-vehicles.md) | ✅ Complete | 3/3 |
| 55 | [Webhooks](55-webhooks.md) | ✅ Complete | 5/5 |
| 56 | [Work Orders](56-work-orders.md) | ⚠️ Unverified | 0/7 |

**Legend**:
- ✅ Complete — all endpoints in this domain are implemented
- 🟡 Partial — some endpoints are implemented, some are missing
- ❌ Not Started — client exists but none of the current API endpoints are implemented
- ⚠️ Unverified — no SDK client exists for this domain yet

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
