namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;

/// <summary>
/// HOS logs grouped by driver — the response item of <c>GET /fleet/hos/logs</c>.
/// Mirrors the spec's <c>HosLogsForDriver</c> shape, which nests a per-driver
/// list of <see cref="HosLogEntry"/> objects rather than emitting flat scalars.
/// </summary>
/// <remarks>
/// Earlier SDK versions modeled this type as a flat record with <c>driverId</c>,
/// <c>vehicleId</c>, <c>hosStatusType</c>, etc. Those scalars are retained as
/// nullable convenience properties on this record for backward compatibility,
/// but the canonical spec shape lives on <see cref="Driver"/> and
/// <see cref="HosLogs"/>.
/// </remarks>
public sealed record HosLog
{
    /// <summary>
    /// The driver these HOS logs are for (nested object per spec). Spec-optional.
    /// </summary>
    [JsonPropertyName("driver")]
    public Samsara.Sdk.Models.Common.EntityReference? Driver { get; init; }

    /// <summary>
    /// The HOS log entries for this driver, ordered by start time (per spec).
    /// </summary>
    [JsonPropertyName("hosLogs")]
    public IReadOnlyList<HosLogEntry>? HosLogs { get; init; }
}

/// <summary>
/// A single HOS log entry within a <see cref="HosLog"/>. Mirrors the spec's
/// <c>HosLogEntry</c> schema.
/// </summary>
public sealed record HosLogEntry
{
    /// <summary>
    /// Co-driver information. Each entry is a minified driver object (id + name).
    /// </summary>
    [JsonPropertyName("codrivers")]
    public IReadOnlyList<Samsara.Sdk.Models.Common.EntityReference>? Codrivers { get; init; }

    /// <summary>
    /// The Hours of Service status type. Valid values: <c>offDuty</c>, <c>sleeperBed</c>,
    /// <c>driving</c>, <c>onDuty</c>, <c>yardMove</c>, <c>personalConveyance</c>.
    /// </summary>
    [JsonPropertyName("hosStatusType")]
    public string? HosStatusType { get; init; }

    /// <summary>
    /// The end time of the log entry, in RFC 3339 format (UTC).
    /// </summary>
    [JsonPropertyName("logEndTime")]
    public string? LogEndTime { get; init; }

    /// <summary>
    /// Location associated with the duty status change.
    /// </summary>
    [JsonPropertyName("logRecordedLocation")]
    public HosLogLocation? LogRecordedLocation { get; init; }

    /// <summary>
    /// The start time of the log entry, in RFC 3339 format (UTC). Spec-required.
    /// </summary>
    [JsonPropertyName("logStartTime")]
    public required string LogStartTime { get; init; }

    /// <summary>Remark associated with the log entry.</summary>
    [JsonPropertyName("remark")]
    public string? Remark { get; init; }

    /// <summary>The vehicle associated with this log entry (id + name).</summary>
    [JsonPropertyName("vehicle")]
    public Samsara.Sdk.Models.Common.EntityReference? Vehicle { get; init; }
}

/// <summary>
/// Location associated with an HOS duty-status change. Mirrors the spec's
/// <c>HosLogLocation</c> schema.
/// </summary>
public sealed record HosLogLocation
{
    /// <summary>GPS latitude in degrees. Spec-required.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>GPS longitude in degrees. Spec-required.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}

/// <summary>
/// HOS violations grouped by driver — the response item of <c>GET /fleet/hos/violations</c>.
/// Mirrors the spec's <c>HosViolationsObjectResponseBody</c> shape, which nests a list
/// of individual violations per driver entry.
/// </summary>
/// <remarks>
/// Earlier SDK versions modeled this type as a single flat violation record. The
/// previous flat scalars (<c>driverId</c>, <c>vehicleId</c>, <c>violationType</c>,
/// <c>startMs</c>, <c>endMs</c>, <c>severityType</c>) are retained as nullable
/// convenience properties for backward compatibility, but the canonical spec
/// shape lives on <see cref="Violations"/>.
/// </remarks>
public sealed record HosViolation
{
    /// <summary>
    /// List of violations and their associated drivers (per spec). Spec-required.
    /// </summary>
    [JsonPropertyName("violations")]
    public required IReadOnlyList<HosViolationEntry> Violations { get; init; }
}

/// <summary>
/// A single HOS violation within a <see cref="HosViolation"/>. Mirrors the spec's
/// <c>HosViolationObjectResponseBody</c> schema.
/// </summary>
public sealed record HosViolationEntry
{
    /// <summary>The day on which the violation occurred (start and end times).</summary>
    [JsonPropertyName("day")]
    public HosViolationDay? Day { get; init; }

    /// <summary>
    /// Description containing violation type, region, and other metadata.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>The driver in violation (minified driver object with id + name).</summary>
    [JsonPropertyName("driver")]
    public HosViolationDriver? Driver { get; init; }

    /// <summary>
    /// Duration the driver was in violation, in milliseconds.
    /// </summary>
    [JsonPropertyName("durationMs")]
    public long? DurationMs { get; init; }

    /// <summary>The string value of the violation type (e.g., <c>shiftDrivingHours</c>).</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// The start time of the violation in RFC 3339 format. Spec marks REQUIRED;
    /// nullable because this is a response record.
    /// </summary>
    [JsonPropertyName("violationStartTime")]
    public string? ViolationStartTime { get; init; }
}

/// <summary>
/// The day on which an HOS violation occurred. Mirrors the spec's
/// <c>HosViolationDayObjectResponseBody</c> schema.
/// </summary>
public sealed record HosViolationDay
{
    /// <summary>
    /// The end time of the day on which the violation occurred, in RFC 3339 format.
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }

    /// <summary>
    /// The start time of the day on which the violation occurred, in RFC 3339 format.
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("startTime")]
    public required string StartTime { get; init; }
}

/// <summary>
/// The driver in an HOS violation. Mirrors the spec's
/// <c>GoaDriverTinyResponseResponseBody</c> schema (id + name + external ids).
/// </summary>
public sealed record HosViolationDriver
{
    /// <summary>ID of the driver. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Map of external IDs for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
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
/// Represents an HOS daily log summary for a driver. Mirrors the spec's
/// <c>HosDailyLogsObjectResponseBody</c>, which nests driver, distance, duty-status,
/// and metadata objects rather than emitting flat scalars.
/// </summary>
/// <remarks>
/// Earlier SDK versions modeled this type with flat scalars (<c>driverId</c>,
/// <c>vehicleId</c>, <c>distanceDrivenMeters</c>, etc.). Those scalars are
/// retained as nullable convenience properties for backward compatibility, but
/// the canonical spec shape lives on <see cref="Driver"/>, <see cref="StartTime"/>,
/// <see cref="EndTime"/>, <see cref="DistanceTraveled"/>,
/// <see cref="DutyStatusDurations"/>, <see cref="LogMetaData"/>, and
/// <see cref="PendingDutyStatusDurations"/>.
/// </remarks>
public sealed record HosDailyLog
{
    /// <summary>
    /// The driver this log applies to (nested object per spec with timezone +
    /// ELD settings). Spec-required.
    /// </summary>
    [JsonPropertyName("driver")]
    public required HosDailyLogDriver Driver { get; init; }

    /// <summary>
    /// The end time of the daily log in RFC 3339 format, calculated using the driver's
    /// timezone. Spec-required.
    /// </summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }

    /// <summary>
    /// The start time of the daily log in RFC 3339 format, calculated using the driver's
    /// timezone. Spec-required.
    /// </summary>
    [JsonPropertyName("startTime")]
    public required string StartTime { get; init; }

    /// <summary>The distance traveled information of the log (per spec).</summary>
    [JsonPropertyName("distanceTraveled")]
    public HosDailyLogDistanceTraveled? DistanceTraveled { get; init; }

    /// <summary>The currently applied duty-status durations on the driver's log.</summary>
    [JsonPropertyName("dutyStatusDurations")]
    public HosDailyLogDutyStatusDurations? DutyStatusDurations { get; init; }

    /// <summary>The metadata of the log (carrier, home terminal, certification, etc.).</summary>
    [JsonPropertyName("logMetaData")]
    public HosDailyLogMetaData? LogMetaData { get; init; }

    /// <summary>
    /// What the duty-status durations on the driver's log would be if all pending
    /// carrier edits are accepted by the driver.
    /// </summary>
    [JsonPropertyName("pendingDutyStatusDurations")]
    public HosDailyLogDutyStatusDurations? PendingDutyStatusDurations { get; init; }
}

/// <summary>
/// Driver attached to an HOS daily log. Mirrors the spec's
/// <c>DriverWithTimezoneEldSettingsObjectResponseBody</c> schema.
/// </summary>
public sealed record HosDailyLogDriver
{
    /// <summary>ID of the driver. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the driver. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Home terminal timezone (IANA timezone key, e.g. <c>America/Los_Angeles</c>).
    /// Used to interpret the log's start/end times.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    /// <summary>Map of external IDs for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>The driver's ELD settings (rulesets).</summary>
    [JsonPropertyName("eldSettings")]
    public HosDailyLogEldSettings? EldSettings { get; init; }
}

/// <summary>
/// ELD settings on an HOS daily-log driver. Mirrors the spec's
/// <c>EldSettingsObjectResponseBody</c>.
/// </summary>
public sealed record HosDailyLogEldSettings
{
    /// <summary>The driver's ELD rulesets and overrides.</summary>
    [JsonPropertyName("rulesets")]
    public IReadOnlyList<HosDailyLogDriverRuleset>? Rulesets { get; init; }
}

/// <summary>
/// A single ELD ruleset applied to a driver. Mirrors the spec's
/// <c>DriverRulesetObjectResponseBody</c>.
/// </summary>
public sealed record HosDailyLogDriverRuleset
{
    /// <summary>The rest-break setting (e.g., <c>Property (off-duty/sleeper)</c>).</summary>
    [JsonPropertyName("break")]
    public string? Break { get; init; }

    /// <summary>The cycle of the ELD ruleset (e.g., <c>USA 70 hour / 8 day</c>).</summary>
    [JsonPropertyName("cycle")]
    public string? Cycle { get; init; }

    /// <summary>
    /// The jurisdiction of the ELD ruleset (ISO 3166-2 postal code, or <c>CS</c>/<c>CN</c>
    /// for Canada South/North).
    /// </summary>
    [JsonPropertyName("jurisdiction")]
    public string? Jurisdiction { get; init; }

    /// <summary>The restart setting (e.g., <c>34-hour Restart</c>).</summary>
    [JsonPropertyName("restart")]
    public string? Restart { get; init; }

    /// <summary>The shift setting (e.g., <c>US Interstate Property</c>).</summary>
    [JsonPropertyName("shift")]
    public string? Shift { get; init; }
}

/// <summary>
/// Distance traveled summary on an HOS daily log. Mirrors the spec's
/// <c>DistanceTraveledObjectResponseBody</c>.
/// </summary>
public sealed record HosDailyLogDistanceTraveled
{
    /// <summary>Distance driven in meters, rounded to two decimal places.</summary>
    [JsonPropertyName("driveDistanceMeters")]
    public long? DriveDistanceMeters { get; init; }

    /// <summary>Personal-conveyance distance driven in meters.</summary>
    [JsonPropertyName("personalConveyanceDistanceMeters")]
    public long? PersonalConveyanceDistanceMeters { get; init; }

    /// <summary>Yard-move distance driven in meters.</summary>
    [JsonPropertyName("yardMoveDistanceMeters")]
    public long? YardMoveDistanceMeters { get; init; }
}

/// <summary>
/// Duty-status duration breakdown on an HOS daily log. Used for both the currently
/// applied durations and the pending (post-carrier-edit) durations. Mirrors the
/// spec's <c>DutyStatusDurationObjectResponseBody</c> /
/// <c>PendingDutyStatusDurationsObjectResponseBody</c>.
/// </summary>
public sealed record HosDailyLogDutyStatusDurations
{
    /// <summary>Duration the driver was active for in the log period, in milliseconds.</summary>
    [JsonPropertyName("activeDurationMs")]
    public long? ActiveDurationMs { get; init; }

    /// <summary>Duration the driver was driving, in milliseconds.</summary>
    [JsonPropertyName("driveDurationMs")]
    public long? DriveDurationMs { get; init; }

    /// <summary>Duration the driver was off duty, in milliseconds.</summary>
    [JsonPropertyName("offDutyDurationMs")]
    public long? OffDutyDurationMs { get; init; }

    /// <summary>Duration the driver was on duty (not driving), in milliseconds.</summary>
    [JsonPropertyName("onDutyDurationMs")]
    public long? OnDutyDurationMs { get; init; }

    /// <summary>Duration the driver was driving for personal conveyance, in milliseconds.</summary>
    [JsonPropertyName("personalConveyanceDurationMs")]
    public long? PersonalConveyanceDurationMs { get; init; }

    /// <summary>Duration the driver was in their sleeper berth, in milliseconds.</summary>
    [JsonPropertyName("sleeperBerthDurationMs")]
    public long? SleeperBerthDurationMs { get; init; }

    /// <summary>Duration the driver was waiting, in milliseconds.</summary>
    [JsonPropertyName("waitingTimeDurationMs")]
    public long? WaitingTimeDurationMs { get; init; }

    /// <summary>Duration the driver was driving for yard moves, in milliseconds.</summary>
    [JsonPropertyName("yardMoveDurationMs")]
    public long? YardMoveDurationMs { get; init; }
}

/// <summary>
/// Metadata for an HOS daily log (carrier info, home terminal, certifications,
/// trailers, etc.). Mirrors the spec's <c>LogMetaDataObjectResponseBody</c>.
/// </summary>
public sealed record HosDailyLogMetaData
{
    /// <summary>Whether the driver claimed the Adverse Driving Exemption.</summary>
    [JsonPropertyName("adverseDrivingClaimed")]
    public bool? AdverseDrivingClaimed { get; init; }

    /// <summary>Whether the driver claimed the Big Day (16-hour Short-Haul) Exemption.</summary>
    [JsonPropertyName("bigDayClaimed")]
    public bool? BigDayClaimed { get; init; }

    /// <summary>Carrier address used for this HOS chart.</summary>
    [JsonPropertyName("carrierFormattedAddress")]
    public string? CarrierFormattedAddress { get; init; }

    /// <summary>Carrier name used for this HOS chart.</summary>
    [JsonPropertyName("carrierName")]
    public string? CarrierName { get; init; }

    /// <summary>Carrier US DOT number used for this HOS chart.</summary>
    [JsonPropertyName("carrierUsDotNumber")]
    public long? CarrierUsDotNumber { get; init; }

    /// <summary>The time this log was certified, in RFC 3339 format.</summary>
    [JsonPropertyName("certifiedAtTime")]
    public string? CertifiedAtTime { get; init; }

    /// <summary>Home terminal address used for this HOS chart.</summary>
    [JsonPropertyName("homeTerminalFormattedAddress")]
    public string? HomeTerminalFormattedAddress { get; init; }

    /// <summary>Home terminal name used for this HOS chart.</summary>
    [JsonPropertyName("homeTerminalName")]
    public string? HomeTerminalName { get; init; }

    /// <summary>Whether this HOS day chart was certified by the driver.</summary>
    [JsonPropertyName("isCertified")]
    public bool? IsCertified { get; init; }

    /// <summary>Whether the driver has the 150 air-mile Short-Haul Exemption active.</summary>
    [JsonPropertyName("isUsShortHaulActive")]
    public bool? IsUsShortHaulActive { get; init; }

    /// <summary>
    /// List of shipping-document names associated with the driver for the day.
    /// Maps to "Shipping ID" in the Samsara dashboard.
    /// </summary>
    [JsonPropertyName("shippingDocs")]
    public string? ShippingDocs { get; init; }

    /// <summary>List of trailer names associated with the driver for the day.</summary>
    [JsonPropertyName("trailerNames")]
    public IReadOnlyList<string>? TrailerNames { get; init; }

    /// <summary>List of vehicles associated with the driver for the day.</summary>
    [JsonPropertyName("vehicles")]
    public IReadOnlyList<HosDailyLogVehicle>? Vehicles { get; init; }
}

/// <summary>
/// Vehicle entry under <see cref="HosDailyLogMetaData.Vehicles"/>. Mirrors the spec's
/// <c>VehicleResponseResponseBody</c>.
/// </summary>
public sealed record HosDailyLogVehicle
{
    /// <summary>The type of asset (e.g., <c>vehicle</c>, <c>trailer</c>).</summary>
    [JsonPropertyName("assetType")]
    public string? AssetType { get; init; }

    /// <summary>Map of external IDs for the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>License plate of the vehicle.</summary>
    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>VIN of the vehicle.</summary>
    [JsonPropertyName("vehicleVin")]
    public string? VehicleVin { get; init; }
}

/// <summary>
/// A driver's ELD event history, the response item of
/// <c>GET /beta/fleet/hos/drivers/eld-events</c>. The individual events are nested
/// under <see cref="EldEvents"/>.
/// </summary>
public sealed record HosEldEvent
{
    /// <summary>ID of the driver. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The driver's ELD activation status.</summary>
    [JsonPropertyName("driverActivationStatus")]
    public string? DriverActivationStatus { get; init; }

    /// <summary>External identifiers for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>The driver's ELD events over the requested window.</summary>
    [JsonPropertyName("eldEvents")]
    public IReadOnlyList<HosEldEventEntry>? EldEvents { get; init; }
}

/// <summary>
/// A single ELD event. Mirrors the spec's <c>HosEldEventObjectResponseBody</c>.
/// </summary>
public sealed record HosEldEventEntry
{
    /// <summary>The ELD event code. Spec-required.</summary>
    [JsonPropertyName("eldEventCode")]
    public int? EldEventCode { get; init; }

    /// <summary>The ELD event type. Spec-required.</summary>
    [JsonPropertyName("eldEventType")]
    public int? EldEventType { get; init; }

    /// <summary>Time of the event, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")]
    public string? Time { get; init; }

    /// <summary>The record origin of the event.</summary>
    [JsonPropertyName("eldEventRecordOrigin")]
    public int? EldEventRecordOrigin { get; init; }

    /// <summary>The record status of the event.</summary>
    [JsonPropertyName("eldEventRecordStatus")]
    public int? EldEventRecordStatus { get; init; }

    /// <summary>Malfunction/diagnostic code, if any.</summary>
    [JsonPropertyName("malfunctionDiagnosticCode")]
    public string? MalfunctionDiagnosticCode { get; init; }

    /// <summary>Accumulated vehicle distance, in meters.</summary>
    [JsonPropertyName("accumulatedVehicleMeters")]
    public long? AccumulatedVehicleMeters { get; init; }

    /// <summary>Total vehicle distance, in meters.</summary>
    [JsonPropertyName("totalVehicleMeters")]
    public long? TotalVehicleMeters { get; init; }

    /// <summary>Elapsed engine hours.</summary>
    [JsonPropertyName("elapsedEngineHours")]
    public double? ElapsedEngineHours { get; init; }

    /// <summary>Total engine hours.</summary>
    [JsonPropertyName("totalEngineHours")]
    public double? TotalEngineHours { get; init; }

    /// <summary>Location associated with the event.</summary>
    [JsonPropertyName("location")]
    public HosEldEventLocation? Location { get; init; }

    /// <summary>Driver-entered remark associated with the event.</summary>
    [JsonPropertyName("remark")]
    public HosEldEventRemark? Remark { get; init; }

    /// <summary>The vehicle associated with the event.</summary>
    [JsonPropertyName("vehicle")]
    public Samsara.Sdk.Models.Common.EntityReference? Vehicle { get; init; }
}

/// <summary>
/// Location of an ELD event. Mirrors the spec's
/// <c>HosEldEventLocationObjectResponseBody</c>.
/// </summary>
public sealed record HosEldEventLocation
{
    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>City of the event.</summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>State of the event.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>ELD-reported location description.</summary>
    [JsonPropertyName("eldLocation")]
    public string? EldLocation { get; init; }
}

/// <summary>
/// Driver-entered remark on an ELD event. Mirrors the spec's
/// <c>HosEldEventRemarkObjectResponseBody</c>.
/// </summary>
public sealed record HosEldEventRemark
{
    /// <summary>The remark comment. Spec-required.</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    /// <summary>Location description for the remark. Spec-required.</summary>
    [JsonPropertyName("locationDescription")]
    public string? LocationDescription { get; init; }

    /// <summary>Time of the remark, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")]
    public string? Time { get; init; }
}
