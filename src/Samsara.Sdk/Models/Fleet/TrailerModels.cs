namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

public sealed record Trailer
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }

    [JsonPropertyName("enabledForMobile")]
    public bool? EnabledForMobile { get; init; }

    [JsonPropertyName("trailerSerialNumber")]
    public string? TrailerSerialNumber { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<Common.TagReference>? Tags { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("installedGateway")]
    public GatewayInfo? InstalledGateway { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("make")]
    public string? Make { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("year")]
    public int? Year { get; init; }

    [JsonPropertyName("enabledForCommunication")]
    public bool? EnabledForCommunication { get; init; }
}

public sealed record CreateTrailerRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }

    [JsonPropertyName("enabledForMobile")]
    public bool? EnabledForMobile { get; init; }

    [JsonPropertyName("trailerSerialNumber")]
    public string? TrailerSerialNumber { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("make")]
    public string? Make { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("year")]
    public int? Year { get; init; }
}

public sealed record UpdateTrailerRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }

    [JsonPropertyName("enabledForMobile")]
    public bool? EnabledForMobile { get; init; }

    [JsonPropertyName("odometerMeters")]
    public long? OdometerMeters { get; init; }

    [JsonPropertyName("trailerSerialNumber")]
    public string? TrailerSerialNumber { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("make")]
    public string? Make { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("year")]
    public int? Year { get; init; }
}

/// <summary>Trailer statistics snapshot.</summary>
public sealed record TrailerStats
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("carrierReeferState")] public object? CarrierReeferState { get; init; }
    [JsonPropertyName("gps")] public object? Gps { get; init; }
    [JsonPropertyName("gpsOdometerMeters")] public object? GpsOdometerMeters { get; init; }
    [JsonPropertyName("reeferAlarms")] public object? ReeferAlarms { get; init; }
    [JsonPropertyName("reeferAmbientAirTemperatureMilliC")] public object? ReeferAmbientAirTemperatureMilliC { get; init; }
    [JsonPropertyName("reeferDoorStateZone1")] public object? ReeferDoorStateZone1 { get; init; }
    [JsonPropertyName("reeferDoorStateZone2")] public object? ReeferDoorStateZone2 { get; init; }
    [JsonPropertyName("reeferDoorStateZone3")] public object? ReeferDoorStateZone3 { get; init; }
    [JsonPropertyName("reeferFuelPercent")] public object? ReeferFuelPercent { get; init; }
    [JsonPropertyName("reeferObdEngineSeconds")] public object? ReeferObdEngineSeconds { get; init; }
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone1")] public object? ReeferReturnAirTemperatureMilliCZone1 { get; init; }
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone2")] public object? ReeferReturnAirTemperatureMilliCZone2 { get; init; }
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone3")] public object? ReeferReturnAirTemperatureMilliCZone3 { get; init; }
    [JsonPropertyName("reeferRunMode")] public object? ReeferRunMode { get; init; }
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone1")] public object? ReeferSetPointTemperatureMilliCZone1 { get; init; }
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone2")] public object? ReeferSetPointTemperatureMilliCZone2 { get; init; }
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone3")] public object? ReeferSetPointTemperatureMilliCZone3 { get; init; }
    [JsonPropertyName("reeferStateZone1")] public object? ReeferStateZone1 { get; init; }
    [JsonPropertyName("reeferStateZone2")] public object? ReeferStateZone2 { get; init; }
    [JsonPropertyName("reeferStateZone3")] public object? ReeferStateZone3 { get; init; }
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone1")] public object? ReeferSupplyAirTemperatureMilliCZone1 { get; init; }
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone2")] public object? ReeferSupplyAirTemperatureMilliCZone2 { get; init; }
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone3")] public object? ReeferSupplyAirTemperatureMilliCZone3 { get; init; }

    // Not in current spec; retained for back-compat.
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
