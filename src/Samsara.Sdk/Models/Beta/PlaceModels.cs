namespace Samsara.Sdk.Models.Beta;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// A soft-deletion marker for a place, returned by <c>GET /places/deletions</c>
/// (operationId <c>getPlaceDeletions</c>, beta). Poll this endpoint with the
/// previous page's end cursor to learn which places have been deleted.
/// </summary>
public sealed record PlaceDeletionMarker
{
    /// <summary>Identifier of the deleted place (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>When the place was deleted (spec REQUIRED).</summary>
    [JsonPropertyName("deletedAtTime")]
    public required DateTimeOffset DeletedAtTime { get; init; }

    /// <summary>External IDs that were associated with the deleted place.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A place (geofenced location). Mirrors the spec's
/// <c>PlaceResponseObjectResponseBody</c> — the <c>data</c> payload of
/// <c>GET /places</c>, <c>POST /places</c> and <c>PATCH /places</c>.
/// </summary>
/// <remarks>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </remarks>
public sealed record Place
{
    /// <summary>Samsara place id. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Place name. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Address string. Spec-required.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>
    /// Latitude of the place pin (map marker), stored independently of the geofence.
    /// </summary>
    [JsonPropertyName("addressLatitude")]
    public double? AddressLatitude { get; init; }

    /// <summary>
    /// Longitude of the place pin (map marker), stored independently of the geofence.
    /// </summary>
    [JsonPropertyName("addressLongitude")]
    public double? AddressLongitude { get; init; }

    /// <summary>The geofence configured for the place. Spec-required.</summary>
    [JsonPropertyName("geofence")]
    public PlaceGeofence? Geofence { get; init; }

    /// <summary>Business contacts associated with the place.</summary>
    [JsonPropertyName("businessContacts")]
    public PlaceBusinessContacts? BusinessContacts { get; init; }

    /// <summary>
    /// Camera recording mode: <c>inherit</c>, <c>fullRecording</c>,
    /// <c>driverPrivacy</c>, <c>completePrivacy</c>, <c>unknown</c> or
    /// <c>unspecified</c>.
    /// </summary>
    [JsonPropertyName("cameraRecordingModeType")]
    public string? CameraRecordingModeType { get; init; }

    /// <summary>External ids, returned when <c>includeExternalIds=true</c>.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>IFTA exemption labels.</summary>
    [JsonPropertyName("iftaExemptionTypes")]
    public IReadOnlyList<string>? IftaExemptionTypes { get; init; }

    /// <summary>Whether rolled stops are auto-dismissed at this place.</summary>
    [JsonPropertyName("isAutoDismissRolledStopsEnabled")]
    public bool? IsAutoDismissRolledStopsEnabled { get; init; }

    /// <summary>Whether addresses inside the geofence are shown on the map.</summary>
    [JsonPropertyName("isShowAddressesEnabled")]
    public bool? IsShowAddressesEnabled { get; init; }

    /// <summary>Navigation locations configured for the place.</summary>
    [JsonPropertyName("navigation")]
    public PlaceNavigation? Navigation { get; init; }

    /// <summary>Free-form notes.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Assigned place types.</summary>
    [JsonPropertyName("placeTypes")]
    public IReadOnlyList<string>? PlaceTypes { get; init; }

    /// <summary>Route-planning rows when present.</summary>
    [JsonPropertyName("routing")]
    public IReadOnlyList<PlaceRouting>? Routing { get; init; }

    /// <summary>Configured safety event exclusions.</summary>
    [JsonPropertyName("safetyEventExclusions")]
    public IReadOnlyList<string>? SafetyEventExclusions { get; init; }

    /// <summary>Street View configuration for the place.</summary>
    [JsonPropertyName("streetView")]
    public PlaceStreetView? StreetView { get; init; }

    /// <summary>Tags, returned when <c>includeTags=true</c>.</summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }

    /// <summary>Created timestamp, RFC 3339. Spec-required.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Updated timestamp, RFC 3339. Spec-required.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// Business contacts for a place. Mirrors the spec's
/// <c>PlaceBusinessContactsResponseResponseBody</c> and its byte-identical request
/// twin <c>PostPlaceBusinessContactsInputRequestBody</c> (neither marks any member
/// required, so a single record serves both directions).
/// </summary>
public sealed record PlaceBusinessContacts
{
    /// <summary>Named contacts.</summary>
    [JsonPropertyName("contacts")]
    public IReadOnlyList<PlaceBusinessContact>? Contacts { get; init; }

    /// <summary>Place-level email addresses.</summary>
    [JsonPropertyName("email")]
    public IReadOnlyList<string>? Email { get; init; }

    /// <summary>Place-level phone numbers.</summary>
    [JsonPropertyName("phoneNumbers")]
    public IReadOnlyList<string>? PhoneNumbers { get; init; }
}

/// <summary>
/// One named business contact at a place. Mirrors the spec's
/// <c>PlaceBusinessContactResponseResponseBody</c> and its byte-identical request
/// twin <c>PostPlaceBusinessContactInputRequestBody</c>.
/// </summary>
public sealed record PlaceBusinessContact
{
    /// <summary>Display name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Contact email.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>Contact phone.</summary>
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; init; }
}

/// <summary>
/// The geofence of a place. Mirrors the spec's
/// <c>PlaceGeofenceResponseResponseBody</c>.
/// </summary>
public sealed record PlaceGeofence
{
    /// <summary>
    /// Geofence shape: <c>circle</c>, <c>polygon</c> or <c>unknown</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Circle definition, when <c>type</c> is <c>circle</c>.</summary>
    [JsonPropertyName("circle")]
    public PlaceGeofenceCircle? Circle { get; init; }

    /// <summary>Polygon definition, when <c>type</c> is <c>polygon</c>.</summary>
    [JsonPropertyName("polygon")]
    public PlaceGeofencePolygon? Polygon { get; init; }
}

/// <summary>
/// A circular geofence. Mirrors the spec's
/// <c>PlaceGeofenceCircleResponseResponseBody</c>.
/// </summary>
public sealed record PlaceGeofenceCircle
{
    /// <summary>Circle center latitude in decimal degrees. Spec-required.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Circle center longitude in decimal degrees. Spec-required.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Radius in meters. Spec-required.</summary>
    [JsonPropertyName("radiusMeters")]
    public int? RadiusMeters { get; init; }
}

/// <summary>
/// A polygonal geofence. Mirrors the spec's
/// <c>PlaceGeofencePolygonResponseResponseBody</c>.
/// </summary>
public sealed record PlaceGeofencePolygon
{
    /// <summary>Polygon vertices; at least three. Spec-required.</summary>
    [JsonPropertyName("vertices")]
    public IReadOnlyList<PlaceGeofenceVertex>? Vertices { get; init; }
}

/// <summary>
/// One vertex of a polygonal geofence. Mirrors the spec's
/// <c>PlaceGeofenceVertexResponseResponseBody</c>.
/// </summary>
public sealed record PlaceGeofenceVertex
{
    /// <summary>Latitude in decimal degrees. Spec-required.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec-required.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// Navigation locations for a place. Mirrors the spec's
/// <c>PlaceNavigationResponseResponseBody</c>.
/// </summary>
public sealed record PlaceNavigation
{
    /// <summary>Navigation locations. Spec-required.</summary>
    [JsonPropertyName("locations")]
    public IReadOnlyList<PlaceNavigationLocation>? Locations { get; init; }
}

/// <summary>
/// One navigation location (entrance/exit) at a place. Mirrors the spec's
/// <c>PlaceNavigationLocationResponseResponseBody</c>.
/// </summary>
public sealed record PlaceNavigationLocation
{
    /// <summary>Location name. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Navigation location type: <c>entrance</c>, <c>exit</c> or <c>twoWay</c>.
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("locationType")]
    public string? LocationType { get; init; }

    /// <summary>Latitude in decimal degrees. Spec-required.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec-required.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Driver instructions for this location.</summary>
    [JsonPropertyName("driverInstructions")]
    public string? DriverInstructions { get; init; }
}

/// <summary>
/// Street View configuration for a place. Mirrors the spec's
/// <c>PlaceStreetViewResponseResponseBody</c>.
/// </summary>
public sealed record PlaceStreetView
{
    /// <summary>Whether street view is enabled. Spec-required.</summary>
    [JsonPropertyName("isEnabled")]
    public bool? IsEnabled { get; init; }

    /// <summary>Latitude of the street view camera.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude of the street view camera.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Camera heading clockwise from true north.</summary>
    [JsonPropertyName("headingDegrees")]
    public double? HeadingDegrees { get; init; }

    /// <summary>Camera pitch relative to the Street View horizon.</summary>
    [JsonPropertyName("pitchDegrees")]
    public double? PitchDegrees { get; init; }

    /// <summary>Zoom level.</summary>
    [JsonPropertyName("zoom")]
    public double? Zoom { get; init; }
}

/// <summary>
/// A route-planning row attached to a place. Mirrors the spec's
/// <c>RoutingResponseResponseBody</c> (named <c>PlaceRouting</c> here because the
/// schema is reached only through <c>PlaceResponseObjectResponseBody.routing</c>).
/// </summary>
public sealed record PlaceRouting
{
    /// <summary>Hub (planner) UUID. Spec-required.</summary>
    [JsonPropertyName("hubId")]
    public string? HubId { get; init; }

    /// <summary>Whether this routing row is a depot. Spec-required.</summary>
    [JsonPropertyName("isDepot")]
    public bool? IsDepot { get; init; }

    /// <summary>Default instructions for drivers.</summary>
    [JsonPropertyName("driverInstructions")]
    public string? DriverInstructions { get; init; }

    /// <summary>Hub-facing notes.</summary>
    [JsonPropertyName("hubNotes")]
    public string? HubNotes { get; init; }

    /// <summary>Order service time settings at this stop.</summary>
    [JsonPropertyName("orderServiceTime")]
    public PlaceRoutingOrderServiceTime? OrderServiceTime { get; init; }

    /// <summary>Stop position preference: <c>first</c> or <c>last</c>.</summary>
    [JsonPropertyName("position")]
    public string? Position { get; init; }

    /// <summary>Route priority from 1 (lowest) to 5 (highest).</summary>
    [JsonPropertyName("priority")]
    public int? Priority { get; init; }

    /// <summary>
    /// Required planner skills. Each entry mirrors the spec's
    /// <c>RoutingRequiredSkillResponseResponseBody</c>, whose <c>{ id, name }</c>
    /// shape is exactly the shared <c>EntityReference</c>.
    /// </summary>
    [JsonPropertyName("requiredSkills")]
    public IReadOnlyList<EntityReference>? RequiredSkills { get; init; }

    /// <summary>Customer-defined external identifier within the hub.</summary>
    [JsonPropertyName("routingExternalId")]
    public string? RoutingExternalId { get; init; }

    /// <summary>Additional service time settings.</summary>
    [JsonPropertyName("serviceTime")]
    public PlaceRoutingServiceTime? ServiceTime { get; init; }

    /// <summary>Configured service windows.</summary>
    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<PlaceRoutingServiceWindow>? ServiceWindows { get; init; }
}

/// <summary>
/// Order service time settings on a place's routing row. Mirrors the spec's
/// <c>RoutingOrderServiceTimeResponseResponseBody</c>.
/// </summary>
public sealed record PlaceRoutingOrderServiceTime
{
    /// <summary>Whether order service time settings apply at this stop. Spec-required.</summary>
    [JsonPropertyName("isEnabled")]
    public bool? IsEnabled { get; init; }

    /// <summary>
    /// Mode: <c>unknown</c>, <c>unspecified</c>, <c>fixed</c> or <c>variable</c>.
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("modeType")]
    public string? ModeType { get; init; }

    /// <summary>Capacity-driven service time, for the <c>variable</c> mode.</summary>
    [JsonPropertyName("capacityServiceTime")]
    public PlaceRoutingCapacityServiceTime? CapacityServiceTime { get; init; }

    /// <summary>Fixed order service time in seconds, for the <c>fixed</c> mode.</summary>
    [JsonPropertyName("fixedServiceTimeSeconds")]
    public long? FixedServiceTimeSeconds { get; init; }
}

/// <summary>
/// Capacity-driven order service time. Mirrors the spec's
/// <c>RoutingCapacityServiceTimeResponseResponseBody</c>.
/// </summary>
public sealed record PlaceRoutingCapacityServiceTime
{
    /// <summary>Capacity UUID. Spec-required.</summary>
    [JsonPropertyName("capacityId")]
    public string? CapacityId { get; init; }

    /// <summary>Quantity units per service time chunk. Spec-required.</summary>
    [JsonPropertyName("quantityPerServiceTime")]
    public double? QuantityPerServiceTime { get; init; }

    /// <summary>Service time in seconds. Spec-required.</summary>
    [JsonPropertyName("serviceTimeSeconds")]
    public long? ServiceTimeSeconds { get; init; }
}

/// <summary>
/// Additional service time on a place's routing row. Mirrors the spec's
/// <c>RoutingServiceTimeResponseResponseBody</c>.
/// </summary>
public sealed record PlaceRoutingServiceTime
{
    /// <summary>Whether additional service time is enabled. Spec-required.</summary>
    [JsonPropertyName("isEnabled")]
    public bool? IsEnabled { get; init; }

    /// <summary>Whole minutes of additional service time. Spec-required.</summary>
    [JsonPropertyName("serviceTimeMinutes")]
    public long? ServiceTimeMinutes { get; init; }
}

/// <summary>
/// A recurring local-time service window on a place's routing row. Mirrors the
/// spec's <c>RoutingServiceWindowResponseResponseBody</c>.
/// </summary>
public sealed record PlaceRoutingServiceWindow
{
    /// <summary>Days this window applies. Spec-required.</summary>
    [JsonPropertyName("days")]
    public IReadOnlyList<string>? Days { get; init; }

    /// <summary>Start time as <c>HH:MM:SS</c> in the org timezone. Spec-required.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End time as <c>HH:MM:SS</c> in the org timezone. Spec-required.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }
}

/// <summary>
/// Body for <c>POST /places</c>. Mirrors the spec's
/// <c>PlacesPostPlaceRequestBody</c>.
/// </summary>
public sealed record PlaceCreateRequest
{
    /// <summary>Place name. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Single-line address string. Spec marks REQUIRED.</summary>
    [JsonPropertyName("address")]
    public required string Address { get; init; }

    /// <summary>The geofence for the new place. Spec marks REQUIRED.</summary>
    [JsonPropertyName("geofence")]
    public required PlaceGeofenceInput Geofence { get; init; }

    /// <summary>
    /// Latitude of the place pin (map marker), stored independently of the geofence.
    /// </summary>
    [JsonPropertyName("addressLatitude")]
    public double? AddressLatitude { get; init; }

    /// <summary>
    /// Longitude of the place pin (map marker), stored independently of the geofence.
    /// </summary>
    [JsonPropertyName("addressLongitude")]
    public double? AddressLongitude { get; init; }

    /// <summary>Business contacts to associate with the place.</summary>
    [JsonPropertyName("businessContacts")]
    public PlaceBusinessContacts? BusinessContacts { get; init; }

    /// <summary>
    /// Camera recording mode: <c>fullRecording</c>, <c>driverPrivacy</c>,
    /// <c>completePrivacy</c>, <c>inherit</c>, <c>unknown</c> or <c>unspecified</c>.
    /// </summary>
    [JsonPropertyName("cameraRecordingModeType")]
    public string? CameraRecordingModeType { get; init; }

    /// <summary>External identifiers to associate with the place.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>IFTA exemption types for this place.</summary>
    [JsonPropertyName("iftaExemptionTypes")]
    public IReadOnlyList<string>? IftaExemptionTypes { get; init; }

    /// <summary>When true, show addresses inside the geofence on the map.</summary>
    [JsonPropertyName("isShowAddressesEnabled")]
    public bool? IsShowAddressesEnabled { get; init; }

    /// <summary>Navigation locations for the place.</summary>
    [JsonPropertyName("navigation")]
    public PlaceNavigationInput? Navigation { get; init; }

    /// <summary>Optional notes.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Place type categories to assign.</summary>
    [JsonPropertyName("placeTypes")]
    public IReadOnlyList<string>? PlaceTypes { get; init; }

    /// <summary>Initial route-planning rows for the new place.</summary>
    [JsonPropertyName("routing")]
    public IReadOnlyList<PlaceRoutingInput>? Routing { get; init; }

    /// <summary>Safety event types excluded at this place.</summary>
    [JsonPropertyName("safetyEventExclusions")]
    public IReadOnlyList<string>? SafetyEventExclusions { get; init; }

    /// <summary>Street View configuration.</summary>
    [JsonPropertyName("streetView")]
    public PlaceStreetViewInput? StreetView { get; init; }

    /// <summary>Tags to associate.</summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<PlaceTagInput>? Tags { get; init; }
}

/// <summary>
/// Body for <c>PATCH /places</c>. Mirrors the spec's
/// <c>PlacesPatchPlaceRequestBody</c>. The place is identified by the
/// <c>placeId</c> or <c>externalId</c> query parameter, so every member is optional.
/// </summary>
public sealed record PlaceUpdateRequest
{
    /// <summary>Place name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Single-line address string.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>
    /// Latitude of the place pin (map marker), stored independently of the geofence.
    /// </summary>
    [JsonPropertyName("addressLatitude")]
    public double? AddressLatitude { get; init; }

    /// <summary>
    /// Longitude of the place pin (map marker), stored independently of the geofence.
    /// </summary>
    [JsonPropertyName("addressLongitude")]
    public double? AddressLongitude { get; init; }

    /// <summary>Replacement geofence.</summary>
    [JsonPropertyName("geofence")]
    public PlaceGeofenceInput? Geofence { get; init; }

    /// <summary>Replacement business contacts.</summary>
    [JsonPropertyName("businessContacts")]
    public PlaceBusinessContacts? BusinessContacts { get; init; }

    /// <summary>
    /// Camera recording mode: <c>fullRecording</c>, <c>driverPrivacy</c>,
    /// <c>completePrivacy</c>, <c>inherit</c>, <c>unknown</c> or <c>unspecified</c>.
    /// </summary>
    [JsonPropertyName("cameraRecordingModeType")]
    public string? CameraRecordingModeType { get; init; }

    /// <summary>External identifiers for the place.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>When present, replaces IFTA exemption types for the place.</summary>
    [JsonPropertyName("iftaExemptionTypes")]
    public IReadOnlyList<string>? IftaExemptionTypes { get; init; }

    /// <summary>When true, show addresses inside the geofence on the map.</summary>
    [JsonPropertyName("isShowAddressesEnabled")]
    public bool? IsShowAddressesEnabled { get; init; }

    /// <summary>Replacement navigation locations.</summary>
    [JsonPropertyName("navigation")]
    public PlaceNavigationInput? Navigation { get; init; }

    /// <summary>Notes.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>When present, replaces the place's type categories.</summary>
    [JsonPropertyName("placeTypes")]
    public IReadOnlyList<string>? PlaceTypes { get; init; }

    /// <summary>Routing rows to upsert or remove.</summary>
    [JsonPropertyName("routing")]
    public PlaceRoutingPatchInput? Routing { get; init; }

    /// <summary>When present, replaces safety event exclusions for the place.</summary>
    [JsonPropertyName("safetyEventExclusions")]
    public IReadOnlyList<string>? SafetyEventExclusions { get; init; }

    /// <summary>Replacement Street View configuration.</summary>
    [JsonPropertyName("streetView")]
    public PlaceStreetViewInput? StreetView { get; init; }

    /// <summary>When present, replaces all tag associations for the place.</summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<PlaceTagInput>? Tags { get; init; }
}

/// <summary>
/// A geofence written to a place. Mirrors the spec's
/// <c>PlaceGeofenceInputRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>PlaceGeofence</c>: the request adds the
/// <c>auto</c> suggestion block and accepts a different <c>type</c> enum
/// (<c>circle</c>, <c>polygon</c>, <c>auto</c>).
/// </remarks>
public sealed record PlaceGeofenceInput
{
    /// <summary>Geofence shape: <c>circle</c>, <c>polygon</c> or <c>auto</c>.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Circle definition, when <c>type</c> is <c>circle</c>.</summary>
    [JsonPropertyName("circle")]
    public PlaceGeofenceCircleInput? Circle { get; init; }

    /// <summary>Polygon definition, when <c>type</c> is <c>polygon</c>.</summary>
    [JsonPropertyName("polygon")]
    public PlaceGeofencePolygonInput? Polygon { get; init; }

    /// <summary>Automatic geofence suggestion settings, when <c>type</c> is <c>auto</c>.</summary>
    [JsonPropertyName("auto")]
    public PlaceGeofenceAutoInput? Auto { get; init; }
}

/// <summary>
/// A circular geofence written to a place. Mirrors the spec's
/// <c>PlaceGeofenceCircleInputRequestBody</c>.
/// </summary>
public sealed record PlaceGeofenceCircleInput
{
    /// <summary>Radius in meters; must be positive. Spec marks REQUIRED.</summary>
    [JsonPropertyName("radiusMeters")]
    public required int RadiusMeters { get; init; }

    /// <summary>
    /// Circle center latitude in decimal degrees. Omit on POST to geocode from the
    /// address.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// Circle center longitude in decimal degrees. Omit on POST to geocode from the
    /// address.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// A polygonal geofence written to a place. Mirrors the spec's
/// <c>PlaceGeofencePolygonInputRequestBody</c>.
/// </summary>
public sealed record PlaceGeofencePolygonInput
{
    /// <summary>Polygon vertices; at least three. Spec marks REQUIRED.</summary>
    [JsonPropertyName("vertices")]
    public required IReadOnlyList<PlaceGeofenceVertexInput> Vertices { get; init; }
}

/// <summary>
/// One vertex of a polygonal geofence written to a place. Mirrors the spec's
/// <c>GeofenceVertexInputRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>PlaceGeofenceVertex</c> because the request
/// marks both coordinates REQUIRED.
/// </remarks>
public sealed record PlaceGeofenceVertexInput
{
    /// <summary>Latitude in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}

/// <summary>
/// Automatic geofence suggestion settings. Mirrors the spec's
/// <c>PlaceGeofenceAutoInputRequestBody</c>.
/// </summary>
public sealed record PlaceGeofenceAutoInput
{
    /// <summary>What to do when no candidate is selected.</summary>
    [JsonPropertyName("fallbackBehavior")]
    public PlaceGeofenceAutoFallbackBehaviorInput? FallbackBehavior { get; init; }

    /// <summary>
    /// When true, replace the request name with the winning suggestion's name.
    /// </summary>
    [JsonPropertyName("isSuggestedNameEnabled")]
    public bool? IsSuggestedNameEnabled { get; init; }

    /// <summary>
    /// When true and both the top-level address and seed coordinates are present,
    /// validate the address against the coordinates.
    /// </summary>
    [JsonPropertyName("isValidateAddressEnabled")]
    public bool? IsValidateAddressEnabled { get; init; }

    /// <summary>Seed latitude in WGS84 decimal degrees. Supply with <c>longitude</c>.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Seed longitude in WGS84 decimal degrees. Supply with <c>latitude</c>.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Drop candidates with area above this value, in square meters.</summary>
    [JsonPropertyName("maxAreaSquareMeters")]
    public double? MaxAreaSquareMeters { get; init; }

    /// <summary>Drop candidates whose source polygon exceeds this vertex count.</summary>
    [JsonPropertyName("maxSourceVertices")]
    public long? MaxSourceVertices { get; init; }

    /// <summary>Simplify the selected polygon to at most this many vertices.</summary>
    [JsonPropertyName("maxVertices")]
    public long? MaxVertices { get; init; }

    /// <summary>Restrict the candidate search to this bounding box.</summary>
    [JsonPropertyName("searchBounds")]
    public PlaceGeofenceAutoSearchBoundsInput? SearchBounds { get; init; }

    /// <summary>Candidate ordering: <c>smallestFirst</c> or <c>largestFirst</c>.</summary>
    [JsonPropertyName("sizeOrder")]
    public string? SizeOrder { get; init; }

    /// <summary>
    /// Candidate types in priority order: <c>building</c>, <c>parcel</c>,
    /// <c>landUse</c>, <c>boundary</c>, <c>facility</c>, <c>infrastructure</c>.
    /// </summary>
    [JsonPropertyName("suggestionTypes")]
    public IReadOnlyList<string>? SuggestionTypes { get; init; }
}

/// <summary>
/// Fallback behavior when automatic geofence suggestion finds no candidate.
/// Mirrors the spec's <c>PlaceGeofenceAutoFallbackBehaviorInputRequestBody</c>.
/// </summary>
public sealed record PlaceGeofenceAutoFallbackBehaviorInput
{
    /// <summary>Behavior: <c>fail</c> or <c>circle</c>.</summary>
    [JsonPropertyName("behaviorType")]
    public string? BehaviorType { get; init; }

    /// <summary>Circle to fall back to, when <c>behaviorType</c> is <c>circle</c>.</summary>
    [JsonPropertyName("circle")]
    public PlaceGeofenceAutoFallbackCircleInput? Circle { get; init; }
}

/// <summary>
/// The circle used when automatic geofence suggestion falls back. Mirrors the
/// spec's <c>PlaceGeofenceAutoFallbackCircleInputRequestBody</c>.
/// </summary>
public sealed record PlaceGeofenceAutoFallbackCircleInput
{
    /// <summary>Fallback circle radius in meters; must be positive. Spec marks REQUIRED.</summary>
    [JsonPropertyName("radiusMeters")]
    public required int RadiusMeters { get; init; }
}

/// <summary>
/// Bounding box constraining automatic geofence suggestion. Mirrors the spec's
/// <c>PlaceGeofenceAutoSearchBoundsInputRequestBody</c>.
/// </summary>
public sealed record PlaceGeofenceAutoSearchBoundsInput
{
    /// <summary>Minimum latitude in WGS84 decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("minLatitude")]
    public required double MinLatitude { get; init; }

    /// <summary>Maximum latitude in WGS84 decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("maxLatitude")]
    public required double MaxLatitude { get; init; }

    /// <summary>Minimum longitude in WGS84 decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("minLongitude")]
    public required double MinLongitude { get; init; }

    /// <summary>Maximum longitude in WGS84 decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("maxLongitude")]
    public required double MaxLongitude { get; init; }
}

/// <summary>
/// Navigation locations written to a place. Mirrors the spec's
/// <c>PostPlaceNavigationInputRequestBody</c>.
/// </summary>
public sealed record PlaceNavigationInput
{
    /// <summary>Navigation locations. Spec marks REQUIRED.</summary>
    [JsonPropertyName("locations")]
    public required IReadOnlyList<PlaceNavigationLocationInput> Locations { get; init; }
}

/// <summary>
/// One navigation location written to a place. Mirrors the spec's
/// <c>PostPlaceNavigationLocationInputRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>PlaceNavigationLocation</c> because the request
/// marks name, type and both coordinates REQUIRED.
/// </remarks>
public sealed record PlaceNavigationLocationInput
{
    /// <summary>Display name for the navigation location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Navigation location type: <c>entrance</c>, <c>exit</c> or <c>twoWay</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("locationType")]
    public required string LocationType { get; init; }

    /// <summary>Latitude in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    /// <summary>Optional instructions for drivers.</summary>
    [JsonPropertyName("driverInstructions")]
    public string? DriverInstructions { get; init; }
}

/// <summary>
/// Street View configuration written to a place. Mirrors the spec's
/// <c>PlaceStreetViewInputRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>PlaceStreetView</c> because the request marks
/// <c>isEnabled</c> REQUIRED.
/// </remarks>
public sealed record PlaceStreetViewInput
{
    /// <summary>Whether street view is enabled. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isEnabled")]
    public required bool IsEnabled { get; init; }

    /// <summary>Latitude of the street view camera.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude of the street view camera.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Camera heading clockwise from true north; 0 ≤ headingDegrees &lt; 360.</summary>
    [JsonPropertyName("headingDegrees")]
    public double? HeadingDegrees { get; init; }

    /// <summary>Camera pitch relative to the Street View horizon.</summary>
    [JsonPropertyName("pitchDegrees")]
    public double? PitchDegrees { get; init; }

    /// <summary>Zoom level.</summary>
    [JsonPropertyName("zoom")]
    public double? Zoom { get; init; }
}

/// <summary>
/// A route-planning row written to a place. Mirrors the spec's
/// <c>PlaceRoutingInputRequestBody</c>.
/// </summary>
public sealed record PlaceRoutingInput
{
    /// <summary>Planner hub UUID for this row. Spec marks REQUIRED.</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Whether this routing row is a depot.</summary>
    [JsonPropertyName("isDepot")]
    public bool? IsDepot { get; init; }

    /// <summary>Default instructions for drivers at this stop.</summary>
    [JsonPropertyName("driverInstructions")]
    public string? DriverInstructions { get; init; }

    /// <summary>Hub-facing notes for this routing row.</summary>
    [JsonPropertyName("hubNotes")]
    public string? HubNotes { get; init; }

    /// <summary>Order service time settings for this row.</summary>
    [JsonPropertyName("orderServiceTime")]
    public PlaceRoutingOrderServiceTimeInput? OrderServiceTime { get; init; }

    /// <summary>Stop position preference: <c>first</c> or <c>last</c>.</summary>
    [JsonPropertyName("position")]
    public string? Position { get; init; }

    /// <summary>Route priority from 1 (lowest) to 5 (highest).</summary>
    [JsonPropertyName("priority")]
    public int? Priority { get; init; }

    /// <summary>Required planner skills for this routing row.</summary>
    [JsonPropertyName("requiredSkills")]
    public IReadOnlyList<PlaceRoutingSkillInput>? RequiredSkills { get; init; }

    /// <summary>Customer-defined external identifier within the hub.</summary>
    [JsonPropertyName("routingExternalId")]
    public string? RoutingExternalId { get; init; }

    /// <summary>Additional service time settings.</summary>
    [JsonPropertyName("serviceTime")]
    public PlaceRoutingServiceTimeInput? ServiceTime { get; init; }

    /// <summary>Recurring local-time service windows for this routing row.</summary>
    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<PlaceRoutingServiceWindowInput>? ServiceWindows { get; init; }
}

/// <summary>
/// Routing rows to upsert or remove on <c>PATCH /places</c>. Mirrors the spec's
/// <c>PlaceRoutingPatchInputRequestBody</c>.
/// </summary>
public sealed record PlaceRoutingPatchInput
{
    /// <summary>Rows to create or update, keyed by <c>hubId</c>.</summary>
    [JsonPropertyName("upsert")]
    public IReadOnlyList<PlaceRoutingInput>? Upsert { get; init; }

    /// <summary>Hub ids to delete. Omitted hubs are unchanged.</summary>
    [JsonPropertyName("removeHubIds")]
    public IReadOnlyList<string>? RemoveHubIds { get; init; }
}

/// <summary>
/// Order service time settings written to a place's routing row. Mirrors the
/// spec's <c>PlaceRoutingOrderServiceTimeInputRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>PlaceRoutingOrderServiceTime</c>: the request
/// marks <c>modeType</c> REQUIRED and leaves <c>isEnabled</c> optional, the
/// reverse of the response's required set.
/// </remarks>
public sealed record PlaceRoutingOrderServiceTimeInput
{
    /// <summary>Mode: <c>fixed</c> or <c>variable</c>. Spec marks REQUIRED.</summary>
    [JsonPropertyName("modeType")]
    public required string ModeType { get; init; }

    /// <summary>
    /// When true, order service time settings apply at this stop.
    /// </summary>
    [JsonPropertyName("isEnabled")]
    public bool? IsEnabled { get; init; }

    /// <summary>Capacity-driven service time, for the <c>variable</c> mode.</summary>
    [JsonPropertyName("capacityServiceTime")]
    public PlaceRoutingCapacityServiceTimeInput? CapacityServiceTime { get; init; }

    /// <summary>
    /// Fixed order service time in seconds. Required by the API when
    /// <c>modeType</c> is <c>fixed</c>.
    /// </summary>
    [JsonPropertyName("fixedServiceTimeSeconds")]
    public long? FixedServiceTimeSeconds { get; init; }
}

/// <summary>
/// Capacity-driven order service time written to a place's routing row. Mirrors
/// the spec's <c>PlaceRoutingCapacityServiceTimeInputRequestBody</c>.
/// </summary>
public sealed record PlaceRoutingCapacityServiceTimeInput
{
    /// <summary>Capacity UUID. Spec marks REQUIRED.</summary>
    [JsonPropertyName("capacityId")]
    public required string CapacityId { get; init; }

    /// <summary>Quantity units per service time chunk. Spec marks REQUIRED.</summary>
    [JsonPropertyName("quantityPerServiceTime")]
    public required double QuantityPerServiceTime { get; init; }

    /// <summary>Service time in seconds. Spec marks REQUIRED.</summary>
    [JsonPropertyName("serviceTimeSeconds")]
    public required long ServiceTimeSeconds { get; init; }
}

/// <summary>
/// A planner skill required at a place's routing row. Mirrors the spec's
/// <c>PlaceRoutingRequiredSkillInputRequestBody</c>.
/// </summary>
/// <remarks>
/// The response side carries <c>{ id, name }</c> and reuses the shared
/// <c>EntityReference</c>; the request accepts only <c>id</c>, so it gets its own
/// record.
/// </remarks>
public sealed record PlaceRoutingSkillInput
{
    /// <summary>Planner skill UUID. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// Additional service time written to a place's routing row. Mirrors the spec's
/// <c>PlaceRoutingServiceTimeInputRequestBody</c>.
/// </summary>
public sealed record PlaceRoutingServiceTimeInput
{
    /// <summary>Whether additional service time is enabled. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isEnabled")]
    public required bool IsEnabled { get; init; }

    /// <summary>Whole minutes of additional service time. Spec marks REQUIRED.</summary>
    [JsonPropertyName("serviceTimeMinutes")]
    public required long ServiceTimeMinutes { get; init; }
}

/// <summary>
/// A recurring local-time service window written to a place's routing row.
/// Mirrors the spec's <c>PlaceRoutingServiceWindowInputRequestBody</c>.
/// </summary>
public sealed record PlaceRoutingServiceWindowInput
{
    /// <summary>Days this window applies. Spec marks REQUIRED.</summary>
    [JsonPropertyName("days")]
    public required IReadOnlyList<string> Days { get; init; }

    /// <summary>Start time as <c>HH:MM:SS</c> in the org timezone. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public required string StartTime { get; init; }

    /// <summary>End time as <c>HH:MM:SS</c> in the org timezone. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }
}

/// <summary>
/// A tag association written to a place. Mirrors the spec's
/// <c>PostPlaceTagRefRequestBody</c>.
/// </summary>
/// <remarks>
/// The response side carries <c>{ id, name, parentTagId }</c> and reuses the shared
/// <c>TagReference</c>; the request accepts only <c>id</c>, so it gets its own record.
/// </remarks>
public sealed record PlaceTagInput
{
    /// <summary>Numeric Samsara tag id. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// One forward-geocode candidate for an address, returned by
/// <c>GET /places/geocode</c> (<c>getPlaceGeocode</c>, beta). Mirrors the spec's
/// <c>PlaceGeocodeResultResponseResponseBody</c>.
/// </summary>
public sealed record PlaceGeocodeResult
{
    /// <summary>Latitude in WGS84 decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in WGS84 decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// One geofence suggestion candidate around a seed point, returned by
/// <c>GET /places/geofence</c> (<c>getPlaceGeofence</c>, beta). Mirrors the
/// spec's <c>PlaceGeofenceSuggestionCandidateResponseResponseBody</c>.
/// </summary>
/// <remarks>
/// The spec's envelope also carries a sibling <c>recommended</c> candidate
/// alongside <c>data</c> and <c>pagination</c>. The SDK surfaces the paginated
/// <c>data</c> array; the recommended candidate is the first element of the
/// first page in the server's chosen sort order (<c>sizeOrder</c>).
/// </remarks>
public sealed record PlaceGeofenceSuggestion
{
    /// <summary>Candidate name from map data when available. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Candidate source: <c>building</c>, <c>parcel</c>, <c>landUse</c>,
    /// <c>boundary</c>, <c>facility</c>, <c>infrastructure</c> or <c>unknown</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Candidate area in square meters. Spec marks REQUIRED.</summary>
    [JsonPropertyName("areaSquareMeters")]
    public double? AreaSquareMeters { get; init; }

    /// <summary>The suggested geofence geometry. Spec marks REQUIRED.</summary>
    [JsonPropertyName("geofence")]
    public PlaceGeofence? Geofence { get; init; }
}
