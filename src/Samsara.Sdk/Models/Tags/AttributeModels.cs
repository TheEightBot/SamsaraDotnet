namespace Samsara.Sdk.Models.Tags;

using System.Text.Json.Serialization;

public sealed record AttributeDefinition
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

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

    /// <summary>Unit of the attribute (only set for <c>number</c> attribute types).</summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }

    /// <summary>Representation of attribute values that includes value ids
    /// (<c>attributeValueTiny</c> in the spec). <c>null</c> for <c>text</c> and
    /// <c>freeform-multi-select</c> attribute types.</summary>
    [JsonPropertyName("values")]
    public IReadOnlyList<object>? Values { get; init; }

    /// <summary>Entities that this attribute is applied onto. Present (non-null)
    /// on expanded responses (<c>GET /attributes/{id}</c>, <c>POST /attributes</c>,
    /// <c>PATCH /attributes/{id}</c>); omitted on the <c>GET /attributes</c> list
    /// response, where consumers should treat it as empty.</summary>
    [JsonPropertyName("entities")]
    public IReadOnlyList<AttributeEntity> Entities { get; init; } = Array.Empty<AttributeEntity>();
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

    /// <summary>Entities that will be applied to this attribute
    /// (spec inner schema: <c>CreateAttributeRequest_entities</c>).</summary>
    [JsonPropertyName("entities")]
    public IReadOnlyList<object>? Entities { get; init; }

    /// <summary>Unit of the attribute (only for <c>number</c> attribute types).
    /// Defaults to <c>NO_UNIT</c> server-side when omitted.</summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
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

    /// <summary>Entities that will be applied to this attribute
    /// (spec inner schema: <c>CreateAttributeRequest_entities</c>).</summary>
    [JsonPropertyName("entities")]
    public IReadOnlyList<object>? Entities { get; init; }
}
