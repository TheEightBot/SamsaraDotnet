namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Webhooks;

internal sealed class WebhooksClient : SamsaraServiceClientBase, IWebhooksClient
{
    private const string BasePath = "webhooks";

    public WebhooksClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Webhook> ListAsync(string? ids = null, CancellationToken cancellationToken = default)
        => PaginateAsync<Webhook>(
            QueryBuilder.WithParams(BasePath, ("ids", ids)),
            cancellationToken: cancellationToken);

    public Task<Webhook> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Webhook>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Webhook> CreateAsync(CreateWebhookRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Webhook>(BasePath, request, cancellationToken);

    public Task<Webhook> UpdateAsync(string id, UpdateWebhookRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Webhook>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
