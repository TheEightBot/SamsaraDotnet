namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Drivers;

internal sealed class DriversClient : SamsaraServiceClientBase, IDriversClient
{
    private const string BasePath = "fleet/drivers";

    public DriversClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Driver> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Driver>(BasePath, cancellationToken: cancellationToken);

    public Task<Driver> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Driver>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Driver> CreateAsync(CreateDriverRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Driver>(BasePath, request, cancellationToken);

    public Task<Driver> UpdateAsync(string id, UpdateDriverRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Driver>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
