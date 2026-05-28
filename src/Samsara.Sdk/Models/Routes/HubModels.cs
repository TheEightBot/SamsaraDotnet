namespace Samsara.Sdk.Models.Routes;

using System.Text.Json.Serialization;

public sealed record Hub
{
    /// <summary>Hub identifier (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Hub name (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Hub timezone (spec REQUIRED on <c>GET /hubs</c>).</summary>
    [JsonPropertyName("timeZone")]
    public required string TimeZone { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED on <c>GET /hubs</c>).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED on <c>GET /hubs</c>).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hubs</c> spec response — retained as nullable
    /// back-compat property per the workflow precedent for response-side extras.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hubs</c> spec response — retained as nullable
    /// back-compat property per the workflow precedent for response-side extras.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hubs</c> spec response — retained as nullable
    /// back-compat property per the workflow precedent for response-side extras.
    /// </summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hubs</c> spec response — retained as nullable
    /// back-compat property per the workflow precedent for response-side extras.
    /// </summary>
    [JsonPropertyName("geofence")]
    public Addresses.Geofence? Geofence { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hubs</c> spec response — retained as nullable
    /// back-compat property per the workflow precedent for response-side extras.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<Common.TagReference>? Tags { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hubs</c> spec response — retained as nullable
    /// back-compat property per the workflow precedent for response-side extras.
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

public sealed record CreateHubRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

public sealed record UpdateHubRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>Hub capacity returned by <c>GET /hub/capacities</c>.</summary>
public sealed record HubCapacity
{
    /// <summary>Capacity identifier (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Hub identifier (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Capacity name (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Unit of measurement (spec REQUIRED).</summary>
    [JsonPropertyName("unit")]
    public required string Unit { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hub/capacities</c> spec response — retained as
    /// nullable back-compat property per the workflow precedent.
    /// </summary>
    [JsonPropertyName("capacity")]
    public int? Capacity { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hub/capacities</c> spec response — retained as
    /// nullable back-compat property per the workflow precedent.
    /// </summary>
    [JsonPropertyName("usedCapacity")]
    public int? UsedCapacity { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hub/capacities</c> spec response — retained as
    /// nullable back-compat property per the workflow precedent.
    /// </summary>
    [JsonPropertyName("timeSlot")]
    public string? TimeSlot { get; init; }
}

/// <summary>A custom property for hubs (returned by <c>GET /hub/customProperties</c>).</summary>
public sealed record HubCustomProperty
{
    /// <summary>Custom property identifier (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Hub identifier (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Custom property name (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>CSV column names that map to this custom property (spec REQUIRED).</summary>
    [JsonPropertyName("csvColumns")]
    public required string CsvColumns { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }

    /// <summary>
    /// Not part of the <c>GET /hub/customProperties</c> spec response —
    /// retained as nullable back-compat property per the workflow precedent.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>A hub location returned by <c>GET /hub/locations</c>, <c>POST /hub/locations</c>, and <c>PATCH /hub/location/{id}</c>.</summary>
public sealed record HubLocation
{
    /// <summary>The Samsara-generated unique identifier for the location (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>The name of the location (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>The physical address of the location (spec REQUIRED).</summary>
    [JsonPropertyName("address")]
    public required string Address { get; init; }

    /// <summary>The customer-provided identifier for the location (spec REQUIRED).</summary>
    [JsonPropertyName("customerLocationId")]
    public required string CustomerLocationId { get; init; }

    /// <summary>The ID of the hub this location belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Indicates if the location is a depot (spec REQUIRED).</summary>
    [JsonPropertyName("isDepot")]
    public required bool IsDepot { get; init; }

    /// <summary>Latitude coordinate of the location (spec REQUIRED).</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude coordinate of the location (spec REQUIRED).</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    /// <summary>Instructions for the driver (spec REQUIRED).</summary>
    [JsonPropertyName("driverInstructions")]
    public required string DriverInstructions { get; init; }

    /// <summary>Notes for the planner (spec REQUIRED).</summary>
    [JsonPropertyName("plannerNotes")]
    public required string PlannerNotes { get; init; }

    /// <summary>Estimated service time at this location in seconds (spec REQUIRED).</summary>
    [JsonPropertyName("serviceTimeSeconds")]
    public required int ServiceTimeSeconds { get; init; }

    /// <summary>Service windows during which work can be performed at this location (spec REQUIRED).</summary>
    [JsonPropertyName("serviceWindows")]
    public required IReadOnlyList<object> ServiceWindows { get; init; }

    /// <summary>Skills required for service at this location (spec REQUIRED).</summary>
    [JsonPropertyName("skillsRequired")]
    public required IReadOnlyList<object> SkillsRequired { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }

    /// <summary>
    /// Not part of the spec response — retained as nullable back-compat
    /// property per the workflow precedent for response-side extras.
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }
}

/// <summary>
/// A single hub location input object posted as part of the
/// <c>{ data: HubLocationInputObjectRequestBody[] }</c> envelope to
/// <c>POST /hub/locations</c>. Renamed from <c>CreateHubLocationRequest</c>
/// during the 2026-05-27 model sync — the prior name now refers to the
/// outer envelope (<see cref="CreateHubLocationsRequest"/>).
/// </summary>
public sealed record CreateHubLocationInput
{
    /// <summary>The physical address of the location (spec REQUIRED).</summary>
    [JsonPropertyName("address")]
    public required string Address { get; init; }

    /// <summary>The customer-provided identifier for the location (spec REQUIRED).</summary>
    [JsonPropertyName("customerLocationId")]
    public required string CustomerLocationId { get; init; }

    /// <summary>The ID of the hub this location belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Indicates if the location is a depot (spec REQUIRED).</summary>
    [JsonPropertyName("isDepot")]
    public required bool IsDepot { get; init; }

    /// <summary>The name of the location (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Latitude coordinate (optional — geocoded from address when omitted).</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude coordinate (optional — geocoded from address when omitted).</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Instructions for the driver.</summary>
    [JsonPropertyName("driverInstructions")]
    public string? DriverInstructions { get; init; }

    /// <summary>Notes for the planner.</summary>
    [JsonPropertyName("plannerNotes")]
    public string? PlannerNotes { get; init; }

    /// <summary>Estimated service time at this location in seconds.</summary>
    [JsonPropertyName("serviceTimeSeconds")]
    public int? ServiceTimeSeconds { get; init; }

    /// <summary>Recurring service windows for the location.</summary>
    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<object>? ServiceWindows { get; init; }

    /// <summary>Skill IDs required for service at this location.</summary>
    [JsonPropertyName("skillsRequired")]
    public IReadOnlyList<object>? SkillsRequired { get; init; }
}

/// <summary>
/// Envelope request body for <c>POST /hub/locations</c> — the spec wraps the
/// array of inputs in <c>{ data: [...] }</c>.
/// </summary>
public sealed record CreateHubLocationsRequest
{
    [JsonPropertyName("data")]
    public required IReadOnlyList<CreateHubLocationInput> Data { get; init; }
}

/// <summary>
/// Inner request body for updating a hub location — posted inside the
/// <see cref="UpdateHubLocationEnvelopeRequest"/> envelope.
/// </summary>
public sealed record UpdateHubLocationRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("address")]
    public string? Address { get; init; }

    [JsonPropertyName("customerLocationId")]
    public string? CustomerLocationId { get; init; }

    [JsonPropertyName("isDepot")]
    public bool? IsDepot { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("driverInstructions")]
    public string? DriverInstructions { get; init; }

    [JsonPropertyName("plannerNotes")]
    public string? PlannerNotes { get; init; }

    [JsonPropertyName("serviceTimeSeconds")]
    public int? ServiceTimeSeconds { get; init; }

    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<object>? ServiceWindows { get; init; }

    [JsonPropertyName("skillsRequired")]
    public IReadOnlyList<object>? SkillsRequired { get; init; }
}

/// <summary>
/// Envelope request body for <c>PATCH /hub/location/{id}</c> — the spec wraps
/// the update payload in <c>{ data: T }</c>.
/// </summary>
public sealed record UpdateHubLocationEnvelopeRequest
{
    [JsonPropertyName("data")]
    public required UpdateHubLocationRequest Data { get; init; }
}

/// <summary>A hub skill (returned by <c>GET /hub/skills</c>).</summary>
public sealed record HubSkill
{
    /// <summary>Skill identifier (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Hub identifier (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Skill name (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>A hub dispatch plan returned by <c>GET /hub/plans</c> and <c>POST /hub/plan</c>.</summary>
public sealed record HubPlan
{
    /// <summary>The Samsara-generated unique identifier (UUID) for the plan (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>The name of the plan (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>The ID of the hub this plan belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Shift start time for the plan in RFC 3339 format (spec REQUIRED).</summary>
    [JsonPropertyName("shiftStartTime")]
    public required DateTimeOffset ShiftStartTime { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>Request body for creating a hub plan (<c>POST /hub/plan</c>).</summary>
public sealed record CreateHubPlanRequest
{
    /// <summary>The ID of the hub the plan belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>The name of the plan (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>The ID of a saved session configuration (preset) to apply when creating the plan.</summary>
    [JsonPropertyName("sessionConfigurationId")]
    public string? SessionConfigurationId { get; init; }

    /// <summary>Shift start time for the plan in RFC 3339 format. Defaults to 9:00 AM on the next business day in the hub's timezone when omitted.</summary>
    [JsonPropertyName("shiftStartTime")]
    public DateTimeOffset? ShiftStartTime { get; init; }
}

/// <summary>A hub plan order returned by <c>GET /hub/plan/orders</c> and <c>POST /hub/plan/orders</c>.</summary>
public sealed record HubPlanOrder
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("planId")] public required string PlanId { get; init; }
    [JsonPropertyName("hubId")] public required string HubId { get; init; }
    [JsonPropertyName("customerOrderId")] public required string CustomerOrderId { get; init; }
    [JsonPropertyName("priority")] public required long Priority { get; init; }
    [JsonPropertyName("createdAtTime")] public required DateTimeOffset CreatedAtTime { get; init; }
    [JsonPropertyName("updatedAtTime")] public required DateTimeOffset UpdatedAtTime { get; init; }
    [JsonPropertyName("customProperties")] public required IReadOnlyList<object> CustomProperties { get; init; }
    [JsonPropertyName("quantities")] public required IReadOnlyList<object> Quantities { get; init; }
    [JsonPropertyName("skillsRequired")] public required IReadOnlyList<object> SkillsRequired { get; init; }
    [JsonPropertyName("routeId")] public string? RouteId { get; init; }
    [JsonPropertyName("pickup")] public object? Pickup { get; init; }
    [JsonPropertyName("delivery")] public object? Delivery { get; init; }
}

/// <summary>
/// A single hub plan order input object posted as part of the
/// <c>{ data: OrderInputObjectRequestBody[] }</c> envelope to
/// <c>POST /hub/plan/orders</c>. Renamed from <c>CreateHubPlanOrdersRequest</c>
/// during the 2026-05-27 model sync — the prior name now refers to the
/// outer envelope (<see cref="CreateHubPlanOrdersRequest"/>).
/// </summary>
public sealed record CreateHubPlanOrderInput
{
    /// <summary>The customer-provided identifier for the order (spec REQUIRED).</summary>
    [JsonPropertyName("customerOrderId")]
    public required string CustomerOrderId { get; init; }

    /// <summary>The ID of the hub the order belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>The ID of the plan the order belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("planId")]
    public required string PlanId { get; init; }

    /// <summary>An array of custom property values for the order.</summary>
    [JsonPropertyName("customProperties")]
    public IReadOnlyList<object>? CustomProperties { get; init; }

    /// <summary>Delivery task details (spec ref <c>OrderTaskRequestBody</c>).</summary>
    [JsonPropertyName("delivery")]
    public object? Delivery { get; init; }

    /// <summary>Pickup task details (spec ref <c>OrderTaskRequestBody</c>).</summary>
    [JsonPropertyName("pickup")]
    public object? Pickup { get; init; }

    /// <summary>Priority of the order (e.g., 1 for high, 5 for low).</summary>
    [JsonPropertyName("priority")]
    public long? Priority { get; init; }

    /// <summary>An array of quantities for the order.</summary>
    [JsonPropertyName("quantities")]
    public IReadOnlyList<object>? Quantities { get; init; }

    /// <summary>An array of skill IDs required to fulfill the order.</summary>
    [JsonPropertyName("skillsRequired")]
    public IReadOnlyList<object>? SkillsRequired { get; init; }
}

/// <summary>
/// Envelope request body for <c>POST /hub/plan/orders</c> — the spec wraps the
/// array of plan order inputs in <c>{ data: [...] }</c>.
/// </summary>
public sealed record CreateHubPlanOrdersRequest
{
    [JsonPropertyName("data")]
    public required IReadOnlyList<CreateHubPlanOrderInput> Data { get; init; }
}
