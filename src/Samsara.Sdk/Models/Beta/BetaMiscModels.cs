namespace Samsara.Sdk.Models.Beta;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Job object to be passed back.
/// Returned by <c>GET /beta/industrial/jobs</c>, <c>POST /beta/industrial/jobs</c> and <c>PATCH
/// /beta/industrial/jobs</c> (beta).
/// Mirrors the spec schema <c>JobResponseObjectResponseBody</c>.
/// </summary>
public sealed record IndustrialJob
{
    /// <summary>jobLocation object. Spec marks this required on the response.</summary>
    [JsonPropertyName("address")]
    public IndustrialJobLocation? Address { get; init; }

    /// <summary>When the job was created. Spec marks this required on the response.</summary>
    [JsonPropertyName("createdAt")]
    public string? CreatedAt { get; init; }

    /// <summary>Customer name for job. Spec marks this required on the response.</summary>
    [JsonPropertyName("customerName")]
    public string? CustomerName { get; init; }

    /// <summary>End date of job in RFC 3339 format. Spec marks this required on the response.</summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; init; }

    /// <summary>
    /// fleet devices in this job (cannot have both industrial assets and fleet devices in
    /// the same job).
    /// </summary>
    [JsonPropertyName("fleetDevices")]
    public IReadOnlyList<IndustrialJobFleetDevice>? FleetDevices { get; init; }

    /// <summary>Job id. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Industrial Assets in this job (cannot have both industrial assets and fleet devices
    /// in the same job).
    /// </summary>
    [JsonPropertyName("industrialAssets")]
    public IReadOnlyList<IndustrialJobAsset>? IndustrialAssets { get; init; }

    /// <summary>When the job was last modified. Spec marks this required on the response.</summary>
    [JsonPropertyName("modifiedAt")]
    public string? ModifiedAt { get; init; }

    /// <summary>Job name. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Notes for the upcoming job. Spec marks this required on the response.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>
    /// Specifies the time window (in milliseconds) after a stop's scheduled arrival time
    /// during which the stop is considered 'on-time'.
    /// </summary>
    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    /// <summary>
    /// Specifies the time window (in milliseconds) before a stop's scheduled arrival time
    /// during which the stop is considered 'on-time'.
    /// </summary>
    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }

    /// <summary>Start date of job in RFC 3339 format. Spec marks this required on the response.</summary>
    [JsonPropertyName("startDate")]
    public string? StartDate { get; init; }

    /// <summary>
    /// The current job status. One of: <c>active</c>, <c>scheduled</c>, <c>completed</c>.
    /// Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Samsara uuid. Spec marks this required on the response.</summary>
    [JsonPropertyName("uuid")]
    public string? Uuid { get; init; }
}

/// <summary>
/// jobLocation object.
/// Mirrors the spec schema <c>jobLocationResponseObjectResponseBody</c>.
/// </summary>
public sealed record IndustrialJobLocation
{
    /// <summary>Address of a location. Spec marks this required on the response.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>Latitude of a location. Spec marks this required on the response.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude of a location. Spec marks this required on the response.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Name of a location. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// fleetDeviceObject.
/// Mirrors the spec schema <c>fleetDeviceObjectResponseBody</c>.
/// </summary>
public sealed record IndustrialJobFleetDevice
{
    /// <summary>Id of the device. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Name of the device. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// industrialAssetObject.
/// Mirrors the spec schema <c>industrialAssetObjectResponseBody</c>.
/// </summary>
public sealed record IndustrialJobAsset
{
    /// <summary>Id of the device. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the industrial asset. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Job object to be created.
/// Request body for <c>POST /beta/industrial/jobs</c> (operationId <c>createJob</c>, beta).
/// Mirrors the spec schema <c>JobsCreateJobRequestBody</c>.
/// </summary>
public sealed record CreateIndustrialJobRequest
{
    /// <summary>Job object to be created. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("job")]
    public required CreateIndustrialJobInput Job { get; init; }
}

/// <summary>
/// Job object to be created.
/// Mirrors the spec schema <c>PostJobObjectRequestBody</c>.
/// </summary>
public sealed record CreateIndustrialJobInput
{
    /// <summary>A location object for the job.</summary>
    [JsonPropertyName("address")]
    public IndustrialJobLocationInput? Address { get; init; }

    /// <summary>Customer name for job.</summary>
    [JsonPropertyName("customerName")]
    public string? CustomerName { get; init; }

    /// <summary>
    /// End date of job in RFC 3339 format. Must be greater than or equal to the start date.
    /// Spec marks this REQUIRED.
    /// </summary>
    [JsonPropertyName("endDate")]
    public required string EndDate { get; init; }

    /// <summary>
    /// Fleet devices to be added to this job (cannot have both industrial assets and fleet
    /// devices in the same job).
    /// </summary>
    [JsonPropertyName("fleetDeviceIds")]
    public IReadOnlyList<long>? FleetDeviceIds { get; init; }

    /// <summary>Job Id. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// IndustrialAssets to be added to this job (cannot have both industrial assets and
    /// fleet devices in the same job).
    /// </summary>
    [JsonPropertyName("industrialAssetIds")]
    public IReadOnlyList<string>? IndustrialAssetIds { get; init; }

    /// <summary>Job name. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Notes for the upcoming job.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>
    /// Specifies the time window (in milliseconds) after a stop's scheduled arrival time
    /// during which the stop is considered 'on-time'.
    /// </summary>
    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    /// <summary>
    /// Specifies the time window (in milliseconds) before a stop's scheduled arrival time
    /// during which the stop is considered 'on-time'.
    /// </summary>
    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }

    /// <summary>Start date of job in RFC 3339 format. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("startDate")]
    public required string StartDate { get; init; }
}

/// <summary>
/// Job object with fields to update. If a field is not provided, it will not be updated.
/// Request body for <c>PATCH /beta/industrial/jobs</c> (operationId <c>patchJob</c>, beta).
/// Mirrors the spec schema <c>JobsPatchJobRequestBody</c>.
/// </summary>
public sealed record UpdateIndustrialJobRequest
{
    /// <summary>
    /// Job object with fields to update. If a field is not provided, it will not be
    /// updated. Spec marks this REQUIRED.
    /// </summary>
    [JsonPropertyName("job")]
    public required UpdateIndustrialJobInput Job { get; init; }

    /// <summary>
    /// Defaults to true if user does not want to overwrite entire history for an active job
    /// (irrelevant for scheduled/completed jobs).
    /// </summary>
    [JsonPropertyName("keepHistory")]
    public bool? KeepHistory { get; init; }
}

/// <summary>
/// Job object with fields to update. If a field is not provided, it will not be updated.
/// Mirrors the spec schema <c>PatchJobObjectRequestBody</c>.
/// </summary>
public sealed record UpdateIndustrialJobInput
{
    /// <summary>A location object for the job.</summary>
    [JsonPropertyName("address")]
    public IndustrialJobLocationInput? Address { get; init; }

    /// <summary>Customer name for job.</summary>
    [JsonPropertyName("customerName")]
    public string? CustomerName { get; init; }

    /// <summary>End date of job in RFC 3339 format. Must be greater than or equal to the start date.</summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; init; }

    /// <summary>
    /// Fleet devices to be added to this job (cannot have both industrial assets and fleet
    /// devices in the same job).
    /// </summary>
    [JsonPropertyName("fleetDeviceIds")]
    public IReadOnlyList<long>? FleetDeviceIds { get; init; }

    /// <summary>Job Id.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// IndustrialAssets to be added to this job (cannot have both industrial assets and
    /// fleet devices in the same job).
    /// </summary>
    [JsonPropertyName("industrialAssetIds")]
    public IReadOnlyList<string>? IndustrialAssetIds { get; init; }

    /// <summary>Job name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Notes for the upcoming job.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>
    /// Specifies the time window (in milliseconds) after a stop's scheduled arrival time
    /// during which the stop is considered 'on-time'.
    /// </summary>
    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    /// <summary>
    /// Specifies the time window (in milliseconds) before a stop's scheduled arrival time
    /// during which the stop is considered 'on-time'.
    /// </summary>
    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }

    /// <summary>Start date of job in RFC 3339 format.</summary>
    [JsonPropertyName("startDate")]
    public string? StartDate { get; init; }
}

/// <summary>
/// A location object for the job.
/// Mirrors the spec schema <c>PostJobObjectjobLocationRequestObjectRequestBody</c> and its
/// byte-identical PATCH twin <c>PatchJobObjectjobLocationRequestObjectRequestBody</c>.
/// </summary>
public sealed record IndustrialJobLocationInput
{
    /// <summary>Address of a location. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("address")]
    public required string Address { get; init; }

    /// <summary>Latitude of a location. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude of a location. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    /// <summary>Name of the location. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

/// <summary>
/// Information about a device including its identity, last known location, last connected time,
/// and health status.
/// One item of the <c>data</c> array returned by <c>GET /devices</c> (operationId
/// <c>getDevices</c>, beta).
/// Mirrors the spec schema <c>DeviceResponseResponseBody</c>.
/// </summary>
public sealed record BetaDevice
{
    /// <summary>Asset that the device is tied to. Spec marks this required on the response.</summary>
    [JsonPropertyName("asset")]
    public EntityReference? Asset { get; init; }

    /// <summary>Health information for the device.</summary>
    [JsonPropertyName("health")]
    public BetaDeviceHealth? Health { get; init; }

    /// <summary>The last time the device was connected, in RFC 3339 format.</summary>
    [JsonPropertyName("lastConnectedTime")]
    public DateTimeOffset? LastConnectedTime { get; init; }

    /// <summary>The most recent location information for the device.</summary>
    [JsonPropertyName("lastKnownLocation")]
    public BetaDeviceLastKnownLocation? LastKnownLocation { get; init; }

    /// <summary>
    /// The product model name of the device. One of 40 spec-defined values, including
    /// <c>AG24</c>, <c>AG24EU</c>, <c>AG26</c>, <c>AG26EU</c>; see the Samsara API
    /// reference for the full enumeration. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    /// <summary>The serial number of the device. Spec marks this required on the response.</summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-
    /// and-Tag-Nesting) associated with the Device.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }
}

/// <summary>
/// Health information for the device.
/// Mirrors the spec schema <c>HealthResponseResponseBody</c>.
/// </summary>
public sealed record BetaDeviceHealth
{
    /// <summary>Detailed health related metadata for the device.</summary>
    [JsonPropertyName("healthDetails")]
    public BetaDeviceHealthDetails? HealthDetails { get; init; }

    /// <summary>The list of active health reasons affecting this device.</summary>
    [JsonPropertyName("healthReasons")]
    public IReadOnlyList<BetaDeviceHealthReason>? HealthReasons { get; init; }

    /// <summary>
    /// Current overall health status of the device. One of: <c>dataPending</c>,
    /// <c>healthy</c>, <c>needsAttention</c>, <c>needsReplacement</c>.
    /// </summary>
    [JsonPropertyName("healthStatus")]
    public string? HealthStatus { get; init; }

    /// <summary>
    /// Primary health reason affecting the device's current health status. One of 17 spec-
    /// defined values, including <c>assetUnplugged</c>, <c>cameraMisaligned</c>,
    /// <c>dataPending</c>, <c>healthy</c>; see the Samsara API reference for the full
    /// enumeration.
    /// </summary>
    [JsonPropertyName("primaryHealthReason")]
    public string? PrimaryHealthReason { get; init; }

    /// <summary>The timestamp when the primary health reason began, in RFC3339 format.</summary>
    [JsonPropertyName("primaryHealthReasonStartTime")]
    public DateTimeOffset? PrimaryHealthReasonStartTime { get; init; }

    /// <summary>
    /// Recommended steps to resolve the current health reason. One of 42 spec-defined
    /// values, including <c>recommendedActionAgHealthy</c>,
    /// <c>recommendedActionAgLowDeviceBatteryAG45</c>,
    /// <c>recommendedActionAgLowDeviceBatteryAG46</c>,
    /// <c>recommendedActionAgLowDeviceBatteryAG51</c>; see the Samsara API reference for
    /// the full enumeration.
    /// </summary>
    [JsonPropertyName("recommendedAction")]
    public string? RecommendedAction { get; init; }
}

/// <summary>
/// Detailed health related metadata for the device.
/// Mirrors the spec schema <c>HealthDetailsResponseResponseBody</c>.
/// </summary>
public sealed record BetaDeviceHealthDetails
{
    /// <summary>BLE asset tag-specific health metadata details.</summary>
    [JsonPropertyName("bleAssetTagDetails")]
    public BetaBleAssetTagDetails? BleAssetTagDetails { get; init; }

    /// <summary>Camera connector-specific health metadata details.</summary>
    [JsonPropertyName("cameraConnectorDetails")]
    public BetaCameraConnectorDetails? CameraConnectorDetails { get; init; }

    /// <summary>Camera-specific health metadata details.</summary>
    [JsonPropertyName("cameraDetails")]
    public BetaCameraDetails? CameraDetails { get; init; }

    /// <summary>Gateway-specific health metadata.</summary>
    [JsonPropertyName("gatewayDetails")]
    public BetaGatewayDetails? GatewayDetails { get; init; }
}

/// <summary>
/// BLE asset tag-specific health metadata details.
/// Mirrors the spec schema <c>BleAssetTagDetailsResponseResponseBody</c>.
/// </summary>
public sealed record BetaBleAssetTagDetails
{
    /// <summary>
    /// The BLE asset tag's battery state. One of: <c>critical</c>, <c>low</c>, <c>ok</c>,
    /// <c>unknown</c>.
    /// </summary>
    [JsonPropertyName("batteryState")]
    public string? BatteryState { get; init; }

    /// <summary>
    /// The timestamp when the BLE asset tag was last detected by a gateway in the Samsara
    /// network, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("lastCheckInTime")]
    public DateTimeOffset? LastCheckInTime { get; init; }
}

/// <summary>
/// Camera connector-specific health metadata details.
/// Mirrors the spec schema <c>CameraConnectorDetailsResponseResponseBody</c>.
/// </summary>
public sealed record BetaCameraConnectorDetails
{
    /// <summary>
    /// The timestamp when the gateway was last connected to the vehicle, in RFC 3339
    /// format.
    /// </summary>
    [JsonPropertyName("gatewayLastConnectedTime")]
    public DateTimeOffset? GatewayLastConnectedTime { get; init; }

    /// <summary>
    /// The percentage of successful recording time during the time when the vehicle is on
    /// an active trip over the past 50 hours, in percentage points.
    /// </summary>
    [JsonPropertyName("lastFiftyHoursUptimePercentage")]
    public double? LastFiftyHoursUptimePercentage { get; init; }

    /// <summary>The serial number of the vehicle gateway that the camera connector is connected to.</summary>
    [JsonPropertyName("vehicleGatewaySerial")]
    public string? VehicleGatewaySerial { get; init; }
}

/// <summary>
/// Camera-specific health metadata details.
/// Mirrors the spec schema <c>CameraDetailsResponseResponseBody</c>.
/// </summary>
public sealed record BetaCameraDetails
{
    /// <summary>
    /// The timestamp when the gateway was last connected to the vehicle, in RFC 3339
    /// format.
    /// </summary>
    [JsonPropertyName("gatewayLastConnectedTime")]
    public DateTimeOffset? GatewayLastConnectedTime { get; init; }

    /// <summary>
    /// The percentage of successful recording time during the time when the vehicle is on
    /// an active trip over the past 50 hours, in percentage points.
    /// </summary>
    [JsonPropertyName("lastFiftyHoursUptimePercentage")]
    public double? LastFiftyHoursUptimePercentage { get; init; }

    /// <summary>The serial number of the vehicle gateway that the camera is connected to.</summary>
    [JsonPropertyName("vehicleGatewaySerial")]
    public string? VehicleGatewaySerial { get; init; }
}

/// <summary>
/// Gateway-specific health metadata.
/// Mirrors the spec schema <c>GatewayDetailsResponseResponseBody</c>.
/// </summary>
public sealed record BetaGatewayDetails
{
    /// <summary>Gateway cellular connectivity information.</summary>
    [JsonPropertyName("cellConnectivity")]
    public BetaCellConnectivity? CellConnectivity { get; init; }

    /// <summary>
    /// The gateway's battery state. One of: <c>critical</c>, <c>low</c>, <c>ok</c>,
    /// <c>unknown</c>.
    /// </summary>
    [JsonPropertyName("gatewayBatteryState")]
    public string? GatewayBatteryState { get; init; }

    /// <summary>The battery temperature of the gateway, in degrees Celsius.</summary>
    [JsonPropertyName("gatewayBatteryTemp")]
    public double? GatewayBatteryTemp { get; init; }

    /// <summary>The battery voltage of the gateway, in volts.</summary>
    [JsonPropertyName("gatewayBatteryVolts")]
    public double? GatewayBatteryVolts { get; init; }

    /// <summary>The timestamp of the gateway's last check-in, in RFC 3339 format.</summary>
    [JsonPropertyName("lastCheckInTime")]
    public DateTimeOffset? LastCheckInTime { get; init; }

    /// <summary>The battery voltage of the vehicle that gateway is connected to, in volts.</summary>
    [JsonPropertyName("vehicleBatteryVolts")]
    public double? VehicleBatteryVolts { get; init; }
}

/// <summary>
/// Gateway cellular connectivity information.
/// Mirrors the spec schema <c>CellConnectivityResponseResponseBody</c>.
/// </summary>
public sealed record BetaCellConnectivity
{
    /// <summary>The cellular network provider name.</summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; init; }

    /// <summary>
    /// The cellular signal strength indicator. One of: <c>1/4</c>, <c>2/4</c>, <c>3/4</c>,
    /// <c>4/4</c>, <c>unknown</c>.
    /// </summary>
    [JsonPropertyName("signalBar")]
    public string? SignalBar { get; init; }
}

/// <summary>
/// Information about an active health reason affecting the device.
/// Mirrors the spec schema <c>HealthReasonResponseResponseBody</c>.
/// </summary>
public sealed record BetaDeviceHealthReason
{
    /// <summary>
    /// The type of health reason detected. One of 17 spec-defined values, including
    /// <c>assetUnplugged</c>, <c>cameraMisaligned</c>, <c>dataPending</c>, <c>healthy</c>;
    /// see the Samsara API reference for the full enumeration.
    /// </summary>
    [JsonPropertyName("healthReasonCode")]
    public string? HealthReasonCode { get; init; }

    /// <summary>The timestamp when this health reason began, in RFC3339 format.</summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }
}

/// <summary>
/// The most recent location information for the device.
/// Mirrors the spec schema <c>LastKnownLocationResponseResponseBody</c>.
/// </summary>
public sealed record BetaDeviceLastKnownLocation
{
    /// <summary>The unique ID of the address.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Latitude of a location.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude of a location.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// Returned by <c>GET /beta/aemp/Fleet/{pageNumber}</c> (operationId
/// <c>getAempEquipmentList</c>, beta). This endpoint is AEMP/ISO 15143-3 shaped and has no
/// <c>data</c> envelope.
/// Mirrors the spec schema <c>AempEquipmentGetAempEquipmentListResponseBody</c>.
/// </summary>
public sealed record AempEquipmentList
{
    /// <summary>
    /// Contains a list of equipment objects and links. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("Fleet")]
    public AempFleet? Fleet { get; init; }
}

/// <summary>
/// Contains a list of equipment objects and links.
/// Mirrors the spec schema <c>AempFleetListResponseBody</c>.
/// </summary>
public sealed record AempFleet
{
    /// <summary>The list of Equipment objects. Spec marks this required on the response.</summary>
    [JsonPropertyName("Equipment")]
    public IReadOnlyList<AempEquipment>? Equipment { get; init; }

    /// <summary>
    /// The list of links associated with the current API request. Spec marks this required
    /// on the response.
    /// </summary>
    [JsonPropertyName("Links")]
    public IReadOnlyList<AempLink>? Links { get; init; }

    /// <summary>
    /// Date and time at which the snapshot of the fleet was created in RFC 3339 format.
    /// Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("snapshotTime")]
    public string? SnapshotTime { get; init; }

    /// <summary>
    /// The version of the ISO/TS 15143-3 standard. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; init; }
}

/// <summary>
/// Contains equipment fields.
/// Mirrors the spec schema <c>AempEquipmentWithAdditionalFieldsResponseBody</c>.
/// </summary>
public sealed record AempEquipment
{
    /// <summary>Equipment operating hours.</summary>
    [JsonPropertyName("CumulativeOperatingHours")]
    public AempCumulativeOperatingHours? CumulativeOperatingHours { get; init; }

    /// <summary>DEF remaining in equipment.</summary>
    [JsonPropertyName("DEFRemaining")]
    public AempDefRemaining? DEFRemaining { get; init; }

    /// <summary>Equipment odometer distance.</summary>
    [JsonPropertyName("Distance")]
    public AempDistance? Distance { get; init; }

    /// <summary>Equipment engine status.</summary>
    [JsonPropertyName("EngineStatus")]
    public AempEngineStatus? EngineStatus { get; init; }

    /// <summary>Equipment header fields. Spec marks this required on the response.</summary>
    [JsonPropertyName("EquipmentHeader")]
    public AempEquipmentHeader? EquipmentHeader { get; init; }

    /// <summary>Fuel remaining in equipment.</summary>
    [JsonPropertyName("FuelRemaining")]
    public AempFuelRemaining? FuelRemaining { get; init; }

    /// <summary>Equipment location. Spec marks this required on the response.</summary>
    [JsonPropertyName("Location")]
    public AempLocation? Location { get; init; }
}

/// <summary>
/// Equipment header fields.
/// Mirrors the spec schema <c>EquipmentHeaderWithAdditionalFieldsResponseBody</c>.
/// </summary>
public sealed record AempEquipmentHeader
{
    /// <summary>
    /// The unique Samsara ID of the equipment. This is automatically generated when the
    /// Equipment object is created. It cannot be changed.
    /// </summary>
    [JsonPropertyName("EquipmentID")]
    public string? EquipmentID { get; init; }

    /// <summary>The model of the equipment.</summary>
    [JsonPropertyName("Model")]
    public string? Model { get; init; }

    /// <summary>The make of the equipment.</summary>
    [JsonPropertyName("OEMName")]
    public string? OEMName { get; init; }

    /// <summary>The PIN number of the equipment.</summary>
    [JsonPropertyName("PIN")]
    public string? PIN { get; init; }

    /// <summary>The serial number of the equipment.</summary>
    [JsonPropertyName("SerialNumber")]
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Telematics unit install date in RFC 3339 format. Millisecond precision and timezones
    /// are supported. (Examples: 2019-06-13T19:08:25Z, 2019-06-13T19:08:25.455Z, OR
    /// 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("UnitInstallDateTime")]
    public string? UnitInstallDateTime { get; init; }
}

/// <summary>
/// Equipment location.
/// Mirrors the spec schema <c>LocationResponseBody</c>.
/// </summary>
public sealed record AempLocation
{
    /// <summary>Location latitude.</summary>
    [JsonPropertyName("Latitude")]
    public double? Latitude { get; init; }

    /// <summary>Location longitude.</summary>
    [JsonPropertyName("Longitude")]
    public double? Longitude { get; init; }

    /// <summary>
    /// Date time in RFC 3339 format. Millisecond precision and timezones are supported.
    /// (Examples: 2019-06-13T19:08:25Z, 2019-06-13T19:08:25.455Z, OR
    /// 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("datetime")]
    public string? Datetime { get; init; }
}

/// <summary>
/// Equipment odometer distance.
/// Mirrors the spec schema <c>DistanceResponseBody</c>.
/// </summary>
public sealed record AempDistance
{
    /// <summary>Odometer value reported by equipment.</summary>
    [JsonPropertyName("Odometer")]
    public double? Odometer { get; init; }

    /// <summary>Unit of measurement for distance.</summary>
    [JsonPropertyName("OdometerUnits")]
    public string? OdometerUnits { get; init; }

    /// <summary>
    /// Date time in RFC 3339 format. Millisecond precision and timezones are supported.
    /// (Examples: 2019-06-13T19:08:25Z, 2019-06-13T19:08:25.455Z, OR
    /// 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("datetime")]
    public string? Datetime { get; init; }
}

/// <summary>
/// Equipment engine status.
/// Mirrors the spec schema <c>EngineStatusResponseBody</c>.
/// </summary>
public sealed record AempEngineStatus
{
    /// <summary>Boolean value for whether engine is running or not.</summary>
    [JsonPropertyName("Running")]
    public bool? Running { get; init; }

    /// <summary>
    /// Date time in RFC 3339 format. Millisecond precision and timezones are supported.
    /// (Examples: 2019-06-13T19:08:25Z, 2019-06-13T19:08:25.455Z, OR
    /// 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("datetime")]
    public string? Datetime { get; init; }
}

/// <summary>
/// Fuel remaining in equipment.
/// Mirrors the spec schema <c>FuelRemainingResponseBody</c>.
/// </summary>
public sealed record AempFuelRemaining
{
    /// <summary>Percent of fuel remaining in tank.</summary>
    [JsonPropertyName("Percent")]
    public double? Percent { get; init; }

    /// <summary>
    /// Date time in RFC 3339 format. Millisecond precision and timezones are supported.
    /// (Examples: 2019-06-13T19:08:25Z, 2019-06-13T19:08:25.455Z, OR
    /// 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("datetime")]
    public string? Datetime { get; init; }
}

/// <summary>
/// DEF remaining in equipment.
/// Mirrors the spec schema <c>DEFRemainingResponseBody</c>.
/// </summary>
public sealed record AempDefRemaining
{
    /// <summary>Percent of DEF remaining in tank.</summary>
    [JsonPropertyName("Percent")]
    public double? Percent { get; init; }

    /// <summary>
    /// Date time in RFC 3339 format. Millisecond precision and timezones are supported.
    /// (Examples: 2019-06-13T19:08:25Z, 2019-06-13T19:08:25.455Z, OR
    /// 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("datetime")]
    public string? Datetime { get; init; }
}

/// <summary>
/// Equipment operating hours.
/// Mirrors the spec schema <c>CumulativeOperatingHoursResponseBody</c>.
/// </summary>
public sealed record AempCumulativeOperatingHours
{
    /// <summary>Total number of equipment operating hours.</summary>
    [JsonPropertyName("Hour")]
    public double? Hour { get; init; }

    /// <summary>
    /// Date time in RFC 3339 format. Millisecond precision and timezones are supported.
    /// (Examples: 2019-06-13T19:08:25Z, 2019-06-13T19:08:25.455Z, OR
    /// 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("datetime")]
    public string? Datetime { get; init; }
}

/// <summary>
/// Contains a list of relevant links.
/// Mirrors the spec schema <c>AempLinkResponseBody</c>.
/// </summary>
public sealed record AempLink
{
    /// <summary>The hyperlink of the relationship. Spec marks this required on the response.</summary>
    [JsonPropertyName("href")]
    public string? Href { get; init; }

    /// <summary>The link relationship to the current call. Spec marks this required on the response.</summary>
    [JsonPropertyName("rel")]
    public string? Rel { get; init; }
}

/// <summary>
/// The <c>data</c> object returned by <c>GET /beta/fleet/drivers/efficiency</c> (operationId
/// <c>getDriverEfficiency</c>, beta).
/// Mirrors the spec schema <c>DriverEfficienciesResponse_data</c>.
/// </summary>
public sealed record BetaDriverEfficiencySummary
{
    /// <summary>A list of driver and associated vehicle efficiency data.</summary>
    [JsonPropertyName("driverSummaries")]
    public IReadOnlyList<BetaDriverEfficiency>? DriverSummaries { get; init; }

    /// <summary>
    /// End time of the window for which this efficiency report was computed. Will be a UTC
    /// timestamp in RFC 3339 format. For example: `2020-03-16T16:00:00Z`.
    /// </summary>
    [JsonPropertyName("summaryEndTime")]
    public DateTimeOffset? SummaryEndTime { get; init; }

    /// <summary>
    /// Start time of the window for which this efficiency report was computed. Will be a
    /// UTC timestamp in RFC 3339 format. For example: `2020-03-15T16:00:00Z`.
    /// </summary>
    [JsonPropertyName("summaryStartTime")]
    public DateTimeOffset? SummaryStartTime { get; init; }
}

/// <summary>
/// Summary of a driver's efficiency.
/// Mirrors the spec schema <c>DriverEfficiency</c>.
/// </summary>
public sealed record BetaDriverEfficiency
{
    /// <summary>Quick braking events (less than one second after accelerating).</summary>
    [JsonPropertyName("anticipationBrakeEventCount")]
    public double? AnticipationBrakeEventCount { get; init; }

    /// <summary>Time spent without engaging the accelerator or brake in milliseconds.</summary>
    [JsonPropertyName("coastingDurationMs")]
    public double? CoastingDurationMs { get; init; }

    /// <summary>Time spent in cruise control in milliseconds.</summary>
    [JsonPropertyName("cruiseControlDurationMs")]
    public double? CruiseControlDurationMs { get; init; }

    /// <summary>A minified driver object.</summary>
    [JsonPropertyName("driver")]
    public BetaDriverEfficiencyDriver? Driver { get; init; }

    /// <summary>Time in efficient RPM (800 to 17000) in milliseconds.</summary>
    [JsonPropertyName("greenBandDrivingDurationMs")]
    public double? GreenBandDrivingDurationMs { get; init; }

    /// <summary>Time the vehicle engine torque is greater than 90% in milliseconds.</summary>
    [JsonPropertyName("highTorqueMs")]
    public double? HighTorqueMs { get; init; }

    /// <summary>Driving time spent over the efficient speed threshold in milliseconds.</summary>
    [JsonPropertyName("overSpeedMs")]
    public double? OverSpeedMs { get; init; }

    /// <summary>Total number of brake events.</summary>
    [JsonPropertyName("totalBrakeEventCount")]
    public double? TotalBrakeEventCount { get; init; }

    /// <summary>Distance driven in meters.</summary>
    [JsonPropertyName("totalDistanceDrivenMeters")]
    public double? TotalDistanceDrivenMeters { get; init; }

    /// <summary>Time driven in milliseconds.</summary>
    [JsonPropertyName("totalDriveTimeDurationMs")]
    public double? TotalDriveTimeDurationMs { get; init; }

    /// <summary>Fuel consumption in milliliters.</summary>
    [JsonPropertyName("totalFuelConsumedMl")]
    public double? TotalFuelConsumedMl { get; init; }

    /// <summary>Time spent idling in milliseconds.</summary>
    [JsonPropertyName("totalIdleTimeDurationMs")]
    public double? TotalIdleTimeDurationMs { get; init; }

    /// <summary>Time spent with power take off enabled while idling in milliseconds.</summary>
    [JsonPropertyName("totalPowerTakeOffDurationMs")]
    public double? TotalPowerTakeOffDurationMs { get; init; }

    /// <summary>
    /// Summaries of vehicle efficiency for each vehicle the driver was driving during the
    /// given time period.
    /// </summary>
    [JsonPropertyName("vehicleSummaries")]
    public IReadOnlyList<BetaDriverEfficiencyVehicleSummary>? VehicleSummaries { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>VehicleSummary</c>.
/// </summary>
public sealed record BetaDriverEfficiencyVehicleSummary
{
    /// <summary>Quick braking events (less than one second after accelerating).</summary>
    [JsonPropertyName("anticipationBrakeEventCount")]
    public double? AnticipationBrakeEventCount { get; init; }

    /// <summary>Time spent without engaging the accelerator or brake in milliseconds.</summary>
    [JsonPropertyName("coastingDurationMs")]
    public double? CoastingDurationMs { get; init; }

    /// <summary>Time spent in cruise control in milliseconds.</summary>
    [JsonPropertyName("cruiseControlDurationMs")]
    public double? CruiseControlDurationMs { get; init; }

    /// <summary>Distance driven in meters.</summary>
    [JsonPropertyName("distanceDrivenMeters")]
    public double? DistanceDrivenMeters { get; init; }

    /// <summary>Time driven in milliseconds.</summary>
    [JsonPropertyName("driveTimeDurationMs")]
    public double? DriveTimeDurationMs { get; init; }

    /// <summary>Fuel consumption in milliliters.</summary>
    [JsonPropertyName("fuelConsumedMl")]
    public double? FuelConsumedMl { get; init; }

    /// <summary>Time in efficient RPM (800 to 17000) in milliseconds.</summary>
    [JsonPropertyName("greenBandDrivingDurationMs")]
    public double? GreenBandDrivingDurationMs { get; init; }

    /// <summary>Time the vehicle engine torque is greater than 90% in milliseconds.</summary>
    [JsonPropertyName("highTorqueMs")]
    public double? HighTorqueMs { get; init; }

    /// <summary>Time spent idling in milliseconds.</summary>
    [JsonPropertyName("idleTimeDurationMs")]
    public double? IdleTimeDurationMs { get; init; }

    /// <summary>Driving time spent over the efficient speed threshold in milliseconds.</summary>
    [JsonPropertyName("overSpeedMs")]
    public double? OverSpeedMs { get; init; }

    /// <summary>Time spent with power take off enabled while idling in milliseconds.</summary>
    [JsonPropertyName("powerTakeOffDurationMs")]
    public double? PowerTakeOffDurationMs { get; init; }

    /// <summary>Total number of brake events.</summary>
    [JsonPropertyName("totalBrakeEventCount")]
    public double? TotalBrakeEventCount { get; init; }

    /// <summary>A minified vehicle object.</summary>
    [JsonPropertyName("vehicle")]
    public BetaDriverEfficiencyVehicle? Vehicle { get; init; }
}

/// <summary>
/// A minified driver object.
/// Mirrors the spec schema <c>ExtendedDriverTinyResponse</c>.
/// </summary>
public sealed record BetaDriverEfficiencyDriver
{
    /// <summary>
    /// The [external IDs](https://developers.samsara.com/docs/external-ids) for the given
    /// object.
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>ID of the driver.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Username of the driver.</summary>
    [JsonPropertyName("username")]
    public string? Username { get; init; }
}

/// <summary>
/// A minified vehicle object.
/// Mirrors the spec schema <c>vehicleTinyResponse</c>.
/// </summary>
public sealed record BetaDriverEfficiencyVehicle
{
    /// <summary>
    /// The [external IDs](https://developers.samsara.com/docs/external-ids) for the given
    /// object.
    /// </summary>
    [JsonPropertyName("ExternalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Detection log entry.
/// One item of the <c>data</c> array returned by <c>GET /detections/stream</c> (operationId
/// <c>getDetections</c>, beta).
/// Mirrors the spec schema <c>DetectionLogDetectionObjectResponseBody</c>.
/// </summary>
public sealed record Detection
{
    /// <summary>
    /// Asset that the detection is tied to. Always returned. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("asset")]
    public DetectionAsset? Asset { get; init; }

    /// <summary>
    /// Time the detection was detected or in-cab alert played in UTC. RFC 3339 format.
    /// Always returned. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>
    /// Driver that is assigned to the safety event. Always returned. Null if driver is not
    /// assigned.
    /// </summary>
    [JsonPropertyName("driver")]
    public DetectionDriver? Driver { get; init; }

    /// <summary>Unique Samsara ID (uuid) of the detection. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Whether an in cab alert played aloud in the cab. Always returned. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("inCabAlertPlayed")]
    public bool? InCabAlertPlayed { get; init; }

    /// <summary>
    /// Details on the associated safety event generated. Always returned. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("safetyEvent")]
    public IReadOnlyList<DetectionSafetyEvent>? SafetyEvent { get; init; }

    /// <summary>
    /// The label associated with the detection. Always returned. One of 34 spec-defined
    /// values, including <c>acceleration</c>, <c>braking</c>, <c>crash</c>, <c>drowsy</c>;
    /// see the Samsara API reference for the full enumeration. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("triggerDetectionLabel")]
    public string? TriggerDetectionLabel { get; init; }

    /// <summary>
    /// Time the detection was updated in Samsara in UTC. RFC 3339 format. Always returned.
    /// Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// Asset that the detection is tied to. Always returned.
/// Mirrors the spec schema <c>DetectionLogAssetObjectResponseBody</c>.
/// </summary>
public sealed record DetectionAsset
{
    /// <summary>List of attributes associated with the entity.</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AttributeTiny>? Attributes { get; init; }

    /// <summary>
    /// Unique ID for the asset object that is reporting the detection. Always returned.
    /// Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name for the asset object that is reporting the safety event. Only returned when
    /// includeAsset is set to true.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Tags for the asset associated with the safety event. Only returned when includeAsset
    /// is set to true.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }
}

/// <summary>
/// Driver that is assigned to the safety event. Always returned. Null if driver is not
/// assigned.
/// Mirrors the spec schema <c>DetectionLogDriverObjectResponseBody</c>.
/// </summary>
public sealed record DetectionDriver
{
    /// <summary>List of attributes associated with the entity.</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AttributeTiny>? Attributes { get; init; }

    /// <summary>
    /// A map of external ids for the driver assigned to the safety event. Only returned
    /// when includeDriver is set to true.
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// Unique ID for the driver object that is assigned to the safety event. Always
    /// returned when a driver is assigned. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the driver assigned to the safety event. Only returned when includeDriver is
    /// set to true.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Tags for the driver assigned to the safety event. Only returned when includeDriver
    /// is set to true.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }
}

/// <summary>
/// Details on the associated safety event generated.
/// Mirrors the spec schema <c>DetectionLogSafetyEventObjectResponseBody</c>.
/// </summary>
public sealed record DetectionSafetyEvent
{
    /// <summary>
    /// Unique Samsara ID (uuid) of the safety event. Only returned when
    /// safetyEvent.inboxEvent is true.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Whether a corresponding safety event was published to the Safety Inbox. Always
    /// returned. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("inboxEvent")]
    public bool? InboxEvent { get; init; }

    /// <summary>
    /// The reason the detection was filtered out of the inbox. Only returned when
    /// safetyEvent.inboxEvent is false. One of 13 spec-defined values, including
    /// <c>overDailyLimit</c>, <c>overHourlyLimit</c>, <c>overTripLimit</c>,
    /// <c>belowConfidenceThreshold</c>; see the Samsara API reference for the full
    /// enumeration.
    /// </summary>
    [JsonPropertyName("inboxFilterReason")]
    public string? InboxFilterReason { get; init; }
}

/// <summary>
/// Full detail of a voice agent session.
/// One item of the <c>data</c> array returned by <c>GET /agent-studio/voice-sessions</c>
/// (operationId <c>getVoiceSessions</c>, beta).
/// Mirrors the spec schema <c>AgentStudioVoiceSessionDetailResponseBody</c>.
/// </summary>
public sealed record VoiceSession
{
    /// <summary>
    /// Identifier of the Agent Studio agent that handled the session. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("agentId")]
    public string? AgentId { get; init; }

    /// <summary>
    /// Display name of the agent that handled the session. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("agentName")]
    public string? AgentName { get; init; }

    /// <summary>
    /// Lifecycle events that occurred during the session, ordered by time ascending. Spec
    /// marks this required on the response.
    /// </summary>
    [JsonPropertyName("callEvents")]
    public IReadOnlyList<VoiceSessionCallEvent>? CallEvents { get; init; }

    /// <summary>Duration of the session, in milliseconds. Spec marks this required on the response.</summary>
    [JsonPropertyName("durationMilliseconds")]
    public long? DurationMilliseconds { get; init; }

    /// <summary>
    /// Time at which the session started, in RFC 3339 format. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("happenedAtTime")]
    public DateTimeOffset? HappenedAtTime { get; init; }

    /// <summary>Unique identifier for the voice session. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Human-readable name of the call recipient (driver name, dashboard user name, or
    /// phone number). Empty string when the recipient could not be resolved. Spec marks
    /// this required on the response.
    /// </summary>
    [JsonPropertyName("recipient")]
    public string? Recipient { get; init; }

    /// <summary>
    /// Presigned URL for the call recording. Empty string when no recording is available
    /// (e.g. the session has not completed). Expires at recordingUrlExpiresAtTime. Spec
    /// marks this required on the response.
    /// </summary>
    [JsonPropertyName("recordingUrl")]
    public string? RecordingUrl { get; init; }

    /// <summary>
    /// Time at which recordingUrl stops working, in RFC 3339 format. Empty string when
    /// recordingUrl is empty. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("recordingUrlExpiresAtTime")]
    public string? RecordingUrlExpiresAtTime { get; init; }

    /// <summary>
    /// Lifecycle status of the session. One of: <c>completed</c>, <c>running</c>,
    /// <c>failed</c>, <c>unknown</c>. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("sessionStatus")]
    public string? SessionStatus { get; init; }

    /// <summary>
    /// Tool calls made by the agent during the session, ordered by start time ascending.
    /// Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("toolCalls")]
    public IReadOnlyList<VoiceSessionToolCall>? ToolCalls { get; init; }

    /// <summary>
    /// Conversation transcript, ordered by start time ascending. Spec marks this required
    /// on the response.
    /// </summary>
    [JsonPropertyName("transcript")]
    public IReadOnlyList<VoiceSessionTranscriptEntry>? Transcript { get; init; }

    /// <summary>
    /// Human-readable name of the trigger that initiated the session, as configured in
    /// Automations. Empty string when no trigger is associated with the session. Spec marks
    /// this required on the response.
    /// </summary>
    [JsonPropertyName("triggerType")]
    public string? TriggerType { get; init; }

    /// <summary>
    /// Time at which the session was last updated, in RFC 3339 format. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// A lifecycle event that occurred during a voice session.
/// Mirrors the spec schema <c>AgentStudioVoiceSessionCallEventResponseBody</c>.
/// </summary>
public sealed record VoiceSessionCallEvent
{
    /// <summary>
    /// Time at which the event occurred, in RFC 3339 format. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("happenedAtTime")]
    public DateTimeOffset? HappenedAtTime { get; init; }

    /// <summary>
    /// Human-readable description of the lifecycle event. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// A tool invocation made by the agent during a voice session.
/// Mirrors the spec schema <c>AgentStudioVoiceSessionToolCallResponseBody</c>.
/// </summary>
public sealed record VoiceSessionToolCall
{
    /// <summary>
    /// Arguments passed to the tool, as a JSON-encoded string. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("arguments")]
    public string? Arguments { get; init; }

    /// <summary>
    /// Wall-clock duration of the tool call, in milliseconds. Zero when the duration is
    /// unknown. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("durationMilliseconds")]
    public long? DurationMilliseconds { get; init; }

    /// <summary>Name of the tool that was invoked. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Output returned by the tool, as a string. Empty when the tool produced no output.
    /// Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("output")]
    public string? Output { get; init; }

    /// <summary>
    /// Offset from the start of the session at which the tool call started, in
    /// milliseconds. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("startMilliseconds")]
    public long? StartMilliseconds { get; init; }

    /// <summary>
    /// Outcome of the tool call. Defaults to `unknown` when the outcome could not be
    /// determined. One of: <c>success</c>, <c>error</c>, <c>unknown</c>. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

/// <summary>
/// A single utterance in a voice session transcript.
/// Mirrors the spec schema <c>AgentStudioVoiceSessionTranscriptEntryResponseBody</c>.
/// </summary>
public sealed record VoiceSessionTranscriptEntry
{
    /// <summary>
    /// Offset from the start of the session at which this utterance ended, in milliseconds.
    /// Approximated from the start of the next utterance (or the end of the session for the
    /// final utterance). Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("endMilliseconds")]
    public long? EndMilliseconds { get; init; }

    /// <summary>
    /// Who produced this utterance. Defaults to `unknown` when the speaker cannot be
    /// confidently classified. One of: <c>agent</c>, <c>driver</c>, <c>admin</c>,
    /// <c>unknown</c>. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("speakerType")]
    public string? SpeakerType { get; init; }

    /// <summary>
    /// Offset from the start of the session at which this utterance began, in milliseconds.
    /// Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("startMilliseconds")]
    public long? StartMilliseconds { get; init; }

    /// <summary>Transcribed text of the utterance. Spec marks this required on the response.</summary>
    [JsonPropertyName("text")]
    public string? Text { get; init; }
}

/// <summary>
/// Summary of a voice agent session.
/// One item of the <c>data</c> array returned by <c>GET /agent-studio/voice-sessions/stream</c>
/// (operationId <c>getVoiceSessionsStream</c>, beta).
/// Mirrors the spec schema <c>AgentStudioVoiceSessionSummaryResponseBody</c>.
/// </summary>
public sealed record VoiceSessionSummary
{
    /// <summary>
    /// Identifier of the Agent Studio agent that handled the session. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("agentId")]
    public string? AgentId { get; init; }

    /// <summary>
    /// Display name of the agent that handled the session. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("agentName")]
    public string? AgentName { get; init; }

    /// <summary>Duration of the session, in milliseconds. Spec marks this required on the response.</summary>
    [JsonPropertyName("durationMilliseconds")]
    public long? DurationMilliseconds { get; init; }

    /// <summary>
    /// Time at which the session started, in RFC 3339 format. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("happenedAtTime")]
    public DateTimeOffset? HappenedAtTime { get; init; }

    /// <summary>Unique identifier for the voice session. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Human-readable name of the call recipient (driver name, dashboard user name, or
    /// phone number). Empty string when the recipient could not be resolved. Spec marks
    /// this required on the response.
    /// </summary>
    [JsonPropertyName("recipient")]
    public string? Recipient { get; init; }

    /// <summary>
    /// Lifecycle status of the session. One of: <c>completed</c>, <c>running</c>,
    /// <c>failed</c>, <c>unknown</c>. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("sessionStatus")]
    public string? SessionStatus { get; init; }

    /// <summary>
    /// Human-readable name of the trigger that initiated the session, as configured in
    /// Automations. Empty string when no trigger is associated with the session. Spec marks
    /// this required on the response.
    /// </summary>
    [JsonPropertyName("triggerType")]
    public string? TriggerType { get; init; }

    /// <summary>
    /// Time at which the session was last updated, in RFC 3339 format. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}
