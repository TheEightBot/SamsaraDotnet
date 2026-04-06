namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Routes;

/// <summary>
/// Client for managing Samsara routes.
/// </summary>
public interface IRoutesClient
{
    IAsyncEnumerable<Route> ListAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<Route> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Route> CreateAsync(CreateRouteRequest request, CancellationToken cancellationToken = default);
    Task<Route> UpdateAsync(string id, UpdateRouteRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
