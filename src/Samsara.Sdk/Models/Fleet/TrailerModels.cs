namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

public sealed record Trailer
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("make")]
    public string? Make { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("year")]
    public int? Year { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<Common.TagReference>? Tags { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("enabledForCommunication")]
    public bool? EnabledForCommunication { get; init; }

    [JsonPropertyName("installedGateway")]
    public GatewayInfo? InstalledGateway { get; init; }
}

public sealed record CreateTrailerRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("make")]
    public string? Make { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("year")]
    public int? Year { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

public sealed record UpdateTrailerRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("make")]
    public string? Make { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("year")]
    public int? Year { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>Trailer statistics snapshot.</summary>
public sealed record TrailerStats
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("location")] public TrailerLocation? Location { get; init; }
    [JsonPropertyName("temperature")] public double? Temperature { get; init; }
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
    [JsonPropertyName("engineHours")] public double? EngineHours { get; init; }
    [JsonPropertyName("odometer")] public double? Odometer { get; init; }
}

/// <summary>Location info for a trailer.</summary>
public sealed record TrailerLocation
{
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }
    [JsonPropertyName("heading")] public double? Heading { get; init; }
    [JsonPropertyName("speed")] public double? Speed { get; init; }
}
