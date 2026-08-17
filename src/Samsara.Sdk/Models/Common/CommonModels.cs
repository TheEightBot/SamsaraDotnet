namespace Samsara.Sdk.Models.Common;

using System.Text.Json.Serialization;

/// <summary>
/// A lightweight reference to a Samsara tag.
/// </summary>
public sealed record TagReference
{
    /// <summary>
    /// Samsara ID of the tag. Nullable: the spec lists <c>id</c> as optional on
    /// the tag references reached from <c>Address.tags</c>, and deserialization
    /// relaxes <c>required</c>, so a non-nullable property would silently hold
    /// null when the API omits it.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("parentTagId")]
    public string? ParentTagId { get; init; }
}

/// <summary>
/// A lightweight <c>{ id, name }</c> reference, matching the Samsara spec's various
/// <c>*TinyResponse</c> schemas (driver, vehicle, etc.).
/// </summary>
public sealed record EntityReference
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// External identifiers for the referenced object. Present on the
    /// <c>*TinyResponse</c> variants that carry an external-ID map (e.g. the
    /// vehicle reference on an IFTA vehicle report).
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// Given name of the referenced person. Present on the contact-shaped
    /// variants (e.g. <c>Address.contacts</c>).
    /// </summary>
    [JsonPropertyName("firstName")]
    public string? FirstName { get; init; }

    /// <summary>
    /// Family name of the referenced person. Present on the contact-shaped
    /// variants (e.g. <c>Address.contacts</c>).
    /// </summary>
    [JsonPropertyName("lastName")]
    public string? LastName { get; init; }
}

/// <summary>
/// A minified custom attribute attached to a Samsara entity. Mirrors the spec's
/// <c>attributeTiny</c> schema and its structurally identical siblings
/// <c>GoaAttributeTiny</c>, <c>GoaAttributeTinyRequestBody</c> and
/// <c>GoaAttributeTinyResponseBody</c>.
/// </summary>
/// <remarks>
/// <para>
/// The request and response variants of this schema are byte-identical (same
/// five properties, none required), so a single record serves both directions.
/// </para>
/// <para>
/// This record lives in the Common namespace because the identical schema is
/// reached from four separate domains — vehicles (<c>GET /fleet/vehicles</c>,
/// <c>GET|PATCH /fleet/vehicles/{id}</c>), assets (<c>GET|POST|PATCH /assets</c>),
/// trailers (<c>GET|POST|PATCH /fleet/trailers</c>) and equipment
/// (<c>PATCH /beta/fleet/equipment/{id}</c>) — so no single domain namespace can
/// own it without becoming a de-facto shared type.
/// </para>
/// </remarks>
public sealed record AttributeTiny
{
    /// <summary>The Samsara ID of the attribute object.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the attribute.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>String values associated with this attribute.</summary>
    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }

    /// <summary>Number values associated with this attribute.</summary>
    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    /// <summary>
    /// Date values associated with this attribute (RFC 3339 full-date format:
    /// <c>YYYY-MM-DD</c>).
    /// </summary>
    [JsonPropertyName("dateValues")]
    public IReadOnlyList<string>? DateValues { get; init; }
}

/// <summary>
/// An external ID associated with a Samsara object.
/// </summary>
public sealed record ExternalId
{
    [JsonPropertyName("externalId")]
    public required string Value { get; init; }

    [JsonPropertyName("integrationId")]
    public string? IntegrationId { get; init; }
}

/// <summary>
/// A wrapper for standard Samsara API responses that embed data with a "data" key.
/// </summary>
/// <typeparam name="T">The type of the response payload.</typeparam>
public sealed record SamsaraResponse<T>
{
    [JsonPropertyName("data")]
    public required T Data { get; init; }
}

/// <summary>
/// A wrapper for standard Samsara list responses.
/// </summary>
/// <typeparam name="T">The type of items in the list.</typeparam>
public sealed record SamsaraListResponse<T>
{
    [JsonPropertyName("data")]
    public required IReadOnlyList<T> Data { get; init; }

    [JsonPropertyName("pagination")]
    public Samsara.Sdk.Pagination.PaginationInfo? Pagination { get; init; }
}

/// <summary>
/// A wrapper for paginated Samsara responses whose <c>data</c> is a single
/// object that itself wraps the page's items (e.g. <c>{ "data": { "media":
/// [...] }, "pagination": {...} }</c>), rather than the more common
/// <c>{ "data": [...], "pagination": {...} }</c> handled by
/// <see cref="SamsaraListResponse{T}"/>.
/// </summary>
/// <typeparam name="TData">The type of the inner <c>data</c> object.</typeparam>
public sealed record SamsaraNestedListResponse<TData>
{
    [JsonPropertyName("data")]
    public required TData Data { get; init; }

    [JsonPropertyName("pagination")]
    public Samsara.Sdk.Pagination.PaginationInfo? Pagination { get; init; }
}
