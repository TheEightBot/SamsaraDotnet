namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a vehicle in the Samsara fleet.
/// </summary>
public sealed record Vehicle
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("make")]
    public string? Make { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("year")]
    public string? Year { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("staticAssignedDriver")]
    public DriverReference? StaticAssignedDriver { get; init; }

    [JsonPropertyName("gateway")]
    public GatewayInfo? Gateway { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("harshAccelerationSettingType")]
    public string? HarshAccelerationSettingType { get; init; }

    [JsonPropertyName("vehicleRegulationMode")]
    public string? VehicleRegulationMode { get; init; }

    [JsonPropertyName("auxInputType1")]
    public string? AuxInputType1 { get; init; }

    [JsonPropertyName("auxInputType2")]
    public string? AuxInputType2 { get; init; }
}

/// <summary>
/// Lightweight reference to a driver associated with a vehicle.
/// </summary>
public sealed record DriverReference
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Information about the Samsara gateway device installed in a vehicle.
/// </summary>
public sealed record GatewayInfo
{
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }
}

/// <summary>
/// Request body for updating a vehicle (PATCH).
/// </summary>
public sealed record UpdateVehicleRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("staticAssignedDriverId")]
    public string? StaticAssignedDriverId { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("harshAccelerationSettingType")]
    public string? HarshAccelerationSettingType { get; init; }

    [JsonPropertyName("vehicleRegulationMode")]
    public string? VehicleRegulationMode { get; init; }

    [JsonPropertyName("auxInputType1")]
    public string? AuxInputType1 { get; init; }

    [JsonPropertyName("auxInputType2")]
    public string? AuxInputType2 { get; init; }
}

/// <summary>
/// Vehicle location snapshot.
/// </summary>
public sealed record VehicleLocation
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("heading")]
    public double? Heading { get; init; }

    [JsonPropertyName("speed")]
    public double? Speed { get; init; }

    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }
}

/// <summary>
/// Vehicle statistics data point (fuel, engine hours, odometer, etc).
/// </summary>
public sealed record VehicleStats
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("engineState")]
    public EngineState? EngineState { get; init; }

    [JsonPropertyName("fuelPercent")]
    public FuelPercent? FuelPercent { get; init; }

    [JsonPropertyName("obdOdometerMeters")]
    public ObdOdometer? ObdOdometerMeters { get; init; }

    [JsonPropertyName("gpsOdometerMeters")]
    public GpsOdometer? GpsOdometerMeters { get; init; }

    [JsonPropertyName("engineSeconds")]
    public EngineSeconds? EngineSeconds { get; init; }

    [JsonPropertyName("gps")]
    public GpsData? Gps { get; init; }
}

/// <summary>
/// Engine on/off state.
/// </summary>
public sealed record EngineState
{
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// Vehicle fuel level as percentage.
/// </summary>
public sealed record FuelPercent
{
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// OBD-reported odometer reading.
/// </summary>
public sealed record ObdOdometer
{
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// GPS-calculated odometer reading.
/// </summary>
public sealed record GpsOdometer
{
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// Total engine run time.
/// </summary>
public sealed record EngineSeconds
{
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("value")]
    public long? Value { get; init; }
}

/// <summary>
/// GPS position data.
/// </summary>
public sealed record GpsData
{
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("headingDegrees")]
    public double? HeadingDegrees { get; init; }

    [JsonPropertyName("speedMilesPerHour")]
    public double? SpeedMilesPerHour { get; init; }

    [JsonPropertyName("reverseGeo")]
    public ReverseGeo? ReverseGeo { get; init; }
}

/// <summary>
/// Reverse geocoded address for a GPS location.
/// </summary>
public sealed record ReverseGeo
{
    [JsonPropertyName("formattedLocation")]
    public string? FormattedLocation { get; init; }
}

/// <summary>
/// Represents an equipment asset (trailer, powered or unpowered equipment).
/// </summary>
public sealed record Equipment
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("equipmentSerialNumber")]
    public string? EquipmentSerialNumber { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }
}

/// <summary>
/// Equipment location snapshot.
/// </summary>
public sealed record EquipmentLocation
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }
}
