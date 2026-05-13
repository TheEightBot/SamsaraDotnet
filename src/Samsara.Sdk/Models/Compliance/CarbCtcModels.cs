namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;

/// <summary>Represents a vehicle enrolled in CARB CTC compliance.</summary>
public sealed record CarbCtcVehicle
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("vin")] public string? Vin { get; init; }
    [JsonPropertyName("licensePlate")] public string? LicensePlate { get; init; }
    [JsonPropertyName("complianceStatus")] public string? ComplianceStatus { get; init; }
    [JsonPropertyName("modelYear")] public int? ModelYear { get; init; }
    [JsonPropertyName("fuelType")] public string? FuelType { get; init; }
}

/// <summary>Represents a historical CARB CTC event for a vehicle.</summary>
public sealed record CarbCtcVehicleHistory
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
    [JsonPropertyName("event")] public string? Event { get; init; }
    [JsonPropertyName("details")] public string? Details { get; init; }
}
