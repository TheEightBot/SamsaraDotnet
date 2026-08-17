namespace Samsara.Sdk.Models.Documents;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Response object for a Samsara form template (mirrors spec
/// <c>FormTemplateResponseObjectResponseBody</c>).
/// </summary>
public sealed record FormTemplate
{
    /// <summary>Unique identifier of the form template. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Title of the form template. Spec-required.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>Description of the form template, when present.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Unique identifier of the form template revision (uuid). Spec-required.</summary>
    [JsonPropertyName("revisionId")]
    public string? RevisionId { get; init; }

    /// <summary>Category of the form template (e.g. <c>general</c>, <c>safety</c>).</summary>
    [JsonPropertyName("formCategory")]
    public string? FormCategory { get; init; }

    /// <summary>Approval configuration for the template. Mirrors the spec's
    /// <c>FormsApprovalConfigObjectResponseBody</c>.</summary>
    [JsonPropertyName("approvalConfig")]
    public FormsApprovalConfig? ApprovalConfig { get; init; }

    /// <summary>List of fields in the template (spec-required). Each entry is a heterogeneous
    /// field-definition object; left untyped to preserve the full per-field-type payload.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<JsonElement>? Fields { get; init; }

    /// <summary>List of sections in the template (spec-required).</summary>
    [JsonPropertyName("sections")]
    public IReadOnlyList<FormSection> Sections { get; init; } = Array.Empty<FormSection>();

    /// <summary>Creator of the template (spec-required). Mirrors the spec's
    /// <c>FormsPolymorphicUserObjectResponseBody</c>.</summary>
    [JsonPropertyName("createdBy")]
    public FormsPolymorphicUser? CreatedBy { get; init; }

    /// <summary>Last updater of the template (spec-required). Mirrors the spec's
    /// <c>FormsPolymorphicUserObjectResponseBody</c>.</summary>
    [JsonPropertyName("updatedBy")]
    public FormsPolymorphicUser? UpdatedBy { get; init; }

    /// <summary>Creation time of the template, RFC 3339 (spec-required).</summary>
    [JsonPropertyName("createdAtTime")]
    public string CreatedAtTime { get; init; } = string.Empty;

    /// <summary>Last update time of the template, RFC 3339 (spec-required).</summary>
    [JsonPropertyName("updatedAtTime")]
    public string UpdatedAtTime { get; init; } = string.Empty;
}

/// <summary>
/// Approval configuration for a form template. Mirrors the spec's
/// <c>FormsApprovalConfigObjectResponseBody</c>.
/// </summary>
public sealed record FormsApprovalConfig
{
    /// <summary>Type of approval configuration (e.g. <c>none</c>, <c>single</c>). Spec-required.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Single-approval configuration, present when <see cref="Type"/> is single. Mirrors the
    /// spec's <c>FormsSingleApprovalConfigObjectResponseBody</c>.</summary>
    [JsonPropertyName("singleApprovalConfig")]
    public FormsSingleApprovalConfig? SingleApprovalConfig { get; init; }
}

/// <summary>
/// Single-approval configuration for a form template. Mirrors the spec's
/// <c>FormsSingleApprovalConfigObjectResponseBody</c>.
/// </summary>
public sealed record FormsSingleApprovalConfig
{
    /// <summary>Whether the approver can be manually selected (true by default). Spec-required.</summary>
    [JsonPropertyName("allowManualApproverSelection")]
    public bool? AllowManualApproverSelection { get; init; }

    /// <summary>Approval requirements (spec-required). Left untyped to preserve the nested
    /// requirements tree (<c>SingleApprovalRequirementsObjectResponseBody</c>).</summary>
    [JsonPropertyName("requirements")]
    public JsonElement? Requirements { get; init; }
}

/// <summary>
/// A polymorphic user reference on a form template/submission (driver or user).
/// Mirrors the spec's <c>FormsPolymorphicUserObjectResponseBody</c>.
/// </summary>
public sealed record FormsPolymorphicUser
{
    /// <summary>Samsara ID of the user. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Type of user (e.g. <c>driver</c>, <c>user</c>). Spec-required.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// A section of a form template. Mirrors the spec's
/// <c>FormsTemplateSectionObjectResponseBody</c>
/// (<c>GET /form-templates</c> → <c>data.sections</c>).
/// </summary>
/// <remarks>
/// The 2026-08-17 spec-parity sweep found this record modelled a shape the API
/// never sends. A section does NOT carry its own <c>fields</c> array or a
/// <c>title</c>; it carries a <c>label</c> plus an inclusive index range into
/// the template's flat <c>fields</c> array. Spec marks all four members
/// REQUIRED; they stay nullable because this is a response record.
/// </remarks>
public sealed record FormSection
{
    /// <summary>Identifier of the section (UUID). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Label of the section. Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// Index of the first field of <c>FormTemplate.Fields</c> that belongs to
    /// this section; index 0 is the first field. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("fieldIndexFirstInclusive")]
    public long? FieldIndexFirstInclusive { get; init; }

    /// <summary>
    /// Index of the last field of <c>FormTemplate.Fields</c> that belongs to
    /// this section (inclusive). Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("fieldIndexLastInclusive")]
    public long? FieldIndexLastInclusive { get; init; }
}

// FormFieldDefinition was removed in the 2026-08-17 spec-parity sweep. It was
// only ever reachable through FormSection.Fields, which the spec does not
// define, and its own {id,label,type,required} shape matches no spec schema:
// form-template fields live in the flat FormTemplate.Fields array, which
// sections address by index.

/// <summary>
/// Response object for a form submission (mirrors spec
/// <c>FormSubmissionResponseObjectResponseBody</c>).
/// </summary>
public sealed record FormSubmission
{
    /// <summary>ID of the form submission. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Title of the form submission, if set.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Status of the submission (spec-required). Valid values:
    /// <c>notStarted</c>, <c>completed</c>, <c>archived</c>, <c>inProgress</c>,
    /// <c>needsReview</c>, <c>changesRequested</c>, <c>approved</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>Whether the submission is required (spec-required).</summary>
    [JsonPropertyName("isRequired")]
    public bool IsRequired { get; init; }

    /// <summary>Reference to the form template (spec-required). Mirrors the spec's
    /// <c>FormTemplateReferenceObjectResponseBody</c>.</summary>
    [JsonPropertyName("formTemplate")]
    public FormTemplateReference? FormTemplate { get; init; }

    /// <summary>User/driver who submitted the form (spec-required). Mirrors the spec's
    /// <c>FormsPolymorphicUserObjectResponseBody</c>.</summary>
    [JsonPropertyName("submittedBy")]
    public FormsPolymorphicUser? SubmittedBy { get; init; }

    /// <summary>User/driver assigned to the form, when present. Mirrors the spec's
    /// <c>FormsPolymorphicUserObjectResponseBody</c>.</summary>
    [JsonPropertyName("assignedTo")]
    public FormsPolymorphicUser? AssignedTo { get; init; }

    /// <summary>Approval details, when the submission has been reviewed. Mirrors the spec's
    /// <c>FormsProductSubmissionApprovalDetailsObjectResponseBody</c>.</summary>
    [JsonPropertyName("approvalDetails")]
    public FormSubmissionApprovalDetails? ApprovalDetails { get; init; }

    /// <summary>Asset associated with the submission (tracked or untracked). Mirrors the spec's
    /// <c>FormsAssetObjectResponseBody</c>.</summary>
    [JsonPropertyName("asset")]
    public FormsAsset? Asset { get; init; }

    /// <summary>Geofence associated with the submission (tracked or untracked). Mirrors the spec's
    /// <c>FormsGeofenceObjectResponseBody</c>.</summary>
    [JsonPropertyName("geofence")]
    public FormsGeofence? Geofence { get; init; }

    /// <summary>Location at submission time (latitude/longitude). Mirrors the spec's
    /// <c>FormsLocationObjectResponseBody</c>.</summary>
    [JsonPropertyName("location")]
    public FormsLocation? Location { get; init; }

    /// <summary>Score of the submission, when scoring is configured. Mirrors the spec's
    /// <c>FormsScoreObjectResponseBody</c>.</summary>
    [JsonPropertyName("score")]
    public FormsScore? Score { get; init; }

    /// <summary>Map of external ids associated with the submission.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>List of field inputs in the submission (spec-required). Each entry is a heterogeneous
    /// field-input object; left untyped to preserve the full per-field-type payload.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<JsonElement>? Fields { get; init; }

    /// <summary>Route id, when the submission was assigned to a route stop.</summary>
    [JsonPropertyName("routeId")]
    public string? RouteId { get; init; }

    /// <summary>Route stop id, when the submission was assigned to a route stop.</summary>
    [JsonPropertyName("routeStopId")]
    public string? RouteStopId { get; init; }

    /// <summary>Time at which the submission was assigned, if applicable.</summary>
    [JsonPropertyName("assignedAtTime")]
    public DateTimeOffset? AssignedAtTime { get; init; }

    /// <summary>Due time of the submission, if applicable.</summary>
    [JsonPropertyName("dueAtTime")]
    public DateTimeOffset? DueAtTime { get; init; }

    /// <summary>Submission time, RFC 3339 string (spec-required).</summary>
    [JsonPropertyName("submittedAtTime")]
    public string SubmittedAtTime { get; init; } = string.Empty;

    /// <summary>Creation time, RFC 3339 string (spec-required).</summary>
    [JsonPropertyName("createdAtTime")]
    public string CreatedAtTime { get; init; } = string.Empty;

    /// <summary>Last update time, RFC 3339 string (spec-required).</summary>
    [JsonPropertyName("updatedAtTime")]
    public string UpdatedAtTime { get; init; } = string.Empty;
}

/// <summary>
/// Reference to the form template a submission was created from. Mirrors the spec's
/// <c>FormTemplateReferenceObjectResponseBody</c>.
/// </summary>
public sealed record FormTemplateReference
{
    /// <summary>ID of the form template. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Revision ID of the form template (uuid). Spec-required.</summary>
    [JsonPropertyName("revisionId")]
    public string? RevisionId { get; init; }
}

/// <summary>
/// Approval details for a reviewed form submission. Mirrors the spec's
/// <c>FormsProductSubmissionApprovalDetailsObjectResponseBody</c>.
/// </summary>
public sealed record FormSubmissionApprovalDetails
{
    /// <summary>Reviewer comment on the submission. Spec-required.</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }
}

/// <summary>
/// Asset associated with a form submission (tracked or untracked). Mirrors the spec's
/// <c>FormsAssetObjectResponseBody</c>.
/// </summary>
public sealed record FormsAsset
{
    /// <summary>Whether the asset is <c>tracked</c> or <c>untracked</c>. Spec-required.</summary>
    [JsonPropertyName("entryType")]
    public string? EntryType { get; init; }

    /// <summary>Samsara ID of the asset (present for tracked assets).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Display name of the asset.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external IDs for the asset.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Geofence associated with a form submission (tracked or untracked). Mirrors the spec's
/// <c>FormsGeofenceObjectResponseBody</c>.
/// </summary>
public sealed record FormsGeofence
{
    /// <summary>Whether the geofence is <c>tracked</c> or <c>untracked</c>. Spec-required.</summary>
    [JsonPropertyName("entryType")]
    public string? EntryType { get; init; }

    /// <summary>Samsara ID of the address/geofence (present for tracked geofences).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Display name of the geofence.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Formatted address of the geofence.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>A map of external IDs for the geofence.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Location captured at form submission time. Mirrors the spec's
/// <c>FormsLocationObjectResponseBody</c>.
/// </summary>
public sealed record FormsLocation
{
    /// <summary>Latitude in degrees. Spec-required.</summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }

    /// <summary>Longitude in degrees. Spec-required.</summary>
    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }
}

/// <summary>
/// Score of a form submission, when scoring is configured. Mirrors the spec's
/// <c>FormsScoreObjectResponseBody</c>.
/// </summary>
public sealed record FormsScore
{
    /// <summary>Maximum possible points. Spec-required.</summary>
    [JsonPropertyName("maxPoints")]
    public double MaxPoints { get; init; }

    /// <summary>Score as a percentage (0–100). Spec-required.</summary>
    [JsonPropertyName("scorePercent")]
    public double ScorePercent { get; init; }

    /// <summary>Points scored. Spec-required.</summary>
    [JsonPropertyName("scorePoints")]
    public double ScorePoints { get; init; }
}

/// <summary>
/// Response object for a form PDF export job (mirrors spec
/// <c>FormSubmissionPdfExportResponseObjectResponseBody</c>).
/// </summary>
public sealed record FormPdfExport
{
    /// <summary>ID of the form submission being exported (spec-required).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Unique id for the PDF export that was created (spec-required).</summary>
    [JsonPropertyName("pdfId")]
    public string PdfId { get; init; } = string.Empty;

    /// <summary>
    /// Status of the PDF export job (spec-required). Valid values:
    /// <c>unknown</c>, <c>pending</c>, <c>done</c>, <c>failed</c>.
    /// </summary>
    [JsonPropertyName("jobStatus")]
    public string JobStatus { get; init; } = string.Empty;

    /// <summary>Time at which the PDF export POST request was made (spec-required).</summary>
    [JsonPropertyName("requestedAtTime")]
    public DateTimeOffset RequestedAtTime { get; init; }

    /// <summary>Time at which the job expires (spec-required).</summary>
    [JsonPropertyName("expiresAtTime")]
    public DateTimeOffset ExpiresAtTime { get; init; }

    /// <summary>Time at which the job was completed (only when <c>jobStatus</c> is <c>done</c>).</summary>
    [JsonPropertyName("completedAtTime")]
    public DateTimeOffset? CompletedAtTime { get; init; }

    /// <summary>URL to download the PDF (only when <c>jobStatus</c> is <c>done</c>).</summary>
    [JsonPropertyName("pdfUrl")]
    public string? PdfUrl { get; init; }

    /// <summary>Time at which the <c>pdfUrl</c> expires (when present).</summary>
    [JsonPropertyName("pdfUrlExpiresAtTime")]
    public DateTimeOffset? PdfUrlExpiresAtTime { get; init; }

    /// <summary>Error message for failed PDF export jobs.</summary>
    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; init; }
}

/// <summary>Request body for <c>POST /form-submissions</c>.</summary>
public sealed record CreateFormSubmissionRequest
{
    /// <summary>
    /// Reference to the form template being submitted (spec-required). Mirrors the spec's
    /// <c>FormTemplateRequestObjectRequestBody</c>.
    /// </summary>
    [JsonPropertyName("formTemplate")]
    public required FormTemplateRequest FormTemplate { get; init; }

    /// <summary>
    /// Initial status of the form submission (spec-required). Only valid
    /// value on create is <c>notStarted</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; init; }

    /// <summary>Title of the form submission.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>Driver or user the submission is assigned to. Mirrors the spec's
    /// <c>FormSubmissionRequestAssignedToRequestBody</c>.</summary>
    [JsonPropertyName("assignedTo")]
    public FormSubmissionAssignedTo? AssignedTo { get; init; }

    /// <summary>Due time for the submission, RFC 3339.</summary>
    [JsonPropertyName("dueAtTime")]
    public DateTimeOffset? DueAtTime { get; init; }

    /// <summary>Field inputs to populate at creation time. Each entry is a heterogeneous
    /// field-input object; left untyped to preserve the full per-field-type payload.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<JsonElement>? Fields { get; init; }

    /// <summary>Whether the worker is required to complete this form at a route stop.</summary>
    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; init; }

    /// <summary>Route stop id the submission is assigned to.</summary>
    [JsonPropertyName("routeStopId")]
    public string? RouteStopId { get; init; }
}

/// <summary>
/// Reference to the form template a submission is created from. Mirrors the spec's
/// <c>FormTemplateRequestObjectRequestBody</c>.
/// </summary>
public sealed record FormTemplateRequest
{
    /// <summary>ID of the form template to submit. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Revision ID of the form template (defaults to the latest revision if omitted).</summary>
    [JsonPropertyName("revisionId")]
    public string? RevisionId { get; init; }
}

/// <summary>
/// Driver or user a form submission is assigned to, supplied on create/update. Mirrors the spec's
/// <c>FormSubmissionRequestAssignedToRequestBody</c>.
/// </summary>
public sealed record FormSubmissionAssignedTo
{
    /// <summary>Samsara ID of the assignee. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Type of assignee (e.g. <c>driver</c>, <c>user</c>). Spec-required.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

/// <summary>Request body for <c>PATCH /form-submissions</c>. The submission id is in the body.</summary>
public sealed record UpdateFormSubmissionRequest
{
    /// <summary>ID of the form submission to update (spec-required).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>New title for the submission.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// New status. Valid values: <c>notStarted</c>, <c>archived</c>,
    /// <c>inProgress</c>, <c>changesRequested</c>, <c>approved</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Whether the submission is required at the assigned route stop.</summary>
    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; init; }

    /// <summary>Approval details for review workflow. Mirrors the spec's
    /// <c>FormSubmissionRequestApprovalDetailsRequestBody</c>.</summary>
    [JsonPropertyName("approvalDetails")]
    public FormSubmissionApprovalDetailsRequest? ApprovalDetails { get; init; }

    /// <summary>Driver or user the submission is assigned to. Mirrors the spec's
    /// <c>FormSubmissionRequestAssignedToRequestBody</c>.</summary>
    [JsonPropertyName("assignedTo")]
    public FormSubmissionAssignedTo? AssignedTo { get; init; }

    /// <summary>Due time for the submission, RFC 3339.</summary>
    [JsonPropertyName("dueAtTime")]
    public DateTimeOffset? DueAtTime { get; init; }

    /// <summary>Route stop id the submission is assigned to.</summary>
    [JsonPropertyName("routeStopId")]
    public string? RouteStopId { get; init; }
}

/// <summary>
/// Approval details supplied when updating a form submission. Mirrors the spec's
/// <c>FormSubmissionRequestApprovalDetailsRequestBody</c>.
/// </summary>
public sealed record FormSubmissionApprovalDetailsRequest
{
    /// <summary>Reviewer comment on the submission.</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }
}
