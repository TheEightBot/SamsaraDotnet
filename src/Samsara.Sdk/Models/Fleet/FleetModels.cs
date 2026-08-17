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

    /// <summary>
    /// Custom attributes on the vehicle. The spec's <c>attributeTiny</c> /
    /// <c>GoaAttributeTinyResponseBody</c> shape — id, name, and the typed value
    /// lists — not the full <c>Attribute</c> definition returned by
    /// <c>GET /attributes</c>.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AttributeTiny>? Attributes { get; init; }

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
/// <c>GrossVehicleWeight</c> schema, shared by the vehicle response bodies and
/// the <c>PATCH /fleet/vehicles/{id}</c> request body.
/// </summary>
public sealed record VehicleGrossWeight
{
    /// <summary>Unit of the weight value (<c>lb</c> or <c>kg</c>).</summary>
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

    /// <summary>External identifiers for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
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

    /// <summary>Gross vehicle weight. Mirrors the spec's <c>GrossVehicleWeight</c> schema.</summary>
    [JsonPropertyName("grossVehicleWeight")]
    public VehicleGrossWeight? GrossVehicleWeight { get; init; }

    [JsonPropertyName("gatewaySerial")]
    public string? GatewaySerial { get; init; }

    [JsonPropertyName("vehicleType")]
    public string? VehicleType { get; init; }

    /// <summary>Custom attributes to set on the vehicle (spec schema <c>attributeTiny</c>).</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AttributeTiny>? Attributes { get; init; }

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
/// Vehicle statistics snapshot, returned by <c>GET /fleet/vehicles/stats</c>
/// (<c>IVehiclesClient.ListStatsAsync</c>). Each metric is the single
/// most-recent <c>{ time, value }</c> sample. The time-series feed/history
/// endpoints return arrays of samples instead; see <see cref="VehicleStatsSample"/>.
/// </summary>
public sealed record VehicleStats
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Ambient air temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("ambientAirTemperatureMilliC")]
    public VehicleStatValue? AmbientAirTemperatureMilliC { get; init; }

    [JsonPropertyName("auxInput1")]
    public VehicleStatAuxInput? AuxInput1 { get; init; }

    [JsonPropertyName("auxInput2")]
    public VehicleStatAuxInput? AuxInput2 { get; init; }

    [JsonPropertyName("auxInput3")]
    public VehicleStatAuxInput? AuxInput3 { get; init; }

    [JsonPropertyName("auxInput4")]
    public VehicleStatAuxInput? AuxInput4 { get; init; }

    [JsonPropertyName("auxInput5")]
    public VehicleStatAuxInput? AuxInput5 { get; init; }

    [JsonPropertyName("auxInput6")]
    public VehicleStatAuxInput? AuxInput6 { get; init; }

    [JsonPropertyName("auxInput7")]
    public VehicleStatAuxInput? AuxInput7 { get; init; }

    [JsonPropertyName("auxInput8")]
    public VehicleStatAuxInput? AuxInput8 { get; init; }

    [JsonPropertyName("auxInput9")]
    public VehicleStatAuxInput? AuxInput9 { get; init; }

    [JsonPropertyName("auxInput10")]
    public VehicleStatAuxInput? AuxInput10 { get; init; }

    [JsonPropertyName("auxInput11")]
    public VehicleStatAuxInput? AuxInput11 { get; init; }

    [JsonPropertyName("auxInput12")]
    public VehicleStatAuxInput? AuxInput12 { get; init; }

    [JsonPropertyName("auxInput13")]
    public VehicleStatAuxInput? AuxInput13 { get; init; }

    /// <summary>Barometric pressure, in pascals.</summary>
    [JsonPropertyName("barometricPressurePa")]
    public VehicleStatValue? BarometricPressurePa { get; init; }

    /// <summary>Battery voltage, in millivolts.</summary>
    [JsonPropertyName("batteryMilliVolts")]
    public VehicleStatValue? BatteryMilliVolts { get; init; }

    /// <summary>Diesel exhaust fluid level, in milli-percent.</summary>
    [JsonPropertyName("defLevelMilliPercent")]
    public VehicleStatValue? DefLevelMilliPercent { get; init; }

    /// <summary>Door status as read from the ECU (or AUX as a fallback).</summary>
    [JsonPropertyName("ecuDoorStatus")]
    public VehicleStatStringValue? EcuDoorStatus { get; init; }

    /// <summary>ECU-reported road speed, in miles per hour.</summary>
    [JsonPropertyName("ecuSpeedMph")]
    public VehicleStatDoubleValue? EcuSpeedMph { get; init; }

    /// <summary>Engine coolant temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("engineCoolantTemperatureMilliC")]
    public VehicleStatValue? EngineCoolantTemperatureMilliC { get; init; }

    /// <summary>Engine immobilizer state.</summary>
    [JsonPropertyName("engineImmobilizer")]
    public VehicleStatEngineImmobilizer? EngineImmobilizer { get; init; }

    /// <summary>Engine load, as a percentage.</summary>
    [JsonPropertyName("engineLoadPercent")]
    public VehicleStatValue? EngineLoadPercent { get; init; }

    /// <summary>Engine oil pressure, in kilopascals.</summary>
    [JsonPropertyName("engineOilPressureKPa")]
    public VehicleStatValue? EngineOilPressureKPa { get; init; }

    /// <summary>Engine RPM.</summary>
    [JsonPropertyName("engineRpm")]
    public VehicleStatValue? EngineRpm { get; init; }

    /// <summary>Engine on/off state (<c>Off</c>, <c>On</c>, or <c>Idle</c>).</summary>
    [JsonPropertyName("engineState")]
    public VehicleStatStringValue? EngineState { get; init; }

    /// <summary>Average EV battery temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("evAverageBatteryTemperatureMilliCelsius")]
    public VehicleStatValue? EvAverageBatteryTemperatureMilliCelsius { get; init; }

    /// <summary>EV battery current, in milliamps.</summary>
    [JsonPropertyName("evBatteryCurrentMilliAmp")]
    public VehicleStatValue? EvBatteryCurrentMilliAmp { get; init; }

    /// <summary>EV battery state of health, in milli-percent.</summary>
    [JsonPropertyName("evBatteryStateOfHealthMilliPercent")]
    public VehicleStatValue? EvBatteryStateOfHealthMilliPercent { get; init; }

    /// <summary>EV battery voltage, in millivolts.</summary>
    [JsonPropertyName("evBatteryVoltageMilliVolt")]
    public VehicleStatValue? EvBatteryVoltageMilliVolt { get; init; }

    /// <summary>EV charging current, in milliamps.</summary>
    [JsonPropertyName("evChargingCurrentMilliAmp")]
    public VehicleStatValue? EvChargingCurrentMilliAmp { get; init; }

    /// <summary>EV charging energy, in micro-watt-hours.</summary>
    [JsonPropertyName("evChargingEnergyMicroWh")]
    public VehicleStatValue? EvChargingEnergyMicroWh { get; init; }

    /// <summary>EV charging status code.</summary>
    [JsonPropertyName("evChargingStatus")]
    public VehicleStatValue? EvChargingStatus { get; init; }

    /// <summary>EV charging voltage, in millivolts.</summary>
    [JsonPropertyName("evChargingVoltageMilliVolt")]
    public VehicleStatValue? EvChargingVoltageMilliVolt { get; init; }

    /// <summary>EV energy consumed, in micro-watt-hours.</summary>
    [JsonPropertyName("evConsumedEnergyMicroWh")]
    public VehicleStatValue? EvConsumedEnergyMicroWh { get; init; }

    /// <summary>EV distance driven, in meters.</summary>
    [JsonPropertyName("evDistanceDrivenMeters")]
    public VehicleStatValue? EvDistanceDrivenMeters { get; init; }

    /// <summary>EV energy regenerated, in micro-watt-hours.</summary>
    [JsonPropertyName("evRegeneratedEnergyMicroWh")]
    public VehicleStatValue? EvRegeneratedEnergyMicroWh { get; init; }

    /// <summary>EV state of charge, in milli-percent.</summary>
    [JsonPropertyName("evStateOfChargeMilliPercent")]
    public VehicleStatValue? EvStateOfChargeMilliPercent { get; init; }

    /// <summary>External IDs associated with the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Engine fault codes read from J1939, OBD-II, and OEM vehicles.</summary>
    [JsonPropertyName("faultCodes")]
    public VehicleStatFaultCodes? FaultCodes { get; init; }

    /// <summary>Fuel consumed, in milliliters.</summary>
    [JsonPropertyName("fuelConsumedMilliliters")]
    public VehicleStatValue? FuelConsumedMilliliters { get; init; }

    /// <summary>Fuel level, as a percentage.</summary>
    [JsonPropertyName("fuelPercent")]
    public VehicleStatValue? FuelPercent { get; init; }

    /// <summary>GPS reading.</summary>
    [JsonPropertyName("gps")]
    public VehicleStatGps? Gps { get; init; }

    /// <summary>GPS-measured trip distance, in meters.</summary>
    [JsonPropertyName("gpsDistanceMeters")]
    public VehicleStatDoubleValue? GpsDistanceMeters { get; init; }

    /// <summary>GPS-derived odometer, in meters.</summary>
    [JsonPropertyName("gpsOdometerMeters")]
    public VehicleStatValue? GpsOdometerMeters { get; init; }

    /// <summary>Idling duration, in milliseconds.</summary>
    [JsonPropertyName("idlingDurationMilliseconds")]
    public VehicleStatValue? IdlingDurationMilliseconds { get; init; }

    /// <summary>Intake manifold temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("intakeManifoldTemperatureMilliC")]
    public VehicleStatValue? IntakeManifoldTemperatureMilliC { get; init; }

    /// <summary>Most-recent NFC card scan.</summary>
    [JsonPropertyName("nfcCardScan")]
    public VehicleStatNfcCardScan? NfcCardScan { get; init; }

    /// <summary>OBD-reported engine seconds.</summary>
    [JsonPropertyName("obdEngineSeconds")]
    public VehicleStatValue? ObdEngineSeconds { get; init; }

    /// <summary>OBD-reported odometer, in meters.</summary>
    [JsonPropertyName("obdOdometerMeters")]
    public VehicleStatValue? ObdOdometerMeters { get; init; }

    /// <summary>Driver seatbelt state (<c>Buckled</c> / <c>Unbuckled</c>).</summary>
    [JsonPropertyName("seatbeltDriver")]
    public VehicleStatStringValue? SeatbeltDriver { get; init; }

    /// <summary>Whether the spreader is active (<c>On</c> / <c>Off</c>).</summary>
    [JsonPropertyName("spreaderActive")]
    public VehicleStatStringValue? SpreaderActive { get; init; }

    /// <summary>Spreader air temperature.</summary>
    [JsonPropertyName("spreaderAirTemp")]
    public VehicleStatValue? SpreaderAirTemp { get; init; }

    /// <summary>Spreader blast state (<c>On</c> / <c>Off</c>).</summary>
    [JsonPropertyName("spreaderBlastState")]
    public VehicleStatStringValue? SpreaderBlastState { get; init; }

    /// <summary>Spreader granular material name.</summary>
    [JsonPropertyName("spreaderGranularName")]
    public VehicleStatStringValue? SpreaderGranularName { get; init; }

    /// <summary>Spreader granular material application rate.</summary>
    [JsonPropertyName("spreaderGranularRate")]
    public VehicleStatValue? SpreaderGranularRate { get; init; }

    /// <summary>Spreader liquid material name.</summary>
    [JsonPropertyName("spreaderLiquidName")]
    public VehicleStatStringValue? SpreaderLiquidName { get; init; }

    /// <summary>Spreader liquid material application rate.</summary>
    [JsonPropertyName("spreaderLiquidRate")]
    public VehicleStatValue? SpreaderLiquidRate { get; init; }

    /// <summary>Spreader on state (<c>On</c> / <c>Off</c>).</summary>
    [JsonPropertyName("spreaderOnState")]
    public VehicleStatStringValue? SpreaderOnState { get; init; }

    /// <summary>Spreader plow status (<c>Up</c> / <c>Down</c>).</summary>
    [JsonPropertyName("spreaderPlowStatus")]
    public VehicleStatStringValue? SpreaderPlowStatus { get; init; }

    /// <summary>Spreader pre-wet material name.</summary>
    [JsonPropertyName("spreaderPrewetName")]
    public VehicleStatStringValue? SpreaderPrewetName { get; init; }

    /// <summary>Spreader pre-wet material application rate.</summary>
    [JsonPropertyName("spreaderPrewetRate")]
    public VehicleStatValue? SpreaderPrewetRate { get; init; }

    /// <summary>Spreader road temperature.</summary>
    [JsonPropertyName("spreaderRoadTemp")]
    public VehicleStatValue? SpreaderRoadTemp { get; init; }

    /// <summary>Synthetic (Samsara-computed) engine seconds.</summary>
    [JsonPropertyName("syntheticEngineSeconds")]
    public VehicleStatValue? SyntheticEngineSeconds { get; init; }
}

/// <summary>
/// Vehicle statistics time-series row, returned by
/// <c>GET /fleet/vehicles/stats/feed</c> (<c>IVehiclesClient.GetStatsFeedAsync</c>)
/// and <c>GET /fleet/vehicles/stats/history</c> (<c>IVehiclesClient.GetStatsHistoryAsync</c>).
/// Each metric is an array of <c>{ time, value }</c> samples covering the
/// requested window. The snapshot endpoint returns single values instead; see
/// <see cref="VehicleStats"/>.
/// </summary>
/// <remarks>
/// On the feed/history endpoints <c>id</c> and <c>name</c> are spec-optional, so
/// they are nullable here (unlike the snapshot shape where they are required).
/// </remarks>
public sealed record VehicleStatsSample
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Ambient air temperature samples, in milli-degrees Celsius.</summary>
    [JsonPropertyName("ambientAirTemperatureMilliC")]
    public IReadOnlyList<VehicleStatValue>? AmbientAirTemperatureMilliC { get; init; }

    [JsonPropertyName("auxInput1")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput1 { get; init; }

    [JsonPropertyName("auxInput2")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput2 { get; init; }

    [JsonPropertyName("auxInput3")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput3 { get; init; }

    [JsonPropertyName("auxInput4")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput4 { get; init; }

    [JsonPropertyName("auxInput5")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput5 { get; init; }

    [JsonPropertyName("auxInput6")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput6 { get; init; }

    [JsonPropertyName("auxInput7")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput7 { get; init; }

    [JsonPropertyName("auxInput8")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput8 { get; init; }

    [JsonPropertyName("auxInput9")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput9 { get; init; }

    [JsonPropertyName("auxInput10")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput10 { get; init; }

    [JsonPropertyName("auxInput11")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput11 { get; init; }

    [JsonPropertyName("auxInput12")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput12 { get; init; }

    [JsonPropertyName("auxInput13")]
    public IReadOnlyList<VehicleStatAuxInput>? AuxInput13 { get; init; }

    /// <summary>Barometric pressure samples, in pascals.</summary>
    [JsonPropertyName("barometricPressurePa")]
    public IReadOnlyList<VehicleStatValue>? BarometricPressurePa { get; init; }

    /// <summary>Battery voltage samples, in millivolts.</summary>
    [JsonPropertyName("batteryMilliVolts")]
    public IReadOnlyList<VehicleStatValue>? BatteryMilliVolts { get; init; }

    /// <summary>Diesel exhaust fluid level samples, in milli-percent.</summary>
    [JsonPropertyName("defLevelMilliPercent")]
    public IReadOnlyList<VehicleStatValue>? DefLevelMilliPercent { get; init; }

    /// <summary>Door status samples.</summary>
    [JsonPropertyName("ecuDoorStatus")]
    public IReadOnlyList<VehicleStatStringValue>? EcuDoorStatus { get; init; }

    /// <summary>ECU road speed samples, in miles per hour.</summary>
    [JsonPropertyName("ecuSpeedMph")]
    public IReadOnlyList<VehicleStatDoubleValue>? EcuSpeedMph { get; init; }

    /// <summary>Engine coolant temperature samples, in milli-degrees Celsius.</summary>
    [JsonPropertyName("engineCoolantTemperatureMilliC")]
    public IReadOnlyList<VehicleStatValue>? EngineCoolantTemperatureMilliC { get; init; }

    /// <summary>Engine immobilizer samples.</summary>
    [JsonPropertyName("engineImmobilizer")]
    public IReadOnlyList<VehicleStatEngineImmobilizer>? EngineImmobilizer { get; init; }

    /// <summary>Engine load samples, as a percentage.</summary>
    [JsonPropertyName("engineLoadPercent")]
    public IReadOnlyList<VehicleStatValue>? EngineLoadPercent { get; init; }

    /// <summary>Engine oil pressure samples, in kilopascals.</summary>
    [JsonPropertyName("engineOilPressureKPa")]
    public IReadOnlyList<VehicleStatValue>? EngineOilPressureKPa { get; init; }

    /// <summary>Engine RPM samples.</summary>
    [JsonPropertyName("engineRpm")]
    public IReadOnlyList<VehicleStatValue>? EngineRpm { get; init; }

    /// <summary>Engine on/off state samples.</summary>
    [JsonPropertyName("engineStates")]
    public IReadOnlyList<VehicleStatStringValue>? EngineStates { get; init; }

    /// <summary>Average EV battery temperature samples, in milli-degrees Celsius.</summary>
    [JsonPropertyName("evAverageBatteryTemperatureMilliCelsius")]
    public IReadOnlyList<VehicleStatValue>? EvAverageBatteryTemperatureMilliCelsius { get; init; }

    /// <summary>EV battery current samples, in milliamps.</summary>
    [JsonPropertyName("evBatteryCurrentMilliAmp")]
    public IReadOnlyList<VehicleStatValue>? EvBatteryCurrentMilliAmp { get; init; }

    /// <summary>EV battery state of health samples, in milli-percent.</summary>
    [JsonPropertyName("evBatteryStateOfHealthMilliPercent")]
    public IReadOnlyList<VehicleStatValue>? EvBatteryStateOfHealthMilliPercent { get; init; }

    /// <summary>EV battery voltage samples, in millivolts.</summary>
    [JsonPropertyName("evBatteryVoltageMilliVolt")]
    public IReadOnlyList<VehicleStatValue>? EvBatteryVoltageMilliVolt { get; init; }

    /// <summary>EV charging current samples, in milliamps.</summary>
    [JsonPropertyName("evChargingCurrentMilliAmp")]
    public IReadOnlyList<VehicleStatValue>? EvChargingCurrentMilliAmp { get; init; }

    /// <summary>EV charging energy samples, in micro-watt-hours.</summary>
    [JsonPropertyName("evChargingEnergyMicroWh")]
    public IReadOnlyList<VehicleStatValue>? EvChargingEnergyMicroWh { get; init; }

    /// <summary>EV charging status samples.</summary>
    [JsonPropertyName("evChargingStatus")]
    public IReadOnlyList<VehicleStatValue>? EvChargingStatus { get; init; }

    /// <summary>EV charging voltage samples, in millivolts.</summary>
    [JsonPropertyName("evChargingVoltageMilliVolt")]
    public IReadOnlyList<VehicleStatValue>? EvChargingVoltageMilliVolt { get; init; }

    /// <summary>EV energy consumed samples, in micro-watt-hours.</summary>
    [JsonPropertyName("evConsumedEnergyMicroWh")]
    public IReadOnlyList<VehicleStatValue>? EvConsumedEnergyMicroWh { get; init; }

    /// <summary>EV distance driven samples, in meters.</summary>
    [JsonPropertyName("evDistanceDrivenMeters")]
    public IReadOnlyList<VehicleStatValue>? EvDistanceDrivenMeters { get; init; }

    /// <summary>EV energy regenerated samples, in micro-watt-hours.</summary>
    [JsonPropertyName("evRegeneratedEnergyMicroWh")]
    public IReadOnlyList<VehicleStatValue>? EvRegeneratedEnergyMicroWh { get; init; }

    /// <summary>EV state of charge samples, in milli-percent.</summary>
    [JsonPropertyName("evStateOfChargeMilliPercent")]
    public IReadOnlyList<VehicleStatValue>? EvStateOfChargeMilliPercent { get; init; }

    /// <summary>External IDs associated with the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Engine fault code samples.</summary>
    [JsonPropertyName("faultCodes")]
    public IReadOnlyList<VehicleStatFaultCodes>? FaultCodes { get; init; }

    /// <summary>Fuel consumed samples, in milliliters.</summary>
    [JsonPropertyName("fuelConsumedMilliliters")]
    public IReadOnlyList<VehicleStatValue>? FuelConsumedMilliliters { get; init; }

    /// <summary>Fuel level samples, as a percentage.</summary>
    [JsonPropertyName("fuelPercents")]
    public IReadOnlyList<VehicleStatValue>? FuelPercents { get; init; }

    /// <summary>GPS reading samples.</summary>
    [JsonPropertyName("gps")]
    public IReadOnlyList<VehicleStatGps>? Gps { get; init; }

    /// <summary>GPS-measured trip distance samples, in meters.</summary>
    [JsonPropertyName("gpsDistanceMeters")]
    public IReadOnlyList<VehicleStatDoubleValue>? GpsDistanceMeters { get; init; }

    /// <summary>GPS-derived odometer samples, in meters.</summary>
    [JsonPropertyName("gpsOdometerMeters")]
    public IReadOnlyList<VehicleStatValue>? GpsOdometerMeters { get; init; }

    /// <summary>Idling duration samples, in milliseconds.</summary>
    [JsonPropertyName("idlingDurationMilliseconds")]
    public IReadOnlyList<VehicleStatValue>? IdlingDurationMilliseconds { get; init; }

    /// <summary>Intake manifold temperature samples, in milli-degrees Celsius.</summary>
    [JsonPropertyName("intakeManifoldTemperatureMilliC")]
    public IReadOnlyList<VehicleStatValue>? IntakeManifoldTemperatureMilliC { get; init; }

    /// <summary>NFC card scan samples.</summary>
    [JsonPropertyName("nfcCardScans")]
    public IReadOnlyList<VehicleStatNfcCardScan>? NfcCardScans { get; init; }

    /// <summary>OBD-reported engine seconds samples.</summary>
    [JsonPropertyName("obdEngineSeconds")]
    public IReadOnlyList<VehicleStatValue>? ObdEngineSeconds { get; init; }

    /// <summary>OBD-reported odometer samples, in meters.</summary>
    [JsonPropertyName("obdOdometerMeters")]
    public IReadOnlyList<VehicleStatValue>? ObdOdometerMeters { get; init; }

    /// <summary>Driver seatbelt state samples.</summary>
    [JsonPropertyName("seatbeltDriver")]
    public IReadOnlyList<VehicleStatStringValue>? SeatbeltDriver { get; init; }

    /// <summary>Spreader active samples.</summary>
    [JsonPropertyName("spreaderActive")]
    public IReadOnlyList<VehicleStatStringValue>? SpreaderActive { get; init; }

    /// <summary>Spreader air temperature samples.</summary>
    [JsonPropertyName("spreaderAirTemp")]
    public IReadOnlyList<VehicleStatValue>? SpreaderAirTemp { get; init; }

    /// <summary>Spreader blast state samples.</summary>
    [JsonPropertyName("spreaderBlastState")]
    public IReadOnlyList<VehicleStatStringValue>? SpreaderBlastState { get; init; }

    /// <summary>Spreader granular material name samples.</summary>
    [JsonPropertyName("spreaderGranularName")]
    public IReadOnlyList<VehicleStatStringValue>? SpreaderGranularName { get; init; }

    /// <summary>Spreader granular material application rate samples.</summary>
    [JsonPropertyName("spreaderGranularRate")]
    public IReadOnlyList<VehicleStatValue>? SpreaderGranularRate { get; init; }

    /// <summary>Spreader liquid material name samples.</summary>
    [JsonPropertyName("spreaderLiquidName")]
    public IReadOnlyList<VehicleStatStringValue>? SpreaderLiquidName { get; init; }

    /// <summary>Spreader liquid material application rate samples.</summary>
    [JsonPropertyName("spreaderLiquidRate")]
    public IReadOnlyList<VehicleStatValue>? SpreaderLiquidRate { get; init; }

    /// <summary>Spreader on state samples.</summary>
    [JsonPropertyName("spreaderOnState")]
    public IReadOnlyList<VehicleStatStringValue>? SpreaderOnState { get; init; }

    /// <summary>Spreader plow status samples.</summary>
    [JsonPropertyName("spreaderPlowStatus")]
    public IReadOnlyList<VehicleStatStringValue>? SpreaderPlowStatus { get; init; }

    /// <summary>Spreader pre-wet material name samples.</summary>
    [JsonPropertyName("spreaderPrewetName")]
    public IReadOnlyList<VehicleStatStringValue>? SpreaderPrewetName { get; init; }

    /// <summary>Spreader pre-wet material application rate samples.</summary>
    [JsonPropertyName("spreaderPrewetRate")]
    public IReadOnlyList<VehicleStatValue>? SpreaderPrewetRate { get; init; }

    /// <summary>Spreader road temperature samples.</summary>
    [JsonPropertyName("spreaderRoadTemp")]
    public IReadOnlyList<VehicleStatValue>? SpreaderRoadTemp { get; init; }

    /// <summary>Synthetic (Samsara-computed) engine seconds samples.</summary>
    [JsonPropertyName("syntheticEngineSeconds")]
    public IReadOnlyList<VehicleStatValue>? SyntheticEngineSeconds { get; init; }
}

/// <summary>
/// A single integer-valued vehicle statistic sample (<c>{ time, value }</c>),
/// shared by the vehicle stats snapshot and feed/history endpoints.
/// </summary>
public sealed record VehicleStatValue
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The measured value. Spec-required.</summary>
    [JsonPropertyName("value")] public required long Value { get; init; }

    /// <summary>
    /// Decorated values captured alongside this sample (spec
    /// <c>VehicleStatsDecorations</c>) — the other metrics as of the same moment.
    /// </summary>
    [JsonPropertyName("decorations")] public VehicleStatDecorations? Decorations { get; init; }
}

/// <summary>
/// A single floating-point vehicle statistic sample (<c>{ time, value }</c>),
/// e.g. a speed or distance reading.
/// </summary>
public sealed record VehicleStatDoubleValue
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The measured value. Spec-required.</summary>
    [JsonPropertyName("value")] public required double Value { get; init; }

    /// <summary>
    /// Decorated values captured alongside this sample (spec
    /// <c>VehicleStatsDecorations</c>) — the other metrics as of the same moment.
    /// </summary>
    [JsonPropertyName("decorations")] public VehicleStatDecorations? Decorations { get; init; }
}

/// <summary>
/// A single string- or enum-valued vehicle statistic sample
/// (<c>{ time, value }</c>), e.g. an engine state or spreader status. The value
/// is exposed as a string to remain forward-compatible with new enum members.
/// </summary>
public sealed record VehicleStatStringValue
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The measured value. Spec-required.</summary>
    [JsonPropertyName("value")] public required string Value { get; init; }

    /// <summary>
    /// Decorated values captured alongside this sample (spec
    /// <c>VehicleStatsDecorations</c>) — the other metrics as of the same moment.
    /// </summary>
    [JsonPropertyName("decorations")] public VehicleStatDecorations? Decorations { get; init; }
}

/// <summary>
/// A single auxiliary-input vehicle statistic sample. Carries a boolean state
/// and an optional human-readable input name.
/// </summary>
public sealed record VehicleStatAuxInput
{
    /// <summary>
    /// Timestamp of the sample, in RFC 3339 format. Nullable: the spec's
    /// <c>VehicleStatsAuxInput</c> declares no <c>required</c> list at all.
    /// </summary>
    [JsonPropertyName("time")] public DateTimeOffset? Time { get; init; }

    /// <summary>
    /// Whether the auxiliary input is active. Nullable: the spec's
    /// <c>VehicleStatsAuxInput</c> declares no <c>required</c> list at all.
    /// </summary>
    [JsonPropertyName("value")] public bool? Value { get; init; }

    /// <summary>Human-readable name configured for this auxiliary input.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }

    /// <summary>
    /// Decorated values captured alongside this sample (spec
    /// <c>VehicleStatsDecorations</c>) — the other metrics as of the same moment.
    /// </summary>
    [JsonPropertyName("decorations")] public VehicleStatDecorations? Decorations { get; init; }
}

/// <summary>
/// Engine immobilizer sample. Reports whether the immobilizer is connected and
/// its current ignition state.
/// </summary>
public sealed record VehicleStatEngineImmobilizer
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>Whether the engine immobilizer is connected. Spec-required.</summary>
    [JsonPropertyName("connected")] public required bool Connected { get; init; }

    /// <summary>Immobilizer state (<c>ignition_disabled</c> / <c>ignition_enabled</c>). Spec-required.</summary>
    [JsonPropertyName("state")] public required string State { get; init; }

    /// <summary>
    /// Decorated values captured alongside this sample (spec
    /// <c>VehicleStatsDecorations</c>) — the other metrics as of the same moment.
    /// </summary>
    [JsonPropertyName("decorations")] public VehicleStatDecorations? Decorations { get; init; }
}

/// <summary>
/// NFC card scan sample. Carries the scanned card reference and the scan time.
/// </summary>
public sealed record VehicleStatNfcCardScan
{
    /// <summary>Timestamp of the scan, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The scanned NFC card. Spec-required.</summary>
    [JsonPropertyName("card")] public required VehicleStatNfcCard Card { get; init; }

    /// <summary>
    /// Decorated values captured alongside this sample (spec
    /// <c>VehicleStatsDecorations</c>) — the other metrics as of the same moment.
    /// </summary>
    [JsonPropertyName("decorations")] public VehicleStatDecorations? Decorations { get; init; }
}

/// <summary>An NFC card reference on a <see cref="VehicleStatNfcCardScan"/>.</summary>
public sealed record VehicleStatNfcCard
{
    /// <summary>Samsara ID of the NFC card.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }
}

/// <summary>
/// A single GPS reading sample on a vehicle stats response. Mirrors the spec's
/// <c>VehicleStatsGps</c> schema.
/// </summary>
public sealed record VehicleStatGps
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

    /// <summary>Whether the reported speed is sourced from the ECU rather than GPS.</summary>
    [JsonPropertyName("isEcuSpeed")] public bool? IsEcuSpeed { get; init; }

    /// <summary>The nearest known address (place) to the reading.</summary>
    [JsonPropertyName("address")] public VehicleStatAddress? Address { get; init; }

    /// <summary>Reverse-geocoded address for the reading.</summary>
    [JsonPropertyName("reverseGeo")] public ReverseGeo? ReverseGeo { get; init; }

    /// <summary>
    /// Decorated values captured alongside this sample (spec
    /// <c>VehicleStatsDecorations</c>) — the other metrics as of the same moment.
    /// </summary>
    [JsonPropertyName("decorations")] public VehicleStatDecorations? Decorations { get; init; }
}

/// <summary>
/// The nearest known address (place) to a GPS reading. Mirrors the spec's
/// <c>addressTinyResponse</c> schema.
/// </summary>
public sealed record VehicleStatAddress
{
    /// <summary>Samsara ID of the address.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Name of the address.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }
}

/// <summary>
/// Engine fault codes sample, read from J1939, OBD-II, and OEM vehicles. Mirrors
/// the spec's <c>VehicleStatsFaultCodes</c> schema.
/// </summary>
public sealed record VehicleStatFaultCodes
{
    /// <summary>Timestamp of the reading, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The CAN bus protocol the fault codes were read from.</summary>
    [JsonPropertyName("canBusType")] public string? CanBusType { get; init; }

    /// <summary>J1939 (heavy-duty) fault codes.</summary>
    [JsonPropertyName("j1939")] public VehicleStatFaultCodesJ1939? J1939 { get; init; }

    /// <summary>OBD-II (light-duty) fault codes.</summary>
    [JsonPropertyName("obdii")] public VehicleStatFaultCodesObdii? Obdii { get; init; }

    /// <summary>OEM-specific fault codes.</summary>
    [JsonPropertyName("oem")] public VehicleStatFaultCodesOem? Oem { get; init; }

    /// <summary>
    /// Decorated values captured alongside this sample (spec
    /// <c>VehicleStatsDecorations</c>) — the other metrics as of the same moment.
    /// </summary>
    [JsonPropertyName("decorations")] public VehicleStatDecorations? Decorations { get; init; }
}

/// <summary>J1939 fault-code detail on a <see cref="VehicleStatFaultCodes"/> reading.</summary>
public sealed record VehicleStatFaultCodesJ1939
{
    /// <summary>Check-engine indicator lamp states.</summary>
    [JsonPropertyName("checkEngineLights")] public VehicleStatCheckEngineLights? CheckEngineLights { get; init; }

    /// <summary>Active diagnostic trouble codes.</summary>
    [JsonPropertyName("diagnosticTroubleCodes")] public IReadOnlyList<VehicleStatJ1939Dtc>? DiagnosticTroubleCodes { get; init; }
}

/// <summary>Check-engine lamp states reported over J1939.</summary>
public sealed record VehicleStatCheckEngineLights
{
    [JsonPropertyName("emissionsIsOn")] public bool? EmissionsIsOn { get; init; }
    [JsonPropertyName("protectIsOn")] public bool? ProtectIsOn { get; init; }
    [JsonPropertyName("stopIsOn")] public bool? StopIsOn { get; init; }
    [JsonPropertyName("warningIsOn")] public bool? WarningIsOn { get; init; }
}

/// <summary>A single J1939 diagnostic trouble code.</summary>
public sealed record VehicleStatJ1939Dtc
{
    /// <summary>Suspect Parameter Number.</summary>
    [JsonPropertyName("spnId")] public int? SpnId { get; init; }

    /// <summary>Failure Mode Identifier.</summary>
    [JsonPropertyName("fmiId")] public int? FmiId { get; init; }

    /// <summary>Human-readable description of the SPN.</summary>
    [JsonPropertyName("spnDescription")] public string? SpnDescription { get; init; }

    /// <summary>Human-readable description of the FMI.</summary>
    [JsonPropertyName("fmiDescription")] public string? FmiDescription { get; init; }

    /// <summary>Occurrence count for this trouble code.</summary>
    [JsonPropertyName("occurrenceCount")] public int? OccurrenceCount { get; init; }

    /// <summary>Transmitting source address.</summary>
    [JsonPropertyName("txId")] public int? TxId { get; init; }

    /// <summary>
    /// The MIL status, indicating a check engine light. Spec marks REQUIRED;
    /// nullable because this is a response record.
    /// </summary>
    [JsonPropertyName("milStatus")] public int? MilStatus { get; init; }

    /// <summary>The source address name corresponding to the <c>txId</c>.</summary>
    [JsonPropertyName("sourceAddressName")] public string? SourceAddressName { get; init; }

    /// <summary>Vendor-specific data for J1939 vehicles.</summary>
    [JsonPropertyName("vendorSpecificFields")] public VehicleStatJ1939VendorSpecificFields? VendorSpecificFields { get; init; }
}

/// <summary>OBD-II fault-code detail on a <see cref="VehicleStatFaultCodes"/> reading.</summary>
public sealed record VehicleStatFaultCodesObdii
{
    /// <summary>Whether the malfunction-indicator (check-engine) lamp is on.</summary>
    [JsonPropertyName("checkEngineLightIsOn")] public bool? CheckEngineLightIsOn { get; init; }

    /// <summary>Diagnostic trouble code groupings, by transmitting source.</summary>
    [JsonPropertyName("diagnosticTroubleCodes")] public IReadOnlyList<VehicleStatObdiiDtcGroup>? DiagnosticTroubleCodes { get; init; }
}

/// <summary>A grouping of OBD-II diagnostic trouble codes from one transmitter.</summary>
public sealed record VehicleStatObdiiDtcGroup
{
    /// <summary>Transmitting source identifier.</summary>
    [JsonPropertyName("txId")] public int? TxId { get; init; }

    /// <summary>Ignition type (<c>spark</c> / <c>compression</c>).</summary>
    [JsonPropertyName("ignitionType")] public string? IgnitionType { get; init; }

    /// <summary>Whether the malfunction-indicator lamp is set.</summary>
    [JsonPropertyName("milStatus")] public bool? MilStatus { get; init; }

    /// <summary>Readings from the engine readiness monitors.</summary>
    [JsonPropertyName("monitorStatus")] public VehicleStatObdiiMonitorStatus? MonitorStatus { get; init; }

    /// <summary>Confirmed diagnostic trouble codes.</summary>
    [JsonPropertyName("confirmedDtcs")] public IReadOnlyList<VehicleStatObdiiDtc>? ConfirmedDtcs { get; init; }

    /// <summary>Pending diagnostic trouble codes.</summary>
    [JsonPropertyName("pendingDtcs")] public IReadOnlyList<VehicleStatObdiiDtc>? PendingDtcs { get; init; }

    /// <summary>Permanent diagnostic trouble codes.</summary>
    [JsonPropertyName("permanentDtcs")] public IReadOnlyList<VehicleStatObdiiDtc>? PermanentDtcs { get; init; }
}

/// <summary>A single OBD-II diagnostic trouble code.</summary>
public sealed record VehicleStatObdiiDtc
{
    /// <summary>Numeric DTC identifier.</summary>
    [JsonPropertyName("dtcId")] public int? DtcId { get; init; }

    /// <summary>Short code (e.g. <c>P0420</c>).</summary>
    [JsonPropertyName("dtcShortCode")] public string? DtcShortCode { get; init; }

    /// <summary>Human-readable description of the DTC.</summary>
    [JsonPropertyName("dtcDescription")] public string? DtcDescription { get; init; }
}

/// <summary>
/// OEM-specific fault-code detail on a <see cref="VehicleStatFaultCodes"/>
/// reading. Mirrors the spec's <c>VehicleStatsFaultCodesOem</c> schema.
/// </summary>
public sealed record VehicleStatFaultCodesOem
{
    /// <summary>OEM-specific diagnostic trouble codes.</summary>
    [JsonPropertyName("diagnosticTroubleCodes")] public IReadOnlyList<VehicleStatOemDtc>? DiagnosticTroubleCodes { get; init; }
}

/// <summary>
/// A single OEM-specific diagnostic trouble code. Mirrors the spec's
/// <c>VehicleStatsFaultCodesOemTroubleCode</c> schema.
/// </summary>
/// <remarks>
/// Named with the <c>VehicleStat</c> prefix (rather than the stripped spec name)
/// to sit alongside the sibling <c>VehicleStatObdiiDtc</c> and
/// <c>VehicleStatJ1939Dtc</c> records, and to avoid colliding with the
/// maintenance domain's unrelated <c>DiagnosticTroubleCode</c> record.
/// </remarks>
public sealed record VehicleStatOemDtc
{
    /// <summary>The OEM code identifier.</summary>
    [JsonPropertyName("codeIdentifier")] public string? CodeIdentifier { get; init; }

    /// <summary>The OEM code description.</summary>
    [JsonPropertyName("codeDescription")] public string? CodeDescription { get; init; }

    /// <summary>The OEM code severity.</summary>
    [JsonPropertyName("codeSeverity")] public string? CodeSeverity { get; init; }

    /// <summary>The OEM code source.</summary>
    [JsonPropertyName("codeSource")] public string? CodeSource { get; init; }
}

/// <summary>
/// The other vehicle metrics captured alongside a single stats sample. Mirrors
/// the spec's <c>VehicleStatsDecorations</c>, which every
/// <c>...WithDecoration</c> response body hangs off its <c>decorations</c>
/// property.
/// </summary>
/// <remarks>
/// Decoration entries are NOT the same shape as the metric they decorate: most
/// carry only a <c>value</c> (no <c>time</c>, since the time is the decorated
/// sample's own). Only the entries the spec genuinely gives a <c>time</c> reuse
/// the <c>{ time, value }</c> records. Everything here is nullable — a
/// decoration is populated only when the caller asked for it via the
/// <c>decorations</c> query parameter.
/// </remarks>
public sealed record VehicleStatDecorations
{
    /// <summary>Ambient air temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("ambientAirTemperatureMilliC")] public VehicleStatDecorationValue? AmbientAirTemperatureMilliC { get; init; }

    /// <summary>Auxiliary input 1 as of this sample.</summary>
    [JsonPropertyName("auxInput1")] public VehicleStatDecorationAuxInput? AuxInput1 { get; init; }

    /// <summary>Auxiliary input 2 as of this sample.</summary>
    [JsonPropertyName("auxInput2")] public VehicleStatDecorationAuxInput? AuxInput2 { get; init; }

    /// <summary>Auxiliary input 3 as of this sample.</summary>
    [JsonPropertyName("auxInput3")] public VehicleStatDecorationAuxInput? AuxInput3 { get; init; }

    /// <summary>Auxiliary input 4 as of this sample.</summary>
    [JsonPropertyName("auxInput4")] public VehicleStatDecorationAuxInput? AuxInput4 { get; init; }

    /// <summary>Auxiliary input 5 as of this sample.</summary>
    [JsonPropertyName("auxInput5")] public VehicleStatDecorationAuxInput? AuxInput5 { get; init; }

    /// <summary>Auxiliary input 6 as of this sample.</summary>
    [JsonPropertyName("auxInput6")] public VehicleStatDecorationAuxInput? AuxInput6 { get; init; }

    /// <summary>Auxiliary input 7 as of this sample.</summary>
    [JsonPropertyName("auxInput7")] public VehicleStatDecorationAuxInput? AuxInput7 { get; init; }

    /// <summary>Auxiliary input 8 as of this sample.</summary>
    [JsonPropertyName("auxInput8")] public VehicleStatDecorationAuxInput? AuxInput8 { get; init; }

    /// <summary>Auxiliary input 9 as of this sample.</summary>
    [JsonPropertyName("auxInput9")] public VehicleStatDecorationAuxInput? AuxInput9 { get; init; }

    /// <summary>Auxiliary input 10 as of this sample.</summary>
    [JsonPropertyName("auxInput10")] public VehicleStatDecorationAuxInput? AuxInput10 { get; init; }

    /// <summary>Auxiliary input 11 as of this sample.</summary>
    [JsonPropertyName("auxInput11")] public VehicleStatDecorationAuxInput? AuxInput11 { get; init; }

    /// <summary>Auxiliary input 12 as of this sample.</summary>
    [JsonPropertyName("auxInput12")] public VehicleStatDecorationAuxInput? AuxInput12 { get; init; }

    /// <summary>Auxiliary input 13 as of this sample.</summary>
    [JsonPropertyName("auxInput13")] public VehicleStatDecorationAuxInput? AuxInput13 { get; init; }

    /// <summary>Barometric pressure, in pascals.</summary>
    [JsonPropertyName("barometricPressurePa")] public VehicleStatDecorationValue? BarometricPressurePa { get; init; }

    /// <summary>Battery voltage, in millivolts.</summary>
    [JsonPropertyName("batteryMilliVolts")] public VehicleStatDecorationValue? BatteryMilliVolts { get; init; }

    /// <summary>Diesel exhaust fluid level, in milli-percent.</summary>
    [JsonPropertyName("defLevelMilliPercent")] public VehicleStatDecorationValue? DefLevelMilliPercent { get; init; }

    /// <summary>Door status as read from the ECU. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("ecuDoorStatus")] public VehicleStatStringValue? EcuDoorStatus { get; init; }

    /// <summary>ECU-reported road speed, in miles per hour.</summary>
    [JsonPropertyName("ecuSpeedMph")] public VehicleStatDecorationDoubleValue? EcuSpeedMph { get; init; }

    /// <summary>Engine coolant temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("engineCoolantTemperatureMilliC")] public VehicleStatDecorationValue? EngineCoolantTemperatureMilliC { get; init; }

    /// <summary>Engine immobilizer state. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("engineImmobilizer")] public VehicleStatEngineImmobilizer? EngineImmobilizer { get; init; }

    /// <summary>Engine load, as a percentage.</summary>
    [JsonPropertyName("engineLoadPercent")] public VehicleStatDecorationValue? EngineLoadPercent { get; init; }

    /// <summary>Engine oil pressure, in kilopascals.</summary>
    [JsonPropertyName("engineOilPressureKPa")] public VehicleStatDecorationValue? EngineOilPressureKPa { get; init; }

    /// <summary>Engine RPM.</summary>
    [JsonPropertyName("engineRpm")] public VehicleStatDecorationValue? EngineRpm { get; init; }

    /// <summary>Engine on/off state (<c>Off</c>, <c>On</c>, or <c>Idle</c>).</summary>
    [JsonPropertyName("engineStates")] public VehicleStatDecorationStringValue? EngineStates { get; init; }

    /// <summary>Average EV battery temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("evAverageBatteryTemperatureMilliCelsius")] public VehicleStatValue? EvAverageBatteryTemperatureMilliCelsius { get; init; }

    /// <summary>EV battery current, in milliamps.</summary>
    [JsonPropertyName("evBatteryCurrentMilliAmp")] public VehicleStatValue? EvBatteryCurrentMilliAmp { get; init; }

    /// <summary>EV battery state of health, in milli-percent.</summary>
    [JsonPropertyName("evBatteryStateOfHealthMilliPercent")] public VehicleStatValue? EvBatteryStateOfHealthMilliPercent { get; init; }

    /// <summary>EV battery voltage, in millivolts.</summary>
    [JsonPropertyName("evBatteryVoltageMilliVolt")] public VehicleStatValue? EvBatteryVoltageMilliVolt { get; init; }

    /// <summary>EV charging current, in milliamps.</summary>
    [JsonPropertyName("evChargingCurrentMilliAmp")] public VehicleStatValue? EvChargingCurrentMilliAmp { get; init; }

    /// <summary>EV charging energy, in micro-watt-hours.</summary>
    [JsonPropertyName("evChargingEnergyMicroWh")] public VehicleStatValue? EvChargingEnergyMicroWh { get; init; }

    /// <summary>EV charging status code.</summary>
    [JsonPropertyName("evChargingStatus")] public VehicleStatValue? EvChargingStatus { get; init; }

    /// <summary>EV charging voltage, in millivolts.</summary>
    [JsonPropertyName("evChargingVoltageMilliVolt")] public VehicleStatValue? EvChargingVoltageMilliVolt { get; init; }

    /// <summary>EV energy consumed, in micro-watt-hours.</summary>
    [JsonPropertyName("evConsumedEnergyMicroWh")] public VehicleStatValue? EvConsumedEnergyMicroWh { get; init; }

    /// <summary>EV distance driven, in meters.</summary>
    [JsonPropertyName("evDistanceDrivenMeters")] public VehicleStatValue? EvDistanceDrivenMeters { get; init; }

    /// <summary>EV energy regenerated, in micro-watt-hours.</summary>
    [JsonPropertyName("evRegeneratedEnergyMicroWh")] public VehicleStatValue? EvRegeneratedEnergyMicroWh { get; init; }

    /// <summary>EV state of charge, in milli-percent.</summary>
    [JsonPropertyName("evStateOfChargeMilliPercent")] public VehicleStatValue? EvStateOfChargeMilliPercent { get; init; }

    /// <summary>Engine fault codes. Has no <c>time</c> of its own.</summary>
    [JsonPropertyName("faultCodes")] public VehicleStatDecorationFaultCodes? FaultCodes { get; init; }

    /// <summary>Fuel consumed, in milliliters.</summary>
    [JsonPropertyName("fuelConsumedMilliliters")] public VehicleStatDecorationValue? FuelConsumedMilliliters { get; init; }

    /// <summary>Fuel level, as a percentage.</summary>
    [JsonPropertyName("fuelPercents")] public VehicleStatDecorationValue? FuelPercents { get; init; }

    /// <summary>GPS reading. Has no <c>time</c> of its own.</summary>
    [JsonPropertyName("gps")] public VehicleStatDecorationGps? Gps { get; init; }

    /// <summary>GPS-measured trip distance, in meters.</summary>
    [JsonPropertyName("gpsDistanceMeters")] public VehicleStatDecorationDoubleValue? GpsDistanceMeters { get; init; }

    /// <summary>GPS-derived odometer, in meters.</summary>
    [JsonPropertyName("gpsOdometerMeters")] public VehicleStatDecorationValue? GpsOdometerMeters { get; init; }

    /// <summary>Idling duration, in milliseconds.</summary>
    [JsonPropertyName("idlingDurationMilliseconds")] public VehicleStatDecorationValue? IdlingDurationMilliseconds { get; init; }

    /// <summary>Intake manifold temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("intakeManifoldTemperatureMilliC")] public VehicleStatDecorationValue? IntakeManifoldTemperatureMilliC { get; init; }

    /// <summary>OBD-reported engine seconds.</summary>
    [JsonPropertyName("obdEngineSeconds")] public VehicleStatDecorationValue? ObdEngineSeconds { get; init; }

    /// <summary>OBD-reported odometer, in meters.</summary>
    [JsonPropertyName("obdOdometerMeters")] public VehicleStatDecorationValue? ObdOdometerMeters { get; init; }

    /// <summary>Driver seatbelt state. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("seatbeltDriver")] public VehicleStatStringValue? SeatbeltDriver { get; init; }

    /// <summary>Whether the spreader is active. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderActive")] public VehicleStatStringValue? SpreaderActive { get; init; }

    /// <summary>Spreader air temperature. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderAirTemp")] public VehicleStatValue? SpreaderAirTemp { get; init; }

    /// <summary>Spreader blast state. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderBlastState")] public VehicleStatStringValue? SpreaderBlastState { get; init; }

    /// <summary>Spreader granular material name. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderGranularName")] public VehicleStatStringValue? SpreaderGranularName { get; init; }

    /// <summary>Spreader granular application rate. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderGranularRate")] public VehicleStatValue? SpreaderGranularRate { get; init; }

    /// <summary>Spreader liquid material name. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderLiquidName")] public VehicleStatStringValue? SpreaderLiquidName { get; init; }

    /// <summary>Spreader liquid application rate. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderLiquidRate")] public VehicleStatValue? SpreaderLiquidRate { get; init; }

    /// <summary>Spreader on state. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderOnState")] public VehicleStatStringValue? SpreaderOnState { get; init; }

    /// <summary>Spreader plow status. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderPlowStatus")] public VehicleStatStringValue? SpreaderPlowStatus { get; init; }

    /// <summary>Spreader pre-wet material name. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderPrewetName")] public VehicleStatStringValue? SpreaderPrewetName { get; init; }

    /// <summary>Spreader pre-wet application rate. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderPrewetRate")] public VehicleStatValue? SpreaderPrewetRate { get; init; }

    /// <summary>Spreader road temperature. Carries its own <c>time</c>.</summary>
    [JsonPropertyName("spreaderRoadTemp")] public VehicleStatValue? SpreaderRoadTemp { get; init; }

    /// <summary>Tire pressures, in kilopascals.</summary>
    [JsonPropertyName("tirePressure")] public VehicleStatTirePressure? TirePressure { get; init; }
}

/// <summary>
/// An integer-valued decoration entry (<c>{ value }</c>) — the decorated
/// sample's own <c>time</c> applies, so the spec gives these no timestamp.
/// </summary>
public sealed record VehicleStatDecorationValue
{
    /// <summary>The measured value. Spec marks REQUIRED; nullable because this is a response record.</summary>
    [JsonPropertyName("value")] public long? Value { get; init; }
}

/// <summary>A floating-point decoration entry (<c>{ value }</c>).</summary>
public sealed record VehicleStatDecorationDoubleValue
{
    /// <summary>The measured value. Spec marks REQUIRED; nullable because this is a response record.</summary>
    [JsonPropertyName("value")] public double? Value { get; init; }
}

/// <summary>A string- or enum-valued decoration entry (<c>{ value }</c>).</summary>
public sealed record VehicleStatDecorationStringValue
{
    /// <summary>The measured value. Spec marks REQUIRED; nullable because this is a response record.</summary>
    [JsonPropertyName("value")] public string? Value { get; init; }
}

/// <summary>
/// An auxiliary-input decoration entry. Mirrors the spec's
/// <c>VehicleStatsAuxInputDecoration</c> (<c>{ name, value }</c> — no time).
/// </summary>
public sealed record VehicleStatDecorationAuxInput
{
    /// <summary>Human-readable name configured for this auxiliary input. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }

    /// <summary>Whether the auxiliary input is active. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")] public bool? Value { get; init; }
}

/// <summary>
/// A GPS decoration entry. Mirrors the spec's <c>VehicleStatsDecorations_gps</c>,
/// which is <see cref="VehicleStatGps"/> without the <c>time</c>.
/// </summary>
public sealed record VehicleStatDecorationGps
{
    /// <summary>Latitude in degrees. Spec marks REQUIRED; nullable because this is a response record.</summary>
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }

    /// <summary>Longitude in degrees. Spec marks REQUIRED; nullable because this is a response record.</summary>
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }

    /// <summary>Heading in degrees from true north.</summary>
    [JsonPropertyName("headingDegrees")] public double? HeadingDegrees { get; init; }

    /// <summary>Speed in miles per hour.</summary>
    [JsonPropertyName("speedMilesPerHour")] public double? SpeedMilesPerHour { get; init; }

    /// <summary>Whether the reported speed is sourced from the ECU rather than GPS.</summary>
    [JsonPropertyName("isEcuSpeed")] public bool? IsEcuSpeed { get; init; }

    /// <summary>The nearest known address (place) to the reading.</summary>
    [JsonPropertyName("address")] public VehicleStatAddress? Address { get; init; }

    /// <summary>Reverse-geocoded address for the reading.</summary>
    [JsonPropertyName("reverseGeo")] public ReverseGeo? ReverseGeo { get; init; }
}

/// <summary>
/// A fault-code decoration entry. Mirrors the spec's
/// <c>VehicleStatsFaultCodesValue</c>, which is <see cref="VehicleStatFaultCodes"/>
/// without the <c>time</c>.
/// </summary>
public sealed record VehicleStatDecorationFaultCodes
{
    /// <summary>The CAN bus protocol the fault codes were read from.</summary>
    [JsonPropertyName("canBusType")] public string? CanBusType { get; init; }

    /// <summary>J1939 (heavy-duty) fault codes.</summary>
    [JsonPropertyName("j1939")] public VehicleStatFaultCodesJ1939? J1939 { get; init; }

    /// <summary>OBD-II (light-duty) fault codes.</summary>
    [JsonPropertyName("obdii")] public VehicleStatFaultCodesObdii? Obdii { get; init; }

    /// <summary>OEM-specific fault codes.</summary>
    [JsonPropertyName("oem")] public VehicleStatFaultCodesOem? Oem { get; init; }
}

/// <summary>
/// Tire pressures, in kilopascals. Mirrors the spec's
/// <c>VehicleStatsTirePressures</c>.
/// </summary>
public sealed record VehicleStatTirePressure
{
    /// <summary>Back-left tire pressure, in kilopascals.</summary>
    [JsonPropertyName("backLeftTirePressureKPa")] public long? BackLeftTirePressureKPa { get; init; }

    /// <summary>Back-right tire pressure, in kilopascals.</summary>
    [JsonPropertyName("backRightTirePressureKPa")] public long? BackRightTirePressureKPa { get; init; }

    /// <summary>Front-left tire pressure, in kilopascals.</summary>
    [JsonPropertyName("frontLeftTirePressureKPa")] public long? FrontLeftTirePressureKPa { get; init; }

    /// <summary>Front-right tire pressure, in kilopascals.</summary>
    [JsonPropertyName("frontRightTirePressureKPa")] public long? FrontRightTirePressureKPa { get; init; }
}

/// <summary>
/// Vendor-specific J1939 fault-code data. Mirrors the spec's
/// <c>VehicleStatsFaultCodesVendorSpecificFields</c>.
/// </summary>
public sealed record VehicleStatJ1939VendorSpecificFields
{
    /// <summary>The DTC description, if available.</summary>
    [JsonPropertyName("dtcDescription")] public string? DtcDescription { get; init; }

    /// <summary>A link to vendor repair instructions, if available.</summary>
    [JsonPropertyName("repairInstructionsUrl")] public string? RepairInstructionsUrl { get; init; }
}

/// <summary>
/// OBD-II engine-sensor monitor readiness. Mirrors the spec's
/// <c>VehicleStatsFaultCodesPassengerMonitorStatus</c>. Each reading is
/// <c>U</c> (unsupported), <c>N</c> (not complete) or <c>R</c> (complete), kept
/// as a string to stay forward-compatible with new enum members.
/// </summary>
public sealed record VehicleStatObdiiMonitorStatus
{
    /// <summary>Catalyst monitor readiness.</summary>
    [JsonPropertyName("catalyst")] public string? Catalyst { get; init; }

    /// <summary>Comprehensive-component monitor readiness.</summary>
    [JsonPropertyName("comprehensive")] public string? Comprehensive { get; init; }

    /// <summary>EGR system monitor readiness.</summary>
    [JsonPropertyName("egr")] public string? Egr { get; init; }

    /// <summary>Evaporative system monitor readiness.</summary>
    [JsonPropertyName("evapSystem")] public string? EvapSystem { get; init; }

    /// <summary>Fuel system monitor readiness.</summary>
    [JsonPropertyName("fuel")] public string? Fuel { get; init; }

    /// <summary>Heated-catalyst monitor readiness.</summary>
    [JsonPropertyName("heatedCatalyst")] public string? HeatedCatalyst { get; init; }

    /// <summary>Heated-O2-sensor monitor readiness.</summary>
    [JsonPropertyName("heatedO2Sensor")] public string? HeatedO2Sensor { get; init; }

    /// <summary>ISO/SAE-reserved monitor readiness.</summary>
    [JsonPropertyName("isoSaeReserved")] public string? IsoSaeReserved { get; init; }

    /// <summary>Misfire monitor readiness.</summary>
    [JsonPropertyName("misfire")] public string? Misfire { get; init; }

    /// <summary>Count of sensors reporting <c>N</c> (not complete).</summary>
    [JsonPropertyName("notReadyCount")] public int? NotReadyCount { get; init; }

    /// <summary>O2-sensor monitor readiness.</summary>
    [JsonPropertyName("o2Sensor")] public string? O2Sensor { get; init; }

    /// <summary>Secondary-air monitor readiness.</summary>
    [JsonPropertyName("secondaryAir")] public string? SecondaryAir { get; init; }
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

    /// <summary>Custom attributes to set on the equipment (spec schema <c>GoaAttributeTiny</c>).</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AttributeTiny>? Attributes { get; init; }
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

    /// <summary>Custom attributes on the equipment (spec schema
    /// <c>GoaAttributeTinyResponseBody</c>). Returned by the beta
    /// <c>PATCH /beta/fleet/equipment/{id}</c> response.</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AttributeTiny>? Attributes { get; init; }
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
    [JsonPropertyName("intervals")] public required IReadOnlyList<SpeedingIntervalDetail> Intervals { get; init; }

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
/// A single speeding interval within a trip. Mirrors the spec's
/// <c>SpeedingIntervalResponseBody</c> schema — the item type of
/// <see cref="SpeedingInterval.Intervals"/>.
/// </summary>
/// <remarks>
/// Named <c>SpeedingIntervalDetail</c> rather than the stripped spec name
/// <c>SpeedingInterval</c>, which is already taken by the per-trip wrapper
/// record this type is nested inside.
/// </remarks>
public sealed record SpeedingIntervalDetail
{
    /// <summary>UTC time the interval started, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")] public DateTimeOffset? StartTime { get; init; }

    /// <summary>UTC time the interval ended, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")] public DateTimeOffset? EndTime { get; init; }

    /// <summary>Whether the interval has been dismissed. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isDismissed")] public bool? IsDismissed { get; init; }

    /// <summary>Location of the closest location point to the interval. Spec marks REQUIRED.</summary>
    [JsonPropertyName("location")] public SpeedingIntervalLocation? Location { get; init; }

    /// <summary>The max speed exceeded during the interval, in km/h. Spec marks REQUIRED.</summary>
    [JsonPropertyName("maxSpeedKilometersPerHour")] public double? MaxSpeedKilometersPerHour { get; init; }

    /// <summary>The posted speed limit for the interval, in km/h. Spec marks REQUIRED.</summary>
    [JsonPropertyName("postedSpeedLimitKilometersPerHour")] public double? PostedSpeedLimitKilometersPerHour { get; init; }

    /// <summary>
    /// Severity level of the interval (<c>light</c>, <c>moderate</c>, <c>heavy</c>
    /// or <c>severe</c>). Exposed as a string to stay forward-compatible with new
    /// enum members. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("severityLevel")] public string? SeverityLevel { get; init; }
}

/// <summary>
/// Location of the closest location point to a <see cref="SpeedingIntervalDetail"/>.
/// Mirrors the spec's <c>SpeedingIntervalLocationResponseResponseBody</c> schema.
/// </summary>
/// <remarks>
/// The nested address reuses <see cref="AssetLocationAddress"/>: both properties
/// resolve to the same spec schema, <c>AddressResponseResponseBody</c>.
/// </remarks>
public sealed record SpeedingIntervalLocation
{
    /// <summary>Latitude of the closest location point to the interval. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")] public double? Latitude { get; init; }

    /// <summary>Longitude of the closest location point to the interval. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")] public double? Longitude { get; init; }

    /// <summary>
    /// Heading of the asset in degrees; may be 0 when the asset is not moving.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("headingDegrees")] public long? HeadingDegrees { get; init; }

    /// <summary>
    /// Radial accuracy of the GPS location in meters. Only returned when strong
    /// GPS is not available.
    /// </summary>
    [JsonPropertyName("accuracyMeters")] public double? AccuracyMeters { get; init; }

    /// <summary>Closest address to the interval location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("address")] public AssetLocationAddress? Address { get; init; }
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

// ---------------------------------------------------------------------------
// Engine immobilizer (beta) — GET /fleet/vehicles/immobilizer/stream and
// PATCH /beta/fleet/vehicles/{id}/immobilizer.
// ---------------------------------------------------------------------------

/// <summary>
/// An engine immobilizer state reported for a vehicle. Mirrors the spec's
/// <c>EngineImmobilizerStateResponseBody</c> schema, the item type of
/// <c>GET /fleet/vehicles/immobilizer/stream</c>.
/// </summary>
/// <remarks>
/// Distinct from <see cref="VehicleStatEngineImmobilizer"/>, which is the
/// immobilizer sample embedded in the vehicle-stats payload.
/// </remarks>
public sealed record EngineImmobilizerState
{
    /// <summary>The ID of the vehicle the engine immobilizer is connected to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }

    /// <summary>
    /// UTC time in RFC 3339 format at which the state was reported. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("happenedAtTime")] public string? HappenedAtTime { get; init; }

    /// <summary>Whether the engine immobilizer is connected to the vehicle. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isConnectedToVehicle")] public bool? IsConnectedToVehicle { get; init; }

    /// <summary>The state of each relay. Spec marks REQUIRED.</summary>
    [JsonPropertyName("relayStates")] public IReadOnlyList<EngineImmobilizerRelayState>? RelayStates { get; init; }
}

/// <summary>
/// The state of a single engine-immobilizer relay. Mirrors the spec's
/// <c>EngineImmobilizerRelayStateResponseBody</c> schema.
/// </summary>
/// <remarks>
/// The request half is <see cref="EngineImmobilizerRelayStateInput"/>: the two
/// spec schemas carry the same members, but they stay split so
/// <c>required</c> appears only on the request DTO.
/// </remarks>
public sealed record EngineImmobilizerRelayState
{
    /// <summary>
    /// The ID of the relay. Valid values: <c>relay1</c>, <c>relay2</c>. Spec
    /// marks REQUIRED.
    /// </summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Whether the relay is open. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isOpen")] public bool? IsOpen { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /beta/fleet/vehicles/{id}/immobilizer</c>. Mirrors
/// the spec's <c>EngineImmobilizerUpdateEngineImmobilizerStateRequestBody</c>
/// schema.
/// </summary>
public sealed record UpdateEngineImmobilizerStateRequest
{
    /// <summary>
    /// The relay states to apply. A relay omitted from the list is left
    /// unchanged; an empty list is rejected with a 400. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("relayStates")] public required IReadOnlyList<EngineImmobilizerRelayStateInput> RelayStates { get; init; }
}

/// <summary>
/// A relay state to apply. Mirrors the spec's
/// <c>UpdateEngineImmobilizerRelayStateRequestBodyRequestBody</c> schema.
/// </summary>
public sealed record EngineImmobilizerRelayStateInput
{
    /// <summary>
    /// The ID of the relay. Valid values: <c>relay1</c>, <c>relay2</c>. Spec
    /// marks REQUIRED.
    /// </summary>
    [JsonPropertyName("id")] public required string Id { get; init; }

    /// <summary>
    /// The desired state of the relay: <c>true</c> to open it, <c>false</c> to
    /// close it. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("isOpen")] public required bool IsOpen { get; init; }
}

// ---------------------------------------------------------------------------
// Gateway pairing (beta) — POST /gateways/pair.
// ---------------------------------------------------------------------------

/// <summary>
/// Request body for <c>POST /gateways/pair</c>. Mirrors the spec's
/// <c>GatewaysPairGatewaysRequestBody</c> schema.
/// </summary>
public sealed record PairGatewaysRequest
{
    /// <summary>The gateway-to-device pairings to apply. Spec marks REQUIRED.</summary>
    [JsonPropertyName("pairs")] public required IReadOnlyList<GatewayPairInput> Pairs { get; init; }

    /// <summary>
    /// When <c>true</c>, devices the reassigned gateways were previously linked
    /// to are moved to the unassigned pool.
    /// </summary>
    [JsonPropertyName("removeOrphanDevices")] public bool? RemoveOrphanDevices { get; init; }
}

/// <summary>
/// A single gateway-to-device pairing instruction. Mirrors the spec's
/// <c>PairGatewayPairObjectRequestBody</c> schema.
/// </summary>
public sealed record GatewayPairInput
{
    /// <summary>
    /// The serial of the gateway to reassign. The gateway must already be
    /// activated in the organization. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("gatewaySerial")] public required string GatewaySerial { get; init; }

    /// <summary>
    /// The serial of the target device to pair the gateway with, in the
    /// standard Samsara serial format. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("deviceSerial")] public required string DeviceSerial { get; init; }
}

/// <summary>
/// The outcome of a single gateway-to-device pairing. Mirrors the spec's
/// <c>PairGatewayResultObjectResponseBody</c> schema.
/// </summary>
public sealed record GatewayPairResult
{
    /// <summary>The gateway that was paired. Spec marks REQUIRED.</summary>
    [JsonPropertyName("gateway")] public GatewayPairGateway? Gateway { get; init; }

    /// <summary>The device the gateway is now paired with. Spec marks REQUIRED.</summary>
    [JsonPropertyName("device")] public GatewayPairDevice? Device { get; init; }

    /// <summary>The device the gateway was previously linked to, when it was displaced.</summary>
    [JsonPropertyName("previousDevice")] public GatewayPairDevice? PreviousDevice { get; init; }

    /// <summary>The gateway that was displaced from the target device, when there was one.</summary>
    [JsonPropertyName("displacedGateway")] public GatewayPairGateway? DisplacedGateway { get; init; }
}

/// <summary>
/// Identifying information for a device involved in a pairing operation.
/// Mirrors the spec's <c>PairResultDeviceObjectResponseBody</c> schema.
/// </summary>
public sealed record GatewayPairDevice
{
    /// <summary>The unique Samsara ID of the device. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>The name of the device.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }

    /// <summary>The serial number of the device. Spec marks REQUIRED.</summary>
    [JsonPropertyName("serial")] public string? Serial { get; init; }

    /// <summary>
    /// The type of the device. Valid values: <c>vehicle</c>, <c>asset</c>,
    /// <c>equipment</c>, <c>trailer</c>, <c>industrial</c>, <c>assetTag</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("type")] public string? Type { get; init; }
}

/// <summary>
/// Identifying information for a gateway involved in a pairing operation.
/// Mirrors the spec's <c>PairResultGatewayObjectResponseBody</c> schema.
/// </summary>
public sealed record GatewayPairGateway
{
    /// <summary>The unique Samsara ID of the gateway. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>
    /// The model of the gateway (e.g. <c>VG34</c>, <c>AG46</c>). Spec marks
    /// REQUIRED and enumerates 47 values; modelled as a string so a newly
    /// released model does not break deserialization.
    /// </summary>
    [JsonPropertyName("model")] public string? Model { get; init; }

    /// <summary>The serial number of the gateway. Spec marks REQUIRED.</summary>
    [JsonPropertyName("serial")] public string? Serial { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /fleet/equipment/{id}/digital-output</c>
/// (<c>setEquipmentDigitalOutput</c>, beta). Mirrors the spec's
/// <c>EquipmentOutputControlSetEquipmentDigitalOutputRequestBody</c>.
/// </summary>
public sealed record SetEquipmentDigitalOutputRequest
{
    /// <summary>
    /// The digital output pin to control. Only pin <c>1</c> is currently
    /// supported. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("pinId")] public required int PinId { get; init; }

    /// <summary>
    /// The desired output state — <c>true</c> to energize the output,
    /// <c>false</c> to de-energize it. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("state")] public required bool State { get; init; }

    /// <summary>
    /// How long, in seconds, to hold the requested state before the device
    /// automatically reverts it. Omit (or <c>0</c>) to hold indefinitely.
    /// </summary>
    [JsonPropertyName("durationSeconds")] public int? DurationSeconds { get; init; }
}

/// <summary>
/// The applied digital-output state returned by
/// <c>PATCH /fleet/equipment/{id}/digital-output</c> (beta). Mirrors the spec's
/// <c>SetEquipmentDigitalOutputDataResponseBody</c>.
/// </summary>
public sealed record EquipmentDigitalOutputState
{
    /// <summary>The Samsara ID of the gateway whose digital output was controlled. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")] public long? Id { get; init; }

    /// <summary>The digital output pin that was controlled. Spec marks REQUIRED.</summary>
    [JsonPropertyName("pinId")] public int? PinId { get; init; }

    /// <summary>The output state that was applied. Spec marks REQUIRED.</summary>
    [JsonPropertyName("state")] public bool? State { get; init; }

    /// <summary>
    /// The duration, in seconds, the state will be held. <c>0</c> means
    /// indefinitely. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("durationSeconds")] public int? DurationSeconds { get; init; }
}

/// <summary>
/// The page envelope returned by the legacy <c>GET /v1/fleet/locations</c>
/// endpoint. Mirrors the spec's <c>FleetLocationsGetFleetLocationsResponseBody</c>.
/// </summary>
/// <remarks>
/// <para>
/// This v1 endpoint does <b>not</b> use the v2 <c>{ data: [...], pagination: {...} }</c>
/// envelope: its page items sit in a <b>top-level</b> <c>vehicles</c> array beside a
/// top-level <c>pagination</c> block. Deserializing it with the standard
/// <c>SamsaraListResponse&lt;T&gt;</c> would silently find no <c>data</c> member, which
/// is why <c>VehiclesClient.V1GetFleetLocationsAsync</c> paginates through this record
/// instead. Consumers normally never see it — the client surfaces
/// <see cref="V1VehicleLocation"/> items directly.
/// </para>
/// <para>Spec marks both members REQUIRED; they stay nullable because this is a response record.</para>
/// </remarks>
public sealed record V1FleetLocationsResponse
{
    /// <summary>List of vehicle locations on this page. Spec marks REQUIRED.</summary>
    [JsonPropertyName("vehicles")]
    public IReadOnlyList<V1VehicleLocation>? Vehicles { get; init; }

    /// <summary>
    /// Cursor pagination for the page (spec schema <c>FleetLocationsPaginationResponseBody</c>,
    /// property-identical to every other Samsara pagination block). Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("pagination")]
    public Samsara.Sdk.Pagination.PaginationInfo? Pagination { get; init; }
}

/// <summary>
/// The current location of a vehicle as returned by the legacy
/// <c>GET /v1/fleet/locations</c> endpoint. Mirrors the spec's
/// <c>VehicleLocationResponseBody</c>.
/// </summary>
/// <remarks>
/// Distinct from the v2 <see cref="VehicleLocation"/> returned by
/// <c>/fleet/vehicles/locations</c>: this v1 shape types every id as an
/// <c>integer/int64</c>, flattens latitude/longitude/heading/speed onto the vehicle
/// itself rather than nesting them under a <c>location</c> object, and reports its
/// timestamp as Unix milliseconds. Spec marks only <c>id</c> required; it stays
/// nullable because this is a response record.
/// </remarks>
public sealed record V1VehicleLocation
{
    /// <summary>ID of the vehicle. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Vehicle Identification Number (VIN) of the vehicle.</summary>
    [JsonPropertyName("vin")]
    public string? Vin { get; init; }

    /// <summary>The ID of the driver currently assigned to this vehicle.</summary>
    [JsonPropertyName("driverId")]
    public long? DriverId { get; init; }

    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Text representation of the nearest identifiable location to the coordinates.</summary>
    [JsonPropertyName("location")]
    public string? Location { get; init; }

    /// <summary>Heading in degrees.</summary>
    [JsonPropertyName("heading")]
    public double? Heading { get; init; }

    /// <summary>Speed in miles per hour.</summary>
    [JsonPropertyName("speed")]
    public double? Speed { get; init; }

    /// <summary>The number of meters reported by the odometer.</summary>
    [JsonPropertyName("odometerMeters")]
    public long? OdometerMeters { get; init; }

    /// <summary>The source of <see cref="OdometerMeters"/> — <c>GPS</c> or <c>OBD</c>.</summary>
    [JsonPropertyName("odometerType")]
    public string? OdometerType { get; init; }

    /// <summary>Whether a trip is currently in progress for this vehicle.</summary>
    [JsonPropertyName("onTrip")]
    public bool? OnTrip { get; init; }

    /// <summary>Currently active route IDs the vehicle is in.</summary>
    [JsonPropertyName("routeIds")]
    public IReadOnlyList<long>? RouteIds { get; init; }

    /// <summary>The time the reported location was logged, as a Unix timestamp in milliseconds.</summary>
    [JsonPropertyName("time")]
    public long? Time { get; init; }
}
