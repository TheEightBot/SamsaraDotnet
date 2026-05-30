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

    /// <summary>Category of the training course. Spec-required
    /// (<c>TrainingCategoryObjectResponseBody</c>).</summary>
    [JsonPropertyName("category")]
    public required TrainingCourseCategory Category { get; init; }

    [JsonPropertyName("estimatedTimeToCompleteMinutes")]
    public required long EstimatedTimeToCompleteMinutes { get; init; }

    /// <summary>Labels of the training course
    /// (<c>TrainingCourseLabelObjectResponseBody</c>).</summary>
    [JsonPropertyName("labels")]
    public IReadOnlyList<TrainingCourseLabel>? Labels { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>Category of a training course. Mirrors the spec's
/// <c>TrainingCategoryObjectResponseBody</c>.</summary>
public sealed record TrainingCourseCategory
{
    /// <summary>Category ID of the course. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Category name of the course. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

/// <summary>Label of a training course. Mirrors the spec's
/// <c>TrainingCourseLabelObjectResponseBody</c>.</summary>
public sealed record TrainingCourseLabel
{
    /// <summary>Name of the course label (e.g. <c>safety</c>). Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Type of the course label (e.g. <c>accel</c>, <c>braking</c>, <c>speeding</c>;
    /// see the spec for the full enumeration). Spec-required.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}
