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

    public IAsyncEnumerable<AlertConfiguration> ListConfigurationsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<AlertConfiguration>("alerts/configurations", cancellationToken: cancellationToken);
}
