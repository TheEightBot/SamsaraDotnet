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
/// A field within a document.
/// </summary>
public sealed record DocumentField
{
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

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
/// A field type within a document type.
/// </summary>
public sealed record DocumentFieldType
{
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("valueType")]
    public string? ValueType { get; init; }

    [JsonPropertyName("numberValueTypeMetadata")]
    public NumberValueTypeMetadata? NumberValueTypeMetadata { get; init; }
}

/// <summary>
/// Metadata for a number-type document field.
/// </summary>
public sealed record NumberValueTypeMetadata
{
    [JsonPropertyName("numDecimalPlaces")]
    public int? NumDecimalPlaces { get; init; }
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
    public IReadOnlyList<DocumentField>? Fields { get; init; }

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
