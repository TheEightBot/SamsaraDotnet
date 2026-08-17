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
    public TripGeofence? Geofence { get; init; }
}

/// <summary>
/// The closest address the trip location's GPS coordinates match to. Mirrors the
/// spec's <c>AddressResponseResponseBody</c> — a purely descriptive, unkeyed
/// address breakdown, <em>not</em> a reference to a saved Samsara address.
/// </summary>
/// <remarks>
/// BREAKING (2026-08-17 spec-parity sweep): this record previously declared
/// <c>id</c> and <c>name</c>, neither of which exists in
/// <c>AddressResponseResponseBody</c>. There was zero overlap with the wire
/// shape, so every trip address deserialized to an all-null instance. The
/// <c>{ id, name }</c> pair it modelled belongs to the unrelated legacy v1
/// schemas <c>V1TripResponse_startAddress</c> / <c>_endAddress</c>, which are
/// now modelled by <see cref="V1TripAddress"/>.
/// </remarks>
public sealed record TripLocationAddress
{
    /// <summary>The name of the city.</summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>The country.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    /// <summary>The name of the neighborhood, if one exists.</summary>
    [JsonPropertyName("neighborhood")]
    public string? Neighborhood { get; init; }

    /// <summary>A point that may be of interest to the user.</summary>
    [JsonPropertyName("pointOfInterest")]
    public string? PointOfInterest { get; init; }

    /// <summary>The zip / postal code.</summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; init; }

    /// <summary>The name of the state.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>The street name.</summary>
    [JsonPropertyName("street")]
    public string? Street { get; init; }

    /// <summary>The street number of the address.</summary>
    [JsonPropertyName("streetNumber")]
    public string? StreetNumber { get; init; }
}

/// <summary>
/// The closest geofence to a <see cref="TripLocation"/>, based on a 1000 meter
/// radial search. Mirrors the spec's <c>GeofenceResponseResponseBody</c>.
/// </summary>
/// <remarks>
/// Named <c>TripGeofence</c> rather than <c>Geofence</c> because
/// <c>Samsara.Sdk.Models.Addresses.Geofence</c> already exists and models a
/// different, much richer schema (the geofence definition on an address).
/// </remarks>
public sealed record TripGeofence
{
    /// <summary>Unique ID of the geofence object.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>A map of external ids for the geofence.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
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

    /// <summary>Structured start address (nearest identifiable location to the start coordinates).</summary>
    [JsonPropertyName("startAddress")]
    public V1TripAddress? StartAddress { get; init; }

    /// <summary>Structured end address (nearest identifiable location to the end coordinates).</summary>
    [JsonPropertyName("endAddress")]
    public V1TripAddress? EndAddress { get; init; }

    /// <summary>Start GPS coordinates.</summary>
    [JsonPropertyName("startCoordinates")]
    public V1TripCoordinates? StartCoordinates { get; init; }

    /// <summary>End GPS coordinates.</summary>
    [JsonPropertyName("endCoordinates")]
    public V1TripCoordinates? EndCoordinates { get; init; }
}

/// <summary>
/// The nearest identifiable location to a legacy <see cref="V1Trip"/> endpoint's
/// start or end coordinates. Mirrors the spec's identical
/// <c>V1TripResponse_startAddress</c> and <c>V1TripResponse_endAddress</c>
/// schemas, merged into a single record because the two are byte-identical.
/// </summary>
public sealed record V1TripAddress
{
    /// <summary>
    /// The ID of the address. Modelled as <c>double</c> rather than <c>long</c>
    /// because the spec declares this property <c>type: number</c> (with an
    /// <c>int64</c> format hint) instead of <c>type: integer</c>, unlike every
    /// other identifier on the legacy v1 trip shape.
    /// </summary>
    [JsonPropertyName("id")]
    public double? Id { get; init; }

    /// <summary>The name of the address.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The formatted address.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }
}

/// <summary>
/// Start or end coordinates, in decimal degrees, on a legacy <see cref="V1Trip"/>.
/// Mirrors the spec's identical <c>V1TripResponse_startCoordinates</c> and
/// <c>V1TripResponse_endCoordinates</c> schemas, merged into a single record
/// because the two are byte-identical.
/// </summary>
public sealed record V1TripCoordinates
{
    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
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
