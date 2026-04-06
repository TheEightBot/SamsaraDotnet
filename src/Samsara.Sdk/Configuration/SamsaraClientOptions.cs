namespace Samsara.Sdk.Configuration;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Configuration options for the Samsara API client.
/// </summary>
public sealed class SamsaraClientOptions
{
    /// <summary>
    /// The configuration section name used in appsettings.json.
    /// </summary>
    public const string SectionName = "Samsara";

    /// <summary>
    /// The default base URL for the Samsara API (US region).
    /// </summary>
    public const string DefaultBaseUrl = "https://api.samsara.com";

    /// <summary>
    /// The base URL for the Samsara API (EU region).
    /// </summary>
    public const string EuBaseUrl = "https://api.eu.samsara.com";

    /// <summary>
    /// The API token used for Bearer authentication.
    /// Required unless <see cref="TokenProvider"/> is set.
    /// </summary>
    public string? ApiToken { get; set; }

    /// <summary>
    /// A dynamic token provider for OAuth 2.0 or rotating token scenarios.
    /// When set, takes precedence over <see cref="ApiToken"/>.
    /// </summary>
    public Func<CancellationToken, ValueTask<string>>? TokenProvider { get; set; }

    /// <summary>
    /// The base URL for the Samsara API. Defaults to the US region.
    /// Set to <see cref="EuBaseUrl"/> for EU deployments.
    /// </summary>
    [Required]
    [Url]
    public string BaseUrl { get; set; } = DefaultBaseUrl;

    /// <summary>
    /// Default page size for paginated requests. Max 512.
    /// </summary>
    [Range(1, 512)]
    public int? DefaultPageSize { get; set; }

    /// <summary>
    /// HTTP request timeout. Defaults to 30 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Maximum number of retries for transient failures. Defaults to 3.
    /// </summary>
    [Range(0, 10)]
    public int RetryCount { get; set; } = 3;

    internal void Validate()
    {
        if (string.IsNullOrWhiteSpace(ApiToken) && TokenProvider is null)
        {
            throw new InvalidOperationException(
                $"Either {nameof(ApiToken)} or {nameof(TokenProvider)} must be configured.");
        }

        if (string.IsNullOrWhiteSpace(BaseUrl))
        {
            throw new InvalidOperationException($"{nameof(BaseUrl)} must not be empty.");
        }

        if (DefaultPageSize is < 1 or > 512)
        {
            throw new InvalidOperationException($"{nameof(DefaultPageSize)} must be between 1 and 512.");
        }
    }
}
