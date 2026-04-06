namespace Samsara.Sdk.Models.Safety;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a safety event (harsh braking, speeding, collision, etc).
/// </summary>
public sealed record SafetyEvent
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("behaviorLabel")]
    public string? BehaviorLabel { get; init; }

    [JsonPropertyName("maxGForce")]
    public double? MaxGForce { get; init; }

    [JsonPropertyName("vehicle")]
    public SafetyEventVehicle? Vehicle { get; init; }

    [JsonPropertyName("driver")]
    public SafetyEventDriver? Driver { get; init; }

    [JsonPropertyName("location")]
    public SafetyEventLocation? Location { get; init; }

    [JsonPropertyName("coachingState")]
    public string? CoachingState { get; init; }

    [JsonPropertyName("incidentReportUrl")]
    public string? IncidentReportUrl { get; init; }

    [JsonPropertyName("downloadForwardVideoUrl")]
    public string? DownloadForwardVideoUrl { get; init; }

    [JsonPropertyName("downloadInwardVideoUrl")]
    public string? DownloadInwardVideoUrl { get; init; }
}

/// <summary>
/// Vehicle reference within a safety event.
/// </summary>
public sealed record SafetyEventVehicle
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Driver reference within a safety event.
/// </summary>
public sealed record SafetyEventDriver
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Location of a safety event.
/// </summary>
public sealed record SafetyEventLocation
{
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// Vehicle safety score.
/// </summary>
public sealed record VehicleSafetyScore
{
    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    [JsonPropertyName("safetyScore")]
    public double? SafetyScore { get; init; }

    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    [JsonPropertyName("totalTimeDrivenMs")]
    public long? TotalTimeDrivenMs { get; init; }

    [JsonPropertyName("totalDistanceDrivenMeters")]
    public double? TotalDistanceDrivenMeters { get; init; }

    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }

    [JsonPropertyName("crashCount")]
    public int? CrashCount { get; init; }

    [JsonPropertyName("harshAccelCount")]
    public int? HarshAccelCount { get; init; }

    [JsonPropertyName("harshBrakingCount")]
    public int? HarshBrakingCount { get; init; }

    [JsonPropertyName("harshTurningCount")]
    public int? HarshTurningCount { get; init; }
}

/// <summary>
/// Driver safety score.
/// </summary>
public sealed record DriverSafetyScore
{
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    [JsonPropertyName("safetyScore")]
    public double? SafetyScore { get; init; }

    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    [JsonPropertyName("totalTimeDrivenMs")]
    public long? TotalTimeDrivenMs { get; init; }

    [JsonPropertyName("totalDistanceDrivenMeters")]
    public double? TotalDistanceDrivenMeters { get; init; }

    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }
}

/// <summary>
/// A time range used for safety score calculations.
/// </summary>
public sealed record TimeRange
{
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
}
