namespace Samsara.Sdk.Models.Documents;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a document in the Samsara system.
/// </summary>
public sealed record Document
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("documentTypeId")]
    public string? DocumentTypeId { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("fields")]
    public IReadOnlyList<DocumentField>? Fields { get; init; }

    [JsonPropertyName("createdAtMs")]
    public long? CreatedAtMs { get; init; }

    [JsonPropertyName("updatedAtMs")]
    public long? UpdatedAtMs { get; init; }
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
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("fieldTypes")]
    public IReadOnlyList<DocumentFieldType>? FieldTypes { get; init; }

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
    [JsonPropertyName("documentTypeId")]
    public required string DocumentTypeId { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("fields")]
    public IReadOnlyList<DocumentField>? Fields { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }
}
