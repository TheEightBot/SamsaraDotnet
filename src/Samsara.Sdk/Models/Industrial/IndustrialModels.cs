namespace Samsara.Sdk.Models.Industrial;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an industrial sensor or data point.
/// </summary>
public sealed record IndustrialAsset
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("macAddress")]
    public string? MacAddress { get; init; }

    [JsonPropertyName("dataInputs")]
    public IReadOnlyList<DataInput>? DataInputs { get; init; }
}

/// <summary>
/// A data input for an industrial asset / sensor.
/// </summary>
public sealed record DataInput
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("points")]
    public IReadOnlyList<DataPoint>? Points { get; init; }
}

/// <summary>
/// A time-series data point for an industrial data input.
/// </summary>
public sealed record DataPoint
{
    [JsonPropertyName("timeMs")]
    public long? TimeMs { get; init; }

    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// Machine history entry.
/// </summary>
public sealed record MachineHistoryEntry
{
    [JsonPropertyName("machineId")]
    public string? MachineId { get; init; }

    [JsonPropertyName("vibrations")]
    public IReadOnlyList<MachineVibration>? Vibrations { get; init; }
}

/// <summary>
/// A vibration reading for a machine.
/// </summary>
public sealed record MachineVibration
{
    [JsonPropertyName("x")]
    public double? X { get; init; }

    [JsonPropertyName("y")]
    public double? Y { get; init; }

    [JsonPropertyName("z")]
    public double? Z { get; init; }

    [JsonPropertyName("time")]
    public long? Time { get; init; }
}
