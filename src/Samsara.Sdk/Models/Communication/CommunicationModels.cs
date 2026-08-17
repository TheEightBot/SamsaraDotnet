namespace Samsara.Sdk.Models.Communication;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// A driver message returned by the legacy v1 endpoint
/// <c>GET /v1/fleet/messages</c> (spec <c>V1MessageResponse</c>). All fields below
/// are spec-REQUIRED.
/// </summary>
public sealed record DriverMessage
{
    /// <summary>ID of the driver the message was sent to or sent by (spec <c>int64</c>).</summary>
    [JsonPropertyName("driverId")]
    public required long DriverId { get; init; }

    /// <summary>True if the recipient has read the message.</summary>
    [JsonPropertyName("isRead")]
    public required bool IsRead { get; init; }

    /// <summary>Sender of the message (name + type).</summary>
    [JsonPropertyName("sender")]
    public required V1MessageSender Sender { get; init; }

    /// <summary>Time the message was sent to the recipient (Unix epoch milliseconds).</summary>
    [JsonPropertyName("sentAtMs")]
    public required long SentAtMs { get; init; }

    /// <summary>Body of the message.</summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }
}

/// <summary>
/// Sender of a driver message (spec <c>V1MessageSender</c>). Both fields are
/// spec-REQUIRED.
/// </summary>
public sealed record V1MessageSender
{
    /// <summary>Name of the user that sent the message.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Type of sender — either <c>dispatch</c> or <c>driver</c>.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

/// <summary>
/// Request body for <c>POST /v1/fleet/messages</c>: send the same text to a list
/// of driver IDs. Both fields are spec-REQUIRED.
/// </summary>
public sealed record SendDriverMessageRequest
{
    /// <summary>IDs of the drivers the message should be sent to (spec items are <c>int64</c>).</summary>
    [JsonPropertyName("driverIds")]
    public required IReadOnlyList<string> DriverIds { get; init; }

    /// <summary>Text of the message. Max 2500 characters.</summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }
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
/// What the triggers are scoped to. These are the objects this alert applies to.
/// Mirrors the spec schema <c>ScopeObjectResponseBody</c>.
/// </summary>
public sealed record AlertScope
{
    /// <summary>
    /// Whether it applies to all applicable objects.
    /// </summary>
    [JsonPropertyName("all")]
    public required bool All { get; init; }

    /// <summary>
    /// The assets these triggers are scoped to.
    /// </summary>
    [JsonPropertyName("assets")]
    public IReadOnlyList<AlertTinyAsset>? Assets { get; init; }

    /// <summary>
    /// The drivers these triggers are scoped to.
    /// </summary>
    [JsonPropertyName("drivers")]
    public IReadOnlyList<AlertTinyDriver>? Drivers { get; init; }

    /// <summary>
    /// The tags these triggers are scoped to.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }

    /// <summary>
    /// The widgets these triggers are scoped to.
    /// </summary>
    [JsonPropertyName("widgets")]
    public IReadOnlyList<AlertTinyWidget>? Widgets { get; init; }
}

/// <summary>
/// The trigger of an alert.
/// Mirrors the spec schema <c>WorkflowTriggerObjectResponseBody</c>.
/// </summary>
public sealed record AlertTrigger
{
    /// <summary>
    /// The <c>triggerParams</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("triggerParams")]
    public AlertTriggerParams? TriggerParams { get; init; }

    /// <summary>
    /// The id of the trigger type. Reference the following list for the ids: Ambient Temperature = 1003
    /// Asset Reading = 1062 DVIR Submitted for Asset = 5005 Driver Recorded = 5027 Sudden Fuel Level
    /// Rise = 5034 Sudden Fuel Level Drop = 5035 Training Assignment Due Soon = 8003 Training
    /// Assignment Past Due =...
    /// </summary>
    [JsonPropertyName("triggerTypeId")]
    public required int TriggerTypeId { get; init; }
}

/// <summary>
/// Action to take.
/// Mirrors the spec schema <c>ActionObjectResponseBody</c>.
/// </summary>
public sealed record AlertAction
{
    /// <summary>
    /// The <c>actionParams</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("actionParams")]
    public AlertActionParams? ActionParams { get; init; }

    /// <summary>
    /// The id of the of the action type. Reference the following list for the ids: The following action
    /// types are in Beta: Driver App Push = 5 Functions = 14 The following action types are Stable:
    /// Notification (Email, Text, Samsara Fleet Push) = 1 Dashboard Notification = 3 Webhook = 4 Slack
    /// = 6
    /// </summary>
    [JsonPropertyName("actionTypeId")]
    public required int ActionTypeId { get; init; }
}

/// <summary>
/// Settings on when the alert should be operational.
/// Mirrors the spec schema <c>OperationalSettingsObjectResponseBody</c>.
/// </summary>
public sealed record AlertOperationalSettings
{
    /// <summary>
    /// The type of time ranges. Valid values: `activeBetween`, `inactiveBetween` Valid values:
    /// <c>activeBetween</c>, <c>inactiveBetween</c>
    /// </summary>
    [JsonPropertyName("timeRangeType")]
    public required string TimeRangeType { get; init; }

    /// <summary>
    /// The time ranges this alert applies to.
    /// </summary>
    [JsonPropertyName("timeRanges")]
    public required IReadOnlyList<AlertTimeRange> TimeRanges { get; init; }
}

/// <summary>
/// Object representing the granular details of the condition. These details will vary depending on the
/// condition.
/// Mirrors the spec schema <c>WorkflowIncidentConditionObjectResponseBody</c>.
/// </summary>
public sealed record AlertIncidentCondition
{
    /// <summary>
    /// Descriptive name of the condition.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// The <c>details</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("details")]
    public WorkflowIncidentDetails? Details { get; init; }

    /// <summary>
    /// Unique identifier describing the type of condition being represented.
    /// </summary>
    [JsonPropertyName("triggerId")]
    public long? TriggerId { get; init; }
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

/// <summary>
/// The action type specific details. Set webhookIds for Slack or Webhook actions. Set recipients for
/// Notifications. Set driverAppNotification for Driver App Push. Other action types don't need to set a
/// param.
/// Mirrors the spec schema <c>ActionParamsObjectResponseBody</c>.
/// </summary>
public sealed record AlertActionParams
{
    /// <summary>
    /// The <c>driverAppNotification</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driverAppNotification")]
    public AlertDriverAppNotification? DriverAppNotification { get; init; }

    /// <summary>
    /// Recipient of the action.
    /// </summary>
    [JsonPropertyName("recipients")]
    public IReadOnlyList<AlertRecipient>? Recipients { get; init; }

    /// <summary>
    /// The <c>webhooks</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("webhooks")]
    public AlertWebhookParams? Webhooks { get; init; }
}

/// <summary>
/// Details specific to Ambient Temperature.
/// Mirrors the spec schema <c>AmbientTemperatureResponseBody</c>.
/// </summary>
public sealed record AlertAmbientTemperature
{
    /// <summary>
    /// The <c>sensor</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("sensor")]
    public AlertObjectSensor? Sensor { get; init; }
}

/// <summary>
/// Details specific to Ambient Temperature.
/// Mirrors the spec schema <c>AmbientTemperatureDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertAmbientTemperatureDetails
{
    /// <summary>
    /// Whether the cargo is full.
    /// </summary>
    [JsonPropertyName("cargoIsFull")]
    public bool? CargoIsFull { get; init; }

    /// <summary>
    /// Whether the doors are closed.
    /// </summary>
    [JsonPropertyName("doorsAreClosed")]
    public bool? DoorsAreClosed { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }

    /// <summary>
    /// How to evaluate the threshold. Valid values: `GREATER`, `INSIDE_RANGE`, `LESS`, `OUTSIDE_RANGE`
    /// Valid values: <c>GREATER</c>, <c>INSIDE_RANGE</c>, <c>LESS</c>, <c>OUTSIDE_RANGE</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public required string Operation { get; init; }

    /// <summary>
    /// The temperature in Celcius threshold value.
    /// </summary>
    [JsonPropertyName("temperatureCelcius")]
    public required long TemperatureCelcius { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>barcodeValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertBarcodeValue
{
    /// <summary>
    /// The barcode type that was scanned.
    /// </summary>
    [JsonPropertyName("barcodeType")]
    public string? BarcodeType { get; init; }

    /// <summary>
    /// The captured barcode value.
    /// </summary>
    [JsonPropertyName("barcodeValue")]
    public string? BarcodeValue { get; init; }
}

/// <summary>
/// Trigger when behavior count meets the specified condition.
/// Mirrors the spec schema <c>BehaviorCountDetailsResponseBody</c>.
/// </summary>
public sealed record AlertBehaviorCountDetails
{
    /// <summary>
    /// The comparison to use when comparing the value to the threshold. Valid values: `EQUAL_TO`,
    /// `GREATER_THAN`, `GREATER_THAN_OR_EQUAL_TO`, `LESS_THAN`, `LESS_THAN_OR_EQUAL_TO` Valid values:
    /// <c>EQUAL_TO</c>, <c>GREATER_THAN</c>, <c>GREATER_THAN_OR_EQUAL_TO</c>, <c>LESS_THAN</c>,
    /// <c>LESS_THAN_OR_EQUAL_TO</c>
    /// </summary>
    [JsonPropertyName("comparison")]
    public required string Comparison { get; init; }

    /// <summary>
    /// The number of behaviors to compare to.
    /// </summary>
    [JsonPropertyName("numBehaviors")]
    public required int NumBehaviors { get; init; }

    /// <summary>
    /// The number of days to compare to.
    /// </summary>
    [JsonPropertyName("numDays")]
    public required int NumDays { get; init; }
}

/// <summary>
/// Details specific to Camera Connector Disconnected.
/// Mirrors the spec schema <c>CameraConnectorDisconectedResponseBody</c>.
/// </summary>
public sealed record AlertCameraConnectorDisconected
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Camera Stream Issue.
/// Mirrors the spec schema <c>CameraStreamIssueResponseBody</c>.
/// </summary>
public sealed record AlertCameraStreamIssue
{
    /// <summary>
    /// The <c>cameraDevice</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cameraDevice")]
    public AlertObjectWorkforceCameraDevice? CameraDevice { get; init; }
}

/// <summary>
/// Details specific to Cell Signal Loss.
/// Mirrors the spec schema <c>CellSignalLossResponseBody</c>.
/// </summary>
public sealed record AlertCellSignalLoss
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Cell Signal Loss
/// Mirrors the spec schema <c>CellSignalLossDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertCellSignalLossDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Information about a circular geofence. This field is only needed if the geofence is a circle.
/// Mirrors the spec schema <c>CircleResponseBody</c>.
/// </summary>
public sealed record AlertCircle
{
    /// <summary>
    /// Latitude of the address. Will be geocoded from formattedAddress if not provided.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// Longitude of the address. Will be geocoded from formattedAddress if not provided.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>
    /// The name of the cirlce.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// The radius of the circular geofence in meters.
    /// </summary>
    [JsonPropertyName("radiusMeters")]
    public required long RadiusMeters { get; init; }
}

/// <summary>
/// Details specific to Cloud Backup Upload Issue.
/// Mirrors the spec schema <c>CloudBackupUploadIssueResponseBody</c>.
/// </summary>
public sealed record AlertCloudBackupUploadIssue
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>conditionalFieldSectionObjectResponseBody</c>.
/// </summary>
public sealed record AlertConditionalFieldSection
{
    /// <summary>
    /// The index of the first conditional field associated with the triggeringFieldValue in the
    /// fieldTypes list.
    /// </summary>
    [JsonPropertyName("conditionalFieldFirstIndex")]
    public long? ConditionalFieldFirstIndex { get; init; }

    /// <summary>
    /// The index of the last conditional field associated with the triggeringFieldValue in the
    /// fieldTypes list.
    /// </summary>
    [JsonPropertyName("conditionalFieldLastIndex")]
    public long? ConditionalFieldLastIndex { get; init; }

    /// <summary>
    /// The index of the multiple choice field in the fieldTypes list that triggers one or more
    /// conditional fields.
    /// </summary>
    [JsonPropertyName("triggeringFieldIndex")]
    public long? TriggeringFieldIndex { get; init; }

    /// <summary>
    /// The multiple choice option value that triggers the conditional fields.
    /// </summary>
    [JsonPropertyName("triggeringFieldValue")]
    public string? TriggeringFieldValue { get; init; }
}

/// <summary>
/// The threshold value of the alert for continuous readings.
/// Mirrors the spec schema <c>ContinuousReadingAlertThresholdResponseBody</c>.
/// </summary>
public sealed record AlertContinuousReadingAlertThreshold
{
    /// <summary>
    /// The operation used when comparing the value to the threshold. Valid values: `GREATER`,
    /// `INSIDE_RANGE`, `LESS`, `OUTSIDE_RANGE` Valid values: <c>GREATER</c>, <c>INSIDE_RANGE</c>,
    /// <c>LESS</c>, <c>OUTSIDE_RANGE</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public string? Operation { get; init; }

    /// <summary>
    /// The lower threshold of criticality.
    /// </summary>
    [JsonPropertyName("threshold")]
    public long? Threshold { get; init; }

    /// <summary>
    /// The unit of the threshold defined by reading type. Valid values: `ampere`, `bar`, `cad`,
    /// `celsius`, `chf`, `day`, `decimaldegrees`, `eur`, `fahrenheit`, `foot`, `gallon`,
    /// `gallonperkilogram`, `gallonsperhour`, `galpermi`, `gbp`, `gforce`, `gperliter`, `gperm`,
    /// `hertz`, `hour`, `imperialgallonperkilo... Valid values: <c>ampere</c>, <c>bar</c>, <c>cad</c>,
    /// <c>celsius</c>, <c>chf</c>, <c>day</c>, <c>decimaldegrees</c>, <c>eur</c>, <c>fahrenheit</c>,
    /// <c>foot</c>, <c>gallon</c>, <c>gallonperkilogram</c>, <c>gallonsperhour</c>, <c>galpermi</c>,
    /// <c>gbp</c>, <c>gforce</c>, <c>gperliter</c>, <c>gperm</c>, <c>hertz</c>, <c>hour</c>,
    /// <c>imperialgallonperkilogram</c>, <c>impgallon</c>, <c>impgallonsperhour</c>,
    /// <c>impgalpermi</c>, <c>inch</c>, <c>kelvin</c>, <c>kgper100kmgaseousfuel</c>,
    /// <c>kgpergallon</c>, <c>kgperkm</c>, <c>kgperliter</c>, <c>kgpermi</c>, <c>kilogram</c>,
    /// <c>kilogramgaseousfuel</c>, <c>kilometer</c>, <c>kilopascal</c>, <c>kilowatthour</c>,
    /// <c>kmperhr</c>, <c>kmperl</c>, <c>kmperlgaseousfuel</c>, <c>lbpermi</c>, <c>liter</c>,
    /// <c>litergaseousfuel</c>, <c>literperkilogram</c>, <c>literpertonne</c>, <c>litersperhour</c>,
    /// <c>lper100km</c>, <c>lper100kmgaseousfuel</c>, <c>lperkm</c>, <c>lperm</c>, <c>meter</c>,
    /// <c>meterspersec</c>, <c>mile</c>, <c>milliknot</c>, <c>millisecond</c>, <c>millivolt</c>,
    /// <c>minute</c>, <c>mipergal</c>, <c>miperhr</c>, <c>miperimpgal</c>, <c>month</c>,
    /// <c>mpgusgalgaseousfuel</c>, <c>mpkggaseousfuel</c>, <c>mxn</c>, <c>percent</c>, <c>pound</c>,
    /// <c>poundsPerSquareInch</c>, <c>poundspergallon</c>, <c>poundsperliter</c>, <c>rpm</c>,
    /// <c>second</c>, <c>usd</c>, <c>usgallongaseousfuel</c>, <c>volt</c>, <c>voltAmpere</c>,
    /// <c>voltAmpereReactive</c>, <c>watt</c>, <c>watthour</c>, <c>week</c>
    /// </summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }

    /// <summary>
    /// The upper threshold of criticality. Required for RANGE operations.
    /// </summary>
    [JsonPropertyName("upperThreshold")]
    public long? UpperThreshold { get; init; }
}

/// <summary>
/// Details specific to DVIR Submitted by Device
/// Mirrors the spec schema <c>DVIRSubmittedDeviceTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertDVIRSubmittedDeviceTriggerDetails
{
    /// <summary>
    /// The trigger will only fire if the selected DVIR types are submitted within the duration.
    /// </summary>
    [JsonPropertyName("dvirMinDurationMilliseconds")]
    public long? DvirMinDurationMilliseconds { get; init; }

    /// <summary>
    /// Filter to these types of DVIR submissions. Valid values: <c>SAFE_NO_DEFECTS</c>,
    /// <c>SAFE_WITH_DEFECTS</c>, <c>UNSAFE</c>
    /// </summary>
    [JsonPropertyName("dvirSubmissionTypes")]
    public IReadOnlyList<string>? DvirSubmissionTypes { get; init; }
}

/// <summary>
/// Details specific to Dashcam Disconnected.
/// Mirrors the spec schema <c>DashcamDisconnectedResponseBody</c>.
/// </summary>
public sealed record AlertDashcamDisconnected
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Data Input Value.
/// Mirrors the spec schema <c>DataInputValueResponseBody</c>.
/// </summary>
public sealed record AlertDataInputValue
{
    /// <summary>
    /// The <c>machineInput</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("machineInput")]
    public EntityReference? MachineInput { get; init; }
}

/// <summary>
/// The value of a date time field. Only present for date time fields.
/// Mirrors the spec schema <c>dateTimeValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertDateTimeValue
{
    /// <summary>
    /// Date time value inin RFC 3339 format.
    /// </summary>
    [JsonPropertyName("dateTime")]
    public DateTimeOffset? DateTime { get; init; }
}

/// <summary>
/// Details specific to DEF Level
/// Mirrors the spec schema <c>DefLevelTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertDefLevelTriggerDetails
{
    /// <summary>
    /// The DEF percentage threshold value.
    /// </summary>
    [JsonPropertyName("defLevelPercent")]
    public required long DefLevelPercent { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }

    /// <summary>
    /// How to evaluate the threshold. Valid values: `GREATER`, `LESS` Valid values: <c>GREATER</c>,
    /// <c>LESS</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public required string Operation { get; init; }
}

/// <summary>
/// Details specific to Device Movement.
/// Mirrors the spec schema <c>DeviceMovementDataResponseBody</c>.
/// </summary>
public sealed record AlertDeviceMovementData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Device Movement Stopped.
/// Mirrors the spec schema <c>DeviceMovementStoppedDataResponseBody</c>.
/// </summary>
public sealed record AlertDeviceMovementStoppedData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Device Movement
/// Mirrors the spec schema <c>DeviceMovementTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertDeviceMovementTriggerDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// A single document.
/// Mirrors the spec schema <c>documentResponseObjectResponseBody</c>.
/// </summary>
public sealed record AlertDocumentResponse
{
    /// <summary>
    /// List of the document conditional field sections.
    /// </summary>
    [JsonPropertyName("conditionalFieldSections")]
    public IReadOnlyList<AlertConditionalFieldSection>? ConditionalFieldSections { get; init; }

    /// <summary>
    /// Time the document was created in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>
    /// The <c>documentType</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("documentType")]
    public EntityReference? DocumentType { get; init; }

    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertGoaDriverTiny? Driver { get; init; }

    /// <summary>
    /// The fields associated with this document.
    /// </summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<AlertField>? Fields { get; init; }

    /// <summary>
    /// Universally unique identifier for the document.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the document.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Notes on the document.
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>
    /// The <c>route</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("route")]
    public AlertGoaRouteTiny? Route { get; init; }

    /// <summary>
    /// The <c>routeStop</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("routeStop")]
    public AlertGoaRouteStopTiny? RouteStop { get; init; }

    /// <summary>
    /// The condition of the document created for the driver. Can be either Required or Submitted.
    /// Required documents are pre-populated documents for the Driver to fill out in the Driver App and
    /// have not yet been submitted. Submitted documents have been submitted by the driver in the Driver
    /// App. Archived do... Valid values: <c>submitted</c>, <c>required</c>, <c>archived</c>
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>
    /// Time the document was updated in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertGoaVehicleTiny? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Door Open.
/// Mirrors the spec schema <c>DoorOpenResponseBody</c>.
/// </summary>
public sealed record AlertDoorOpen
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Driver app notification settings
/// Mirrors the spec schema <c>DriverAppNotificationObjectResponseBody</c>.
/// </summary>
public sealed record AlertDriverAppNotification
{
    /// <summary>
    /// The <c>inAppNotificationOptions</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("inAppNotificationOptions")]
    public AlertInAppNotificationOptions? InAppNotificationOptions { get; init; }

    /// <summary>
    /// The <c>pushNotificationOptions</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("pushNotificationOptions")]
    public AlertPushNotificationOptions? PushNotificationOptions { get; init; }
}

/// <summary>
/// Details specific to Driver App Sign In.
/// Mirrors the spec schema <c>DriverAppSignInResponseBody</c>.
/// </summary>
public sealed record AlertDriverAppSignIn
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>pinnedVehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("pinnedVehicle")]
    public AlertObjectVehicle? PinnedVehicle { get; init; }
}

/// <summary>
/// Details specific to Driver App Sign Out.
/// Mirrors the spec schema <c>DriverAppSignOutResponseBody</c>.
/// </summary>
public sealed record AlertDriverAppSignOut
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }
}

/// <summary>
/// Details specific to Driver Document Submitted.
/// Mirrors the spec schema <c>DriverDocumentSubmittedResponseBody</c>.
/// </summary>
public sealed record AlertDriverDocumentSubmitted
{
    /// <summary>
    /// The <c>document</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("document")]
    public AlertDocumentResponse? Document { get; init; }
}

/// <summary>
/// Details specific to Driver Document Submitted
/// Mirrors the spec schema <c>DriverDocumentSubmittedDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertDriverDocumentSubmittedDetails
{
    /// <summary>
    /// Specific template IDs to be alerted on.
    /// </summary>
    [JsonPropertyName("templateIds")]
    public required IReadOnlyList<string> TemplateIds { get; init; }
}

/// <summary>
/// Details specific to Driver Message Received.
/// Mirrors the spec schema <c>DriverMessageReceivedResponseBody</c>.
/// </summary>
public sealed record AlertDriverMessageReceived
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }
}

/// <summary>
/// Details specific to Driver Message Sent.
/// Mirrors the spec schema <c>DriverMessageSentResponseBody</c>.
/// </summary>
public sealed record AlertDriverMessageSent
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }
}

/// <summary>
/// Trigger when driver or tag IDs meet the specified condition.
/// Mirrors the spec schema <c>DriverOrTagIdsDetailsResponseBody</c>.
/// </summary>
public sealed record AlertDriverOrTagIdsDetails
{
    /// <summary>
    /// On which driver IDs to trigger on.
    /// </summary>
    [JsonPropertyName("driverIds")]
    public IReadOnlyList<string>? DriverIds { get; init; }

    /// <summary>
    /// On which tag IDs to trigger on.
    /// </summary>
    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }
}

/// <summary>
/// Details specific to Driver Recorded.
/// Mirrors the spec schema <c>DriverRecordedResponseBody</c>.
/// </summary>
public sealed record AlertDriverRecorded
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// The user or driver assigned to complete the DVIR.
/// Mirrors the spec schema <c>Dvir2AssignedToPolymorphicUserObjectResponseBody</c>.
/// </summary>
public sealed record AlertDvir2AssignedToPolymorphicUser
{
    /// <summary>
    /// The name of the user or driver assigned to the DVIR.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The polymorphic user ID that identifies the user or driver.
    /// </summary>
    [JsonPropertyName("polymorphicUserId")]
    public string? PolymorphicUserId { get; init; }
}

/// <summary>
/// Device details associated with the DVIR.
/// Mirrors the spec schema <c>Dvir2DeviceObjectResponseBody</c>.
/// </summary>
public sealed record AlertDvir2Device
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The license plate of the vehicle.
    /// </summary>
    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    /// <summary>
    /// The name of the device.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The VIN of the vehicle.
    /// </summary>
    [JsonPropertyName("vehicleVin")]
    public string? VehicleVin { get; init; }
}

/// <summary>
/// Base form submission metadata.
/// Mirrors the spec schema <c>Dvir2FormSubmissionObjectResponseBody</c>.
/// </summary>
public sealed record AlertDvir2FormSubmission
{
    /// <summary>
    /// The <c>assignedToPolymorphicUser</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("assignedToPolymorphicUser")]
    public AlertDvir2AssignedToPolymorphicUser? AssignedToPolymorphicUser { get; init; }

    /// <summary>
    /// Time of when the form submission is due. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("dueDate")]
    public DateTimeOffset? DueDate { get; init; }

    /// <summary>
    /// Time when the form submission was last updated on the server. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("serverUpdatedAt")]
    public DateTimeOffset? ServerUpdatedAt { get; init; }

    /// <summary>
    /// The unique UUID of the form submission
    /// </summary>
    [JsonPropertyName("uuid")]
    public string? Uuid { get; init; }
}

/// <summary>
/// A device (vehicle or trailer) related to the DVIR.
/// Mirrors the spec schema <c>Dvir2RelatedDeviceObjectResponseBody</c>.
/// </summary>
public sealed record AlertDvir2RelatedDevice
{
    /// <summary>
    /// The type of asset. Valid values: `Vehicle`, `Trailer` Valid values: <c>Vehicle</c>,
    /// <c>Trailer</c>
    /// </summary>
    [JsonPropertyName("assetType")]
    public string? AssetType { get; init; }

    /// <summary>
    /// The <c>device</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("device")]
    public AlertDvir2Device? Device { get; init; }

    /// <summary>
    /// The ID of the device.
    /// </summary>
    [JsonPropertyName("deviceId")]
    public long? DeviceId { get; init; }
}

/// <summary>
/// A DVIR description.
/// Mirrors the spec schema <c>Dvir2SubmissionResponseObjectResponseBody</c>.
/// </summary>
public sealed record AlertDvir2SubmissionResponse
{
    /// <summary>
    /// The <c>baseFormSubmission</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("baseFormSubmission")]
    public AlertDvir2FormSubmission? BaseFormSubmission { get; init; }

    /// <summary>
    /// Inspection type of the DVIR. Valid values: `preTrip`, `postTrip`, `mechanic`, `unset` Valid
    /// values: <c>preTrip</c>, <c>postTrip</c>, <c>mechanic</c>, <c>unset</c>
    /// </summary>
    [JsonPropertyName("inspectionType")]
    public string? InspectionType { get; init; }

    /// <summary>
    /// List of devices (vehicles/trailers) associated with the DVIR.
    /// </summary>
    [JsonPropertyName("relatedDevices")]
    public IReadOnlyList<AlertDvir2RelatedDevice>? RelatedDevices { get; init; }

    /// <summary>
    /// The unique UUID of the DVIR2 submission
    /// </summary>
    [JsonPropertyName("uuid")]
    public string? Uuid { get; init; }
}

/// <summary>
/// A description of a DVIR defect
/// Mirrors the spec schema <c>DvirDefectsObject_v2022_09_13ResponseBody</c>.
/// </summary>
public sealed record AlertDvirDefectsObject_v2022_09_13
{
    /// <summary>
    /// Comment on the defect.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    /// <summary>
    /// Time when the defect was created. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>
    /// The severity of the DVIR defect. Valid values: `minor`, `major`, `unspecified` Valid values:
    /// <c>minor</c>, <c>major</c>, <c>unspecified</c>
    /// </summary>
    [JsonPropertyName("defectSeverity")]
    public string? DefectSeverity { get; init; }

    /// <summary>
    /// The type of DVIR defect.
    /// </summary>
    [JsonPropertyName("defectType")]
    public string? DefectType { get; init; }

    /// <summary>
    /// The ID of the DVIR defect type.
    /// </summary>
    [JsonPropertyName("defectTypeId")]
    public string? DefectTypeId { get; init; }

    /// <summary>
    /// The ID of the defect.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Signifies if this defect is resolved.
    /// </summary>
    [JsonPropertyName("isResolved")]
    public bool? IsResolved { get; init; }

    /// <summary>
    /// The mechanic notes on this defect.
    /// </summary>
    [JsonPropertyName("mechanicNotes")]
    public string? MechanicNotes { get; init; }

    /// <summary>
    /// Time when mechanic notes were last updated. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("mechanicNotesUpdatedAtTime")]
    public DateTimeOffset? MechanicNotesUpdatedAtTime { get; init; }

    /// <summary>
    /// Time when this defect was resolved. Will not be returned if the defect is unresolved. UTC
    /// timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("resolvedAtTime")]
    public DateTimeOffset? ResolvedAtTime { get; init; }

    /// <summary>
    /// The <c>resolvedBy</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("resolvedBy")]
    public AlertDvirResolvedBy? ResolvedBy { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertGoaTrailerTiny? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertVehicleWithGatewayTiny? Vehicle { get; init; }
}

/// <summary>
/// The person who resolved this defect.
/// Mirrors the spec schema <c>DvirResolvedByObjectResponseBody</c>.
/// </summary>
public sealed record AlertDvirResolvedBy
{
    /// <summary>
    /// ID of the entity that resolved this defect. If the defect was resolved by a driver, this will be
    /// a Samsara Driver ID. If the defect was resolved by a mechanic, this will be the Samsara
    /// Dashboard User ID of the mechanic.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the person who resolved this defect.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Indicates whether this defect was resolved by a driver or a mechanic. Valid values: `driver`,
    /// `mechanic` Valid values: <c>driver</c>, <c>mechanic</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// Details specific to Engine Idle.
/// Mirrors the spec schema <c>EngineIdleDataResponseBody</c>.
/// </summary>
public sealed record AlertEngineIdleData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Engine Idle
/// Mirrors the spec schema <c>EngineIdleTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertEngineIdleTriggerDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Details specific to Engine Off.
/// Mirrors the spec schema <c>EngineOffResponseBody</c>.
/// </summary>
public sealed record AlertEngineOff
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Engine Off
/// Mirrors the spec schema <c>EngineOffDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertEngineOffDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Details specific to Engine On.
/// Mirrors the spec schema <c>EngineOnResponseBody</c>.
/// </summary>
public sealed record AlertEngineOn
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Engine On
/// Mirrors the spec schema <c>EngineOnDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertEngineOnDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// The threshold value of the alert for enum readings.
/// Mirrors the spec schema <c>EnumReadingAlertThresholdResponseBody</c>.
/// </summary>
public sealed record AlertEnumReadingAlertThreshold
{
    /// <summary>
    /// The numeric representation of the enum value.
    /// </summary>
    [JsonPropertyName("number")]
    public long? Number { get; init; }

    /// <summary>
    /// The symbol representation of the enum value.
    /// </summary>
    [JsonPropertyName("symbol")]
    public string? Symbol { get; init; }
}

/// <summary>
/// Information about a location when the vehicle was stopped.
/// Mirrors the spec schema <c>EventLocationResponseBody</c>.
/// </summary>
public sealed record AlertEventLocation
{
    /// <summary>
    /// Latitude of the event.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// Longitude of the event.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>fieldObjectResponseBody</c>.
/// </summary>
public sealed record AlertField
{
    /// <summary>
    /// The name of the field.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// The type of field. Valid values: `photo`, `string`, `number`, `multipleChoice`, `signature`,
    /// `dateTime`, `scannedDocument`, `barcode` Valid values: <c>photo</c>, <c>string</c>,
    /// <c>number</c>, <c>multipleChoice</c>, <c>signature</c>, <c>dateTime</c>, <c>scannedDocument</c>,
    /// <c>barcode</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// The <c>value</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("value")]
    public AlertFieldObjectValue? Value { get; init; }
}

/// <summary>
/// The value of the document field. The shape of value depends on the type.
/// Mirrors the spec schema <c>fieldObjectValueResponseBody</c>.
/// </summary>
public sealed record AlertFieldObjectValue
{
    /// <summary>
    /// The value of a barcode scanning field. Only present for barcode scanning fields.
    /// </summary>
    [JsonPropertyName("barcodeValue")]
    public IReadOnlyList<AlertBarcodeValue>? BarcodeValue { get; init; }

    /// <summary>
    /// The <c>dateTimeValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dateTimeValue")]
    public AlertDateTimeValue? DateTimeValue { get; init; }

    /// <summary>
    /// The value of a multiple choice field. Only present for multiple choice fields.
    /// </summary>
    [JsonPropertyName("multipleChoiceValue")]
    public IReadOnlyList<AlertMultipleChoiceValue>? MultipleChoiceValue { get; init; }

    /// <summary>
    /// The value of a number field. Only present for number fields.
    /// </summary>
    [JsonPropertyName("numberValue")]
    public double? NumberValue { get; init; }

    /// <summary>
    /// The value of a photo field. Only present for photo fields.
    /// </summary>
    [JsonPropertyName("photoValue")]
    public IReadOnlyList<AlertPhotoValue>? PhotoValue { get; init; }

    /// <summary>
    /// The value of a scanned document field. Only present for scanned document fields.
    /// </summary>
    [JsonPropertyName("scannedDocumentValue")]
    public IReadOnlyList<AlertScannedDocumentValue>? ScannedDocumentValue { get; init; }

    /// <summary>
    /// The <c>signatureValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("signatureValue")]
    public AlertSignatureValue? SignatureValue { get; init; }

    /// <summary>
    /// The value of a string field. Only present for string fields.
    /// </summary>
    [JsonPropertyName("stringValue")]
    public string? StringValue { get; init; }
}

/// <summary>
/// Form Submission response object.
/// Mirrors the spec schema <c>FormSubmissionResponseObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormSubmissionResponse
{
    /// <summary>
    /// The <c>approvalDetails</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("approvalDetails")]
    public AlertFormsProductSubmissionApprovalDetails? ApprovalDetails { get; init; }

    /// <summary>
    /// The <c>asset</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("asset")]
    public AlertFormsAsset? Asset { get; init; }

    /// <summary>
    /// Assignment time of the form submission. Sometimes returned if the submission was assigned to a
    /// user or driver. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("assignedAtTime")]
    public DateTimeOffset? AssignedAtTime { get; init; }

    /// <summary>
    /// The <c>assignedTo</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("assignedTo")]
    public AlertFormsPolymorphicUser? AssignedTo { get; init; }

    /// <summary>
    /// Creation time of the form submission. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>
    /// Time of when the submission is due. Sometimes returned, if the submission has a due date. UTC
    /// timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("dueAtTime")]
    public DateTimeOffset? DueAtTime { get; init; }

    /// <summary>
    /// Duration between when the form submission was started on the client and submitted, in
    /// milliseconds. Omitted until the form is actually submitted or when the client start timestamp
    /// was not recorded.
    /// </summary>
    [JsonPropertyName("durationMs")]
    public long? DurationMs { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// List of field inputs in a form submission.
    /// </summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<AlertFormsFieldInput>? Fields { get; init; }

    /// <summary>
    /// The <c>formTemplate</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("formTemplate")]
    public AlertFormTemplateReference? FormTemplate { get; init; }

    /// <summary>
    /// The <c>geofence</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("geofence")]
    public AlertFormsGeofence? Geofence { get; init; }

    /// <summary>
    /// ID of the form submission.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Indicates whether the worker is required to complete this form or not. Always returned.
    /// </summary>
    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; init; }

    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public AlertFormsLocation? Location { get; init; }

    /// <summary>
    /// ID of the route. Sometimes returned if the submission was assigned to a route stop.
    /// </summary>
    [JsonPropertyName("routeId")]
    public string? RouteId { get; init; }

    /// <summary>
    /// ID of the route stop. Sometimes returned if the submission was assigned to a route stop.
    /// </summary>
    [JsonPropertyName("routeStopId")]
    public string? RouteStopId { get; init; }

    /// <summary>
    /// The <c>score</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("score")]
    public AlertFormsScore? Score { get; init; }

    /// <summary>
    /// State for the Form Submission. Always returned. Valid values: `notStarted`, `completed`,
    /// `archived`, `inProgress`, `needsReview`, `changesRequested`, `approved` Valid values:
    /// <c>notStarted</c>, <c>completed</c>, <c>archived</c>, <c>inProgress</c>, <c>needsReview</c>,
    /// <c>changesRequested</c>, <c>approved</c>
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>
    /// Submission time of the form submission. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("submittedAtTime")]
    public DateTimeOffset? SubmittedAtTime { get; init; }

    /// <summary>
    /// The <c>submittedBy</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("submittedBy")]
    public AlertFormsPolymorphicUser? SubmittedBy { get; init; }

    /// <summary>
    /// Title of the form submission. Sometimes returned if the submission has a title.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Update time of the form submission. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// Details specific to Form Submitted.
/// Mirrors the spec schema <c>FormSubmittedResponseBody</c>.
/// </summary>
public sealed record AlertFormSubmitted
{
    /// <summary>
    /// The <c>form</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("form")]
    public AlertFormSubmissionResponse? Form { get; init; }
}

/// <summary>
/// Form template reference object.
/// Mirrors the spec schema <c>FormTemplateReferenceObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormTemplateReference
{
    /// <summary>
    /// ID of the form template.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// ID of the form template revision.
    /// </summary>
    [JsonPropertyName("revisionId")]
    public string? RevisionId { get; init; }
}

/// <summary>
/// Details specific to Form Updated.
/// Mirrors the spec schema <c>FormUpdatedResponseBody</c>.
/// </summary>
public sealed record AlertFormUpdated
{
    /// <summary>
    /// The <c>form</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("form")]
    public AlertFormSubmissionResponse? Form { get; init; }
}

/// <summary>
/// Tracked or untracked (i.e. manually entered) asset object.
/// Mirrors the spec schema <c>FormsAssetObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsAsset
{
    /// <summary>
    /// The type of entry for the asset. Valid values: `tracked`, `untracked` Valid values:
    /// <c>tracked</c>, <c>untracked</c>
    /// </summary>
    [JsonPropertyName("entryType")]
    public string? EntryType { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of a tracked asset. Included if 'entryType' is `tracked`.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of an untracked (i.e. manually entered) asset.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// The value of an asset form input field.
/// Mirrors the spec schema <c>FormsAssetValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsAssetValue
{
    /// <summary>
    /// The <c>asset</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("asset")]
    public AlertFormsAsset? Asset { get; init; }
}

/// <summary>
/// A single barcode entry.
/// Mirrors the spec schema <c>FormsBarcodeObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsBarcode
{
    /// <summary>
    /// The captured barcode value.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// The value of a barcode form input field.
/// Mirrors the spec schema <c>FormsBarcodeValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsBarcodeValue
{
    /// <summary>
    /// List of barcode entries.
    /// </summary>
    [JsonPropertyName("barcodes")]
    public IReadOnlyList<AlertFormsBarcode>? Barcodes { get; init; }
}

/// <summary>
/// The value of a check boxes form input field.
/// Mirrors the spec schema <c>FormsCheckBoxesValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsCheckBoxesValue
{
    /// <summary>
    /// List of selected options.
    /// </summary>
    [JsonPropertyName("value")]
    public IReadOnlyList<string>? Value { get; init; }

    /// <summary>
    /// List of selected option IDs.
    /// </summary>
    [JsonPropertyName("valueIds")]
    public IReadOnlyList<string>? ValueIds { get; init; }
}

/// <summary>
/// The value of a datetime form input field.
/// Mirrors the spec schema <c>FormsDateTimeValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsDateTimeValue
{
    /// <summary>
    /// Calendar date in YYYY-MM-DD format in the stored field timezone. Present when type is `date`
    /// (date-only fields).
    /// </summary>
    [JsonPropertyName("dateValue")]
    public DateTimeOffset? DateValue { get; init; }

    /// <summary>
    /// The type of datetime format. Valid values: `datetime`, `date`, `time` Valid values:
    /// <c>datetime</c>, <c>date</c>, <c>time</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("value")]
    public DateTimeOffset? Value { get; init; }
}

/// <summary>
/// Forms input field object.
/// Mirrors the spec schema <c>FormsFieldInputObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsFieldInput
{
    /// <summary>
    /// The <c>assetValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("assetValue")]
    public AlertFormsAssetValue? AssetValue { get; init; }

    /// <summary>
    /// The <c>barcodeValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("barcodeValue")]
    public AlertFormsBarcodeValue? BarcodeValue { get; init; }

    /// <summary>
    /// The <c>checkBoxesValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("checkBoxesValue")]
    public AlertFormsCheckBoxesValue? CheckBoxesValue { get; init; }

    /// <summary>
    /// The <c>dateTimeValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dateTimeValue")]
    public AlertFormsDateTimeValue? DateTimeValue { get; init; }

    /// <summary>
    /// The <c>geofenceValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("geofenceValue")]
    public AlertFormsGeofenceValue? GeofenceValue { get; init; }

    /// <summary>
    /// ID of the forms input field object.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The <c>issue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("issue")]
    public AlertFormsIssueCreatedByField? Issue { get; init; }

    /// <summary>
    /// Forms input field label.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// List of forms media record objects.
    /// </summary>
    [JsonPropertyName("mediaList")]
    public IReadOnlyList<AlertFormsMediaRecord>? MediaList { get; init; }

    /// <summary>
    /// The <c>mediaValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("mediaValue")]
    public AlertFormsMediaValue? MediaValue { get; init; }

    /// <summary>
    /// The <c>multipleChoiceValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("multipleChoiceValue")]
    public AlertFormsMultipleChoiceValue? MultipleChoiceValue { get; init; }

    /// <summary>
    /// A note attached to the field input.
    /// </summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>
    /// The <c>numberValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("numberValue")]
    public AlertFormsNumberValue? NumberValue { get; init; }

    /// <summary>
    /// The <c>personValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("personValue")]
    public AlertFormsPersonValue? PersonValue { get; init; }

    /// <summary>
    /// The <c>signatureValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("signatureValue")]
    public AlertFormsSignatureValue? SignatureValue { get; init; }

    /// <summary>
    /// The <c>tableValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("tableValue")]
    public AlertFormsTableValue? TableValue { get; init; }

    /// <summary>
    /// The <c>textValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("textValue")]
    public AlertFormsTextValue? TextValue { get; init; }

    /// <summary>
    /// Type of the field. Valid values: `number`, `text`, `multiple_choice`, `check_boxes`, `datetime`,
    /// `signature`, `media`, `asset`, `table`, `person`, `geofence`, `barcode` Valid values:
    /// <c>number</c>, <c>text</c>, <c>multiple_choice</c>, <c>check_boxes</c>, <c>datetime</c>,
    /// <c>signature</c>, <c>media</c>, <c>asset</c>, <c>table</c>, <c>person</c>, <c>geofence</c>,
    /// <c>barcode</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// Tracked or untracked (i.e. manually entered) geofence object.
/// Mirrors the spec schema <c>FormsGeofenceObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsGeofence
{
    /// <summary>
    /// Address of the geofence. Included if 'entryType' is `tracked`.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>
    /// The type of entry for the geofence. Valid values: `tracked`, `untracked` Valid values:
    /// <c>tracked</c>, <c>untracked</c>
    /// </summary>
    [JsonPropertyName("entryType")]
    public string? EntryType { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of the tracked geofence. Included if 'entryType' is `tracked`.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of an untracked (i.e. manually entered) geofence.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// The value of a geofence form input field.
/// Mirrors the spec schema <c>FormsGeofenceValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsGeofenceValue
{
    /// <summary>
    /// The <c>geofence</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("geofence")]
    public AlertFormsGeofence? Geofence { get; init; }
}

/// <summary>
/// Issue created from this form input field input object.
/// Mirrors the spec schema <c>FormsIssueCreatedByFieldObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsIssueCreatedByField
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of the issue created from this form input field input object.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// Form template location object.
/// Mirrors the spec schema <c>FormsLocationObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsLocation
{
    /// <summary>
    /// Latitude of a location.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// Longitude of a location.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// Forms media record object.
/// Mirrors the spec schema <c>FormsMediaRecordObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsMediaRecord
{
    /// <summary>
    /// ID of the media record.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Status of the media record. Valid values: `unknown`, `processing`, `finished` Valid values:
    /// <c>unknown</c>, <c>processing</c>, <c>finished</c>
    /// </summary>
    [JsonPropertyName("processingStatus")]
    public string? ProcessingStatus { get; init; }

    /// <summary>
    /// URL containing a link to associated media content. Included if 'processingStatus' is 'finished'.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Expiration time of the media record 'url'. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("urlExpiresAt")]
    public DateTimeOffset? UrlExpiresAt { get; init; }
}

/// <summary>
/// The value of a media form input field.
/// Mirrors the spec schema <c>FormsMediaValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsMediaValue
{
    /// <summary>
    /// List of forms media record objects.
    /// </summary>
    [JsonPropertyName("mediaList")]
    public IReadOnlyList<AlertFormsMediaRecord>? MediaList { get; init; }
}

/// <summary>
/// The value of a multiple choice form input field.
/// Mirrors the spec schema <c>FormsMultipleChoiceValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsMultipleChoiceValue
{
    /// <summary>
    /// Selected option.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }

    /// <summary>
    /// ID of the selected option.
    /// </summary>
    [JsonPropertyName("valueId")]
    public string? ValueId { get; init; }
}

/// <summary>
/// The value of a number form input field.
/// Mirrors the spec schema <c>FormsNumberValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsNumberValue
{
    /// <summary>
    /// Number value.
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// Tracked or untracked (i.e. manually entered) person object.
/// Mirrors the spec schema <c>FormsPersonObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsPerson
{
    /// <summary>
    /// The type of entry for the person. Valid values: `tracked`, `untracked` Valid values:
    /// <c>tracked</c>, <c>untracked</c>
    /// </summary>
    [JsonPropertyName("entryType")]
    public string? EntryType { get; init; }

    /// <summary>
    /// Name of an untracked (i.e. manually entered) person.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The <c>polymorphicUserId</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("polymorphicUserId")]
    public AlertFormsPolymorphicUser? PolymorphicUserId { get; init; }
}

/// <summary>
/// The value of a person form input field.
/// Mirrors the spec schema <c>FormsPersonValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsPersonValue
{
    /// <summary>
    /// The <c>person</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("person")]
    public AlertFormsPerson? Person { get; init; }
}

/// <summary>
/// User or driver object.
/// Mirrors the spec schema <c>FormsPolymorphicUserObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsPolymorphicUser
{
    /// <summary>
    /// ID of the polymorphic user.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The type of the polymorphic user. Valid values: `driver`, `user` Valid values: <c>driver</c>,
    /// <c>user</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// The value of the approval details for a forms product submission.
/// Mirrors the spec schema <c>FormsProductSubmissionApprovalDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsProductSubmissionApprovalDetails
{
    /// <summary>
    /// Comment from the approver when requesting changes or approving the submission.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }
}

/// <summary>
/// Forms score object.
/// Mirrors the spec schema <c>FormsScoreObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsScore
{
    /// <summary>
    /// Total possible points of the form submission.
    /// </summary>
    [JsonPropertyName("maxPoints")]
    public double? MaxPoints { get; init; }

    /// <summary>
    /// Percentage score of the form submission, calculated as scorePoints / maxPoints.
    /// </summary>
    [JsonPropertyName("scorePercent")]
    public double? ScorePercent { get; init; }

    /// <summary>
    /// Score, in points, of the form submission.
    /// </summary>
    [JsonPropertyName("scorePoints")]
    public double? ScorePoints { get; init; }
}

/// <summary>
/// The value of a signature form input field.
/// Mirrors the spec schema <c>FormsSignatureValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsSignatureValue
{
    /// <summary>
    /// The <c>media</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("media")]
    public AlertFormsMediaRecord? Media { get; init; }
}

/// <summary>
/// Defines a cell in a table row.
/// Mirrors the spec schema <c>FormsTableCellObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsTableCell
{
    /// <summary>
    /// The <c>barcodeValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("barcodeValue")]
    public AlertFormsBarcodeValue? BarcodeValue { get; init; }

    /// <summary>
    /// The <c>checkBoxesValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("checkBoxesValue")]
    public AlertFormsCheckBoxesValue? CheckBoxesValue { get; init; }

    /// <summary>
    /// The <c>dateTimeValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dateTimeValue")]
    public AlertFormsDateTimeValue? DateTimeValue { get; init; }

    /// <summary>
    /// Unique identifier for the cell.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The <c>mediaValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("mediaValue")]
    public AlertFormsMediaValue? MediaValue { get; init; }

    /// <summary>
    /// The <c>multipleChoiceValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("multipleChoiceValue")]
    public AlertFormsMultipleChoiceValue? MultipleChoiceValue { get; init; }

    /// <summary>
    /// The <c>numberValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("numberValue")]
    public AlertFormsNumberValue? NumberValue { get; init; }

    /// <summary>
    /// The <c>personValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("personValue")]
    public AlertFormsPersonValue? PersonValue { get; init; }

    /// <summary>
    /// The <c>signatureValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("signatureValue")]
    public AlertFormsSignatureValue? SignatureValue { get; init; }

    /// <summary>
    /// The <c>textValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("textValue")]
    public AlertFormsTextValue? TextValue { get; init; }

    /// <summary>
    /// Type of the cell field. Valid values: `number`, `text`, `multiple_choice`, `check_boxes`,
    /// `datetime`, `signature`, `media`, `person`, `barcode` Valid values: <c>number</c>, <c>text</c>,
    /// <c>multiple_choice</c>, <c>check_boxes</c>, <c>datetime</c>, <c>signature</c>, <c>media</c>,
    /// <c>person</c>, <c>barcode</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// Defines a column in a table form input field.
/// Mirrors the spec schema <c>FormsTableColumnObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsTableColumn
{
    /// <summary>
    /// Unique identifier for the column.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Label of the column.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// Type of the column field. Valid values: `text`, `number`, `datetime`, `check_boxes`,
    /// `multiple_choice`, `signature`, `media`, `person`, `barcode` Valid values: <c>text</c>,
    /// <c>number</c>, <c>datetime</c>, <c>check_boxes</c>, <c>multiple_choice</c>, <c>signature</c>,
    /// <c>media</c>, <c>person</c>, <c>barcode</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// Defines a row in a table form input field.
/// Mirrors the spec schema <c>FormsTableRowObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsTableRow
{
    /// <summary>
    /// List of cells in the row.
    /// </summary>
    [JsonPropertyName("cells")]
    public IReadOnlyList<AlertFormsTableCell>? Cells { get; init; }

    /// <summary>
    /// Unique identifier for the row.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// The value of a table form input field.
/// Mirrors the spec schema <c>FormsTableValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsTableValue
{
    /// <summary>
    /// List of table columns.
    /// </summary>
    [JsonPropertyName("columns")]
    public IReadOnlyList<AlertFormsTableColumn>? Columns { get; init; }

    /// <summary>
    /// List of table rows.
    /// </summary>
    [JsonPropertyName("rows")]
    public IReadOnlyList<AlertFormsTableRow>? Rows { get; init; }
}

/// <summary>
/// The value of a text form input field.
/// Mirrors the spec schema <c>FormsTextValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertFormsTextValue
{
    /// <summary>
    /// Text value.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// Details specific to Fuel Level Percentage.
/// Mirrors the spec schema <c>FuelLevelPercentageResponseBody</c>.
/// </summary>
public sealed record AlertFuelLevelPercentage
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Fuel Level Percentage
/// Mirrors the spec schema <c>FuelLevelTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertFuelLevelTriggerDetails
{
    /// <summary>
    /// The fuel level percentage threshold value.
    /// </summary>
    [JsonPropertyName("fuelLevelPercent")]
    public required long FuelLevelPercent { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }

    /// <summary>
    /// How to evaluate the threshold. Valid values: `LESS` Valid values: <c>LESS</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public required string Operation { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>fuelVolumeResponseBody</c>.
/// </summary>
public sealed record AlertFuelVolume
{
    /// <summary>
    /// Units in which volume is being presented. Valid values: `GALLONS`, `LITERS` Valid values:
    /// <c>GALLONS</c>, <c>LITERS</c>
    /// </summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }

    /// <summary>
    /// The volume of the measured fuel.
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// Details specific to Gateway Disconnected.
/// Mirrors the spec schema <c>GatewayDisconnectedResponseBody</c>.
/// </summary>
public sealed record AlertGatewayDisconnected
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Gateway Disconnected
/// Mirrors the spec schema <c>GatewayDisconnectedDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertGatewayDisconnectedDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting. Can only be either
    /// 900000 (15 minutes) or 3600000 (60 min).
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Details specific to Gateway Unplugged
/// Mirrors the spec schema <c>GatewayUnpluggedTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertGatewayUnpluggedTriggerDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>GatewayWithVehicleTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGatewayWithVehicleTiny
{
    /// <summary>
    /// The model of the gateway installed on the asset. Valid values: `AG15`, `AG24`, `AG24EU`, `AG26`,
    /// `AG26EU`, `AG41`, `AG41EU`, `AG45`, `AG45EU`, `AG46`, `AG46EU`, `AG46P`, `AG46PEU`, `AG51`,
    /// `AG51EU`, `AG52`, `AG52EU`, `AG53`, `AG53EU`, `AT11`, `AT11X`, `AT12`, `AT12X`, `AT13`, `IG15`,
    /// `IG21`, `IG41`,... Valid values: <c>AG15</c>, <c>AG24</c>, <c>AG24EU</c>, <c>AG26</c>,
    /// <c>AG26EU</c>, <c>AG41</c>, <c>AG41EU</c>, <c>AG45</c>, <c>AG45EU</c>, <c>AG46</c>,
    /// <c>AG46EU</c>, <c>AG46P</c>, <c>AG46PEU</c>, <c>AG51</c>, <c>AG51EU</c>, <c>AG52</c>,
    /// <c>AG52EU</c>, <c>AG53</c>, <c>AG53EU</c>, <c>AT11</c>, <c>AT11X</c>, <c>AT12</c>, <c>AT12X</c>,
    /// <c>AT13</c>, <c>IG15</c>, <c>IG21</c>, <c>IG41</c>, <c>IG61</c>, <c>SG1</c>, <c>SG1B</c>,
    /// <c>SG1G</c>, <c>SG1G32</c>, <c>SG1x</c>, <c>VG32</c>, <c>VG33</c>, <c>VG34</c>, <c>VG34EU</c>,
    /// <c>VG34FN</c>, <c>VG34M</c>, <c>VG54ATT</c>, <c>VG54EU</c>, <c>VG54FN</c>, <c>VG54NA</c>,
    /// <c>VG54NAE</c>, <c>VG54NAH</c>, <c>VG55EU</c>, <c>VG55FN</c>, <c>VG55NA</c>
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    /// <summary>
    /// The serial number of the gateway installed on the asset.
    /// </summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertGoaVehicleTiny? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Geofence Entry
/// Mirrors the spec schema <c>GeofenceEntryTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertGeofenceEntryTriggerDetails
{
    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public required AlertLocation Location { get; init; }
}

/// <summary>
/// Details specific to Geofence Exit
/// Mirrors the spec schema <c>GeofenceExitTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertGeofenceExitTriggerDetails
{
    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public required AlertLocation Location { get; init; }
}

/// <summary>
/// A minified Address object
/// Mirrors the spec schema <c>GoaAddressTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaAddressTiny
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// Id of the address
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the address
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Attribute properties.
/// Mirrors the spec schema <c>GoaAttributeTinyResponseBody</c>.
/// </summary>
public sealed record AlertGoaAttributeTiny
{
    /// <summary>
    /// List of date values associated with the attribute (RFC 3339 full-date format: YYYY-MM-DD).
    /// </summary>
    [JsonPropertyName("dateValues")]
    public IReadOnlyList<string>? DateValues { get; init; }

    /// <summary>
    /// Id of the attribute
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the attribute
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// List of number values associated with the attribute
    /// </summary>
    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    /// <summary>
    /// List of string values associated with the attribute.
    /// </summary>
    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }
}

/// <summary>
/// A minified driver object. This object is only returned if the route is assigned to the driver.
/// Mirrors the spec schema <c>GoaDriverTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaDriverTiny
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of the driver
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the driver
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A minified form object.
/// Mirrors the spec schema <c>GoaFormTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaFormTiny
{
    /// <summary>
    /// ID of the form
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// A minified gateway object
/// Mirrors the spec schema <c>GoaGatewayTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaGatewayTiny
{
    /// <summary>
    /// The model of the gateway installed on the asset. Valid values: `AG15`, `AG24`, `AG24EU`, `AG26`,
    /// `AG26EU`, `AG41`, `AG41EU`, `AG45`, `AG45EU`, `AG46`, `AG46EU`, `AG46P`, `AG46PEU`, `AG51`,
    /// `AG51EU`, `AG52`, `AG52EU`, `AG53`, `AG53EU`, `AT11`, `AT11X`, `AT12`, `AT12X`, `AT13`, `IG15`,
    /// `IG21`, `IG41`,... Valid values: <c>AG15</c>, <c>AG24</c>, <c>AG24EU</c>, <c>AG26</c>,
    /// <c>AG26EU</c>, <c>AG41</c>, <c>AG41EU</c>, <c>AG45</c>, <c>AG45EU</c>, <c>AG46</c>,
    /// <c>AG46EU</c>, <c>AG46P</c>, <c>AG46PEU</c>, <c>AG51</c>, <c>AG51EU</c>, <c>AG52</c>,
    /// <c>AG52EU</c>, <c>AG53</c>, <c>AG53EU</c>, <c>AT11</c>, <c>AT11X</c>, <c>AT12</c>, <c>AT12X</c>,
    /// <c>AT13</c>, <c>IG15</c>, <c>IG21</c>, <c>IG41</c>, <c>IG61</c>, <c>SG1</c>, <c>SG1B</c>,
    /// <c>SG1G</c>, <c>SG1G32</c>, <c>SG1x</c>, <c>VG32</c>, <c>VG33</c>, <c>VG34</c>, <c>VG34EU</c>,
    /// <c>VG34FN</c>, <c>VG34M</c>, <c>VG54ATT</c>, <c>VG54EU</c>, <c>VG54FN</c>, <c>VG54NA</c>,
    /// <c>VG54NAE</c>, <c>VG54NAH</c>, <c>VG55EU</c>, <c>VG55FN</c>, <c>VG55NA</c>
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    /// <summary>
    /// The serial number of the gateway installed on the asset.
    /// </summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }
}

/// <summary>
/// A minified issue object.
/// Mirrors the spec schema <c>GoaIssueTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaIssueTiny
{
    /// <summary>
    /// ID of the issue
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// A minified route stop object
/// Mirrors the spec schema <c>GoaRouteStopTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaRouteStopTiny
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// Id of the route stop
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the route stop
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A minified representation of a single route.
/// Mirrors the spec schema <c>GoaRouteTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaRouteTiny
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// Unique identifier for the route.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the route.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A minified trailer object
/// Mirrors the spec schema <c>GoaTrailerTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaTrailerTiny
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of the trailer
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the trailer
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A minified vehicle object. This object is only returned if the route is assigned to the vehicle.
/// Mirrors the spec schema <c>GoaVehicleTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertGoaVehicleTiny
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of the vehicle
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the vehicle
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Details specific to GPS Signal Loss.
/// Mirrors the spec schema <c>GpsSignalLossResponseBody</c>.
/// </summary>
public sealed record AlertGpsSignalLoss
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to GPS Signal Loss
/// Mirrors the spec schema <c>GpsSignalLossDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertGpsSignalLossDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Details specific to HOS Violation
/// Mirrors the spec schema <c>HOSViolationTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertHOSViolationTriggerDetails
{
    /// <summary>
    /// Alert if driver has this specified time until driving causes an HOS violation.
    /// </summary>
    [JsonPropertyName("maxUntilViolationMilliseconds")]
    public required long MaxUntilViolationMilliseconds { get; init; }

    /// <summary>
    /// The type of HOS violation. Valid values: `CaliforniaMealbreakMissed`, `CycleHoursOn`,
    /// `DailyDrivingHours`, `DailyOnDutyHours`, `Invalid`, `RestbreakMissed`, `ShiftDrivingHours`,
    /// `ShiftHours`, `ShiftOnDutyHours`, `UnsubmittedLogs` Valid values:
    /// <c>CaliforniaMealbreakMissed</c>, <c>CycleHoursOn</c>, <c>DailyDrivingHours</c>,
    /// <c>DailyOnDutyHours</c>, <c>Invalid</c>, <c>RestbreakMissed</c>, <c>ShiftDrivingHours</c>,
    /// <c>ShiftHours</c>, <c>ShiftOnDutyHours</c>, <c>UnsubmittedLogs</c>
    /// </summary>
    [JsonPropertyName("violation")]
    public required string Violation { get; init; }
}

/// <summary>
/// Details specific to Harsh Event.
/// Mirrors the spec schema <c>HarshEventDataResponseBody</c>.
/// </summary>
public sealed record AlertHarshEventData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Harsh Events
/// Mirrors the spec schema <c>HarshEventTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertHarshEventTriggerDetails
{
    /// <summary>
    /// On which harsh events to trigger on. Valid values: <c>haAccel</c>, <c>haBraking</c>,
    /// <c>haCameraMisaligned</c>, <c>haCrash</c>, <c>haDistractedDriving</c>,
    /// <c>haDistractedDrivingCalibration</c>, <c>haDrinkPolicy</c>, <c>haDriverObstructionPolicy</c>,
    /// <c>haDrowsinessDetection</c>, <c>haEvent</c>, <c>haFalsePositive</c>, <c>haFoodPolicy</c>,
    /// <c>haHighSpeedSuddenDisconnect</c>, <c>haImpact</c>, <c>haInvalid</c>, <c>haLaneDeparture</c>,
    /// <c>haMaskPolicy</c>, <c>haNearCollision</c>, <c>haOutwardObstructionPolicy</c>,
    /// <c>haPassengerPolicy</c>, <c>haPersonalProtectiveEquipment</c>, <c>haPhonePolicy</c>,
    /// <c>haPolicyDetector</c>, <c>haProximityWarning</c>, <c>haRearCollisionWarning</c>,
    /// <c>haRedLightViolation</c>, <c>haReversing</c>, <c>haRolledStopSign</c>, <c>haRollover</c>,
    /// <c>haRolloverProtectionBrakeControlActivated</c>,
    /// <c>haRolloverProtectionEngineControlActivated</c>, <c>haSeatbeltPolicy</c>, <c>haSharpTurn</c>,
    /// <c>haSignDetection</c>, <c>haSmokingPolicy</c>, <c>haSpeeding</c>, <c>haTailgating</c>,
    /// <c>haTileRollingRailroadCrossing</c>, <c>haTileRollingStopSign</c>,
    /// <c>haTrafficLightDetection</c>, <c>haUnsafeParking</c>, <c>haVehicleInBlindSpotWarning</c>,
    /// <c>haVulnerableRoadUserCollisionWarning</c>, <c>haYawControlBrakeControlActivated</c>,
    /// <c>haYawControlEngineControlActivated</c>
    /// </summary>
    [JsonPropertyName("types")]
    public required IReadOnlyList<string> Types { get; init; }
}

/// <summary>
/// Details specific to HOS Duty Status.
/// Mirrors the spec schema <c>HosDutyStatusDataResponseBody</c>.
/// </summary>
public sealed record AlertHosDutyStatusData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }
}

/// <summary>
/// Details specific to Hos Violation.
/// Mirrors the spec schema <c>HosViolationDataResponseBody</c>.
/// </summary>
public sealed record AlertHosViolationData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }
}

/// <summary>
/// Options for in-app notifications
/// Mirrors the spec schema <c>InAppNotificationOptionsObjectResponseBody</c>.
/// </summary>
public sealed record AlertInAppNotificationOptions
{
    /// <summary>
    /// Whether the alert will dictate the title of the alert. Both canDictateAlertTitle and
    /// canPlayAlertSound should be enabled or disabled together.
    /// </summary>
    [JsonPropertyName("canDictateAlertTitle")]
    public bool? CanDictateAlertTitle { get; init; }

    /// <summary>
    /// Whether the alert will play a sound. Both canDictateAlertTitle and canPlayAlertSound should be
    /// enabled or disabled together.
    /// </summary>
    [JsonPropertyName("canPlayAlertSound")]
    public bool? CanPlayAlertSound { get; init; }

    /// <summary>
    /// Custom text to display in the notification (320 character max).
    /// </summary>
    [JsonPropertyName("customText")]
    public string? CustomText { get; init; }

    /// <summary>
    /// Whether in-app notifications are enabled.
    /// </summary>
    [JsonPropertyName("isEnabled")]
    public required bool IsEnabled { get; init; }
}

/// <summary>
/// Details specific to Inactivity.
/// Mirrors the spec schema <c>InactivityResponseBody</c>.
/// </summary>
public sealed record AlertInactivity
{
    /// <summary>
    /// The <c>cameraStream</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cameraStream")]
    public AlertObjectOnvifCameraStream? CameraStream { get; init; }
}

/// <summary>
/// Details specific to Inside Geofence.
/// Mirrors the spec schema <c>InsideGeofenceDataResponseBody</c>.
/// </summary>
public sealed record AlertInsideGeofenceData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Inside Geofence
/// Mirrors the spec schema <c>InsideGeofenceTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertInsideGeofenceTriggerDetails
{
    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public required AlertLocation Location { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Details specific to Issue Created.
/// Mirrors the spec schema <c>IssueCreatedResponseBody</c>.
/// </summary>
public sealed record AlertIssueCreated
{
    /// <summary>
    /// The <c>issue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("issue")]
    public AlertIssueResponse? Issue { get; init; }
}

/// <summary>
/// Issue response object.
/// Mirrors the spec schema <c>IssueResponseObjectResponseBody</c>.
/// </summary>
public sealed record AlertIssueResponse
{
    /// <summary>
    /// The <c>asset</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("asset")]
    public AlertFormsAsset? Asset { get; init; }

    /// <summary>
    /// The <c>assignedTo</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("assignedTo")]
    public AlertFormsPolymorphicUser? AssignedTo { get; init; }

    /// <summary>
    /// Creation time of the issue. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>
    /// Description of the issue. Included if the issue was given a description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Due date of the issue. UTC timestamp in RFC 3339 format. Included if the issue was assigned a
    /// due date.
    /// </summary>
    [JsonPropertyName("dueDate")]
    public DateTimeOffset? DueDate { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of the issue.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The <c>issueSource</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("issueSource")]
    public AlertIssueSource? IssueSource { get; init; }

    /// <summary>
    /// List of media objects for the issue. Included if the issue has media.
    /// </summary>
    [JsonPropertyName("mediaList")]
    public IReadOnlyList<AlertFormsMediaRecord>? MediaList { get; init; }

    /// <summary>
    /// Priority of the issue. Included if the issue was assigned a priority. Valid values: `low`,
    /// `medium`, `high` Valid values: <c>low</c>, <c>medium</c>, <c>high</c>
    /// </summary>
    [JsonPropertyName("priority")]
    public string? Priority { get; init; }

    /// <summary>
    /// Status of the issue. Valid values: `open`, `inProgress`, `resolved`, `dismissed` Valid values:
    /// <c>open</c>, <c>inProgress</c>, <c>resolved</c>, <c>dismissed</c>
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>
    /// Submission time of the issue. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("submittedAtTime")]
    public DateTimeOffset? SubmittedAtTime { get; init; }

    /// <summary>
    /// The <c>submittedBy</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("submittedBy")]
    public AlertFormsPolymorphicUser? SubmittedBy { get; init; }

    /// <summary>
    /// Title of the issue.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Update time of the issue. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// Contains information about where an issue came from.
/// Mirrors the spec schema <c>IssueSourceObjectResponseBody</c>.
/// </summary>
public sealed record AlertIssueSource
{
    /// <summary>
    /// ID of the issue's source object. The format depends on the 'type'. Included if 'type' is not
    /// 'ad-hoc'.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The type of issue source. Valid values: `form`, `ad-hoc` Valid values: <c>form</c>, <c>ad-
    /// hoc</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// Details specific to Jamming Detected.
/// Mirrors the spec schema <c>JammingDetectedResponseBody</c>.
/// </summary>
public sealed record AlertJammingDetected
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Live Sharing Link response object.
/// Mirrors the spec schema <c>LiveSharingLinkResponseObjectResponseBody</c>.
/// </summary>
public sealed record AlertLiveSharingLinkResponse
{
    /// <summary>
    /// Date that this link expires, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("expiresAtTime")]
    public DateTimeOffset? ExpiresAtTime { get; init; }

    /// <summary>
    /// The shareable URL of the vehicle's location.
    /// </summary>
    [JsonPropertyName("liveSharingUrl")]
    public string? LiveSharingUrl { get; init; }

    /// <summary>
    /// Name of the Live Sharing Link.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A location. Polygon and Circle is deprecated, but may be set for old Alerts. At least one location
/// must be selected.
/// Mirrors the spec schema <c>LocationObjectResponseBody</c>.
/// </summary>
public sealed record AlertLocation
{
    /// <summary>
    /// All locations with selected address IDs will trigger.
    /// </summary>
    [JsonPropertyName("addressIds")]
    public IReadOnlyList<string>? AddressIds { get; init; }

    /// <summary>
    /// All locations with the selected address types will trigger. Valid values:
    /// <c>agricultureSource</c>, <c>alertsOnly</c>, <c>authorizedZone</c>, <c>avoidanceZone</c>,
    /// <c>customerSite</c>, <c>industrialSite</c>, <c>inventory</c>, <c>knownGPSJammingZone</c>,
    /// <c>riskZone</c>, <c>shortHaul</c>, <c>unauthorizedZone</c>, <c>undefined</c>, <c>vendor</c>,
    /// <c>workforceSite</c>, <c>yard</c>
    /// </summary>
    [JsonPropertyName("addressTypes")]
    public IReadOnlyList<string>? AddressTypes { get; init; }

    /// <summary>
    /// The <c>circle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("circle")]
    public AlertCircle? Circle { get; init; }

    /// <summary>
    /// The <c>polygon</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("polygon")]
    public AlertPolygon? Polygon { get; init; }

    /// <summary>
    /// All locations with selected tag will trigger.
    /// </summary>
    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }
}

/// <summary>
/// A single route stop for a route.
/// Mirrors the spec schema <c>MinimalRouteStopResponseBody</c>.
/// </summary>
public sealed record AlertMinimalRouteStop
{
    /// <summary>
    /// Actual arrival time, if it exists, for the route stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("actualArrivalTime")]
    public DateTimeOffset? ActualArrivalTime { get; init; }

    /// <summary>
    /// Actual departure time, if it exists, for the route stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("actualDepartureTime")]
    public DateTimeOffset? ActualDepartureTime { get; init; }

    /// <summary>
    /// The time the stop became en-route, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("enRouteTime")]
    public DateTimeOffset? EnRouteTime { get; init; }

    /// <summary>
    /// Estimated time of arrival, if this stop is currently en-route, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("eta")]
    public DateTimeOffset? Eta { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// Unique identifier for the route stop.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The shareable url of the stop's current status.
    /// </summary>
    [JsonPropertyName("liveSharingUrl")]
    public string? LiveSharingUrl { get; init; }

    /// <summary>
    /// Skipped time, if it exists, for the route stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("skippedTime")]
    public DateTimeOffset? SkippedTime { get; init; }

    /// <summary>
    /// The current state of the route stop. Valid values: `unassigned`, `scheduled`, `en route`,
    /// `skipped`, `arrived`, `departed` Valid values: <c>unassigned</c>, <c>scheduled</c>, <c>en
    /// route</c>, <c>skipped</c>, <c>arrived</c>, <c>departed</c>
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }
}

/// <summary>
/// Details specific to Missing DVIR Past Due.
/// Mirrors the spec schema <c>MissingDvirPastDueResponseBody</c>.
/// </summary>
public sealed record AlertMissingDvirPastDue
{
    /// <summary>
    /// The <c>dvir</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dvir")]
    public AlertDvir2SubmissionResponse? Dvir { get; init; }
}

/// <summary>
/// Details specific to Motion Detected.
/// Mirrors the spec schema <c>MotionDetectedResponseBody</c>.
/// </summary>
public sealed record AlertMotionDetected
{
    /// <summary>
    /// The <c>cameraStream</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cameraStream")]
    public AlertObjectOnvifCameraStream? CameraStream { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>multipleChoiceValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertMultipleChoiceValue
{
    /// <summary>
    /// Boolean representing if the choice has been selected.
    /// </summary>
    [JsonPropertyName("selected")]
    public bool? Selected { get; init; }

    /// <summary>
    /// Description of the choice.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// The Asset associated with the alert.
/// Mirrors the spec schema <c>alertObjectAssetResponseBody</c>.
/// </summary>
public sealed record AlertObjectAsset
{
    /// <summary>
    /// List of attributes associated with the entity
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AlertGoaAttributeTiny>? Attributes { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The ID of the asset.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the asset.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The serial number of the gateway installed on the asset.
    /// </summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-and-Tag-
    /// Nesting) associated with the asset.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }

    /// <summary>
    /// The operational context in which the asset interacts with the Samsara system. Examples: Vehicle
    /// (eg: truck, bus...), Trailer (eg: dry van, reefer, flatbed...), Powered Equipment (eg: dozer,
    /// crane...), Unpowered Equipment (eg: container, dumpster...), or Uncategorized. Valid values:
    /// `uncategorized`,... Valid values: <c>uncategorized</c>, <c>trailer</c>, <c>equipment</c>,
    /// <c>unpowered</c>, <c>vehicle</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// A driver associated with the alert
/// Mirrors the spec schema <c>alertObjectDriverResponseBody</c>.
/// </summary>
public sealed record AlertObjectDriver
{
    /// <summary>
    /// List of attributes associated with the entity
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AlertGoaAttributeTiny>? Attributes { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The ID of the driver
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the driver.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-and-Tag-
    /// Nesting) associated with the driver.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }
}

/// <summary>
/// A camera stream associated with the alert.
/// Mirrors the spec schema <c>alertObjectOnvifCameraStreamResponseBody</c>.
/// </summary>
public sealed record AlertObjectOnvifCameraStream
{
    /// <summary>
    /// The <c>cameraDevice</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cameraDevice")]
    public AlertObjectWorkforceCameraDevice? CameraDevice { get; init; }

    /// <summary>
    /// The ID of the camera stream associated with the alert.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the camera stream.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-and-Tag-
    /// Nesting) associated with the camera stream.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }
}

/// <summary>
/// The product associated with the alert
/// Mirrors the spec schema <c>alertObjectProductResponseBody</c>.
/// </summary>
public sealed record AlertObjectProduct
{
    /// <summary>
    /// The short name associated with the product.
    /// </summary>
    [JsonPropertyName("shortName")]
    public string? ShortName { get; init; }
}

/// <summary>
/// A route associated with the alert.
/// Mirrors the spec schema <c>alertObjectRouteResponseBody</c>.
/// </summary>
public sealed record AlertObjectRoute
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The ID of the route.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the route.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A sensor associated with the alert.
/// Mirrors the spec schema <c>alertObjectSensorResponseBody</c>.
/// </summary>
public sealed record AlertObjectSensor
{
    /// <summary>
    /// Thye ID of the sensor associated with the alert
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the sensor.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The Pinned Device ID associated with the alert
    /// </summary>
    [JsonPropertyName("pinnedDeviceId")]
    public string? PinnedDeviceId { get; init; }

    /// <summary>
    /// The <c>product</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("product")]
    public AlertObjectProduct? Product { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-and-Tag-
    /// Nesting) associated with the sensor.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }
}

/// <summary>
/// A site associated with the alert.
/// Mirrors the spec schema <c>alertObjectSitesResponseBody</c>.
/// </summary>
public sealed record AlertObjectSites
{
    /// <summary>
    /// The ID of the site associated with the alert
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the site
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-and-Tag-
    /// Nesting) associated with the Site.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }
}

/// <summary>
/// A trailer associated with the alert
/// Mirrors the spec schema <c>alertObjectTrailerResponseBody</c>.
/// </summary>
public sealed record AlertObjectTrailer
{
    /// <summary>
    /// List of attributes associated with the entity
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AlertGoaAttributeTiny>? Attributes { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The ID of the trailer. This is automatically generated when the trailer is created. It cannot be
    /// changed.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the trailer. This is set by a fleet administrator and will appear in both Samsara’s
    /// cloud dashboard as well as the Samsara Driver mobile app. By default, this name is the serial
    /// number of the Samsara Asset Gateway. It can be set or updated through the Samsara Dashboard or
    /// through the API...
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-and-Tag-
    /// Nesting) associated with the Trailer.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }

    /// <summary>
    /// The serial number of the trailer.
    /// </summary>
    [JsonPropertyName("trailerSerialNumber")]
    public string? TrailerSerialNumber { get; init; }
}

/// <summary>
/// The vehicle associated with the alert.
/// Mirrors the spec schema <c>alertObjectVehicleResponseBody</c>.
/// </summary>
public sealed record AlertObjectVehicle
{
    /// <summary>
    /// List of attributes associated with the entity
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AlertGoaAttributeTiny>? Attributes { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The ID of the vehicle.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the vehicle.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The serial number of the gateway installed on the asset.
    /// </summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    /// <summary>
    /// The <c>staticAssignedDriver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("staticAssignedDriver")]
    public EntityReference? StaticAssignedDriver { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-and-Tag-
    /// Nesting) associated with the vehicle.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }
}

/// <summary>
/// A camera device associated with the alert
/// Mirrors the spec schema <c>alertObjectWorkforceCameraDeviceResponseBody</c>.
/// </summary>
public sealed record AlertObjectWorkforceCameraDevice
{
    /// <summary>
    /// The ID of the camera device associated with the alert
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The name of the camera device
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The list of sites associated with the camera device.
    /// </summary>
    [JsonPropertyName("sites")]
    public IReadOnlyList<AlertObjectSites>? Sites { get; init; }

    /// <summary>
    /// The list of [tags](https://kb.samsara.com/hc/en-us/articles/360026674631-Using-Tags-and-Tag-
    /// Nesting) associated with the camera device.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<AlertTinyTag>? Tags { get; init; }
}

/// <summary>
/// Details specific to Out Of Route.
/// Mirrors the spec schema <c>OutOfRouteResponseBody</c>.
/// </summary>
public sealed record AlertOutOfRoute
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Out Of Route
/// Mirrors the spec schema <c>OutOfRouteDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertOutOfRouteDetails
{
    /// <summary>
    /// The minimum distance in meters a vehicle has to be from its active route path to be considered
    /// out of its route.
    /// </summary>
    [JsonPropertyName("maxOffRouteMeters")]
    public required long MaxOffRouteMeters { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Details specific to Out-of-Sequence Stop Arrival.
/// Mirrors the spec schema <c>OutOfSequenceStopArrivalDataResponseBody</c>.
/// </summary>
public sealed record AlertOutOfSequenceStopArrivalData
{
    /// <summary>
    /// Name of the stop the driver actually arrived at.
    /// </summary>
    [JsonPropertyName("actualStopName")]
    public string? ActualStopName { get; init; }

    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// Name of the stop the driver was expected to arrive at.
    /// </summary>
    [JsonPropertyName("expectedStopName")]
    public string? ExpectedStopName { get; init; }

    /// <summary>
    /// The <c>route</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("route")]
    public AlertObjectRoute? Route { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Outside Geofence.
/// Mirrors the spec schema <c>OutsideGeofenceDataResponseBody</c>.
/// </summary>
public sealed record AlertOutsideGeofenceData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Outside Geofence
/// Mirrors the spec schema <c>OutsideGeofenceTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertOutsideGeofenceTriggerDetails
{
    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public required AlertLocation Location { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Details specific to Panic Button.
/// Mirrors the spec schema <c>PanicButtonResponseBody</c>.
/// </summary>
public sealed record AlertPanicButton
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Panic Button
/// Mirrors the spec schema <c>PanicButtonDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertPanicButtonDetails
{
    /// <summary>
    /// If true, only receive alerts when the panic button is pressed, otherwise receive alerts when the
    /// panic button is pressed or looses connection.
    /// </summary>
    [JsonPropertyName("isFilteringOutPowerLoss")]
    public required bool IsFilteringOutPowerLoss { get; init; }
}

/// <summary>
/// Details specific to Person Detected.
/// Mirrors the spec schema <c>PersonDetectedResponseBody</c>.
/// </summary>
public sealed record AlertPersonDetected
{
    /// <summary>
    /// The <c>cameraStream</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cameraStream")]
    public AlertObjectOnvifCameraStream? CameraStream { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>photoValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertPhotoValue
{
    /// <summary>
    /// Id of the photo.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Url of the photo.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}

/// <summary>
/// Information about a polygon geofence. This field is only needed if the geofence is a polygon.
/// Mirrors the spec schema <c>PolygonResponseBody</c>.
/// </summary>
public sealed record AlertPolygon
{
    /// <summary>
    /// The name of the polygon.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// The vertices of the polygon geofence. These geofence vertices describe the perimeter of the
    /// polygon, and must consist of at least 3 vertices and less than 40.
    /// </summary>
    [JsonPropertyName("vertices")]
    public IReadOnlyList<AlertVertex>? Vertices { get; init; }
}

/// <summary>
/// Details specific to Preventive Maintenance Schedule Due.
/// Mirrors the spec schema <c>PreventiveMaintenanceScheduleDueDataResponseBody</c>.
/// </summary>
public sealed record AlertPreventiveMaintenanceScheduleDueData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The dimension(s) (date, odometer, engine hours) that caused this schedule instance to become
    /// due. Valid values: <c>date</c>, <c>odometer</c>, <c>engineHours</c>
    /// </summary>
    [JsonPropertyName("dueReasons")]
    public IReadOnlyList<string>? DueReasons { get; init; }

    /// <summary>
    /// Description of the preventive maintenance schedule.
    /// </summary>
    [JsonPropertyName("scheduleDescription")]
    public string? ScheduleDescription { get; init; }

    /// <summary>
    /// Unique ID of the preventive maintenance schedule.
    /// </summary>
    [JsonPropertyName("scheduleId")]
    public string? ScheduleId { get; init; }

    /// <summary>
    /// Title of the preventive maintenance schedule.
    /// </summary>
    [JsonPropertyName("scheduleTitle")]
    public string? ScheduleTitle { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Options for push notifications
/// Mirrors the spec schema <c>PushNotificationOptionsObjectResponseBody</c>.
/// </summary>
public sealed record AlertPushNotificationOptions
{
    /// <summary>
    /// Whether push notifications are enabled.
    /// </summary>
    [JsonPropertyName("isEnabled")]
    public required bool IsEnabled { get; init; }
}

/// <summary>
/// Threshold to alert on if reading is continuous, either enum or continuous threshold may be set.
/// Mirrors the spec schema <c>ReadingTriggerContinuousValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertReadingTriggerContinuousValue
{
    /// <summary>
    /// The operation to use when comparing the value to the threshold. Valid values: `GREATER`,
    /// `INSIDE_RANGE`, `LESS`, `OUTSIDE_RANGE` Valid values: <c>GREATER</c>, <c>INSIDE_RANGE</c>,
    /// <c>LESS</c>, <c>OUTSIDE_RANGE</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public required string Operation { get; init; }

    /// <summary>
    /// The lower threshold of criticality.
    /// </summary>
    [JsonPropertyName("threshold")]
    public required long Threshold { get; init; }

    /// <summary>
    /// The unit of the threshold defined by reading type. If not provided base unit of the reading will
    /// be used. Valid values: `ampere`, `bar`, `cad`, `celsius`, `chf`, `day`, `decimaldegrees`, `eur`,
    /// `fahrenheit`, `foot`, `gallon`, `gallonperkilogram`, `gallonsperhour`, `galpermi`, `gbp`,
    /// `gforce`, `gperl... Valid values: <c>ampere</c>, <c>bar</c>, <c>cad</c>, <c>celsius</c>,
    /// <c>chf</c>, <c>day</c>, <c>decimaldegrees</c>, <c>eur</c>, <c>fahrenheit</c>, <c>foot</c>,
    /// <c>gallon</c>, <c>gallonperkilogram</c>, <c>gallonsperhour</c>, <c>galpermi</c>, <c>gbp</c>,
    /// <c>gforce</c>, <c>gperliter</c>, <c>gperm</c>, <c>hertz</c>, <c>hour</c>,
    /// <c>imperialgallonperkilogram</c>, <c>impgallon</c>, <c>impgallonsperhour</c>,
    /// <c>impgalpermi</c>, <c>inch</c>, <c>kelvin</c>, <c>kgper100kmgaseousfuel</c>,
    /// <c>kgpergallon</c>, <c>kgperkm</c>, <c>kgperliter</c>, <c>kgpermi</c>, <c>kilogram</c>,
    /// <c>kilogramgaseousfuel</c>, <c>kilometer</c>, <c>kilopascal</c>, <c>kilowatthour</c>,
    /// <c>kmperhr</c>, <c>kmperl</c>, <c>kmperlgaseousfuel</c>, <c>lbpermi</c>, <c>liter</c>,
    /// <c>litergaseousfuel</c>, <c>literperkilogram</c>, <c>literpertonne</c>, <c>litersperhour</c>,
    /// <c>lper100km</c>, <c>lper100kmgaseousfuel</c>, <c>lperkm</c>, <c>lperm</c>, <c>meter</c>,
    /// <c>meterspersec</c>, <c>mile</c>, <c>milliknot</c>, <c>millisecond</c>, <c>millivolt</c>,
    /// <c>minute</c>, <c>mipergal</c>, <c>miperhr</c>, <c>miperimpgal</c>, <c>month</c>,
    /// <c>mpgusgalgaseousfuel</c>, <c>mpkggaseousfuel</c>, <c>mxn</c>, <c>percent</c>, <c>pound</c>,
    /// <c>poundsPerSquareInch</c>, <c>poundspergallon</c>, <c>poundsperliter</c>, <c>rpm</c>,
    /// <c>second</c>, <c>usd</c>, <c>usgallongaseousfuel</c>, <c>volt</c>, <c>voltAmpere</c>,
    /// <c>voltAmpereReactive</c>, <c>watt</c>, <c>watthour</c>, <c>week</c>
    /// </summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }

    /// <summary>
    /// The upper threshold of criticality. Required for RANGE operations.
    /// </summary>
    [JsonPropertyName("upperThreshold")]
    public long? UpperThreshold { get; init; }
}

/// <summary>
/// Details specific to reading based Trigger, readings can be discovered through the Readings API.
/// Mirrors the spec schema <c>ReadingTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertReadingTriggerDetails
{
    /// <summary>
    /// The <c>continuousThreshold</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("continuousThreshold")]
    public AlertReadingTriggerContinuousValue? ContinuousThreshold { get; init; }

    /// <summary>
    /// The type of the entity associated with the reading.
    /// </summary>
    [JsonPropertyName("entityType")]
    public required string EntityType { get; init; }

    /// <summary>
    /// The <c>enumThreshold</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("enumThreshold")]
    public AlertReadingTriggerEnumValue? EnumThreshold { get; init; }

    /// <summary>
    /// The ID of the reading.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Enum value to alert on if reading is discrete, either enum or continuous threshold may be set.
/// Mirrors the spec schema <c>ReadingTriggerEnumValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertReadingTriggerEnumValue
{
    /// <summary>
    /// The numeric representation of the enum value.
    /// </summary>
    [JsonPropertyName("number")]
    public long? Number { get; init; }

    /// <summary>
    /// The symbol representation of the enum value.
    /// </summary>
    [JsonPropertyName("symbol")]
    public string? Symbol { get; init; }
}

/// <summary>
/// Recipient of an Action. One of userId contactId or roleId needs to be set.
/// Mirrors the spec schema <c>RecipientObjectResponseBody</c>.
/// </summary>
public sealed record AlertRecipient
{
    /// <summary>
    /// The ID of the contact.
    /// </summary>
    [JsonPropertyName("contactId")]
    public string? ContactId { get; init; }

    /// <summary>
    /// How the user/contact/role should be notified. Valid values: <c>push</c>, <c>sms</c>,
    /// <c>email</c>
    /// </summary>
    [JsonPropertyName("notificationTypes")]
    public IReadOnlyList<string>? NotificationTypes { get; init; }

    /// <summary>
    /// The ID of the role.
    /// </summary>
    [JsonPropertyName("roleId")]
    public string? RoleId { get; init; }

    /// <summary>
    /// The type of recipients Valid values: `user`, `contact`, `role` Valid values: <c>user</c>,
    /// <c>contact</c>, <c>role</c>
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>
    /// The ID of the user.
    /// </summary>
    [JsonPropertyName("userId")]
    public string? UserId { get; init; }
}

/// <summary>
/// Details specific to Reefer Temperature.
/// Mirrors the spec schema <c>ReeferTemperatureResponseBody</c>.
/// </summary>
public sealed record AlertReeferTemperature
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// An optional dictionary, only necessary to override the defaults for route start and end conditions.
/// Mirrors the spec schema <c>RouteSettingsResponseBody</c>.
/// </summary>
public sealed record AlertRouteSettings
{
    /// <summary>
    /// Defaults to 'arriveLastStop' which ends the route upon arriving at the final stop. The condition
    /// 'departLastStop' ends the route upon departing the last stop. If 'arriveLastStop' is set, then
    /// the departure time of the final stop should not be set. Valid values: `arriveLastStop`,
    /// `departLastStop` Valid values: <c>arriveLastStop</c>, <c>departLastStop</c>
    /// </summary>
    [JsonPropertyName("routeCompletionCondition")]
    public string? RouteCompletionCondition { get; init; }

    /// <summary>
    /// Defaults to 'departFirstStop' which starts the route upon departing the first stop in the route.
    /// The condition 'arriveFirstStop' starts the route upon arriving at the first stop in the route.
    /// If 'departFirstStop' is set, the arrival time of the first stop should not be set. Valid values:
    /// `departFirs... Valid values: <c>departFirstStop</c>, <c>arriveFirstStop</c>
    /// </summary>
    [JsonPropertyName("routeStartingCondition")]
    public string? RouteStartingCondition { get; init; }

    /// <summary>
    /// Determines how stops are sequenced on the route. 'scheduledArrivalTime' sequences stops by their
    /// scheduled arrival times (default). 'manual' allows custom sequencing via stop.sequenceNumber.
    /// 'unknown' indicates the method is not specified. Valid values: `unknown`,
    /// `scheduledArrivalTime`, `manual` Valid values: <c>unknown</c>, <c>scheduledArrivalTime</c>,
    /// <c>manual</c>
    /// </summary>
    [JsonPropertyName("sequencingMethod")]
    public string? SequencingMethod { get; init; }
}

/// <summary>
/// Details specific to Route Start Delayed.
/// Mirrors the spec schema <c>RouteStartDelayedDataResponseBody</c>.
/// </summary>
public sealed record AlertRouteStartDelayedData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>RouteStopAppointmentWindowResponseBody</c>.
/// </summary>
public sealed record AlertRouteStopAppointmentWindow
{
    /// <summary>
    /// The end time of the appointment window for the stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    /// <summary>
    /// The start time of the appointment window for the stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>RouteStopDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertRouteStopDetails
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertGoaDriverTiny? Driver { get; init; }

    /// <summary>
    /// The operation that was performed as part of this route update. Valid values: `stop arrived`,
    /// `stop departed` Valid values: <c>stop arrived</c>, <c>stop departed</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public string? Operation { get; init; }

    /// <summary>
    /// The <c>route</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("route")]
    public AlertWebhookRouteResponse? Route { get; init; }

    /// <summary>
    /// The <c>routeStopDetails</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("routeStopDetails")]
    public AlertMinimalRouteStop? RouteStopDetails { get; init; }

    /// <summary>
    /// The timestamp of the route in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    /// <summary>
    /// The type of route update. The route tracking updates occur as a route is completed and stops
    /// transition from one state to another. Currently only Route Tracking updates are supported, but
    /// this will change in the future when additional types are added. Valid values: `route tracking`
    /// Valid values: <c>route tracking</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertVehicleWithGatewayTiny? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Route Stop ETA.
/// Mirrors the spec schema <c>RouteStopETAResponseBody</c>.
/// </summary>
public sealed record AlertRouteStopETA
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Route Stop Early/Late Arrival.
/// Mirrors the spec schema <c>RouteStopEarlyLateArrivalDataResponseBody</c>.
/// </summary>
public sealed record AlertRouteStopEarlyLateArrivalData
{
    /// <summary>
    /// Whether the vehicle arrived early or late relative to the scheduled time. Valid values: `early`,
    /// `late` Valid values: <c>early</c>, <c>late</c>
    /// </summary>
    [JsonPropertyName("arrivalStatus")]
    public string? ArrivalStatus { get; init; }

    /// <summary>
    /// The absolute deviation in minutes from the scheduled arrival time. Always positive. Use
    /// arrivalStatus to determine if early or late.
    /// </summary>
    [JsonPropertyName("deviationMinutes")]
    public long? DeviationMinutes { get; init; }

    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Route Stop Estimated Arrival
/// Mirrors the spec schema <c>RouteStopEstimatedArrivalDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertRouteStopEstimatedArrivalDetails
{
    /// <summary>
    /// Time threshold for when to send an alert. Sends an alert when the ETA is less than the
    /// threshold.
    /// </summary>
    [JsonPropertyName("alertBeforeArrivalMilliseconds")]
    public required long AlertBeforeArrivalMilliseconds { get; init; }

    /// <summary>
    /// If true, will include a live sharing link in the alert. Defaults to false.
    /// </summary>
    [JsonPropertyName("hasLiveShareLink")]
    public bool? HasLiveShareLink { get; init; }

    /// <summary>
    /// If true, will only alert if the vehicle is en-route to the stop. Defaults to false.
    /// </summary>
    [JsonPropertyName("isAlertOnRouteStopOnly")]
    public bool? IsAlertOnRouteStopOnly { get; init; }

    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public required AlertLocation Location { get; init; }
}

/// <summary>
/// This field is used to indicate stops along the route for which an address has not been persisted.
/// This field is mutually exclusive with addressId.
/// Mirrors the spec schema <c>RoutesSingleUseAddressObjectResponseBody</c>.
/// </summary>
public sealed record AlertRoutesSingleUseAddress
{
    /// <summary>
    /// Address of the stop.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>
    /// The latitude of the location
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// The longitude of the location
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>
    /// The radius in meters for the geofence around this location. Must be a positive value.
    /// </summary>
    [JsonPropertyName("radiusMeters")]
    public double? RadiusMeters { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>RoutesStopResponseObjectResponseBody</c>.
/// </summary>
public sealed record AlertRoutesStopResponse
{
    /// <summary>
    /// Actual arrival time, if it exists, for the route stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("actualArrivalTime")]
    public DateTimeOffset? ActualArrivalTime { get; init; }

    /// <summary>
    /// Actual departure time, if it exists, for the route stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("actualDepartureTime")]
    public DateTimeOffset? ActualDepartureTime { get; init; }

    /// <summary>
    /// Actual GPS-measured distance traveled from the previous stop's departure to this stop's arrival,
    /// in meters. Null for the first stop, skipped stops, or if GPS data is unavailable.
    /// </summary>
    [JsonPropertyName("actualDistanceMeters")]
    public long? ActualDistanceMeters { get; init; }

    /// <summary>
    /// The <c>address</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("address")]
    public AlertGoaAddressTiny? Address { get; init; }

    /// <summary>
    /// Appointment windows for the stop.
    /// </summary>
    [JsonPropertyName("appointmentWindows")]
    public IReadOnlyList<AlertRouteStopAppointmentWindow>? AppointmentWindows { get; init; }

    /// <summary>
    /// List of documents associated with the stop.
    /// </summary>
    [JsonPropertyName("documents")]
    public IReadOnlyList<EntityReference>? Documents { get; init; }

    /// <summary>
    /// The time the stop became en-route, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("enRouteTime")]
    public DateTimeOffset? EnRouteTime { get; init; }

    /// <summary>
    /// Estimated time of arrival, if this stop is currently en-route, in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("eta")]
    public DateTimeOffset? Eta { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// List of forms associated with the stop.
    /// </summary>
    [JsonPropertyName("forms")]
    public IReadOnlyList<AlertGoaFormTiny>? Forms { get; init; }

    /// <summary>
    /// Id of the stop
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// List of issues associated with the stop.
    /// </summary>
    [JsonPropertyName("issues")]
    public IReadOnlyList<AlertGoaIssueTiny>? Issues { get; init; }

    /// <summary>
    /// The shareable url of the stop's current status.
    /// </summary>
    [JsonPropertyName("liveSharingUrl")]
    public string? LiveSharingUrl { get; init; }

    /// <summary>
    /// List of shareable, non-expired 'By Location' Live Sharing Links.
    /// </summary>
    [JsonPropertyName("locationLiveSharingLinks")]
    public IReadOnlyList<AlertLiveSharingLinkResponse>? LocationLiveSharingLinks { get; init; }

    /// <summary>
    /// Name of the stop
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Notes for the stop
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>
    /// Specifies the time window (in milliseconds) after a stop's scheduled arrival time during which
    /// the stop is considered 'on-time'.
    /// </summary>
    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    /// <summary>
    /// Specifies the time window (in milliseconds) before a stop's scheduled arrival time during which
    /// the stop is considered 'on-time'.
    /// </summary>
    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }

    /// <summary>
    /// Planned driving distance from the previous stop in meters. Based on routing calculations at
    /// route creation time. Null for the first stop or if routing data is unavailable.
    /// </summary>
    [JsonPropertyName("plannedDistanceMeters")]
    public long? PlannedDistanceMeters { get; init; }

    /// <summary>
    /// Scheduled arrival time, if it exists, for the stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    /// <summary>
    /// Scheduled departure time, if it exists, for the stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    /// <summary>
    /// Manual sequence position of this stop. Only used when route.settings.sequencingMethod=manual.
    /// </summary>
    [JsonPropertyName("sequenceNumber")]
    public long? SequenceNumber { get; init; }

    /// <summary>
    /// The <c>singleUseLocation</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("singleUseLocation")]
    public AlertRoutesSingleUseAddress? SingleUseLocation { get; init; }

    /// <summary>
    /// Skipped time, if it exists, for the route stop in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("skippedTime")]
    public DateTimeOffset? SkippedTime { get; init; }

    /// <summary>
    /// The current state of the route stop. Valid values: `unassigned`, `scheduled`, `en route`,
    /// `skipped`, `arrived`, `departed` Valid values: <c>unassigned</c>, <c>scheduled</c>, <c>en
    /// route</c>, <c>skipped</c>, <c>arrived</c>, <c>departed</c>
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }
}

/// <summary>
/// Details specific to Safety Behavior
/// Mirrors the spec schema <c>SafetyBehaviorTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertSafetyBehaviorTriggerDetails
{
    /// <summary>
    /// The <c>behaviorCount</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("behaviorCount")]
    public AlertBehaviorCountDetails? BehaviorCount { get; init; }

    /// <summary>
    /// On which safety behaviors to trigger on. Valid values: <c>Acceleration</c>,
    /// <c>AggressiveDriving</c>, <c>BluetoothHeadset</c>, <c>Braking</c>,
    /// <c>ContextConstructionOrWorkZone</c>, <c>ContextSnowyOrIcy</c>,
    /// <c>ContextVulnerableRoadUser</c>, <c>ContextWet</c>, <c>Crash</c>, <c>CustomerCustomEvent1</c>,
    /// <c>CustomerCustomEvent10</c>, <c>CustomerCustomEvent2</c>, <c>CustomerCustomEvent3</c>,
    /// <c>CustomerCustomEvent4</c>, <c>CustomerCustomEvent5</c>, <c>CustomerCustomEvent6</c>,
    /// <c>CustomerCustomEvent7</c>, <c>CustomerCustomEvent8</c>, <c>CustomerCustomEvent9</c>,
    /// <c>DefensiveDriving</c>, <c>DidNotYield</c>, <c>Drinking</c>, <c>Drowsy</c>, <c>Eating</c>,
    /// <c>EatingDrinking</c>, <c>EdgeDistractedDriving</c>, <c>EdgeRailroadCrossingViolation</c>,
    /// <c>FollowingDistance</c>, <c>FollowingDistanceModerate</c>, <c>FollowingDistanceSevere</c>,
    /// <c>ForwardCollisionWarning</c>, <c>GenericDistraction</c>, <c>GenericTailgating</c>,
    /// <c>HarshImpact</c>, <c>HarshTurn</c>, <c>HeavySpeeding</c>, <c>HighSpeedSuddenDisconnect</c>,
    /// <c>HosViolation</c>, <c>Idling</c>, <c>Invalid</c>, <c>LaneDeparture</c>, <c>LateResponse</c>,
    /// <c>LeftTurn</c>, <c>LightSpeeding</c>, <c>MaxSpeed</c>, <c>MobileUsage</c>,
    /// <c>ModerateSpeeding</c>, <c>NearCollison</c>, <c>NearPedestrianCollision</c>, <c>NoSeatbelt</c>,
    /// <c>ObstructedCamera</c>, <c>OtherViolation</c>, <c>Passenger</c>, <c>PolicyViolationMask</c>,
    /// <c>ProtectiveEquipment</c>, <c>ProximityWarning</c>, <c>RanRedLight</c>, <c>Reversing</c>,
    /// <c>RollingStop</c>, <c>RolloverProtection</c>, <c>SamsaraCustomEvent1</c>,
    /// <c>SamsaraCustomEvent10</c>, <c>SamsaraCustomEvent2</c>, <c>SamsaraCustomEvent3</c>,
    /// <c>SamsaraCustomEvent4</c>, <c>SamsaraCustomEvent5</c>, <c>SamsaraCustomEvent6</c>,
    /// <c>SamsaraCustomEvent7</c>, <c>SamsaraCustomEvent8</c>, <c>SamsaraCustomEvent9</c>,
    /// <c>SevereSpeeding</c>, <c>Smoking</c>, <c>Speeding</c>, <c>UTurn</c>, <c>UnsafeManeuver</c>,
    /// <c>UnsafeParking</c>, <c>VehicleInBlindSpotWarning</c>,
    /// <c>VulnerableRoadUserCollisionWarning</c>, <c>YawControl</c>
    /// </summary>
    [JsonPropertyName("behaviors")]
    public required IReadOnlyList<string> Behaviors { get; init; }

    /// <summary>
    /// The <c>drivers</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("drivers")]
    public AlertDriverOrTagIdsDetails? Drivers { get; init; }

    /// <summary>
    /// The <c>safetyScore</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("safetyScore")]
    public AlertSafetyScoreDetails? SafetyScore { get; init; }

    /// <summary>
    /// On which event severity to trigger on. Valid values: <c>low</c>, <c>medium</c>, <c>high</c>
    /// </summary>
    [JsonPropertyName("severities")]
    public IReadOnlyList<string>? Severities { get; init; }

    /// <summary>
    /// On which event status to trigger on. Valid values: <c>NEEDS_COACHING</c>,
    /// <c>NEEDS_RECOGNITION</c>, <c>NEEDS_REVIEW</c>, <c>RECOGNIZED</c>, <c>REVIEWED</c>
    /// </summary>
    [JsonPropertyName("statuses")]
    public IReadOnlyList<string>? Statuses { get; init; }
}

/// <summary>
/// Trigger when safety score meets the specified condition.
/// Mirrors the spec schema <c>SafetyScoreDetailsResponseBody</c>.
/// </summary>
public sealed record AlertSafetyScoreDetails
{
    /// <summary>
    /// The comparison to use when comparing the value to the threshold. Valid values: `EQUAL_TO`,
    /// `GREATER_THAN`, `GREATER_THAN_OR_EQUAL_TO`, `LESS_THAN`, `LESS_THAN_OR_EQUAL_TO` Valid values:
    /// <c>EQUAL_TO</c>, <c>GREATER_THAN</c>, <c>GREATER_THAN_OR_EQUAL_TO</c>, <c>LESS_THAN</c>,
    /// <c>LESS_THAN_OR_EQUAL_TO</c>
    /// </summary>
    [JsonPropertyName("comparison")]
    public required string Comparison { get; init; }

    /// <summary>
    /// The score to compare to.
    /// </summary>
    [JsonPropertyName("score")]
    public required int Score { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>scannedDocumentValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertScannedDocumentValue
{
    /// <summary>
    /// Id of the scanned document.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Url of the scanned document.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}

/// <summary>
/// Details specific to Scheduled Maintenance By Engine Hours.
/// Mirrors the spec schema <c>ScheduledMaintenanceByEngineHoursResponseBody</c>.
/// </summary>
public sealed record AlertScheduledMaintenanceByEngineHours
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Scheduled Maintenance By Engine Hours
/// Mirrors the spec schema <c>ScheduledMaintenanceByEngineHoursDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertScheduledMaintenanceByEngineHoursDetails
{
    /// <summary>
    /// Alert when maintenance is due in the specified number of hours.
    /// </summary>
    [JsonPropertyName("dueInHours")]
    public required long DueInHours { get; init; }

    /// <summary>
    /// The id of the maintenance schedule.
    /// </summary>
    [JsonPropertyName("scheduleId")]
    public required string ScheduleId { get; init; }
}

/// <summary>
/// Details specific to Scheduled Maintenance.
/// Mirrors the spec schema <c>ScheduledMaintenanceDataResponseBody</c>.
/// </summary>
public sealed record AlertScheduledMaintenanceData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Scheduled Maintenance By Odometer.
/// Mirrors the spec schema <c>ScheduledMaintenanceOdometerDataResponseBody</c>.
/// </summary>
public sealed record AlertScheduledMaintenanceOdometerData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Scheduled Maintenance by Odometer
/// Mirrors the spec schema <c>ScheduledMaintenanceOdometerTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertScheduledMaintenanceOdometerTriggerDetails
{
    /// <summary>
    /// Alert when vehicle odometer has this many meters left until maintenance is due.
    /// </summary>
    [JsonPropertyName("dueInMeters")]
    public required long DueInMeters { get; init; }

    /// <summary>
    /// The id of the maintenance schedule.
    /// </summary>
    [JsonPropertyName("scheduleId")]
    public required string ScheduleId { get; init; }
}

/// <summary>
/// Details specific to Scheduled Maintenance by Date
/// Mirrors the spec schema <c>ScheduledMaintenanceTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertScheduledMaintenanceTriggerDetails
{
    /// <summary>
    /// Alert when maintenance is due in the specified number of days.
    /// </summary>
    [JsonPropertyName("dueInDays")]
    public required long DueInDays { get; init; }

    /// <summary>
    /// The id of the maintenance schedule.
    /// </summary>
    [JsonPropertyName("scheduleId")]
    public required string ScheduleId { get; init; }
}

/// <summary>
/// Information about a geofence settings.
/// Mirrors the spec schema <c>SettingsResponseBody</c>.
/// </summary>
public sealed record AlertSettings
{
    /// <summary>
    /// The geofence setting. If this setting set to true, then underlying geofence addresses will be
    /// shown in reports instead of a geofence's name.
    /// </summary>
    [JsonPropertyName("showAddresses")]
    public IReadOnlyList<AlertVertex>? ShowAddresses { get; init; }
}

/// <summary>
/// The start of a severe speeding event
/// Mirrors the spec schema <c>SevereSpeedingStartedObjectResponseBody</c>.
/// </summary>
public sealed record AlertSevereSpeedingStarted
{
    /// <summary>
    /// The speeding start time in RFC 3339 format (Examples: 2019-06-13T19:08:25Z,
    /// 2019-06-13T19:08:25.455Z, OR 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>
    /// The trip start time in RFC 3339 format (Examples: 2019-06-13T19:08:25Z,
    /// 2019-06-13T19:08:25.455Z, OR 2015-09-15T14:00:12-04:00).
    /// </summary>
    [JsonPropertyName("tripStartTime")]
    public DateTimeOffset? TripStartTime { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public WorkflowVehicle? Vehicle { get; init; }
}

/// <summary>
/// The start of a severe speeding event
/// Mirrors the spec schema <c>SevereSpeedingStartedResponseObjectResponseBody</c>.
/// </summary>
public sealed record AlertSevereSpeedingStartedResponse
{
    /// <summary>
    /// The <c>data</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("data")]
    public AlertSevereSpeedingStarted? Data { get; init; }
}

/// <summary>
/// The value of a signature field. Only present for signature fields.
/// Mirrors the spec schema <c>signatureValueObjectResponseBody</c>.
/// </summary>
public sealed record AlertSignatureValue
{
    /// <summary>
    /// Id of the signature field.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the signee for a signature field.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Time the signature was captured in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("signedAtTime")]
    public DateTimeOffset? SignedAtTime { get; init; }

    /// <summary>
    /// Url of a signature field's PNG signature image.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}

/// <summary>
/// Details specific to Site Gateway Disconnected.
/// Mirrors the spec schema <c>SiteGatewayDisconnectedResponseBody</c>.
/// </summary>
public sealed record AlertSiteGatewayDisconnected
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// A specific vehicle fault code.
/// Mirrors the spec schema <c>SpecificVehicleFaultCodeObjectResponseBody</c>.
/// </summary>
public sealed record AlertSpecificVehicleFaultCode
{
    /// <summary>
    /// The specific fault code name.
    /// </summary>
    [JsonPropertyName("faultCode")]
    public required string FaultCode { get; init; }

    /// <summary>
    /// The specific fault code type. Valid values: `INVALID_FAULT_CODE_TYPE`, `J1939_DTC`, `J1939_SPN`,
    /// `PASSENGER_DTC` Valid values: <c>INVALID_FAULT_CODE_TYPE</c>, <c>J1939_DTC</c>,
    /// <c>J1939_SPN</c>, <c>PASSENGER_DTC</c>
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

/// <summary>
/// Details specific to Speed.
/// Mirrors the spec schema <c>SpeedDataResponseBody</c>.
/// </summary>
public sealed record AlertSpeedData
{
    /// <summary>
    /// Current speed of the vehicle in kilometers per hour.
    /// </summary>
    [JsonPropertyName("currentSpeedKilometersPerHour")]
    public int? CurrentSpeedKilometersPerHour { get; init; }

    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// Minimum duration of the current speed in milliseconds.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public long? MinDurationMilliseconds { get; init; }

    /// <summary>
    /// Operation of the current and threshold comparison. Valid values: `GREATER`, `INSIDE_RANGE`,
    /// `LESS`, `OUTSIDE_RANGE` Valid values: <c>GREATER</c>, <c>INSIDE_RANGE</c>, <c>LESS</c>,
    /// <c>OUTSIDE_RANGE</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public string? Operation { get; init; }

    /// <summary>
    /// Threshold speed of the vehicle in kilometers per hour.
    /// </summary>
    [JsonPropertyName("thresholdSpeedKilometersPerHour")]
    public int? ThresholdSpeedKilometersPerHour { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Speed
/// Mirrors the spec schema <c>SpeedTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertSpeedTriggerDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }

    /// <summary>
    /// How to evaluate the threshold. Valid values: `GREATER`, `LESS` Valid values: <c>GREATER</c>,
    /// <c>LESS</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public required string Operation { get; init; }

    /// <summary>
    /// The speed threshold value.
    /// </summary>
    [JsonPropertyName("speedKilometersPerHour")]
    public required long SpeedKilometersPerHour { get; init; }
}

/// <summary>
/// Details specific to Sudden Fuel Level Drop.
/// Mirrors the spec schema <c>SuddenFuelLevelDropResponseBody</c>.
/// </summary>
public sealed record AlertSuddenFuelLevelDrop
{
    /// <summary>
    /// The end time of the fuel level change in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("changeEndTime")]
    public DateTimeOffset? ChangeEndTime { get; init; }

    /// <summary>
    /// The start time of the fuel level change in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("changeStartTime")]
    public DateTimeOffset? ChangeStartTime { get; init; }

    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The fuel level after the sudden fuel level drop in millipercents.
    /// </summary>
    [JsonPropertyName("fuelLevelAfterMillipercent")]
    public int? FuelLevelAfterMillipercent { get; init; }

    /// <summary>
    /// The fuel level before the sudden fuel level drop in millipercents.
    /// </summary>
    [JsonPropertyName("fuelLevelBeforeMillipercent")]
    public int? FuelLevelBeforeMillipercent { get; init; }

    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public AlertEventLocation? Location { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Sudden Fuel Level Drop
/// Mirrors the spec schema <c>SuddenFuelLevelDropTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertSuddenFuelLevelDropTriggerDetails
{
    /// <summary>
    /// The minimum fuel level change in percents to trigger on. Need to be between 5 to 100.
    /// </summary>
    [JsonPropertyName("minFuelLevelChangeInPercents")]
    public required long MinFuelLevelChangeInPercents { get; init; }
}

/// <summary>
/// Details specific to Sudden Fuel Level Rise.
/// Mirrors the spec schema <c>SuddenFuelLevelRiseResponseBody</c>.
/// </summary>
public sealed record AlertSuddenFuelLevelRise
{
    /// <summary>
    /// The end time of the fuel level change in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("changeEndTime")]
    public DateTimeOffset? ChangeEndTime { get; init; }

    /// <summary>
    /// The start time of the fuel level change in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("changeStartTime")]
    public DateTimeOffset? ChangeStartTime { get; init; }

    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The fuel level after the sudden fuel level rise in millipercents.
    /// </summary>
    [JsonPropertyName("fuelLevelAfterMillipercent")]
    public int? FuelLevelAfterMillipercent { get; init; }

    /// <summary>
    /// The fuel level before the sudden fuel level rise in millipercents.
    /// </summary>
    [JsonPropertyName("fuelLevelBeforeMillipercent")]
    public int? FuelLevelBeforeMillipercent { get; init; }

    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public AlertEventLocation? Location { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Sudden Fuel Level Rise
/// Mirrors the spec schema <c>SuddenFuelLevelRiseTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertSuddenFuelLevelRiseTriggerDetails
{
    /// <summary>
    /// The minimum fuel level change in percents to trigger on. Need to be between 5 to 100.
    /// </summary>
    [JsonPropertyName("minFuelLevelChangeInPercents")]
    public required long MinFuelLevelChangeInPercents { get; init; }
}

/// <summary>
/// Details specific to Tampering Detected.
/// Mirrors the spec schema <c>TamperingDetectedResponseBody</c>.
/// </summary>
public sealed record AlertTamperingDetected
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// A daily time range. If start time of day is greater than end time of day, then the time range
/// applies overnight from the specified day of week into the following day.
/// Mirrors the spec schema <c>TimeRangeObjectResponseBody</c>.
/// </summary>
public sealed record AlertTimeRange
{
    /// <summary>
    /// Which days this timezone applies to. Valid values: <c>FRIDAY</c>, <c>MONDAY</c>,
    /// <c>SATURDAY</c>, <c>SUNDAY</c>, <c>THURSDAY</c>, <c>TUESDAY</c>, <c>WEDNESDAY</c>
    /// </summary>
    [JsonPropertyName("daysOfWeek")]
    public required IReadOnlyList<string> DaysOfWeek { get; init; }

    /// <summary>
    /// The time of day at which the time range starts. In 24 hour kitchen clock format.
    /// </summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }

    /// <summary>
    /// The time of day at which the time range starts. In 24 hour kitchen clock format.
    /// </summary>
    [JsonPropertyName("startTime")]
    public required string StartTime { get; init; }

    /// <summary>
    /// The timezone of the time range uses [IANA timezone database](https://www.iana.org/time-zones)
    /// keys (e.g. `America/Los_Angeles`, `America/New_York`, `Europe/London`, etc.). You can find a
    /// mapping of common timezone formats to IANA timezone keys
    /// [here](https://unicode.org/cldr/charts/latest/supplement...
    /// </summary>
    [JsonPropertyName("timezone")]
    public required string Timezone { get; init; }
}

/// <summary>
/// Vehicle, trailer or other equipment to be tracked.
/// Mirrors the spec schema <c>TinyAssetObjectResponseBody</c>.
/// </summary>
public sealed record AlertTinyAsset
{
    /// <summary>
    /// ID of the asset.
    /// </summary>
    [JsonPropertyName("assetId")]
    public required string AssetId { get; init; }

    /// <summary>
    /// The operational context in which the asset interacts with the Samsara system. Examples: Vehicle
    /// (eg: truck, bus...), Trailer (eg: dry van, reefer, flatbed...), Powered Equipment (eg: dozer,
    /// crane...), Unpowered Equipment (eg: container, dumpster...), or Uncategorized. Valid values:
    /// `uncategorized`,... Valid values: <c>uncategorized</c>, <c>trailer</c>, <c>equipment</c>,
    /// <c>unpowered</c>, <c>vehicle</c>
    /// </summary>
    [JsonPropertyName("assetType")]
    public required string AssetType { get; init; }
}

/// <summary>
/// The driver of a vehicle.
/// Mirrors the spec schema <c>TinyDriverObjectResponseBody</c>.
/// </summary>
public sealed record AlertTinyDriver
{
    /// <summary>
    /// ID of the driver.
    /// </summary>
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }
}

/// <summary>
/// A minified tag object
/// Mirrors the spec schema <c>GoaTagTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertTinyTag
{
    /// <summary>
    /// ID of the tag
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Name of the tag.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// If this tag is part a hierarchical tag tree, this is the ID of the parent tag, otherwise this
    /// will be omitted.
    /// </summary>
    [JsonPropertyName("parentTagId")]
    public string? ParentTagId { get; init; }
}

/// <summary>
/// Widget to be tracked.
/// Mirrors the spec schema <c>TinyWidgetObjectResponseBody</c>.
/// </summary>
public sealed record AlertTinyWidget
{
    /// <summary>
    /// ID of the widget.
    /// </summary>
    [JsonPropertyName("widgetId")]
    public required string WidgetId { get; init; }
}

/// <summary>
/// Details specific to Tire Fault Code. At least one fault code or fault code group must be selected.
/// Mirrors the spec schema <c>TireFaultCodeDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertTireFaultCodeDetails
{
    /// <summary>
    /// If true then alert over pressure, under pressure, across axle fault, or leak detected fault
    /// codes. Defaults to false.
    /// </summary>
    [JsonPropertyName("hasCautionaryTireFaultCodes")]
    public bool? HasCautionaryTireFaultCodes { get; init; }

    /// <summary>
    /// If true then alert over temperature or extreme pressure over or under fault codes. Defaults to
    /// false.
    /// </summary>
    [JsonPropertyName("hasCriticalTireFaultCodes")]
    public bool? HasCriticalTireFaultCodes { get; init; }

    /// <summary>
    /// The tire manufacturer. Valid values: `MANUFACTURER_BENDIX`, `MANUFACTURER_CONTINENTAL`,
    /// `MANUFACTURER_DORAN`, `MANUFACTURER_HENDRICKSON`, `MANUFACTURER_INVALID`,
    /// `MANUFACTURER_PASSENGER_CAN`, `MANUFACTURER_PRESSURE_PRO`, `MANUFACTURER_SENSATA`,
    /// `MANUFACTURER_SYSGRATION`, `MANUFACTURER_UNIVERSAL_J193... Valid values:
    /// <c>MANUFACTURER_BENDIX</c>, <c>MANUFACTURER_CONTINENTAL</c>, <c>MANUFACTURER_DORAN</c>,
    /// <c>MANUFACTURER_HENDRICKSON</c>, <c>MANUFACTURER_INVALID</c>, <c>MANUFACTURER_PASSENGER_CAN</c>,
    /// <c>MANUFACTURER_PRESSURE_PRO</c>, <c>MANUFACTURER_SENSATA</c>, <c>MANUFACTURER_SYSGRATION</c>,
    /// <c>MANUFACTURER_UNIVERSAL_J1939</c>, <c>MANUFACTURER_UNIVERSAL_R141</c>
    /// </summary>
    [JsonPropertyName("manufacturer")]
    public required string Manufacturer { get; init; }

    /// <summary>
    /// The list of specific tire fault codes to be alerted on. Valid values:
    /// <c>TIRE_ALERT_ACROSS_AXLE_FAULT</c>, <c>TIRE_ALERT_EXTREME_OVER_PRESSURE</c>,
    /// <c>TIRE_ALERT_EXTREME_UNDER_PRESSURE</c>, <c>TIRE_ALERT_FLAT_SPOT</c>,
    /// <c>TIRE_ALERT_IMBALANCE</c>, <c>TIRE_ALERT_INVALID</c>, <c>TIRE_ALERT_LEAK_DETECTED</c>,
    /// <c>TIRE_ALERT_OVER_PRESSURE</c>, <c>TIRE_ALERT_OVER_TEMPERATURE</c>,
    /// <c>TIRE_ALERT_SENSOR_DEFECT</c>, <c>TIRE_ALERT_SENSOR_LOOSE_OR_FLIPPED</c>,
    /// <c>TIRE_ALERT_SENSOR_LOW_BATTERY</c>, <c>TIRE_ALERT_SENSOR_MISSING</c>,
    /// <c>TIRE_ALERT_UNDER_PRESSURE</c>
    /// </summary>
    [JsonPropertyName("specificTireFaultCodes")]
    public IReadOnlyList<string>? SpecificTireFaultCodes { get; init; }
}

/// <summary>
/// Details specific to Tire Faults.
/// Mirrors the spec schema <c>TireFaultsResponseBody</c>.
/// </summary>
public sealed record AlertTireFaults
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Trailer Moving Without Power.
/// Mirrors the spec schema <c>TrailerMovingWithoutPowerDataResponseBody</c>.
/// </summary>
public sealed record AlertTrailerMovingWithoutPowerData
{
    /// <summary>
    /// Voltage value from the trigger metadata when the alert fired.
    /// </summary>
    [JsonPropertyName("currentVoltage")]
    public int? CurrentVoltage { get; init; }

    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }
}

/// <summary>
/// An assignment group of a specific course or a category can be selected for an alert.
/// Mirrors the spec schema <c>TrainingAssignmentNearDueDateTriggerAssignmentGroupObjectResponseBody</c>.
/// </summary>
public sealed record AlertTrainingAssignmentNearDueDateTriggerAssignmentGroup
{
    /// <summary>
    /// Assignment group type. Valid values: `CATEGORY`, `COURSE` Valid values: <c>CATEGORY</c>,
    /// <c>COURSE</c>
    /// </summary>
    [JsonPropertyName("assignmentGroupType")]
    public required string AssignmentGroupType { get; init; }

    /// <summary>
    /// The unique ID of the assignment group.
    /// </summary>
    [JsonPropertyName("assignmentGroupUuid")]
    public required string AssignmentGroupUuid { get; init; }
}

/// <summary>
/// Details specific to Training Assignment Near Due Date
/// Mirrors the spec schema <c>TrainingAssignmentNearDueDateTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertTrainingAssignmentNearDueDateTriggerDetails
{
    /// <summary>
    /// The assignment groups the trigger is configured for.
    /// </summary>
    [JsonPropertyName("assignmentGroups")]
    public IReadOnlyList<AlertTrainingAssignmentNearDueDateTriggerAssignmentGroup>? AssignmentGroups { get; init; }

    /// <summary>
    /// Whether the trigger is configured in days or weeks. Valid values: `DAYS`, `WEEKS` Valid values:
    /// <c>DAYS</c>, <c>WEEKS</c>
    /// </summary>
    [JsonPropertyName("conditionUnits")]
    public required string ConditionUnits { get; init; }

    /// <summary>
    /// The number of days or weeks near the due date to trigger on.
    /// </summary>
    [JsonPropertyName("conditionValue")]
    public required long ConditionValue { get; init; }

    /// <summary>
    /// The timezone that the alert will be set up in.
    /// </summary>
    [JsonPropertyName("timezone")]
    public required string Timezone { get; init; }
}

/// <summary>
/// The trigger type specific details. Only the field that corresponds to the trigger type is filled in.
/// Mirrors the spec schema <c>TriggerParamsObjectResponseBody</c>.
/// </summary>
public sealed record AlertTriggerParams
{
    /// <summary>
    /// The <c>ambientTemperature</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("ambientTemperature")]
    public AlertAmbientTemperatureDetails? AmbientTemperature { get; init; }

    /// <summary>
    /// The <c>cellSignalLoss</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cellSignalLoss")]
    public AlertCellSignalLossDetails? CellSignalLoss { get; init; }

    /// <summary>
    /// The <c>defLevel</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("defLevel")]
    public AlertDefLevelTriggerDetails? DefLevel { get; init; }

    /// <summary>
    /// The <c>deviceMovement</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("deviceMovement")]
    public AlertDeviceMovementTriggerDetails? DeviceMovement { get; init; }

    /// <summary>
    /// The <c>documentSubmitted</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("documentSubmitted")]
    public AlertDriverDocumentSubmittedDetails? DocumentSubmitted { get; init; }

    /// <summary>
    /// The <c>dvirSubmittedDevice</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dvirSubmittedDevice")]
    public AlertDVIRSubmittedDeviceTriggerDetails? DvirSubmittedDevice { get; init; }

    /// <summary>
    /// The <c>engineIdle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("engineIdle")]
    public AlertEngineIdleTriggerDetails? EngineIdle { get; init; }

    /// <summary>
    /// The <c>engineOff</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("engineOff")]
    public AlertEngineOffDetails? EngineOff { get; init; }

    /// <summary>
    /// The <c>engineOn</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("engineOn")]
    public AlertEngineOnDetails? EngineOn { get; init; }

    /// <summary>
    /// The <c>fuelLevel</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("fuelLevel")]
    public AlertFuelLevelTriggerDetails? FuelLevel { get; init; }

    /// <summary>
    /// The <c>gatewayDisconnected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("gatewayDisconnected")]
    public AlertGatewayDisconnectedDetails? GatewayDisconnected { get; init; }

    /// <summary>
    /// The <c>gatewayUnplugged</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("gatewayUnplugged")]
    public AlertGatewayUnpluggedTriggerDetails? GatewayUnplugged { get; init; }

    /// <summary>
    /// The <c>geofenceEntry</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("geofenceEntry")]
    public AlertGeofenceEntryTriggerDetails? GeofenceEntry { get; init; }

    /// <summary>
    /// The <c>geofenceExit</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("geofenceExit")]
    public AlertGeofenceExitTriggerDetails? GeofenceExit { get; init; }

    /// <summary>
    /// The <c>gpsSignalLoss</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("gpsSignalLoss")]
    public AlertGpsSignalLossDetails? GpsSignalLoss { get; init; }

    /// <summary>
    /// The <c>harshEvent</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("harshEvent")]
    public AlertHarshEventTriggerDetails? HarshEvent { get; init; }

    /// <summary>
    /// The <c>hosViolation</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("hosViolation")]
    public AlertHOSViolationTriggerDetails? HosViolation { get; init; }

    /// <summary>
    /// The <c>insideGeofence</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("insideGeofence")]
    public AlertInsideGeofenceTriggerDetails? InsideGeofence { get; init; }

    /// <summary>
    /// The <c>outOfRoute</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("outOfRoute")]
    public AlertOutOfRouteDetails? OutOfRoute { get; init; }

    /// <summary>
    /// The <c>outsideGeofence</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("outsideGeofence")]
    public AlertOutsideGeofenceTriggerDetails? OutsideGeofence { get; init; }

    /// <summary>
    /// The <c>panicButton</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("panicButton")]
    public AlertPanicButtonDetails? PanicButton { get; init; }

    /// <summary>
    /// The <c>reading</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("reading")]
    public AlertReadingTriggerDetails? Reading { get; init; }

    /// <summary>
    /// The <c>routeStopEstimatedArrival</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("routeStopEstimatedArrival")]
    public AlertRouteStopEstimatedArrivalDetails? RouteStopEstimatedArrival { get; init; }

    /// <summary>
    /// The <c>safetyBehavior</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("safetyBehavior")]
    public AlertSafetyBehaviorTriggerDetails? SafetyBehavior { get; init; }

    /// <summary>
    /// The <c>scheduledMaintenance</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("scheduledMaintenance")]
    public AlertScheduledMaintenanceTriggerDetails? ScheduledMaintenance { get; init; }

    /// <summary>
    /// The <c>scheduledMaintenanceByEngineHours</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("scheduledMaintenanceByEngineHours")]
    public AlertScheduledMaintenanceByEngineHoursDetails? ScheduledMaintenanceByEngineHours { get; init; }

    /// <summary>
    /// The <c>scheduledMaintenanceOdometer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("scheduledMaintenanceOdometer")]
    public AlertScheduledMaintenanceOdometerTriggerDetails? ScheduledMaintenanceOdometer { get; init; }

    /// <summary>
    /// The <c>speed</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("speed")]
    public AlertSpeedTriggerDetails? Speed { get; init; }

    /// <summary>
    /// The <c>suddenFuelLevelDrop</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("suddenFuelLevelDrop")]
    public AlertSuddenFuelLevelDropTriggerDetails? SuddenFuelLevelDrop { get; init; }

    /// <summary>
    /// The <c>suddenFuelLevelRise</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("suddenFuelLevelRise")]
    public AlertSuddenFuelLevelRiseTriggerDetails? SuddenFuelLevelRise { get; init; }

    /// <summary>
    /// The <c>tireFaultCode</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("tireFaultCode")]
    public AlertTireFaultCodeDetails? TireFaultCode { get; init; }

    /// <summary>
    /// The <c>trainingAssignmentNearDueDate</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trainingAssignmentNearDueDate")]
    public AlertTrainingAssignmentNearDueDateTriggerDetails? TrainingAssignmentNearDueDate { get; init; }

    /// <summary>
    /// The <c>unassignedDriving</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("unassignedDriving")]
    public AlertUnassignedDrivingTriggerDetails? UnassignedDriving { get; init; }

    /// <summary>
    /// The <c>vehicleBatteryVoltage</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicleBatteryVoltage")]
    public AlertVehicleBatterVoltageDetails? VehicleBatteryVoltage { get; init; }

    /// <summary>
    /// The <c>vehicleFaultCode</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicleFaultCode")]
    public AlertVehicleFaultCodeDetails? VehicleFaultCode { get; init; }
}

/// <summary>
/// Details specific to Unassigned Driving.
/// Mirrors the spec schema <c>UnassignedDrivingDataResponseBody</c>.
/// </summary>
public sealed record AlertUnassignedDrivingData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Unassigned Driving
/// Mirrors the spec schema <c>UnassignedDrivingTriggerDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertUnassignedDrivingTriggerDetails
{
    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }
}

/// <summary>
/// Details specific to Vehicle Battery Voltage
/// Mirrors the spec schema <c>VehicleBatterVoltageDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertVehicleBatterVoltageDetails
{
    /// <summary>
    /// The battery volt threshold value.
    /// </summary>
    [JsonPropertyName("batteryVolts")]
    public required long BatteryVolts { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public required long MinDurationMilliseconds { get; init; }

    /// <summary>
    /// How to evaluate the threshold. Valid values: `GREATER`, `LESS` Valid values: <c>GREATER</c>,
    /// <c>LESS</c>
    /// </summary>
    [JsonPropertyName("operation")]
    public required string Operation { get; init; }
}

/// <summary>
/// Details specific to Vehicle Battery Voltage.
/// Mirrors the spec schema <c>VehicleBatteryVoltageResponseBody</c>.
/// </summary>
public sealed record AlertVehicleBatteryVoltage
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Vehicle DEF Level Percentage.
/// Mirrors the spec schema <c>VehicleDefLevelPercentageResponseBody</c>.
/// </summary>
public sealed record AlertVehicleDefLevelPercentage
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Vehicle Detected.
/// Mirrors the spec schema <c>VehicleDetectedResponseBody</c>.
/// </summary>
public sealed record AlertVehicleDetected
{
    /// <summary>
    /// The <c>cameraStream</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cameraStream")]
    public AlertObjectOnvifCameraStream? CameraStream { get; init; }
}

/// <summary>
/// Details specific to Vehicle Fault Code. At least one fault code or fault code group must be
/// selected.
/// Mirrors the spec schema <c>VehicleFaultCodeDetailsObjectResponseBody</c>.
/// </summary>
public sealed record AlertVehicleFaultCodeDetails
{
    /// <summary>
    /// If true then alert on codes for less serious errors that do not warrant stopping. Defaults to
    /// false.
    /// </summary>
    [JsonPropertyName("hasAnyAmberWarningLampCodes")]
    public bool? HasAnyAmberWarningLampCodes { get; init; }

    /// <summary>
    /// If true this means that any code is alertable. Defaults to false.
    /// </summary>
    [JsonPropertyName("hasAnyFaultCodes")]
    public bool? HasAnyFaultCodes { get; init; }

    /// <summary>
    /// If true then alert on emission-related codes. Defaults to false.
    /// </summary>
    [JsonPropertyName("hasAnyMalfunctionIndicatorLampCodes")]
    public bool? HasAnyMalfunctionIndicatorLampCodes { get; init; }

    /// <summary>
    /// If true then alert on codes for non-electric vehicle parts. Defaults to false.
    /// </summary>
    [JsonPropertyName("hasAnyProtectionLampCodes")]
    public bool? HasAnyProtectionLampCodes { get; init; }

    /// <summary>
    /// If true then alert when the vehicle warrants stopping. Defaults to false.
    /// </summary>
    [JsonPropertyName("hasAnyRedStopLampCodes")]
    public bool? HasAnyRedStopLampCodes { get; init; }

    /// <summary>
    /// If true then alert when the ABS light is on. Defaults to false.
    /// </summary>
    [JsonPropertyName("hasAnyTrailerAbsLampCodes")]
    public bool? HasAnyTrailerAbsLampCodes { get; init; }

    /// <summary>
    /// The number of milliseconds the trigger needs to stay active before alerting.
    /// </summary>
    [JsonPropertyName("minDurationMilliseconds")]
    public long? MinDurationMilliseconds { get; init; }

    /// <summary>
    /// The list of specific fault codes to be alerted on.
    /// </summary>
    [JsonPropertyName("specificFaultCodes")]
    public IReadOnlyList<AlertSpecificVehicleFaultCode>? SpecificFaultCodes { get; init; }
}

/// <summary>
/// Details specific to Vehicle Faults.
/// Mirrors the spec schema <c>VehicleFaultsResponseBody</c>.
/// </summary>
public sealed record AlertVehicleFaults
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertObjectTrailer? Trailer { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// Details specific to Trailer Assignment Mismatch.
/// Mirrors the spec schema <c>VehicleTrailerMismatchDataResponseBody</c>.
/// </summary>
public sealed record AlertVehicleTrailerMismatchData
{
    /// <summary>
    /// Trailers the vehicle is currently associated with pulling.
    /// </summary>
    [JsonPropertyName("currentlyPullingTrailers")]
    public IReadOnlyList<AlertObjectTrailer>? CurrentlyPullingTrailers { get; init; }

    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// Trailers the driver selected in the dispatch workflow.
    /// </summary>
    [JsonPropertyName("driverSelectedTrailers")]
    public IReadOnlyList<AlertObjectTrailer>? DriverSelectedTrailers { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }
}

/// <summary>
/// A minified vehicle object. This object is only returned if the route is assigned to the vehicle.
/// Mirrors the spec schema <c>VehicleWithGatewayTinyResponseResponseBody</c>.
/// </summary>
public sealed record AlertVehicleWithGatewayTiny
{
    /// <summary>
    /// The type of the asset. Valid values: `uncategorized`, `trailer`, `equipment`, `unpowered`,
    /// `vehicle` Valid values: <c>uncategorized</c>, <c>trailer</c>, <c>equipment</c>,
    /// <c>unpowered</c>, <c>vehicle</c>
    /// </summary>
    [JsonPropertyName("assetType")]
    public string? AssetType { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The <c>gateway</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("gateway")]
    public AlertGoaGatewayTiny? Gateway { get; init; }

    /// <summary>
    /// ID of the vehicle
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The license plate of the vehicle.
    /// </summary>
    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    /// <summary>
    /// Name of the vehicle
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The VIN of the vehicle.
    /// </summary>
    [JsonPropertyName("vin")]
    public string? Vin { get; init; }
}

/// <summary>
/// The vertex of the polygon geofence. These geofence vertices describe the perimeter of the polygon,
/// and must consist of at least 3 vertices and less than 40.
/// Mirrors the spec schema <c>VertexResponseBody</c>.
/// </summary>
public sealed record AlertVertex
{
    /// <summary>
    /// The latitude of a geofence vertex in decimal degrees.
    /// </summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>
    /// The longitude of a geofence vertex in decimal degrees.
    /// </summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}

/// <summary>
/// The webhook configuration for an Action.
/// Mirrors the spec schema <c>WebhookParamsObjectResponseBody</c>.
/// </summary>
public sealed record AlertWebhookParams
{
    /// <summary>
    /// This determines the alert webhook payload type to use. Learn more:
    /// https://developers.samsara.com/docs/webhook-reference. Valid values: `legacy`, `enriched` Valid
    /// values: <c>legacy</c>, <c>enriched</c>
    /// </summary>
    [JsonPropertyName("payloadType")]
    public string? PayloadType { get; init; }

    /// <summary>
    /// The webhook IDs.
    /// </summary>
    [JsonPropertyName("webhookIds")]
    public required IReadOnlyList<string> WebhookIds { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>WebhookRouteResponseObjectResponseBody</c>.
/// </summary>
public sealed record AlertWebhookRouteResponse
{
    /// <summary>
    /// Actual end time, if it exists, for the route in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("actualRouteEndTime")]
    public DateTimeOffset? ActualRouteEndTime { get; init; }

    /// <summary>
    /// Actual start time, if it exists, for the route in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("actualRouteStartTime")]
    public DateTimeOffset? ActualRouteStartTime { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of the route
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Route name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Notes for the route
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>
    /// Scheduled end time, if it exists, for the route in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("scheduledRouteEndTime")]
    public DateTimeOffset? ScheduledRouteEndTime { get; init; }

    /// <summary>
    /// Scheduled start time, if it exists, for the route in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("scheduledRouteStartTime")]
    public DateTimeOffset? ScheduledRouteStartTime { get; init; }

    /// <summary>
    /// The <c>settings</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("settings")]
    public AlertRouteSettings? Settings { get; init; }

    /// <summary>
    /// List of stops along the route
    /// </summary>
    [JsonPropertyName("stops")]
    public IReadOnlyList<AlertRoutesStopResponse>? Stops { get; init; }
}

/// <summary>
/// Details specific to Worker Safety SOS.
/// Mirrors the spec schema <c>WorkerSafetySosDataResponseBody</c>.
/// </summary>
public sealed record AlertWorkerSafetySosData
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertObjectDriver? Driver { get; init; }

    /// <summary>
    /// The <c>location</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("location")]
    public AlertEventLocation? Location { get; init; }

    /// <summary>
    /// The source of the SOS signal. Valid values: `appSos`, `noResponseCheckIn`, `wearableSos`,
    /// `fallDetected` Valid values: <c>appSos</c>, <c>noResponseCheckIn</c>, <c>wearableSos</c>,
    /// <c>fallDetected</c>
    /// </summary>
    [JsonPropertyName("sourceType")]
    public string? SourceType { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertObjectVehicle? Vehicle { get; init; }

    /// <summary>
    /// URL of the Worker Safety incident in the cloud dashboard.
    /// </summary>
    [JsonPropertyName("workerSafetyIncidentUrl")]
    public string? WorkerSafetyIncidentUrl { get; init; }
}

/// <summary>
/// A minimal Address object representation used in AddressEventObject objects
/// Mirrors the spec schema <c>WorkflowAddressEventWithGeofenceObjectResponseBody</c>.
/// </summary>
public sealed record WorkflowAddressEventWithGeofence
{
    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// The full street address for this address/geofence, as it might be recognized by Google Maps.
    /// </summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>
    /// The <c>geofence</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("geofence")]
    public WorkflowGeofence? Geofence { get; init; }

    /// <summary>
    /// Id of the address
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the address
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// An author signature for DVIRs with a signed time.
/// Mirrors the spec schema <c>WorkflowAuthorSignatureObjectResponseBody</c>.
/// </summary>
public sealed record WorkflowAuthorSignature
{
    /// <summary>
    /// The <c>signatoryUser</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("signatoryUser")]
    public EntityReference? SignatoryUser { get; init; }

    /// <summary>
    /// The time when the DVIR was signed. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("signedAtTime")]
    public DateTimeOffset? SignedAtTime { get; init; }

    /// <summary>
    /// Whether the DVIR was submitted by a driver or mechanic. Valid values: `driver`, `mechanic` Valid
    /// values: <c>driver</c>, <c>mechanic</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// Information about a circular geofence. This field is only needed if the geofence is a circle.
/// Mirrors the spec schema <c>WorkflowCircleResponseBody</c>.
/// </summary>
public sealed record WorkflowCircle
{
    /// <summary>
    /// Latitude of the address. Will be geocoded from formattedAddress if not provided.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// Longitude of the address. Will be geocoded from formattedAddress if not provided.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>
    /// The radius of the circular geofence in meters.
    /// </summary>
    [JsonPropertyName("radiusMeters")]
    public long? RadiusMeters { get; init; }
}

/// <summary>
/// A DVIR description
/// Mirrors the spec schema <c>WorkflowDvirObjectResponseBody</c>.
/// </summary>
public sealed record WorkflowDvir
{
    /// <summary>
    /// The <c>authorSignature</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("authorSignature")]
    public WorkflowAuthorSignature? AuthorSignature { get; init; }

    /// <summary>
    /// Defects registered for the DVIR.
    /// </summary>
    [JsonPropertyName("defects")]
    public IReadOnlyList<AlertDvirDefectsObject_v2022_09_13>? Defects { get; init; }

    /// <summary>
    /// Time when the driver signed and completed this DVIR. UTC timestamp in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    /// <summary>
    /// Optional string if your jurisdiction requires a location of the DVIR.
    /// </summary>
    [JsonPropertyName("formattedLocation")]
    public string? FormattedLocation { get; init; }

    /// <summary>
    /// The unique id of the DVIR
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The mechanics notes on the DVIR.
    /// </summary>
    [JsonPropertyName("mechanicNotes")]
    public string? MechanicNotes { get; init; }

    /// <summary>
    /// Indicates if a defect needs correction.
    /// </summary>
    [JsonPropertyName("needsCorrection")]
    public bool? NeedsCorrection { get; init; }

    /// <summary>
    /// The odometer reading in meters.
    /// </summary>
    [JsonPropertyName("odometerMeters")]
    public long? OdometerMeters { get; init; }

    /// <summary>
    /// The condition of vehicle on which DVIR was done. Valid values: `safe`, `unsafe`, `resolved`
    /// Valid values: <c>safe</c>, <c>unsafe</c>, <c>resolved</c>
    /// </summary>
    [JsonPropertyName("safetyStatus")]
    public string? SafetyStatus { get; init; }

    /// <summary>
    /// The <c>secondSignature</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("secondSignature")]
    public WorkflowAuthorSignature? SecondSignature { get; init; }

    /// <summary>
    /// Time when driver began filling out this DVIR in RFC 3339 format.
    /// </summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>
    /// The <c>thirdSignature</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("thirdSignature")]
    public WorkflowAuthorSignature? ThirdSignature { get; init; }

    /// <summary>
    /// The <c>trailer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailer")]
    public AlertGoaTrailerTiny? Trailer { get; init; }

    /// <summary>
    /// Inspection type of the DVIR. Valid values: `preTrip`, `postTrip`, `mechanic`, `unspecified`
    /// Valid values: <c>preTrip</c>, <c>postTrip</c>, <c>mechanic</c>, <c>unspecified</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// Details specific to DVIR Submitted.
/// Mirrors the spec schema <c>WorkflowDvirSubmittedResponseObjectResponseBody</c>.
/// </summary>
public sealed record WorkflowDvirSubmittedResponse
{
    /// <summary>
    /// The <c>driver</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driver")]
    public AlertGoaDriverTiny? Driver { get; init; }

    /// <summary>
    /// The <c>dvir</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dvir")]
    public WorkflowDvir? Dvir { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertVehicleWithGatewayTiny? Vehicle { get; init; }
}

/// <summary>
/// The geofence that defines this address and its bounds. This can either be a circle or a polygon, but
/// not both.
/// Mirrors the spec schema <c>WorkflowGeofenceResponseBody</c>.
/// </summary>
public sealed record WorkflowGeofence
{
    /// <summary>
    /// The <c>circle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("circle")]
    public WorkflowCircle? Circle { get; init; }

    /// <summary>
    /// The <c>polygon</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("polygon")]
    public WorkflowPolygon? Polygon { get; init; }

    /// <summary>
    /// The <c>settings</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("settings")]
    public AlertSettings? Settings { get; init; }
}

/// <summary>
/// Mirrors the spec schema <c>WorkflowGeofenceEventResponseObjectResponseBody</c>.
/// </summary>
public sealed record WorkflowGeofenceEventResponse
{
    /// <summary>
    /// The <c>address</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("address")]
    public WorkflowAddressEventWithGeofence? Address { get; init; }

    /// <summary>
    /// The <c>fuelVolume</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("fuelVolume")]
    public AlertFuelVolume? FuelVolume { get; init; }

    /// <summary>
    /// The <c>vehicle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public AlertVehicleWithGatewayTiny? Vehicle { get; init; }
}

/// <summary>
/// Object representing the granular details of the condition. These details will vary depending on the
/// condition.
/// Mirrors the spec schema <c>WorkflowIncidentDetailsObjectResponseBody</c>.
/// </summary>
public sealed record WorkflowIncidentDetails
{
    /// <summary>
    /// The <c>ambientTemperature</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("ambientTemperature")]
    public AlertAmbientTemperature? AmbientTemperature { get; init; }

    /// <summary>
    /// The <c>cameraConnectorDisconected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cameraConnectorDisconected")]
    public AlertCameraConnectorDisconected? CameraConnectorDisconected { get; init; }

    /// <summary>
    /// The <c>cameraStreamIssue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cameraStreamIssue")]
    public AlertCameraStreamIssue? CameraStreamIssue { get; init; }

    /// <summary>
    /// The <c>cellSignalLoss</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cellSignalLoss")]
    public AlertCellSignalLoss? CellSignalLoss { get; init; }

    /// <summary>
    /// The <c>cloudBackupUploadIssue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("cloudBackupUploadIssue")]
    public AlertCloudBackupUploadIssue? CloudBackupUploadIssue { get; init; }

    /// <summary>
    /// The <c>dashcamDisconnected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dashcamDisconnected")]
    public AlertDashcamDisconnected? DashcamDisconnected { get; init; }

    /// <summary>
    /// The <c>dataInputValue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dataInputValue")]
    public AlertDataInputValue? DataInputValue { get; init; }

    /// <summary>
    /// The <c>deviceMovement</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("deviceMovement")]
    public AlertDeviceMovementData? DeviceMovement { get; init; }

    /// <summary>
    /// The <c>deviceMovementStopped</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("deviceMovementStopped")]
    public AlertDeviceMovementStoppedData? DeviceMovementStopped { get; init; }

    /// <summary>
    /// The <c>doorOpen</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("doorOpen")]
    public AlertDoorOpen? DoorOpen { get; init; }

    /// <summary>
    /// The <c>driverAppSignIn</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driverAppSignIn")]
    public AlertDriverAppSignIn? DriverAppSignIn { get; init; }

    /// <summary>
    /// The <c>driverAppSignOut</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driverAppSignOut")]
    public AlertDriverAppSignOut? DriverAppSignOut { get; init; }

    /// <summary>
    /// The <c>driverDocumentSubmitted</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driverDocumentSubmitted")]
    public AlertDriverDocumentSubmitted? DriverDocumentSubmitted { get; init; }

    /// <summary>
    /// The <c>driverMessageReceived</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driverMessageReceived")]
    public AlertDriverMessageReceived? DriverMessageReceived { get; init; }

    /// <summary>
    /// The <c>driverMessageSent</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driverMessageSent")]
    public AlertDriverMessageSent? DriverMessageSent { get; init; }

    /// <summary>
    /// The <c>driverRecorded</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("driverRecorded")]
    public AlertDriverRecorded? DriverRecorded { get; init; }

    /// <summary>
    /// The <c>dvirSubmittedDevice</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("dvirSubmittedDevice")]
    public WorkflowDvirSubmittedResponse? DvirSubmittedDevice { get; init; }

    /// <summary>
    /// The <c>engineIdle</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("engineIdle")]
    public AlertEngineIdleData? EngineIdle { get; init; }

    /// <summary>
    /// The <c>engineOff</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("engineOff")]
    public AlertEngineOff? EngineOff { get; init; }

    /// <summary>
    /// The <c>engineOn</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("engineOn")]
    public AlertEngineOn? EngineOn { get; init; }

    /// <summary>
    /// The <c>formSubmitted</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("formSubmitted")]
    public AlertFormSubmitted? FormSubmitted { get; init; }

    /// <summary>
    /// The <c>formUpdated</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("formUpdated")]
    public AlertFormUpdated? FormUpdated { get; init; }

    /// <summary>
    /// The <c>fuelLevelPercentage</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("fuelLevelPercentage")]
    public AlertFuelLevelPercentage? FuelLevelPercentage { get; init; }

    /// <summary>
    /// The <c>gatewayDisconnected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("gatewayDisconnected")]
    public AlertGatewayDisconnected? GatewayDisconnected { get; init; }

    /// <summary>
    /// The <c>gatewayUnplugged</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("gatewayUnplugged")]
    public AlertGatewayWithVehicleTiny? GatewayUnplugged { get; init; }

    /// <summary>
    /// The <c>geofenceEntry</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("geofenceEntry")]
    public WorkflowGeofenceEventResponse? GeofenceEntry { get; init; }

    /// <summary>
    /// The <c>geofenceExit</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("geofenceExit")]
    public WorkflowGeofenceEventResponse? GeofenceExit { get; init; }

    /// <summary>
    /// The <c>gpsSignalLoss</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("gpsSignalLoss")]
    public AlertGpsSignalLoss? GpsSignalLoss { get; init; }

    /// <summary>
    /// The <c>harshEvent</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("harshEvent")]
    public AlertHarshEventData? HarshEvent { get; init; }

    /// <summary>
    /// The <c>hosDutyStatus</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("hosDutyStatus")]
    public AlertHosDutyStatusData? HosDutyStatus { get; init; }

    /// <summary>
    /// The <c>hosViolation</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("hosViolation")]
    public AlertHosViolationData? HosViolation { get; init; }

    /// <summary>
    /// The <c>inactivity</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("inactivity")]
    public AlertInactivity? Inactivity { get; init; }

    /// <summary>
    /// The <c>insideGeofence</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("insideGeofence")]
    public AlertInsideGeofenceData? InsideGeofence { get; init; }

    /// <summary>
    /// The <c>issueCreated</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("issueCreated")]
    public AlertIssueCreated? IssueCreated { get; init; }

    /// <summary>
    /// The <c>jammingDetected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("jammingDetected")]
    public AlertJammingDetected? JammingDetected { get; init; }

    /// <summary>
    /// The <c>missingDvirPastDue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("missingDvirPastDue")]
    public AlertMissingDvirPastDue? MissingDvirPastDue { get; init; }

    /// <summary>
    /// The <c>motionDetected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("motionDetected")]
    public AlertMotionDetected? MotionDetected { get; init; }

    /// <summary>
    /// The <c>outOfRoute</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("outOfRoute")]
    public AlertOutOfRoute? OutOfRoute { get; init; }

    /// <summary>
    /// The <c>outOfSequenceStopArrival</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("outOfSequenceStopArrival")]
    public AlertOutOfSequenceStopArrivalData? OutOfSequenceStopArrival { get; init; }

    /// <summary>
    /// The <c>outsideGeofence</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("outsideGeofence")]
    public AlertOutsideGeofenceData? OutsideGeofence { get; init; }

    /// <summary>
    /// The <c>panicButton</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("panicButton")]
    public AlertPanicButton? PanicButton { get; init; }

    /// <summary>
    /// The <c>personDetected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("personDetected")]
    public AlertPersonDetected? PersonDetected { get; init; }

    /// <summary>
    /// The <c>preventiveMaintenanceScheduleDue</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("preventiveMaintenanceScheduleDue")]
    public AlertPreventiveMaintenanceScheduleDueData? PreventiveMaintenanceScheduleDue { get; init; }

    /// <summary>
    /// The <c>reading</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("reading")]
    public WorkflowReadingDetails? Reading { get; init; }

    /// <summary>
    /// The <c>reeferTemperature</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("reeferTemperature")]
    public AlertReeferTemperature? ReeferTemperature { get; init; }

    /// <summary>
    /// The <c>routeStartDelayed</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("routeStartDelayed")]
    public AlertRouteStartDelayedData? RouteStartDelayed { get; init; }

    /// <summary>
    /// The <c>routeStopArrival</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("routeStopArrival")]
    public AlertRouteStopDetails? RouteStopArrival { get; init; }

    /// <summary>
    /// The <c>routeStopDeparture</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("routeStopDeparture")]
    public AlertRouteStopDetails? RouteStopDeparture { get; init; }

    /// <summary>
    /// The <c>routeStopETA</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("routeStopETA")]
    public AlertRouteStopETA? RouteStopETA { get; init; }

    /// <summary>
    /// The <c>routeStopEarlyLateArrival</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("routeStopEarlyLateArrival")]
    public AlertRouteStopEarlyLateArrivalData? RouteStopEarlyLateArrival { get; init; }

    /// <summary>
    /// The <c>scheduledMaintenance</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("scheduledMaintenance")]
    public AlertScheduledMaintenanceData? ScheduledMaintenance { get; init; }

    /// <summary>
    /// The <c>scheduledMaintenanceByEngineHours</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("scheduledMaintenanceByEngineHours")]
    public AlertScheduledMaintenanceByEngineHours? ScheduledMaintenanceByEngineHours { get; init; }

    /// <summary>
    /// The <c>scheduledMaintenanceOdometer</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("scheduledMaintenanceOdometer")]
    public AlertScheduledMaintenanceOdometerData? ScheduledMaintenanceOdometer { get; init; }

    /// <summary>
    /// The <c>severeSpeeding</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("severeSpeeding")]
    public AlertSevereSpeedingStartedResponse? SevereSpeeding { get; init; }

    /// <summary>
    /// The <c>siteGatewayDisconnected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("siteGatewayDisconnected")]
    public AlertSiteGatewayDisconnected? SiteGatewayDisconnected { get; init; }

    /// <summary>
    /// The <c>speed</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("speed")]
    public AlertSpeedData? Speed { get; init; }

    /// <summary>
    /// The <c>suddenFuelLevelDrop</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("suddenFuelLevelDrop")]
    public AlertSuddenFuelLevelDrop? SuddenFuelLevelDrop { get; init; }

    /// <summary>
    /// The <c>suddenFuelLevelRise</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("suddenFuelLevelRise")]
    public AlertSuddenFuelLevelRise? SuddenFuelLevelRise { get; init; }

    /// <summary>
    /// The <c>tamperingDetected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("tamperingDetected")]
    public AlertTamperingDetected? TamperingDetected { get; init; }

    /// <summary>
    /// The <c>tireFaults</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("tireFaults")]
    public AlertTireFaults? TireFaults { get; init; }

    /// <summary>
    /// The <c>trailerMovingWithoutPower</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("trailerMovingWithoutPower")]
    public AlertTrailerMovingWithoutPowerData? TrailerMovingWithoutPower { get; init; }

    /// <summary>
    /// The <c>unassignedDriving</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("unassignedDriving")]
    public AlertUnassignedDrivingData? UnassignedDriving { get; init; }

    /// <summary>
    /// The <c>vehicleBatteryVoltage</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicleBatteryVoltage")]
    public AlertVehicleBatteryVoltage? VehicleBatteryVoltage { get; init; }

    /// <summary>
    /// The <c>vehicleDefLevelPercentage</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicleDefLevelPercentage")]
    public AlertVehicleDefLevelPercentage? VehicleDefLevelPercentage { get; init; }

    /// <summary>
    /// The <c>vehicleDetected</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicleDetected")]
    public AlertVehicleDetected? VehicleDetected { get; init; }

    /// <summary>
    /// The <c>vehicleFaults</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicleFaults")]
    public AlertVehicleFaults? VehicleFaults { get; init; }

    /// <summary>
    /// The <c>vehicleTrailerMismatch</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("vehicleTrailerMismatch")]
    public AlertVehicleTrailerMismatchData? VehicleTrailerMismatch { get; init; }

    /// <summary>
    /// The <c>workerSafetySos</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("workerSafetySos")]
    public AlertWorkerSafetySosData? WorkerSafetySos { get; init; }
}

/// <summary>
/// Information about a polygon geofence. This field is only needed if the geofence is a polygon.
/// Mirrors the spec schema <c>WorkflowPolygonResponseBody</c>.
/// </summary>
public sealed record WorkflowPolygon
{
    /// <summary>
    /// The vertices of the polygon geofence. These geofence vertices describe the perimeter of the
    /// polygon, and must consist of at least 3 vertices and less than 40.
    /// </summary>
    [JsonPropertyName("vertices")]
    public IReadOnlyList<AlertVertex>? Vertices { get; init; }
}

/// <summary>
/// Details specific to Reading Triggers.
/// Mirrors the spec schema <c>ReadingTriggerDetailsResponseBody</c>.
/// </summary>
public sealed record WorkflowReadingDetails
{
    /// <summary>
    /// The <c>asset</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("asset")]
    public AlertObjectAsset? Asset { get; init; }

    /// <summary>
    /// The <c>continuousThreshold</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("continuousThreshold")]
    public AlertContinuousReadingAlertThreshold? ContinuousThreshold { get; init; }

    /// <summary>
    /// The <c>enumThreshold</c> value from the spec schema.
    /// </summary>
    [JsonPropertyName("enumThreshold")]
    public AlertEnumReadingAlertThreshold? EnumThreshold { get; init; }

    /// <summary>
    /// The ID of the reading.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// A vehicle object
/// Mirrors the spec schema <c>VehicleResponseResponseBody</c>.
/// </summary>
public sealed record WorkflowVehicle
{
    /// <summary>
    /// The type of the asset. Valid values: `uncategorized`, `trailer`, `equipment`, `unpowered`,
    /// `vehicle` Valid values: <c>uncategorized</c>, <c>trailer</c>, <c>equipment</c>,
    /// <c>unpowered</c>, <c>vehicle</c>
    /// </summary>
    [JsonPropertyName("assetType")]
    public string? AssetType { get; init; }

    /// <summary>
    /// A map of external ids
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>
    /// ID of the vehicle
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The license plate of the vehicle.
    /// </summary>
    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    /// <summary>
    /// Name of the vehicle
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The VIN of the vehicle.
    /// </summary>
    [JsonPropertyName("vehicleVin")]
    public string? VehicleVin { get; init; }
}
