namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Communication;

/// <summary>
/// Client for Samsara alert configurations and incidents. The spec exposes only
/// <c>/alerts/configurations*</c> and <c>/alerts/incidents/stream</c> — there is no
/// top-level <c>/alerts</c> resource.
/// </summary>
public interface IAlertsClient
{
    /// <summary>List alert configurations.</summary>
    IAsyncEnumerable<AlertConfiguration> ListConfigurationsAsync(
        IReadOnlyList<string>? ids = null,
        string? status = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    Task<AlertConfiguration> CreateConfigurationAsync(CreateAlertConfigurationRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update a configuration. The id is sent in the body, not in the path.</summary>
    Task<AlertConfiguration> UpdateConfigurationAsync(UpdateAlertConfigurationRequest request, CancellationToken cancellationToken = default);

    /// <summary>Delete a configuration. The id is sent as a query parameter.</summary>
    Task DeleteConfigurationAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>Stream alert incidents. <c>startTime</c> and <c>configurationIds</c> are required.</summary>
    IAsyncEnumerable<AlertIncident> GetIncidentsStreamAsync(
        DateTimeOffset startTime,
        IReadOnlyList<string> configurationIds,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default);
}
