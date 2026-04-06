namespace Samsara.Sdk.Models.Industrial;

using System.Text.Json.Serialization;

public sealed record Sensor
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("macAddress")]
    public string? MacAddress { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("serialNumber")]
    public string? SerialNumber { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<Common.TagReference>? Tags { get; init; }
}

public sealed record SensorHistoryResult
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("series")]
    public IReadOnlyList<SensorDataPoint>? Series { get; init; }
}

public sealed record SensorDataPoint
{
    [JsonPropertyName("time")]
    public string? Time { get; init; }

    [JsonPropertyName("temperature")]
    public double? Temperature { get; init; }

    [JsonPropertyName("humidity")]
    public double? Humidity { get; init; }

    [JsonPropertyName("doorClosed")]
    public bool? DoorClosed { get; init; }

    [JsonPropertyName("cargoEmpty")]
    public bool? CargoEmpty { get; init; }
}
