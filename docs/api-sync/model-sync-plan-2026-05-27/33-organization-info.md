# Organization Info — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/33-organization-info.md`](../33-organization-info.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented on 2026-05-27**

## Implementation notes

All five LOW findings were on `OrganizationInfo` extras
(`address`, `city`, `state`, `zip`, `country`) on the `GET /me` response.
Per the established workflow precedent (`08-carrier-proposed-assignments`,
`13-driver-trailer-assignments`, `14-driver-vehicle-assignments`,
`28-live-sharing-links`, `29-location-and-speed`), each was retained as a
nullable back-compat property and annotated with an XML doc comment that:

1. Notes the field is **not part of the spec inner schema**.
2. Points callers to the canonical replacement where one exists
   (e.g. `Address` → `CarrierSettings.MainOfficeAddress`); for `city`,
   `state`, `zip`, and `country` the spec exposes no direct replacement, so
   the doc simply records that fact.

No spec-required additions or type tightening were needed — the plan had
zero CRITICAL / HIGH / MEDIUM findings. `OrganizationCarrierSettings`
(which has unrelated drift around `carrierName` and the `dotNumber`
`integer` vs `string` shape) is **out of scope** for this plan and was not
touched.

Files touched: `src/Samsara.Sdk/Models/Organization/OrganizationModels.cs`.

Verification: `dotnet build` green (0 warnings, 0 errors); all 59 unit
tests pass; `python3 tools/check-sdk-sync.py` exits 0
(matched=323/323, mismatched=0, unresolved=0, not implemented=0).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `OrganizationInfo` | response | 0 | 0 | 0 | 5 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=0, LOW=5  
**Total deduped findings**: 5

## LOW (5)

### `OrganizationInfo` (response)

- **[extra_property]** OrganizationInfo.address (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /me`
  - Recommended fix: Remove `OrganizationInfo.Address` (not in spec).
- **[extra_property]** OrganizationInfo.city (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /me`
  - Recommended fix: Remove `OrganizationInfo.City` (not in spec).
- **[extra_property]** OrganizationInfo.country (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /me`
  - Recommended fix: Remove `OrganizationInfo.Country` (not in spec).
- **[extra_property]** OrganizationInfo.state (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /me`
  - Recommended fix: Remove `OrganizationInfo.State` (not in spec).
- **[extra_property]** OrganizationInfo.zip (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /me`
  - Recommended fix: Remove `OrganizationInfo.Zip` (not in spec).

