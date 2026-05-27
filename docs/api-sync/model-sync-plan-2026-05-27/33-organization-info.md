# Organization Info — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/33-organization-info.md`](../33-organization-info.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


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

