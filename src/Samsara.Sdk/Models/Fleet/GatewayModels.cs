namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a Samsara gateway, returned by <c>GET /gateways</c> and <c>POST /gateways</c>.
/// </summary>
public sealed record Gateway
{
    /// <summary>The model of the gateway installed on the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("model")]
    public required string Model { get; init; }

    /// <summary>The serial number of the gateway installed on the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("serial")]
    public required string Serial { get; init; }

    /// <summary>Accessory devices connected to the gateway.</summary>
    [JsonPropertyName("accessoryDevices")]
    public IReadOnlyList<GatewayAccessoryDevice>? AccessoryDevices { get; init; }

    /// <summary>The asset the gateway is installed on (id + externalIds in the spec).</summary>
    [JsonPropertyName("asset")]
    public DriverReference? Asset { get; init; }

    /// <summary>Connectivity status of the gateway (health + last-connected timestamp).</summary>
    [JsonPropertyName("connectionStatus")]
    public GatewayConnectionStatus? ConnectionStatus { get; init; }

    /// <summary>Gateway data usage over the trailing 30 days.</summary>
    [JsonPropertyName("dataUsageLast30Days")]
    public GatewayDataUsage? DataUsageLast30Days { get; init; }
}

/// <summary>
/// Accessory device connected to a Samsara VG gateway (e.g. an EM-series device).
/// </summary>
public sealed record GatewayAccessoryDevice
{
    /// <summary>Product model name of the accessory device (e.g. <c>EM11</c>).</summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    /// <summary>Serial number of the accessory device.</summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }
}

/// <summary>
/// Connectivity status of a Samsara gateway.
/// </summary>
public sealed record GatewayConnectionStatus
{
    /// <summary>Most recent gateway health status (e.g. <c>Connected</c>, <c>Unplugged</c>).</summary>
    [JsonPropertyName("healthStatus")]
    public string? HealthStatus { get; init; }

    /// <summary>The last time the gateway was connected in RFC 3339 format.</summary>
    [JsonPropertyName("lastConnected")]
    public string? LastConnected { get; init; }
}

/// <summary>
/// Gateway data usage in bytes over the trailing 30 days.
/// </summary>
public sealed record GatewayDataUsage
{
    /// <summary>Cellular data usage in bytes.</summary>
    [JsonPropertyName("cellularDataUsageBytes")]
    public long? CellularDataUsageBytes { get; init; }

    /// <summary>Wi-Fi hotspot data usage in bytes.</summary>
    [JsonPropertyName("hotspotUsageBytes")]
    public long? HotspotUsageBytes { get; init; }
}

/// <summary>Request body for <c>POST /gateways</c>.</summary>
public sealed record CreateGatewayRequest
{
    [JsonPropertyName("serial")] public required string Serial { get; init; }
}
