namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Routes;

/// <summary>
/// Client for managing Samsara hubs.
/// </summary>
public interface IHubsClient
{
    IAsyncEnumerable<Hub> ListAsync(CancellationToken cancellationToken = default);
    /// <summary>List hubs via the dedicated <c>GET /hubs</c> endpoint.</summary>
    IAsyncEnumerable<Hub> ListHubsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> ListPlanRoutesAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<HubPlanOrder> ListPlanOrdersAsync(
        string planId,
        string? orderIds = null,
        CancellationToken cancellationToken = default);
    Task DeletePlanOrdersAsync(
        string planId,
        string? orderIds = null,
        bool? deleteAll = null,
        CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> ListRouteTemplatesAsync(
        string hubId,
        string? id = null,
        string? name = null,
        CancellationToken cancellationToken = default);
    Task DeleteRouteTemplatesAsync(string id, CancellationToken cancellationToken = default);
    Task<Hub> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Hub> CreateAsync(CreateHubRequest request, CancellationToken cancellationToken = default);
    Task<Hub> UpdateAsync(string id, UpdateHubRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<HubCapacity> ListCapacitiesAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<HubCustomProperty> ListCustomPropertiesAsync(CancellationToken cancellationToken = default);
    Task<HubLocation> UpdateLocationAsync(string id, UpdateHubLocationRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<HubLocation> ListLocationsAsync(CancellationToken cancellationToken = default);
    Task<HubLocation> CreateLocationAsync(CreateHubLocationRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<HubSkill> ListSkillsAsync(CancellationToken cancellationToken = default);
    Task<HubPlan> CreatePlanAsync(CreateHubPlanRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<HubPlan> ListPlansAsync(CancellationToken cancellationToken = default);
    Task<HubPlanOrder> CreatePlanOrdersAsync(CreateHubPlanOrdersRequest request, CancellationToken cancellationToken = default);
}
