namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Communication;

internal sealed class AlertsClient : SamsaraServiceClientBase, IAlertsClient
{
    private const string BasePath = "alerts";

    public AlertsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Alert> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Alert>(BasePath, cancellationToken: cancellationToken);

    public Task<Alert> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Alert>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Alert> CreateAsync(CreateAlertRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Alert>(BasePath, request, cancellationToken);

    public Task<Alert> UpdateAsync(string id, UpdateAlertRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Alert>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<AlertConfiguration> ListConfigurationsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<AlertConfiguration>("alerts/configurations", cancellationToken: cancellationToken);

    public Task<AlertConfiguration> CreateConfigurationAsync(CreateAlertConfigurationRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<AlertConfiguration>("alerts/configurations", request, cancellationToken);

    public Task<AlertConfiguration> UpdateConfigurationAsync(string id, UpdateAlertConfigurationRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<AlertConfiguration>($"alerts/configurations/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteConfigurationAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"alerts/configurations/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<AlertIncident> ListIncidentsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<AlertIncident>("alerts/incidents", cancellationToken: cancellationToken);

    public Task<AlertIncident> GetIncidentAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<AlertIncident>($"alerts/incidents/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<AlertIncident> GetIncidentsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<AlertIncident>(QueryBuilder.WithTimeRange("alerts/incidents/stream", startTime, endTime), cancellationToken: cancellationToken);
}
