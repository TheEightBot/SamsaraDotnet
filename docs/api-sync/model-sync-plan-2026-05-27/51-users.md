# Users — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/51-users.md`](../51-users.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `User` | response | 0 | 0 | 4 | 0 |
| `UserRole` | response | 0 | 0 | 0 | 1 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=4, LOW=1  
**Total deduped findings**: 5

## MEDIUM (4)

### `User` (response)

- **[response_required_drift]** User.authType (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /users`, `GET /users/{id}`, `PATCH /users/{id}`, `POST /users`
  - Recommended fix: Tighten `User.AuthType` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** User.email (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /users`, `GET /users/{id}`, `PATCH /users/{id}`, `POST /users`
  - Recommended fix: Tighten `User.Email` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** User.name (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /users`, `GET /users/{id}`, `PATCH /users/{id}`, `POST /users`
  - Recommended fix: Tighten `User.Name` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** User.roles (response): spec marks REQUIRED but SDK exposes as nullable (`IReadOnlyList<UserRole>?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /users`, `GET /users/{id}`, `PATCH /users/{id}`, `POST /users`
  - Recommended fix: Tighten `User.Roles` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

## LOW (1)

### `UserRole` (response)

- **[extra_property]** UserRole.tagId (response): present in SDK but not in spec inner schema.
  - Endpoints: `GET /user-roles`
  - Recommended fix: Remove `UserRole.TagId` (not in spec).

