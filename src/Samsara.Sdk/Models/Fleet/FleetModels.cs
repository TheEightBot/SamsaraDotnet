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

    /// <summary>Gross vehicle weight, returned by <c>GET /fleet/vehicles/{id}</c> and the PATCH response.</summary>
    [JsonPropertyName("grossVehicleWeight")]
    public VehicleGrossWeight? GrossVehicleWeight { get; init; }

    /// <summary>Trailer sensor configuration (cargo/temperature/humidity/door sensors by area).</summary>
    [JsonPropertyName("sensorConfiguration")]
    public VehicleSensorConfiguration? SensorConfiguration { get; init; }
}

/// <summary>
/// Gross vehicle weight on a <see cref="Vehicle"/>. Mirrors the spec's
/// <c>grossVehicleWeight</c> object.
/// </summary>
public sealed record VehicleGrossWeight
{
    /// <summary>Unit of the weight value (e.g. <c>pounds</c>, <c>kilograms</c>).</summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }

    /// <summary>Gross vehicle weight value, expressed in <see cref="Unit"/>.</summary>
    [JsonPropertyName("weight")]
    public long? Weight { get; init; }
}

/// <summary>
/// Trailer sensor configuration on a <see cref="Vehicle"/>. Mirrors the spec's
/// <c>sensorConfiguration</c> object.
/// </summary>
public sealed record VehicleSensorConfiguration
{
    /// <summary>Sensor areas (each grouping cargo/temperature/humidity sensors by position).</summary>
    [JsonPropertyName("areas")]
    public IReadOnlyList<VehicleSensorArea>? Areas { get; init; }

    /// <summary>Door sensors by position.</summary>
    [JsonPropertyName("doors")]
    public IReadOnlyList<VehicleSensorDoor>? Doors { get; init; }
}

/// <summary>A sensor area within a <see cref="VehicleSensorConfiguration"/>.</summary>
public sealed record VehicleSensorArea
{
    /// <summary>Position label for the area.</summary>
    [JsonPropertyName("position")]
    public string? Position { get; init; }

    /// <summary>Cargo sensors in this area.</summary>
    [JsonPropertyName("cargoSensors")]
    public IReadOnlyList<VehicleSensor>? CargoSensors { get; init; }

    /// <summary>Temperature sensors in this area.</summary>
    [JsonPropertyName("temperatureSensors")]
    public IReadOnlyList<VehicleSensor>? TemperatureSensors { get; init; }

    /// <summary>Humidity sensors in this area.</summary>
    [JsonPropertyName("humiditySensors")]
    public IReadOnlyList<VehicleSensor>? HumiditySensors { get; init; }
}

/// <summary>A door sensor within a <see cref="VehicleSensorConfiguration"/>.</summary>
public sealed record VehicleSensorDoor
{
    /// <summary>Position label for the door.</summary>
    [JsonPropertyName("position")]
    public string? Position { get; init; }

    /// <summary>The door sensor.</summary>
    [JsonPropertyName("sensor")]
    public VehicleSensor? Sensor { get; init; }
}

/// <summary>A single Samsara sensor (cargo/temperature/humidity/door).</summary>
public sealed record VehicleSensor
{
    /// <summary>Sensor ID.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Sensor MAC address.</summary>
    [JsonPropertyName("mac")]
    public string? Mac { get; init; }

    /// <summary>Sensor name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
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

    /// <summary>
    /// Single location point (snapshot shape, <c>GET /fleet/vehicles/locations</c>).
    /// Null on the feed/history shapes, which populate <see cref="Locations"/> instead.
    /// </summary>
    [JsonPropertyName("location")]
    public VehicleLocationPoint? Location { get; init; }

    /// <summary>
    /// Location points (feed/history shapes, <c>.../locations/feed</c> and
    /// <c>.../locations/history</c>). Null on the snapshot shape, which populates
    /// <see cref="Location"/> instead.
    /// </summary>
    [JsonPropertyName("locations")]
    public IReadOnlyList<VehicleLocationPoint>? Locations { get; init; }
}

/// <summary>
/// A single vehicle location point — the nested object the spec returns under
/// <c>location</c> (snapshot) and each element of <c>locations</c> (feed/history).
/// </summary>
public sealed record VehicleLocationPoint
{
    /// <summary>Latitude of the vehicle (degrees). Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude of the vehicle (degrees). Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    /// <summary>Time the location was recorded, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("time")]
    public required DateTimeOffset Time { get; init; }

    /// <summary>Heading of the vehicle in degrees (0 = north).</summary>
    [JsonPropertyName("heading")]
    public double? Heading { get; init; }

    /// <summary>Speed of the vehicle in miles per hour.</summary>
    [JsonPropertyName("speed")]
    public double? Speed { get; init; }

    /// <summary>Reverse-geocoded location for this point.</summary>
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
    /// <summary>Unique identifier of the equipment. Required on <c>GET /fleet/equipment</c>;
    /// nullable because the same record is reused for the <c>PATCH /beta/fleet/equipment/{id}</c>
    /// response where the spec lists <c>id</c> optional.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Asset serial number. Returned by the standard <c>GET /fleet/equipment</c>
    /// (the beta equipment update uses <see cref="EquipmentSerialNumber"/> instead).</summary>
    [JsonPropertyName("assetSerial")]
    public string? AssetSerial { get; init; }

    /// <summary>Information about the gateway installed on this equipment.</summary>
    [JsonPropertyName("installedGateway")]
    public EquipmentInstalledGateway? InstalledGateway { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Serial number as returned by the beta <c>PATCH /beta/fleet/equipment/{id}</c>
    /// response (the standard equipment endpoints use <see cref="AssetSerial"/>).</summary>
    [JsonPropertyName("equipmentSerialNumber")]
    public string? EquipmentSerialNumber { get; init; }

    /// <summary>Custom attributes on the equipment. Returned by the beta
    /// <c>PATCH /beta/fleet/equipment/{id}</c> response.</summary>
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

/// <summary>
/// Represents a trip's speeding intervals, returned by
/// <c>GET /speeding-intervals/stream</c>.
/// </summary>
public sealed record SpeedingInterval
{
    /// <summary>The asset (vehicle) the speeding occurred on. Spec marks REQUIRED.</summary>
    [JsonPropertyName("asset")] public required SpeedingIntervalAsset Asset { get; init; }

    /// <summary>Time the record was created, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")] public required DateTimeOffset CreatedAtTime { get; init; }

    /// <summary>The individual speeding intervals within the trip. Spec marks REQUIRED.</summary>
    [JsonPropertyName("intervals")] public required IReadOnlyList<object> Intervals { get; init; }

    /// <summary>Start time of the trip, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("tripStartTime")] public required DateTimeOffset TripStartTime { get; init; }

    /// <summary>Time the record was last updated, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")] public required DateTimeOffset UpdatedAtTime { get; init; }

    /// <summary>Samsara ID of the driver on the trip, when available.</summary>
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }
}

/// <summary>
/// Asset (vehicle) reference on a <see cref="SpeedingInterval"/>. Mirrors the
/// spec's <c>TripAssetResponseBody</c>.
/// </summary>
public sealed record SpeedingIntervalAsset
{
    /// <summary>Samsara ID of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")] public required string Id { get; init; }

    /// <summary>Name of the asset.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }

    /// <summary>Asset type (e.g. <c>vehicle</c>).</summary>
    [JsonPropertyName("type")] public string? Type { get; init; }

    /// <summary>Vehicle identification number (VIN) of the asset.</summary>
    [JsonPropertyName("vin")] public string? Vin { get; init; }
}

/// <summary>
/// Equipment statistics snapshot, returned by <c>GET /fleet/equipment/stats</c>.
/// Each metric is the single most-recent <c>{ time, value }</c> sample. The
/// time-series feed/history endpoints return arrays instead; see
/// <see cref="EquipmentStatsSample"/>.
/// </summary>
public sealed record EquipmentStats
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }

    /// <summary>Engine RPM.</summary>
    [JsonPropertyName("engineRpm")] public EquipmentStatValue? EngineRpm { get; init; }

    /// <summary>Total engine seconds.</summary>
    [JsonPropertyName("engineSeconds")] public EquipmentStatValue? EngineSeconds { get; init; }

    /// <summary>Engine on/off state.</summary>
    [JsonPropertyName("engineState")] public EquipmentStatStringValue? EngineState { get; init; }

    /// <summary>Engine total idle time, in minutes.</summary>
    [JsonPropertyName("engineTotalIdleTimeMinutes")] public EquipmentStatValue? EngineTotalIdleTimeMinutes { get; init; }

    /// <summary>Fuel level, as a percentage.</summary>
    [JsonPropertyName("fuelPercent")] public EquipmentStatValue? FuelPercent { get; init; }

    /// <summary>Gateway-reported engine seconds.</summary>
    [JsonPropertyName("gatewayEngineSeconds")] public EquipmentStatValue? GatewayEngineSeconds { get; init; }

    /// <summary>Gateway-reported engine on/off state.</summary>
    [JsonPropertyName("gatewayEngineState")] public EquipmentStatStringValue? GatewayEngineState { get; init; }

    /// <summary>GPS reading.</summary>
    [JsonPropertyName("gps")] public EquipmentStatGps? Gps { get; init; }

    /// <summary>GPS-derived odometer, in meters.</summary>
    [JsonPropertyName("gpsOdometerMeters")] public EquipmentStatValue? GpsOdometerMeters { get; init; }

    /// <summary>OBD-reported engine seconds.</summary>
    [JsonPropertyName("obdEngineSeconds")] public EquipmentStatValue? ObdEngineSeconds { get; init; }

    /// <summary>OBD-reported engine on/off state.</summary>
    [JsonPropertyName("obdEngineState")] public EquipmentStatStringValue? ObdEngineState { get; init; }
}

/// <summary>
/// Equipment statistics time-series row, returned by
/// <c>GET /fleet/equipment/stats/feed</c> and <c>GET /fleet/equipment/stats/history</c>.
/// Each metric is an array of <c>{ time, value }</c> samples covering the
/// requested window. The snapshot endpoint returns single values instead; see
/// <see cref="EquipmentStats"/>.
/// </summary>
public sealed record EquipmentStatsSample
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }

    /// <summary>Engine RPM samples.</summary>
    [JsonPropertyName("engineRpm")] public IReadOnlyList<EquipmentStatValue>? EngineRpm { get; init; }

    /// <summary>Total engine seconds samples.</summary>
    [JsonPropertyName("engineSeconds")] public IReadOnlyList<EquipmentStatValue>? EngineSeconds { get; init; }

    /// <summary>Engine on/off state samples.</summary>
    [JsonPropertyName("engineStates")] public IReadOnlyList<EquipmentStatStringValue>? EngineStates { get; init; }

    /// <summary>Engine total idle time (minutes) samples.</summary>
    [JsonPropertyName("engineTotalIdleTimeMinutes")] public IReadOnlyList<EquipmentStatValue>? EngineTotalIdleTimeMinutes { get; init; }

    /// <summary>Fuel percent samples.</summary>
    [JsonPropertyName("fuelPercents")] public IReadOnlyList<EquipmentStatValue>? FuelPercents { get; init; }

    /// <summary>Gateway-reported engine seconds samples.</summary>
    [JsonPropertyName("gatewayEngineSeconds")] public IReadOnlyList<EquipmentStatValue>? GatewayEngineSeconds { get; init; }

    /// <summary>Gateway engine on/off state samples.</summary>
    [JsonPropertyName("gatewayEngineStates")] public IReadOnlyList<EquipmentStatStringValue>? GatewayEngineStates { get; init; }

    /// <summary>Gateway J1939 engine seconds samples.</summary>
    [JsonPropertyName("gatewayJ1939EngineSeconds")] public IReadOnlyList<EquipmentStatValue>? GatewayJ1939EngineSeconds { get; init; }

    /// <summary>GPS reading samples.</summary>
    [JsonPropertyName("gps")] public IReadOnlyList<EquipmentStatGps>? Gps { get; init; }

    /// <summary>GPS-derived odometer (meters) samples.</summary>
    [JsonPropertyName("gpsOdometerMeters")] public IReadOnlyList<EquipmentStatValue>? GpsOdometerMeters { get; init; }

    /// <summary>OBD-reported engine seconds samples.</summary>
    [JsonPropertyName("obdEngineSeconds")] public IReadOnlyList<EquipmentStatValue>? ObdEngineSeconds { get; init; }

    /// <summary>OBD engine on/off state samples.</summary>
    [JsonPropertyName("obdEngineStates")] public IReadOnlyList<EquipmentStatStringValue>? ObdEngineStates { get; init; }
}

/// <summary>
/// A single numeric equipment statistic sample (<c>{ time, value }</c>), shared
/// by the equipment stats snapshot and feed/history endpoints.
/// </summary>
public sealed record EquipmentStatValue
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The measured value. Spec-required.</summary>
    [JsonPropertyName("value")] public required long Value { get; init; }
}

/// <summary>
/// A single string-valued equipment statistic sample (<c>{ time, value }</c>),
/// e.g. an engine on/off state.
/// </summary>
public sealed record EquipmentStatStringValue
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The measured value (e.g. <c>On</c> / <c>Off</c>). Spec-required.</summary>
    [JsonPropertyName("value")] public required string Value { get; init; }
}

/// <summary>
/// A single GPS reading sample on an equipment stats response. Mirrors the
/// spec's <c>EquipmentStatsGps</c> schema.
/// </summary>
public sealed record EquipmentStatGps
{
    /// <summary>Latitude in degrees. Spec-required.</summary>
    [JsonPropertyName("latitude")] public required double Latitude { get; init; }

    /// <summary>Longitude in degrees. Spec-required.</summary>
    [JsonPropertyName("longitude")] public required double Longitude { get; init; }

    /// <summary>Timestamp of the reading, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>Heading in degrees from true north.</summary>
    [JsonPropertyName("headingDegrees")] public double? HeadingDegrees { get; init; }

    /// <summary>Speed in miles per hour.</summary>
    [JsonPropertyName("speedMilesPerHour")] public double? SpeedMilesPerHour { get; init; }

    /// <summary>The nearest known address (place) to the reading.</summary>
    [JsonPropertyName("address")] public EquipmentStatAddress? Address { get; init; }

    /// <summary>Reverse-geocoded address for the reading.</summary>
    [JsonPropertyName("reverseGeo")] public ReverseGeo? ReverseGeo { get; init; }
}

/// <summary>
/// The nearest known address (place) to a GPS reading. Mirrors the spec's
/// <c>addressTinyResponse</c> schema.
/// </summary>
public sealed record EquipmentStatAddress
{
    /// <summary>Samsara ID of the address.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Name of the address.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }
}
