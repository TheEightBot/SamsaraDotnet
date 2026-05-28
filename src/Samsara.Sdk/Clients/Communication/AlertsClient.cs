namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Communication;

/// <summary>
/// Client for Samsara alert configurations and incidents. The spec exposes only
/// <c>/alerts/configurations*</c> and <c>/alerts/incidents/stream</c> — there is no
/// top-level <c>/alerts</c> resource.
/// </summary>
internal sealed class AlertsClient : SamsaraServiceClientBase, IAlertsClient
{
    private const string ConfigurationsPath = "alerts/configurations";
    private const string IncidentsStreamPath = "alerts/incidents/stream";

    public AlertsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>List alert configurations (<c>getConfigurations</c>).</summary>
    public IAsyncEnumerable<AlertConfiguration> ListConfigurationsAsync(
        IReadOnlyList<string>? ids = null,
        string? status = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<AlertConfiguration>(
            QueryBuilder.WithParams(ConfigurationsPath,
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("status", status),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<AlertConfiguration> CreateConfigurationAsync(CreateAlertConfigurationRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<AlertConfiguration>(ConfigurationsPath, request, cancellationToken);

    /// <summary>Update a configuration. The id is sent in the body, not in the path.</summary>
    public Task<AlertConfiguration> UpdateConfigurationAsync(UpdateAlertConfigurationRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<AlertConfiguration>(ConfigurationsPath, request, cancellationToken);

    /// <summary>Delete a configuration. The id is sent as a query parameter, not in the path.</summary>
    public Task DeleteConfigurationAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(
            QueryBuilder.WithParams(ConfigurationsPath, ("id", id)),
            cancellationToken);

    /// <summary>
    /// Stream alert incidents (<c>GET /alerts/incidents/stream</c>). Both <c>startTime</c>
    /// and <c>configurationIds</c> are required by the spec.
    /// </summary>
    public IAsyncEnumerable<AlertIncident> GetIncidentsStreamAsync(
        DateTimeOffset startTime,
        IReadOnlyList<string> configurationIds,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<AlertIncident>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange(IncidentsStreamPath, startTime, endTime),
                ("configurationIds", string.Join(",", configurationIds))),
            cancellationToken: cancellationToken);
}
