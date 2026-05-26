namespace Samsara.Sdk.Models.Industrial;

using System.Text.Json.Serialization;

// Samsara v1 sensors API — all endpoints are POST under /v1/sensors/* and use long ids.

/// <summary>Basic sensor descriptor returned by <c>POST /v1/sensors/list</c>.</summary>
public sealed record V1Sensor
{
    [JsonPropertyName("id")]
    public required long Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("macAddress")]
    public string? MacAddress { get; init; }
}

/// <summary>Response wrapper for <c>POST /v1/sensors/list</c>.</summary>
public sealed record V1SensorListResponse
{
    [JsonPropertyName("sensors")]
    public IReadOnlyList<V1Sensor>? Sensors { get; init; }
}

/// <summary>Request body for the per-reading endpoints (cargo/door/humidity/temperature).</summary>
public sealed record V1SensorReadingsRequest
{
    [JsonPropertyName("sensors")]
    public required IReadOnlyList<long> Sensors { get; init; }
}

/// <summary>Request body for <c>POST /v1/sensors/history</c>.</summary>
public sealed record V1SensorHistoryRequest
{
    [JsonPropertyName("startMs")]
    public required long StartMs { get; init; }

    [JsonPropertyName("endMs")]
    public required long EndMs { get; init; }

    [JsonPropertyName("series")]
    public required IReadOnlyList<V1SensorHistorySeries> Series { get; init; }

    [JsonPropertyName("fillMissing")]
    public string? FillMissing { get; init; }
}

/// <summary>A single sensor/field series request.</summary>
public sealed record V1SensorHistorySeries
{
    [JsonPropertyName("field")]
    public required string Field { get; init; }

    [JsonPropertyName("sensorId")]
    public required long SensorId { get; init; }

    [JsonPropertyName("widgetField")]
    public string? WidgetField { get; init; }
}

/// <summary>One timestamped row from <c>POST /v1/sensors/history</c>.</summary>
public sealed record V1SensorHistoryDataPoint
{
    [JsonPropertyName("timeMs")]
    public long? TimeMs { get; init; }

    [JsonPropertyName("series")]
    public IReadOnlyList<long>? Series { get; init; }
}

/// <summary>Response wrapper for <c>POST /v1/sensors/history</c>.</summary>
public sealed record V1SensorHistoryResponse
{
    [JsonPropertyName("results")]
    public IReadOnlyList<V1SensorHistoryDataPoint>? Results { get; init; }
}

/// <summary>A single temperature reading.</summary>
public sealed record V1TemperatureReading
{
    [JsonPropertyName("id")] public required long Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("ambientTemperature")] public long? AmbientTemperature { get; init; }
    [JsonPropertyName("ambientTemperatureTime")] public DateTimeOffset? AmbientTemperatureTime { get; init; }
    [JsonPropertyName("probeTemperature")] public long? ProbeTemperature { get; init; }
    [JsonPropertyName("probeTemperatureTime")] public DateTimeOffset? ProbeTemperatureTime { get; init; }
    [JsonPropertyName("trailerId")] public long? TrailerId { get; init; }
    [JsonPropertyName("vehicleId")] public long? VehicleId { get; init; }
}

/// <summary>A single door status reading.</summary>
public sealed record V1DoorReading
{
    [JsonPropertyName("id")] public required long Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("doorClosed")] public bool? DoorClosed { get; init; }
    [JsonPropertyName("doorStatusTime")] public DateTimeOffset? DoorStatusTime { get; init; }
    [JsonPropertyName("trailerId")] public long? TrailerId { get; init; }
    [JsonPropertyName("vehicleId")] public long? VehicleId { get; init; }
}

/// <summary>A single humidity reading.</summary>
public sealed record V1HumidityReading
{
    [JsonPropertyName("id")] public required long Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("humidity")] public long? Humidity { get; init; }
    [JsonPropertyName("humidityTime")] public DateTimeOffset? HumidityTime { get; init; }
    [JsonPropertyName("trailerId")] public long? TrailerId { get; init; }
    [JsonPropertyName("vehicleId")] public long? VehicleId { get; init; }
}

/// <summary>A single cargo status reading.</summary>
public sealed record V1CargoReading
{
    [JsonPropertyName("id")] public required long Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("cargoEmpty")] public bool? CargoEmpty { get; init; }
    [JsonPropertyName("cargoStatusTime")] public DateTimeOffset? CargoStatusTime { get; init; }
    [JsonPropertyName("redEyeDistance")] public long? RedEyeDistance { get; init; }
    [JsonPropertyName("trailerId")] public long? TrailerId { get; init; }
    [JsonPropertyName("vehicleId")] public long? VehicleId { get; init; }
}

/// <summary>Generic per-type wrapper around the <c>{ groupId, sensors[] }</c> response shape.</summary>
public sealed record V1SensorReadingsResponse<T>
{
    [JsonPropertyName("groupId")]
    public long? GroupId { get; init; }

    [JsonPropertyName("sensors")]
    public IReadOnlyList<T>? Sensors { get; init; }
}
