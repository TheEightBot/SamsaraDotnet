namespace Samsara.Sdk.Models.Assignments;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a driver-to-vehicle assignment.
/// </summary>
public sealed record DriverVehicleAssignment
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }

    [JsonPropertyName("assignmentType")]
    public string? AssignmentType { get; init; }
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
/// Represents a trailer assignment to a vehicle.
/// </summary>
public sealed record TrailerAssignment
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("trailerId")]
    public string? TrailerId { get; init; }

    [JsonPropertyName("trailerName")]
    public string? TrailerName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
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

    /// <summary>
    /// Convenience accessor for the assignment's driver ID. Retained alongside the nested
    /// <see cref="Driver"/> object for backward compatibility with earlier SDK shapes; not
    /// part of the spec inner schema.
    /// </summary>
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    /// <summary>
    /// Convenience accessor for the assignment's driver name. Retained alongside the nested
    /// <see cref="Driver"/> object for backward compatibility with earlier SDK shapes; not
    /// part of the spec inner schema.
    /// </summary>
    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    /// <summary>
    /// Convenience accessor for the assignment's vehicle ID. Retained alongside the nested
    /// <see cref="Vehicle"/> object for backward compatibility with earlier SDK shapes; not
    /// part of the spec inner schema.
    /// </summary>
    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    /// <summary>
    /// Convenience accessor for the assignment's vehicle name. Retained alongside the nested
    /// <see cref="Vehicle"/> object for backward compatibility with earlier SDK shapes; not
    /// part of the spec inner schema.
    /// </summary>
    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }
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
    /// The driver this assignment is for (nested object per spec). Spec-required.
    /// </summary>
    [JsonPropertyName("driver")]
    public required DriverTrailerAssignmentDriver Driver { get; init; }

    /// <summary>
    /// The trailer this assignment is for (nested object per spec). Spec-required.
    /// </summary>
    [JsonPropertyName("trailer")]
    public required DriverTrailerAssignmentTrailer Trailer { get; init; }

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
    /// Convenience accessor for the assignment's driver ID. Retained alongside the nested
    /// <see cref="Driver"/> object for backward compatibility with earlier SDK shapes; not
    /// part of the spec inner schema.
    /// </summary>
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    /// <summary>
    /// Convenience accessor for the assignment's driver name. Retained for backward
    /// compatibility with earlier SDK shapes; not part of the spec inner schema.
    /// </summary>
    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    /// <summary>
    /// Convenience accessor for the assignment's trailer ID. Retained alongside the nested
    /// <see cref="Trailer"/> object for backward compatibility with earlier SDK shapes; not
    /// part of the spec inner schema.
    /// </summary>
    [JsonPropertyName("trailerId")]
    public string? TrailerId { get; init; }

    /// <summary>
    /// Convenience accessor for the assignment's trailer name. Retained for backward
    /// compatibility with earlier SDK shapes; not part of the spec inner schema.
    /// </summary>
    [JsonPropertyName("trailerName")]
    public string? TrailerName { get; init; }

    /// <summary>
    /// Legacy assignment timestamp. Retained for backward compatibility with earlier SDK
    /// shapes; not part of the spec inner schema. Prefer <see cref="StartTime"/> /
    /// <see cref="EndTime"/> / <see cref="CreatedAtTime"/> / <see cref="UpdatedAtTime"/>.
    /// </summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }
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
