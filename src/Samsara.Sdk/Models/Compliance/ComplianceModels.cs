namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a Hours of Service (HOS) log entry.
/// </summary>
public sealed record HosLog
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("codriverIds")]
    public IReadOnlyList<string>? CodriverIds { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("hosStatusType")]
    public string? HosStatusType { get; init; }

    [JsonPropertyName("logStartMs")]
    public long? LogStartMs { get; init; }

    [JsonPropertyName("locLat")]
    public double? LocLat { get; init; }

    [JsonPropertyName("locLng")]
    public double? LocLng { get; init; }

    [JsonPropertyName("locCity")]
    public string? LocCity { get; init; }

    [JsonPropertyName("locState")]
    public string? LocState { get; init; }

    [JsonPropertyName("locName")]
    public string? LocName { get; init; }

    [JsonPropertyName("groupId")]
    public long? GroupId { get; init; }

    [JsonPropertyName("remark")]
    public string? Remark { get; init; }
}

/// <summary>
/// Represents an HOS violation.
/// </summary>
public sealed record HosViolation
{
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("violationType")]
    public string? ViolationType { get; init; }

    [JsonPropertyName("startMs")]
    public long? StartMs { get; init; }

    [JsonPropertyName("endMs")]
    public long? EndMs { get; init; }

    [JsonPropertyName("severityType")]
    public string? SeverityType { get; init; }
}

/// <summary>
/// Represents a DVIR (Driver Vehicle Inspection Report).
/// </summary>
public sealed record DvirEntry
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("inspectionType")]
    public string? InspectionType { get; init; }

    [JsonPropertyName("vehicle")]
    public DvirVehicle? Vehicle { get; init; }

    [JsonPropertyName("authorSignature")]
    public DvirSignature? AuthorSignature { get; init; }

    [JsonPropertyName("mechanicSignature")]
    public DvirSignature? MechanicSignature { get; init; }

    [JsonPropertyName("nextDriverSignature")]
    public DvirSignature? NextDriverSignature { get; init; }

    [JsonPropertyName("vehicleCondition")]
    public string? VehicleCondition { get; init; }

    [JsonPropertyName("defects")]
    public IReadOnlyList<DvirDefect>? Defects { get; init; }

    [JsonPropertyName("safeToOperate")]
    public bool? SafeToOperate { get; init; }

    [JsonPropertyName("timeMs")]
    public long? TimeMs { get; init; }

    [JsonPropertyName("odometerMiles")]
    public double? OdometerMiles { get; init; }
}

/// <summary>
/// Vehicle reference in a DVIR.
/// </summary>
public sealed record DvirVehicle
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Signature for a DVIR entry.
/// </summary>
public sealed record DvirSignature
{
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("signedAtMs")]
    public long? SignedAtMs { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }
}

/// <summary>
/// A defect noted in a DVIR inspection.
/// </summary>
public sealed record DvirDefect
{
    [JsonPropertyName("defectType")]
    public string? DefectType { get; init; }

    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    [JsonPropertyName("isResolved")]
    public bool? IsResolved { get; init; }
}

/// <summary>
/// HOS clocks for a single driver — the response item of <c>GET /fleet/hos/clocks</c>.
/// </summary>
public sealed record HosClocksForDriver
{
    [JsonPropertyName("driver")]
    public Samsara.Sdk.Models.Common.EntityReference? Driver { get; init; }

    [JsonPropertyName("clocks")]
    public HosClocks? Clocks { get; init; }

    [JsonPropertyName("currentDutyStatus")]
    public HosCurrentDutyStatus? CurrentDutyStatus { get; init; }

    [JsonPropertyName("currentVehicle")]
    public Samsara.Sdk.Models.Common.EntityReference? CurrentVehicle { get; init; }

    [JsonPropertyName("violations")]
    public HosViolationClocks? Violations { get; init; }
}

/// <summary>
/// HOS remaining-duration clocks (break / cycle / drive / shift).
/// </summary>
public sealed record HosClocks
{
    [JsonPropertyName("break")]
    public HosBreakClock? Break { get; init; }

    [JsonPropertyName("cycle")]
    public HosCycleClock? Cycle { get; init; }

    [JsonPropertyName("drive")]
    public HosDriveClock? Drive { get; init; }

    [JsonPropertyName("shift")]
    public HosShiftClock? Shift { get; init; }
}

/// <summary>Break clock.</summary>
public sealed record HosBreakClock
{
    [JsonPropertyName("timeUntilBreakDurationMs")]
    public double? TimeUntilBreakDurationMs { get; init; }
}

/// <summary>Cycle clock.</summary>
public sealed record HosCycleClock
{
    [JsonPropertyName("cycleRemainingDurationMs")]
    public double? CycleRemainingDurationMs { get; init; }

    [JsonPropertyName("cycleStartedAtTime")]
    public DateTimeOffset? CycleStartedAtTime { get; init; }

    [JsonPropertyName("cycleTomorrowDurationMs")]
    public double? CycleTomorrowDurationMs { get; init; }
}

/// <summary>Drive clock.</summary>
public sealed record HosDriveClock
{
    [JsonPropertyName("driveRemainingDurationMs")]
    public double? DriveRemainingDurationMs { get; init; }
}

/// <summary>Shift clock.</summary>
public sealed record HosShiftClock
{
    [JsonPropertyName("shiftRemainingDurationMs")]
    public double? ShiftRemainingDurationMs { get; init; }
}

/// <summary>Current duty status for an HOS clock entry.</summary>
public sealed record HosCurrentDutyStatus
{
    [JsonPropertyName("hosStatusType")]
    public string? HosStatusType { get; init; }
}

/// <summary>Active HOS violation durations.</summary>
public sealed record HosViolationClocks
{
    [JsonPropertyName("cycleViolationDurationMs")]
    public double? CycleViolationDurationMs { get; init; }

    [JsonPropertyName("shiftDrivingViolationDurationMs")]
    public double? ShiftDrivingViolationDurationMs { get; init; }
}

/// <summary>
/// Represents an HOS daily log summary for a driver.
/// </summary>
public sealed record HosDailyLog
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("certificationState")]
    public string? CertificationState { get; init; }

    [JsonPropertyName("date")]
    public string? Date { get; init; }

    [JsonPropertyName("distanceDrivenMeters")]
    public double? DistanceDrivenMeters { get; init; }
}

/// <summary>
/// Represents an ELD event record from HOS data.
/// </summary>
public sealed record HosEldEvent
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("driverActivationStatus")]
    public string? DriverActivationStatus { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("eventType")]
    public string? EventType { get; init; }

    [JsonPropertyName("eventCode")]
    public string? EventCode { get; init; }

    [JsonPropertyName("eventTime")]
    public DateTimeOffset? EventTime { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("odometer")]
    public double? Odometer { get; init; }

    [JsonPropertyName("engineHours")]
    public double? EngineHours { get; init; }
}
