namespace Samsara.Sdk.Models.Industrial;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>Definition of a reading exposed by <c>GET /readings/definitions</c>.
/// Mirrors the spec's <c>ReadingDefinitionResponseBody</c>.</summary>
public sealed record ReadingDefinition
{
    /// <summary>The category enumeration that this reading belongs to. Spec
    /// marks REQUIRED on the response.</summary>
    [JsonPropertyName("category")] public required string Category { get; init; }

    /// <summary>The human readable description for this reading (translated to
    /// English). Spec marks REQUIRED on the response.</summary>
    [JsonPropertyName("description")] public required string Description { get; init; }

    /// <summary>Entity type of this reading (e.g. <c>asset</c>, <c>sensor</c>).
    /// Spec marks REQUIRED on the response.</summary>
    [JsonPropertyName("entityType")] public required string EntityType { get; init; }

    /// <summary>Array of enumeration values when the reading is of an enum
    /// type.</summary>
    [JsonPropertyName("enumValues")] public IReadOnlyList<EnumValue>? EnumValues { get; init; }

    /// <summary>Whether this reading can be ingested using the API. Spec marks
    /// REQUIRED on the response.</summary>
    [JsonPropertyName("ingestionEnabled")] public required bool IngestionEnabled { get; init; }

    /// <summary>The label for this reading that is suitable to show to a user
    /// (translated to English). Spec marks REQUIRED on the response.</summary>
    [JsonPropertyName("label")] public required string Label { get; init; }

    /// <summary>The ID of the reading used to fetch time series data in other
    /// endpoints. Spec marks REQUIRED on the response.</summary>
    [JsonPropertyName("readingId")] public required string ReadingId { get; init; }

    /// <summary>The type information for the reading. Contains the complete
    /// type structure including <c>dataType</c>, <c>unit</c>, <c>enumValues</c>,
    /// <c>fields</c>, etc. Spec marks REQUIRED on the response.</summary>
    [JsonPropertyName("type")] public required JsonElement Type { get; init; }

    /// <summary>Reading id. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should prefer
    /// <see cref="ReadingId"/>.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Reading name. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should prefer
    /// <see cref="Label"/>.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }

    /// <summary>Reading data type. Not part of the spec inner schema; retained
    /// as a nullable back-compat convenience — callers should inspect
    /// <see cref="Type"/> for the canonical data-type payload.</summary>
    [JsonPropertyName("dataType")] public string? DataType { get; init; }

    /// <summary>Reading units. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should inspect
    /// <see cref="Type"/> for the canonical unit payload.</summary>
    [JsonPropertyName("units")] public string? Units { get; init; }
}

/// <summary>Enumeration value attached to a <see cref="ReadingDefinition"/>.
/// Mirrors the spec's <c>EnumValueResponseBody</c>.</summary>
public sealed record EnumValue
{
    /// <summary>The label for this enum value that is suitable to show to a
    /// user (translated to English). Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")] public required string Label { get; init; }

    /// <summary>The symbol that can be used to represent this enumeration
    /// value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("symbol")] public required string Symbol { get; init; }
}

/// <summary>A history of reading values for an entity returned by
/// <c>GET /readings/history</c>. Mirrors the spec's
/// <c>ReadingHistoryResponseBody</c>.</summary>
public sealed record ReadingHistory
{
    /// <summary>The ID of the entity this reading is for. Spec marks REQUIRED
    /// on the response.</summary>
    [JsonPropertyName("entityId")] public required string EntityId { get; init; }

    /// <summary>A map of external ids associated with the entity.</summary>
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>The time in RFC 3339 format when the reading was measured.</summary>
    [JsonPropertyName("happenedAtTime")] public DateTimeOffset? HappenedAtTime { get; init; }

    /// <summary>The value of the reading.</summary>
    [JsonPropertyName("value")] public JsonElement? Value { get; init; }

    /// <summary>Reading id. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — the canonical reading identifier is
    /// supplied via the <c>readingId</c> query parameter on the request.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Event time. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should prefer
    /// <see cref="HappenedAtTime"/>.</summary>
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
}

/// <summary>A snapshot of a reading value at a point in time returned by
/// <c>GET /readings/latest</c>. Mirrors the spec's
/// <c>ReadingSnapshotResponseBody</c>.</summary>
public sealed record ReadingSnapshot
{
    /// <summary>The ID of the entity this reading is for. Spec marks REQUIRED
    /// on the response.</summary>
    [JsonPropertyName("entityId")] public required string EntityId { get; init; }

    /// <summary>A map of external ids associated with the entity.</summary>
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>The time in RFC 3339 format when the reading was measured.</summary>
    [JsonPropertyName("happenedAtTime")] public DateTimeOffset? HappenedAtTime { get; init; }

    /// <summary>The ID of the reading for which the data is being returned.
    /// Spec marks REQUIRED on the response.</summary>
    [JsonPropertyName("readingId")] public required string ReadingId { get; init; }

    /// <summary>The value of the reading.</summary>
    [JsonPropertyName("value")] public JsonElement? Value { get; init; }

    /// <summary>Reading id. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should prefer
    /// <see cref="ReadingId"/>.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Entity name. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should look up the entity
    /// name via the corresponding entity endpoint.</summary>
    [JsonPropertyName("entityName")] public string? EntityName { get; init; }

    /// <summary>Event time. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should prefer
    /// <see cref="HappenedAtTime"/>.</summary>
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
}
