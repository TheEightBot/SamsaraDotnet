namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

internal sealed class HubsClient : SamsaraServiceClientBase, IHubsClient
{
    private const string BasePath = "addresses";

    public HubsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Hub> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Hub>(BasePath, cancellationToken: cancellationToken);

    public Task<Hub> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Hub>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Hub> CreateAsync(CreateHubRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Hub>(BasePath, request, cancellationToken);

    public Task<Hub> UpdateAsync(string id, UpdateHubRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Hub>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
