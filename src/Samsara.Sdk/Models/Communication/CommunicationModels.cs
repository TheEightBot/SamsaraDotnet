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

/// <summary>
/// Request body for <c>POST /alerts/configurations</c>. The spec requires
/// <c>name</c>, <c>isEnabled</c>, <c>scope</c>, <c>actions</c>, and <c>triggers</c>.
/// </summary>
public sealed record CreateAlertConfigurationRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("isEnabled")]
    public required bool IsEnabled { get; init; }

    /// <summary>Configuration scope (assets/tags/etc.) — see Samsara API docs.</summary>
    [JsonPropertyName("scope")]
    public required object Scope { get; init; }

    /// <summary>Trigger conditions — see Samsara API docs.</summary>
    [JsonPropertyName("triggers")]
    public required IReadOnlyList<object> Triggers { get; init; }

    /// <summary>Notification actions (webhook/email/SMS) — see Samsara API docs.</summary>
    [JsonPropertyName("actions")]
    public required IReadOnlyList<object> Actions { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("operationalSettings")]
    public object? OperationalSettings { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /alerts/configurations</c>. The id is sent here, not in the URL.
/// </summary>
public sealed record UpdateAlertConfigurationRequest
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("isEnabled")]
    public bool? IsEnabled { get; init; }

    [JsonPropertyName("scope")]
    public object? Scope { get; init; }

    [JsonPropertyName("triggers")]
    public IReadOnlyList<object>? Triggers { get; init; }

    [JsonPropertyName("actions")]
    public IReadOnlyList<object>? Actions { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("operationalSettings")]
    public object? OperationalSettings { get; init; }
}

/// <summary>
/// Notification setting for an alert configuration.
/// </summary>
public sealed record AlertNotificationSetting
{
    [JsonPropertyName("contactId")]
    public string? ContactId { get; init; }

    [JsonPropertyName("notificationType")]
    public string? NotificationType { get; init; }
}

/// <summary>
/// Represents an alert incident (a triggered alert event).
/// </summary>
public sealed record AlertIncident
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("alertId")]
    public string? AlertId { get; init; }

    [JsonPropertyName("configurationId")]
    public string? ConfigurationId { get; init; }

    [JsonPropertyName("triggeredAtTime")]
    public DateTimeOffset? TriggeredAtTime { get; init; }

    [JsonPropertyName("resolvedAtTime")]
    public DateTimeOffset? ResolvedAtTime { get; init; }

    [JsonPropertyName("vehicle")]
    public AlertVehicle? Vehicle { get; init; }

    [JsonPropertyName("driver")]
    public AlertDriver? Driver { get; init; }
}
