namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Communication;

/// <summary>
/// Client for Samsara alerts.
/// </summary>
public interface IAlertsClient
{
    IAsyncEnumerable<Alert> ListAsync(CancellationToken cancellationToken = default);
    Task<Alert> GetAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<AlertConfiguration> ListConfigurationsAsync(CancellationToken cancellationToken = default);
}
