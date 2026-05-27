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

/// <summary>Asset location and speed snapshot from the
/// <c>GET /assets/location-and-speed/stream</c> endpoint.</summary>
public sealed record AssetLocationAndSpeed
{
    /// <summary>Asset that the location readings are tied to. Spec marks
    /// REQUIRED on the inner response schema (mirrors
    /// <c>AssetResponseResponseBody</c>).</summary>
    [JsonPropertyName("asset")] public required AssetLocationAndSpeedAsset Asset { get; init; }

    /// <summary>UTC timestamp in RFC 3339 format of the event. Spec marks
    /// REQUIRED on the response.</summary>
    [JsonPropertyName("happenedAtTime")] public required DateTimeOffset HappenedAtTime { get; init; }

    /// <summary>Location object. Spec marks REQUIRED on the response.</summary>
    [JsonPropertyName("location")] public required AssetLocation Location { get; init; }

    /// <summary>Speed object (optional in the spec — present only when
    /// <c>includeSpeed=true</c> is passed on the request).</summary>
    [JsonPropertyName("speed")] public AssetLocationAndSpeedSpeed? Speed { get; init; }

    /// <summary>Asset id, hoisted from <see cref="Asset"/>. Not part of the
    /// spec inner schema; retained as a nullable back-compat convenience —
    /// callers should prefer <c>Asset.Id</c>.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Asset name. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should look up the name via
    /// the <c>Assets</c> endpoint.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }

    /// <summary>Event time. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — callers should prefer
    /// <see cref="HappenedAtTime"/>.</summary>
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
}

/// <summary>Minified asset reference attached to a location-and-speed
/// reading. Mirrors the spec's <c>AssetResponseResponseBody</c>.</summary>
public sealed record AssetLocationAndSpeedAsset
{
    /// <summary>Asset id. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")] public required string Id { get; init; }

    /// <summary>Map of external ids associated with the asset.</summary>
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>Speed details attached to a location-and-speed reading. Mirrors
/// the spec's <c>SpeedResponseResponseBody</c>.</summary>
public sealed record AssetLocationAndSpeedSpeed
{
    /// <summary>Speed of the asset based on ECU data (meters per second).</summary>
    [JsonPropertyName("ecuSpeedMetersPerSecond")] public double? EcuSpeedMetersPerSecond { get; init; }

    /// <summary>Speed of the asset based on GPS data (meters per second).</summary>
    [JsonPropertyName("gpsSpeedMetersPerSecond")] public double? GpsSpeedMetersPerSecond { get; init; }
}

/// <summary>Asset location details.</summary>
public sealed record AssetLocation
{
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }
    [JsonPropertyName("heading")] public double? Heading { get; init; }
    [JsonPropertyName("reverseGeo")] public ReverseGeo? ReverseGeo { get; init; }
}
