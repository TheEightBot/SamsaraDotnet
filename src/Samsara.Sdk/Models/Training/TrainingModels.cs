namespace Samsara.Sdk.Models.Training;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a training assignment for a driver.
/// </summary>
public sealed record TrainingAssignment
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("courseId")]
    public string? CourseId { get; init; }

    [JsonPropertyName("courseName")]
    public string? CourseName { get; init; }

    [JsonPropertyName("assignedAtTime")]
    public DateTimeOffset? AssignedAtTime { get; init; }

    [JsonPropertyName("completedAtTime")]
    public DateTimeOffset? CompletedAtTime { get; init; }

    [JsonPropertyName("dueAtTime")]
    public DateTimeOffset? DueAtTime { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("score")]
    public double? Score { get; init; }
}

/// <summary>
/// Represents a training course.
/// </summary>
public sealed record TrainingCourse
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("isActive")]
    public bool? IsActive { get; init; }

    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}
