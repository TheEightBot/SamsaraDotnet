namespace Samsara.Sdk.Models.Routes;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a route in the Samsara system.
/// </summary>
public sealed record Route
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("driver")]
    public RouteDriver? Driver { get; init; }

    [JsonPropertyName("vehicle")]
    public RouteVehicle? Vehicle { get; init; }

    [JsonPropertyName("stops")]
    public IReadOnlyList<RouteStop>? Stops { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("scheduledRouteStartTime")]
    public DateTimeOffset? ScheduledRouteStartTime { get; init; }

    [JsonPropertyName("scheduledRouteEndTime")]
    public DateTimeOffset? ScheduledRouteEndTime { get; init; }

    [JsonPropertyName("actualRouteStartTime")]
    public DateTimeOffset? ActualRouteStartTime { get; init; }

    [JsonPropertyName("actualRouteEndTime")]
    public DateTimeOffset? ActualRouteEndTime { get; init; }

    [JsonPropertyName("orgLocalTimezone")]
    public string? OrgLocalTimezone { get; init; }

    [JsonPropertyName("recurringRouteLiveSharingLinks")]
    public IReadOnlyList<RouteLiveSharingLink>? RecurringRouteLiveSharingLinks { get; init; }
}

/// <summary>
/// Driver assigned to a route.
/// </summary>
public sealed record RouteDriver
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Vehicle assigned to a route.
/// </summary>
public sealed record RouteVehicle
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A stop within a route.
/// </summary>
public sealed record RouteStop
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    [JsonPropertyName("actualArrivalTime")]
    public DateTimeOffset? ActualArrivalTime { get; init; }

    [JsonPropertyName("actualDepartureTime")]
    public DateTimeOffset? ActualDepartureTime { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("singleUseLocation")]
    public SingleUseLocation? SingleUseLocation { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("sequenceNumber")]
    public int? SequenceNumber { get; init; }

    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }

    [JsonPropertyName("enRouteTime")]
    public DateTimeOffset? EnRouteTime { get; init; }

    [JsonPropertyName("eta")]
    public DateTimeOffset? Eta { get; init; }

    [JsonPropertyName("skippedTime")]
    public DateTimeOffset? SkippedTime { get; init; }

    [JsonPropertyName("actualDistanceMeters")]
    public long? ActualDistanceMeters { get; init; }

    [JsonPropertyName("plannedDistanceMeters")]
    public long? PlannedDistanceMeters { get; init; }

    [JsonPropertyName("liveSharingUrl")]
    public string? LiveSharingUrl { get; init; }

    [JsonPropertyName("address")]
    public RouteStopAddress? Address { get; init; }

    [JsonPropertyName("orders")]
    public IReadOnlyList<RouteStopOrderTaskReference>? Orders { get; init; }
}

/// <summary>
/// The saved address a <see cref="RouteStop"/> points at. Mirrors the spec's
/// <c>GoaAddressTinyResponseResponseBody</c> (a minified Address object).
/// </summary>
/// <remarks>
/// Not <c>EntityReference</c>: that record is a bare <c>{ id, name }</c> pair and
/// would drop <c>externalIds</c>, which this schema carries.
/// </remarks>
public sealed record RouteStopAddress
{
    /// <summary>Id of the address. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the address. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external ids for the address.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A canonical order task attached to a <see cref="RouteStop"/>. Mirrors the
/// spec's <c>RouteStopOrderTaskReferenceObjectResponseBody</c>.
/// </summary>
public sealed record RouteStopOrderTaskReference
{
    /// <summary>Samsara-generated canonical order UUID. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Order task ID attached to this stop. Spec-required.</summary>
    [JsonPropertyName("taskId")]
    public string? TaskId { get; init; }
}

/// <summary>
/// A live-sharing link on a <see cref="Route"/>. Mirrors the spec's
/// <c>LiveSharingLinkResponseObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Named <c>RouteLiveSharingLink</c> because <c>Samsara.Sdk.Models.Fleet.LiveSharingLink</c>
/// already exists and mirrors a different schema
/// (<c>LiveSharingLinkFullResponseObjectResponseBody</c>, which additionally
/// carries <c>id</c>, <c>type</c> and <c>description</c>).
/// </remarks>
public sealed record RouteLiveSharingLink
{
    /// <summary>Name of the Live Sharing Link. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The shareable URL of the vehicle's location. Spec-required.</summary>
    [JsonPropertyName("liveSharingUrl")]
    public string? LiveSharingUrl { get; init; }

    /// <summary>Date that this link expires, in RFC 3339 format.</summary>
    [JsonPropertyName("expiresAtTime")]
    public string? ExpiresAtTime { get; init; }
}

/// <summary>
/// A one-time location used as a route stop (not saved as an address).
/// </summary>
public sealed record SingleUseLocation
{
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}

/// <summary>
/// Route settings.
/// </summary>
public sealed record RouteSettings
{
    [JsonPropertyName("routeCompletionCondition")]
    public string? RouteCompletionCondition { get; init; }

    [JsonPropertyName("routeStartingCondition")]
    public string? RouteStartingCondition { get; init; }

    [JsonPropertyName("sequencingMethod")]
    public string? SequencingMethod { get; init; }
}

/// <summary>
/// Request body for creating a new route.
/// </summary>
public sealed record CreateRouteRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("stops")]
    public required IReadOnlyList<CreateRouteStopRequest> Stops { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("recomputeScheduledTimes")]
    public bool? RecomputeScheduledTimes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }
}

/// <summary>
/// Request body for a stop in a new route.
/// </summary>
public sealed record CreateRouteStopRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("singleUseLocation")]
    public SingleUseLocation? SingleUseLocation { get; init; }

    [JsonPropertyName("sequenceNumber")]
    public int? SequenceNumber { get; init; }

    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }
}

/// <summary>
/// Request body for updating a route (PATCH).
/// </summary>
public sealed record UpdateRouteRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }

    [JsonPropertyName("stops")]
    public IReadOnlyList<UpdateRouteStopRequest>? Stops { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("recomputeScheduledTimes")]
    public bool? RecomputeScheduledTimes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }
}

/// <summary>
/// Request body for updating a stop within a route.
/// </summary>
public sealed record UpdateRouteStopRequest
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    [JsonPropertyName("singleUseLocation")]
    public SingleUseLocation? SingleUseLocation { get; init; }

    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("sequenceNumber")]
    public int? SequenceNumber { get; init; }

    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }
}

/// <summary>Represents a route audit log event (route feed object), returned by
/// <c>GET /fleet/routes/audit-logs/feed</c>.</summary>
public sealed record RouteAuditEvent
{
    /// <summary>The before/after changes that were applied as part of this route update (spec REQUIRED).</summary>
    [JsonPropertyName("changes")] public required RouteAuditChanges Changes { get; init; }

    /// <summary>The route this update applies to (spec REQUIRED).</summary>
    [JsonPropertyName("route")] public required Route Route { get; init; }

    /// <summary>The source of this route update (e.g. <c>automatic</c>, <c>driver</c>, <c>admin</c>) — spec REQUIRED.</summary>
    [JsonPropertyName("source")] public required string Source { get; init; }

    /// <summary>The type of route update (e.g. <c>route tracking</c>) — spec REQUIRED.</summary>
    [JsonPropertyName("type")] public required string Type { get; init; }

    /// <summary>The timestamp of the route update in RFC 3339 format (spec REQUIRED).</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The operation that was performed as part of this route update (e.g. <c>stop scheduled</c>).</summary>
    [JsonPropertyName("operation")] public string? Operation { get; init; }
}

/// <summary>The before/after route snapshots captured by a <see cref="RouteAuditEvent"/>.
/// Mirrors the spec's <c>RouteChangesResponseBody</c>.</summary>
public sealed record RouteAuditChanges
{
    /// <summary>The route state before the update. Spec-required.</summary>
    [JsonPropertyName("before")] public required RouteAuditSnapshot Before { get; init; }

    /// <summary>The route state after the update. Spec-required.</summary>
    [JsonPropertyName("after")] public required RouteAuditSnapshot After { get; init; }
}

/// <summary>A minimal route snapshot (the changed stops only) inside a
/// <see cref="RouteAuditChanges"/>. Mirrors the spec's
/// <c>MinimalRouteAuditLogsResponseBody</c>.</summary>
public sealed record RouteAuditSnapshot
{
    /// <summary>The stops captured in this snapshot.</summary>
    [JsonPropertyName("stops")] public IReadOnlyList<RouteAuditStop>? Stops { get; init; }
}

/// <summary>A minimal route-stop snapshot inside a route audit log change.
/// Mirrors the spec's <c>MinimalRouteStopAuditLogsResponseBody</c>.</summary>
public sealed record RouteAuditStop
{
    /// <summary>Unique identifier of the stop. Spec-required.</summary>
    [JsonPropertyName("id")] public required string Id { get; init; }

    /// <summary>The state of the stop (e.g. <c>scheduled</c>, <c>enRoute</c>, <c>skipped</c>).</summary>
    [JsonPropertyName("state")] public string? State { get; init; }

    /// <summary>External identifiers for the stop.</summary>
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>The live-sharing URL for the stop.</summary>
    [JsonPropertyName("liveSharingUrl")] public string? LiveSharingUrl { get; init; }

    /// <summary>Scheduled arrival time, in RFC 3339 format.</summary>
    [JsonPropertyName("scheduledArrivalTime")] public DateTimeOffset? ScheduledArrivalTime { get; init; }

    /// <summary>Scheduled departure time, in RFC 3339 format.</summary>
    [JsonPropertyName("scheduledDepartureTime")] public DateTimeOffset? ScheduledDepartureTime { get; init; }

    /// <summary>Actual arrival time, in RFC 3339 format.</summary>
    [JsonPropertyName("actualArrivalTime")] public DateTimeOffset? ActualArrivalTime { get; init; }

    /// <summary>Actual departure time, in RFC 3339 format.</summary>
    [JsonPropertyName("actualDepartureTime")] public DateTimeOffset? ActualDepartureTime { get; init; }

    /// <summary>The time the asset went en route to the stop, in RFC 3339 format.</summary>
    [JsonPropertyName("enRouteTime")] public DateTimeOffset? EnRouteTime { get; init; }

    /// <summary>Estimated time of arrival, in RFC 3339 format.</summary>
    [JsonPropertyName("eta")] public DateTimeOffset? Eta { get; init; }

    /// <summary>The time the stop was skipped, in RFC 3339 format.</summary>
    [JsonPropertyName("skippedTime")] public DateTimeOffset? SkippedTime { get; init; }
}
