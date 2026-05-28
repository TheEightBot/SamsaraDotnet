# Users — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/51-users.md`](../51-users.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `5685b0b` on 2026-05-27**

## Implementation notes

All 4 MEDIUM `response_required_drift` findings were applied: `User.Name`,
`User.Email`, `User.AuthType`, and `User.Roles` were tightened from nullable to
non-nullable using `required` (repo convention for "tighten to non-nullable",
cf. `Address.FormattedAddress`). Verified safe — there are NO `new User(...)`
construction sites anywhere in src/tools/tests. **Breaking**: consumers may now
rely on non-null `name`/`email`/`authType`/`roles` (and the compiler enforces
these on any future construction). `User.Id` was already `required string` and
is unchanged.

The 1 LOW `extra_property` (`UserRole.tagId`) was intentionally RETAINED as a
nullable back-compat property per the workflow precedent (cf.
`40-safety-scores`, `49-trainingcourses`, `50-trips`) rather than removed, with
a `// Not in current spec; retained for back-compat.` comment added above it.

The CLI `List All` users render site (`tools/Samsara.Cli/TuiApp.cs`) was
simplified to drop the now-redundant `?? ""` on `u.Name` / `u.Email`, reflecting
the new non-null guarantee.

Files touched: `src/Samsara.Sdk/Models/Organization/OrganizationModels.cs`,
`tools/Samsara.Cli/TuiApp.cs`. `ListAsync` and the `IUsersClient`/`UsersClient`
methods are unchanged. No JsonContext changes (`User`/`UserRole` already
registered; only nullability tightening, no new props/types). No test changes
(no `new User`/`new UserRole` construction). `CreateUserRequest` /
`UpdateUserRequest` are out of scope and untouched.

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

