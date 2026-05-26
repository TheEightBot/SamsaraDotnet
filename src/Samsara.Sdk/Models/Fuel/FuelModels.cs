namespace Samsara.Sdk.Models.Fuel;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

// ── Fuel & energy reports ─────────────────────────────────────────────────────

/// <summary>A vehicle-keyed fuel/energy report row (item in <c>GET /fleet/reports/vehicles/fuel-energy</c>).</summary>
public sealed record FuelEnergyVehicleReport
{
    [JsonPropertyName("vehicle")]
    public EntityReference? Vehicle { get; init; }

    [JsonPropertyName("distanceTraveledMeters")]
    public double? DistanceTraveledMeters { get; init; }

    [JsonPropertyName("efficiencyMpge")]
    public double? EfficiencyMpge { get; init; }

    [JsonPropertyName("energyUsedKwh")]
    public double? EnergyUsedKwh { get; init; }

    [JsonPropertyName("engineIdleTimeDurationMs")]
    public long? EngineIdleTimeDurationMs { get; init; }

    [JsonPropertyName("engineRunTimeDurationMs")]
    public long? EngineRunTimeDurationMs { get; init; }

    [JsonPropertyName("estCarbonEmissionsKg")]
    public double? EstCarbonEmissionsKg { get; init; }

    [JsonPropertyName("estFuelEnergyCost")]
    public FuelEnergyCost? EstFuelEnergyCost { get; init; }

    [JsonPropertyName("fuelConsumedMl")]
    public double? FuelConsumedMl { get; init; }
}

/// <summary>A driver-keyed fuel/energy report row.</summary>
public sealed record FuelEnergyDriverReport
{
    [JsonPropertyName("driver")]
    public EntityReference? Driver { get; init; }

    [JsonPropertyName("distanceTraveledMeters")]
    public double? DistanceTraveledMeters { get; init; }

    [JsonPropertyName("efficiencyMpge")]
    public double? EfficiencyMpge { get; init; }

    [JsonPropertyName("energyUsedKwh")]
    public double? EnergyUsedKwh { get; init; }

    [JsonPropertyName("engineIdleTimeDurationMs")]
    public long? EngineIdleTimeDurationMs { get; init; }

    [JsonPropertyName("engineRunTimeDurationMs")]
    public long? EngineRunTimeDurationMs { get; init; }

    [JsonPropertyName("estCarbonEmissionsKg")]
    public double? EstCarbonEmissionsKg { get; init; }

    [JsonPropertyName("estFuelEnergyCost")]
    public FuelEnergyCost? EstFuelEnergyCost { get; init; }

    [JsonPropertyName("fuelConsumedMl")]
    public double? FuelConsumedMl { get; init; }
}

/// <summary>Cost values returned inside a fuel/energy report.</summary>
public sealed record FuelEnergyCost
{
    [JsonPropertyName("amount")] public double? Amount { get; init; }
    [JsonPropertyName("currency")] public string? Currency { get; init; }
}

/// <summary><c>data</c> wrapper for the vehicle fuel-energy report endpoint.</summary>
public sealed record FuelEnergyVehicleReportsResponse
{
    [JsonPropertyName("vehicleReports")]
    public IReadOnlyList<FuelEnergyVehicleReport>? VehicleReports { get; init; }
}

/// <summary><c>data</c> wrapper for the driver fuel-energy report endpoint.</summary>
public sealed record FuelEnergyDriverReportsResponse
{
    [JsonPropertyName("driverReports")]
    public IReadOnlyList<FuelEnergyDriverReport>? DriverReports { get; init; }
}

// ── Driver efficiency ─────────────────────────────────────────────────────────

/// <summary>Per-driver efficiency datapoint (item of <c>GET /driver-efficiency/drivers</c>).</summary>
public sealed record DriverEfficiencyByDriver
{
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }
    [JsonPropertyName("difficultyScore")] public object? DifficultyScore { get; init; }
    [JsonPropertyName("percentageData")] public object? PercentageData { get; init; }
    [JsonPropertyName("rawData")] public object? RawData { get; init; }
    [JsonPropertyName("scoreData")] public object? ScoreData { get; init; }
}

/// <summary>Per-vehicle efficiency datapoint.</summary>
public sealed record DriverEfficiencyByVehicle
{
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("difficultyScore")] public object? DifficultyScore { get; init; }
    [JsonPropertyName("percentageData")] public object? PercentageData { get; init; }
    [JsonPropertyName("rawData")] public object? RawData { get; init; }
    [JsonPropertyName("scoreData")] public object? ScoreData { get; init; }
}

// ── Fuel purchase ────────────────────────────────────────────────────────────

/// <summary>Request body for <c>POST /fuel-purchase</c>.</summary>
public sealed record CreateFuelPurchaseRequest
{
    [JsonPropertyName("fuelQuantityLiters")] public required string FuelQuantityLiters { get; init; }
    [JsonPropertyName("transactionLocation")] public required string TransactionLocation { get; init; }
    [JsonPropertyName("transactionPrice")] public required object TransactionPrice { get; init; }
    [JsonPropertyName("transactionReference")] public required string TransactionReference { get; init; }
    [JsonPropertyName("transactionTime")] public required string TransactionTime { get; init; }
    [JsonPropertyName("discount")] public object? Discount { get; init; }
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }
    [JsonPropertyName("fuelGrade")] public string? FuelGrade { get; init; }
    [JsonPropertyName("iftaFuelType")] public string? IftaFuelType { get; init; }
    [JsonPropertyName("merchantName")] public string? MerchantName { get; init; }
    [JsonPropertyName("source")] public string? Source { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
}

/// <summary>Response of <c>POST /fuel-purchase</c>.</summary>
public sealed record FuelPurchase
{
    [JsonPropertyName("id")] public string? Id { get; init; }
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("transactionReference")] public string? TransactionReference { get; init; }
    [JsonPropertyName("transactionTime")] public DateTimeOffset? TransactionTime { get; init; }
    [JsonPropertyName("transactionLocation")] public string? TransactionLocation { get; init; }
    [JsonPropertyName("fuelQuantityLiters")] public string? FuelQuantityLiters { get; init; }
    [JsonPropertyName("fuelGrade")] public string? FuelGrade { get; init; }
    [JsonPropertyName("iftaFuelType")] public string? IftaFuelType { get; init; }
}
