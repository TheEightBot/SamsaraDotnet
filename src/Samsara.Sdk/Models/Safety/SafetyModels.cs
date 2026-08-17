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

/// <summary>
/// Driver safety score as returned by the legacy
/// <c>GET /v1/fleet/drivers/{driverId}/safety/score</c>. Mirrors the spec's
/// <c>V1DriverSafetyScoreResponse</c>.
/// </summary>
/// <remarks>
/// Distinct from <see cref="DriverSafetyScore"/>, which mirrors the v2
/// <c>DriverSafetyScoreResponseBody</c> returned by
/// <c>GET /safety-scores/drivers</c>: the legacy shape reports raw event counts
/// and an embedded harsh-event list instead of behavior and speeding breakdowns.
/// The endpoint returns the object directly — there is no <c>{ data: ... }</c>
/// envelope. The spec marks nothing required on this schema.
/// </remarks>
public sealed record V1DriverSafetyScore
{
    /// <summary>Samsara ID of the driver.</summary>
    [JsonPropertyName("driverId")]
    public long? DriverId { get; init; }

    /// <summary>The driver's safety score over the requested window.</summary>
    [JsonPropertyName("safetyScore")]
    public int? SafetyScore { get; init; }

    /// <summary>Qualitative rank corresponding to the safety score.</summary>
    [JsonPropertyName("safetyScoreRank")]
    public string? SafetyScoreRank { get; init; }

    /// <summary>Number of crashes in the window.</summary>
    [JsonPropertyName("crashCount")]
    public int? CrashCount { get; init; }

    /// <summary>Number of harsh accelerations in the window.</summary>
    [JsonPropertyName("harshAccelCount")]
    public int? HarshAccelCount { get; init; }

    /// <summary>Number of harsh braking events in the window.</summary>
    [JsonPropertyName("harshBrakingCount")]
    public int? HarshBrakingCount { get; init; }

    /// <summary>Number of harsh turning events in the window.</summary>
    [JsonPropertyName("harshTurningCount")]
    public int? HarshTurningCount { get; init; }

    /// <summary>Total number of harsh events in the window.</summary>
    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    /// <summary>The individual harsh events in the window.</summary>
    [JsonPropertyName("harshEvents")]
    public IReadOnlyList<V1SafetyHarshEvent>? HarshEvents { get; init; }

    /// <summary>Time spent over the speed limit, in milliseconds.</summary>
    [JsonPropertyName("timeOverSpeedLimitMs")]
    public long? TimeOverSpeedLimitMs { get; init; }

    /// <summary>Total distance driven in the window, in meters.</summary>
    [JsonPropertyName("totalDistanceDrivenMeters")]
    public long? TotalDistanceDrivenMeters { get; init; }

    /// <summary>Total time driven in the window, in milliseconds.</summary>
    [JsonPropertyName("totalTimeDrivenMs")]
    public long? TotalTimeDrivenMs { get; init; }
}

/// <summary>
/// Vehicle safety score as returned by the legacy
/// <c>GET /v1/fleet/vehicles/{vehicleId}/safety/score</c>. Mirrors the spec's
/// <c>V1VehicleSafetyScoreResponse</c>.
/// </summary>
/// <remarks>
/// Distinct from <see cref="VehicleSafetyScore"/>, which mirrors the v2
/// <c>VehicleSafetyScoreResponseBody</c> returned by
/// <c>GET /safety-scores/vehicles</c>. Property-identical to
/// <see cref="V1DriverSafetyScore"/> except that it keys on <c>vehicleId</c>
/// rather than <c>driverId</c>, so the two are modelled separately. The endpoint
/// returns the object directly — there is no <c>{ data: ... }</c> envelope. The
/// spec marks nothing required on this schema.
/// </remarks>
public sealed record V1VehicleSafetyScore
{
    /// <summary>Samsara ID of the vehicle.</summary>
    [JsonPropertyName("vehicleId")]
    public long? VehicleId { get; init; }

    /// <summary>The vehicle's safety score over the requested window.</summary>
    [JsonPropertyName("safetyScore")]
    public int? SafetyScore { get; init; }

    /// <summary>Qualitative rank corresponding to the safety score.</summary>
    [JsonPropertyName("safetyScoreRank")]
    public string? SafetyScoreRank { get; init; }

    /// <summary>Number of crashes in the window.</summary>
    [JsonPropertyName("crashCount")]
    public int? CrashCount { get; init; }

    /// <summary>Number of harsh accelerations in the window.</summary>
    [JsonPropertyName("harshAccelCount")]
    public int? HarshAccelCount { get; init; }

    /// <summary>Number of harsh braking events in the window.</summary>
    [JsonPropertyName("harshBrakingCount")]
    public int? HarshBrakingCount { get; init; }

    /// <summary>Number of harsh turning events in the window.</summary>
    [JsonPropertyName("harshTurningCount")]
    public int? HarshTurningCount { get; init; }

    /// <summary>Total number of harsh events in the window.</summary>
    [JsonPropertyName("totalHarshEventCount")]
    public int? TotalHarshEventCount { get; init; }

    /// <summary>The individual harsh events in the window.</summary>
    [JsonPropertyName("harshEvents")]
    public IReadOnlyList<V1SafetyHarshEvent>? HarshEvents { get; init; }

    /// <summary>Time spent over the speed limit, in milliseconds.</summary>
    [JsonPropertyName("timeOverSpeedLimitMs")]
    public long? TimeOverSpeedLimitMs { get; init; }

    /// <summary>Total distance driven in the window, in meters.</summary>
    [JsonPropertyName("totalDistanceDrivenMeters")]
    public long? TotalDistanceDrivenMeters { get; init; }

    /// <summary>Total time driven in the window, in milliseconds.</summary>
    [JsonPropertyName("totalTimeDrivenMs")]
    public long? TotalTimeDrivenMs { get; init; }
}

/// <summary>
/// A harsh event summarised on a legacy safety-score response. Mirrors the spec's
/// <c>V1SafetyReportHarshEvent</c>, shared by
/// <see cref="V1DriverSafetyScore.HarshEvents"/> and
/// <see cref="V1VehicleSafetyScore.HarshEvents"/>.
/// </summary>
/// <remarks>
/// Use <c>ILegacyApisClient.V1GetVehicleHarshEventAsync</c> with
/// <see cref="VehicleId"/> and <see cref="TimestampMs"/> to fetch the full detail
/// for one of these events. The spec marks nothing required on this schema.
/// </remarks>
public sealed record V1SafetyHarshEvent
{
    /// <summary>The kind of harsh event (e.g. <c>Harsh Brake</c>, <c>Harsh Turn</c>).</summary>
    [JsonPropertyName("harshEventType")]
    public string? HarshEventType { get; init; }

    /// <summary>Time of the event, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("timestampMs")]
    public long? TimestampMs { get; init; }

    /// <summary>Samsara ID of the vehicle the event occurred on.</summary>
    [JsonPropertyName("vehicleId")]
    public long? VehicleId { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /safety-events/batch</c>. Mirrors the spec's
/// <c>SafetyEventsV2PatchSafetyEventsV2BatchRequestBody</c>.
/// </summary>
public sealed record PatchSafetyEventsBatchRequest
{
    /// <summary>Samsara IDs of the safety events to update. Spec marks REQUIRED.</summary>
    [JsonPropertyName("safetyEventIds")]
    public required IReadOnlyList<string> SafetyEventIds { get; init; }

    /// <summary>
    /// Behavior labels to add to each event (e.g. <c>Acceleration</c>,
    /// <c>Braking</c>, <c>Speeding</c>). See the spec for the full enumeration.
    /// </summary>
    [JsonPropertyName("behaviorLabelsToAdd")]
    public IReadOnlyList<string>? BehaviorLabelsToAdd { get; init; }

    /// <summary>Behavior labels to remove from each event.</summary>
    [JsonPropertyName("behaviorLabelsToRemove")]
    public IReadOnlyList<string>? BehaviorLabelsToRemove { get; init; }

    /// <summary>
    /// Context labels to add to each event (e.g. <c>Congested</c>, <c>Night</c>,
    /// <c>Raining</c>). See the spec for the full enumeration.
    /// </summary>
    [JsonPropertyName("contextLabelsToAdd")]
    public IReadOnlyList<string>? ContextLabelsToAdd { get; init; }

    /// <summary>Context labels to remove from each event.</summary>
    [JsonPropertyName("contextLabelsToRemove")]
    public IReadOnlyList<string>? ContextLabelsToRemove { get; init; }

    /// <summary>
    /// New coaching state for each event: <c>needsReview</c>, <c>reviewed</c>,
    /// <c>needsCoaching</c>, <c>coached</c>, <c>dismissed</c>,
    /// <c>needsRecognition</c> or <c>recognized</c>.
    /// </summary>
    [JsonPropertyName("eventState")]
    public string? EventState { get; init; }

    /// <summary>Why the events are being dismissed, when <c>eventState</c> is <c>dismissed</c>.</summary>
    [JsonPropertyName("dismissalReason")]
    public SafetyEventsBatchDismissalReason? DismissalReason { get; init; }
}

/// <summary>
/// Dismissal reason supplied to <c>PATCH /safety-events/batch</c>. Mirrors the
/// spec's <c>PatchSafetyEventsDismissalReasonBodyRequestBody</c>.
/// </summary>
/// <remarks>
/// Distinct from <see cref="SafetyEventDismissalReason"/>, which is the
/// response-side shape on a v2 safety event.
/// </remarks>
public sealed record SafetyEventsBatchDismissalReason
{
    /// <summary>
    /// Reason code: <c>incorrect</c>, <c>minorEvent</c> or <c>other</c>. Spec
    /// marks REQUIRED.
    /// </summary>
    [JsonPropertyName("code")]
    public required string Code { get; init; }

    /// <summary>Free-text comment accompanying the dismissal.</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }
}

/// <summary>
/// Result of <c>PATCH /safety-events/batch</c>. Mirrors the spec's
/// <c>SafetyEventsV2PatchSafetyEventsV2BatchResponseBody</c>.
/// </summary>
/// <remarks>
/// This operation returns its payload at the top level — the spec defines no
/// <c>{ data: ... }</c> envelope on it, unlike most v2 endpoints. Spec marks both
/// properties REQUIRED; they stay nullable because this is a response record.
/// </remarks>
public sealed record SafetyEventsBatchResult
{
    /// <summary>Identifier of the batch request, for support follow-up. Spec marks REQUIRED.</summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; init; }

    /// <summary>Per-event outcome, in the order the IDs were supplied. Spec marks REQUIRED.</summary>
    [JsonPropertyName("responses")]
    public IReadOnlyList<SafetyEventsBatchResponseItem>? Responses { get; init; }
}

/// <summary>
/// The outcome for one safety event in a batch update. Mirrors the spec's
/// <c>PatchSafetyEventsResponseItemResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks both properties REQUIRED; they stay nullable because this is a
/// response record.
/// </remarks>
public sealed record SafetyEventsBatchResponseItem
{
    /// <summary>HTTP-style status code for this event's update. Spec marks REQUIRED.</summary>
    [JsonPropertyName("status")]
    public long? Status { get; init; }

    /// <summary>Identifies which safety event this outcome applies to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("data")]
    public SafetyEventsBatchResponseItemData? Data { get; init; }
}

/// <summary>
/// The safety event a batch-update outcome applies to. Mirrors the spec's
/// <c>PatchSafetyEventsResponseItemDataResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>safetyEventId</c> REQUIRED; it stays nullable because this is a
/// response record.
/// </remarks>
public sealed record SafetyEventsBatchResponseItemData
{
    /// <summary>Samsara ID of the safety event. Spec marks REQUIRED.</summary>
    [JsonPropertyName("safetyEventId")]
    public string? SafetyEventId { get; init; }
}
