namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

/// <summary>Represents a vehicle idling event.</summary>
public sealed record IdlingEvent
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("vehicleName")] public string? VehicleName { get; init; }
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }
    [JsonPropertyName("driverName")] public string? DriverName { get; init; }
    [JsonPropertyName("startTime")] public DateTimeOffset? StartTime { get; init; }
    [JsonPropertyName("endTime")] public DateTimeOffset? EndTime { get; init; }
    [JsonPropertyName("durationMs")] public long? DurationMs { get; init; }
    [JsonPropertyName("fuelConsumedMl")] public double? FuelConsumedMl { get; init; }
    [JsonPropertyName("address")] public string? Address { get; init; }
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }
}
