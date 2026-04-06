namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Communication;

internal sealed class MessagesClient : SamsaraServiceClientBase, IMessagesClient
{
    private const string BasePath = "fleet/messages";

    public MessagesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<DriverMessage> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DriverMessage>(BasePath, cancellationToken: cancellationToken);

    public Task SendAsync(SendDriverMessageRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync(BasePath, request, cancellationToken);
}
