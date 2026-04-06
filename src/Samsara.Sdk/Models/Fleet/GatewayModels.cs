namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

public sealed record Gateway
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("mainBus")]
    public string? MainBus { get; init; }

    [JsonPropertyName("firmwareVersion")]
    public string? FirmwareVersion { get; init; }

    [JsonPropertyName("wifiMacAddress")]
    public string? WifiMacAddress { get; init; }

    [JsonPropertyName("simCardId")]
    public string? SimCardId { get; init; }

    [JsonPropertyName("vehicle")]
    public DriverReference? Vehicle { get; init; }

    [JsonPropertyName("asset")]
    public DriverReference? Asset { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<Common.TagReference>? Tags { get; init; }
}
