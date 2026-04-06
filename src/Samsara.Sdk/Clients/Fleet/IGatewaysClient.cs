namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for listing Samsara gateways.
/// </summary>
public interface IGatewaysClient
{
    IAsyncEnumerable<Gateway> ListAsync(CancellationToken cancellationToken = default);
    Task<Gateway> GetAsync(string id, CancellationToken cancellationToken = default);
}
