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
    public required string FirstName { get; init; }

    [JsonPropertyName("lastName")]
    public required string LastName { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("phone")]
    public required string Phone { get; init; }
}

/// <summary>
/// Request body for <c>POST /contacts</c>. Per spec, all fields are optional
/// (the schema declares no <c>required</c> properties).
/// </summary>
public sealed record CreateContactRequest
{
    [JsonPropertyName("firstName")] public string? FirstName { get; init; }
    [JsonPropertyName("lastName")] public string? LastName { get; init; }
    [JsonPropertyName("email")] public string? Email { get; init; }
    [JsonPropertyName("phone")] public string? Phone { get; init; }
}

/// <summary>Request body for <c>PATCH /contacts/{id}</c>.</summary>
public sealed record UpdateContactRequest
{
    [JsonPropertyName("firstName")] public string? FirstName { get; init; }
    [JsonPropertyName("lastName")] public string? LastName { get; init; }
    [JsonPropertyName("email")] public string? Email { get; init; }
    [JsonPropertyName("phone")] public string? Phone { get; init; }
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
/// Alert configuration (rule). Spec marks
/// <c>id</c>, <c>name</c>, <c>isEnabled</c>, <c>scope</c>, <c>actions</c>,
/// <c>triggers</c>, <c>createdAtTime</c>, and <c>lastModifiedAtTime</c> as REQUIRED.
/// </summary>
public sealed record AlertConfiguration
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("isEnabled")]
    public required bool IsEnabled { get; init; }

    /// <summary>Configuration scope (assets/tags/etc.). See <see cref="AlertScope"/>.</summary>
    [JsonPropertyName("scope")]
    public required AlertScope Scope { get; init; }

    /// <summary>Trigger conditions. Each item follows the spec's <c>WorkflowTriggerObject</c> shape.</summary>
    [JsonPropertyName("triggers")]
    public required IReadOnlyList<AlertTrigger> Triggers { get; init; }

    /// <summary>Notification actions (webhook/email/SMS). Each item follows the spec's <c>ActionObject</c> shape.</summary>
    [JsonPropertyName("actions")]
    public required IReadOnlyList<AlertAction> Actions { get; init; }

    [JsonPropertyName("createdAtTime")]
    public required DateTimeOffset CreatedAtTime { get; init; }

    [JsonPropertyName("lastModifiedAtTime")]
    public required DateTimeOffset LastModifiedAtTime { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Operational time-range settings (when the alert is active). See <see cref="AlertOperationalSettings"/>.</summary>
    [JsonPropertyName("operationalSettings")]
    public AlertOperationalSettings? OperationalSettings { get; init; }
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

    /// <summary>Configuration scope (assets/tags/etc.). See <see cref="AlertScope"/>.</summary>
    [JsonPropertyName("scope")]
    public required AlertScope Scope { get; init; }

    /// <summary>Trigger conditions. Each item follows the spec's <c>WorkflowTriggerObject</c> shape.</summary>
    [JsonPropertyName("triggers")]
    public required IReadOnlyList<AlertTrigger> Triggers { get; init; }

    /// <summary>Notification actions (webhook/email/SMS). Each item follows the spec's <c>ActionObject</c> shape.</summary>
    [JsonPropertyName("actions")]
    public required IReadOnlyList<AlertAction> Actions { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Operational time-range settings (when the alert is active). See <see cref="AlertOperationalSettings"/>.</summary>
    [JsonPropertyName("operationalSettings")]
    public AlertOperationalSettings? OperationalSettings { get; init; }
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

    /// <summary>Configuration scope (assets/tags/etc.). See <see cref="AlertScope"/>.</summary>
    [JsonPropertyName("scope")]
    public AlertScope? Scope { get; init; }

    [JsonPropertyName("triggers")]
    public IReadOnlyList<AlertTrigger>? Triggers { get; init; }

    [JsonPropertyName("actions")]
    public IReadOnlyList<AlertAction>? Actions { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Operational time-range settings (when the alert is active). See <see cref="AlertOperationalSettings"/>.</summary>
    [JsonPropertyName("operationalSettings")]
    public AlertOperationalSettings? OperationalSettings { get; init; }
}

/// <summary>
/// Scope of an alert configuration — the objects the triggers apply to. Spec marks
/// <c>all</c> as REQUIRED. The asset/driver/tag/widget arrays use the spec's "tiny"
/// reference shapes (id+name); they are exposed as <see cref="object"/> here so callers
/// can pass either an anonymous object or a typed reference without coupling this
/// model to dozens of dependent schemas.
/// </summary>
public sealed record AlertScope
{
    /// <summary>Whether the scope applies to all applicable objects.</summary>
    [JsonPropertyName("all")]
    public required bool All { get; init; }

    [JsonPropertyName("assets")]
    public IReadOnlyList<object>? Assets { get; init; }

    [JsonPropertyName("drivers")]
    public IReadOnlyList<object>? Drivers { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<object>? Tags { get; init; }

    [JsonPropertyName("widgets")]
    public IReadOnlyList<object>? Widgets { get; init; }
}

/// <summary>
/// A single trigger inside an alert configuration. Spec marks <c>triggerTypeId</c>
/// as REQUIRED. <c>triggerParams</c> is left as a weak <see cref="object"/> because the
/// spec defines hundreds of trigger-type-specific param shapes (see the Samsara
/// <c>TriggerParamsObject</c> schemas).
/// </summary>
public sealed record AlertTrigger
{
    /// <summary>Trigger type id — see Samsara API docs for the full list (e.g. 1000 = Vehicle Speed).</summary>
    [JsonPropertyName("triggerTypeId")]
    public required int TriggerTypeId { get; init; }

    [JsonPropertyName("triggerParams")]
    public object? TriggerParams { get; init; }
}

/// <summary>
/// A single action inside an alert configuration. Spec marks <c>actionTypeId</c> as
/// REQUIRED. <c>actionParams</c> is left as a weak <see cref="object"/> because the spec
/// defines many action-type-specific param shapes.
/// </summary>
public sealed record AlertAction
{
    /// <summary>Action type id — see Samsara API docs (e.g. 1 = Notification, 4 = Webhook).</summary>
    [JsonPropertyName("actionTypeId")]
    public required int ActionTypeId { get; init; }

    [JsonPropertyName("actionParams")]
    public object? ActionParams { get; init; }
}

/// <summary>
/// Settings on when the alert should be operational. Spec marks <c>timeRangeType</c>
/// and <c>timeRanges</c> as REQUIRED.
/// </summary>
public sealed record AlertOperationalSettings
{
    /// <summary>Valid values: <c>activeBetween</c>, <c>inactiveBetween</c>.</summary>
    [JsonPropertyName("timeRangeType")]
    public required string TimeRangeType { get; init; }

    [JsonPropertyName("timeRanges")]
    public required IReadOnlyList<object> TimeRanges { get; init; }
}

/// <summary>
/// A single condition inside an <see cref="AlertIncident"/>. Spec marks <c>description</c>,
/// <c>details</c>, and <c>triggerId</c> as REQUIRED. <c>details</c> is left as a weak
/// <see cref="object"/> because the spec's <c>WorkflowIncidentDetails</c> shape varies by trigger.
/// </summary>
public sealed record AlertIncidentCondition
{
    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("details")]
    public required object Details { get; init; }

    /// <summary>Unique identifier describing the type of condition (trigger type id).</summary>
    [JsonPropertyName("triggerId")]
    public required long TriggerId { get; init; }
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
/// Represents an alert incident (a triggered alert event). Spec marks
/// <c>configurationId</c>, <c>happenedAtTime</c>, <c>incidentUrl</c>, <c>isResolved</c>,
/// <c>updatedAtTime</c>, and <c>conditions</c> as REQUIRED. The spec inner schema does
/// not expose an <c>id</c>, top-level vehicle/driver references, or <c>alertId</c>.
/// </summary>
public sealed record AlertIncident
{
    [JsonPropertyName("configurationId")]
    public required string ConfigurationId { get; init; }

    /// <summary>Time the incident occurred (RFC 3339).</summary>
    [JsonPropertyName("happenedAtTime")]
    public required DateTimeOffset HappenedAtTime { get; init; }

    /// <summary>URL of the incident in the Samsara cloud dashboard.</summary>
    [JsonPropertyName("incidentUrl")]
    public required string IncidentUrl { get; init; }

    [JsonPropertyName("isResolved")]
    public required bool IsResolved { get; init; }

    /// <summary>Time the incident was last updated (RFC 3339).</summary>
    [JsonPropertyName("updatedAtTime")]
    public required DateTimeOffset UpdatedAtTime { get; init; }

    /// <summary>Conditions associated with the incident. Each follows the spec's <c>WorkflowIncidentCondition</c> shape.</summary>
    [JsonPropertyName("conditions")]
    public required IReadOnlyList<AlertIncidentCondition> Conditions { get; init; }

    /// <summary>Time the incident was resolved (RFC 3339), if resolved.</summary>
    [JsonPropertyName("resolvedAtTime")]
    public DateTimeOffset? ResolvedAtTime { get; init; }
}
