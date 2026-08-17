namespace Samsara.Sdk.Models.Beta;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Documents;

/// <summary>
/// A driver/asset qualification record. Mirrors the spec's
/// <c>QualificationRecordResponseObjectResponseBody</c> (the <c>data</c> payload of
/// <c>GET /qualification-records</c>, <c>GET /qualification-records/stream</c>,
/// <c>POST /qualification-records</c> and <c>PATCH /qualification-records</c>).
/// </summary>
/// <remarks>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </remarks>
public sealed record QualificationRecord
{
    /// <summary>ID of the qualification record. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The worker or asset the record belongs to. Spec-required.</summary>
    [JsonPropertyName("owner")]
    public QualificationOwner? Owner { get; init; }

    /// <summary>The qualification type this record instantiates. Spec-required.</summary>
    [JsonPropertyName("qualificationType")]
    public QualificationTypeReference? QualificationType { get; init; }

    /// <summary>
    /// Record status: <c>active</c>, <c>archived</c> or <c>deleted</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("recordStatus")]
    public string? RecordStatus { get; init; }

    /// <summary>Issue/effective date, UTC RFC 3339. Spec-required.</summary>
    [JsonPropertyName("issueDate")]
    public DateTimeOffset? IssueDate { get; init; }

    /// <summary>Expiration date, UTC RFC 3339.</summary>
    [JsonPropertyName("expirationDate")]
    public DateTimeOffset? ExpirationDate { get; init; }

    /// <summary>A map of external ids for the record.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Field inputs captured on the record. Spec-required.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<QualificationFieldInput>? Fields { get; init; }

    /// <summary>Creator of the record. Spec-required.</summary>
    [JsonPropertyName("createdBy")]
    public FormsPolymorphicUser? CreatedBy { get; init; }

    /// <summary>Last updater of the record. Spec-required.</summary>
    [JsonPropertyName("updatedBy")]
    public FormsPolymorphicUser? UpdatedBy { get; init; }

    /// <summary>Creation time, UTC RFC 3339. Spec-required.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Last updated time, UTC RFC 3339. Spec-required.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// The worker or asset a qualification record belongs to. Mirrors the spec's
/// <c>QualificationOwnerObjectResponseBody</c>.
/// </summary>
public sealed record QualificationOwner
{
    /// <summary>ID of the owner (worker or asset). Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Owner entity type: <c>worker</c> or <c>asset</c>. Spec-required.</summary>
    [JsonPropertyName("entityType")]
    public string? EntityType { get; init; }

    /// <summary>A map of external ids for the owner.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// The qualification type a record instantiates. Mirrors the spec's
/// <c>QualificationTypeReferenceObjectResponseBody</c>.
/// </summary>
public sealed record QualificationTypeReference
{
    /// <summary>ID of the qualification type (template uuid). Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the qualification type.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>ID of the qualification type revision (template revision uuid).</summary>
    [JsonPropertyName("revisionId")]
    public string? RevisionId { get; init; }
}

/// <summary>
/// One field input captured on a qualification record. Mirrors the spec's
/// <c>QualificationFieldInputObjectResponseBody</c>, which reuses the shared
/// <c>Forms*</c> value schemas already modelled in
/// <c>Samsara.Sdk.Models.Documents</c>.
/// </summary>
public sealed record QualificationFieldInput
{
    /// <summary>ID of the qualification input field object (uuid). Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Type of the field. Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>datetime</c>, <c>signature</c>,
    /// <c>media</c>, <c>table</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Qualification input field label.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>Value for a number field.</summary>
    [JsonPropertyName("numberValue")]
    public FormsNumberValue? NumberValue { get; init; }

    /// <summary>Value for a text field.</summary>
    [JsonPropertyName("textValue")]
    public FormsTextValue? TextValue { get; init; }

    /// <summary>Value for a multiple-choice field.</summary>
    [JsonPropertyName("multipleChoiceValue")]
    public FormsMultipleChoiceValue? MultipleChoiceValue { get; init; }

    /// <summary>Value for a check-boxes field.</summary>
    [JsonPropertyName("checkBoxesValue")]
    public FormsCheckBoxesValue? CheckBoxesValue { get; init; }

    /// <summary>Value for a datetime field.</summary>
    [JsonPropertyName("dateTimeValue")]
    public FormsDateTimeValue? DateTimeValue { get; init; }

    /// <summary>Value for a signature field.</summary>
    [JsonPropertyName("signatureValue")]
    public FormsSignatureValue? SignatureValue { get; init; }

    /// <summary>Value for a table field.</summary>
    [JsonPropertyName("tableValue")]
    public FormsTableValue? TableValue { get; init; }

    /// <summary>List of qualification media records attached to the field.</summary>
    [JsonPropertyName("mediaList")]
    public IReadOnlyList<FormsMediaRecord>? MediaList { get; init; }
}

/// <summary>
/// A qualification type (template). Mirrors the spec's
/// <c>QualificationTypeResponseObjectResponseBody</c> (the <c>data</c> payload of
/// <c>GET /qualification-types</c>).
/// </summary>
public sealed record QualificationType
{
    /// <summary>Unique identifier of the qualification type (uuid). Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the qualification type. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Entity type the qualification applies to: <c>worker</c> or <c>asset</c>. Spec-required.</summary>
    [JsonPropertyName("entityType")]
    public string? EntityType { get; init; }

    /// <summary>Unique identifier of the qualification type revision (uuid). Spec-required.</summary>
    [JsonPropertyName("revisionId")]
    public string? RevisionId { get; init; }

    /// <summary>
    /// Field definitions on the qualification type. Spec-required. Each entry mirrors
    /// the spec's <c>FormsFieldDefinitionObjectResponseBody</c>.
    /// </summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<FormsFieldDefinition>? Fields { get; init; }

    /// <summary>Creator of the qualification type. Spec-required.</summary>
    [JsonPropertyName("createdBy")]
    public FormsPolymorphicUser? CreatedBy { get; init; }

    /// <summary>Last updater of the qualification type. Spec-required.</summary>
    [JsonPropertyName("updatedBy")]
    public FormsPolymorphicUser? UpdatedBy { get; init; }

    /// <summary>Creation time, UTC RFC 3339. Spec-required.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Update time, UTC RFC 3339. Spec-required.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// Body for <c>POST /qualification-records</c>. Mirrors the spec's
/// <c>QualificationsPostQualificationRecordRequestBody</c>.
/// </summary>
public sealed record QualificationRecordCreateRequest
{
    /// <summary>The worker or asset the record belongs to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("owner")]
    public required QualificationOwnerInput Owner { get; init; }

    /// <summary>The qualification type to instantiate. Spec marks REQUIRED.</summary>
    [JsonPropertyName("qualificationType")]
    public required QualificationTypeInput QualificationType { get; init; }

    /// <summary>Issue/effective date, UTC RFC 3339. Spec marks REQUIRED.</summary>
    [JsonPropertyName("issueDate")]
    public required DateTimeOffset IssueDate { get; init; }

    /// <summary>Expiration date, UTC RFC 3339.</summary>
    [JsonPropertyName("expirationDate")]
    public DateTimeOffset? ExpirationDate { get; init; }

    /// <summary>A map of external ids to associate with the record.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Custom field values for the record.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<QualificationRecordFieldInput>? Fields { get; init; }
}

/// <summary>
/// Body for <c>PATCH /qualification-records</c>. Mirrors the spec's
/// <c>QualificationsPatchQualificationRecordRequestBody</c>. The record is
/// identified by the <c>id</c> member of the body, not by a query parameter.
/// </summary>
public sealed record QualificationRecordUpdateRequest
{
    /// <summary>ID of the qualification record to update. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Replacement owner for the record.</summary>
    [JsonPropertyName("owner")]
    public QualificationOwnerInput? Owner { get; init; }

    /// <summary>Issue/effective date, UTC RFC 3339.</summary>
    [JsonPropertyName("issueDate")]
    public DateTimeOffset? IssueDate { get; init; }

    /// <summary>
    /// Expiration date, UTC RFC 3339. Set to <c>1970-01-01T00:00:00Z</c> to clear it.
    /// </summary>
    [JsonPropertyName("expirationDate")]
    public DateTimeOffset? ExpirationDate { get; init; }

    /// <summary>A map of external ids for the record.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Custom field values to set. Only include fields that need changing.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<QualificationRecordFieldInput>? Fields { get; init; }
}

/// <summary>
/// Body for <c>POST /qualification-records/archive</c> and
/// <c>POST /qualification-records/unarchive</c>. Mirrors the spec's byte-identical
/// <c>QualificationsArchiveQualificationRecordRequestBody</c> and
/// <c>QualificationsUnarchiveQualificationRecordRequestBody</c>.
/// </summary>
public sealed record QualificationRecordIdRequest
{
    /// <summary>ID of the qualification record to archive or unarchive. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// The worker or asset written as a qualification record's owner. Mirrors the
/// spec's <c>QualificationOwnerRequestObjectRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>QualificationOwner</c>: the request carries no
/// <c>externalIds</c> and marks both members REQUIRED.
/// </remarks>
public sealed record QualificationOwnerInput
{
    /// <summary>ID of the owner (worker or asset). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Owner entity type: <c>worker</c> or <c>asset</c>. Spec marks REQUIRED.</summary>
    [JsonPropertyName("entityType")]
    public required string EntityType { get; init; }
}

/// <summary>
/// The qualification type referenced when writing a record. Mirrors the spec's
/// <c>QualificationTypeRequestObjectRequestBody</c>.
/// </summary>
public sealed record QualificationTypeInput
{
    /// <summary>ID of the qualification type (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// One field value written to a qualification record. Mirrors the spec's
/// <c>QualificationRecordRequestFieldInputObjectRequestBody</c>, which reuses the
/// shared <c>FormSubmissionRequest*</c> value schemas already modelled in
/// <c>Samsara.Sdk.Models.Documents</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>QualificationFieldInput</c>: the request value
/// shapes genuinely differ (a media value is <c>{ base64Payload, mediaType }</c> on
/// the way in and a processed media record on the way out), the request carries no
/// <c>label</c>, and it marks members REQUIRED.
/// </remarks>
public sealed record QualificationRecordFieldInput
{
    /// <summary>ID of the qualification input field object (uuid). Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Type of the field. Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>datetime</c>, <c>table</c>,
    /// <c>media</c>, <c>signature</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Value for a number field.</summary>
    [JsonPropertyName("numberValue")]
    public FormSubmissionRequestNumberValue? NumberValue { get; init; }

    /// <summary>Value for a text field.</summary>
    [JsonPropertyName("textValue")]
    public FormSubmissionRequestTextValue? TextValue { get; init; }

    /// <summary>Value for a multiple-choice field.</summary>
    [JsonPropertyName("multipleChoiceValue")]
    public FormSubmissionRequestMultipleChoiceValue? MultipleChoiceValue { get; init; }

    /// <summary>Value for a check-boxes field.</summary>
    [JsonPropertyName("checkBoxesValue")]
    public FormSubmissionRequestCheckBoxesValue? CheckBoxesValue { get; init; }

    /// <summary>Value for a datetime field.</summary>
    [JsonPropertyName("dateTimeValue")]
    public FormSubmissionRequestDateTimeValue? DateTimeValue { get; init; }

    /// <summary>Value for a media field.</summary>
    [JsonPropertyName("mediaValue")]
    public FormSubmissionRequestMediaValue? MediaValue { get; init; }

    /// <summary>Value for a signature field.</summary>
    [JsonPropertyName("signatureValue")]
    public QualificationSignatureValueInput? SignatureValue { get; init; }

    /// <summary>Value for a table field.</summary>
    [JsonPropertyName("tableValue")]
    public FormSubmissionRequestTableValue? TableValue { get; init; }
}

/// <summary>
/// Value written to a signature field on a qualification record. Mirrors the spec's
/// <c>FormSubmissionRequestSignatureValueObjectRequestBody</c>.
/// </summary>
/// <remarks>
/// The shared <c>FormSubmissionRequest*</c> family lives in
/// <c>Samsara.Sdk.Models.Documents</c>, but the signature member of that family has
/// no record there yet. It is modelled here under a qualification-scoped name so
/// this file owns it outright; if the Documents domain later adds
/// <c>FormSubmissionRequestSignatureValue</c>, this record can forward to it.
/// </remarks>
public sealed record QualificationSignatureValueInput
{
    /// <summary>The signature media to upload. Spec marks REQUIRED.</summary>
    [JsonPropertyName("media")]
    public required FormSubmissionRequestMediaItem Media { get; init; }
}
