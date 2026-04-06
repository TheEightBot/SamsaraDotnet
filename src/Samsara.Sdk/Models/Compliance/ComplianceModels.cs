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
/// HOS clocks (remaining drive time, etc.) for a driver.
/// </summary>
public sealed record HosClocks
{
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    [JsonPropertyName("timeUntilBreakMs")]
    public long? TimeUntilBreakMs { get; init; }

    [JsonPropertyName("drivingTimeMs")]
    public long? DrivingTimeMs { get; init; }

    [JsonPropertyName("dutyTimeMs")]
    public long? DutyTimeMs { get; init; }

    [JsonPropertyName("cycleTimeMs")]
    public long? CycleTimeMs { get; init; }

    [JsonPropertyName("shiftDriveTimeMs")]
    public long? ShiftDriveTimeMs { get; init; }

    [JsonPropertyName("shiftTimeMs")]
    public long? ShiftTimeMs { get; init; }

    [JsonPropertyName("cycleTimeLeftMs")]
    public long? CycleTimeLeftMs { get; init; }

    [JsonPropertyName("shiftDriveTimeLeftMs")]
    public long? ShiftDriveTimeLeftMs { get; init; }

    [JsonPropertyName("shiftTimeLeftMs")]
    public long? ShiftTimeLeftMs { get; init; }
}
