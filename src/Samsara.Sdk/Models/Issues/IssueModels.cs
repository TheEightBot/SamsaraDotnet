namespace Samsara.Sdk.Models.Issues;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an issue in Samsara.
/// </summary>
public sealed record Issue
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("priority")]
    public string? Priority { get; init; }

    [JsonPropertyName("assigneeId")]
    public string? AssigneeId { get; init; }

    [JsonPropertyName("assigneeName")]
    public string? AssigneeName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }

    [JsonPropertyName("resolvedAtTime")]
    public DateTimeOffset? ResolvedAtTime { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /issues</c>. The issue id is sent here, not in the URL.
/// </summary>
public sealed record UpdateIssueRequest
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("priority")]
    public string? Priority { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("dueDate")]
    public string? DueDate { get; init; }

    /// <summary>Assignee — passed as a free-form object (e.g. <c>new { userId = "..." }</c>).</summary>
    [JsonPropertyName("assignedTo")]
    public object? AssignedTo { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("media")]
    public IReadOnlyList<object>? Media { get; init; }
}

/// <summary>
/// Request body for <c>POST /issues</c>.
/// </summary>
public sealed record CreateIssueRequest
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>The asset (vehicle/equipment) the issue is associated with — pass an
    /// object such as <c>new { id = "..." }</c>.</summary>
    [JsonPropertyName("asset")]
    public required object Asset { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("priority")]
    public string? Priority { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("dueDate")]
    public string? DueDate { get; init; }

    [JsonPropertyName("assignedTo")]
    public object? AssignedTo { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("media")]
    public IReadOnlyList<object>? Media { get; init; }
}
