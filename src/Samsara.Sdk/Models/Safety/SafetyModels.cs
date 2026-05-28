namespace Samsara.Sdk.Models.Safety;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a safety event (harsh braking, speeding, collision, etc).
/// Mirrors the spec's <c>SafetyEventV2ObjectResponseBody</c> returned by
/// <c>GET /safety-events</c> and <c>GET /safety-events/stream</c>.
/// </summary>
public sealed record SafetyEvent
{
    /// <summary>The unique Samsara ID (uuid) of the safety event. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Asset that the safety event is tied to. Spec-required.</summary>
    [JsonPropertyName("asset")]
    public required SafetyEventAsset Asset { get; init; }

    /// <summary>Driver that is assigned to the safety event. Spec-required.</summary>
    [JsonPropertyName("driver")]
    public required SafetyEventDriver Driver { get; init; }

    /// <summary>
    /// The most up-to-date behavior labels associated with the safety event. These labels
    /// can be updated by Safety Admins. Spec-required.
    /// </summary>
    [JsonPropertyName("behaviorLabels")]
    public required IReadOnlyList<SafetyEventBehaviorLabel> BehaviorLabels { get; init; }

    /// <summary>
    /// The most up-to-date context labels associated with the safety event. AI generated
    /// labels can be updated by Safety Admins. Spec-required.
    /// </summary>
    [JsonPropertyName("contextLabels")]
    public required IReadOnlyList<SafetyEventContextLabel> ContextLabels { get; init; }

    /// <summary>UTC time the Safety Event was created in Samsara in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("createdAtTime")]
    public required string CreatedAtTime { get; init; }

    /// <summary>UTC time the Safety Event updated in Samsara in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("updatedAtTime")]
    public required string UpdatedAtTime { get; init; }

    /// <summary>UTC time the Safety Event started in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("startMs")]
    public required string StartMs { get; init; }

    /// <summary>UTC time the Safety Event ended in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("endMs")]
    public required string EndMs { get; init; }

    /// <summary>
    /// The current state of the Safety Event. Valid values: <c>unknown</c>, <c>needsReview</c>,
    /// <c>reviewed</c>, <c>needsCoaching</c>, <c>coached</c>, <c>dismissed</c>,
    /// <c>needsRecognition</c>, <c>recognized</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("eventState")]
    public required string EventState { get; init; }

    /// <summary>A link to the Safety Event review page for the associated event. Spec-required.</summary>
    [JsonPropertyName("inboxEventUrl")]
    public required string InboxEventUrl { get; init; }

    /// <summary>
    /// If a harsh event, the URL of the associated incident report page. If a speeding event,
    /// the URL of the associated speeding report page. Spec-required.
    /// </summary>
    [JsonPropertyName("incidentReportUrl")]
    public required string IncidentReportUrl { get; init; }

    /// <summary>Location of the safety event. Spec-required.</summary>
    [JsonPropertyName("location")]
    public required SafetyEventLocation Location { get; init; }

    /// <summary>
    /// The maximum acceleration value as a multiplier on the force of gravity (g). Spec-required.
    /// </summary>
    [JsonPropertyName("maxAccelerationGForce")]
    public required double MaxAccelerationGForce { get; init; }

    /// <summary>Unique user ID for the assigned coach.</summary>
    [JsonPropertyName("assignedCoach")]
    public string? AssignedCoach { get; init; }

    /// <summary>Camera streams that detected the safety event.</summary>
    [JsonPropertyName("detectedStreams")]
    public IReadOnlyList<SafetyEventMedia>? DetectedStreams { get; init; }

    /// <summary>Dismissal reason associated with the Safety Event.</summary>
    [JsonPropertyName("dismissalReason")]
    public SafetyEventDismissalReason? DismissalReason { get; init; }

    /// <summary>Media assets available for the safety event.</summary>
    [JsonPropertyName("media")]
    public IReadOnlyList<SafetyEventMedia>? Media { get; init; }

    /// <summary>Speeding data associated with the event. Only returned for speeding related events.</summary>
    [JsonPropertyName("speedingMetadata")]
    public SafetyEventSpeedingMetadata? SpeedingMetadata { get; init; }

    /// <summary>
    /// UTC time the trip ended in RFC 3339 format. Null when the Safety Event occurs off-trip
    /// or the trip is ongoing.
    /// </summary>
    [JsonPropertyName("tripEndTime")]
    public string? TripEndTime { get; init; }

    /// <summary>
    /// UTC time the trip started in RFC 3339 format. Null when the Safety Event occurs off-trip.
    /// </summary>
    [JsonPropertyName("tripStartTime")]
    public string? TripStartTime { get; init; }

    /// <summary>
    /// The user ID associated with the user who made the event state change. Only returned if
    /// the event state changes.
    /// </summary>
    [JsonPropertyName("updatedByUserId")]
    public string? UpdatedByUserId { get; init; }

    /// <summary>
    /// Vehicle reference (legacy flat shape, retained for back-compat). The spec models the
    /// reporting asset under <see cref="Asset"/>.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public SafetyEventVehicle? Vehicle { get; init; }

    /// <summary>
    /// Event time (legacy flat shape, retained for back-compat). The spec models event timing
    /// under <see cref="StartMs"/>/<see cref="EndMs"/>.
    /// </summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }
}

/// <summary>
/// Asset that the safety event is tied to. Mirrors the spec's
/// <c>SafetyEventV2AssetObjectResponseBody</c>.
/// </summary>
public sealed record SafetyEventAsset
{
    /// <summary>Unique ID for the asset object that is reporting the safety event. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Name for the asset object that is reporting the safety event. Only returns when
    /// <c>includeAsset</c> is set to <c>true</c>.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Type for the asset object. Only returns when <c>includeAsset</c> is set to <c>true</c>.
    /// Valid values: <c>uncategorized</c>, <c>trailer</c>, <c>equipment</c>, <c>unpowered</c>,
    /// <c>vehicle</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>VIN for the asset object. Only returns when <c>includeAsset</c> is set to <c>true</c>.</summary>
    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    /// <summary>
    /// Attributes for the asset associated with the safety event. Only returns when
    /// <c>includeAsset</c> is set to <c>true</c>.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<SafetyEventAttribute>? Attributes { get; init; }

    /// <summary>
    /// Tags for the asset associated with the safety event. Only returns when
    /// <c>includeAsset</c> is set to <c>true</c>.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<SafetyEventTag>? Tags { get; init; }

    /// <summary>A map of external IDs.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Vehicle reference within a safety event (legacy flat shape, retained for back-compat).
/// </summary>
public sealed record SafetyEventVehicle
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Driver that is assigned to the safety event. Mirrors the spec's
/// <c>SafetyEventV2DriverObjectResponseBody</c>.
/// </summary>
public sealed record SafetyEventDriver
{
    /// <summary>Unique ID for the driver object that is assigned to the safety event. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the driver. Only returns when <c>includeDriver</c> is set to <c>true</c>.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Attributes for the driver associated with the safety event. Only returns when
    /// <c>includeDriver</c> is set to <c>true</c>.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<SafetyEventAttribute>? Attributes { get; init; }

    /// <summary>
    /// Tags for the driver associated with the safety event. Only returns when
    /// <c>includeDriver</c> is set to <c>true</c>.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<SafetyEventTag>? Tags { get; init; }

    /// <summary>A map of external IDs.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A behavior label associated with a safety event. Mirrors the spec's
/// <c>SafetyEventV2BehaviorLabelsResponseBody</c>.
/// </summary>
public sealed record SafetyEventBehaviorLabel
{
    /// <summary>
    /// The label associated with the safety event (e.g. <c>Acceleration</c>, <c>Braking</c>,
    /// <c>Crash</c>, <c>Speeding</c>). See the spec for the full enumeration.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// The source of the label associated with the safety event. Valid values:
    /// <c>automated</c>, <c>userGenerated</c>.
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; init; }
}

/// <summary>
/// A context label associated with a safety event. Mirrors the spec's
/// <c>SafetyEventV2ContextLabelsResponseBody</c>.
/// </summary>
public sealed record SafetyEventContextLabel
{
    /// <summary>
    /// The user ID associated with the user who created the context label. A value of 0
    /// indicates the label is auto-generated. Spec-required.
    /// </summary>
    [JsonPropertyName("authorId")]
    public required string AuthorId { get; init; }

    /// <summary>Time the context label was created. Spec-required.</summary>
    [JsonPropertyName("createdAtTime")]
    public required string CreatedAtTime { get; init; }

    /// <summary>Name of the context label. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

/// <summary>
/// A media asset available for a safety event. Mirrors the spec's
/// <c>SafetyEventV2MediaResponseBody</c>.
/// </summary>
public sealed record SafetyEventMedia
{
    /// <summary>
    /// The media input type of the camera. Valid values: <c>dashcamRoadFacing</c>,
    /// <c>dashcamDriverFacing</c>, <c>analog1</c>, <c>analog2</c>, <c>analog3</c>,
    /// <c>analog4</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("input")]
    public required string Input { get; init; }

    /// <summary>URL to download the media asset. Spec-required.</summary>
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    /// <summary>The serial number of the auxiliary camera device.</summary>
    [JsonPropertyName("auxcamSerial")]
    public string? AuxcamSerial { get; init; }

    /// <summary>
    /// The currently assigned role name of the camera. May change if camera role settings are
    /// updated. See the spec for the full enumeration of valid values.
    /// </summary>
    [JsonPropertyName("cameraRole")]
    public string? CameraRole { get; init; }
}

/// <summary>
/// Dismissal reason associated with a safety event. Mirrors the spec's
/// <c>SafetyEventDismissalReasonResponseBody</c>.
/// </summary>
public sealed record SafetyEventDismissalReason
{
    /// <summary>
    /// The dismissal reason code associated with the event. Valid values: <c>incorrect</c>,
    /// <c>minorEvent</c>, <c>other</c>.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>The dismissal reason comment associated with the event.</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }
}

/// <summary>
/// Speeding data associated with a safety event. Mirrors the spec's
/// <c>SafetyEventSpeedingMetadataResponseBody</c>.
/// </summary>
public sealed record SafetyEventSpeedingMetadata
{
    /// <summary>The max speed exceeded during the event, in kilometers per hour. Spec-required.</summary>
    [JsonPropertyName("maxSpeedKilometersPerHour")]
    public required long MaxSpeedKilometersPerHour { get; init; }

    /// <summary>The posted speed limit associated with the event, in kilometers per hour. Spec-required.</summary>
    [JsonPropertyName("postedSpeedLimitKilometersPerHour")]
    public required long PostedSpeedLimitKilometersPerHour { get; init; }
}

/// <summary>
/// Location of a safety event. Mirrors the spec's <c>LocationResponseResponseBody</c>.
/// </summary>
public sealed record SafetyEventLocation
{
    /// <summary>Heading of the asset in degrees. May be 0 if the asset is not moving. Spec-required.</summary>
    [JsonPropertyName("headingDegrees")]
    public required long HeadingDegrees { get; init; }

    /// <summary>Latitude of the location of the asset. Spec-required.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude of the location of the asset. Spec-required.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    /// <summary>
    /// Radial accuracy of the GPS location in meters. Only returned if strong GPS is not available.
    /// </summary>
    [JsonPropertyName("accuracyMeters")]
    public double? AccuracyMeters { get; init; }

    /// <summary>Closest address that the GPS latitude and longitude match to.</summary>
    [JsonPropertyName("address")]
    public SafetyEventAddress? Address { get; init; }

    /// <summary>Closest geofence based on a 1000 meter radial search.</summary>
    [JsonPropertyName("geofence")]
    public SafetyEventGeofence? Geofence { get; init; }
}

/// <summary>
/// Closest address matched to a safety event location. Mirrors the spec's
/// <c>AddressResponseResponseBody</c>.
/// </summary>
public sealed record SafetyEventAddress
{
    /// <summary>The street number of the address.</summary>
    [JsonPropertyName("streetNumber")]
    public string? StreetNumber { get; init; }

    /// <summary>The street name.</summary>
    [JsonPropertyName("street")]
    public string? Street { get; init; }

    /// <summary>The name of the neighborhood if one exists.</summary>
    [JsonPropertyName("neighborhood")]
    public string? Neighborhood { get; init; }

    /// <summary>The name of the city.</summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>The name of the state.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>The zip code.</summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; init; }

    /// <summary>The country.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    /// <summary>A point that may be of interest to the user.</summary>
    [JsonPropertyName("pointOfInterest")]
    public string? PointOfInterest { get; init; }
}

/// <summary>
/// Closest geofence to a safety event location. Mirrors the spec's
/// <c>GeofenceResponseResponseBody</c>.
/// </summary>
public sealed record SafetyEventGeofence
{
    /// <summary>Unique ID of the geofence object.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>A map of external IDs.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A minified attribute associated with a safety event asset or driver. Mirrors the spec's
/// <c>GoaAttributeTinyResponseBody</c>.
/// </summary>
public sealed record SafetyEventAttribute
{
    /// <summary>ID of the attribute.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the attribute.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>List of string values associated with the attribute.</summary>
    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }

    /// <summary>List of number values associated with the attribute.</summary>
    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    /// <summary>
    /// List of date values associated with the attribute (RFC 3339 full-date format: YYYY-MM-DD).
    /// </summary>
    [JsonPropertyName("dateValues")]
    public IReadOnlyList<string>? DateValues { get; init; }
}

/// <summary>
/// A minified tag associated with a safety event asset or driver. Mirrors the spec's
/// <c>GoaTagTinyResponseResponseBody</c>.
/// </summary>
public sealed record SafetyEventTag
{
    /// <summary>ID of the tag. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the tag. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// If this tag is part of a hierarchical tag tree, the ID of the parent tag; otherwise omitted.
    /// </summary>
    [JsonPropertyName("parentTagId")]
    public string? ParentTagId { get; init; }
}

/// <summary>
/// Vehicle safety score. Mirrors the spec's <c>VehicleSafetyScoreResponseBody</c> returned by
/// <c>GET /safety-scores/vehicles</c>.
/// </summary>
public sealed record VehicleSafetyScore
{
    /// <summary>ID of the vehicle. Spec-required.</summary>
    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    /// <summary>The safety score for the vehicle. Spec-required.</summary>
    [JsonPropertyName("vehicleScore")]
    public required int VehicleScore { get; init; }

    /// <summary>Breakdown of the behaviors that contributed to the score. Spec-required.</summary>
    [JsonPropertyName("behaviors")]
    public required IReadOnlyList<SafetyScoreBehavior> Behaviors { get; init; }

    /// <summary>Breakdown of the speeding intervals that contributed to the score. Spec-required.</summary>
    [JsonPropertyName("speeding")]
    public required IReadOnlyList<SafetyScoreSpeeding> Speeding { get; init; }

    /// <summary>Total distance driven over the time range, in meters. Spec-required.</summary>
    [JsonPropertyName("driveDistanceMeters")]
    public required long DriveDistanceMeters { get; init; }

    /// <summary>Total time driven over the time range, in milliseconds. Spec-required.</summary>
    [JsonPropertyName("driveTimeMilliseconds")]
    public required long DriveTimeMilliseconds { get; init; }

    /// <summary>
    /// Composite safety score (legacy flat shape, retained for back-compat). The spec models the
    /// vehicle score under <see cref="VehicleScore"/>.
    /// </summary>
    [JsonPropertyName("safetyScore")]
    public double? SafetyScore { get; init; }

    /// <summary>
    /// Total harsh event count (legacy flat shape, retained for back-compat). The spec models
    /// behavior counts under <see cref="Behaviors"/>.
    /// </summary>
    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    /// <summary>
    /// Total time driven in milliseconds (legacy flat shape, retained for back-compat). The spec
    /// models this under <see cref="DriveTimeMilliseconds"/>.
    /// </summary>
    [JsonPropertyName("totalTimeDrivenMs")]
    public long? TotalTimeDrivenMs { get; init; }

    /// <summary>
    /// Total distance driven in meters (legacy flat shape, retained for back-compat). The spec
    /// models this under <see cref="DriveDistanceMeters"/>.
    /// </summary>
    [JsonPropertyName("totalDistanceDrivenMeters")]
    public double? TotalDistanceDrivenMeters { get; init; }

    /// <summary>Time range (legacy flat shape, retained for back-compat). Not modeled by the spec.</summary>
    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }

    /// <summary>
    /// Crash count (legacy flat shape, retained for back-compat). The spec models behavior counts
    /// under <see cref="Behaviors"/>.
    /// </summary>
    [JsonPropertyName("crashCount")]
    public int? CrashCount { get; init; }

    /// <summary>
    /// Harsh acceleration count (legacy flat shape, retained for back-compat). The spec models
    /// behavior counts under <see cref="Behaviors"/>.
    /// </summary>
    [JsonPropertyName("harshAccelCount")]
    public int? HarshAccelCount { get; init; }

    /// <summary>
    /// Harsh braking count (legacy flat shape, retained for back-compat). The spec models behavior
    /// counts under <see cref="Behaviors"/>.
    /// </summary>
    [JsonPropertyName("harshBrakingCount")]
    public int? HarshBrakingCount { get; init; }

    /// <summary>
    /// Harsh turning count (legacy flat shape, retained for back-compat). The spec models behavior
    /// counts under <see cref="Behaviors"/>.
    /// </summary>
    [JsonPropertyName("harshTurningCount")]
    public int? HarshTurningCount { get; init; }
}

/// <summary>
/// Driver safety score. Mirrors the spec's <c>DriverSafetyScoreResponseBody</c> returned by
/// <c>GET /safety-scores/drivers</c>.
/// </summary>
public sealed record DriverSafetyScore
{
    /// <summary>ID of the driver. Spec-required.</summary>
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    /// <summary>The safety score for the driver. Spec-required.</summary>
    [JsonPropertyName("driverScore")]
    public required int DriverScore { get; init; }

    /// <summary>Breakdown of the behaviors that contributed to the score. Spec-required.</summary>
    [JsonPropertyName("behaviors")]
    public required IReadOnlyList<SafetyScoreBehavior> Behaviors { get; init; }

    /// <summary>Breakdown of the speeding intervals that contributed to the score. Spec-required.</summary>
    [JsonPropertyName("speeding")]
    public required IReadOnlyList<SafetyScoreSpeeding> Speeding { get; init; }

    /// <summary>Total distance driven over the time range, in meters. Spec-required.</summary>
    [JsonPropertyName("driveDistanceMeters")]
    public required long DriveDistanceMeters { get; init; }

    /// <summary>Total time driven over the time range, in milliseconds. Spec-required.</summary>
    [JsonPropertyName("driveTimeMilliseconds")]
    public required long DriveTimeMilliseconds { get; init; }

    /// <summary>
    /// Composite safety score (legacy flat shape, retained for back-compat). The spec models the
    /// driver score under <see cref="DriverScore"/>.
    /// </summary>
    [JsonPropertyName("safetyScore")]
    public double? SafetyScore { get; init; }

    /// <summary>
    /// Total harsh event count (legacy flat shape, retained for back-compat). The spec models
    /// behavior counts under <see cref="Behaviors"/>.
    /// </summary>
    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    /// <summary>
    /// Total time driven in milliseconds (legacy flat shape, retained for back-compat). The spec
    /// models this under <see cref="DriveTimeMilliseconds"/>.
    /// </summary>
    [JsonPropertyName("totalTimeDrivenMs")]
    public long? TotalTimeDrivenMs { get; init; }

    /// <summary>
    /// Total distance driven in meters (legacy flat shape, retained for back-compat). The spec
    /// models this under <see cref="DriveDistanceMeters"/>.
    /// </summary>
    [JsonPropertyName("totalDistanceDrivenMeters")]
    public double? TotalDistanceDrivenMeters { get; init; }

    /// <summary>Time range (legacy flat shape, retained for back-compat). Not modeled by the spec.</summary>
    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }
}

/// <summary>
/// A time range used for safety score calculations (legacy flat shape, retained for back-compat).
/// </summary>
public sealed record TimeRange
{
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
}

/// <summary>
/// Safety score aggregated by tag. Mirrors the spec's <c>TagSafetyScoreResponseBody</c> returned
/// by <c>GET /safety-scores/tags</c>.
/// </summary>
public sealed record TagSafetyScore
{
    /// <summary>ID of the tag. Spec-required.</summary>
    [JsonPropertyName("tagId")]
    public required string TagId { get; init; }

    /// <summary>The safety score for the tag. Spec-required.</summary>
    [JsonPropertyName("tagScore")]
    public required int TagScore { get; init; }

    /// <summary>Breakdown of the behaviors that contributed to the score. Spec-required.</summary>
    [JsonPropertyName("behaviors")]
    public required IReadOnlyList<SafetyScoreBehavior> Behaviors { get; init; }

    /// <summary>Breakdown of the speeding intervals that contributed to the score. Spec-required.</summary>
    [JsonPropertyName("speeding")]
    public required IReadOnlyList<SafetyScoreSpeeding> Speeding { get; init; }

    /// <summary>Total distance driven over the time range, in meters. Spec-required.</summary>
    [JsonPropertyName("driveDistanceMeters")]
    public required long DriveDistanceMeters { get; init; }

    /// <summary>Total time driven over the time range, in milliseconds. Spec-required.</summary>
    [JsonPropertyName("driveTimeMilliseconds")]
    public required long DriveTimeMilliseconds { get; init; }

    /// <summary>Name of the tag (legacy flat shape, retained for back-compat). Not modeled by the spec.</summary>
    [JsonPropertyName("tagName")]
    public string? TagName { get; init; }

    /// <summary>
    /// Composite safety score (legacy flat shape, retained for back-compat). The spec models the
    /// tag score under <see cref="TagScore"/>.
    /// </summary>
    [JsonPropertyName("safetyScore")]
    public double? SafetyScore { get; init; }

    /// <summary>
    /// Total harsh event count (legacy flat shape, retained for back-compat). The spec models
    /// behavior counts under <see cref="Behaviors"/>.
    /// </summary>
    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    /// <summary>Time range (legacy flat shape, retained for back-compat). Not modeled by the spec.</summary>
    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }
}

/// <summary>
/// Safety score aggregated by tag group. Mirrors the spec's <c>TagGroupSafetyScoreResponseBody</c>
/// returned by <c>GET /safety-scores/tag-group</c>.
/// </summary>
public sealed record TagGroupSafetyScore
{
    /// <summary>The combined safety score across the tag group. Spec-required.</summary>
    [JsonPropertyName("combinedScore")]
    public required int CombinedScore { get; init; }

    /// <summary>Breakdown of the behaviors that contributed to the score. Spec-required.</summary>
    [JsonPropertyName("behaviors")]
    public required IReadOnlyList<SafetyScoreBehavior> Behaviors { get; init; }

    /// <summary>Breakdown of the speeding intervals that contributed to the score. Spec-required.</summary>
    [JsonPropertyName("speeding")]
    public required IReadOnlyList<SafetyScoreSpeeding> Speeding { get; init; }

    /// <summary>Total distance driven over the time range, in meters. Spec-required.</summary>
    [JsonPropertyName("driveDistanceMeters")]
    public required long DriveDistanceMeters { get; init; }

    /// <summary>Total time driven over the time range, in milliseconds. Spec-required.</summary>
    [JsonPropertyName("driveTimeMilliseconds")]
    public required long DriveTimeMilliseconds { get; init; }

    /// <summary>
    /// ID of the tag group (legacy flat shape, retained for back-compat). Not modeled by the
    /// spec's <c>TagGroupSafetyScoreResponseBody</c>.
    /// </summary>
    [JsonPropertyName("tagGroupId")]
    public string? TagGroupId { get; init; }

    /// <summary>Name of the tag group (legacy flat shape, retained for back-compat). Not modeled by the spec.</summary>
    [JsonPropertyName("tagGroupName")]
    public string? TagGroupName { get; init; }

    /// <summary>
    /// Composite safety score (legacy flat shape, retained for back-compat). The spec models the
    /// combined score under <see cref="CombinedScore"/>.
    /// </summary>
    [JsonPropertyName("safetyScore")]
    public double? SafetyScore { get; init; }

    /// <summary>
    /// Total harsh event count (legacy flat shape, retained for back-compat). The spec models
    /// behavior counts under <see cref="Behaviors"/>.
    /// </summary>
    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    /// <summary>Time range (legacy flat shape, retained for back-compat). Not modeled by the spec.</summary>
    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }
}

/// <summary>
/// A single behavior's contribution to a safety score. Mirrors the spec's
/// <c>SafetyScoreBehaviorObjectResponseBody</c>.
/// </summary>
public sealed record SafetyScoreBehavior
{
    /// <summary>
    /// Type of the behavior. Valid values: <c>acceleration</c>, <c>braking</c>, <c>crash</c>,
    /// <c>defensiveDriving</c>, <c>didNotYield</c>, <c>distractedDrivingAutomatic</c>,
    /// <c>distractedDrivingManual</c>, <c>drowsy</c>, <c>eatingDrinking</c>,
    /// <c>followingDistance</c>, <c>followingDistanceModerate</c>, <c>followingDistanceSevere</c>,
    /// <c>forwardCollisionWarning</c>, <c>harshTurn</c>, <c>laneDeparture</c>, <c>lateResponse</c>,
    /// <c>mobileUsage</c>, <c>nearCollision</c>, <c>noSeatbelt</c>, <c>obstructedCamera</c>,
    /// <c>ranRedLight</c>, <c>rollingStop</c>, <c>smoking</c>, <c>speedingManual</c>,
    /// <c>unknown</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("behaviorType")]
    public required string BehaviorType { get; init; }

    /// <summary>Count of occurrences of the behavior. Spec-required.</summary>
    [JsonPropertyName("count")]
    public required long Count { get; init; }

    /// <summary>
    /// Total points increased or deducted from the score due to the total count of behaviors.
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("scoreImpact")]
    public required double ScoreImpact { get; init; }
}

/// <summary>
/// A single speeding interval's contribution to a safety score. Mirrors the spec's
/// <c>SafetyScoreSpeedingObjectResponseBody</c>.
/// </summary>
public sealed record SafetyScoreSpeeding
{
    /// <summary>
    /// Type of speeding. Valid values: <c>light</c>, <c>moderate</c>, <c>heavy</c>, <c>severe</c>,
    /// <c>maxSpeed</c>, <c>unknown</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("speedingType")]
    public required string SpeedingType { get; init; }

    /// <summary>Total time spent speeding for the speeding type, in milliseconds. Spec-required.</summary>
    [JsonPropertyName("durationMilliseconds")]
    public required long DurationMilliseconds { get; init; }

    /// <summary>
    /// Total points increased or deducted from the score due to the total time spent speeding.
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("scoreImpact")]
    public required double ScoreImpact { get; init; }
}
