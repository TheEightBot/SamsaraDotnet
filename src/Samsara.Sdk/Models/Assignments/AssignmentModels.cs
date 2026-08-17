namespace Samsara.Sdk.Models.Assignments;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a driver-to-vehicle assignment. Mirrors the spec's
/// <c>DriverVehicleAssignmentV2ObjectResponseBody</c> for the GET list response,
/// and is reused for the POST/PATCH responses whose payloads only carry
/// <see cref="Message"/>.
/// </summary>
public sealed record DriverVehicleAssignment
{
    /// <summary>
    /// The driver this assignment is for (nested object). Spec-required on the
    /// <c>GET</c> list response; null on the <c>POST</c>/<c>PATCH</c> responses, which
    /// return only <see cref="Message"/>.
    /// </summary>
    [JsonPropertyName("driver")]
    public DriverVehicleAssignmentDriver? Driver { get; init; }

    /// <summary>
    /// The vehicle this assignment is for (nested object). Spec-required on the
    /// <c>GET</c> list response; null on the <c>POST</c>/<c>PATCH</c> responses, which
    /// return only <see cref="Message"/>.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public DriverVehicleAssignmentVehicle? Vehicle { get; init; }

    /// <summary>
    /// Indicates whether the driver is a passenger. Spec-required on the <c>GET</c> list
    /// response; null on the <c>POST</c>/<c>PATCH</c> responses.
    /// </summary>
    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }

    /// <summary>
    /// Start time of the assignment in RFC 3339 format (e.g., <c>2019-06-13T19:08:25Z</c>).
    /// Spec-required on the <c>GET</c> list response; null on the <c>POST</c>/<c>PATCH</c>
    /// responses.
    /// </summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>
    /// Time at which the assignment was made, in RFC 3339 format. Optional.
    /// </summary>
    [JsonPropertyName("assignedAtTime")]
    public string? AssignedAtTime { get; init; }

    /// <summary>
    /// End time of the assignment in RFC 3339 format. Omitted while the assignment is ongoing.
    /// </summary>
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    /// <summary>
    /// Name of the assigning source for the driver assignment record. Valid values:
    /// <c>invalid</c>, <c>unknown</c>, <c>HOS</c>, <c>idCard</c>, <c>static</c>, <c>faceId</c>,
    /// <c>tachograph</c>, <c>safetyManual</c>, <c>RFID</c>, <c>trailer</c>, <c>external</c>,
    /// <c>qrCode</c>, <c>driverApp</c>, <c>voiceSignIn</c>, <c>smartAssign</c>.
    /// </summary>
    [JsonPropertyName("assignmentType")]
    public string? AssignmentType { get; init; }

    /// <summary>
    /// Metadata object for external assignment source data.
    /// </summary>
    [JsonPropertyName("metadata")]
    public DriverVehicleAssignmentMetadata? Metadata { get; init; }

    /// <summary>
    /// Outcome message returned by POST and PATCH responses (e.g., "Driver assignment was
    /// successfully submitted"). Not present on GET payloads.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }
}

/// <summary>
/// Driver associated with a driver-vehicle assignment. Mirrors the spec's
/// <c>GoaDriverTinyResponseResponseBody</c>.
/// </summary>
public sealed record DriverVehicleAssignmentDriver
{
    /// <summary>ID of the driver. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Map of external IDs for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Vehicle associated with a driver-vehicle assignment. Mirrors the spec's
/// <c>GoaVehicleTinyResponseResponseBody</c>.
/// </summary>
public sealed record DriverVehicleAssignmentVehicle
{
    /// <summary>ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Map of external IDs for the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Metadata about a driver-vehicle assignment. Mirrors the spec's
/// <c>DriverAssignmentMetadataTinyObjectResponseBody</c>.
/// </summary>
public sealed record DriverVehicleAssignmentMetadata
{
    /// <summary>Assigned source name from an external source.</summary>
    [JsonPropertyName("sourceName")]
    public string? SourceName { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/driver-vehicle-assignments</c>.
/// </summary>
public sealed record CreateDriverVehicleAssignmentRequest
{
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    [JsonPropertyName("assignedAtTime")]
    public DateTimeOffset? AssignedAtTime { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }

    [JsonPropertyName("metadata")]
    public IReadOnlyDictionary<string, string>? Metadata { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /fleet/driver-vehicle-assignments</c>. The composite identifier
/// (driverId + vehicleId + startTime) lives in the body.
/// </summary>
public sealed record UpdateDriverVehicleAssignmentRequest
{
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    [JsonPropertyName("startTime")]
    public required DateTimeOffset StartTime { get; init; }

    [JsonPropertyName("assignedAtTime")]
    public DateTimeOffset? AssignedAtTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }

    [JsonPropertyName("metadata")]
    public IReadOnlyDictionary<string, string>? Metadata { get; init; }
}

/// <summary>
/// Request body for <c>DELETE /fleet/driver-vehicle-assignments</c>. Identifies the assignment
/// to end via the vehicleId (and optional matching fields).
/// </summary>
public sealed record DeleteDriverVehicleAssignmentsRequest
{
    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    [JsonPropertyName("assignedAtTime")]
    public DateTimeOffset? AssignedAtTime { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }
}

/// <summary>
/// Response body of <c>GET /v1/fleet/trailers/assignments</c>: a top-level
/// <c>trailers</c> array beside a top-level <c>pagination</c> block, with no
/// <c>data</c> envelope. Mirrors the spec's <c>inline_response_200_7</c> schema.
/// </summary>
/// <remarks>
/// This is the page envelope, not the page item. The item is
/// <see cref="V1TrailerWithAssignments"/> (spec <c>V1TrailerAssignmentsResponse</c>),
/// which is also the entire body of the per-trailer endpoint
/// <c>GET /v1/fleet/trailers/{trailerId}/assignments</c>.
/// </remarks>
public sealed record V1TrailerAssignmentsListResponse
{
    /// <summary>The trailers on this page, each with its driver assignment rows.</summary>
    [JsonPropertyName("trailers")]
    public IReadOnlyList<V1TrailerWithAssignments>? Trailers { get; init; }

    /// <summary>Bidirectional cursor pagination metadata (spec schema <c>V1Pagination</c>).
    /// This endpoint's forward cursor is spent on the <c>startingAfter</c> query
    /// parameter, not the v2 <c>after</c>.</summary>
    [JsonPropertyName("pagination")]
    public V1PaginationInfo? Pagination { get; init; }
}

/// <summary>
/// A trailer and its driver assignment rows. This is both the item type of the
/// <c>trailers</c> array on <c>GET /v1/fleet/trailers/assignments</c> and the whole
/// (non-paginated) body of <c>GET /v1/fleet/trailers/{trailerId}/assignments</c>.
/// Mirrors the spec's <c>V1TrailerAssignmentsResponse</c> schema (the composition of
/// <c>V1TrailerBase</c> and <c>V1TrailerAssignmentsResponse_allOf</c>).
/// </summary>
/// <remarks>
/// The <c>V1</c> prefix follows the existing legacy-model convention in this SDK
/// (<c>V1Trip</c>, <c>V1Sensor</c>, <c>V1MessageSender</c>) and keeps this v1
/// shape distinct from the v2 trailer models in
/// <c>Samsara.Sdk.Models.Fleet</c>.
/// </remarks>
public sealed record V1TrailerWithAssignments
{
    /// <summary>ID of the trailer. Spec type is int64. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Trailer name, given when the trailer was created via the trailer
    /// portal. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The driver assignment rows for this trailer.</summary>
    [JsonPropertyName("trailerAssignments")]
    public IReadOnlyList<V1TrailerAssignmentEntry>? TrailerAssignments { get; init; }
}

/// <summary>
/// A single driver-to-trailer assignment row on a v1 trailer assignments
/// response. Mirrors the spec's <c>V1TrailerAssignmentResponse</c> schema.
/// </summary>
/// <remarks>
/// Named <c>V1TrailerAssignmentEntry</c> rather than the stripped spec name
/// <c>V1TrailerAssignment</c>, which would read as a sibling of the enclosing
/// <see cref="V1TrailerWithAssignments"/> rather than as one of its rows.
/// </remarks>
public sealed record V1TrailerAssignmentEntry
{
    /// <summary>The ID of the driver associated with this trailer. Spec type is int64.</summary>
    [JsonPropertyName("driverId")]
    public long? DriverId { get; init; }

    /// <summary>The time at which the driver started the assignment, in
    /// milliseconds since the Unix epoch.</summary>
    [JsonPropertyName("startMs")]
    public long? StartMs { get; init; }

    /// <summary>The time at which the driver ended the assignment, in
    /// milliseconds since the Unix epoch. Omitted while the assignment is
    /// current.</summary>
    [JsonPropertyName("endMs")]
    public long? EndMs { get; init; }
}

/// <summary>
/// Bidirectional cursor pagination metadata on a legacy v1 response. Mirrors the
/// spec's <c>V1Pagination</c> schema.
/// </summary>
/// <remarks>
/// Distinct from <c>Samsara.Sdk.Pagination.PaginationInfo</c>, which mirrors the
/// v2 <c>paginationResponse</c> schema and carries only the forward cursor; the
/// v1 shape adds <see cref="StartCursor"/> and <see cref="HasPrevPage"/>.
/// </remarks>
public sealed record V1PaginationInfo
{
    /// <summary>Cursor identifier representing the first element in the response,
    /// for use with a subsequent request's <c>endingBefore</c> parameter. Spec
    /// marks REQUIRED.</summary>
    [JsonPropertyName("startCursor")]
    public string? StartCursor { get; init; }

    /// <summary>Cursor identifier representing the last element in the response,
    /// for use with a subsequent request's <c>startingAfter</c> parameter. Spec
    /// marks REQUIRED.</summary>
    [JsonPropertyName("endCursor")]
    public string? EndCursor { get; init; }

    /// <summary>True if there are more pages of results after this response.
    /// Spec marks REQUIRED.</summary>
    [JsonPropertyName("hasNextPage")]
    public bool? HasNextPage { get; init; }

    /// <summary>True if there are more pages of results before this response.
    /// Spec marks REQUIRED.</summary>
    [JsonPropertyName("hasPrevPage")]
    public bool? HasPrevPage { get; init; }
}

/// <summary>
/// Represents a carrier-proposed assignment.
/// </summary>
public sealed record CarrierProposedAssignment
{
    /// <summary>Samsara ID for the assignment.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Time after which this assignment will be active and visible to the driver on the mobile app.
    /// RFC 3339 format (e.g., <c>2020-01-27T07:06:25Z</c>). Spec-required.
    /// </summary>
    [JsonPropertyName("activeTime")]
    public required string ActiveTime { get; init; }

    /// <summary>
    /// Time when the driver accepted this assignment in the mobile app. Omitted if not yet accepted.
    /// RFC 3339 format.
    /// </summary>
    [JsonPropertyName("acceptedTime")]
    public string? AcceptedTime { get; init; }

    /// <summary>
    /// Time when the driver first saw this assignment in the mobile app. Omitted if not yet seen.
    /// RFC 3339 format.
    /// </summary>
    [JsonPropertyName("firstSeenTime")]
    public string? FirstSeenTime { get; init; }

    /// <summary>
    /// Time when the driver rejected this assignment in the mobile app. Omitted if not rejected.
    /// RFC 3339 format.
    /// </summary>
    [JsonPropertyName("rejectedTime")]
    public string? RejectedTime { get; init; }

    /// <summary>Shipping documents proposed to the driver. Maximum length 40 characters.</summary>
    [JsonPropertyName("shippingDocs")]
    public string? ShippingDocs { get; init; }

    /// <summary>The driver this assignment is for (nested object per spec).</summary>
    [JsonPropertyName("driver")]
    public CarrierProposedAssignmentDriver? Driver { get; init; }

    /// <summary>The vehicle proposed to the driver (nested object per spec).</summary>
    [JsonPropertyName("vehicle")]
    public CarrierProposedAssignmentVehicle? Vehicle { get; init; }

    /// <summary>The trailers proposed to the driver (nested array per spec).</summary>
    [JsonPropertyName("trailers")]
    public IReadOnlyList<CarrierProposedAssignmentTrailer>? Trailers { get; init; }
}

/// <summary>
/// Driver associated with a carrier-proposed assignment. Mirrors the spec's
/// <c>CarrierProposedAssignmentDriver</c> (<c>driverTinyResponse</c> + external IDs).
/// </summary>
public sealed record CarrierProposedAssignmentDriver
{
    /// <summary>ID of the driver.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External IDs for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Vehicle associated with a carrier-proposed assignment. Mirrors the spec's
/// <c>CarrierProposedAssignmentVehicle</c> (<c>vehicleTinyResponse</c>).
/// </summary>
public sealed record CarrierProposedAssignmentVehicle
{
    /// <summary>ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External IDs for the vehicle. Spec spells this property with a capital E.</summary>
    [JsonPropertyName("ExternalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Trailer associated with a carrier-proposed assignment. Mirrors the spec's
/// <c>CarrierProposedAssignmentTrailer</c> (<c>trailerTinyResponse</c> + external IDs).
/// </summary>
public sealed record CarrierProposedAssignmentTrailer
{
    /// <summary>ID of the trailer.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the trailer.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External IDs for the trailer.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Request body for creating a carrier-proposed assignment.
/// </summary>
public sealed record CreateCarrierProposedAssignmentRequest
{
    /// <summary>
    /// ID for the driver this assignment is for. Spec-required. May be a Samsara ID or external ID.
    /// </summary>
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    /// <summary>
    /// ID for the vehicle to propose. Spec-required. May be a Samsara ID or external ID.
    /// </summary>
    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    /// <summary>
    /// Time after which this assignment will be active and visible to the driver. Not setting it
    /// makes it active immediately. RFC 3339 format (e.g., <c>2020-01-27T07:06:25Z</c>).
    /// </summary>
    [JsonPropertyName("activeTime")]
    public string? ActiveTime { get; init; }

    /// <summary>
    /// Shipping documents proposed to the driver. Maximum length 40 characters.
    /// </summary>
    [JsonPropertyName("shippingDocs")]
    public string? ShippingDocs { get; init; }

    /// <summary>
    /// IDs of trailers to propose. Each may be a Samsara ID or external ID.
    /// Forbidden if <see cref="TrailerNames"/> is set.
    /// </summary>
    [JsonPropertyName("trailerIds")]
    public IReadOnlyList<string>? TrailerIds { get; init; }

    /// <summary>
    /// Names of trailers to propose. Forbidden if <see cref="TrailerIds"/> is set.
    /// </summary>
    [JsonPropertyName("trailerNames")]
    public IReadOnlyList<string>? TrailerNames { get; init; }
}

/// <summary>
/// Request body for updating a carrier-proposed assignment.
/// </summary>
public sealed record UpdateCarrierProposedAssignmentRequest
{
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

/// <summary>
/// Represents a driver-to-trailer assignment. Mirrors the spec's
/// <c>GetDriverTrailerAssignmentsResponseBody</c> inner schema, which nests the
/// driver and trailer references rather than emitting flat scalars.
/// </summary>
public sealed record DriverTrailerAssignment
{
    /// <summary>Samsara ID of the driver-trailer assignment. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The driver this assignment is for, as a nested object. Returned by
    /// <c>GET /driver-trailer-assignments</c> (where the spec marks it required); the
    /// <c>POST</c>/<c>PATCH</c> responses instead carry the flat <see cref="DriverId"/>
    /// scalar and omit this object, so it is nullable on the shared record.
    /// </summary>
    [JsonPropertyName("driver")]
    public DriverTrailerAssignmentDriver? Driver { get; init; }

    /// <summary>
    /// The trailer this assignment is for, as a nested object. Returned by
    /// <c>GET /driver-trailer-assignments</c> (where the spec marks it required); the
    /// <c>POST</c>/<c>PATCH</c> responses instead carry the flat <see cref="TrailerId"/>
    /// scalar and omit this object, so it is nullable on the shared record.
    /// </summary>
    [JsonPropertyName("trailer")]
    public DriverTrailerAssignmentTrailer? Trailer { get; init; }

    /// <summary>
    /// Time when the driver-trailer assignment starts, in RFC 3339 format
    /// (e.g., <c>2019-06-13T19:08:25Z</c>). Spec-required.
    /// </summary>
    [JsonPropertyName("startTime")]
    public required string StartTime { get; init; }

    /// <summary>
    /// Time when the driver-trailer assignment was created, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>
    /// Time when the driver-trailer assignment will end, in RFC 3339 format.
    /// Omitted while the assignment is still active.
    /// </summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>
    /// Time when the driver-trailer assignment was last updated, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }

    /// <summary>
    /// The assignment's driver ID as a flat scalar. Returned by the
    /// <c>POST</c>/<c>PATCH /driver-trailer-assignments</c> responses (which omit the nested
    /// <see cref="Driver"/> object); null on the <c>GET</c> list response, which nests the
    /// driver under <see cref="Driver"/> instead.
    /// </summary>
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    /// <summary>
    /// The assignment's trailer ID as a flat scalar. Returned by the
    /// <c>POST</c>/<c>PATCH /driver-trailer-assignments</c> responses (which omit the nested
    /// <see cref="Trailer"/> object); null on the <c>GET</c> list response, which nests the
    /// trailer under <see cref="Trailer"/> instead.
    /// </summary>
    [JsonPropertyName("trailerId")]
    public string? TrailerId { get; init; }
}

/// <summary>
/// Driver associated with a driver-trailer assignment. Mirrors the spec's
/// <c>DriverWithExternalIdObjectResponseBody</c> (driver id + external ids map).
/// </summary>
public sealed record DriverTrailerAssignmentDriver
{
    /// <summary>Samsara ID of the driver. Spec-required.</summary>
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    /// <summary>Map of external IDs for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Trailer associated with a driver-trailer assignment. Mirrors the spec's
/// <c>TrailerObjectResponseBody</c>.
/// </summary>
public sealed record DriverTrailerAssignmentTrailer
{
    /// <summary>Samsara ID of the trailer. Spec-required.</summary>
    [JsonPropertyName("trailerId")]
    public required string TrailerId { get; init; }
}

/// <summary>Request body for creating a driver-trailer assignment.</summary>
public sealed record CreateDriverTrailerAssignmentRequest
{
    /// <summary>
    /// ID of the driver. May be a Samsara ID or an
    /// <see href="https://developers.samsara.com/docs/external-ids">external ID</see>.
    /// </summary>
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    /// <summary>
    /// ID of the trailer. May be a Samsara ID or an
    /// <see href="https://developers.samsara.com/docs/external-ids">external ID</see>.
    /// </summary>
    [JsonPropertyName("trailerId")]
    public required string TrailerId { get; init; }

    /// <summary>
    /// Start time in RFC 3339 format. The time needs to be current or within the past
    /// 7 days. Defaults to now if not provided.
    /// </summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }
}

/// <summary>
/// Request body for updating an existing driver-trailer assignment. The assignment
/// identifier travels in the query string (<c>id</c>), so the body only carries the
/// end time per spec.
/// </summary>
public sealed record UpdateDriverTrailerAssignmentRequest
{
    /// <summary>
    /// End time in RFC 3339 format. The end time must not be in the future. Spec-required.
    /// </summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }
}
