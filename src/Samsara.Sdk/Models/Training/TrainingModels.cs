namespace Samsara.Sdk.Models.Training;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a training assignment for a driver.
/// </summary>
public sealed record TrainingAssignment
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("course")]
    public required object Course { get; init; }

    [JsonPropertyName("learner")]
    public required object Learner { get; init; }

    [JsonPropertyName("createdById")]
    public required string CreatedById { get; init; }

    [JsonPropertyName("createdAtTime")]
    public required DateTimeOffset CreatedAtTime { get; init; }

    [JsonPropertyName("updatedById")]
    public required string UpdatedById { get; init; }

    [JsonPropertyName("updatedAtTime")]
    public required DateTimeOffset UpdatedAtTime { get; init; }

    [JsonPropertyName("durationMinutes")]
    public required long DurationMinutes { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("startedAtTime")]
    public DateTimeOffset? StartedAtTime { get; init; }

    [JsonPropertyName("deletedAtTime")]
    public DateTimeOffset? DeletedAtTime { get; init; }

    [JsonPropertyName("isOverdue")]
    public bool? IsOverdue { get; init; }

    [JsonPropertyName("isCompletedLate")]
    public bool? IsCompletedLate { get; init; }

    [JsonPropertyName("scorePercent")]
    public double? ScorePercent { get; init; }

    [JsonPropertyName("completedAtTime")]
    public DateTimeOffset? CompletedAtTime { get; init; }

    [JsonPropertyName("dueAtTime")]
    public DateTimeOffset? DueAtTime { get; init; }

    // Not in current spec; retained for back-compat.
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

    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("revisionId")]
    public required string RevisionId { get; init; }

    [JsonPropertyName("category")]
    public required object Category { get; init; }

    [JsonPropertyName("estimatedTimeToCompleteMinutes")]
    public required long EstimatedTimeToCompleteMinutes { get; init; }

    [JsonPropertyName("labels")]
    public IReadOnlyList<object>? Labels { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("isActive")]
    public bool? IsActive { get; init; }

    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}
