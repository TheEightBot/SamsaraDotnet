namespace Samsara.Sdk.Models.Documents;

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

    /// <summary>List of fields in the template (spec-required). Each entry mirrors the spec's
    /// <c>FormsFieldDefinitionObjectResponseBody</c>; <see cref="Sections"/> addresses this flat
    /// array by index.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<FormsFieldDefinition>? Fields { get; init; }

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

    /// <summary>Approval requirements (spec-required). Mirrors the spec's
    /// <c>SingleApprovalRequirementsObjectResponseBody</c>.</summary>
    [JsonPropertyName("requirements")]
    public SingleApprovalRequirements? Requirements { get; init; }
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

    /// <summary>List of field inputs in the submission (spec-required). Each entry mirrors the
    /// spec's <c>FormsFieldInputObjectResponseBody</c>.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<FormsFieldInput>? Fields { get; init; }

    /// <summary>How long the submission took to complete, in milliseconds.</summary>
    [JsonPropertyName("durationMs")]
    public long? DurationMs { get; init; }

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

    /// <summary>Field inputs to populate at creation time. Each entry mirrors the spec's
    /// <c>FormSubmissionRequestFieldInputObjectRequestBody</c>.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<FormSubmissionRequestFieldInput>? Fields { get; init; }

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

    /// <summary>
    /// Field inputs to write on the submission. Each entry mirrors the spec's
    /// <c>FormSubmissionRequestFieldInputObjectRequestBody</c>.
    /// </summary>
    /// <remarks>
    /// Added by the 2026-08-17 spec-parity sweep. The spec has always defined
    /// <c>fields</c> on <c>PATCH /form-submissions</c>, but the record omitted
    /// it entirely, so there was no way to write answers to an existing
    /// submission through the SDK.
    /// </remarks>
    [JsonPropertyName("fields")]
    public IReadOnlyList<FormSubmissionRequestFieldInput>? Fields { get; init; }
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

// ---------------------------------------------------------------------------
// Form template field definitions (GET /form-templates -> data.fields)
// ---------------------------------------------------------------------------

/// <summary>
/// A field definition on a form template. Mirrors the spec's
/// <c>FormsFieldDefinitionObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// <para>
/// Not to be confused with the <c>FormFieldDefinition</c> stub deleted earlier in
/// the 2026-08-17 sweep: that record modeled a <c>{id,label,type,required}</c>
/// shape reachable only through <c>FormSection.Fields</c>, which the spec does
/// not define. This record mirrors the real 17-property schema of the flat
/// <c>FormTemplate.Fields</c> array.
/// </para>
/// <para>
/// Which properties are populated depends on <see cref="Type"/>; the spec marks
/// <c>id</c>, <c>isRequired</c>, <c>label</c> and <c>type</c> REQUIRED, but all
/// stay nullable because this is a response record.
/// </para>
/// </remarks>
public sealed record FormsFieldDefinition
{
    /// <summary>Identifier of the field (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Label of the field. Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// Type of the field. Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>media</c>, <c>datetime</c>,
    /// <c>signature</c>, <c>asset</c>, <c>person</c>, <c>geofence</c>,
    /// <c>instruction</c>, <c>media_instruction</c>, <c>table</c>,
    /// <c>barcode</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Whether the field must be filled out by the user. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; init; }

    /// <summary>Whether the field allows manual entry of a person. Person fields only.</summary>
    [JsonPropertyName("allowManualEntry")]
    public bool? AllowManualEntry { get; init; }

    /// <summary>
    /// Asset types selectable for this field (<c>vehicle</c>, <c>trailer</c>,
    /// <c>equipment</c>, <c>unpoweredAsset</c>). Asset fields only.
    /// </summary>
    [JsonPropertyName("allowedAssetTypes")]
    public IReadOnlyList<string>? AllowedAssetTypes { get; init; }

    /// <summary>
    /// Type of date/time entry allowed (<c>datetime</c>, <c>date</c>,
    /// <c>time</c>). Datetime fields only.
    /// </summary>
    [JsonPropertyName("allowedDateTimeValueType")]
    public string? AllowedDateTimeValueType { get; init; }

    /// <summary>Identifier of the field that autofills this one, when configured.</summary>
    [JsonPropertyName("autofillFromId")]
    public string? AutofillFromId { get; init; }

    /// <summary>Columns of a table field.</summary>
    [JsonPropertyName("columns")]
    public IReadOnlyList<FormsTableFieldDefinition>? Columns { get; init; }

    /// <summary>Conditional actions attached to this field.</summary>
    [JsonPropertyName("conditionalActions")]
    public IReadOnlyList<FormsConditionalAction>? ConditionalActions { get; init; }

    /// <summary>Role IDs whose users are selectable people for this field. Person fields only.</summary>
    [JsonPropertyName("filterByRoleIds")]
    public IReadOnlyList<string>? FilterByRoleIds { get; init; }

    /// <summary>Whether drivers are selectable people for this field. Person fields only.</summary>
    [JsonPropertyName("includeDrivers")]
    public bool? IncludeDrivers { get; init; }

    /// <summary>Whether org users are selectable people for this field. Person fields only.</summary>
    [JsonPropertyName("includeUsers")]
    public bool? IncludeUsers { get; init; }

    /// <summary>Whether this field can autofill other fields. Media fields only.</summary>
    [JsonPropertyName("isAutofillSource")]
    public bool? IsAutofillSource { get; init; }

    /// <summary>Number of decimal places allowed. Number fields only.</summary>
    [JsonPropertyName("numDecimalPlaces")]
    public long? NumDecimalPlaces { get; init; }

    /// <summary>Select options for check-boxes or multiple-choice fields.</summary>
    [JsonPropertyName("options")]
    public IReadOnlyList<FormsSelectOption>? Options { get; init; }

    /// <summary>Maximum possible score weight for this field, when scoring is configured.</summary>
    [JsonPropertyName("questionWeight")]
    public long? QuestionWeight { get; init; }
}

/// <summary>
/// A column definition inside a table form field. Mirrors the spec's
/// <c>FormsTableFieldDefinitionObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>id</c>, <c>label</c> and <c>type</c> REQUIRED; all stay
/// nullable because this is a response record.
/// </remarks>
public sealed record FormsTableFieldDefinition
{
    /// <summary>Identifier of the field (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Label of the field. Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// Type of the field. Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>media</c>, <c>datetime</c>,
    /// <c>signature</c>, <c>person</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Whether the field allows manual entry of a person. Person fields only.</summary>
    [JsonPropertyName("allowManualEntry")]
    public bool? AllowManualEntry { get; init; }

    /// <summary>
    /// Type of date/time entry allowed (<c>datetime</c>, <c>date</c>,
    /// <c>time</c>). Datetime fields only.
    /// </summary>
    [JsonPropertyName("allowedDateTimeValueType")]
    public string? AllowedDateTimeValueType { get; init; }

    /// <summary>Role IDs whose users are selectable people for this field. Person fields only.</summary>
    [JsonPropertyName("filterByRoleIds")]
    public IReadOnlyList<string>? FilterByRoleIds { get; init; }

    /// <summary>Whether drivers are selectable people for this field. Person fields only.</summary>
    [JsonPropertyName("includeDrivers")]
    public bool? IncludeDrivers { get; init; }

    /// <summary>Whether org users are selectable people for this field. Person fields only.</summary>
    [JsonPropertyName("includeUsers")]
    public bool? IncludeUsers { get; init; }

    /// <summary>Number of decimal places allowed. Number fields only.</summary>
    [JsonPropertyName("numDecimalPlaces")]
    public long? NumDecimalPlaces { get; init; }

    /// <summary>Select options for check-boxes or multiple-choice columns.</summary>
    [JsonPropertyName("options")]
    public IReadOnlyList<FormsSelectOption>? Options { get; init; }
}

/// <summary>
/// A condition on a form field plus the actions taken when it is met. Mirrors
/// the spec's <c>FormsConditionalActionObjectResponseBody</c>.
/// </summary>
public sealed record FormsConditionalAction
{
    /// <summary>Actions to take if the condition is met. Spec marks REQUIRED.</summary>
    [JsonPropertyName("actions")]
    public IReadOnlyList<FormsAction>? Actions { get; init; }

    /// <summary>The condition that must be met. Spec marks REQUIRED.</summary>
    [JsonPropertyName("condition")]
    public FormsCondition? Condition { get; init; }
}

/// <summary>
/// One action taken when a form field's condition is met. Mirrors the spec's
/// <c>FormsActionObjectResponseBody</c>.
/// </summary>
public sealed record FormsAction
{
    /// <summary>
    /// Type of action. Valid values: <c>askFollowupQuestion</c>,
    /// <c>showSection</c>, <c>requirePhoto</c>, <c>requireNote</c>,
    /// <c>createIssue</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Identifier of the follow-up question shown when the condition is met.
    /// Only returned when the action type is <c>askFollowupQuestion</c>.
    /// </summary>
    [JsonPropertyName("fieldId")]
    public string? FieldId { get; init; }

    /// <summary>
    /// Identifier of the conditional section shown when the condition is met.
    /// Only returned when the action type is <c>showSection</c>.
    /// </summary>
    [JsonPropertyName("sectionId")]
    public string? SectionId { get; init; }
}

/// <summary>
/// The condition guarding a form field's conditional actions. Mirrors the spec's
/// <c>FormsConditionObjectResponseBody</c>.
/// </summary>
public sealed record FormsCondition
{
    /// <summary>
    /// Type of condition. Valid values: <c>multipleChoiceValueCondition</c>,
    /// <c>checkBoxesValueCondition</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Option IDs that satisfy the condition when selected.</summary>
    [JsonPropertyName("selectedOptionIds")]
    public IReadOnlyList<string>? SelectedOptionIds { get; init; }
}

/// <summary>
/// A selectable option on a multiple-choice or check-boxes form field. Mirrors
/// the spec's <c>FormsSelectOptionObjectResponseBody</c>.
/// </summary>
public sealed record FormsSelectOption
{
    /// <summary>Identifier of the option. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Label of the option. Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// Whether the question is excluded from the total score when this option is
    /// selected. Only present when the field has scoring.
    /// </summary>
    [JsonPropertyName("ignoreQuestionFromScoreIfSelected")]
    public bool? IgnoreQuestionFromScoreIfSelected { get; init; }

    /// <summary>
    /// Score points received when this option is selected. Only present when the
    /// field has scoring.
    /// </summary>
    [JsonPropertyName("optionScoreWeight")]
    public long? OptionScoreWeight { get; init; }
}

/// <summary>
/// Requirements for the single-approval workflow on a form template. Mirrors the
/// spec's <c>SingleApprovalRequirementsObjectResponseBody</c>.
/// </summary>
public sealed record SingleApprovalRequirements
{
    /// <summary>
    /// Role IDs representing which user roles can approve the submission. Spec
    /// marks REQUIRED.
    /// </summary>
    [JsonPropertyName("roleIds")]
    public IReadOnlyList<string>? RoleIds { get; init; }
}

// ---------------------------------------------------------------------------
// Form submission field inputs — response side
// (GET/POST/PATCH /form-submissions, GET /form-submissions/stream -> data.fields)
// ---------------------------------------------------------------------------

/// <summary>
/// One answered field on a form submission. Mirrors the spec's
/// <c>FormsFieldInputObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// This is a value union expressed as an object: the <c>*Value</c> member that
/// matches <see cref="Type"/> is the populated one. Spec marks <c>id</c> and
/// <c>type</c> REQUIRED; both stay nullable because this is a response record.
/// </remarks>
public sealed record FormsFieldInput
{
    /// <summary>Identifier of the field. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Type of the field. Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>datetime</c>,
    /// <c>signature</c>, <c>media</c>, <c>asset</c>, <c>table</c>,
    /// <c>person</c>, <c>geofence</c>, <c>barcode</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Label of the field.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>Note attached to the answer.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>Issue created from this field, when a conditional action created one.</summary>
    [JsonPropertyName("issue")]
    public FormsIssueCreatedByField? Issue { get; init; }

    /// <summary>Media attached to the answer (from a <c>requirePhoto</c> action).</summary>
    [JsonPropertyName("mediaList")]
    public IReadOnlyList<FormsMediaRecord>? MediaList { get; init; }

    /// <summary>Value of an asset field.</summary>
    [JsonPropertyName("assetValue")]
    public FormsAssetValue? AssetValue { get; init; }

    /// <summary>Value of a barcode field.</summary>
    [JsonPropertyName("barcodeValue")]
    public FormsBarcodeValue? BarcodeValue { get; init; }

    /// <summary>Value of a check-boxes field.</summary>
    [JsonPropertyName("checkBoxesValue")]
    public FormsCheckBoxesValue? CheckBoxesValue { get; init; }

    /// <summary>Value of a datetime field.</summary>
    [JsonPropertyName("dateTimeValue")]
    public FormsDateTimeValue? DateTimeValue { get; init; }

    /// <summary>Value of a geofence field.</summary>
    [JsonPropertyName("geofenceValue")]
    public FormsGeofenceValue? GeofenceValue { get; init; }

    /// <summary>Value of a media field.</summary>
    [JsonPropertyName("mediaValue")]
    public FormsMediaValue? MediaValue { get; init; }

    /// <summary>Value of a multiple-choice field.</summary>
    [JsonPropertyName("multipleChoiceValue")]
    public FormsMultipleChoiceValue? MultipleChoiceValue { get; init; }

    /// <summary>Value of a number field.</summary>
    [JsonPropertyName("numberValue")]
    public FormsNumberValue? NumberValue { get; init; }

    /// <summary>Value of a person field.</summary>
    [JsonPropertyName("personValue")]
    public FormsPersonValue? PersonValue { get; init; }

    /// <summary>Value of a signature field.</summary>
    [JsonPropertyName("signatureValue")]
    public FormsSignatureValue? SignatureValue { get; init; }

    /// <summary>Value of a table field.</summary>
    [JsonPropertyName("tableValue")]
    public FormsTableValue? TableValue { get; init; }

    /// <summary>Value of a text field.</summary>
    [JsonPropertyName("textValue")]
    public FormsTextValue? TextValue { get; init; }
}

/// <summary>
/// The value of an asset form field. Mirrors the spec's
/// <c>FormsAssetValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsAssetValue
{
    /// <summary>The selected or manually entered asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("asset")]
    public FormsAsset? Asset { get; init; }
}

/// <summary>
/// The value of a barcode form field. Mirrors the spec's
/// <c>FormsBarcodeValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsBarcodeValue
{
    /// <summary>List of barcode entries. Spec marks REQUIRED.</summary>
    [JsonPropertyName("barcodes")]
    public IReadOnlyList<FormsBarcode>? Barcodes { get; init; }
}

/// <summary>
/// A single captured barcode. Mirrors the spec's
/// <c>FormsBarcodeObjectResponseBody</c>.
/// </summary>
public sealed record FormsBarcode
{
    /// <summary>The captured barcode value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// The value of a check-boxes form field. Mirrors the spec's
/// <c>FormsCheckBoxesValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsCheckBoxesValue
{
    /// <summary>Labels of the selected options. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public IReadOnlyList<string>? Value { get; init; }

    /// <summary>IDs of the selected options. Spec marks REQUIRED.</summary>
    [JsonPropertyName("valueIds")]
    public IReadOnlyList<string>? ValueIds { get; init; }
}

/// <summary>
/// The value of a datetime form field. Mirrors the spec's
/// <c>FormsDateTimeValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsDateTimeValue
{
    /// <summary>
    /// Type of datetime format (<c>datetime</c>, <c>date</c>, <c>time</c>).
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>UTC timestamp in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public DateTimeOffset? Value { get; init; }

    /// <summary>Calendar date (<c>YYYY-MM-DD</c>) in the stored field timezone.</summary>
    [JsonPropertyName("dateValue")]
    public string? DateValue { get; init; }
}

/// <summary>
/// The value of a geofence form field. Mirrors the spec's
/// <c>FormsGeofenceValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsGeofenceValue
{
    /// <summary>The selected or manually entered geofence. Spec marks REQUIRED.</summary>
    [JsonPropertyName("geofence")]
    public FormsGeofence? Geofence { get; init; }
}

/// <summary>
/// An issue created from a form field by a <c>createIssue</c> conditional
/// action. Mirrors the spec's
/// <c>FormsIssueCreatedByFieldObjectResponseBody</c>.
/// </summary>
public sealed record FormsIssueCreatedByField
{
    /// <summary>ID of the created issue. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>A map of external IDs for the issue.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A media record attached to a form submission. Mirrors the spec's
/// <c>FormsMediaRecordObjectResponseBody</c>.
/// </summary>
public sealed record FormsMediaRecord
{
    /// <summary>ID of the media record (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Status of the media record (<c>unknown</c>, <c>processing</c>,
    /// <c>finished</c>). Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("processingStatus")]
    public string? ProcessingStatus { get; init; }

    /// <summary>Link to the media content. Present once processing has finished.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>Expiry time of <see cref="Url"/>, RFC 3339.</summary>
    [JsonPropertyName("urlExpiresAt")]
    public DateTimeOffset? UrlExpiresAt { get; init; }
}

/// <summary>
/// The value of a media form field. Mirrors the spec's
/// <c>FormsMediaValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsMediaValue
{
    /// <summary>List of media records. Spec marks REQUIRED.</summary>
    [JsonPropertyName("mediaList")]
    public IReadOnlyList<FormsMediaRecord>? MediaList { get; init; }
}

/// <summary>
/// The value of a multiple-choice form field. Mirrors the spec's
/// <c>FormsMultipleChoiceValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsMultipleChoiceValue
{
    /// <summary>Label of the selected option. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }

    /// <summary>ID of the selected option. Spec marks REQUIRED.</summary>
    [JsonPropertyName("valueId")]
    public string? ValueId { get; init; }
}

/// <summary>
/// The value of a number form field. Mirrors the spec's
/// <c>FormsNumberValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsNumberValue
{
    /// <summary>Number value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// The value of a person form field. Mirrors the spec's
/// <c>FormsPersonValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsPersonValue
{
    /// <summary>The selected or manually entered person. Spec marks REQUIRED.</summary>
    [JsonPropertyName("person")]
    public FormsPerson? Person { get; init; }
}

/// <summary>
/// A tracked or manually entered person on a form submission. Mirrors the spec's
/// <c>FormsPersonObjectResponseBody</c>.
/// </summary>
public sealed record FormsPerson
{
    /// <summary>
    /// Whether the person is <c>tracked</c> or <c>untracked</c>. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("entryType")]
    public string? EntryType { get; init; }

    /// <summary>Name of an untracked (manually entered) person.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Reference to the tracked driver or user.</summary>
    [JsonPropertyName("polymorphicUserId")]
    public FormsPolymorphicUser? PolymorphicUserId { get; init; }
}

/// <summary>
/// The value of a signature form field. Mirrors the spec's
/// <c>FormsSignatureValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsSignatureValue
{
    /// <summary>The captured signature image. Spec marks REQUIRED.</summary>
    [JsonPropertyName("media")]
    public FormsMediaRecord? Media { get; init; }
}

/// <summary>
/// The value of a table form field. Mirrors the spec's
/// <c>FormsTableValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsTableValue
{
    /// <summary>Table columns. Spec marks REQUIRED.</summary>
    [JsonPropertyName("columns")]
    public IReadOnlyList<FormsTableColumn>? Columns { get; init; }

    /// <summary>Table rows. Spec marks REQUIRED.</summary>
    [JsonPropertyName("rows")]
    public IReadOnlyList<FormsTableRow>? Rows { get; init; }
}

/// <summary>
/// A column of an answered table field. Mirrors the spec's
/// <c>FormsTableColumnObjectResponseBody</c>.
/// </summary>
public sealed record FormsTableColumn
{
    /// <summary>Unique identifier for the column (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Label of the column. Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// Type of the column field. Valid values: <c>text</c>, <c>number</c>,
    /// <c>datetime</c>, <c>check_boxes</c>, <c>multiple_choice</c>,
    /// <c>signature</c>, <c>media</c>, <c>person</c>, <c>barcode</c>. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// A row of an answered table field. Mirrors the spec's
/// <c>FormsTableRowObjectResponseBody</c>.
/// </summary>
public sealed record FormsTableRow
{
    /// <summary>Unique identifier for the row (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Cells in the row. Spec marks REQUIRED.</summary>
    [JsonPropertyName("cells")]
    public IReadOnlyList<FormsTableCell>? Cells { get; init; }
}

/// <summary>
/// A cell of an answered table field. Mirrors the spec's
/// <c>FormsTableCellObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Like <see cref="FormsFieldInput"/>, this is a value union: the <c>*Value</c>
/// member matching <see cref="Type"/> is the populated one.
/// </remarks>
public sealed record FormsTableCell
{
    /// <summary>Unique identifier for the cell (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Type of the cell field. Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>datetime</c>,
    /// <c>signature</c>, <c>media</c>, <c>person</c>, <c>barcode</c>. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Value of a barcode cell.</summary>
    [JsonPropertyName("barcodeValue")]
    public FormsBarcodeValue? BarcodeValue { get; init; }

    /// <summary>Value of a check-boxes cell.</summary>
    [JsonPropertyName("checkBoxesValue")]
    public FormsCheckBoxesValue? CheckBoxesValue { get; init; }

    /// <summary>Value of a datetime cell.</summary>
    [JsonPropertyName("dateTimeValue")]
    public FormsDateTimeValue? DateTimeValue { get; init; }

    /// <summary>Value of a media cell.</summary>
    [JsonPropertyName("mediaValue")]
    public FormsMediaValue? MediaValue { get; init; }

    /// <summary>Value of a multiple-choice cell.</summary>
    [JsonPropertyName("multipleChoiceValue")]
    public FormsMultipleChoiceValue? MultipleChoiceValue { get; init; }

    /// <summary>Value of a number cell.</summary>
    [JsonPropertyName("numberValue")]
    public FormsNumberValue? NumberValue { get; init; }

    /// <summary>Value of a person cell.</summary>
    [JsonPropertyName("personValue")]
    public FormsPersonValue? PersonValue { get; init; }

    /// <summary>Value of a signature cell.</summary>
    [JsonPropertyName("signatureValue")]
    public FormsSignatureValue? SignatureValue { get; init; }

    /// <summary>Value of a text cell.</summary>
    [JsonPropertyName("textValue")]
    public FormsTextValue? TextValue { get; init; }
}

/// <summary>
/// The value of a text form field. Mirrors the spec's
/// <c>FormsTextValueObjectResponseBody</c>.
/// </summary>
public sealed record FormsTextValue
{
    /// <summary>Text value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

// ---------------------------------------------------------------------------
// Form submission field inputs — request side
// (POST/PATCH /form-submissions -> fields)
// ---------------------------------------------------------------------------

/// <summary>
/// One field answer written to a form submission. Mirrors the spec's
/// <c>FormSubmissionRequestFieldInputObjectRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <see cref="FormsFieldInput"/> rather than shared:
/// the spec's request value shapes are genuinely different (an asset value is
/// <c>{ id }</c> on the way in and <c>{ entryType, id, name, externalIds }</c> on
/// the way out), and the request marks members REQUIRED. Same precedent as
/// <c>ServiceTaskInstanceInput</c> and <c>UsDriverRulesetOverrideInput</c>.
/// </remarks>
public sealed record FormSubmissionRequestFieldInput
{
    /// <summary>Identifier of the field being answered. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Type of the field. Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>datetime</c>,
    /// <c>asset</c>, <c>person</c>, <c>table</c>, <c>geofence</c>,
    /// <c>barcode</c>, <c>media</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Value for an asset field.</summary>
    [JsonPropertyName("assetValue")]
    public FormSubmissionRequestAssetValue? AssetValue { get; init; }

    /// <summary>Value for a barcode field.</summary>
    [JsonPropertyName("barcodeValue")]
    public FormSubmissionRequestBarcodeValue? BarcodeValue { get; init; }

    /// <summary>Value for a check-boxes field.</summary>
    [JsonPropertyName("checkBoxesValue")]
    public FormSubmissionRequestCheckBoxesValue? CheckBoxesValue { get; init; }

    /// <summary>Value for a datetime field.</summary>
    [JsonPropertyName("dateTimeValue")]
    public FormSubmissionRequestDateTimeValue? DateTimeValue { get; init; }

    /// <summary>Value for a geofence field.</summary>
    [JsonPropertyName("geofenceValue")]
    public FormSubmissionRequestGeofenceValue? GeofenceValue { get; init; }

    /// <summary>Value for a media field.</summary>
    [JsonPropertyName("mediaValue")]
    public FormSubmissionRequestMediaValue? MediaValue { get; init; }

    /// <summary>Value for a multiple-choice field.</summary>
    [JsonPropertyName("multipleChoiceValue")]
    public FormSubmissionRequestMultipleChoiceValue? MultipleChoiceValue { get; init; }

    /// <summary>Value for a number field.</summary>
    [JsonPropertyName("numberValue")]
    public FormSubmissionRequestNumberValue? NumberValue { get; init; }

    /// <summary>Value for a person field.</summary>
    [JsonPropertyName("personValue")]
    public FormSubmissionRequestPersonValue? PersonValue { get; init; }

    /// <summary>Value for a table field.</summary>
    [JsonPropertyName("tableValue")]
    public FormSubmissionRequestTableValue? TableValue { get; init; }

    /// <summary>Value for a text field.</summary>
    [JsonPropertyName("textValue")]
    public FormSubmissionRequestTextValue? TextValue { get; init; }
}

/// <summary>
/// Value written to an asset form field. Mirrors the spec's
/// <c>FormSubmissionRequestAssetValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestAssetValue
{
    /// <summary>The asset to record. Spec marks REQUIRED.</summary>
    [JsonPropertyName("asset")]
    public required FormSubmissionRequestAsset Asset { get; init; }
}

/// <summary>
/// The asset referenced by an asset field answer. Mirrors the spec's
/// <c>FormSubmissionRequestAssetObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestAsset
{
    /// <summary>Samsara ID of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// Value written to a barcode form field. Mirrors the spec's
/// <c>FormSubmissionRequestBarcodeValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestBarcodeValue
{
    /// <summary>Barcode entries to record. Spec marks REQUIRED.</summary>
    [JsonPropertyName("barcodes")]
    public required IReadOnlyList<FormSubmissionRequestBarcode> Barcodes { get; init; }
}

/// <summary>
/// A single barcode entry written to a form submission. Mirrors the spec's
/// <c>FormSubmissionRequestBarcodeObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestBarcode
{
    /// <summary>The captured barcode value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }
}

/// <summary>
/// Value written to a check-boxes form field. Mirrors the spec's
/// <c>FormSubmissionRequestCheckBoxesValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestCheckBoxesValue
{
    /// <summary>IDs of the selected options. Spec marks REQUIRED.</summary>
    [JsonPropertyName("valueIds")]
    public required IReadOnlyList<string> ValueIds { get; init; }
}

/// <summary>
/// Value written to a datetime form field. Mirrors the spec's
/// <c>FormSubmissionRequestDateTimeValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestDateTimeValue
{
    /// <summary>UTC timestamp in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required DateTimeOffset Value { get; init; }
}

/// <summary>
/// Value written to a geofence form field. Mirrors the spec's
/// <c>FormSubmissionRequestGeofenceValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestGeofenceValue
{
    /// <summary>The geofence to record. Spec marks REQUIRED.</summary>
    [JsonPropertyName("geofence")]
    public required FormSubmissionRequestGeofence Geofence { get; init; }
}

/// <summary>
/// The geofence referenced by a geofence field answer. Mirrors the spec's
/// <c>FormSubmissionRequestGeofenceObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestGeofence
{
    /// <summary>Samsara ID of the geofence/address. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// Value written to a media form field. Mirrors the spec's
/// <c>FormSubmissionRequestMediaValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestMediaValue
{
    /// <summary>Media items to upload. Spec marks REQUIRED.</summary>
    [JsonPropertyName("mediaList")]
    public required IReadOnlyList<FormSubmissionRequestMediaItem> MediaList { get; init; }
}

/// <summary>
/// A media item uploaded with a form submission. Mirrors the spec's
/// <c>FormSubmissionRequestMediaItemObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestMediaItem
{
    /// <summary>Base64-encoded media payload. Spec marks REQUIRED.</summary>
    [JsonPropertyName("base64Payload")]
    public required string Base64Payload { get; init; }

    /// <summary>Media type of the payload. Spec marks REQUIRED.</summary>
    [JsonPropertyName("mediaType")]
    public required string MediaType { get; init; }
}

/// <summary>
/// Value written to a multiple-choice form field. Mirrors the spec's
/// <c>FormSubmissionRequestMultipleChoiceValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestMultipleChoiceValue
{
    /// <summary>ID of the selected option (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("valueId")]
    public required string ValueId { get; init; }
}

/// <summary>
/// Value written to a number form field. Mirrors the spec's
/// <c>FormSubmissionRequestNumberValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestNumberValue
{
    /// <summary>Number value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required double Value { get; init; }
}

/// <summary>
/// Value written to a person form field. Mirrors the spec's
/// <c>FormSubmissionRequestPersonValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestPersonValue
{
    /// <summary>The person to record. Spec marks REQUIRED.</summary>
    [JsonPropertyName("person")]
    public required FormSubmissionRequestPerson Person { get; init; }
}

/// <summary>
/// The person referenced by a person field answer. Mirrors the spec's
/// <c>FormSubmissionRequestPersonObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestPerson
{
    /// <summary>Polymorphic user ID of the driver or user. Spec marks REQUIRED.</summary>
    [JsonPropertyName("polymorphicUserId")]
    public required string PolymorphicUserId { get; init; }
}

/// <summary>
/// Value written to a table form field. Mirrors the spec's
/// <c>FormSubmissionRequestTableValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestTableValue
{
    /// <summary>Rows to record. Spec marks REQUIRED.</summary>
    [JsonPropertyName("rows")]
    public required IReadOnlyList<FormSubmissionRequestTableRow> Rows { get; init; }
}

/// <summary>
/// A row written to a table form field. Mirrors the spec's
/// <c>FormSubmissionRequestTableRowObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestTableRow
{
    /// <summary>Unique identifier for the row (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Cells in the row. Spec marks REQUIRED.</summary>
    [JsonPropertyName("cells")]
    public required IReadOnlyList<FormSubmissionRequestTableCell> Cells { get; init; }
}

/// <summary>
/// A cell written to a table form field. Mirrors the spec's
/// <c>FormSubmissionRequestTableCellObjectRequestBody</c>.
/// </summary>
/// <remarks>
/// The request-side cell has no <c>mediaValue</c> or <c>signatureValue</c>
/// member — the spec's cell type enum omits <c>media</c> and <c>signature</c> on
/// the way in.
/// </remarks>
public sealed record FormSubmissionRequestTableCell
{
    /// <summary>Unique identifier for the cell (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Type of the cell field. Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>datetime</c>,
    /// <c>person</c>, <c>barcode</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Value for a barcode cell.</summary>
    [JsonPropertyName("barcodeValue")]
    public FormSubmissionRequestBarcodeValue? BarcodeValue { get; init; }

    /// <summary>Value for a check-boxes cell.</summary>
    [JsonPropertyName("checkBoxesValue")]
    public FormSubmissionRequestCheckBoxesValue? CheckBoxesValue { get; init; }

    /// <summary>Value for a datetime cell.</summary>
    [JsonPropertyName("dateTimeValue")]
    public FormSubmissionRequestDateTimeValue? DateTimeValue { get; init; }

    /// <summary>Value for a multiple-choice cell.</summary>
    [JsonPropertyName("multipleChoiceValue")]
    public FormSubmissionRequestMultipleChoiceValue? MultipleChoiceValue { get; init; }

    /// <summary>Value for a number cell.</summary>
    [JsonPropertyName("numberValue")]
    public FormSubmissionRequestNumberValue? NumberValue { get; init; }

    /// <summary>Value for a person cell.</summary>
    [JsonPropertyName("personValue")]
    public FormSubmissionRequestPersonValue? PersonValue { get; init; }

    /// <summary>Value for a text cell.</summary>
    [JsonPropertyName("textValue")]
    public FormSubmissionRequestTextValue? TextValue { get; init; }
}

/// <summary>
/// Value written to a text form field. Mirrors the spec's
/// <c>FormSubmissionRequestTextValueObjectRequestBody</c>.
/// </summary>
public sealed record FormSubmissionRequestTextValue
{
    /// <summary>Text value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }
}
