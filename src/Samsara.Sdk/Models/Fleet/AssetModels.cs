namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json;
using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>Represents a Samsara asset.</summary>
public sealed record Asset
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("make")] public string? Make { get; init; }
    [JsonPropertyName("model")] public string? Model { get; init; }
    [JsonPropertyName("year")] public int? Year { get; init; }
    [JsonPropertyName("licensePlate")] public string? LicensePlate { get; init; }
    [JsonPropertyName("serialNumber")] public string? SerialNumber { get; init; }
    [JsonPropertyName("vin")] public string? Vin { get; init; }
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
    [JsonPropertyName("tags")] public IReadOnlyList<TagReference>? Tags { get; init; }
    /// <summary>List of attributes associated with the asset (raw JSON; shape per spec
    /// <c>GoaAttributeTinyResponseBody</c>).</summary>
    [JsonPropertyName("attributes")] public IReadOnlyList<JsonElement>? Attributes { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
    [JsonPropertyName("readingsIngestionEnabled")] public bool? ReadingsIngestionEnabled { get; init; }
    [JsonPropertyName("regulationMode")] public string? RegulationMode { get; init; }
    /// <summary>Time the asset was created (RFC 3339). Spec marks REQUIRED on the
    /// response.</summary>
    [JsonPropertyName("createdAtTime")] public DateTimeOffset CreatedAtTime { get; init; }
    /// <summary>Time the asset was last updated (RFC 3339). Spec marks REQUIRED on
    /// the response.</summary>
    [JsonPropertyName("updatedAtTime")] public DateTimeOffset UpdatedAtTime { get; init; }
}

/// <summary>Request body for creating an asset.</summary>
public sealed record CreateAssetRequest
{
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("make")] public string? Make { get; init; }
    [JsonPropertyName("model")] public string? Model { get; init; }
    [JsonPropertyName("year")] public int? Year { get; init; }
    [JsonPropertyName("licensePlate")] public string? LicensePlate { get; init; }
    [JsonPropertyName("serialNumber")] public string? SerialNumber { get; init; }
    [JsonPropertyName("vin")] public string? Vin { get; init; }
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
    [JsonPropertyName("tagIds")] public IReadOnlyList<string>? TagIds { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
    [JsonPropertyName("readingsIngestionEnabled")] public bool? ReadingsIngestionEnabled { get; init; }
    [JsonPropertyName("regulationMode")] public string? RegulationMode { get; init; }
    [JsonPropertyName("attributes")] public IReadOnlyList<System.Text.Json.JsonElement>? Attributes { get; init; }
}

/// <summary>Request body for updating an asset.</summary>
public sealed record UpdateAssetRequest
{
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("make")] public string? Make { get; init; }
    [JsonPropertyName("model")] public string? Model { get; init; }
    [JsonPropertyName("year")] public int? Year { get; init; }
    [JsonPropertyName("licensePlate")] public string? LicensePlate { get; init; }
    [JsonPropertyName("serialNumber")] public string? SerialNumber { get; init; }
    [JsonPropertyName("vin")] public string? Vin { get; init; }
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
    [JsonPropertyName("readingsIngestionEnabled")] public bool? ReadingsIngestionEnabled { get; init; }
    [JsonPropertyName("regulationMode")] public string? RegulationMode { get; init; }
}

/// <summary>Request body for deleting assets.</summary>
public sealed record DeleteAssetsRequest
{
    [JsonPropertyName("ids")] public required IReadOnlyList<string> Ids { get; init; }
}

/// <summary>Asset location and speed snapshot.</summary>
public sealed record AssetLocationAndSpeed
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("location")] public AssetLocation? Location { get; init; }
    [JsonPropertyName("speed")] public double? Speed { get; init; }
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
}

/// <summary>Asset location details.</summary>
public sealed record AssetLocation
{
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }
    [JsonPropertyName("heading")] public double? Heading { get; init; }
    [JsonPropertyName("reverseGeo")] public ReverseGeo? ReverseGeo { get; init; }
}
