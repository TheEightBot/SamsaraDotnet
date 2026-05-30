namespace Samsara.Sdk.Models.Routes;

using System.Text.Json.Serialization;

/// <summary>
/// A vehicle trip, returned as a stream item by <c>GET /trips/stream</c>. Mirrors
/// the spec's trip stream inner schema. The legacy <c>GET /v1/fleet/trips</c>
/// endpoint returns a different, v1-only shape; see <see cref="V1Trip"/>.
/// </summary>
public sealed record Trip
{
    /// <summary>The asset (vehicle) the trip was driven on. Required on the stream response.</summary>
    [JsonPropertyName("asset")]
    public TripAsset? Asset { get; init; }

    /// <summary>Completion status of the trip (e.g. <c>completed</c>, <c>inProgress</c>).</summary>
    [JsonPropertyName("completionStatus")]
    public string? CompletionStatus { get; init; }

    /// <summary>Time the trip record was created, in RFC 3339 format.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Start location of the trip.</summary>
    [JsonPropertyName("startLocation")]
    public TripLocation? StartLocation { get; init; }

    /// <summary>End location of the trip (omitted while the trip is still in progress).</summary>
    [JsonPropertyName("endLocation")]
    public TripLocation? EndLocation { get; init; }

    /// <summary>Start time of the trip, in RFC 3339 format.</summary>
    [JsonPropertyName("tripStartTime")]
    public DateTimeOffset? TripStartTime { get; init; }

    /// <summary>End time of the trip, in RFC 3339 format (omitted while the trip is still in progress).</summary>
    [JsonPropertyName("tripEndTime")]
    public DateTimeOffset? TripEndTime { get; init; }

    /// <summary>Time the trip record was last updated, in RFC 3339 format.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// The asset (vehicle) a <see cref="Trip"/> was driven on. Mirrors the spec's
/// <c>TripAssetResponseBody</c>.
/// </summary>
public sealed record TripAsset
{
    /// <summary>Samsara ID of the asset. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the asset.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Asset type (e.g. <c>vehicle</c>).</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Vehicle identification number (VIN) of the asset.</summary>
    [JsonPropertyName("vin")]
    public string? Vin { get; init; }
}

/// <summary>
/// A trip start/end location. Mirrors the spec's <c>LocationResponseResponseBody</c>.
/// </summary>
public sealed record TripLocation
{
    /// <summary>Latitude in decimal degrees. Spec-required.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec-required.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Heading in degrees from true north. Spec-required.</summary>
    [JsonPropertyName("headingDegrees")]
    public long? HeadingDegrees { get; init; }

    /// <summary>Accuracy of the location, in meters.</summary>
    [JsonPropertyName("accuracyMeters")]
    public double? AccuracyMeters { get; init; }

    /// <summary>The matched address for this location, if any.</summary>
    [JsonPropertyName("address")]
    public TripLocationAddress? Address { get; init; }

    /// <summary>The matched geofence for this location, if any.</summary>
    [JsonPropertyName("geofence")]
    public System.Text.Json.JsonElement? Geofence { get; init; }
}

/// <summary>
/// The matched address on a <see cref="TripLocation"/>. Mirrors the spec's
/// <c>AddressResponseResponseBody</c>.
/// </summary>
public sealed record TripLocationAddress
{
    /// <summary>Samsara ID of the address.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the address.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A vehicle trip as returned by the legacy <c>GET /v1/fleet/trips</c> endpoint,
/// which responds with a <see cref="V1TripsResponse"/> wrapper. Mirrors the spec's
/// <c>V1TripResponse_trips</c> inner schema.
/// </summary>
public sealed record V1Trip
{
    /// <summary>IDs of the assets (vehicles) on the trip.</summary>
    [JsonPropertyName("assetIds")]
    public IReadOnlyList<long>? AssetIds { get; init; }

    /// <summary>IDs of the co-drivers on the trip.</summary>
    [JsonPropertyName("codriverIds")]
    public IReadOnlyList<long>? CodriverIds { get; init; }

    /// <summary>ID of the primary driver on the trip.</summary>
    [JsonPropertyName("driverId")]
    public long? DriverId { get; init; }

    /// <summary>Distance travelled on the trip, in meters.</summary>
    [JsonPropertyName("distanceMeters")]
    public long? DistanceMeters { get; init; }

    /// <summary>Fuel consumed on the trip, in milliliters.</summary>
    [JsonPropertyName("fuelConsumedMl")]
    public long? FuelConsumedMl { get; init; }

    /// <summary>Toll distance on the trip, in meters.</summary>
    [JsonPropertyName("tollMeters")]
    public long? TollMeters { get; init; }

    /// <summary>Trip start time, in Unix milliseconds.</summary>
    [JsonPropertyName("startMs")]
    public long? StartMs { get; init; }

    /// <summary>Trip end time, in Unix milliseconds.</summary>
    [JsonPropertyName("endMs")]
    public long? EndMs { get; init; }

    /// <summary>Odometer reading at the start of the trip, in meters.</summary>
    [JsonPropertyName("startOdometer")]
    public long? StartOdometer { get; init; }

    /// <summary>Odometer reading at the end of the trip, in meters.</summary>
    [JsonPropertyName("endOdometer")]
    public long? EndOdometer { get; init; }

    /// <summary>Free-form start location string.</summary>
    [JsonPropertyName("startLocation")]
    public string? StartLocation { get; init; }

    /// <summary>Free-form end location string.</summary>
    [JsonPropertyName("endLocation")]
    public string? EndLocation { get; init; }

    /// <summary>Structured start address.</summary>
    [JsonPropertyName("startAddress")]
    public System.Text.Json.JsonElement? StartAddress { get; init; }

    /// <summary>Structured end address.</summary>
    [JsonPropertyName("endAddress")]
    public System.Text.Json.JsonElement? EndAddress { get; init; }

    /// <summary>Start GPS coordinates.</summary>
    [JsonPropertyName("startCoordinates")]
    public System.Text.Json.JsonElement? StartCoordinates { get; init; }

    /// <summary>End GPS coordinates.</summary>
    [JsonPropertyName("endCoordinates")]
    public System.Text.Json.JsonElement? EndCoordinates { get; init; }
}

/// <summary>
/// The top-level response of the legacy <c>GET /v1/fleet/trips</c> endpoint.
/// Mirrors the spec's <c>V1TripResponse</c> (a <c>{ trips: [...] }</c> wrapper, not
/// the standard <c>{ data: [...] }</c> envelope).
/// </summary>
public sealed record V1TripsResponse
{
    /// <summary>The trips in the requested time range.</summary>
    [JsonPropertyName("trips")]
    public IReadOnlyList<V1Trip>? Trips { get; init; }
}
