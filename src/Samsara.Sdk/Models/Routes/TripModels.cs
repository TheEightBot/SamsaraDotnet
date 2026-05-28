namespace Samsara.Sdk.Models.Routes;

using System.Text.Json.Serialization;

public sealed record Trip
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("asset")]
    public object? Asset { get; init; }

    [JsonPropertyName("completionStatus")]
    public string? CompletionStatus { get; init; }

    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    [JsonPropertyName("tripStartTime")]
    public DateTimeOffset? TripStartTime { get; init; }

    [JsonPropertyName("tripEndTime")]
    public DateTimeOffset? TripEndTime { get; init; }

    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }

    [JsonPropertyName("trips")]
    public IReadOnlyList<object>? Trips { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    [JsonPropertyName("startLocation")]
    public TripLocation? StartLocation { get; init; }

    [JsonPropertyName("endLocation")]
    public TripLocation? EndLocation { get; init; }

    [JsonPropertyName("distanceMeters")]
    public double? DistanceMeters { get; init; }

    [JsonPropertyName("durationMs")]
    public long? DurationMs { get; init; }

    [JsonPropertyName("fuelConsumedMl")]
    public double? FuelConsumedMl { get; init; }

    [JsonPropertyName("coDriver")]
    public RouteDriver? CoDriver { get; init; }
}

public sealed record TripLocation
{
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
