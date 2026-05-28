# Issues — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/25-issues.md`](../25-issues.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `c126783` on 2026-05-27**

## Implementation notes

All HIGH (3), MEDIUM (16), and LOW (6) findings were applied — 25 total.

Files touched:

- `src/Samsara.Sdk/Models/Issues/IssueModels.cs` — `Issue` rebuilt; four new
  nested response records (`IssueAsset`, `IssueUser`, `IssueSource`,
  `IssueMedia`); three new request records (`IssueAssetRequest`,
  `IssueAssigneeRequest`, `IssueMediaItemRequest`); request DTOs retyped.
- `src/Samsara.Sdk/Clients/Issues/IIssuesClient.cs` — `GetStreamAsync`
  signature expanded from 2 to 7 parameters for the full spec query surface.
- `src/Samsara.Sdk/Clients/Issues/IssuesClient.cs` — query-string composition
  via `QueryBuilder.WithParams`, joining array params with `,` per the
  `IndustrialClient` / `ReportsClient` precedent.
- `src/Samsara.Sdk/Serialization/SamsaraJsonContext.cs` — added seven new
  `[JsonSerializable]` registrations (plus the previously-unregistered
  `CreateIssueRequest`).

**HIGH (3) — `Issue` (response) spec-REQUIRED additions**

- **`issueSource`** → `required IssueSource IssueSource` (new nested
  record mirroring `IssueSourceObjectResponseBody`: required `Type` plus
  optional `Id`). Modelled as a typed sub-record rather than the plan's
  recommended `object` placeholder, following the same precedent used in
  `13`/`14`/`23`.
- **`submittedAtTime`** → `required DateTimeOffset SubmittedAtTime`
  (spec `string/date-time`).
- **`submittedBy`** → `required IssueUser SubmittedBy` (new nested record
  mirroring `FormsPolymorphicUserObjectResponseBody`: required `Id` and
  `Type`). Reused for the optional `AssignedTo` field below since both
  spec fields reference the same schema.

**MEDIUM (16)**

- **5 missing optional query parameters on `GetStreamAsync`** added to
  `IIssuesClient` and `IssuesClient`: `status`, `assetIds`,
  `assetExternalIds`, `include`, `assignedToRouteStopIds`. Each typed as
  `IReadOnlyList<string>?` and joined with `,` via
  `QueryBuilder.WithParams`. The plan suggested `IReadOnlyList<object>?`
  for `include` and `status`; tightened to `string` because the spec
  declares them as string arrays.
- **`Issue.asset`** added as `IssueAsset? Asset` (new nested record
  mirroring `FormsAssetObjectResponseBody`: required `EntryType`, optional
  `Id`/`Name`/`ExternalIds`).
- **`Issue.assignedTo`** added as `IssueUser? AssignedTo` (reuses the
  `IssueUser` record introduced for the required `SubmittedBy` field).
- **`Issue.dueDate`** added as `DateTimeOffset? DueDate`
  (spec `string/date-time`).
- **`Issue.mediaList`** added as `IReadOnlyList<IssueMedia>? MediaList`
  (new nested record mirroring `FormsMediaRecordObjectResponseBody`:
  required `Id`, `ProcessingStatus`; optional `Url`, `UrlExpiresAt`).
- **`Issue.createdAtTime`** tightened from `DateTimeOffset?` to
  `required DateTimeOffset` (spec-REQUIRED).
- **`Issue.status`** tightened from `string?` to `required string`
  (spec-REQUIRED). Kept as `string` rather than an enum to preserve
  forward-compatibility with future Samsara status values.
- **`Issue.title`** tightened from `string?` to `required string`
  (spec-REQUIRED).
- **`Issue.updatedAtTime`** tightened from `DateTimeOffset?` to
  `required DateTimeOffset` (spec-REQUIRED).
- **`CreateIssueRequest.asset`** retyped from weak `object` to
  `required IssueAssetRequest Asset` (new request record mirroring
  `PostIssueRequestBodyAssetRequestBody`: required `Id`).
- **`CreateIssueRequest.assignedTo`** retyped from weak `object?` to
  `IssueAssigneeRequest? AssignedTo` (new request record mirroring
  `PostIssueRequestBodyAssignedToRequestBody`: required `Id`/`Type`).
- **`UpdateIssueRequest.assignedTo`** retyped from weak `object?` to
  `IssueAssigneeRequest? AssignedTo` (the spec's
  `PatchIssueRequestBodyAssignedToRequestBody` is structurally identical
  to the POST variant — the record is shared by both DTOs).
- **`Create/UpdateIssueRequest.media`** retyped from
  `IReadOnlyList<object>?` to `IReadOnlyList<IssueMediaItemRequest>?`
  (new request record mirroring
  `FormSubmissionRequestMediaItemObjectRequestBody`: required
  `Base64Payload`/`MediaType`). Not enumerated in the plan because the
  plan only flagged the top-level `object` properties, but applied here
  as the natural companion fix.

**LOW (6) — extra-property removals**

All six SDK-only flat scalars absent from the spec inner schema were
removed: `assigneeId`, `assigneeName`, `vehicleId`, `vehicleName`,
`resolvedAtTime`, `type`. Same approach as `23-idling` — there are no
test/TUI consumers of these fields (the TUI only reads `Id`/`Title`/`Status`,
all of which remain). The new nested `AssignedTo`/`Asset` objects provide
the same information through spec-aligned JSON property names.

Build is green and `tools/check-sdk-sync.py` reports `mismatched=0` /
`not implemented=0`. All 59 unit tests pass.

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `Issue` | response | 0 | 3 | 8 | 6 |
| `(no SDK type)` | query | 0 | 0 | 5 | 0 |
| `CreateIssueRequest` | request | 0 | 0 | 2 | 0 |
| `UpdateIssueRequest` | request | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=3, MEDIUM=16, LOW=6  
**Total deduped findings**: 25

## HIGH (3)

### `Issue` (response)

- **[response_drift_required]** Issue (response) missing REQUIRED property `issueSource` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("issueSource")] public object IssueSource { get; init; }` to response record `Issue` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Issue (response) missing REQUIRED property `submittedAtTime` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("submittedAtTime")] public DateTimeOffset SubmittedAtTime { get; init; }` to response record `Issue` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).
- **[response_drift_required]** Issue (response) missing REQUIRED property `submittedBy` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("submittedBy")] public object SubmittedBy { get; init; }` to response record `Issue` (spec marks REQUIRED; mark `required` if part of guaranteed payload, else nullable).

## MEDIUM (16)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `assetExternalIds` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `assetIds` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assetIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `assignedToRouteStopIds` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<string>? assignedToRouteStopIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `include` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? include = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.
- **[missing_optional_query]** GetStreamAsync (GET /issues/stream) is missing query parameter `status` (spec optional, type=array).
  - Endpoints: `GET /issues/stream`
  - Recommended fix: Add an optional parameter `IReadOnlyList<object>? status = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `CreateIssueRequest` (request)

- **[weak_typing]** CreateIssueRequest.asset: SDK uses weak `object` for spec type `object`.
  - Endpoints: `POST /issues`
  - Recommended fix: Replace weak `object?` with a typed model on `CreateIssueRequest.Asset` (spec type=`object`).
- **[weak_typing]** CreateIssueRequest.assignedTo: SDK uses weak `object` for spec type `object`.
  - Endpoints: `POST /issues`
  - Recommended fix: Replace weak `object?` with a typed model on `CreateIssueRequest.AssignedTo` (spec type=`object`).

### `Issue` (response)

- **[response_drift_optional]** Issue (response) missing property `asset` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("asset")] public object? Asset { get; init; }` to response record `Issue`.
- **[response_drift_optional]** Issue (response) missing property `assignedTo` (spec type=object). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("assignedTo")] public object? AssignedTo { get; init; }` to response record `Issue`.
- **[response_drift_optional]** Issue (response) missing property `dueDate` (spec type=string/date-time). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("dueDate")] public DateTimeOffset? DueDate { get; init; }` to response record `Issue`.
- **[response_drift_optional]** Issue (response) missing property `mediaList` (spec type=array). (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Add `[JsonPropertyName("mediaList")] public IReadOnlyList<object>? MediaList { get; init; }` to response record `Issue`.
- **[response_required_drift]** Issue.createdAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Tighten `Issue.CreatedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Issue.status (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Tighten `Issue.Status` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Issue.title (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Tighten `Issue.Title` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** Issue.updatedAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Tighten `Issue.UpdatedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `UpdateIssueRequest` (request)

- **[weak_typing]** UpdateIssueRequest.assignedTo: SDK uses weak `object` for spec type `object`.
  - Endpoints: `PATCH /issues`
  - Recommended fix: Replace weak `object?` with a typed model on `UpdateIssueRequest.AssignedTo` (spec type=`object`).

## LOW (6)

### `Issue` (response)

- **[extra_property]** Issue.assigneeId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.AssigneeId` (not in spec).
- **[extra_property]** Issue.assigneeName (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.AssigneeName` (not in spec).
- **[extra_property]** Issue.resolvedAtTime (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.ResolvedAtTime` (not in spec).
- **[extra_property]** Issue.type (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.Type` (not in spec).
- **[extra_property]** Issue.vehicleId (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.VehicleId` (not in spec).
- **[extra_property]** Issue.vehicleName (response): present in SDK but not in spec inner schema. (affects 4 endpoints)
  - Endpoints: `GET /issues`, `GET /issues/stream`, `PATCH /issues`, `POST /issues`
  - Recommended fix: Remove `Issue.VehicleName` (not in spec).

