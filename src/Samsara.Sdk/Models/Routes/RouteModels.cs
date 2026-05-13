namespace Samsara.Sdk.Models.Routes;

using System.Text.Json;
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

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; init; }

    [JsonPropertyName("dispatchRouteId")]
    public string? DispatchRouteId { get; init; }

    [JsonPropertyName("distanceMeters")]
    public long? DistanceMeters { get; init; }

    [JsonPropertyName("durationSeconds")]
    public long? DurationSeconds { get; init; }

    [JsonPropertyName("hubId")]
    public string? HubId { get; init; }

    [JsonPropertyName("isEdited")]
    public bool? IsEdited { get; init; }

    [JsonPropertyName("isPinned")]
    public bool? IsPinned { get; init; }

    [JsonPropertyName("planId")]
    public string? PlanId { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("quantities")]
    public JsonElement? Quantities { get; init; }

    [JsonPropertyName("recurringRouteLiveSharingLinks")]
    public IReadOnlyList<JsonElement>? RecurringRouteLiveSharingLinks { get; init; }
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
    public JsonElement? Address { get; init; }

    [JsonPropertyName("hubLocationId")]
    public string? HubLocationId { get; init; }

    [JsonPropertyName("orders")]
    public IReadOnlyList<JsonElement>? Orders { get; init; }
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

/// <summary>Represents a route audit log event.</summary>
public sealed record RouteAuditEvent
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("routeId")] public string? RouteId { get; init; }
    [JsonPropertyName("userId")] public string? UserId { get; init; }
    [JsonPropertyName("eventType")] public string? EventType { get; init; }
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
}
