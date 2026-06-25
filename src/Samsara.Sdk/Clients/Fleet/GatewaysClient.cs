namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class GatewaysClient : SamsaraServiceClientBase, IGatewaysClient
{
    private const string BasePath = "gateways";

    public GatewaysClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Gateway> ListAsync(
        IReadOnlyList<string>? models = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Gateway>(
            QueryBuilder.WithParams(BasePath,
                ("models", models is null ? null : string.Join(",", models))),
            cancellationToken: cancellationToken);

    public Task<Gateway> CreateAsync(CreateGatewayRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Gateway>(BasePath, request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<object> PairGatewaysAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>($"{BasePath}/pair", request, cancellationToken);
}
