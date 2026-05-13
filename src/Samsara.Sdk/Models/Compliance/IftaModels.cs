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

/// <summary>Represents an IFTA detail CSV export job.</summary>
public sealed record IftaDetailJob
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("downloadUrl")] public string? DownloadUrl { get; init; }
    [JsonPropertyName("year")] public int? Year { get; init; }
    [JsonPropertyName("month")] public int? Month { get; init; }
}

/// <summary>Request body for creating an IFTA detail CSV job.</summary>
public sealed record CreateIftaDetailJobRequest
{
    [JsonPropertyName("year")] public required int Year { get; init; }
    [JsonPropertyName("month")] public required int Month { get; init; }
    [JsonPropertyName("vehicleIds")] public IReadOnlyList<string>? VehicleIds { get; init; }
}
