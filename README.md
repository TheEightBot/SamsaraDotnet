# Samsara .NET SDK

<div align="center">

<img src="images/logo.png" alt="Samsara .NET SDK" width="320" />

[![NuGet](https://img.shields.io/nuget/v/Samsara.Sdk.svg?label=NuGet&logo=nuget)](https://www.nuget.org/packages/Samsara.Sdk)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Samsara.Sdk.svg?label=Downloads&logo=nuget)](https://www.nuget.org/packages/Samsara.Sdk)
[![CI](https://github.com/TheEightBot/SamsaraDotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/TheEightBot/SamsaraDotnet/actions/workflows/ci.yml)
[![codecov](https://codecov.io/gh/TheEightBot/SamsaraDotnet/branch/main/graph/badge.svg)](https://codecov.io/gh/TheEightBot/SamsaraDotnet)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

**A modern, idiomatic .NET 8 client for the [Samsara Fleet Management API](https://developers.samsara.com/)**

[Getting Started](#quick-start) · [API Reference](#domain-clients) · [Configuration](#configuration) · [Contributing](CONTRIBUTING.md) · [Changelog](CHANGELOG.md)

</div>

---

## Features

- 🚀 **33 typed domain clients** — vehicles, drivers, routes, compliance, safety, and every other Samsara API domain
- 📄 **Automatic pagination** — `IAsyncEnumerable<T>` handles cursor-based paging transparently
- 🔒 **DI-first design** — single `AddSamsaraClient()` call wires up everything
- ⚡ **Source-generated JSON** — zero-reflection `System.Text.Json` serialization via compile-time context
- 🛡️ **Built-in resilience** — exponential backoff with jitter via `Microsoft.Extensions.Http.Resilience`
- 💡 **Typed exceptions** — `SamsaraBadRequestException`, `SamsaraRateLimitException`, and more
- 🔄 **Dynamic token provider** — OAuth 2.0, rotating tokens, and custom auth flows
- 🌍 **Multi-region** — US and EU endpoints with a single property switch
- 📦 **Immutable models** — every request and response uses `record` types
- 🔍 **Source Link** — step into library source during debugging

---

## Installation

```shell
dotnet add package Samsara.Sdk
```

> **Requirements:** .NET 8.0 or later

---

## Quick Start

### With Dependency Injection (ASP.NET Core / Generic Host)

```csharp
// Program.cs
using Samsara.Sdk.DependencyInjection;

builder.Services.AddSamsaraClient(options =>
{
    options.ApiToken = builder.Configuration["Samsara:ApiToken"];
});
```

Inject `ISamsaraClient` anywhere in your application:

```csharp
public class FleetService(ISamsaraClient samsara)
{
    public async Task<List<Vehicle>> GetAllVehiclesAsync(CancellationToken ct)
    {
        var vehicles = new List<Vehicle>();
        await foreach (var vehicle in samsara.Vehicles.ListAsync(ct))
            vehicles.Add(vehicle);
        return vehicles;
    }
}
```

### Without Dependency Injection

```csharp
using Microsoft.Extensions.DependencyInjection;
using Samsara.Sdk.DependencyInjection;

var services = new ServiceCollection();
services.AddSamsaraClient(options => options.ApiToken = "samsara_api_...");

var provider = services.BuildServiceProvider();
var samsara = provider.GetRequiredService<ISamsaraClient>();

await foreach (var driver in samsara.Drivers.ListAsync())
    Console.WriteLine(driver.Name);
```

---

## Configuration

Configure via `AddSamsaraClient()` delegate or bind directly from `appsettings.json`.

### Options Reference

| Property | Type | Default | Description |
|---|---|---|---|
| `ApiToken` | `string?` | — | Bearer token for authentication. Required if `TokenProvider` is not set. |
| `TokenProvider` | `Func<CancellationToken, ValueTask<string>>?` | — | Dynamic token delegate. Takes precedence over `ApiToken`. |
| `BaseUrl` | `string` | `https://api.samsara.com` | API base URL. Use `SamsaraClientOptions.EuBaseUrl` for EU. |
| `DefaultPageSize` | `int?` | `null` (API default) | Override the page size for paginated requests (1–512). |
| `Timeout` | `TimeSpan` | `00:00:30` | Per-request HTTP timeout. |
| `RetryCount` | `int` | `3` | Max retry attempts for transient failures (0–10). |

### appsettings.json

```json
{
  "Samsara": {
    "ApiToken": "samsara_api_...",
    "BaseUrl": "https://api.samsara.com",
    "DefaultPageSize": 100,
    "Timeout": "00:00:30",
    "RetryCount": 3
  }
}
```

```csharp
builder.Services.AddSamsaraClient(options =>
    builder.Configuration.GetSection("Samsara").Bind(options));
```

### EU Region

```csharp
builder.Services.AddSamsaraClient(options =>
{
    options.ApiToken = "samsara_api_...";
    options.BaseUrl = SamsaraClientOptions.EuBaseUrl;
});
```

### Dynamic Token Provider

Use for OAuth 2.0, Azure Managed Identity, or any rotating-token scenario:

```csharp
builder.Services.AddSamsaraClient(options =>
{
    options.TokenProvider = async ct =>
    {
        // Fetch token from your identity provider or secrets manager
        return await myAuthService.GetBearerTokenAsync(ct);
    };
});
```

---

## Domain Clients

`ISamsaraClient` exposes every Samsara API domain as a typed sub-client:

### Fleet & Assets

| Client | Property | Key Operations |
|---|---|---|
| `IVehiclesClient` | `samsara.Vehicles` | `ListAsync`, `GetAsync`, `UpdateAsync`, `ListLocationsAsync`, `ListStatsAsync` |
| `IDriversClient` | `samsara.Drivers` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `ITrailersClient` | `samsara.Trailers` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `IEquipmentClient` | `samsara.Equipment` | `ListAsync`, `GetAsync`, `UpdateAsync` |
| `IGatewaysClient` | `samsara.Gateways` | `ListAsync`, `GetAsync` |

### Routing & Dispatch

| Client | Property | Key Operations |
|---|---|---|
| `IRoutesClient` | `samsara.Routes` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `ITripsClient` | `samsara.Trips` | `ListAsync`, `GetAsync` |
| `IHubsClient` | `samsara.Hubs` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `IAddressesClient` | `samsara.Addresses` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |

### Safety & Compliance

| Client | Property | Key Operations |
|---|---|---|
| `ISafetyClient` | `samsara.Safety` | `ListEventsAsync`, `GetScoreAsync` |
| `IComplianceClient` | `samsara.Compliance` | `ListHosLogsAsync`, `ListDvirsAsync`, `CreateDvirAsync` |
| `ITachographClient` | `samsara.Tachograph` | `ListFilesAsync`, `GetFileAsync` |
| `IIftaClient` | `samsara.Ifta` | `ListSummaryAsync` |

### Maintenance

| Client | Property | Key Operations |
|---|---|---|
| `IMaintenanceClient` | `samsara.Maintenance` | `ListAlertsAsync`, `ListDvirsAsync` |
| `IIssuesClient` | `samsara.Issues` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync` |
| `IFuelClient` | `samsara.Fuel` | `ListPurchasesAsync`, `ListEnergyAsync` |

### Organization & Users

| Client | Property | Key Operations |
|---|---|---|
| `IOrganizationClient` | `samsara.Organization` | `GetAsync`, `UpdateAsync` |
| `IUsersClient` | `samsara.Users` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `IUserRolesClient` | `samsara.UserRoles` | `ListAsync` |
| `IContactsClient` | `samsara.Contacts` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |

### Documents & Forms

| Client | Property | Key Operations |
|---|---|---|
| `IDocumentsClient` | `samsara.Documents` | `ListAsync`, `GetAsync`, `CreateAsync`, `DeleteAsync` |
| `IFormsClient` | `samsara.Forms` | `ListTemplatesAsync`, `ListSubmissionsAsync` |
| `IMediaClient` | `samsara.Media` | `ListAsync`, `GetAsync` |

### Communication & Alerts

| Client | Property | Key Operations |
|---|---|---|
| `IAlertsClient` | `samsara.Alerts` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `IMessagesClient` | `samsara.Messages` | `ListAsync`, `SendAsync` |
| `IWebhooksClient` | `samsara.Webhooks` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |

### Tags, Attributes & Assignments

| Client | Property | Key Operations |
|---|---|---|
| `ITagsClient` | `samsara.Tags` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `IAttributesClient` | `samsara.Attributes` | `ListAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `IDriverVehicleAssignmentsClient` | `samsara.DriverVehicleAssignments` | `ListAsync`, `CreateAsync`, `DeleteAsync` |
| `ITrailerAssignmentsClient` | `samsara.TrailerAssignments` | `ListAsync`, `CreateAsync`, `DeleteAsync` |
| `ICarrierProposedAssignmentsClient` | `samsara.CarrierProposedAssignments` | `ListAsync`, `CreateAsync` |

### Industrial & Sensors

| Client | Property | Key Operations |
|---|---|---|
| `IIndustrialClient` | `samsara.Industrial` | `ListAssetsAsync`, `GetAssetAsync`, `ListDataInputsAsync` |
| `ISensorsClient` | `samsara.Sensors` | `ListAsync`, `GetHumidityAsync`, `GetTemperatureAsync`, `GetCargoAsync` |

### Training

| Client | Property | Key Operations |
|---|---|---|
| `ITrainingClient` | `samsara.Training` | `ListCoursesAsync`, `ListAssignmentsAsync` |

---

## Usage Examples

### Pagination

All list endpoints return `IAsyncEnumerable<T>`. The SDK fetches subsequent pages automatically:

```csharp
// Stream all vehicles — pages fetched on demand
await foreach (var vehicle in samsara.Vehicles.ListAsync(ct))
{
    Console.WriteLine($"{vehicle.Id}: {vehicle.Name}");
}

// Materialize to a list (System.Linq.Async)
var allDrivers = await samsara.Drivers.ListAsync(ct).ToListAsync(ct);

// Take only the first page's worth
var firstHundred = await samsara.Tags.ListAsync(ct).Take(100).ToListAsync(ct);
```

### CRUD Operations

```csharp
// Create
var tag = await samsara.Tags.CreateAsync(new CreateTagRequest("Night Shift"), ct);

// Read
var vehicle = await samsara.Vehicles.GetAsync("vehicleId", ct);

// Update
var updated = await samsara.Tags.UpdateAsync(tag.Id, new UpdateTagRequest("Night Shift Updated"), ct);

// Delete
await samsara.Tags.DeleteAsync(tag.Id, ct);
```

### Vehicle Stats

```csharp
// Stream live GPS + engine state for all vehicles
await foreach (var stats in samsara.Vehicles.ListStatsAsync("engineStates,gps", ct))
{
    Console.WriteLine($"{stats.Name}: {stats.GpsOdometer}");
}
```

### Vehicle Locations

```csharp
await foreach (var location in samsara.Vehicles.ListLocationsAsync(ct))
{
    Console.WriteLine($"{location.Name}: {location.Latitude}, {location.Longitude}");
}
```

### Create a Route

```csharp
var route = await samsara.Routes.CreateAsync(new CreateRouteRequest(
    Name: "Morning Delivery",
    DriverId: "driver-123",
    VehicleId: "vehicle-456"
), ct);
```

### Send a Driver Message

```csharp
await samsara.Messages.SendAsync(new SendMessageRequest(
    DriverId: "driver-789",
    Text: "Proceed to stop #3"
), ct);
```

### Safety Events

```csharp
await foreach (var ev in samsara.Safety.ListEventsAsync(ct))
{
    Console.WriteLine($"[{ev.Type}] Driver: {ev.DriverId} at {ev.Time}");
}
```

### HOS Compliance Logs

```csharp
await foreach (var log in samsara.Compliance.ListHosLogsAsync(ct))
{
    Console.WriteLine($"{log.DriverId}: {log.Status} ({log.StartTime} – {log.EndTime})");
}
```

---

## Error Handling

The SDK throws strongly-typed exceptions. All exceptions inherit from `SamsaraApiException` which exposes `StatusCode` and `RequestId` for tracing.

| Exception | HTTP Status | Notes |
|---|---|---|
| `SamsaraBadRequestException` | 400 | Invalid request payload or parameters |
| `SamsaraAuthenticationException` | 401 | Invalid or expired API token |
| `SamsaraNotFoundException` | 404 | Resource does not exist |
| `SamsaraRateLimitException` | 429 | Includes `RetryAfter` property |
| `SamsaraServerException` | 500, 502, 503, 504 | Transient server errors |

```csharp
try
{
    var vehicle = await samsara.Vehicles.GetAsync("bad-id", ct);
}
catch (SamsaraNotFoundException ex)
{
    Console.Error.WriteLine($"Not found — request ID: {ex.RequestId}");
}
catch (SamsaraRateLimitException ex) when (ex.RetryAfter.HasValue)
{
    await Task.Delay(ex.RetryAfter.Value, ct);
    // retry...
}
catch (SamsaraApiException ex)
{
    Console.Error.WriteLine($"API error {(int)ex.StatusCode}: {ex.Message}");
}
```

---

## Resilience

The SDK uses `Microsoft.Extensions.Http.Resilience` to automatically retry transient failures (5xx errors and network timeouts) with exponential backoff and jitter. No additional configuration is required for standard use.

```csharp
builder.Services.AddSamsaraClient(options =>
{
    options.ApiToken = "samsara_api_...";
    options.RetryCount = 5;   // Default: 3. Set 0 to disable retries entirely.
    options.Timeout = TimeSpan.FromSeconds(60);
});
```

---

## Requirements

| Component | Version |
|---|---|
| .NET | 8.0 or later |
| `Microsoft.Extensions.Http` | 8.x |
| `Microsoft.Extensions.Http.Resilience` | 8.x |
| `Microsoft.Extensions.DependencyInjection.Abstractions` | 8.x |

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for build instructions, coding conventions, and the release process.

---

## License

[MIT](LICENSE)
