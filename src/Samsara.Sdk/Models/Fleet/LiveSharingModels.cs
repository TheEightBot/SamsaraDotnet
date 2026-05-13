namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

/// <summary>Represents a live sharing link.</summary>
public sealed record LiveSharingLink
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("url")] public string? Url { get; init; }
    [JsonPropertyName("expiresAt")] public DateTimeOffset? ExpiresAt { get; init; }
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("entityId")] public string? EntityId { get; init; }
    [JsonPropertyName("entityType")] public string? EntityType { get; init; }
}

/// <summary>Request body for creating a live sharing link.</summary>
public sealed record CreateLiveSharingLinkRequest
{
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("type")] public required string Type { get; init; }
    [JsonPropertyName("entityId")] public required string EntityId { get; init; }
    [JsonPropertyName("expiresAt")] public DateTimeOffset? ExpiresAt { get; init; }
}

/// <summary>Request body for updating a live sharing link.</summary>
public sealed record UpdateLiveSharingLinkRequest
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("expiresAt")] public DateTimeOffset? ExpiresAt { get; init; }
}
