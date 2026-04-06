# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.1.0] - 2025-04-06

### Added

- Initial release of `Samsara.Sdk`
- Typed `ISamsaraClient` facade exposing 33 domain service clients
- `IAsyncEnumerable<T>` automatic cursor-based pagination across all list endpoints
- Source-generated `System.Text.Json` serialization (zero-reflection)
- `IHttpClientFactory` integration via `AddSamsaraClient()` extension method
- Built-in resilience via `Microsoft.Extensions.Http.Resilience` — exponential backoff with jitter
- Bearer token authentication via `SamsaraAuthenticationHandler`
- Dynamic `TokenProvider` delegate for OAuth 2.0 / rotating-token scenarios
- EU region support via `SamsaraClientOptions.EuBaseUrl`
- Typed exception hierarchy: `SamsaraBadRequestException`, `SamsaraAuthenticationException`,
  `SamsaraNotFoundException`, `SamsaraRateLimitException`, `SamsaraServerException`
- Configurable `RetryCount`, `Timeout`, `DefaultPageSize` options
- Immutable `record` request/response models for all API domains
- Domain clients: Tags, Addresses, Vehicles, Drivers, Safety, Routes, Compliance,
  Maintenance, Documents, Alerts, Fuel, Webhooks, Organization, Users, Contacts,
  Equipment, Industrial, Messages, Trailers, Gateways, UserRoles, Tachograph,
  IFTA, Hubs, Trips, Forms, Attributes, DriverVehicleAssignments,
  TrailerAssignments, CarrierProposedAssignments, Training, Sensors, Issues, Media
- `Samsara.Cli` interactive terminal tool (Spectre.Console TUI)
- XML documentation on all public API surface

[Unreleased]: https://github.com/TheEightBot/SamsaraDotnet/compare/v0.1.0...HEAD
[0.1.0]: https://github.com/TheEightBot/SamsaraDotnet/releases/tag/v0.1.0
