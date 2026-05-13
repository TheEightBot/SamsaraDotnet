namespace Samsara.Sdk.Models.Industrial;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>Represents a reading definition.</summary>
public sealed record ReadingDefinition
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("dataType")] public string? DataType { get; init; }
    [JsonPropertyName("units")] public string? Units { get; init; }
    [JsonPropertyName("entityType")] public string? EntityType { get; init; }
}

/// <summary>Represents a historical reading value.</summary>
public sealed record ReadingHistory
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("entityId")] public string? EntityId { get; init; }
    [JsonPropertyName("value")] public JsonElement? Value { get; init; }
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
}

/// <summary>Represents a snapshot reading value.</summary>
public sealed record ReadingSnapshot
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("entityId")] public string? EntityId { get; init; }
    [JsonPropertyName("entityName")] public string? EntityName { get; init; }
    [JsonPropertyName("value")] public JsonElement? Value { get; init; }
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
}
