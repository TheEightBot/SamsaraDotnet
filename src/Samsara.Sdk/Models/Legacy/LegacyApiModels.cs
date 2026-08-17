namespace Samsara.Sdk.Models.Legacy;

using System.Text.Json.Serialization;

/// <summary>
/// A driver together with their vehicle assignments over the requested window —
/// the <c>data[]</c> item of <c>GET /fleet/drivers/vehicle-assignments</c>.
/// Mirrors the spec's <c>DriversVehicleAssignmentsObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>driverActivationStatus</c>, <c>id</c>, <c>name</c> and
/// <c>vehicleAssignments</c> REQUIRED; every property stays nullable because this
/// is a response record.
/// </remarks>
public sealed record LegacyDriverVehicleAssignments
{
    /// <summary>Samsara ID of the driver. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the driver. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Activation status of the driver: <c>active</c> or <c>deactivated</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("driverActivationStatus")]
    public string? DriverActivationStatus { get; init; }

    /// <summary>A map of external IDs for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The driver's vehicle assignments in the requested window. Each item
    /// mirrors the spec's <c>VehicleAssignmentObjectResponseBody</c>. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("vehicleAssignments")]
    public IReadOnlyList<LegacyVehicleAssignment>? VehicleAssignments { get; init; }
}

/// <summary>
/// A single vehicle assignment on a driver returned by
/// <c>GET /fleet/drivers/vehicle-assignments</c>. Mirrors the spec's
/// <c>VehicleAssignmentObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>assignmentType</c>, <c>isPassenger</c>, <c>startTime</c> and
/// <c>vehicle</c> REQUIRED; every property stays nullable because this is a
/// response record.
/// </remarks>
public sealed record LegacyVehicleAssignment
{
    /// <summary>
    /// How the assignment was created. The spec enumerates only
    /// <c>driverApp</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("assignmentType")]
    public string? AssignmentType { get; init; }

    /// <summary>Start of the assignment (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End of the assignment (RFC 3339). Absent while the assignment is open.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>Whether the driver was a passenger rather than the operator. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }

    /// <summary>The vehicle the driver was assigned to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("vehicle")]
    public LegacyVehicleRef? Vehicle { get; init; }
}

/// <summary>
/// A vehicle together with its driver assignments over the requested window —
/// the <c>data[]</c> item of <c>GET /fleet/vehicles/driver-assignments</c>.
/// Mirrors the spec's <c>VehiclesDriverAssignmentsObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>driverAssignments</c> and <c>id</c> REQUIRED; every property
/// stays nullable because this is a response record.
/// </remarks>
public sealed record LegacyVehicleDriverAssignments
{
    /// <summary>Samsara ID of the vehicle. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external IDs for the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The vehicle's driver assignments in the requested window. Each item
    /// mirrors the spec's <c>DriverAssignmentObjectResponseBody</c>. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("driverAssignments")]
    public IReadOnlyList<LegacyDriverAssignment>? DriverAssignments { get; init; }
}

/// <summary>
/// A single driver assignment on a vehicle returned by
/// <c>GET /fleet/vehicles/driver-assignments</c>. Mirrors the spec's
/// <c>DriverAssignmentObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// This is a distinct schema from <see cref="LegacyVehicleAssignment"/>: the spec
/// marks nothing required here, and the reference it carries is a driver rather
/// than a vehicle.
/// </remarks>
public sealed record LegacyDriverAssignment
{
    /// <summary>
    /// How the assignment was created. The spec enumerates only <c>driverApp</c>.
    /// </summary>
    [JsonPropertyName("assignmentType")]
    public string? AssignmentType { get; init; }

    /// <summary>Start of the assignment (RFC 3339).</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End of the assignment (RFC 3339). Absent while the assignment is open.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>Whether the driver was a passenger rather than the operator.</summary>
    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }

    /// <summary>The driver assigned to the vehicle.</summary>
    [JsonPropertyName("driver")]
    public LegacyDriverRef? Driver { get; init; }
}

/// <summary>
/// A minified vehicle reference on a legacy fleet response. Mirrors the spec's
/// <c>GoaVehicleTinyResponseResponseBody</c>, reached from
/// <see cref="LegacyVehicleAssignment.Vehicle"/> and
/// <see cref="LegacyIdlingReportEvent.Vehicle"/>.
/// </summary>
/// <remarks>
/// This is the lower-case <c>externalIds</c> spelling. The v1 DVIR/defect schema
/// <c>vehicleTinyResponse</c> spells the same map <c>ExternalIds</c> with a
/// capital E — see <see cref="V1SafetyEventVehicle"/> and the 2026-08-17b design
/// note in <c>docs/api-sync/30-maintenance.md</c>.
/// </remarks>
public sealed record LegacyVehicleRef
{
    /// <summary>Samsara ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external IDs for the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A minified driver reference on a legacy fleet response. Mirrors the spec's
/// <c>GoaDriverTinyResponseResponseBody</c>, reached from
/// <see cref="LegacyDriverAssignment.Driver"/>.
/// </summary>
/// <remarks>Spec marks <c>id</c> REQUIRED; it stays nullable because this is a response record.</remarks>
public sealed record LegacyDriverRef
{
    /// <summary>Samsara ID of the driver. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external IDs for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A single idling event — the <c>data[]</c> item of
/// <c>GET /fleet/reports/vehicle/idling</c>. Mirrors the spec's
/// <c>IdlingReportEventResponseBody</c>.
/// </summary>
/// <remarks>
/// <para>
/// Distinct from <c>Samsara.Sdk.Models.Fleet.IdlingEvent</c>, which mirrors the v2
/// <c>/idling-events</c> surface and carries fuel-cost, operator and PTO-state
/// objects this legacy report does not define.
/// </para>
/// <para>
/// The spec marks every property except nothing optional — <c>address</c>,
/// <c>durationMs</c>, <c>endTime</c>, <c>fuelConsumptionMl</c>,
/// <c>isPtoActive</c>, <c>startTime</c> and <c>vehicle</c> are all REQUIRED — but
/// they stay nullable because this is a response record.
/// </para>
/// </remarks>
public sealed record LegacyIdlingReportEvent
{
    /// <summary>Where the vehicle was idling. Spec marks REQUIRED.</summary>
    [JsonPropertyName("address")]
    public LegacyIdlingReportAddress? Address { get; init; }

    /// <summary>Duration of the idling event in milliseconds. Spec marks REQUIRED.</summary>
    [JsonPropertyName("durationMs")]
    public long? DurationMs { get; init; }

    /// <summary>Start of the idling event (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End of the idling event (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>Fuel consumed while idling, in milliliters. Spec marks REQUIRED.</summary>
    [JsonPropertyName("fuelConsumptionMl")]
    public double? FuelConsumptionMl { get; init; }

    /// <summary>Whether Power Take-Off was active during the event. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isPtoActive")]
    public bool? IsPtoActive { get; init; }

    /// <summary>The vehicle that was idling. Spec marks REQUIRED.</summary>
    [JsonPropertyName("vehicle")]
    public LegacyVehicleRef? Vehicle { get; init; }
}

/// <summary>
/// The location of an idling event. Mirrors the spec's
/// <c>IdlingReportEventAddressResponseBody</c>.
/// </summary>
/// <remarks>
/// This is a formatted-address-plus-coordinates value, not a geofence reference —
/// it is a different schema from <c>Samsara.Sdk.Models.Fleet.IdlingEventAddress</c>,
/// which carries a geofence <c>id</c> and <c>addressTypes</c>. Spec marks all
/// three properties REQUIRED; they stay nullable because this is a response record.
/// </remarks>
public sealed record LegacyIdlingReportAddress
{
    /// <summary>Human-readable address of the idling location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("formatted")]
    public string? Formatted { get; init; }

    /// <summary>Latitude of the idling location, in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude of the idling location, in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// An entry in the safety-event audit log feed — the <c>data[]</c> item of
/// <c>GET /fleet/safety-events/audit-logs/feed</c>. Mirrors the spec's
/// <c>SafetyEventActivityFeedItemResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>id</c>, <c>safetyEvent</c>, <c>time</c> and <c>type</c>
/// REQUIRED; they stay nullable because this is a response record.
/// </remarks>
public sealed record LegacySafetyEventActivity
{
    /// <summary>Samsara ID of the activity entry. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Kind of activity recorded: <c>BehaviorLabelActivityType</c>,
    /// <c>CoachingStateActivityType</c> or <c>CreateSafetyEventActivityType</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Time the activity was recorded (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("time")]
    public string? Time { get; init; }

    /// <summary>The safety event the activity applies to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("safetyEvent")]
    public LegacySafetyEventSummary? SafetyEvent { get; init; }
}

/// <summary>
/// The safety event carried on an audit-feed entry. Mirrors the spec's
/// <c>SafetyEventObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// This is a summary shape specific to the audit feed — it is neither
/// <c>Samsara.Sdk.Models.Safety.SafetyEvent</c> (the v2
/// <c>SafetyEventV2ObjectResponseBody</c>) nor <see cref="V1SafetyEvent"/> (the
/// legacy <c>SafetyEvent</c> schema).
/// </remarks>
public sealed record LegacySafetyEventSummary
{
    /// <summary>Samsara ID of the safety event.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Universally unique identifier of the safety event.</summary>
    [JsonPropertyName("uuid")]
    public string? Uuid { get; init; }

    /// <summary>Time of the safety event (RFC 3339).</summary>
    [JsonPropertyName("time")]
    public string? Time { get; init; }

    /// <summary>Behavior labels attached to the safety event.</summary>
    [JsonPropertyName("behaviorLabels")]
    public IReadOnlyList<LegacySafetyEventBehaviorLabel>? BehaviorLabels { get; init; }

    /// <summary>The driver on the safety event.</summary>
    [JsonPropertyName("driver")]
    public LegacySafetyEventEntityRef? Driver { get; init; }

    /// <summary>The vehicle on the safety event.</summary>
    [JsonPropertyName("vehicle")]
    public LegacySafetyEventEntityRef? Vehicle { get; init; }
}

/// <summary>
/// A behavior label on an audit-feed safety event. Mirrors the spec's
/// <c>SafetyEventBehaviorLabelsResponseBody</c>.
/// </summary>
/// <remarks>
/// Distinct from <see cref="V1SafetyEventBehaviorLabel"/>: this shape carries
/// <c>name</c> and <c>type</c>, the legacy list shape carries <c>label</c>,
/// <c>name</c> and <c>source</c>.
/// </remarks>
public sealed record LegacySafetyEventBehaviorLabel
{
    /// <summary>Display name of the behavior label.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Behavior label type (e.g. <c>Acceleration</c>, <c>Braking</c>,
    /// <c>Crash</c>, <c>Speeding</c>). See the spec for the full enumeration.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// A bare <c>{ id }</c> reference on an audit-feed safety event. Mirrors the
/// spec's <c>SafetyEventDriverObjectResponseBody</c> and its property-identical
/// twin <c>SafetyEventVehicleObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// One record serves both because they declare the identical single-property set.
/// Unlike <c>Samsara.Sdk.Models.Common.EntityReference</c> there is no
/// <c>name</c> property; do not add one for symmetry.
/// </remarks>
public sealed record LegacySafetyEventEntityRef
{
    /// <summary>Samsara ID of the referenced driver or vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// A safety event as returned by the legacy list endpoint
/// <c>GET /fleet/safety-events</c>. Mirrors the spec's <c>SafetyEvent</c> schema.
/// </summary>
/// <remarks>
/// Distinct from <c>Samsara.Sdk.Models.Safety.SafetyEvent</c>, which mirrors the
/// v2 <c>SafetyEventV2ObjectResponseBody</c> returned by <c>GET /safety-events</c>
/// and <c>GET /safety-events/stream</c>. The two schemas share a name in the spec
/// but not a shape: this one has no asset, coaching-assignment, context-label,
/// media, tag or attribute data, and instead exposes direct video-download URLs
/// and a coaching state. The <c>V1</c> prefix follows the repo's convention for
/// legacy shapes (<c>V1Trip</c>, <c>V1MaintenanceDvir</c>, <c>V1DefectRecord</c>).
/// </remarks>
public sealed record V1SafetyEvent
{
    /// <summary>Samsara ID of the safety event.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Time of the safety event (RFC 3339).</summary>
    [JsonPropertyName("time")]
    public string? Time { get; init; }

    /// <summary>Behavior labels associated with the safety event.</summary>
    [JsonPropertyName("behaviorLabels")]
    public IReadOnlyList<V1SafetyEventBehaviorLabel>? BehaviorLabels { get; init; }

    /// <summary>
    /// Coaching state of the safety event (e.g. <c>needsReview</c>,
    /// <c>coached</c>, <c>dismissed</c>). See the spec for the full enumeration.
    /// </summary>
    [JsonPropertyName("coachingState")]
    public string? CoachingState { get; init; }

    /// <summary>Time-limited URL to download the forward-facing video.</summary>
    [JsonPropertyName("downloadForwardVideoUrl")]
    public string? DownloadForwardVideoUrl { get; init; }

    /// <summary>Time-limited URL to download the inward-facing video.</summary>
    [JsonPropertyName("downloadInwardVideoUrl")]
    public string? DownloadInwardVideoUrl { get; init; }

    /// <summary>Time-limited URL to download the tracked inward-facing video.</summary>
    [JsonPropertyName("downloadTrackedInwardVideoUrl")]
    public string? DownloadTrackedInwardVideoUrl { get; init; }

    /// <summary>
    /// The driver on the safety event. Mirrors the spec's
    /// <c>driverTinyResponse</c> (<c>{id, name}</c>).
    /// </summary>
    [JsonPropertyName("driver")]
    public Samsara.Sdk.Models.Common.EntityReference? Driver { get; init; }

    /// <summary>Where the safety event occurred.</summary>
    [JsonPropertyName("location")]
    public V1SafetyEventLocation? Location { get; init; }

    /// <summary>Peak acceleration recorded during the event, in g-forces.</summary>
    [JsonPropertyName("maxAccelerationGForce")]
    public double? MaxAccelerationGForce { get; init; }

    /// <summary>The vehicle on the safety event.</summary>
    [JsonPropertyName("vehicle")]
    public V1SafetyEventVehicle? Vehicle { get; init; }
}

/// <summary>
/// A behavior label on a legacy safety event. Mirrors the spec's
/// <c>SafetyEventBehaviorLabel</c>.
/// </summary>
/// <remarks>
/// Distinct from <c>Samsara.Sdk.Models.Safety.SafetyEventBehaviorLabel</c>, which
/// mirrors the v2 <c>SafetyEventV2BehaviorLabelsResponseBody</c> and has no
/// <c>name</c>. Spec marks <c>label</c> and <c>source</c> REQUIRED; they stay
/// nullable because this is a response record.
/// </remarks>
public sealed record V1SafetyEventBehaviorLabel
{
    /// <summary>
    /// The behavior label (e.g. <c>speeding</c>, <c>harshTurn</c>, <c>crash</c>).
    /// Note the legacy enumeration is camel-cased, unlike the v2 one. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>Display name of the behavior label.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Source of the label: <c>automated</c> or <c>userGenerated</c>. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; init; }
}

/// <summary>
/// Coordinates of a legacy safety event. Mirrors the spec's <c>location</c>
/// schema.
/// </summary>
/// <remarks>
/// Spec marks both properties REQUIRED; they stay nullable because this is a
/// response record.
/// </remarks>
public sealed record V1SafetyEventLocation
{
    /// <summary>Latitude in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// The vehicle on a legacy safety event. Mirrors the spec's
/// <c>vehicleTinyResponse</c> (<c>{ExternalIds, id, name}</c>).
/// </summary>
/// <remarks>
/// <b>The capital-E <c>ExternalIds</c> is copied from the spec verbatim and must
/// not be "corrected".</b> <c>vehicleTinyResponse</c> is the only spec schema
/// carrying an external-ID map that spells it with a capital E; its Goa-era
/// sibling <c>GoaVehicleTinyResponseResponseBody</c> — mirrored here by
/// <see cref="LegacyVehicleRef"/> — uses the lower-case spelling. See the
/// 2026-08-17b design note in <c>docs/api-sync/30-maintenance.md</c>, which
/// records the same decision for
/// <c>Samsara.Sdk.Models.Maintenance.V1MaintenanceVehicleRef</c>.
/// </remarks>
public sealed record V1SafetyEventVehicle
{
    /// <summary>Samsara ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// A map of external IDs for the vehicle. The capital <c>E</c> is the spec's
    /// own spelling for <c>vehicleTinyResponse</c> — see the remarks on this
    /// record before changing it.
    /// </summary>
    [JsonPropertyName("ExternalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Details of a single harsh event, returned by
/// <c>GET /v1/fleet/vehicles/{vehicleId}/safety/harsh_event</c>. Mirrors the
/// spec's <c>V1VehicleHarshEventResponse</c>.
/// </summary>
/// <remarks>
/// This endpoint returns the object directly — there is no <c>{ data: ... }</c>
/// envelope. Spec marks <c>harshEventType</c> and <c>incidentReportUrl</c>
/// REQUIRED; they stay nullable because this is a response record.
/// </remarks>
public sealed record V1VehicleHarshEvent
{
    /// <summary>
    /// The kind of harsh event (e.g. <c>Harsh Brake</c>, <c>Harsh Turn</c>,
    /// <c>Harsh Acceleration</c>, <c>Crash</c>). Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("harshEventType")]
    public string? HarshEventType { get; init; }

    /// <summary>URL of the incident report for the event. Spec marks REQUIRED.</summary>
    [JsonPropertyName("incidentReportUrl")]
    public string? IncidentReportUrl { get; init; }

    /// <summary>Time-limited URL to download the forward-facing video.</summary>
    [JsonPropertyName("downloadForwardVideoUrl")]
    public string? DownloadForwardVideoUrl { get; init; }

    /// <summary>Time-limited URL to download the inward-facing video.</summary>
    [JsonPropertyName("downloadInwardVideoUrl")]
    public string? DownloadInwardVideoUrl { get; init; }

    /// <summary>Time-limited URL to download the tracked inward-facing video.</summary>
    [JsonPropertyName("downloadTrackedInwardVideoUrl")]
    public string? DownloadTrackedInwardVideoUrl { get; init; }

    /// <summary>Whether the driver was detected as distracted during the event.</summary>
    [JsonPropertyName("isDistracted")]
    public bool? IsDistracted { get; init; }

    /// <summary>Where the harsh event occurred.</summary>
    [JsonPropertyName("location")]
    public V1VehicleHarshEventLocation? Location { get; init; }
}

/// <summary>
/// Where a harsh event occurred. Mirrors the spec's
/// <c>V1VehicleHarshEventResponse_location</c>.
/// </summary>
public sealed record V1VehicleHarshEventLocation
{
    /// <summary>Human-readable address of the event location.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}
