namespace Samsara.Sdk.Models.Webhooks;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a webhook configuration.
/// </summary>
public sealed record Webhook
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("version")]
    public string? Version { get; init; }

    [JsonPropertyName("customHeaders")]
    public IReadOnlyList<WebhookHeader>? CustomHeaders { get; init; }

    [JsonPropertyName("eventTypes")]
    public IReadOnlyList<string>? EventTypes { get; init; }
}

/// <summary>
/// Custom header sent with webhook requests.
/// </summary>
public sealed record WebhookHeader
{
    [JsonPropertyName("key")]
    public required string Key { get; init; }

    [JsonPropertyName("value")]
    public required string Value { get; init; }
}

/// <summary>
/// Request body for creating a webhook.
/// </summary>
public sealed record CreateWebhookRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("version")]
    public string? Version { get; init; }

    [JsonPropertyName("customHeaders")]
    public IReadOnlyList<WebhookHeader>? CustomHeaders { get; init; }

    [JsonPropertyName("eventTypes")]
    public IReadOnlyList<string>? EventTypes { get; init; }
}

/// <summary>
/// Request body for updating a webhook (PATCH).
/// </summary>
public sealed record UpdateWebhookRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("customHeaders")]
    public IReadOnlyList<WebhookHeader>? CustomHeaders { get; init; }

    [JsonPropertyName("eventTypes")]
    public IReadOnlyList<string>? EventTypes { get; init; }
}
