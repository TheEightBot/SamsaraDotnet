namespace Samsara.Sdk.Models.Routes;

using System.Text.Json.Serialization;

public sealed record Hub
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    [JsonPropertyName("geofence")]
    public Addresses.Geofence? Geofence { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<Common.TagReference>? Tags { get; init; }

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

/// <summary>Hub capacity for a time slot.</summary>
public sealed record HubCapacity
{
    [JsonPropertyName("hubId")] public required string HubId { get; init; }
    [JsonPropertyName("capacity")] public int? Capacity { get; init; }
    [JsonPropertyName("usedCapacity")] public int? UsedCapacity { get; init; }
    [JsonPropertyName("timeSlot")] public string? TimeSlot { get; init; }
}

/// <summary>A custom property for hubs.</summary>
public sealed record HubCustomProperty
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("type")] public string? Type { get; init; }
}

/// <summary>A hub location.</summary>
public sealed record HubLocation
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("address")] public string? Address { get; init; }
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
}

/// <summary>Request body for creating a hub location.</summary>
public sealed record CreateHubLocationRequest
{
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("address")] public string? Address { get; init; }
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
}

/// <summary>Request body for updating a hub location.</summary>
public sealed record UpdateHubLocationRequest
{
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("address")] public string? Address { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
}

/// <summary>A hub skill.</summary>
public sealed record HubSkill
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
}

/// <summary>A hub dispatch plan.</summary>
public sealed record HubPlan
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("date")] public DateTimeOffset? Date { get; init; }
}

/// <summary>Request body for creating a hub plan.</summary>
public sealed record CreateHubPlanRequest
{
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("date")] public required DateTimeOffset Date { get; init; }
}

/// <summary>A hub plan order.</summary>
public sealed record HubPlanOrder
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("planId")] public string? PlanId { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
}

/// <summary>Request body for creating hub plan orders.</summary>
public sealed record CreateHubPlanOrdersRequest
{
    [JsonPropertyName("planId")] public required string PlanId { get; init; }
    [JsonPropertyName("orderIds")] public required IReadOnlyList<string> OrderIds { get; init; }
}
