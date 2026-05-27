# Contacts — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/10-contacts.md`](../10-contacts.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `6d79b55` on 2026-05-27**

## Implementation notes

All 4 LOW findings resolved by relaxing `CreateContactRequest.{FirstName,
LastName, Email, Phone}` from `required string` to `string?` in
`src/Samsara.Sdk/Models/Communication/CommunicationModels.cs`. The spec's
`CreateContactRequest` schema declares no `required` array, so the previous
`required` modifier was an over-tightening that blocked otherwise valid
partial requests (e.g. creating a contact with only a phone number).

The `Contact` response record was not touched — the spec's `Contact` schema
DOES declare `id`, `firstName`, `lastName`, `email`, `phone` as required, so
the SDK's `required` modifiers there remain correct. `UpdateContactRequest`
was already fully nullable and required no change.

Verification: `dotnet build` green (0 warnings, 0 errors); 59 unit tests
pass; `tools/check-sdk-sync.py` reports `MISMATCHED: 0` and exits 0.


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

