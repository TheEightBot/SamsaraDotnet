namespace Samsara.Sdk.Models.Issues;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an issue in Samsara. Mirrors the spec's
/// <c>IssueResponseObjectResponseBody</c>.
/// </summary>
public sealed record Issue
{
    /// <summary>ID of the issue. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Title of the issue. Spec-required.</summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>Description of the issue. Included if the issue was given a description.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Status of the issue. Spec-required. Valid values: <c>open</c>, <c>inProgress</c>,
    /// <c>resolved</c>, <c>dismissed</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; init; }

    /// <summary>
    /// Priority of the issue. Included if the issue was assigned a priority.
    /// Valid values: <c>low</c>, <c>medium</c>, <c>high</c>.
    /// </summary>
    [JsonPropertyName("priority")]
    public string? Priority { get; init; }

    /// <summary>
    /// Creation time of the issue (RFC 3339 UTC). Spec-required.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public required DateTimeOffset CreatedAtTime { get; init; }

    /// <summary>
    /// Update time of the issue (RFC 3339 UTC). Spec-required.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public required DateTimeOffset UpdatedAtTime { get; init; }

    /// <summary>
    /// Submission time of the issue (RFC 3339 UTC). Spec-required.
    /// </summary>
    [JsonPropertyName("submittedAtTime")]
    public required DateTimeOffset SubmittedAtTime { get; init; }

    /// <summary>
    /// Source of the issue (form, ad-hoc, etc.). Spec-required.
    /// </summary>
    [JsonPropertyName("issueSource")]
    public required IssueSource IssueSource { get; init; }

    /// <summary>
    /// Polymorphic user (driver or admin) who submitted the issue. Spec-required.
    /// </summary>
    [JsonPropertyName("submittedBy")]
    public required IssueUser SubmittedBy { get; init; }

    /// <summary>
    /// Due date of the issue (RFC 3339 UTC). Included if the issue was assigned a due date.
    /// </summary>
    [JsonPropertyName("dueDate")]
    public DateTimeOffset? DueDate { get; init; }

    /// <summary>
    /// Asset (vehicle/equipment) the issue is associated with. Included when the issue
    /// references an asset.
    /// </summary>
    [JsonPropertyName("asset")]
    public IssueAsset? Asset { get; init; }

    /// <summary>
    /// Polymorphic user (driver or admin) the issue is assigned to, if any.
    /// </summary>
    [JsonPropertyName("assignedTo")]
    public IssueUser? AssignedTo { get; init; }

    /// <summary>
    /// Media records attached to the issue. Included if the issue has media.
    /// </summary>
    [JsonPropertyName("mediaList")]
    public IReadOnlyList<IssueMedia>? MediaList { get; init; }

    /// <summary>Map of external IDs for the issue.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Tracked or untracked (i.e. manually entered) asset associated with an issue.
/// Mirrors the spec's <c>FormsAssetObjectResponseBody</c>.
/// </summary>
public sealed record IssueAsset
{
    /// <summary>
    /// The type of entry for the asset. Spec-required.
    /// Valid values: <c>tracked</c>, <c>untracked</c>.
    /// </summary>
    [JsonPropertyName("entryType")]
    public required string EntryType { get; init; }

    /// <summary>
    /// ID of a tracked asset. Included if <c>entryType</c> is <c>tracked</c>.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of an untracked (i.e. manually entered) asset.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Map of external IDs for the asset.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Polymorphic user (driver or admin) referenced on an issue (either the submitter or
/// the assignee). Mirrors the spec's <c>FormsPolymorphicUserObjectResponseBody</c>.
/// </summary>
public sealed record IssueUser
{
    /// <summary>ID of the polymorphic user. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The type of the polymorphic user. Spec-required.
    /// Valid values: <c>driver</c>, <c>user</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

/// <summary>
/// Source information for an issue. Mirrors the spec's
/// <c>IssueSourceObjectResponseBody</c>.
/// </summary>
public sealed record IssueSource
{
    /// <summary>
    /// The type of issue source. Spec-required. Valid values: <c>form</c>, <c>ad-hoc</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>
    /// ID of the issue's source object. The format depends on <c>type</c>.
    /// Included if <c>type</c> is not <c>ad-hoc</c>.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// Media record attached to an issue. Mirrors the spec's
/// <c>FormsMediaRecordObjectResponseBody</c>.
/// </summary>
public sealed record IssueMedia
{
    /// <summary>ID of the media record (UUID). Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Status of the media record. Spec-required.
    /// Valid values: <c>unknown</c>, <c>processing</c>, <c>finished</c>.
    /// </summary>
    [JsonPropertyName("processingStatus")]
    public required string ProcessingStatus { get; init; }

    /// <summary>
    /// URL containing a link to the associated media content. Included if
    /// <c>processingStatus</c> is <c>finished</c>.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Expiration time of the media record's <c>url</c> (RFC 3339 UTC).
    /// </summary>
    [JsonPropertyName("urlExpiresAt")]
    public DateTimeOffset? UrlExpiresAt { get; init; }
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

    /// <summary>Issue assignee (typed). Mirrors the spec's
    /// <c>PatchIssueRequestBodyAssignedToRequestBody</c>.</summary>
    [JsonPropertyName("assignedTo")]
    public IssueAssigneeRequest? AssignedTo { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("media")]
    public IReadOnlyList<IssueMediaItemRequest>? Media { get; init; }
}

/// <summary>
/// Request body for <c>POST /issues</c>.
/// </summary>
public sealed record CreateIssueRequest
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>The asset (vehicle/equipment) the issue is associated with.
    /// Mirrors the spec's <c>PostIssueRequestBodyAssetRequestBody</c>.</summary>
    [JsonPropertyName("asset")]
    public required IssueAssetRequest Asset { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("priority")]
    public string? Priority { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("dueDate")]
    public string? DueDate { get; init; }

    /// <summary>Issue assignee (typed). Mirrors the spec's
    /// <c>PostIssueRequestBodyAssignedToRequestBody</c>.</summary>
    [JsonPropertyName("assignedTo")]
    public IssueAssigneeRequest? AssignedTo { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("media")]
    public IReadOnlyList<IssueMediaItemRequest>? Media { get; init; }
}

/// <summary>
/// Asset reference on a <see cref="CreateIssueRequest"/>. Mirrors the spec's
/// <c>PostIssueRequestBodyAssetRequestBody</c>.
/// </summary>
public sealed record IssueAssetRequest
{
    /// <summary>
    /// ID of the asset. Can be either a unique Samsara ID or an external ID.
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// Assignee reference shared by <see cref="CreateIssueRequest"/> and
/// <see cref="UpdateIssueRequest"/>. The spec defines structurally identical
/// <c>PostIssueRequestBodyAssignedToRequestBody</c> and
/// <c>PatchIssueRequestBodyAssignedToRequestBody</c> schemas.
/// </summary>
public sealed record IssueAssigneeRequest
{
    /// <summary>ID of the issue assignee. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Type of the issue assignee. Spec-required. Valid values: <c>user</c>.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

/// <summary>
/// Media item attached to an issue create/update request. Mirrors the spec's
/// <c>FormSubmissionRequestMediaItemObjectRequestBody</c>.
/// </summary>
public sealed record IssueMediaItemRequest
{
    /// <summary>Base64-encoded binary content of the media. Spec-required.</summary>
    [JsonPropertyName("base64Payload")]
    public required string Base64Payload { get; init; }

    /// <summary>MIME type of the media. Spec-required.</summary>
    [JsonPropertyName("mediaType")]
    public required string MediaType { get; init; }
}
