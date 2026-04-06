namespace Samsara.Sdk.Models.Tags;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a Samsara tag used for grouping and organizing resources.
/// </summary>
public sealed record Tag
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("parentTagId")]
    public string? ParentTagId { get; init; }

    [JsonPropertyName("addresses")]
    public IReadOnlyList<TaggedResource>? Addresses { get; init; }

    [JsonPropertyName("assets")]
    public IReadOnlyList<TaggedResource>? Assets { get; init; }

    [JsonPropertyName("drivers")]
    public IReadOnlyList<TaggedResource>? Drivers { get; init; }

    [JsonPropertyName("machines")]
    public IReadOnlyList<TaggedResource>? Machines { get; init; }

    [JsonPropertyName("sensors")]
    public IReadOnlyList<TaggedResource>? Sensors { get; init; }

    [JsonPropertyName("vehicles")]
    public IReadOnlyList<TaggedResource>? Vehicles { get; init; }
}

/// <summary>
/// A lightweight reference to a resource associated with a tag.
/// </summary>
public sealed record TaggedResource
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Request body for creating a new tag.
/// </summary>
public sealed record CreateTagRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("parentTagId")]
    public string? ParentTagId { get; init; }

    [JsonPropertyName("addresses")]
    public IReadOnlyList<TaggedResourceId>? Addresses { get; init; }

    [JsonPropertyName("drivers")]
    public IReadOnlyList<TaggedResourceId>? Drivers { get; init; }

    [JsonPropertyName("vehicles")]
    public IReadOnlyList<TaggedResourceId>? Vehicles { get; init; }
}

/// <summary>
/// Request body for updating a tag (PATCH).
/// </summary>
public sealed record UpdateTagRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("parentTagId")]
    public string? ParentTagId { get; init; }

    [JsonPropertyName("addresses")]
    public IReadOnlyList<TaggedResourceId>? Addresses { get; init; }

    [JsonPropertyName("drivers")]
    public IReadOnlyList<TaggedResourceId>? Drivers { get; init; }

    [JsonPropertyName("vehicles")]
    public IReadOnlyList<TaggedResourceId>? Vehicles { get; init; }
}

/// <summary>
/// A resource ID reference for tag assignment operations.
/// </summary>
public sealed record TaggedResourceId
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}
