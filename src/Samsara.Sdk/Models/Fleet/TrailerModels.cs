namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

public sealed record Trailer
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Custom attributes associated with the trailer (spec schema
    /// <c>GoaAttributeTinyResponseBody</c>). Returned by the by-id, create, and
    /// update endpoints (the <c>GET /fleet/trailers</c> list response omits it).</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<Common.AttributeTiny>? Attributes { get; init; }

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
}

public sealed record CreateTrailerRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Custom attributes to set on the trailer (spec schema
    /// <c>GoaAttributeTinyRequestBody</c>).</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<Common.AttributeTiny>? Attributes { get; init; }

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
}

public sealed record UpdateTrailerRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Custom attributes to set on the trailer (spec schema
    /// <c>GoaAttributeTinyRequestBody</c>).</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<Common.AttributeTiny>? Attributes { get; init; }

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
}

/// <summary>
/// Trailer statistics snapshot, returned by <c>GET /fleet/trailers/stats</c>
/// (<c>ITrailersClient.GetStatsSnapshotAsync</c>). Each metric is the single
/// most-recent sample. The time-series feed/history endpoints return arrays of
/// samples instead; see <see cref="TrailerStatsSample"/>.
/// </summary>
public sealed record TrailerStats
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }

    /// <summary>Overall carrier reefer state.</summary>
    [JsonPropertyName("carrierReeferState")] public TrailerStatReeferState? CarrierReeferState { get; init; }

    /// <summary>GPS reading.</summary>
    [JsonPropertyName("gps")] public TrailerStatGps? Gps { get; init; }

    /// <summary>GPS-derived odometer, in meters.</summary>
    [JsonPropertyName("gpsOdometerMeters")] public TrailerStatValue? GpsOdometerMeters { get; init; }

    /// <summary>Alarms emitted by the reefer.</summary>
    [JsonPropertyName("reeferAlarms")] public TrailerStatReeferAlarms? ReeferAlarms { get; init; }

    /// <summary>Reefer ambient air temperature, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferAmbientAirTemperatureMilliC")] public TrailerStatValue? ReeferAmbientAirTemperatureMilliC { get; init; }

    /// <summary>Reefer door state for zone 1 (<c>open</c> / <c>closed</c>).</summary>
    [JsonPropertyName("reeferDoorStateZone1")] public TrailerStatStringValue? ReeferDoorStateZone1 { get; init; }

    /// <summary>Reefer door state for zone 2 (<c>open</c> / <c>closed</c>).</summary>
    [JsonPropertyName("reeferDoorStateZone2")] public TrailerStatStringValue? ReeferDoorStateZone2 { get; init; }

    /// <summary>Reefer door state for zone 3 (<c>open</c> / <c>closed</c>).</summary>
    [JsonPropertyName("reeferDoorStateZone3")] public TrailerStatStringValue? ReeferDoorStateZone3 { get; init; }

    /// <summary>Reefer fuel level, as a percentage.</summary>
    [JsonPropertyName("reeferFuelPercent")] public TrailerStatValue? ReeferFuelPercent { get; init; }

    /// <summary>Reefer OBD-reported engine seconds.</summary>
    [JsonPropertyName("reeferObdEngineSeconds")] public TrailerStatValue? ReeferObdEngineSeconds { get; init; }

    /// <summary>Reefer return air temperature for zone 1, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone1")] public TrailerStatValue? ReeferReturnAirTemperatureMilliCZone1 { get; init; }

    /// <summary>Reefer return air temperature for zone 2, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone2")] public TrailerStatValue? ReeferReturnAirTemperatureMilliCZone2 { get; init; }

    /// <summary>Reefer return air temperature for zone 3, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone3")] public TrailerStatValue? ReeferReturnAirTemperatureMilliCZone3 { get; init; }

    /// <summary>Reefer run mode.</summary>
    [JsonPropertyName("reeferRunMode")] public TrailerStatStringValue? ReeferRunMode { get; init; }

    /// <summary>Reefer set-point temperature for zone 1, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone1")] public TrailerStatValue? ReeferSetPointTemperatureMilliCZone1 { get; init; }

    /// <summary>Reefer set-point temperature for zone 2, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone2")] public TrailerStatValue? ReeferSetPointTemperatureMilliCZone2 { get; init; }

    /// <summary>Reefer set-point temperature for zone 3, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone3")] public TrailerStatValue? ReeferSetPointTemperatureMilliCZone3 { get; init; }

    /// <summary>Reefer state for zone 1.</summary>
    [JsonPropertyName("reeferStateZone1")] public TrailerStatReeferState? ReeferStateZone1 { get; init; }

    /// <summary>Reefer state for zone 2.</summary>
    [JsonPropertyName("reeferStateZone2")] public TrailerStatReeferState? ReeferStateZone2 { get; init; }

    /// <summary>Reefer state for zone 3.</summary>
    [JsonPropertyName("reeferStateZone3")] public TrailerStatReeferState? ReeferStateZone3 { get; init; }

    /// <summary>Reefer supply air temperature for zone 1, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone1")] public TrailerStatValue? ReeferSupplyAirTemperatureMilliCZone1 { get; init; }

    /// <summary>Reefer supply air temperature for zone 2, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone2")] public TrailerStatValue? ReeferSupplyAirTemperatureMilliCZone2 { get; init; }

    /// <summary>Reefer supply air temperature for zone 3, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone3")] public TrailerStatValue? ReeferSupplyAirTemperatureMilliCZone3 { get; init; }
}

/// <summary>
/// Trailer statistics time-series row, returned by
/// <c>GET /fleet/trailers/stats/feed</c> (<c>ITrailersClient.GetStatsFeedAsync</c>)
/// and <c>GET /fleet/trailers/stats/history</c> (<c>ITrailersClient.GetStatsHistoryAsync</c>).
/// Each metric is an array of samples covering the requested window. The snapshot
/// endpoint returns single values instead; see <see cref="TrailerStats"/>.
/// </summary>
public sealed record TrailerStatsSample
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }

    /// <summary>Carrier reefer state samples.</summary>
    [JsonPropertyName("carrierReeferState")] public IReadOnlyList<TrailerStatReeferState>? CarrierReeferState { get; init; }

    /// <summary>GPS reading samples.</summary>
    [JsonPropertyName("gps")] public IReadOnlyList<TrailerStatGps>? Gps { get; init; }

    /// <summary>GPS-derived odometer samples, in meters.</summary>
    [JsonPropertyName("gpsOdometerMeters")] public IReadOnlyList<TrailerStatValue>? GpsOdometerMeters { get; init; }

    /// <summary>Reefer alarm samples.</summary>
    [JsonPropertyName("reeferAlarms")] public IReadOnlyList<TrailerStatReeferAlarms>? ReeferAlarms { get; init; }

    /// <summary>Reefer ambient air temperature samples, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferAmbientAirTemperatureMilliC")] public IReadOnlyList<TrailerStatValue>? ReeferAmbientAirTemperatureMilliC { get; init; }

    /// <summary>Reefer door state samples for zone 1.</summary>
    [JsonPropertyName("reeferDoorStateZone1")] public IReadOnlyList<TrailerStatStringValue>? ReeferDoorStateZone1 { get; init; }

    /// <summary>Reefer door state samples for zone 2.</summary>
    [JsonPropertyName("reeferDoorStateZone2")] public IReadOnlyList<TrailerStatStringValue>? ReeferDoorStateZone2 { get; init; }

    /// <summary>Reefer door state samples for zone 3.</summary>
    [JsonPropertyName("reeferDoorStateZone3")] public IReadOnlyList<TrailerStatStringValue>? ReeferDoorStateZone3 { get; init; }

    /// <summary>Reefer fuel level samples, as a percentage.</summary>
    [JsonPropertyName("reeferFuelPercent")] public IReadOnlyList<TrailerStatValue>? ReeferFuelPercent { get; init; }

    /// <summary>Reefer OBD-reported engine seconds samples.</summary>
    [JsonPropertyName("reeferObdEngineSeconds")] public IReadOnlyList<TrailerStatValue>? ReeferObdEngineSeconds { get; init; }

    /// <summary>Reefer return air temperature samples for zone 1, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone1")] public IReadOnlyList<TrailerStatValue>? ReeferReturnAirTemperatureMilliCZone1 { get; init; }

    /// <summary>Reefer return air temperature samples for zone 2, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone2")] public IReadOnlyList<TrailerStatValue>? ReeferReturnAirTemperatureMilliCZone2 { get; init; }

    /// <summary>Reefer return air temperature samples for zone 3, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferReturnAirTemperatureMilliCZone3")] public IReadOnlyList<TrailerStatValue>? ReeferReturnAirTemperatureMilliCZone3 { get; init; }

    /// <summary>Reefer run mode samples.</summary>
    [JsonPropertyName("reeferRunMode")] public IReadOnlyList<TrailerStatStringValue>? ReeferRunMode { get; init; }

    /// <summary>Reefer set-point temperature samples for zone 1, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone1")] public IReadOnlyList<TrailerStatValue>? ReeferSetPointTemperatureMilliCZone1 { get; init; }

    /// <summary>Reefer set-point temperature samples for zone 2, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone2")] public IReadOnlyList<TrailerStatValue>? ReeferSetPointTemperatureMilliCZone2 { get; init; }

    /// <summary>Reefer set-point temperature samples for zone 3, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSetPointTemperatureMilliCZone3")] public IReadOnlyList<TrailerStatValue>? ReeferSetPointTemperatureMilliCZone3 { get; init; }

    /// <summary>Reefer state samples for zone 1.</summary>
    [JsonPropertyName("reeferStateZone1")] public IReadOnlyList<TrailerStatReeferState>? ReeferStateZone1 { get; init; }

    /// <summary>Reefer state samples for zone 2.</summary>
    [JsonPropertyName("reeferStateZone2")] public IReadOnlyList<TrailerStatReeferState>? ReeferStateZone2 { get; init; }

    /// <summary>Reefer state samples for zone 3.</summary>
    [JsonPropertyName("reeferStateZone3")] public IReadOnlyList<TrailerStatReeferState>? ReeferStateZone3 { get; init; }

    /// <summary>Reefer supply air temperature samples for zone 1, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone1")] public IReadOnlyList<TrailerStatValue>? ReeferSupplyAirTemperatureMilliCZone1 { get; init; }

    /// <summary>Reefer supply air temperature samples for zone 2, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone2")] public IReadOnlyList<TrailerStatValue>? ReeferSupplyAirTemperatureMilliCZone2 { get; init; }

    /// <summary>Reefer supply air temperature samples for zone 3, in milli-degrees Celsius.</summary>
    [JsonPropertyName("reeferSupplyAirTemperatureMilliCZone3")] public IReadOnlyList<TrailerStatValue>? ReeferSupplyAirTemperatureMilliCZone3 { get; init; }
}

/// <summary>
/// A single integer-valued trailer statistic sample (<c>{ time, value }</c>),
/// shared by the trailer stats snapshot and feed/history endpoints.
/// </summary>
public sealed record TrailerStatValue
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The measured value. Spec-required.</summary>
    [JsonPropertyName("value")] public required long Value { get; init; }
}

/// <summary>
/// A single string- or enum-valued trailer statistic sample
/// (<c>{ time, value }</c>), e.g. a reefer door state or run mode. The value is
/// exposed as a string to remain forward-compatible with new enum members.
/// </summary>
public sealed record TrailerStatStringValue
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The measured value. Spec-required.</summary>
    [JsonPropertyName("value")] public required string Value { get; init; }
}

/// <summary>
/// A reefer-state sample. Carries the overall state and an optional substate
/// (e.g. <c>Pretrip</c>, <c>Defrost</c>) for multi-zone carrier reefers.
/// </summary>
public sealed record TrailerStatReeferState
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The overall reefer state (e.g. <c>Off</c>, <c>On</c>). Spec-required.</summary>
    [JsonPropertyName("value")] public required string Value { get; init; }

    /// <summary>The reefer substate, if available (e.g. <c>Pretrip</c>, <c>Defrost</c>).</summary>
    [JsonPropertyName("substateValue")] public string? SubstateValue { get; init; }
}

/// <summary>
/// A GPS reading sample on a trailer stats response. Mirrors the spec's
/// <c>TrailerStatGps</c> schema. Note that heading and speed are integer-valued
/// here, unlike the vehicle stats GPS shape.
/// </summary>
public sealed record TrailerStatGps
{
    /// <summary>Latitude in degrees. Spec-required.</summary>
    [JsonPropertyName("latitude")] public required double Latitude { get; init; }

    /// <summary>Longitude in degrees. Spec-required.</summary>
    [JsonPropertyName("longitude")] public required double Longitude { get; init; }

    /// <summary>Timestamp of the reading, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>Heading of the trailer, in degrees.</summary>
    [JsonPropertyName("headingDegrees")] public long? HeadingDegrees { get; init; }

    /// <summary>Speed of the trailer, in miles per hour.</summary>
    [JsonPropertyName("speedMilesPerHour")] public long? SpeedMilesPerHour { get; init; }

    /// <summary>Reverse-geocoded address for the reading.</summary>
    [JsonPropertyName("reverseGeo")] public ReverseGeo? ReverseGeo { get; init; }
}

/// <summary>
/// A reefer-alarm sample. Carries the set of alarms reported by the reefer at a
/// point in time.
/// </summary>
public sealed record TrailerStatReeferAlarms
{
    /// <summary>Timestamp of the sample, in RFC 3339 format. Spec-required.</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The alarms reported by the reefer. Spec-required.</summary>
    [JsonPropertyName("alarms")] public required IReadOnlyList<TrailerStatReeferAlarm> Alarms { get; init; }
}

/// <summary>A single reefer alarm on a <see cref="TrailerStatReeferAlarms"/> sample.</summary>
public sealed record TrailerStatReeferAlarm
{
    /// <summary>The ID of the alarm. Spec-required.</summary>
    [JsonPropertyName("alarmCode")] public required string AlarmCode { get; init; }

    /// <summary>The description of the alarm. Spec-required.</summary>
    [JsonPropertyName("description")] public required string Description { get; init; }

    /// <summary>The recommended operator action. Spec-required.</summary>
    [JsonPropertyName("operatorAction")] public required string OperatorAction { get; init; }

    /// <summary>The severity of the alarm (<c>1</c>: ok to run, <c>2</c>: check as specified,
    /// <c>3</c>: take immediate action). Spec-required.</summary>
    [JsonPropertyName("severity")] public required long Severity { get; init; }
}
