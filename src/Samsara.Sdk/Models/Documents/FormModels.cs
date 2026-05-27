namespace Samsara.Sdk.Models.Documents;

using System.Text.Json.Serialization;

/// <summary>
/// Response object for a Samsara form template (mirrors spec
/// <c>FormTemplateResponseObjectResponseBody</c>).
/// </summary>
public sealed record FormTemplate
{
    /// <summary>Unique identifier of the form template.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Title of the form template (spec REQUIRED). The Samsara API renamed the
    /// legacy <c>name</c> field to <c>title</c>; the SDK keeps both
    /// <see cref="Name"/> (legacy) and <see cref="Title"/> (spec) so callers
    /// against older responses continue to deserialize. Servers will populate
    /// one or the other — read whichever is non-null.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Legacy name property — retained for back-compat with consumers that
    /// already read <c>FormTemplate.Name</c>. Prefer <see cref="Title"/> per
    /// the current spec.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Description of the form template, when present.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Legacy integer revision (back-compat). Prefer <see cref="RevisionId"/>
    /// (spec uses a uuid string).
    /// </summary>
    [JsonPropertyName("revision")]
    public int? Revision { get; init; }

    /// <summary>Unique identifier of the form template revision (spec REQUIRED, uuid).</summary>
    [JsonPropertyName("revisionId")]
    public string? RevisionId { get; init; }

    /// <summary>Category of the form template (e.g. <c>general</c>, <c>safety</c>).</summary>
    [JsonPropertyName("formCategory")]
    public string? FormCategory { get; init; }

    /// <summary>Approval configuration for the template (spec object).</summary>
    [JsonPropertyName("approvalConfig")]
    public object? ApprovalConfig { get; init; }

    /// <summary>List of fields in the template (spec REQUIRED).</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<object>? Fields { get; init; }

    /// <summary>List of sections in the template (spec REQUIRED).</summary>
    [JsonPropertyName("sections")]
    public IReadOnlyList<FormSection> Sections { get; init; } = Array.Empty<FormSection>();

    /// <summary>Creator of the template (spec REQUIRED, polymorphic user object).</summary>
    [JsonPropertyName("createdBy")]
    public object? CreatedBy { get; init; }

    /// <summary>Last updater of the template (spec REQUIRED, polymorphic user object).</summary>
    [JsonPropertyName("updatedBy")]
    public object? UpdatedBy { get; init; }

    /// <summary>Creation time of the template, RFC 3339 (spec REQUIRED).</summary>
    [JsonPropertyName("createdAtTime")]
    public string CreatedAtTime { get; init; } = string.Empty;

    /// <summary>Last update time of the template, RFC 3339 (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAtTime")]
    public string UpdatedAtTime { get; init; } = string.Empty;
}

public sealed record FormSection
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("fields")]
    public IReadOnlyList<FormFieldDefinition>? Fields { get; init; }
}

public sealed record FormFieldDefinition
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("required")]
    public bool? Required { get; init; }
}

/// <summary>
/// Response object for a form submission (mirrors spec
/// <c>FormSubmissionResponseObjectResponseBody</c>).
/// </summary>
public sealed record FormSubmission
{
    /// <summary>ID of the form submission.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Title of the form submission, if set.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Status of the submission (spec REQUIRED). Valid values:
    /// <c>notStarted</c>, <c>completed</c>, <c>archived</c>, <c>inProgress</c>,
    /// <c>needsReview</c>, <c>changesRequested</c>, <c>approved</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>Whether the submission is required (spec REQUIRED).</summary>
    [JsonPropertyName("isRequired")]
    public bool IsRequired { get; init; }

    /// <summary>Reference to the form template (spec REQUIRED, object).</summary>
    [JsonPropertyName("formTemplate")]
    public object? FormTemplate { get; init; }

    /// <summary>Polymorphic user/driver who submitted the form (spec REQUIRED).</summary>
    [JsonPropertyName("submittedBy")]
    public object? SubmittedBy { get; init; }

    /// <summary>Polymorphic user/driver assigned to the form, when present.</summary>
    [JsonPropertyName("assignedTo")]
    public object? AssignedTo { get; init; }

    /// <summary>Approval details, when the submission has been reviewed.</summary>
    [JsonPropertyName("approvalDetails")]
    public object? ApprovalDetails { get; init; }

    /// <summary>Asset associated with the submission (tracked or untracked).</summary>
    [JsonPropertyName("asset")]
    public object? Asset { get; init; }

    /// <summary>Geofence associated with the submission (tracked or untracked).</summary>
    [JsonPropertyName("geofence")]
    public object? Geofence { get; init; }

    /// <summary>Location at submission time (latitude/longitude).</summary>
    [JsonPropertyName("location")]
    public object? Location { get; init; }

    /// <summary>Score of the submission, when scoring is configured.</summary>
    [JsonPropertyName("score")]
    public object? Score { get; init; }

    /// <summary>Map of external ids associated with the submission.</summary>
    [JsonPropertyName("externalIds")]
    public object? ExternalIds { get; init; }

    /// <summary>List of field inputs in the submission (spec REQUIRED).</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<object>? Fields { get; init; }

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

    /// <summary>Submission time, RFC 3339 string (spec REQUIRED).</summary>
    [JsonPropertyName("submittedAtTime")]
    public string SubmittedAtTime { get; init; } = string.Empty;

    /// <summary>Creation time, RFC 3339 string (spec REQUIRED).</summary>
    [JsonPropertyName("createdAtTime")]
    public string CreatedAtTime { get; init; } = string.Empty;

    /// <summary>Last update time, RFC 3339 string (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAtTime")]
    public string UpdatedAtTime { get; init; } = string.Empty;

    // ── Back-compat fields (not in current spec inner schema) ─────────────────
    // Retained for consumers that already read these. Prefer the spec-shaped
    // properties above. Servers may not populate any of these.

    /// <summary>Legacy flat form template id (back-compat). Prefer <see cref="FormTemplate"/>.</summary>
    [JsonPropertyName("formTemplateId")]
    public string? FormTemplateId { get; init; }

    /// <summary>Legacy flat form template name (back-compat).</summary>
    [JsonPropertyName("formTemplateName")]
    public string? FormTemplateName { get; init; }

    /// <summary>Legacy flat driver id (back-compat). Prefer <see cref="SubmittedBy"/>.</summary>
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    /// <summary>Legacy flat driver name (back-compat).</summary>
    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    /// <summary>Legacy flat vehicle id (back-compat).</summary>
    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    /// <summary>Legacy flat vehicle name (back-compat).</summary>
    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    /// <summary>Legacy state (back-compat). Prefer <see cref="Status"/>.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Legacy flat field values list (back-compat). Prefer <see cref="Fields"/>.</summary>
    [JsonPropertyName("fieldValues")]
    public IReadOnlyList<FormFieldValue>? FieldValues { get; init; }
}

public sealed record FormFieldValue
{
    [JsonPropertyName("fieldId")]
    public string? FieldId { get; init; }

    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("value")]
    public object? Value { get; init; }
}

/// <summary>
/// Response object for a form PDF export job (mirrors spec
/// <c>FormSubmissionPdfExportResponseObjectResponseBody</c>).
/// </summary>
public sealed record FormPdfExport
{
    /// <summary>ID of the form submission being exported (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Unique id for the PDF export that was created (spec REQUIRED).</summary>
    [JsonPropertyName("pdfId")]
    public string PdfId { get; init; } = string.Empty;

    /// <summary>
    /// Status of the PDF export job (spec REQUIRED). Valid values:
    /// <c>unknown</c>, <c>pending</c>, <c>done</c>, <c>failed</c>.
    /// </summary>
    [JsonPropertyName("jobStatus")]
    public string JobStatus { get; init; } = string.Empty;

    /// <summary>Time at which the PDF export POST request was made (spec REQUIRED).</summary>
    [JsonPropertyName("requestedAtTime")]
    public DateTimeOffset RequestedAtTime { get; init; }

    /// <summary>Time at which the job expires (spec REQUIRED).</summary>
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

    // ── Back-compat fields (not in current spec inner schema) ─────────────────

    /// <summary>Legacy job status property (back-compat). Prefer <see cref="JobStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Legacy form submission id alias (back-compat). Prefer <see cref="Id"/>.</summary>
    [JsonPropertyName("formSubmissionId")]
    public string? FormSubmissionId { get; init; }

    /// <summary>Legacy creation timestamp (back-compat). Prefer <see cref="RequestedAtTime"/>.</summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }
}

/// <summary>Request body for <c>POST /form-submissions</c>.</summary>
public sealed record CreateFormSubmissionRequest
{
    /// <summary>
    /// Reference to the form template being submitted (spec REQUIRED). The
    /// spec accepts an object with <c>id</c> and <c>revisionId</c>; pass an
    /// anonymous object or your own DTO.
    /// </summary>
    [JsonPropertyName("formTemplate")]
    public required object FormTemplate { get; init; }

    /// <summary>
    /// Initial status of the form submission (spec REQUIRED). Only valid
    /// value on create is <c>notStarted</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; init; }

    /// <summary>Title of the form submission.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>Driver or user the submission is assigned to.</summary>
    [JsonPropertyName("assignedTo")]
    public object? AssignedTo { get; init; }

    /// <summary>Due time for the submission, RFC 3339.</summary>
    [JsonPropertyName("dueAtTime")]
    public DateTimeOffset? DueAtTime { get; init; }

    /// <summary>Field inputs to populate at creation time.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<object>? Fields { get; init; }

    /// <summary>Whether the worker is required to complete this form at a route stop.</summary>
    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; init; }

    /// <summary>Route stop id the submission is assigned to.</summary>
    [JsonPropertyName("routeStopId")]
    public string? RouteStopId { get; init; }

    // ── Back-compat fields (not in current spec inner schema) ─────────────────
    // Retained so existing callers compile; new code should use FormTemplate.

    /// <summary>Legacy flat form template id (back-compat). Prefer <see cref="FormTemplate"/>.</summary>
    [JsonPropertyName("formTemplateId")]
    public string? FormTemplateId { get; init; }

    /// <summary>Legacy driver assignment (back-compat). Prefer <see cref="AssignedTo"/>.</summary>
    [JsonPropertyName("driver")]
    public object? Driver { get; init; }

    /// <summary>Legacy vehicle association (back-compat).</summary>
    [JsonPropertyName("vehicle")]
    public object? Vehicle { get; init; }

    /// <summary>Legacy field values payload (back-compat). Prefer <see cref="Fields"/>.</summary>
    [JsonPropertyName("fieldValues")]
    public IReadOnlyList<object>? FieldValues { get; init; }
}

/// <summary>Request body for <c>PATCH /form-submissions</c>. The submission id is in the body.</summary>
public sealed record UpdateFormSubmissionRequest
{
    /// <summary>ID of the form submission to update (spec REQUIRED).</summary>
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

    /// <summary>Approval details for review workflow.</summary>
    [JsonPropertyName("approvalDetails")]
    public object? ApprovalDetails { get; init; }

    /// <summary>Driver or user the submission is assigned to.</summary>
    [JsonPropertyName("assignedTo")]
    public object? AssignedTo { get; init; }

    /// <summary>Due time for the submission, RFC 3339.</summary>
    [JsonPropertyName("dueAtTime")]
    public DateTimeOffset? DueAtTime { get; init; }

    /// <summary>Route stop id the submission is assigned to.</summary>
    [JsonPropertyName("routeStopId")]
    public string? RouteStopId { get; init; }

    // ── Back-compat fields (not in current spec inner schema) ─────────────────

    /// <summary>Legacy field values payload (back-compat).</summary>
    [JsonPropertyName("fieldValues")]
    public IReadOnlyList<object>? FieldValues { get; init; }
}
