namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

internal sealed class RoutesClient : SamsaraServiceClientBase, IRoutesClient
{
    private const string BasePath = "fleet/routes";

    public RoutesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Route> ListAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<Route>(QueryBuilder.WithTimeRange(BasePath, startTime, endTime), cancellationToken: cancellationToken);

    public Task<Route> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Route>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Route> CreateAsync(CreateRouteRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Route>(BasePath, request, cancellationToken);

    public Task<Route> UpdateAsync(string id, UpdateRouteRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Route>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
