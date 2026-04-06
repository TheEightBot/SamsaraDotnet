namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;

public sealed record IftaDetail
{
    [JsonPropertyName("jurisdiction")]
    public string? Jurisdiction { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("totalDistanceMeters")]
    public double? TotalDistanceMeters { get; init; }

    [JsonPropertyName("fuelConsumedMl")]
    public double? FuelConsumedMl { get; init; }

    [JsonPropertyName("fuelType")]
    public string? FuelType { get; init; }

    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }
}

public sealed record IftaSummary
{
    [JsonPropertyName("jurisdiction")]
    public string? Jurisdiction { get; init; }

    [JsonPropertyName("totalDistanceMeters")]
    public double? TotalDistanceMeters { get; init; }

    [JsonPropertyName("taxableDistanceMeters")]
    public double? TaxableDistanceMeters { get; init; }

    [JsonPropertyName("fuelConsumedMl")]
    public double? FuelConsumedMl { get; init; }

    [JsonPropertyName("taxPaidGallons")]
    public double? TaxPaidGallons { get; init; }

    [JsonPropertyName("netTaxableGallons")]
    public double? NetTaxableGallons { get; init; }

    [JsonPropertyName("fuelType")]
    public string? FuelType { get; init; }

    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }
}
