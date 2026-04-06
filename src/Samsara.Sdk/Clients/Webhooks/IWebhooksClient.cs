namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Webhooks;

/// <summary>
/// Client for managing Samsara webhooks.
/// </summary>
public interface IWebhooksClient
{
    IAsyncEnumerable<Webhook> ListAsync(CancellationToken cancellationToken = default);
    Task<Webhook> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Webhook> CreateAsync(CreateWebhookRequest request, CancellationToken cancellationToken = default);
    Task<Webhook> UpdateAsync(string id, UpdateWebhookRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
