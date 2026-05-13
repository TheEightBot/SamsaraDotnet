namespace Samsara.Sdk.Models.Safety;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a safety event (harsh braking, speeding, collision, etc).
/// </summary>
public sealed record SafetyEvent
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("behaviorLabels")]
    public IReadOnlyList<string>? BehaviorLabels { get; init; }

    [JsonPropertyName("vehicle")]
    public SafetyEventVehicle? Vehicle { get; init; }

    [JsonPropertyName("driver")]
    public SafetyEventDriver? Driver { get; init; }
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

/// <summary>
/// Safety score aggregated by tag.
/// </summary>
public sealed record TagSafetyScore
{
    [JsonPropertyName("tagId")]
    public required string TagId { get; init; }

    [JsonPropertyName("tagName")]
    public string? TagName { get; init; }

    [JsonPropertyName("safetyScore")]
    public double? SafetyScore { get; init; }

    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }
}

/// <summary>
/// Safety score aggregated by tag group.
/// </summary>
public sealed record TagGroupSafetyScore
{
    [JsonPropertyName("tagGroupId")]
    public required string TagGroupId { get; init; }

    [JsonPropertyName("tagGroupName")]
    public string? TagGroupName { get; init; }

    [JsonPropertyName("safetyScore")]
    public double? SafetyScore { get; init; }

    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }
}
