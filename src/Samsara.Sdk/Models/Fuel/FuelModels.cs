namespace Samsara.Sdk.Models.Fuel;

using System.Text.Json.Serialization;

/// <summary>
/// Fuel purchase / transaction.
/// </summary>
public sealed record FuelPurchase
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("transactionDateMs")]
    public long? TransactionDateMs { get; init; }

    [JsonPropertyName("vendor")]
    public string? Vendor { get; init; }

    [JsonPropertyName("fuelType")]
    public string? FuelType { get; init; }

    [JsonPropertyName("volumeGallons")]
    public double? VolumeGallons { get; init; }

    [JsonPropertyName("costCents")]
    public long? CostCents { get; init; }

    [JsonPropertyName("pricePerGallon")]
    public double? PricePerGallon { get; init; }

    [JsonPropertyName("odometerMeters")]
    public double? OdometerMeters { get; init; }

    [JsonPropertyName("location")]
    public FuelPurchaseLocation? Location { get; init; }
}

/// <summary>
/// Location of a fuel purchase.
/// </summary>
public sealed record FuelPurchaseLocation
{
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("address")]
    public string? Address { get; init; }
}

/// <summary>
/// Fuel energy level for electric vehicles or reporting.
/// </summary>
public sealed record FuelEnergyLevel
{
    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("percent")]
    public double? Percent { get; init; }

    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }
}
