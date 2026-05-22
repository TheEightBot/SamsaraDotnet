namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for listing Samsara gateways.
/// </summary>
public interface IGatewaysClient
{
    IAsyncEnumerable<Gateway> ListAsync(CancellationToken cancellationToken = default);
}
