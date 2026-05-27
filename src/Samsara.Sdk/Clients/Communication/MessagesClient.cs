namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Communication;

internal sealed class MessagesClient : SamsaraServiceClientBase, IMessagesClient
{
    private const string BasePath = "v1/fleet/messages";

    public MessagesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<DriverMessage> ListAsync(
        long? endMs = null,
        long? durationMs = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DriverMessage>(
            QueryBuilder.WithParams(
                BasePath,
                ("endMs", endMs?.ToString(CultureInfo.InvariantCulture)),
                ("durationMs", durationMs?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken: cancellationToken);

    public Task SendAsync(SendDriverMessageRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync(BasePath, request, cancellationToken);
}
