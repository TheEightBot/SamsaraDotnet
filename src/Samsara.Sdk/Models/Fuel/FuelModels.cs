namespace Samsara.Sdk.Models.Fuel;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

// ── Fuel & energy reports ─────────────────────────────────────────────────────

/// <summary>
/// A vehicle-keyed fuel/energy report row (item in <c>GET /fleet/reports/vehicles/fuel-energy</c>).
/// Spec marks <c>vehicle</c>, <c>distanceTraveledMeters</c>, <c>efficiencyMpge</c>,
/// and <c>estFuelEnergyCost</c> as REQUIRED.
/// </summary>
public sealed record FuelEnergyVehicleReport
{
    [JsonPropertyName("vehicle")]
    public required EntityReference Vehicle { get; init; }

    [JsonPropertyName("distanceTraveledMeters")]
    public required double DistanceTraveledMeters { get; init; }

    [JsonPropertyName("efficiencyMpge")]
    public required double EfficiencyMpge { get; init; }

    [JsonPropertyName("energyUsedKwh")]
    public double? EnergyUsedKwh { get; init; }

    [JsonPropertyName("engineIdleTimeDurationMs")]
    public long? EngineIdleTimeDurationMs { get; init; }

    [JsonPropertyName("engineRunTimeDurationMs")]
    public long? EngineRunTimeDurationMs { get; init; }

    [JsonPropertyName("estCarbonEmissionsKg")]
    public double? EstCarbonEmissionsKg { get; init; }

    [JsonPropertyName("estFuelEnergyCost")]
    public required FuelEnergyCost EstFuelEnergyCost { get; init; }

    [JsonPropertyName("fuelConsumedMl")]
    public double? FuelConsumedMl { get; init; }
}

/// <summary>
/// A driver-keyed fuel/energy report row (item in <c>GET /fleet/reports/drivers/fuel-energy</c>).
/// Spec marks <c>driver</c>, <c>distanceTraveledMeters</c>, <c>efficiencyMpge</c>,
/// and <c>estFuelEnergyCost</c> as REQUIRED.
/// </summary>
public sealed record FuelEnergyDriverReport
{
    [JsonPropertyName("driver")]
    public required EntityReference Driver { get; init; }

    [JsonPropertyName("distanceTraveledMeters")]
    public required double DistanceTraveledMeters { get; init; }

    [JsonPropertyName("efficiencyMpge")]
    public required double EfficiencyMpge { get; init; }

    [JsonPropertyName("energyUsedKwh")]
    public double? EnergyUsedKwh { get; init; }

    [JsonPropertyName("engineIdleTimeDurationMs")]
    public long? EngineIdleTimeDurationMs { get; init; }

    [JsonPropertyName("engineRunTimeDurationMs")]
    public long? EngineRunTimeDurationMs { get; init; }

    [JsonPropertyName("estCarbonEmissionsKg")]
    public double? EstCarbonEmissionsKg { get; init; }

    [JsonPropertyName("estFuelEnergyCost")]
    public required FuelEnergyCost EstFuelEnergyCost { get; init; }

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

/// <summary>
/// Difficulty score breakdown returned with a driver-efficiency datapoint.
/// Spec leaves all fields optional ("Difficulty score won't be available if there is no data to compute it against.").
/// </summary>
public sealed record DriverEfficiencyDifficultyScore
{
    /// <summary>Overall difficulty score (1–5, as string).</summary>
    [JsonPropertyName("overallScore")] public string? OverallScore { get; init; }

    /// <summary>Topography difficulty score (1–5, as string).</summary>
    [JsonPropertyName("topographyScore")] public string? TopographyScore { get; init; }

    /// <summary>Average vehicle-weight score (1–5, as string).</summary>
    [JsonPropertyName("vehicleWeightScore")] public string? VehicleWeightScore { get; init; }
}

/// <summary>
/// Driver-efficiency percentage data (returned when <c>dataFormats=percentage</c>).
/// Spec marks only <c>idlingPercentage</c> as REQUIRED, so it is exposed nullable
/// since the whole nested object is omitted unless the format is requested.
/// </summary>
public sealed record DriverEfficiencyPercentageData
{
    /// <summary>Percentage of time the driver was in quick-braking events vs total brake events.</summary>
    [JsonPropertyName("anticipationPercentage")] public double? AnticipationPercentage { get; init; }

    /// <summary>Percentage of time the driver was coasting.</summary>
    [JsonPropertyName("coastingPercentage")] public double? CoastingPercentage { get; init; }

    /// <summary>Percentage of time the vehicle was in cruise control.</summary>
    [JsonPropertyName("cruiseControlPercentage")] public double? CruiseControlPercentage { get; init; }

    /// <summary>Percentage of time the driver was driving within the green band.</summary>
    [JsonPropertyName("greenBandPercentage")] public double? GreenBandPercentage { get; init; }

    /// <summary>Percentage of time the driver was on high-grade road.</summary>
    [JsonPropertyName("highGradeRoadDrivingPercentage")] public double? HighGradeRoadDrivingPercentage { get; init; }

    /// <summary>Percentage of time the driver was in high torque.</summary>
    [JsonPropertyName("highTorquePercentage")] public double? HighTorquePercentage { get; init; }

    /// <summary>Percentage of time the driver was idling. Spec marks REQUIRED on this nested schema.</summary>
    [JsonPropertyName("idlingPercentage")] public double? IdlingPercentage { get; init; }

    /// <summary>Percentage of time the driver was over the configured speed limit.</summary>
    [JsonPropertyName("overSpeedPercentage")] public double? OverSpeedPercentage { get; init; }

    /// <summary>Percentage of time the driver was wear-free braking.</summary>
    [JsonPropertyName("wearFreeBrakePercentage")] public double? WearFreeBrakePercentage { get; init; }
}

/// <summary>
/// Driver-efficiency raw counters/durations (returned when <c>dataFormats=raw</c>).
/// Spec marks <c>driveTimeDurationMs</c>, <c>engineOnDurationMs</c>, <c>idlingDurationMs</c>,
/// and <c>totalBrakeDurationMs</c> as REQUIRED on this nested schema, but the entire object
/// is omitted unless the format is requested — so all properties remain nullable.
/// </summary>
public sealed record DriverEfficiencyRawData
{
    /// <summary>Number of quick-braking events (less than one second after accelerating).</summary>
    [JsonPropertyName("anticipationBrakeEventCount")] public long? AnticipationBrakeEventCount { get; init; }

    /// <summary>Average vehicle weight in kilograms.</summary>
    [JsonPropertyName("averageVehicleWeightKg")] public long? AverageVehicleWeightKg { get; init; }

    /// <summary>Milliseconds without engaging the accelerator or brake.</summary>
    [JsonPropertyName("coastingDurationMs")] public long? CoastingDurationMs { get; init; }

    /// <summary>Milliseconds in cruise control.</summary>
    [JsonPropertyName("cruiseControlDurationMs")] public long? CruiseControlDurationMs { get; init; }

    /// <summary>Total milliseconds spent driving.</summary>
    [JsonPropertyName("driveTimeDurationMs")] public long? DriveTimeDurationMs { get; init; }

    /// <summary>Total milliseconds with the engine on.</summary>
    [JsonPropertyName("engineOnDurationMs")] public long? EngineOnDurationMs { get; init; }

    /// <summary>Milliseconds driving inside the configured green band.</summary>
    [JsonPropertyName("greenBandDurationMs")] public long? GreenBandDurationMs { get; init; }

    /// <summary>Milliseconds spent on high-grade road.</summary>
    [JsonPropertyName("highGradeRoadDrivingDurationMs")] public long? HighGradeRoadDrivingDurationMs { get; init; }

    /// <summary>Milliseconds with engine torque &gt; 90%.</summary>
    [JsonPropertyName("highTorqueDurationMs")] public long? HighTorqueDurationMs { get; init; }

    /// <summary>Total milliseconds idling.</summary>
    [JsonPropertyName("idlingDurationMs")] public long? IdlingDurationMs { get; init; }

    /// <summary>Milliseconds spent over the configured speed limit.</summary>
    [JsonPropertyName("overSpeedDurationMs")] public long? OverSpeedDurationMs { get; init; }

    /// <summary>Total milliseconds braking.</summary>
    [JsonPropertyName("totalBrakeDurationMs")] public long? TotalBrakeDurationMs { get; init; }

    /// <summary>Total number of brake events.</summary>
    [JsonPropertyName("totalBrakeEventCount")] public long? TotalBrakeEventCount { get; init; }

    /// <summary>Milliseconds spent wear-free braking.</summary>
    [JsonPropertyName("wearFreeBrakeDurationMs")] public long? WearFreeBrakeDurationMs { get; init; }
}

/// <summary>
/// Driver-efficiency score breakdown (returned by default or when <c>dataFormats=score</c>).
/// Scores are letter-graded (A–G) or numeric (0–100 as string) depending on org config.
/// Spec marks <c>overallScore</c> as REQUIRED on this nested schema, but the entire object
/// is omitted in some configurations — exposed nullable.
/// </summary>
public sealed record DriverEfficiencyScoreData
{
    [JsonPropertyName("anticipationScore")] public string? AnticipationScore { get; init; }
    [JsonPropertyName("coastingScore")] public string? CoastingScore { get; init; }
    [JsonPropertyName("cruiseControlScore")] public string? CruiseControlScore { get; init; }
    [JsonPropertyName("greenBandScore")] public string? GreenBandScore { get; init; }
    [JsonPropertyName("highTorqueScore")] public string? HighTorqueScore { get; init; }
    [JsonPropertyName("idlingScore")] public string? IdlingScore { get; init; }
    [JsonPropertyName("overSpeedScore")] public string? OverSpeedScore { get; init; }
    [JsonPropertyName("overallScore")] public string? OverallScore { get; init; }
    [JsonPropertyName("wearFreeBrakeScore")] public string? WearFreeBrakeScore { get; init; }
}

/// <summary>
/// Per-driver efficiency datapoint (item of <c>GET /driver-efficiency/drivers</c>).
/// Spec marks <c>driverId</c> as REQUIRED.
/// </summary>
public sealed record DriverEfficiencyByDriver
{
    [JsonPropertyName("driverId")] public required string DriverId { get; init; }
    [JsonPropertyName("difficultyScore")] public DriverEfficiencyDifficultyScore? DifficultyScore { get; init; }
    [JsonPropertyName("percentageData")] public DriverEfficiencyPercentageData? PercentageData { get; init; }
    [JsonPropertyName("rawData")] public DriverEfficiencyRawData? RawData { get; init; }
    [JsonPropertyName("scoreData")] public DriverEfficiencyScoreData? ScoreData { get; init; }
}

/// <summary>
/// Per-vehicle efficiency datapoint (item of <c>GET /driver-efficiency/vehicles</c>).
/// Spec marks <c>vehicleId</c> as REQUIRED.
/// </summary>
public sealed record DriverEfficiencyByVehicle
{
    [JsonPropertyName("vehicleId")] public required string VehicleId { get; init; }
    [JsonPropertyName("difficultyScore")] public DriverEfficiencyDifficultyScore? DifficultyScore { get; init; }
    [JsonPropertyName("percentageData")] public DriverEfficiencyPercentageData? PercentageData { get; init; }
    [JsonPropertyName("rawData")] public DriverEfficiencyRawData? RawData { get; init; }
    [JsonPropertyName("scoreData")] public DriverEfficiencyScoreData? ScoreData { get; init; }
}

// ── Fuel purchase ────────────────────────────────────────────────────────────

/// <summary>
/// Money amount for a fuel-purchase transaction. Used by
/// <see cref="CreateFuelPurchaseRequest.TransactionPrice"/> and
/// <see cref="CreateFuelPurchaseRequest.Discount"/>.
/// Spec marks both <c>amount</c> and <c>currency</c> as REQUIRED.
/// </summary>
public sealed record FuelPurchaseMoney
{
    /// <summary>The money amount as a string (e.g. <c>"640.2"</c>).</summary>
    [JsonPropertyName("amount")]
    public required string Amount { get; init; }

    /// <summary>
    /// The currency. Spec lists valid values: <c>usd</c>, <c>gbp</c>, <c>cad</c>,
    /// <c>eur</c>, <c>chf</c>, <c>mxn</c>.
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }
}

/// <summary>Request body for <c>POST /fuel-purchase</c>.</summary>
public sealed record CreateFuelPurchaseRequest
{
    [JsonPropertyName("fuelQuantityLiters")] public required string FuelQuantityLiters { get; init; }
    [JsonPropertyName("transactionLocation")] public required string TransactionLocation { get; init; }
    [JsonPropertyName("transactionPrice")] public required FuelPurchaseMoney TransactionPrice { get; init; }
    [JsonPropertyName("transactionReference")] public required string TransactionReference { get; init; }
    [JsonPropertyName("transactionTime")] public required string TransactionTime { get; init; }
    [JsonPropertyName("discount")] public FuelPurchaseMoney? Discount { get; init; }
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }
    [JsonPropertyName("fuelGrade")] public string? FuelGrade { get; init; }
    [JsonPropertyName("iftaFuelType")] public string? IftaFuelType { get; init; }
    [JsonPropertyName("merchantName")] public string? MerchantName { get; init; }
    [JsonPropertyName("source")] public string? Source { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
}

/// <summary>
/// Response of <c>POST /fuel-purchase</c>. The spec response schema returns only
/// <c>uuid</c>; the request fields live on <see cref="CreateFuelPurchaseRequest"/>.
/// </summary>
public sealed record FuelPurchase
{
    /// <summary>Universally unique identifier for the fuel purchase. Spec marks REQUIRED.</summary>
    [JsonPropertyName("uuid")] public required string Uuid { get; init; }
}
