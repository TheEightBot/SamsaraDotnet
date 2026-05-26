namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for listing Samsara gateways.
/// </summary>
public interface IGatewaysClient
{
    IAsyncEnumerable<Gateway> ListAsync(CancellationToken cancellationToken = default);
    Task<Gateway> CreateAsync(CreateGatewayRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
