# Samsara .NET SDK — Full-Sync Completion Plan (2026-05-29)

> **Plan document only.** No SDK code, tests, CLI, or tooling were changed while producing
> this plan (the one exception: the in-flight **Hubs** edit already present in the working
> tree was *reviewed and verified*, not authored here). Per the repo's document-first
> convention, this enumerates every proposed change with its exact spec target and stops
> before implementation. Execute phases only after sign-off.
>
> **Companion docs:** [`full-sync-review-2026-05-21.md`](full-sync-review-2026-05-21.md)
> (endpoint audit), [`model-sync-plan-2026-05-27/`](model-sync-plan-2026-05-27/00-summary.md)
> (property-level audit, 1,719 findings), [`README.md`](README.md) (domain index).

---

## 0. TL;DR

A full endpoint sweep (PR #4) and a 56-domain property-level model sweep are **already
merged**, and the tree **builds clean with 59 passing unit tests**. You are "still hitting
issues" not because the sweeps were sloppy, but because the **tooling that declared success
has structural blind spots**:

1. **The coverage checker cannot see fabricated / mis-homed methods.** It just proved this:
   `HubsClient` shipped four CRUD methods (`Get/Create/Update/Delete`) that the Samsara spec
   **does not have** — they were silently pointed at `BasePath = "addresses"`, so the checker
   matched them against the real `/addresses` operations and reported `mismatched=0`. The
   uncommitted Hubs edit in your tree fixes exactly this. **Hubs is unlikely to be the only one.**
2. **Model/property parity is not gated anywhere.** The 1,719-finding audit was a one-time
   manual pass. Nothing re-checks it, so models drift silently as the spec changes.
3. **The spec is a moving target under a frozen version string.** The live spec is still
   `2025-10-23` but has grown **+13 schemas and +1 endpoint** since the 2026-05-27 baseline.
   Version-keyed drift detection under-triggers.
4. **Deferred correctness work looks cosmetic but isn't.** Several "LOW / extra-property"
   items are **silent data loss** (e.g. `VehicleStats.gps` is an array on `/feed` & `/history`
   but the SDK exposes a single object; HOS/Trip flatten nested spec objects).
5. **There are effectively no integration/contract tests** — the `IntegrationTests` project
   contains no test sources.

This plan closes those gaps in six phases, front-loading the cheap/ready items (Phase 0) and
the blind-spot fix that prevents recurrence (Phase 1).

---

## 1. Verified current state (measured 2026-05-29)

Everything in this section was confirmed empirically against the **live** spec
(`https://developers.samsara.com/openapi/samsara-api.json`, fetched 2026-05-29) and a clean
build of the working tree — not read off the checklist banners.

| Area | Status | Evidence |
|---|---|---|
| **Build (SDK, Release)** | ✅ 0 warnings / 0 errors | `dotnet build src/Samsara.Sdk` |
| **Build (full solution)** | ✅ succeeds | `dotnet test Samsara.Dotnet.sln -c Release` |
| **Unit tests** | ✅ 59 passed / 0 failed | `Samsara.Sdk.Tests.dll` |
| **Integration tests** | ⚠️ **none exist** | `tests/Samsara.Sdk.IntegrationTests/` has no `.cs` sources |
| **Endpoint coverage** | ✅ 318 / 318 matched, 0 mismatched | `check-sdk-sync.py --spec-file <live>` |
| **Unimplemented spec ops** | ⚠️ **1** — `GET /places/deletions` | new beta endpoint, added since baseline |
| **Model sync (CRIT/HIGH/MED)** | ✅ all 56 domains committed | git log Parts 01–56, merged in PR #4 |
| **Model sync (LOW / Phase D)** | ⚠️ largely deferred by design | 431 LOW findings outstanding |
| **Beta typed models** | ⚠️ ~100 props still `object?` | Places, PreferredStations, Qualification, Ridership, Functions, Reports |
| **In-flight Hubs fix** | ✅ reviewed, correct, builds, tests pass | see §3 Phase 0 |
| **Release state** | last tag `v0.2.1`; all model-sync work is `[Unreleased]` | `CHANGELOG.md`, `git tag` |

**Live spec shape:** version `2025-10-23` · 224 paths · **318 operations** · **3,934 schemas**
(was 3,921 at the 2026-05-27 audit → **+13**).

**Spec drift already detected** ([`diff-report.md`](diff-report.md), regenerated 2026-05-29):
1 new endpoint (`GET /places/deletions`) + 13 new schemas, all in the **Places (beta)** domain.

### The in-flight Hubs change (uncommitted, in your working tree) — reviewed ✅

The working tree removes fabricated Hub CRUD and the bogus `BasePath = "addresses"`:

- **Removed methods:** `HubsClient.GetAsync / CreateAsync / UpdateAsync / DeleteAsync`
  (+ `IHubsClient` declarations).
- **Removed models:** `CreateHubRequest`, `UpdateHubRequest` (+ their `JsonSerializable` registrations).
- **CLI:** `tools/Samsara.Cli/TuiApp.cs` retargeted to drop the deleted hub commands.

**Verdict: correct and ready to commit.** The live spec has exactly **one** top-level hub
operation — `GET /hubs` — plus the sub-resources the SDK already covers
(`/hub/capacities`, `/hub/customProperties`, `/hub/locations`, `/hub/location/{id}`,
`/hub/plan`, `/hub/plan/orders`, `/hub/plan/routes`, `/hub/plans`, `/hub/route-templates`,
`/hub/skills`). There is **no** `GET/POST/PATCH/DELETE /hubs/{id}`. The build and all 59
tests pass with this change applied.

---

## 2. Root-cause analysis — why "in sync" wasn't

> This is the part that actually answers "I synced but still hit issues." Each root cause maps
> to a phase in §3.

### RC-1 — The coverage checker is blind to fabricated / mis-homed methods → **Phase 1**

`check-sdk-sync.py` answers one question: *does every spec operation have at least one SDK
method whose resolved path matches it?* It dedups SDK endpoints by `(verb, path)`. Consequences:

- A method named for domain **A** that actually calls domain **B**'s path (Hubs → `/addresses`)
  is counted as coverage for **B** and **never flagged**.
- Two differently-named clients covering the same operation are invisible (the duplicate is
  deduped away).
- An SDK method for an operation **that does not exist in the spec** passes as long as its path
  happens to resolve to *some* real path.

This is precisely the class of bug you just hit with Hubs. The checker said `mismatched=0` the
whole time. **We have no evidence Hubs is unique** — the reverse check that would prove it
doesn't exist yet (it's Phase 1). What we *did* confirm: all 29 `BasePath` constants are now
domain-appropriate (Hubs was the lone offender, now fixed), so any remaining offenders would be
using inline string-literal paths.

### RC-2 — Model/property parity is not reproducible or gated → **Phase 2**

The 2026-05-27 audit that produced 1,719 findings (3 CRITICAL, 329 HIGH, 956 MEDIUM, 431 LOW)
was a **manual, one-shot analysis**. Its output is 58 markdown plan docs, not a runnable check.
Nothing recomputes it. As the spec mutates (RC-3), models silently fall out of parity again with
no signal. CI runs `check-api-sync.py` (spec-vs-baseline) and `check-sdk-sync.py` (paths-vs-spec)
but **neither compares SDK record properties against spec schema properties**.

### RC-3 — The spec mutates in place under a frozen version string → **Phase 4**

Samsara ships changes without bumping `info.version`. The live spec is still `2025-10-23` yet has
**+13 schemas and +1 endpoint** vs the 2026-05-27 baseline. `check-api-sync.py` does diff at the
endpoint/parameter level (good — that's how `/places/deletions` was caught), but:
- It only fires on **endpoint** changes, not **schema/property** changes — so new fields on
  existing models (the most common drift) open no issue.
- The weekly workflow's human-facing summary keys on the version string, which is misleadingly stable.

### RC-4 — Deferred "LOW" work includes real correctness bugs → **Phase 3**

Phase D was deferred as cosmetic cleanup, but the 00-summary's cross-cutting patterns include
genuine data-loss bugs hiding among the "extra property" findings:
- **`VehicleStats.gps` / `TrailerStats.gps`** — array of points on `/feed` & `/history`, but the
  SDK exposes a single `GpsData?`. Time-series GPS is **silently dropped**.
- **HOS / Trip / Tachograph / Route flattening** — the SDK denormalizes nested spec objects into
  flat scalars (`HosLog.driverId` vs spec `data[].driver`), so the documented nested payload is
  missing and every flat field reads as an "extra property."
- **`FormTemplate.name`** doesn't exist in spec (spec uses `title`); also missing `fields`,
  `createdBy`, `revisionId`, etc.
- **81 `response_required_drift`** fields the spec guarantees non-null are exposed as nullable.

### RC-5 — Weak Beta typing → **Phase 3 (workstream D)**

~100 properties across `Clients/Beta/*` are `object?` where the spec has concrete schemas
(`IndustrialJob`, `Detection`, `Device`, `Place`, `PreferredStation`, `QualificationRecord`,
`Ridership*`, `FunctionLog`, `FunctionFile`, …). Functionally usable, but not "valid models."

### RC-6 — Test coverage gap → **Phase 5**

The `IntegrationTests` project is an empty shell (no `.cs` sources). Unit tests exist for ~13
clients out of 50. There are **no contract tests** that deserialize real spec example payloads
into SDK records — which is exactly the cheap, token-free test that would have caught RC-1 and
most of RC-4.

---

## 3. The completion plan (phased)

Ordering principle: **stop the bleeding and prevent recurrence first** (Phases 0–2), then **finish
the correctness backlog** (Phase 3), then **harden against future drift and ship** (Phases 4–5).
Phases 0–2 are the ones that make the "still hitting issues" stop.

### Phase 0 — Land what's ready, close the one open gap *(≈0.5 day, low risk)*

| # | Task | Breaking? | Notes |
|---|---|---|---|
| 0.1 | **Commit the in-flight Hubs fix** | Yes (removes 4 public methods + 2 request types) | Verified correct vs live spec; build + 59 tests green. Batch into the v0.3.0 break (§4). |
| 0.2 | **Implement `GET /places/deletions`** | No (additive) | Add to `PlacesClient` (beta, `BasePath = "places"`). Response schema `PlacesGetPlaceDeletionsResponseBody` → items `PlaceDeletionMarkerObjectResponseBody`. Match the domain's current posture (typed if cheap, else `object?` consistent with other beta ops). Register in `SamsaraJsonContext`. |
| 0.3 | **Refresh baseline + regenerate diff-report** | No | `python3 tools/check-api-sync.py --update-baseline` once 0.2 lands, so the 13 new Places schemas stop showing as drift. |
| 0.4 | **Fold the 13 new Places schemas into the Beta typing backlog** | No | They belong to Phase 3 workstream D, not Phase 0. |

**Exit criteria:** `check-sdk-sync.py` reports `missing=0`; build + tests green; diff-report shows no outstanding endpoint drift.

### Phase 1 — Close the checker blind spot + repo-wide fabricated-method sweep *(≈1–1.5 days)*

This is the highest-leverage phase: it both **finds** the remaining Hubs-class bugs and
**prevents** new ones.

| # | Task |
|---|---|
| 1.1 | **Add a reverse check to `check-sdk-sync.py`** (or a sibling `check-sdk-fabrication.py`) that, for every public client method, asserts: (a) it maps to a **distinct** spec operation (flag any spec op covered by methods from **>1 client**, or by >1 method in the same client); (b) the resolved path's tag/first-segment **belongs to the client's domain** (flag `HubsClient` → `addresses/*`); (c) every method corresponds to a **real** spec `operationId` (flag methods with no spec op even if the path resolves). |
| 1.2 | **Run it repo-wide** and triage every flag: genuine fabrication → remove (batch into the v0.3.0 break); legitimate cross-domain reuse (e.g. `V1GetAllAssetsAsync` living on `IAssetsClient`, documented in 26-legacy) → allowlist with a comment. |
| 1.3 | **Wire 1.1 into CI** alongside the existing sync checks, `--fail-on` for new fabrications. |

**Exit criteria:** reverse check passes (or every flag is explicitly allowlisted with rationale);
CI fails if a future method is fabricated or mis-homed.

### Phase 2 — Make model-level parity reproducible and gated *(≈2–3 days)*

| # | Task |
|---|---|
| 2.1 | **Promote the 2026-05-27 audit into a committed tool** `tools/check-model-sync.py` that re-derives the property-level findings from the live spec on demand (the methodology is already written up in [`model-sync-plan-2026-05-27/00-summary.md`](model-sync-plan-2026-05-27/00-summary.md) §Methodology — implement it as code). Emit JSON + a per-domain markdown report. |
| 2.2 | **Reconcile against the merged remediation** so the committed baseline is "0 CRIT / 0 HIGH outstanding" (matching what Parts 01–56 actually fixed), with MED/LOW counts as the known backlog. |
| 2.3 | **Gate CI** on CRITICAL and HIGH (`--fail-on-severity HIGH`); MED/LOW as a non-blocking report artifact. |

**Exit criteria:** `check-model-sync.py` runs in CI; a new spec field that the SDK misses on a
required payload turns the build red instead of rotting silently.

### Phase 3 — Finish the deferred model-correctness backlog *(≈3–5 days; breaking)*

Do these in correctness-first order, not severity-label order.

| # | Workstream | What | Breaking? |
|---|---|---|---|
| 3.1 | **Data-loss shape fixes (do first)** | `VehicleStats`/`TrailerStats` `gps` array-vs-object split (separate snapshot vs feed/history records); un-flatten HOS/Trip/Tachograph/Route to the spec's nested shape with `[Obsolete]` on the flat scalars; `FormTemplate` `name`→`title` + missing fields. | Yes (source-breaking; JSON-additive) |
| 3.2 | **Response non-null tightening** | 81 `response_required_drift` fields → `required` non-null. | Yes (binary), JSON-compatible |
| 3.3 | **Phase D extra-property cleanup** | 431 LOW — `[Obsolete]` first, remove next major; per [`zz-implementation-phases.md`](model-sync-plan-2026-05-27/zz-implementation-phases.md) Phase D guidance. | Yes, deprecation-gated |
| 3.4 | **Workstream D — Beta typing** | Replace ~100 `object?` props with typed records (Places, PreferredStations, Qualification, Ridership, Functions, Reports). | No (additive) — but explicitly subject to spec change |

**Exit criteria:** `check-model-sync.py` MED count driven down to the agreed floor; no remaining
silent-data-loss findings; Beta domains typed (or consciously deferred as a tracked workstream).

### Phase 4 — Harden spec-drift detection *(≈1 day)*

| # | Task |
|---|---|
| 4.1 | **Add schema/property-level diff** to `check-api-sync.py` (not just endpoints) so new/changed fields on existing models open an issue. |
| 4.2 | **Switch the drift signal from version-string to content** (schema count + content hash) since Samsara mutates in place under a frozen `info.version`. |
| 4.3 | **Document the baseline-refresh discipline** (when to `--update-baseline`, who reviews) so the baseline doesn't silently absorb real drift. |

### Phase 5 — Tests + release *(≈2–4 days)*

| # | Task |
|---|---|
| 5.1 | **Contract tests (highest ROI, no token needed):** for every domain, deserialize the spec's example/response payloads into the SDK records and assert round-trip + required-field presence. This is the test that catches RC-1 and RC-4 directly. |
| 5.2 | **Expand unit coverage** from ~13 clients toward all 50 (path/verb + query-builder assertions per client). |
| 5.3 | **Populate `IntegrationTests`** with token-gated smoke tests (skip when `SAMSARA_API_TOKEN` is unset) for a representative read path per domain. |
| 5.4 | **Batch the release** — see §4. |

---

## 4. Release & breaking-change strategy

**Everything from the model sync is still `[Unreleased]`** (last shipped tag is `v0.2.1`). That is
a gift: consumers have absorbed **none** of these breaks yet, so they can all land in **one**
version bump instead of a painful drip.

- **Cut one minor/major** (recommend **`v0.3.0`**) that bundles Phase 0 (Hubs removal),
  Phase 1 (fabricated-method removals), and Phase 3.1–3.3 (shape fixes, non-null tightening,
  deprecations).
- **Single "Migration / breaking changes" section** in `CHANGELOG.md` enumerating removed methods
  (`HubsClient.Get/Create/Update/Delete`, any Phase-1 finds), re-signatured methods, and `[Obsolete]`
  properties with their replacements.
- **Phase 3.4 (Beta typing)** and **Phase 5** are additive → can trail in `v0.3.x` patches.
- **Phase D removals** (after the `[Obsolete]` cycle) wait for the **next** major.

---

## 5. Definition of done

- [ ] `check-sdk-sync.py`: `mismatched=0`, `missing=0` against the **live** spec.
- [ ] Reverse/fabrication check (Phase 1) passes or every exception is allowlisted with rationale.
- [ ] `check-model-sync.py` (Phase 2) in CI; **0 CRITICAL, 0 HIGH** outstanding; MED/LOW tracked.
- [ ] No remaining silent-data-loss findings (gps array, HOS/Trip flatten, Forms title).
- [ ] Spec-drift detection covers schema/property changes and content hash (Phase 4).
- [ ] Contract tests deserialize spec examples for every domain; unit coverage expanded; build + all tests green.
- [ ] One batched `v0.3.0` release with a complete migration section; baseline refreshed.

---

## 6. Open decisions (need your call before execution)

1. **Scope of this pass.** Minimal ("stop the bleeding" = Phases 0–2) vs. full correctness
   (Phases 0–3) vs. everything incl. drift hardening + tests + release (0–5)?
2. **Beta typing (Phase 3.4).** In scope now, or tracked as a separate workstream (it's the
   single biggest chunk and is explicitly subject to spec change)?
3. **Release appetite.** Cut `v0.3.0` with the batched breaks as soon as Phase 3.1–3.3 land, or
   keep accumulating in `[Unreleased]`?
4. **Phase D removals.** `[Obsolete]`-then-remove-next-major (safe), or remove now while still
   pre-1.0 and unreleased (cleaner, more breaking)?
