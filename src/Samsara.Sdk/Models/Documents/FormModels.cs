namespace Samsara.Sdk.Models.Documents;

using System.Text.Json.Serialization;

public sealed record FormTemplate
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("revision")]
    public int? Revision { get; init; }

    [JsonPropertyName("sections")]
    public IReadOnlyList<FormSection>? Sections { get; init; }

    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
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

public sealed record FormSubmission
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("formTemplateId")]
    public string? FormTemplateId { get; init; }

    [JsonPropertyName("formTemplateName")]
    public string? FormTemplateName { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("submittedAtTime")]
    public string? SubmittedAtTime { get; init; }

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
