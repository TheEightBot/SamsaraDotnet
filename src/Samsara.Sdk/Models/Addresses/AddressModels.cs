namespace Samsara.Sdk.Models.Addresses;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a physical address/geofence in the Samsara system.
/// </summary>
public sealed record Address
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("addressTypes")]
    public IReadOnlyList<string>? AddressTypes { get; init; }

    [JsonPropertyName("geofence")]
    public Geofence? Geofence { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("contactIds")]
    public IReadOnlyList<string>? ContactIds { get; init; }
}

/// <summary>
/// Represents a geofence shape associated with an address.
/// </summary>
public sealed record Geofence
{
    [JsonPropertyName("circle")]
    public GeofenceCircle? Circle { get; init; }

    [JsonPropertyName("polygon")]
    public GeofencePolygon? Polygon { get; init; }
}

/// <summary>
/// A circular geofence definition.
/// </summary>
public sealed record GeofenceCircle
{
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    [JsonPropertyName("radiusMeters")]
    public required long RadiusMeters { get; init; }
}

/// <summary>
/// A polygon geofence definition.
/// </summary>
public sealed record GeofencePolygon
{
    [JsonPropertyName("vertices")]
    public required IReadOnlyList<GeofenceVertex> Vertices { get; init; }
}

/// <summary>
/// A vertex in a polygon geofence.
/// </summary>
public sealed record GeofenceVertex
{
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}

/// <summary>
/// Request body for creating a new address.
/// </summary>
public sealed record CreateAddressRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("addressTypes")]
    public IReadOnlyList<string>? AddressTypes { get; init; }

    [JsonPropertyName("geofence")]
    public Geofence? Geofence { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("contactIds")]
    public IReadOnlyList<string>? ContactIds { get; init; }
}

/// <summary>
/// Request body for updating an address (PATCH).
/// </summary>
public sealed record UpdateAddressRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("addressTypes")]
    public IReadOnlyList<string>? AddressTypes { get; init; }

    [JsonPropertyName("geofence")]
    public Geofence? Geofence { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("contactIds")]
    public IReadOnlyList<string>? ContactIds { get; init; }
}
