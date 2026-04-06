# Samsara .NET API Client — Implementation Plan

> A world-class, idiomatic .NET API client for the [Samsara Fleet Management API](https://developers.samsara.com/).  
> Target: **.NET 8** / C# 12 with modern HTTP client patterns.

---

## Phase 1: Project Foundation & Core Infrastructure

### 1.1 Solution & Project Structure

- [x] Create solution file `Samsara.Dotnet.sln`
- [x] Create class library project `Samsara.Sdk` (net8.0)
- [x] Create unit test project `Samsara.Sdk.Tests` (xUnit + NSubstitute)
- [x] Create integration test project `Samsara.Sdk.IntegrationTests` (separate so CI can exclude)
- [x] Add `Directory.Build.props` for shared build settings (nullable, implicit usings, versioning, `TreatWarningsAsErrors`)
- [x] Add `.editorconfig` with C# coding conventions
- [x] Add `.gitignore` for .NET
- [x] Add `global.json` pinning .NET 8 SDK

**Folder layout:**

```
SamsaraDotnet/
├── src/
│   └── Samsara.Sdk/
│       ├── Authentication/
│       ├── Configuration/
│       ├── Http/
│       ├── Pagination/
│       ├── Serialization/
│       ├── Models/            ← Request/response DTOs by domain
│       ├── Clients/           ← Typed service clients by domain
│       └── Exceptions/
├── tests/
│   ├── Samsara.Sdk.Tests/
│   └── Samsara.Sdk.IntegrationTests/
├── Directory.Build.props
├── global.json
└── Samsara.Dotnet.sln
```

### 1.2 Package References

- [x] `Microsoft.Extensions.Http` — IHttpClientFactory & typed clients
- [x] `Microsoft.Extensions.Http.Resilience` — Resilience pipelines (built on Polly 8)
- [x] `Microsoft.Extensions.Options` — Options pattern for config
- [x] `Microsoft.Extensions.DependencyInjection.Abstractions` — DI abstractions
- [x] `System.Text.Json` (inbox but configure) — JSON serialization
- [x] Test projects: `xunit`, `NSubstitute`, `FluentAssertions`, `Microsoft.NET.Test.Sdk`

---

## Phase 2: Configuration & Authentication

### 2.1 Configuration / Options

- [x] `SamsaraClientOptions` record/class:
  - `ApiToken` (string) — Bearer token
  - `BaseUrl` (string, default `https://api.samsara.com`) — US default, EU: `https://api.eu.samsara.com`
  - `DefaultPageSize` (int?, optional, max 512)
  - `Timeout` (TimeSpan, default 30s)
  - `RetryCount` (int, default 3)
- [x] Validation: token required, base URL required, page size ≤ 512
- [x] Support `IOptions<SamsaraClientOptions>` and `IOptionsSnapshot<>` for runtime reload

### 2.2 Authentication Handler

- [x] `SamsaraAuthenticationHandler : DelegatingHandler` — Injects `Authorization: Bearer {token}` on every request
- [x] Support both static API token and dynamic token provider (`Func<CancellationToken, ValueTask<string>>`) for OAuth 2.0 / rotating tokens
- [x] Unit tests: header injection, token refresh

---

## Phase 3: HTTP Pipeline

### 3.1 HttpClient Registration

- [x] `IServiceCollection.AddSamsaraClient(Action<SamsaraClientOptions>)` extension method
- [x] Register named/typed `HttpClient` via `IHttpClientFactory`
- [x] Configure `BaseAddress`, `Timeout`, default headers (`Accept: application/json`, `User-Agent: SamsaraDotnet/{version}`)
- [x] Register `SamsaraAuthenticationHandler` in the handler pipeline

### 3.2 Resilience / Retry Policy

- [x] Add `Microsoft.Extensions.Http.Resilience` pipeline to the HttpClient:
  - **Retry**: Exponential backoff for transient HTTP errors (5xx, 408, `HttpRequestException`)
  - **Rate-limit retry (429)**: Read `Retry-After` header (decimal seconds), wait that duration, retry up to N times
  - **Circuit breaker**: Open after consecutive failures, half-open probe
  - **Timeout**: Per-attempt timeout
- [x] Make retry count & circuit breaker thresholds configurable
- [x] Unit tests: verify retry on 503, 429 with Retry-After

### 3.3 Request / Response Handling

- [x] `SamsaraHttpClient` (internal, wraps `HttpClient`):
  - `GetAsync<T>`, `PostAsync<TReq, TRes>`, `PatchAsync<TReq, TRes>`, `PutAsync<TReq, TRes>`, `DeleteAsync`
  - Generic JSON deserialization with error handling
  - Extracts `requestId` from error responses for diagnostics
  - All methods accept `CancellationToken`
- [x] Request/response logging via `ILogger<SamsaraHttpClient>` (sanitize auth tokens)
- [x] Unit tests: success, 4xx, 5xx error paths

---

## Phase 4: Serialization

### 4.1 System.Text.Json Configuration

- [x] `SamsaraJsonContext : JsonSerializerContext` — Source-generated serializer for all DTOs (AOT safe, fast)
- [x] Configure: `camelCase` naming, `JsonIgnoreCondition.WhenWritingNull`, `JsonStringEnumConverter`
- [x] Custom converters as needed (e.g., Unix timestamps ↔ `DateTimeOffset`, flexible number parsing)
- [x] Unit tests: round-trip serialization for key DTOs

---

## Phase 5: Pagination

### 5.1 Cursor-Based Pagination

- [x] `PaginatedResponse<T>` model:
  ```csharp
  public record PaginatedResponse<T>(
      IReadOnlyList<T> Data,
      PaginationInfo Pagination);

  public record PaginationInfo(
      string EndCursor,
      bool HasNextPage);
  ```
- [x] `IAsyncEnumerable<T>` paginator: Automatically follows `endCursor` while `hasNextPage == true`
- [x] Support `after` query parameter and optional `limit`
- [x] Handle empty/short pages (keep paginating if `hasNextPage` is true)
- [ ] Per-page callback option for progress monitoring
- [ ] Support `/feed` endpoints (real-time sync: re-poll with cursor, 5s wait when `hasNextPage == false`)
- [x] Unit tests: multi-page iteration, empty pages, cancellation

---

## Phase 6: Error Handling

### 6.1 Exception Hierarchy

- [x] `SamsaraApiException` (base) — Contains `StatusCode`, `Message`, `RequestId`
- [x] `SamsaraBadRequestException` (400)
- [x] `SamsaraAuthenticationException` (401)
- [x] `SamsaraNotFoundException` (404)
- [x] `SamsaraRateLimitException` (429) — Includes `RetryAfter` (TimeSpan)
- [x] `SamsaraServerException` (500, 502, 503, 504)
- [x] Error response model: `{ "message": string, "requestId": string }`
- [x] Unit tests: correct exception type per status code

---

## Phase 7: Domain Models (DTOs)

> Organized by resource domain. These are the request/response data transfer objects.
> All models use `record` types with `init` properties for immutability.

### 7.1 Common / Shared Models

- [x] `Pagination` models (above)
- [x] `TagReference` (id, name) — tags are cross-cutting
- [x] `ExternalId` (key, value) — external IDs
- [x] `AttributeValue` model
- [x] `AddressGeofence` model (circle / polygon)

### 7.2 Fleet Assets Domain

- [x] Vehicle models (Vehicle, VehiclePatch, VehicleStats, VehicleLocation)
- [x] Trailer models (Trailer, TrailerPatch, TrailerStats)
- [x] Equipment models (Equipment, EquipmentLocation, EquipmentStats)
- [x] Gateway models

### 7.3 People Domain

- [x] Driver models (Driver, DriverCreate, DriverPatch)
- [x] User models (User, UserCreate, UserPatch)
- [x] Contact models (Contact, ContactCreate, ContactPatch)
- [x] UserRole models

### 7.4 Location & Telemetry Domain

- [x] VehicleLocation (snapshot, feed, history)
- [x] VehicleStats (snapshot, feed, history) — multiple stat types
- [x] EquipmentLocation and EquipmentStats
- [x] Location and Speed models

### 7.5 Safety Domain

- [x] SafetyEvent models
- [x] SafetyScore models (driver, vehicle, tag)
- [x] Coaching models

### 7.6 Compliance (HOS) Domain

- [x] HosClock, HosLog, HosDailyLog
- [x] HosViolation
- [x] ELD event models
- [x] Tachograph models (EU)

### 7.7 Routing Domain

- [x] Route, RouteStop, RouteCreate, RoutePatch
- [x] Hub, HubPlan, HubLocation models
- [x] Trip models

### 7.8 Maintenance Domain

- [x] DVIR, DVIRCreate
- [x] Defect, DefectType, DefectPatch
- [x] WorkOrder, ServiceTask

### 7.9 Documents & Forms Domain

- [x] Document, DocumentType, DocumentCreate
- [x] Form, FormTemplate, FormSubmission

### 7.10 Communication & Alerts Domain

- [x] Message, MessageCreate
- [x] Alert configuration, AlertIncident
- [x] LiveSharingLink

### 7.11 Fuel & Efficiency Domain

- [x] FuelPurchase
- [x] FuelEnergyReport
- [x] DriverEfficiency
- [x] Idling report models

### 7.12 Tags & Attributes Domain

- [x] Tag, TagCreate, TagPatch
- [x] Attribute, AttributeCreate, AttributePatch

### 7.13 Addresses Domain

- [x] Address, AddressCreate, AddressPatch

### 7.14 Webhooks Domain

- [x] Webhook, WebhookCreate, WebhookPatch
- [x] Webhook event payload models

### 7.15 Organization Domain

- [x] Organization info
- [x] Settings (compliance, driver app, safety)

### 7.16 Assignments Domain

- [ ] DriverVehicleAssignment models
- [ ] TrailerAssignment models
- [ ] CarrierProposedAssignment models

### 7.17 Training Domain

- [ ] TrainingAssignment, TrainingCourse

### 7.18 Industrial / Sensors Domain

- [x] IndustrialAsset, DataInput, DataPoint
- [x] Sensor models (legacy, but still used)

### 7.19 Issues Domain

- [ ] Issue, IssuePatch

---

## Phase 8: Service Clients

> Each service client is a typed class that wraps `SamsaraHttpClient` and exposes clean, strongly-typed methods.
> All list methods return `IAsyncEnumerable<T>` for transparent pagination.
> All methods accept `CancellationToken`.

### 8.1 Top-Level Client

- [x] `ISamsaraClient` interface — Facade exposing all domain sub-clients as properties:
  ```csharp
  public interface ISamsaraClient
  {
      IVehiclesClient Vehicles { get; }
      IDriversClient Drivers { get; }
      IEquipmentClient Equipment { get; }
      // ... all domain clients
  }
  ```
- [x] `SamsaraClient` implementation — Composed via DI

### 8.2 Fleet Assets Clients

- [x] `IVehiclesClient` — List, Get, Update vehicles; locations (snapshot/feed/history); stats (snapshot/feed/history)
- [ ] `ITrailersClient` — CRUD trailers; stats
- [x] `IEquipmentClient` — List, Get, Update equipment; locations; stats
- [ ] `IGatewaysClient` — CRUD gateways

### 8.3 People Clients

- [x] `IDriversClient` — CRUD drivers; efficiency; tachograph activity/files
- [x] `IUsersClient` — CRUD users
- [x] `IContactsClient` — CRUD contacts
- [ ] `IUserRolesClient` — List user roles

### 8.4 Location & Telemetry Clients

- [ ] `IVehicleLocationsClient` — Snapshot, Feed (real-time), History
- [ ] `IVehicleStatsClient` — Snapshot, Feed, History with stat types
- [ ] `IEquipmentLocationsClient` — Snapshot, Feed, History
- [ ] `IEquipmentStatsClient` — Snapshot, Feed, History

### 8.5 Safety Clients

- [x] `ISafetyClient` — Safety events; audit log feed; safety scores (driver/vehicle/tag)

### 8.6 Compliance / HOS Clients

- [x] `IHosClient` — Clocks, Logs, Daily logs, Violations, ELD events
- [ ] `ITachographClient` — Activity history, file history (EU)
- [ ] `IIftaClient` — Jurisdiction reports, vehicle reports, CSV export

### 8.7 Routing Clients

- [x] `IRoutesClient` — CRUD routes; route audit log feed
- [ ] `IHubsClient` — Hubs, Hub plans, Hub locations
- [ ] `ITripsClient` — List trips

### 8.8 Maintenance Clients

- [x] `IDvirsClient` — Create, Update, Stream DVIRs
- [x] `IDefectsClient` — List, Update defects; defect types; defect history

### 8.9 Documents & Forms Client

- [x] `IDocumentsClient` — CRUD documents; document types; PDF generation
- [ ] `IFormsClient` — Form templates, form submissions, PDF export

### 8.10 Communication & Alerts Client

- [x] `IMessagesClient` — Send messages
- [x] `IAlertsClient` — Alert configurations, alert incidents

### 8.11 Fuel & Efficiency Client

- [x] `IFuelClient` — Fuel purchases; fuel-energy reports (driver/vehicle); idling reports

### 8.12 Tags & Attributes Client

- [x] `ITagsClient` — CRUD tags (with PUT for full replace)
- [ ] `IAttributesClient` — CRUD attributes (by entity type)

### 8.13 Addresses Client

- [x] `IAddressesClient` — CRUD addresses

### 8.14 Webhooks Client

- [x] `IWebhooksClient` — CRUD webhooks

### 8.15 Organization Client

- [x] `IOrganizationClient` — Get org info; settings (compliance, driver app, safety)

### 8.16 Assignments Client

- [ ] `IDriverVehicleAssignmentsClient` — CRUD driver-vehicle assignments
- [ ] `ITrailerAssignmentsClient` — List, Get trailer assignments
- [ ] `ICarrierProposedAssignmentsClient` — CRUD carrier-proposed assignments

### 8.17 Training Client

- [ ] `ITrainingClient` — Training assignments, courses

### 8.18 Industrial / Sensors Client

- [x] `IIndustrialAssetsClient` — CRUD industrial assets; data inputs/points
- [ ] `ISensorsClient` — Legacy sensor endpoints (cargo, door, temperature, humidity)

### 8.19 Issues Client

- [ ] `IIssuesClient` — Get, Stream, Update issues

### 8.20 Media Client

- [ ] `IMediaClient` — Retrieve media, list uploaded media

---

## Phase 9: Dependency Injection & Registration

- [x] `IServiceCollection.AddSamsaraClient(Action<SamsaraClientOptions>)` — Registers everything
- [x] All domain clients registered as scoped services in DI
- [x] `ISamsaraClient` as the aggregate entry point
- [ ] Support multiple named clients (e.g., different tokens/orgs) via keyed services (.NET 8)

---

## Phase 10: Testing

### 10.1 Unit Tests

- [x] Authentication handler tests
- [x] Serialization round-trip tests for key DTOs
- [x] Pagination iterator tests (multi-page, empty pages, cancellation)
- [x] Error handling tests (each exception type)
- [x] Resilience tests (retry, circuit breaker, rate-limit backoff)
- [x] Service client tests (using mocked `HttpMessageHandler`)

### 10.2 Integration Tests

- [ ] Optional, gated by `SAMSARA_API_TOKEN` environment variable
- [ ] Real calls to a few safe read-only endpoints (e.g., `GET /me`, `GET /fleet/vehicles`)
- [ ] Verify pagination works end-to-end
- [ ] Verify error responses deserialize correctly

---

## Phase 11: Packaging & Documentation

### 11.1 NuGet Packaging

- [x] Package metadata in `Samsara.Sdk.csproj`: `PackageId`, `Description`, `Authors`, `License`, `RepositoryUrl`, `PackageTags`
- [x] Source Link for debugger step-through
- [x] Deterministic builds
- [x] Include XML doc comments in package

### 11.2 Documentation

- [x] `README.md` — Overview, installation, quick start, authentication, pagination, error handling, DI setup
- [ ] XML doc comments on all public APIs
- [x] Code examples in README for common scenarios:
  - List all vehicles with pagination
  - Get vehicle stats feed (real-time)
  - Create a route
  - Handle rate limits

---

## Phase 12: Advanced Features (Stretch Goals)

- [ ] **Webhook receiver** — ASP.NET Core middleware to validate & deserialize incoming webhook payloads
- [ ] **Observability** — OpenTelemetry integration (HTTP client instrumentation, custom activities for API calls)
- [ ] **Feed polling service** — `IHostedService` that continuously polls `/feed` endpoints and pushes to `IObservable<T>` or `Channel<T>`
- [ ] **Typed query builders** — Fluent filter/query construction for complex endpoints (e.g., stats types, time ranges)

---

## Implementation Order (Recommended)

| Step | What | Why |
|------|-------|------|
| 1 | Phase 1 (Foundation) | Solution structure, packages, build plumbing |
| 2 | Phase 2 (Config & Auth) | Need auth before any HTTP call |
| 3 | Phase 3 (HTTP Pipeline) | Core transport with resilience |
| 4 | Phase 4 (Serialization) | JSON handling for all DTOs |
| 5 | Phase 6 (Error Handling) | Need exceptions before building clients |
| 6 | Phase 5 (Pagination) | Pagination depends on HTTP + serialization |
| 7 | Phase 7.1-7.3 (Core Models) | Shared + fleet + people DTOs first |
| 8 | Phase 8.1-8.3 (Core Clients) | Top-level client, vehicles, drivers |
| 9 | Phase 9 (DI Registration) | Wire everything up, usable at this point |
| 10 | Phase 10.1 (Unit Tests) | Validate core infrastructure |
| 11 | Phase 7.4-7.19 (Remaining Models) | Build out remaining domains |
| 12 | Phase 8.4-8.20 (Remaining Clients) | Build out remaining domain clients |
| 13 | Phase 10.2 (Integration Tests) | End-to-end validation |
| 14 | Phase 11 (Packaging & Docs) | Ship it |
| 15 | Phase 12 (Stretch) | Polish & advanced features |

---

## Key Design Decisions

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Target framework | .NET 8 (net8.0) | LTS, latest patterns, keyed services, source generators |
| HTTP client | `IHttpClientFactory` + typed clients | Proper lifetime management, handler pipeline, DI-friendly |
| Resilience | `Microsoft.Extensions.Http.Resilience` | First-party, Polly 8-based, integrates with `IHttpClientFactory` |
| Serialization | `System.Text.Json` + source generators | AOT-safe, high performance, no reflection |
| Pagination | `IAsyncEnumerable<T>` | Natural C# async iteration, lazy, composable with LINQ |
| DTOs | `record` types with `init` props | Immutable, value equality, concise |
| Error handling | Typed exception hierarchy | Enables callers to catch specific error types |
| Configuration | `IOptions<T>` pattern | Standard .NET config, supports reload, validation |
| Base URL | Configurable (US default, EU option) | Samsara has regional endpoints |
| Rate limiting | `Retry-After` header-driven backoff | Samsara provides exact wait time in response |

---

## API Key Facts (Reference)

| Aspect | Detail |
|--------|--------|
| Base URL (US) | `https://api.samsara.com` |
| Base URL (EU) | `https://api.eu.samsara.com` |
| Auth header | `Authorization: Bearer {token}` |
| Pagination | Cursor-based: `after` query param, response `pagination.endCursor` / `pagination.hasNextPage` |
| Page size | `limit` query param, max 512 |
| Cursor expiry | 30 days |
| Rate limit (global) | 150 req/s per token, 200 req/s per org |
| Rate limit (endpoint) | Level 1: 100/min, Level 2: 5/s, Level 3: 10/s |
| Rate limit response | HTTP 429 with `Retry-After` header (seconds, decimal) |
| Error body | `{ "message": "...", "requestId": "..." }` |
| Error codes | 400, 401, 404, 405, 429, 500, 501, 502, 503, 504 |
| TLS | HTTPS required, TLS 1.2+ |
| Feed endpoints | Real-time sync via cursor; wait ≥5s when `hasNextPage: false` |
