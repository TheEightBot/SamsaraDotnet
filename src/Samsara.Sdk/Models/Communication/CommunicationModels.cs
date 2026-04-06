namespace Samsara.Sdk.Models.Communication;

using System.Text.Json.Serialization;

/// <summary>
/// A message sent or received through the Samsara system.
/// </summary>
public sealed record DriverMessage
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("senderType")]
    public string? SenderType { get; init; }

    [JsonPropertyName("body")]
    public string? Body { get; init; }

    [JsonPropertyName("sentAtMs")]
    public long? SentAtMs { get; init; }

    [JsonPropertyName("readAtMs")]
    public long? ReadAtMs { get; init; }
}

/// <summary>
/// Request body for sending a message to a driver.
/// </summary>
public sealed record SendDriverMessageRequest
{
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    [JsonPropertyName("body")]
    public required string Body { get; init; }
}

/// <summary>
/// Represents a contact in the Samsara system.
/// </summary>
public sealed record Contact
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; init; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }
}

/// <summary>
/// Represents an alert in the Samsara system.
/// </summary>
public sealed record Alert
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("configurationId")]
    public string? ConfigurationId { get; init; }

    [JsonPropertyName("resolvedAtTime")]
    public DateTimeOffset? ResolvedAtTime { get; init; }

    [JsonPropertyName("triggeredAtTime")]
    public DateTimeOffset? TriggeredAtTime { get; init; }

    [JsonPropertyName("conditionName")]
    public string? ConditionName { get; init; }

    [JsonPropertyName("vehicle")]
    public AlertVehicle? Vehicle { get; init; }

    [JsonPropertyName("driver")]
    public AlertDriver? Driver { get; init; }
}

/// <summary>
/// Vehicle reference in an alert.
/// </summary>
public sealed record AlertVehicle
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Driver reference in an alert.
/// </summary>
public sealed record AlertDriver
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Alert configuration (rule).
/// </summary>
public sealed record AlertConfiguration
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("conditionType")]
    public string? ConditionType { get; init; }
}
