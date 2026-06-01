namespace Samsara.Sdk.Models.Common;

using System.Text.Json.Serialization;

/// <summary>
/// A lightweight reference to a Samsara tag.
/// </summary>
public sealed record TagReference
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

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
