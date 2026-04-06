namespace Samsara.Sdk.Models.Routes;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a route in the Samsara system.
/// </summary>
public sealed record Route
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("scheduledStartMs")]
    public long? ScheduledStartMs { get; init; }

    [JsonPropertyName("scheduledEndMs")]
    public long? ScheduledEndMs { get; init; }

    [JsonPropertyName("actualStartMs")]
    public long? ActualStartMs { get; init; }

    [JsonPropertyName("actualEndMs")]
    public long? ActualEndMs { get; init; }

    [JsonPropertyName("driver")]
    public RouteDriver? Driver { get; init; }

    [JsonPropertyName("vehicle")]
    public RouteVehicle? Vehicle { get; init; }

    [JsonPropertyName("stops")]
    public IReadOnlyList<RouteStop>? Stops { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }
}

/// <summary>
/// Driver assigned to a route.
/// </summary>
public sealed record RouteDriver
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Vehicle assigned to a route.
/// </summary>
public sealed record RouteVehicle
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A stop within a route.
/// </summary>
public sealed record RouteStop
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    [JsonPropertyName("actualArrivalTime")]
    public DateTimeOffset? ActualArrivalTime { get; init; }

    [JsonPropertyName("actualDepartureTime")]
    public DateTimeOffset? ActualDepartureTime { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("singleUseLocation")]
    public SingleUseLocation? SingleUseLocation { get; init; }
}

/// <summary>
/// A one-time location used as a route stop (not saved as an address).
/// </summary>
public sealed record SingleUseLocation
{
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}

/// <summary>
/// Route settings.
/// </summary>
public sealed record RouteSettings
{
    [JsonPropertyName("routeCompletionCondition")]
    public string? RouteCompletionCondition { get; init; }

    [JsonPropertyName("routeStartingCondition")]
    public string? RouteStartingCondition { get; init; }
}

/// <summary>
/// Request body for creating a new route.
/// </summary>
public sealed record CreateRouteRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("scheduledStartMs")]
    public long? ScheduledStartMs { get; init; }

    [JsonPropertyName("scheduledEndMs")]
    public long? ScheduledEndMs { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("stops")]
    public required IReadOnlyList<CreateRouteStopRequest> Stops { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }
}

/// <summary>
/// Request body for a stop in a new route.
/// </summary>
public sealed record CreateRouteStopRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("singleUseLocation")]
    public SingleUseLocation? SingleUseLocation { get; init; }
}

/// <summary>
/// Request body for updating a route (PATCH).
/// </summary>
public sealed record UpdateRouteRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("scheduledStartMs")]
    public long? ScheduledStartMs { get; init; }

    [JsonPropertyName("scheduledEndMs")]
    public long? ScheduledEndMs { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }
}
