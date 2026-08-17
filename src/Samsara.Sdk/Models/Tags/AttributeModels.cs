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
    public IReadOnlyList<AttributeValueTiny>? Values { get; init; }

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

    /// <summary>Date values associated with this attribute on this entity
    /// (RFC 3339 full-date format: <c>YYYY-MM-DD</c>).</summary>
    [JsonPropertyName("dateValues")]
    public IReadOnlyList<string>? DateValues { get; init; }

    /// <summary>Spec also returns a generic <c>values</c> array alongside the typed
    /// <c>numberValues</c>/<c>stringValues</c> lists
    /// (<c>attributeValueTiny</c> in the spec).</summary>
    [JsonPropertyName("values")]
    public IReadOnlyList<AttributeValueTiny>? Values { get; init; }
}

/// <summary>
/// A minified attribute value carrying its Samsara value id alongside the
/// human-readable string. Mirrors the spec's <c>attributeValueTiny</c> schema.
/// </summary>
public sealed record AttributeValueTiny
{
    /// <summary>The Samsara ID of this value object.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The human-readable string for this value.</summary>
    [JsonPropertyName("stringValue")]
    public string? StringValue { get; init; }
}

/// <summary>
/// An entity that an attribute is applied to, as supplied on the create and
/// update attribute requests. Mirrors the spec's
/// <c>CreateAttributeRequest_entities</c> schema (shared by
/// <c>POST /attributes</c> and <c>PATCH /attributes/{id}</c>).
/// </summary>
/// <remarks>
/// Kept distinct from the response-side <see cref="AttributeEntity"/>: the
/// request schema types <c>entityId</c> as a string and carries neither
/// <c>name</c> nor <c>values</c>, so the two shapes cannot be merged. The
/// <c>Input</c> suffix follows the existing <c>ServiceTaskInstanceInput</c> /
/// <c>UsDriverRulesetOverrideInput</c> precedent, since the stripped spec name
/// would not be a usable identifier.
/// </remarks>
public sealed record AttributeEntityInput
{
    /// <summary>Entity id, interpreted according to the attribute's entity type.</summary>
    [JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    /// <summary>The external IDs for the given object.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Number values to associate with this attribute on this entity.</summary>
    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    /// <summary>String values to associate with this attribute on this entity.</summary>
    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }

    /// <summary>Date values to associate with this attribute on this entity
    /// (RFC 3339 full-date format: <c>YYYY-MM-DD</c>).</summary>
    [JsonPropertyName("dateValues")]
    public IReadOnlyList<string>? DateValues { get; init; }
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
    public IReadOnlyList<AttributeEntityInput>? Entities { get; init; }

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
    public IReadOnlyList<AttributeEntityInput>? Entities { get; init; }
}
