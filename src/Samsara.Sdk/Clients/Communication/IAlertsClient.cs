namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Communication;

/// <summary>
/// Client for Samsara alerts.
/// </summary>
public interface IAlertsClient
{
    IAsyncEnumerable<Alert> ListAsync(CancellationToken cancellationToken = default);
    Task<Alert> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Alert> CreateAsync(CreateAlertRequest request, CancellationToken cancellationToken = default);
    Task<Alert> UpdateAsync(string id, UpdateAlertRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<AlertConfiguration> ListConfigurationsAsync(CancellationToken cancellationToken = default);
    Task<AlertConfiguration> CreateConfigurationAsync(CreateAlertConfigurationRequest request, CancellationToken cancellationToken = default);
    Task<AlertConfiguration> UpdateConfigurationAsync(string id, UpdateAlertConfigurationRequest request, CancellationToken cancellationToken = default);
    Task DeleteConfigurationAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<AlertIncident> ListIncidentsAsync(CancellationToken cancellationToken = default);
    Task<AlertIncident> GetIncidentAsync(string id, CancellationToken cancellationToken = default);
}
