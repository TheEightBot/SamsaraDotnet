namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Routes;

/// <summary>
/// Client for managing Samsara hubs.
/// </summary>
public interface IHubsClient
{
    IAsyncEnumerable<Hub> ListAsync(CancellationToken cancellationToken = default);
    Task<Hub> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Hub> CreateAsync(CreateHubRequest request, CancellationToken cancellationToken = default);
    Task<Hub> UpdateAsync(string id, UpdateHubRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
