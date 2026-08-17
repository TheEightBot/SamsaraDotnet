namespace Samsara.Sdk.Models.Fleet;

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
    /// <summary>Custom attributes associated with the asset. Mirrors the spec's
    /// <c>GoaAttributeTinyResponseBody</c> schema.</summary>
    [JsonPropertyName("attributes")] public IReadOnlyList<AttributeTiny>? Attributes { get; init; }
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
    /// <summary>Custom attributes to set on the asset. Mirrors the spec's
    /// <c>GoaAttributeTinyRequestBody</c> schema.</summary>
    [JsonPropertyName("attributes")] public IReadOnlyList<AttributeTiny>? Attributes { get; init; }
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

    /// <summary>Tag IDs to associate with the asset.</summary>
    [JsonPropertyName("tagIds")] public IReadOnlyList<string>? TagIds { get; init; }

    /// <summary>Custom attributes to set on the asset. Mirrors the spec's
    /// <c>GoaAttributeTinyRequestBody</c> schema.</summary>
    [JsonPropertyName("attributes")] public IReadOnlyList<AttributeTiny>? Attributes { get; init; }
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

/// <summary>
/// Asset location details on a location-and-speed reading. Mirrors the spec's
/// <c>LocationResponseResponseBody</c> (the only schema this record is reached
/// from is <c>GET /assets/location-and-speed/stream</c>).
/// </summary>
/// <remarks>
/// Spec marks <c>headingDegrees</c>, <c>latitude</c> and <c>longitude</c>
/// REQUIRED. They stay nullable here: the live API omits fields its own spec
/// marks required and <c>SamsaraSerializerOptions.Default</c> is deliberately
/// lenient, so <c>required</c> on a response record would turn a sparse payload
/// into a deserialization crash.
/// </remarks>
public sealed record AssetLocation
{
    /// <summary>Latitude of the location of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }

    /// <summary>Longitude of the location of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }

    /// <summary>
    /// Heading of the asset in degrees; may be 0 when the asset is not moving.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("headingDegrees")] public long? HeadingDegrees { get; init; }

    /// <summary>
    /// Radial accuracy of the GPS location in meters. Only returned when strong
    /// GPS is not available.
    /// </summary>
    [JsonPropertyName("accuracyMeters")] public double? AccuracyMeters { get; init; }

    /// <summary>Closest address that the GPS latitude and longitude match to.</summary>
    [JsonPropertyName("address")] public AssetLocationAddress? Address { get; init; }

    /// <summary>Closest geofence based on a 1000 meter radial search.</summary>
    [JsonPropertyName("geofence")] public AssetLocationGeofence? Geofence { get; init; }
}

/// <summary>
/// Closest address matched to an asset location. Mirrors the spec's
/// <c>AddressResponseResponseBody</c>.
/// </summary>
public sealed record AssetLocationAddress
{
    /// <summary>Street number of the address.</summary>
    [JsonPropertyName("streetNumber")] public string? StreetNumber { get; init; }

    /// <summary>The street name.</summary>
    [JsonPropertyName("street")] public string? Street { get; init; }

    /// <summary>The name of the neighborhood if one exists.</summary>
    [JsonPropertyName("neighborhood")] public string? Neighborhood { get; init; }

    /// <summary>The name of the city.</summary>
    [JsonPropertyName("city")] public string? City { get; init; }

    /// <summary>The name of the state.</summary>
    [JsonPropertyName("state")] public string? State { get; init; }

    /// <summary>The zip code.</summary>
    [JsonPropertyName("postalCode")] public string? PostalCode { get; init; }

    /// <summary>The country.</summary>
    [JsonPropertyName("country")] public string? Country { get; init; }

    /// <summary>A point that may be of interest to the user.</summary>
    [JsonPropertyName("pointOfInterest")] public string? PointOfInterest { get; init; }
}

/// <summary>
/// Closest geofence to an asset location. Mirrors the spec's
/// <c>GeofenceResponseResponseBody</c>.
/// </summary>
public sealed record AssetLocationGeofence
{
    /// <summary>Unique ID of the geofence object.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>A map of external IDs.</summary>
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// The asset side of an asset assignment. Mirrors the spec's
/// <c>AssetResponseResponseBody</c> as reached from
/// <c>GET|POST /fleet/assets/assignments</c>.
/// </summary>
public sealed record AssetAssignmentAsset
{
    /// <summary>ID of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>A map of external ids.</summary>
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// The assignee side of an asset assignment. Mirrors the spec's
/// <c>AssetAssignmentAssigneeResponseObjectResponseBody</c>.
/// </summary>
public sealed record AssetAssignmentAssignee
{
    /// <summary>ID of the assignee. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>
    /// Kind of assignee: <c>unknown</c>, <c>driver</c>, <c>asset</c>,
    /// <c>geofence</c> or <c>job</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("assigneeType")] public string? AssigneeType { get; init; }

    /// <summary>A map of external ids for the assignee.</summary>
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// An active asset assignment (beta). Mirrors the spec's
/// <c>AssetAssignmentResponseObjectResponseBody</c>, the payload of
/// <c>GET /fleet/assets/assignments</c> (<c>listAssetAssignments</c>) and
/// <c>POST /fleet/assets/assignments</c> (<c>createAssetAssignment</c>).
/// </summary>
public sealed record AssetAssignment
{
    /// <summary>The asset that is assigned. Spec marks REQUIRED.</summary>
    [JsonPropertyName("asset")] public AssetAssignmentAsset? Asset { get; init; }

    /// <summary>The entity the asset is assigned to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("assignee")] public AssetAssignmentAssignee? Assignee { get; init; }

    /// <summary>The start time of the assignment in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")] public DateTimeOffset? StartTime { get; init; }

    /// <summary>The end time of the assignment in RFC 3339 format, if the assignment has ended.</summary>
    [JsonPropertyName("endTime")] public DateTimeOffset? EndTime { get; init; }

    /// <summary>The expected end time of the assignment in RFC 3339 format, if one was set at creation.</summary>
    [JsonPropertyName("expectedEndTime")] public DateTimeOffset? ExpectedEndTime { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/assets/assignments</c>
/// (<c>createAssetAssignment</c>, beta). Mirrors the spec's
/// <c>AssetAssignmentsCreateAssetAssignmentRequestBody</c>.
/// </summary>
public sealed record CreateAssetAssignmentRequest
{
    /// <summary>Samsara ID of the asset. Spec REQUIRED.</summary>
    [JsonPropertyName("assetId")] public required string AssetId { get; init; }

    /// <summary>Samsara ID of the assignee. Spec REQUIRED.</summary>
    [JsonPropertyName("assigneeId")] public required string AssigneeId { get; init; }

    /// <summary>
    /// Kind of assignee: <c>driver</c>, <c>asset</c> or <c>geofence</c>. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("assigneeType")] public required string AssigneeType { get; init; }

    /// <summary>
    /// Optional expected end time of the assignment in RFC 3339 format. Must be
    /// strictly after the assignment start.
    /// </summary>
    [JsonPropertyName("expectedEndTime")] public DateTimeOffset? ExpectedEndTime { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/assets/assignments/unassign</c>
/// (<c>unassignAssetAssignment</c>, beta). Mirrors the spec's
/// <c>AssetAssignmentsUnassignAssetAssignmentRequestBody</c>.
/// </summary>
public sealed record UnassignAssetAssignmentRequest
{
    /// <summary>Samsara ID of the asset. Spec REQUIRED.</summary>
    [JsonPropertyName("assetId")] public required string AssetId { get; init; }
}

/// <summary>
/// An association between a peripheral device and the central device it was
/// detected by (beta). Mirrors the spec's <c>AssociationResponseBody</c>, the
/// payload of <c>GET /fleet/assets/associations</c> (<c>listAssociations</c>).
/// </summary>
public sealed record AssetAssociation
{
    /// <summary>The Samsara ID of the central device in this association. Spec marks REQUIRED.</summary>
    [JsonPropertyName("centralId")] public string? CentralId { get; init; }

    /// <summary>The Samsara ID of the peripheral device in this association. Spec marks REQUIRED.</summary>
    [JsonPropertyName("peripheralId")] public string? PeripheralId { get; init; }

    /// <summary>The human-readable name of the peripheral device, if available.</summary>
    [JsonPropertyName("peripheralName")] public string? PeripheralName { get; init; }

    /// <summary>The time when this association started, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("associationStartTime")] public string? AssociationStartTime { get; init; }

    /// <summary>The time when this association ended, in RFC 3339 format. Null if still active.</summary>
    [JsonPropertyName("associationEndTime")] public string? AssociationEndTime { get; init; }
}
