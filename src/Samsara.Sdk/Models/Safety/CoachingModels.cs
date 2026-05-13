namespace Samsara.Sdk.Models.Safety;

using System.Text.Json.Serialization;

/// <summary>Represents a driver-to-coach assignment.</summary>
public sealed record DriverCoachAssignment
{
    [JsonPropertyName("driverId")] public required string DriverId { get; init; }
    [JsonPropertyName("coachId")] public string? CoachId { get; init; }
    [JsonPropertyName("driverName")] public string? DriverName { get; init; }
    [JsonPropertyName("coachName")] public string? CoachName { get; init; }
}

/// <summary>Request body for setting a driver-coach assignment.</summary>
public sealed record SetDriverCoachAssignmentRequest
{
    [JsonPropertyName("driverId")] public required string DriverId { get; init; }
    [JsonPropertyName("coachId")] public required string CoachId { get; init; }
}

/// <summary>Represents a coaching session.</summary>
public sealed record CoachingSession
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }
    [JsonPropertyName("coachId")] public string? CoachId { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("completedAtTime")] public DateTimeOffset? CompletedAtTime { get; init; }
    [JsonPropertyName("scheduledAtTime")] public DateTimeOffset? ScheduledAtTime { get; init; }
    [JsonPropertyName("sessionType")] public string? SessionType { get; init; }
}
