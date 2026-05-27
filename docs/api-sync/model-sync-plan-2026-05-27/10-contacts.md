# Contacts — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/10-contacts.md`](../10-contacts.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `CreateContactRequest` | request | 0 | 0 | 0 | 4 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=0, LOW=4  
**Total deduped findings**: 4

## LOW (4)

### `CreateContactRequest` (request)

- **[required_drift_over]** CreateContactRequest.email: SDK marks `required` but spec is optional.
  - Endpoints: `POST /contacts`
  - Recommended fix: Drop `required` on `CreateContactRequest.Email` (spec marks it optional) — make nullable.
- **[required_drift_over]** CreateContactRequest.firstName: SDK marks `required` but spec is optional.
  - Endpoints: `POST /contacts`
  - Recommended fix: Drop `required` on `CreateContactRequest.FirstName` (spec marks it optional) — make nullable.
- **[required_drift_over]** CreateContactRequest.lastName: SDK marks `required` but spec is optional.
  - Endpoints: `POST /contacts`
  - Recommended fix: Drop `required` on `CreateContactRequest.LastName` (spec marks it optional) — make nullable.
- **[required_drift_over]** CreateContactRequest.phone: SDK marks `required` but spec is optional.
  - Endpoints: `POST /contacts`
  - Recommended fix: Drop `required` on `CreateContactRequest.Phone` (spec marks it optional) — make nullable.

