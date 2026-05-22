namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class GatewaysClient : SamsaraServiceClientBase, IGatewaysClient
{
    private const string BasePath = "gateways";

    public GatewaysClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Gateway> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Gateway>(BasePath, cancellationToken: cancellationToken);
}
