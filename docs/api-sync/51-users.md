# Users — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ✅ Roles corrected 2026-08-17 (v0.5.0 spec-parity sweep)  
> **🐛 Fixed 2026-08-17 — `User.roles` deserialized to nothing.** A single 3-property
> `UserRole` record was serving three divergent spec shapes: `UserRole` `{id, name}` for
> `GET /user-roles`, `UserRoleAssignment` `{role, tag, expireAt}` for `User.roles[]`, and
> `CreateUserRequest_roles` `{roleId, tagId}` for create/update. The wire nests the role
> under `role`, so the SDK read a flat `id`/`name` that is never present — every role on
> every user came back empty, `expireAt` was dropped, and create/update sent `id` where the
> API requires `roleId`, so role assignments silently did not apply. Now modelled as
> `UserRoleAssignment` (responses) and `UserRoleInput` (requests), with `UserRole` reduced to
> the `GET /user-roles` shape. **Breaking.** Covered by `UsersContractTests`.
> **Why no checker caught it**: the union of the three shapes contains every SDK property, so
> neither `missing-optional` nor `extra-property` could fire. Note the 2026-05-27 entry below
> explicitly retained `UserRole.tagId` as "back-compat" — that extra-property finding was the
> one visible symptom, and accepting it instead of resolving the schema is what let the bug
> survive. Treat an unexplained extra property as a shape question, not a cosmetic one.  
> **✅ Resolved 2026-05-27 (model-sync plan)**: see [`model-sync-plan-2026-05-27/51-users.md`](model-sync-plan-2026-05-27/51-users.md). The 4 MEDIUM `response_required_drift` findings on `User` were applied — `name`, `email`, `authType`, and `roles` tightened from nullable to non-nullable via `required` (repo convention; verified safe — no `new User(...)` construction sites). **Breaking**: consumers may now rely on non-null `name`/`email`/`authType`/`roles`. The 1 LOW extra (`UserRole.tagId`) was retained as a nullable back-compat prop rather than removed. CLI `List All` users render simplified (dropped redundant `?? ""` on `u.Name`/`u.Email`). `ListAsync` and the `IUsersClient`/`UsersClient` methods are unchanged; `CreateUserRequest`/`UpdateUserRequest` out of scope. No JsonContext/test changes.  
> **⚠️ 2026-05-21 audit**: model — `User`/`CreateUserRequest` required fields (`name`/`email`/`authType`/`roles`) modelled optional. See [full-sync-review-2026-05-21.md](full-sync-review-2026-05-21.md).  
> **SDK Client**: `IUsersClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../UsersClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Organization/OrganizationModels.cs`  

---

## Endpoints

### ✅ `GET /user-roles`
**Operation ID**: `listUserRoles`  
**Summary**: List all user roles  
**Parameters**: `limit`, `after`  
**Request Body**: No  

- [x] Method defined in `IUsersClient`
- [x] Method implemented in `UsersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /users`
**Operation ID**: `listUsers`  
**Summary**: List all users  
**Parameters**: `limit`, `after`  
**Request Body**: No  

- [x] Method defined in `IUsersClient`
- [x] Method implemented in `UsersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `POST /users`
**Operation ID**: `createUser`  
**Summary**: Create a user  
**Request Body**: Yes  

- [x] Method defined in `IUsersClient`
- [x] Method implemented in `UsersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `DELETE /users/{id}`
**Operation ID**: `deleteUser`  
**Summary**: Delete a user  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IUsersClient`
- [x] Method implemented in `UsersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `GET /users/{id}`
**Operation ID**: `getUser`  
**Summary**: Retrieve a user  
**Parameters**: `id`  
**Request Body**: No  

- [x] Method defined in `IUsersClient`
- [x] Method implemented in `UsersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /users/{id}`
**Operation ID**: `updateUser`  
**Summary**: Update a user  
**Parameters**: `id`  
**Request Body**: Yes  

- [x] Method defined in `IUsersClient`
- [x] Method implemented in `UsersClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Organization/OrganizationModels.cs` for model definitions used by this domain.

- [x] All request models defined as `record` types
- [x] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [x] Nullable reference types used correctly

---

## Notes

**Model audit (2025-05-13):** User request models were missing fields present in the API.

- `CreateUserRequest`: added `expireAt`.
- `UpdateUserRequest`: added `authType`, `expireAt`.
