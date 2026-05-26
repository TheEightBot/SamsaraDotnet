namespace Samsara.Sdk.Models.Tags;

using System.Text.Json.Serialization;

public sealed record AttributeDefinition
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("entityType")]
    public string? EntityType { get; init; }

    [JsonPropertyName("attributeType")]
    public string? AttributeType { get; init; }

    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }

    [JsonPropertyName("entities")]
    public IReadOnlyList<AttributeEntity>? Entities { get; init; }
}

public sealed record AttributeEntity
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }

    /// <summary>Spec also returns a generic <c>values</c> array alongside the typed
    /// <c>numberValues</c>/<c>stringValues</c> lists.</summary>
    [JsonPropertyName("values")]
    public IReadOnlyList<object>? Values { get; init; }
}

public sealed record CreateAttributeRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("entityType")]
    public required string EntityType { get; init; }

    [JsonPropertyName("attributeType")]
    public required string AttributeType { get; init; }

    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }
}

public sealed record UpdateAttributeRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("entityType")]
    public required string EntityType { get; init; }

    [JsonPropertyName("attributeType")]
    public string? AttributeType { get; init; }

    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }
}
