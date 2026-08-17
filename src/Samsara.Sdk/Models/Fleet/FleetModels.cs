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
}

/// <summary>
/// A single auxiliary-input vehicle statistic sample. Carries a boolean state
/// and an optional human-readable input name.
/// </summary>
public sealed record VehicleStatAuxInput
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>Whether the auxiliary input is active. Spec-required.</summary>
    [JsonPropertyName("value")] public required bool Value { get; init; }

    /// <summary>Human-readable name configured for this auxiliary input.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }
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
