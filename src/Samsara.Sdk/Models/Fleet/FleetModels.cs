namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json;
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

    [JsonPropertyName("createdAtTime")]
    public required DateTimeOffset CreatedAtTime { get; init; }

    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }

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

    [JsonPropertyName("auxInputType3")]
    public string? AuxInputType3 { get; init; }

    [JsonPropertyName("auxInputType4")]
    public string? AuxInputType4 { get; init; }

    [JsonPropertyName("auxInputType5")]
    public string? AuxInputType5 { get; init; }

    [JsonPropertyName("auxInputType6")]
    public string? AuxInputType6 { get; init; }

    [JsonPropertyName("auxInputType7")]
    public string? AuxInputType7 { get; init; }

    [JsonPropertyName("auxInputType8")]
    public string? AuxInputType8 { get; init; }

    [JsonPropertyName("auxInputType9")]
    public string? AuxInputType9 { get; init; }

    [JsonPropertyName("auxInputType10")]
    public string? AuxInputType10 { get; init; }

    [JsonPropertyName("auxInputType11")]
    public string? AuxInputType11 { get; init; }

    [JsonPropertyName("auxInputType12")]
    public string? AuxInputType12 { get; init; }

    [JsonPropertyName("auxInputType13")]
    public string? AuxInputType13 { get; init; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<Samsara.Sdk.Models.Tags.AttributeDefinition>? Attributes { get; init; }

    [JsonPropertyName("vehicleType")]
    public string? VehicleType { get; init; }

    [JsonPropertyName("esn")]
    public string? Esn { get; init; }

    [JsonPropertyName("cameraSerial")]
    public string? CameraSerial { get; init; }

    [JsonPropertyName("isRemotePrivacyButtonEnabled")]
    public bool? IsRemotePrivacyButtonEnabled { get; init; }

    [JsonPropertyName("vehicleWeight")]
    public long? VehicleWeight { get; init; }

    [JsonPropertyName("vehicleWeightInKilograms")]
    public long? VehicleWeightInKilograms { get; init; }

    [JsonPropertyName("vehicleWeightInPounds")]
    public long? VehicleWeightInPounds { get; init; }

    [JsonPropertyName("grossVehicleWeight")]
    public object? GrossVehicleWeight { get; init; }

    [JsonPropertyName("sensorConfiguration")]
    public object? SensorConfiguration { get; init; }

    [JsonPropertyName("engineHours")]
    public long? EngineHours { get; init; }

    [JsonPropertyName("odometerMeters")]
    public double? OdometerMeters { get; init; }

    [JsonPropertyName("gatewaySerial")]
    public string? GatewaySerial { get; init; }
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

    [JsonPropertyName("auxInputType3")]
    public string? AuxInputType3 { get; init; }

    [JsonPropertyName("auxInputType4")]
    public string? AuxInputType4 { get; init; }

    [JsonPropertyName("auxInputType5")]
    public string? AuxInputType5 { get; init; }

    [JsonPropertyName("auxInputType6")]
    public string? AuxInputType6 { get; init; }

    [JsonPropertyName("auxInputType7")]
    public string? AuxInputType7 { get; init; }

    [JsonPropertyName("auxInputType8")]
    public string? AuxInputType8 { get; init; }

    [JsonPropertyName("auxInputType9")]
    public string? AuxInputType9 { get; init; }

    [JsonPropertyName("auxInputType10")]
    public string? AuxInputType10 { get; init; }

    [JsonPropertyName("auxInputType11")]
    public string? AuxInputType11 { get; init; }

    [JsonPropertyName("auxInputType12")]
    public string? AuxInputType12 { get; init; }

    [JsonPropertyName("auxInputType13")]
    public string? AuxInputType13 { get; init; }

    [JsonPropertyName("engineHours")]
    public long? EngineHours { get; init; }

    [JsonPropertyName("grossVehicleWeight")]
    public System.Text.Json.JsonElement? GrossVehicleWeight { get; init; }

    [JsonPropertyName("gatewaySerial")]
    public string? GatewaySerial { get; init; }

    [JsonPropertyName("vehicleType")]
    public string? VehicleType { get; init; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }

    [JsonPropertyName("odometerMeters")]
    public long? OdometerMeters { get; init; }
}

/// <summary>
/// Request body for creating a new vehicle.
/// </summary>
public sealed record CreateVehicleRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("make")]
    public string? Make { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("year")]
    public string? Year { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Vehicle location snapshot.
/// </summary>
public sealed record VehicleLocation
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Single location object (snapshot shape, <c>GET /fleet/vehicles/locations</c>).</summary>
    [JsonPropertyName("location")]
    public object? Location { get; init; }

    /// <summary>Location entries (feed/history shapes, <c>.../locations/feed</c> and <c>.../locations/history</c>).</summary>
    [JsonPropertyName("locations")]
    public IReadOnlyList<object>? Locations { get; init; }

    // Not in current spec; retained for back-compat.
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

    /// <summary>Reverse-geocoded location (returned by the location feeds).</summary>
    [JsonPropertyName("reverseGeo")]
    public ReverseGeo? ReverseGeo { get; init; }
}

/// <summary>
/// Vehicle statistics data point (fuel, engine hours, odometer, etc).
/// </summary>
public sealed record VehicleStats
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("ambientAirTemperatureMilliC")]
    public object? AmbientAirTemperatureMilliC { get; init; }

    [JsonPropertyName("auxInput1")]
    public object? AuxInput1 { get; init; }

    [JsonPropertyName("auxInput2")]
    public object? AuxInput2 { get; init; }

    [JsonPropertyName("auxInput3")]
    public object? AuxInput3 { get; init; }

    [JsonPropertyName("auxInput4")]
    public object? AuxInput4 { get; init; }

    [JsonPropertyName("auxInput5")]
    public object? AuxInput5 { get; init; }

    [JsonPropertyName("auxInput6")]
    public object? AuxInput6 { get; init; }

    [JsonPropertyName("auxInput7")]
    public object? AuxInput7 { get; init; }

    [JsonPropertyName("auxInput8")]
    public object? AuxInput8 { get; init; }

    [JsonPropertyName("auxInput9")]
    public object? AuxInput9 { get; init; }

    [JsonPropertyName("auxInput10")]
    public object? AuxInput10 { get; init; }

    [JsonPropertyName("auxInput11")]
    public object? AuxInput11 { get; init; }

    [JsonPropertyName("auxInput12")]
    public object? AuxInput12 { get; init; }

    [JsonPropertyName("auxInput13")]
    public object? AuxInput13 { get; init; }

    [JsonPropertyName("barometricPressurePa")]
    public object? BarometricPressurePa { get; init; }

    [JsonPropertyName("batteryMilliVolts")]
    public object? BatteryMilliVolts { get; init; }

    [JsonPropertyName("defLevelMilliPercent")]
    public object? DefLevelMilliPercent { get; init; }

    [JsonPropertyName("ecuDoorStatus")]
    public object? EcuDoorStatus { get; init; }

    [JsonPropertyName("ecuSpeedMph")]
    public object? EcuSpeedMph { get; init; }

    [JsonPropertyName("engineCoolantTemperatureMilliC")]
    public object? EngineCoolantTemperatureMilliC { get; init; }

    [JsonPropertyName("engineImmobilizer")]
    public object? EngineImmobilizer { get; init; }

    [JsonPropertyName("engineLoadPercent")]
    public object? EngineLoadPercent { get; init; }

    [JsonPropertyName("engineOilPressureKPa")]
    public object? EngineOilPressureKPa { get; init; }

    [JsonPropertyName("engineRpm")]
    public object? EngineRpm { get; init; }

    [JsonPropertyName("evAverageBatteryTemperatureMilliCelsius")]
    public object? EvAverageBatteryTemperatureMilliCelsius { get; init; }

    [JsonPropertyName("evBatteryCurrentMilliAmp")]
    public object? EvBatteryCurrentMilliAmp { get; init; }

    [JsonPropertyName("evBatteryStateOfHealthMilliPercent")]
    public object? EvBatteryStateOfHealthMilliPercent { get; init; }

    [JsonPropertyName("evBatteryVoltageMilliVolt")]
    public object? EvBatteryVoltageMilliVolt { get; init; }

    [JsonPropertyName("evChargingCurrentMilliAmp")]
    public object? EvChargingCurrentMilliAmp { get; init; }

    [JsonPropertyName("evChargingEnergyMicroWh")]
    public object? EvChargingEnergyMicroWh { get; init; }

    [JsonPropertyName("evChargingStatus")]
    public object? EvChargingStatus { get; init; }

    [JsonPropertyName("evChargingVoltageMilliVolt")]
    public object? EvChargingVoltageMilliVolt { get; init; }

    [JsonPropertyName("evConsumedEnergyMicroWh")]
    public object? EvConsumedEnergyMicroWh { get; init; }

    [JsonPropertyName("evDistanceDrivenMeters")]
    public object? EvDistanceDrivenMeters { get; init; }

    [JsonPropertyName("evRegeneratedEnergyMicroWh")]
    public object? EvRegeneratedEnergyMicroWh { get; init; }

    [JsonPropertyName("evStateOfChargeMilliPercent")]
    public object? EvStateOfChargeMilliPercent { get; init; }

    [JsonPropertyName("externalIds")]
    public object? ExternalIds { get; init; }

    [JsonPropertyName("faultCodes")]
    public object? FaultCodes { get; init; }

    [JsonPropertyName("fuelConsumedMilliliters")]
    public object? FuelConsumedMilliliters { get; init; }

    [JsonPropertyName("gpsDistanceMeters")]
    public object? GpsDistanceMeters { get; init; }

    [JsonPropertyName("idlingDurationMilliseconds")]
    public object? IdlingDurationMilliseconds { get; init; }

    [JsonPropertyName("intakeManifoldTemperatureMilliC")]
    public object? IntakeManifoldTemperatureMilliC { get; init; }

    [JsonPropertyName("nfcCardScan")]
    public object? NfcCardScan { get; init; }

    [JsonPropertyName("obdEngineSeconds")]
    public object? ObdEngineSeconds { get; init; }

    [JsonPropertyName("seatbeltDriver")]
    public object? SeatbeltDriver { get; init; }

    [JsonPropertyName("spreaderActive")]
    public object? SpreaderActive { get; init; }

    [JsonPropertyName("spreaderAirTemp")]
    public object? SpreaderAirTemp { get; init; }

    [JsonPropertyName("spreaderBlastState")]
    public object? SpreaderBlastState { get; init; }

    [JsonPropertyName("spreaderGranularName")]
    public object? SpreaderGranularName { get; init; }

    [JsonPropertyName("spreaderGranularRate")]
    public object? SpreaderGranularRate { get; init; }

    [JsonPropertyName("spreaderLiquidName")]
    public object? SpreaderLiquidName { get; init; }

    [JsonPropertyName("spreaderLiquidRate")]
    public object? SpreaderLiquidRate { get; init; }

    [JsonPropertyName("spreaderOnState")]
    public object? SpreaderOnState { get; init; }

    [JsonPropertyName("spreaderPlowStatus")]
    public object? SpreaderPlowStatus { get; init; }

    [JsonPropertyName("spreaderPrewetName")]
    public object? SpreaderPrewetName { get; init; }

    [JsonPropertyName("spreaderPrewetRate")]
    public object? SpreaderPrewetRate { get; init; }

    [JsonPropertyName("spreaderRoadTemp")]
    public object? SpreaderRoadTemp { get; init; }

    [JsonPropertyName("syntheticEngineSeconds")]
    public object? SyntheticEngineSeconds { get; init; }

    [JsonPropertyName("engineStates")]
    public IReadOnlyList<object>? EngineStates { get; init; }

    [JsonPropertyName("fuelPercents")]
    public IReadOnlyList<object>? FuelPercents { get; init; }

    [JsonPropertyName("nfcCardScans")]
    public IReadOnlyList<object>? NfcCardScans { get; init; }

    /// <summary>GPS data: single object on the snapshot endpoint, array on feed/history; exposed as object to accept either shape.</summary>
    [JsonPropertyName("gps")]
    public object? Gps { get; init; }

    /// <summary>GPS-calculated odometer: single object on the snapshot endpoint, array on feed/history; exposed as object to accept either shape.</summary>
    [JsonPropertyName("gpsOdometerMeters")]
    public object? GpsOdometerMeters { get; init; }

    /// <summary>OBD-reported odometer: single object on the snapshot endpoint, array on feed/history; exposed as object to accept either shape.</summary>
    [JsonPropertyName("obdOdometerMeters")]
    public object? ObdOdometerMeters { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    [JsonPropertyName("engineState")]
    public EngineState? EngineState { get; init; }

    [JsonPropertyName("fuelPercent")]
    public FuelPercent? FuelPercent { get; init; }

    [JsonPropertyName("engineSeconds")]
    public EngineSeconds? EngineSeconds { get; init; }
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
/// Request body for creating new equipment.
/// </summary>
public sealed record CreateEquipmentRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Request body for updating existing equipment.
/// </summary>
public sealed record UpdateEquipmentRequest
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("equipmentSerialNumber")]
    public string? EquipmentSerialNumber { get; init; }

    [JsonPropertyName("engineHours")]
    public long? EngineHours { get; init; }

    [JsonPropertyName("odometerMeters")]
    public long? OdometerMeters { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }
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

    /// <summary>Asset serial number (spec property; preferred over legacy <c>equipmentSerialNumber</c>).</summary>
    [JsonPropertyName("assetSerial")]
    public string? AssetSerial { get; init; }

    /// <summary>Information about the gateway installed on this equipment.</summary>
    [JsonPropertyName("installedGateway")]
    public EquipmentInstalledGateway? InstalledGateway { get; init; }

    /// <summary>Legacy serial-number field retained for backward compatibility; prefer <see cref="AssetSerial"/>.</summary>
    [JsonPropertyName("equipmentSerialNumber")]
    public string? EquipmentSerialNumber { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }
}

/// <summary>
/// Information about the Samsara gateway installed on a unit of equipment.
/// </summary>
public sealed record EquipmentInstalledGateway
{
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }
}

/// <summary>
/// Equipment location snapshot (returned by <c>GET /fleet/equipment/locations</c> as a single
/// <see cref="Location"/>, and by <c>GET /fleet/equipment/locations/feed</c> /
/// <c>GET /fleet/equipment/locations/history</c> as an array in <see cref="Locations"/>).
/// </summary>
public sealed record EquipmentLocation
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Most recent location for this equipment (populated by <c>GET /fleet/equipment/locations</c>).</summary>
    [JsonPropertyName("location")]
    public EquipmentLocationPoint? Location { get; init; }

    /// <summary>Time-ordered locations for this equipment (populated by the locations feed and history endpoints).</summary>
    [JsonPropertyName("locations")]
    public IReadOnlyList<EquipmentLocationPoint>? Locations { get; init; }

    /// <summary>Legacy flat latitude; retained for backward compatibility. Prefer <c>Location.Latitude</c>.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Legacy flat longitude; retained for backward compatibility. Prefer <c>Location.Longitude</c>.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Legacy flat timestamp; retained for backward compatibility. Prefer <c>Location.Time</c>.</summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }
}

/// <summary>
/// A single GPS reading included in an <see cref="EquipmentLocation"/> response.
/// </summary>
public sealed record EquipmentLocationPoint
{
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    [JsonPropertyName("heading")]
    public double? Heading { get; init; }

    [JsonPropertyName("speed")]
    public double? Speed { get; init; }

    [JsonPropertyName("time")]
    public required DateTimeOffset Time { get; init; }
}

/// <summary>Represents a speeding interval event.</summary>
public sealed record SpeedingInterval
{
    [JsonPropertyName("asset")] public required object Asset { get; init; }
    [JsonPropertyName("createdAtTime")] public required DateTimeOffset CreatedAtTime { get; init; }
    [JsonPropertyName("intervals")] public required IReadOnlyList<object> Intervals { get; init; }
    [JsonPropertyName("tripStartTime")] public required DateTimeOffset TripStartTime { get; init; }
    [JsonPropertyName("updatedAtTime")] public required DateTimeOffset UpdatedAtTime { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("id")] public string? Id { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("vehicleName")] public string? VehicleName { get; init; }
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }
    [JsonPropertyName("driverName")] public string? DriverName { get; init; }
    [JsonPropertyName("startTime")] public DateTimeOffset? StartTime { get; init; }
    [JsonPropertyName("endTime")] public DateTimeOffset? EndTime { get; init; }
    [JsonPropertyName("maxSpeedMph")] public double? MaxSpeedMph { get; init; }
    [JsonPropertyName("speedLimitMph")] public double? SpeedLimitMph { get; init; }
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }
}

/// <summary>
/// Equipment statistics data point, returned by the equipment stats endpoints
/// (<c>/fleet/equipment/stats</c>, <c>/feed</c>, and <c>/history</c>).
/// </summary>
/// <remarks>
/// Several properties (e.g. <c>engineRpm</c>, <c>engineSeconds</c>, <c>gps</c>,
/// <c>gpsOdometerMeters</c>) are serialized as a single object on the snapshot
/// endpoint and as an array on the feed/history endpoints. They are exposed as
/// <see cref="System.Text.Json.JsonElement"/> so callers can inspect either
/// shape directly.
/// </remarks>
public sealed record EquipmentStats
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }

    /// <summary>Engine RPM. Object on <c>/stats</c>, array on <c>/feed</c> and <c>/history</c>.</summary>
    [JsonPropertyName("engineRpm")] public System.Text.Json.JsonElement? EngineRpm { get; init; }

    /// <summary>Total engine seconds. Object on <c>/stats</c>, array on <c>/feed</c> and <c>/history</c>.</summary>
    [JsonPropertyName("engineSeconds")] public System.Text.Json.JsonElement? EngineSeconds { get; init; }

    /// <summary>Engine state samples (array, returned by <c>/feed</c> and <c>/history</c>).</summary>
    [JsonPropertyName("engineStates")] public IReadOnlyList<object>? EngineStates { get; init; }

    /// <summary>Engine total idle time, in minutes. Object on <c>/stats</c>, array on <c>/feed</c> and <c>/history</c>.</summary>
    [JsonPropertyName("engineTotalIdleTimeMinutes")] public System.Text.Json.JsonElement? EngineTotalIdleTimeMinutes { get; init; }

    /// <summary>Fuel percent samples (array, returned by <c>/feed</c> and <c>/history</c>).</summary>
    [JsonPropertyName("fuelPercents")] public IReadOnlyList<object>? FuelPercents { get; init; }

    /// <summary>Gateway-reported engine seconds. Object on <c>/stats</c>, array on <c>/feed</c> and <c>/history</c>.</summary>
    [JsonPropertyName("gatewayEngineSeconds")] public System.Text.Json.JsonElement? GatewayEngineSeconds { get; init; }

    /// <summary>Gateway engine state (single object, returned by <c>/stats</c>).</summary>
    [JsonPropertyName("gatewayEngineState")] public object? GatewayEngineState { get; init; }

    /// <summary>Gateway engine state samples (array, returned by <c>/feed</c> and <c>/history</c>).</summary>
    [JsonPropertyName("gatewayEngineStates")] public IReadOnlyList<object>? GatewayEngineStates { get; init; }

    /// <summary>Gateway J1939 engine seconds samples (array, returned by <c>/feed</c> and <c>/history</c>).</summary>
    [JsonPropertyName("gatewayJ1939EngineSeconds")] public IReadOnlyList<object>? GatewayJ1939EngineSeconds { get; init; }

    /// <summary>GPS reading. Object on <c>/stats</c>, array on <c>/feed</c> and <c>/history</c>.</summary>
    [JsonPropertyName("gps")] public System.Text.Json.JsonElement? Gps { get; init; }

    /// <summary>GPS-derived odometer (meters). Object on <c>/stats</c>, array on <c>/feed</c> and <c>/history</c>.</summary>
    [JsonPropertyName("gpsOdometerMeters")] public System.Text.Json.JsonElement? GpsOdometerMeters { get; init; }

    /// <summary>OBD-reported engine seconds. Object on <c>/stats</c>, array on <c>/feed</c> and <c>/history</c>.</summary>
    [JsonPropertyName("obdEngineSeconds")] public System.Text.Json.JsonElement? ObdEngineSeconds { get; init; }

    /// <summary>OBD engine state (single object, returned by <c>/stats</c>).</summary>
    [JsonPropertyName("obdEngineState")] public object? ObdEngineState { get; init; }

    /// <summary>OBD engine state samples (array, returned by <c>/feed</c> and <c>/history</c>).</summary>
    [JsonPropertyName("obdEngineStates")] public IReadOnlyList<object>? ObdEngineStates { get; init; }

    // -- Legacy back-compat aliases retained for downstream consumers. Not part of the spec response inner schema. --

    /// <summary>Legacy engine state alias; retained for backward compatibility. Prefer <see cref="GatewayEngineState"/> / <see cref="EngineStates"/>.</summary>
    [JsonPropertyName("engineState")] public EngineState? EngineState { get; init; }

    /// <summary>Legacy fuel percent alias; retained for backward compatibility. Prefer <see cref="FuelPercents"/>.</summary>
    [JsonPropertyName("fuelPercent")] public FuelPercent? FuelPercent { get; init; }

    /// <summary>Legacy OBD odometer alias; retained for backward compatibility. Prefer <see cref="GpsOdometerMeters"/>.</summary>
    [JsonPropertyName("obdOdometer")] public ObdOdometer? ObdOdometer { get; init; }

    /// <summary>Legacy flat timestamp; retained for backward compatibility. Each nested sample carries its own <c>time</c>.</summary>
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }
}
