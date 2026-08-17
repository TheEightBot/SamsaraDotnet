namespace Samsara.Sdk.Models.Documents;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a document in the Samsara system.
/// </summary>
public sealed record Document
{
    /// <summary>Universally unique identifier for the document.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the document.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Document type metadata (minified).</summary>
    [JsonPropertyName("documentType")]
    public required DocumentTypeRef DocumentType { get; init; }

    /// <summary>Driver associated with the document (minified).</summary>
    [JsonPropertyName("driver")]
    public required DriverRef Driver { get; init; }

    /// <summary>Vehicle associated with the document (minified), when applicable.</summary>
    [JsonPropertyName("vehicle")]
    public VehicleRef? Vehicle { get; init; }

    /// <summary>Route associated with the document (minified), when applicable.</summary>
    [JsonPropertyName("route")]
    public RouteRef? Route { get; init; }

    /// <summary>Route stop associated with the document (minified), when applicable.</summary>
    [JsonPropertyName("routeStop")]
    public RouteStopRef? RouteStop { get; init; }

    /// <summary>
    /// Condition of the document — <c>required</c>, <c>submitted</c>, or <c>archived</c>.
    /// </summary>
    [JsonPropertyName("state")]
    public required string State { get; init; }

    /// <summary>Notes on the document.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>The fields associated with this document.</summary>
    [JsonPropertyName("fields")]
    public required IReadOnlyList<DocumentField> Fields { get; init; }

    /// <summary>List of conditional field sections.</summary>
    [JsonPropertyName("conditionalFieldSections")]
    public IReadOnlyList<ConditionalFieldSection>? ConditionalFieldSections { get; init; }

    /// <summary>Time the document was created (RFC 3339).</summary>
    [JsonPropertyName("createdAtTime")]
    public required DateTimeOffset CreatedAtTime { get; init; }

    /// <summary>Time the document was last updated (RFC 3339).</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>A minified document type reference returned alongside a <see cref="Document"/>.</summary>
public sealed record DocumentTypeRef
{
    /// <summary>ID of the document type.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the document type.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>A minified driver reference returned alongside a <see cref="Document"/>.</summary>
public sealed record DriverRef
{
    /// <summary>ID of the driver.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External IDs for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>A minified vehicle reference returned alongside a <see cref="Document"/>.</summary>
public sealed record VehicleRef
{
    /// <summary>ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External IDs for the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>A minified route reference returned alongside a <see cref="Document"/>.</summary>
public sealed record RouteRef
{
    /// <summary>ID of the route.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the route.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External IDs for the route.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>A minified route stop reference returned alongside a <see cref="Document"/>.</summary>
public sealed record RouteStopRef
{
    /// <summary>ID of the route stop.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the route stop.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External IDs for the route stop.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A field section whose visibility depends on the value of an earlier multiple-choice field.
/// </summary>
public sealed record ConditionalFieldSection
{
    /// <summary>The index of the multiple-choice field that triggers this section.</summary>
    [JsonPropertyName("triggeringFieldIndex")]
    public long? TriggeringFieldIndex { get; init; }

    /// <summary>The option value that activates this conditional section.</summary>
    [JsonPropertyName("triggeringFieldValue")]
    public string? TriggeringFieldValue { get; init; }

    /// <summary>The index of the first conditional field in the section.</summary>
    [JsonPropertyName("conditionalFieldFirstIndex")]
    public long? ConditionalFieldFirstIndex { get; init; }

    /// <summary>The index of the last conditional field in the section.</summary>
    [JsonPropertyName("conditionalFieldLastIndex")]
    public long? ConditionalFieldLastIndex { get; init; }
}

/// <summary>
/// A field within a document, as returned on <see cref="Document"/>. Mirrors the
/// response half of the spec's document-field schema
/// (<c>GET /fleet/documents</c> → <c>data.fields</c>).
/// </summary>
/// <remarks>
/// Spec marks <c>label</c>, <c>type</c> and <c>value</c> REQUIRED on the
/// response, but response properties stay nullable here — the live API omits
/// fields its own spec marks required and the SDK deserializes leniently. The
/// request half, where <c>required</c> IS correct, lives on
/// <see cref="DocumentFieldInput"/>.
/// </remarks>
public sealed record DocumentField
{
    /// <summary>The name of the field. Spec marks REQUIRED on the response.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// The type of field: <c>photo</c>, <c>string</c>, <c>number</c>,
    /// <c>multipleChoice</c>, <c>signature</c>, <c>dateTime</c>,
    /// <c>scannedDocument</c> or <c>barcode</c>. Spec marks REQUIRED on the response.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// The value of the document field; its shape depends on
    /// <see cref="Type"/>. Spec marks REQUIRED on the response.
    /// </summary>
    [JsonPropertyName("value")]
    public object? Value { get; init; }

    [JsonPropertyName("photoValue")]
    public IReadOnlyList<DocumentPhoto>? PhotoValue { get; init; }

    [JsonPropertyName("stringValue")]
    public string? StringValue { get; init; }

    [JsonPropertyName("numberValue")]
    public double? NumberValue { get; init; }
}

/// <summary>
/// A field posted as part of <see cref="CreateDocumentRequest"/>. Mirrors the
/// spec's <c>DocumentFieldRequestBody</c> (<c>POST /fleet/documents</c> →
/// <c>fields</c>).
/// </summary>
/// <remarks>
/// Split from the response-side <see cref="DocumentField"/> during the
/// 2026-08-17 spec-parity sweep: the spec requires <c>label</c> and <c>type</c>
/// on the request but also requires <c>value</c> on the response, and
/// <c>required</c> is only ever correct on a request DTO. Same precedent as
/// <c>ServiceTaskInstanceInput</c> / <c>PartInstanceInput</c>.
/// </remarks>
public sealed record DocumentFieldInput
{
    /// <summary>The name of the field. Spec marks REQUIRED on the request.</summary>
    [JsonPropertyName("label")]
    public required string Label { get; init; }

    /// <summary>
    /// The type of field: <c>photo</c>, <c>string</c>, <c>number</c>,
    /// <c>multipleChoice</c>, <c>signature</c>, <c>dateTime</c>,
    /// <c>scannedDocument</c> or <c>barcode</c>. Spec marks REQUIRED on the request.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>
    /// The value of the document field; its shape depends on <see cref="Type"/>.
    /// Optional on the request.
    /// </summary>
    [JsonPropertyName("value")]
    public object? Value { get; init; }
}

/// <summary>
/// A photo attached to a document field.
/// </summary>
public sealed record DocumentPhoto
{
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}

/// <summary>
/// Represents a document type / template.
/// </summary>
public sealed record DocumentType
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("fieldTypes")]
    public IReadOnlyList<DocumentFieldType>? FieldTypes { get; init; }

    /// <summary>List of conditional field sections for this document type.</summary>
    [JsonPropertyName("conditionalFieldSections")]
    public IReadOnlyList<ConditionalFieldSection>? ConditionalFieldSections { get; init; }

    [JsonPropertyName("orgId")]
    public long? OrgId { get; init; }
}

/// <summary>
/// A field type within a document type. Mirrors the spec's document-type field
/// schema (<c>GET /fleet/document-types</c> → <c>data.fieldTypes</c>).
/// </summary>
/// <remarks>
/// The 2026-08-17 spec-parity sweep found this record modelled a shape the API
/// never sends: <c>valueType</c> and <c>numberValueTypeMetadata</c> appear
/// nowhere in the spec. The spec's names are <c>fieldType</c> and
/// <c>numberFieldTypeMetaData</c>, and <c>requiredField</c> was missing entirely.
/// Spec marks <c>fieldType</c>, <c>label</c> and <c>requiredField</c> REQUIRED;
/// they stay nullable because this is a response record.
/// </remarks>
public sealed record DocumentFieldType
{
    /// <summary>The name of the field type. Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// The type of value this field can have: <c>photo</c>, <c>string</c>,
    /// <c>number</c>, <c>multipleChoice</c>, <c>signature</c>, <c>dateTime</c>,
    /// <c>scannedDocument</c> or <c>barcode</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("fieldType")]
    public string? FieldType { get; init; }

    /// <summary>
    /// Indicates whether the field is required on documents of this type.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("requiredField")]
    public bool? RequiredField { get; init; }

    /// <summary>The number field metadata, present for <c>number</c> field types.</summary>
    [JsonPropertyName("numberFieldTypeMetaData")]
    public NumberFieldTypeMetaData? NumberFieldTypeMetaData { get; init; }

    /// <summary>The multiple choice option labels, present for <c>multipleChoice</c> field types.</summary>
    [JsonPropertyName("multipleChoiceFieldTypeMetaData")]
    public IReadOnlyList<MultipleChoiceFieldTypeMetaData>? MultipleChoiceFieldTypeMetaData { get; init; }

    /// <summary>The signature field metadata, present for <c>signature</c> field types.</summary>
    [JsonPropertyName("signatureFieldTypeMetaData")]
    public SignatureFieldTypeMetaData? SignatureFieldTypeMetaData { get; init; }
}

/// <summary>
/// Metadata for a <c>number</c> document-type field. Mirrors the spec's
/// <c>numberFieldTypeMetaData</c> object.
/// </summary>
public sealed record NumberFieldTypeMetaData
{
    /// <summary>Number of decimal places the field accepts.</summary>
    [JsonPropertyName("numberOfDecimalPlaces")]
    public long? NumberOfDecimalPlaces { get; init; }
}

/// <summary>
/// One multiple-choice option on a document-type field. Mirrors an item of the
/// spec's <c>multipleChoiceFieldTypeMetaData</c> array.
/// </summary>
public sealed record MultipleChoiceFieldTypeMetaData
{
    /// <summary>Label of the multiple choice option.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }
}

/// <summary>
/// Metadata for a <c>signature</c> document-type field. Mirrors the spec's
/// <c>signatureFieldTypeMetaData</c> object.
/// </summary>
public sealed record SignatureFieldTypeMetaData
{
    /// <summary>Legal text displayed above the signature field.</summary>
    [JsonPropertyName("legalText")]
    public string? LegalText { get; init; }
}

/// <summary>
/// Request body for creating a document.
/// </summary>
public sealed record CreateDocumentRequest
{
    /// <summary>ID of the document type.</summary>
    [JsonPropertyName("documentTypeId")]
    public required string DocumentTypeId { get; init; }

    /// <summary>ID of the driver (Samsara ID or external ID).</summary>
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    /// <summary>Name of the document.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>ID of the vehicle (Samsara ID or external ID), when applicable.</summary>
    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    /// <summary>ID of the route stop (Samsara ID or external ID), when applicable.</summary>
    [JsonPropertyName("routeStopId")]
    public string? RouteStopId { get; init; }

    /// <summary>
    /// Document state — <c>submitted</c> or <c>required</c>. Defaults to <c>required</c> server-side.
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>The fields associated with this document.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<DocumentFieldInput>? Fields { get; init; }

    /// <summary>Notes on the document.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }
}

/// <summary>Represents a document PDF generation job.</summary>
public sealed record DocumentPdfJob
{
    /// <summary>ID of the PDF file generated (or being generated) for the document.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>ID of the source document.</summary>
    [JsonPropertyName("documentId")]
    public string? DocumentId { get; init; }

    // The fields below are returned only by GET /fleet/documents/pdfs/{id}
    // (DocumentPdfQueryResponse_data), which shares this record; the POST
    // /fleet/documents/pdfs create response carries only id + documentId.

    /// <summary>Status of the PDF generation job (<c>requested</c>, <c>processing</c>, <c>completed</c>).</summary>
    [JsonPropertyName("jobStatus")]
    public string? JobStatus { get; init; }

    /// <summary>Time PDF generation was requested (RFC 3339).</summary>
    [JsonPropertyName("requestedAtTime")]
    public string? RequestedAtTime { get; init; }

    /// <summary>Time PDF generation completed (RFC 3339), once the job is done.</summary>
    [JsonPropertyName("completedAtTime")]
    public string? CompletedAtTime { get; init; }

    /// <summary>S3 pre-signed URL to download the generated PDF.</summary>
    [JsonPropertyName("downloadDocumentPdfUrl")]
    public string? DownloadDocumentPdfUrl { get; init; }
}

/// <summary>Request body for generating a document PDF.</summary>
public sealed record GenerateDocumentPdfRequest
{
    [JsonPropertyName("documentId")] public required string DocumentId { get; init; }
}
