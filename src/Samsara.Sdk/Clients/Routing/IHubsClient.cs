namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Routes;

/// <summary>
/// Client for managing Samsara hubs.
/// </summary>
public interface IHubsClient
{
    IAsyncEnumerable<Hub> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>List hubs via the dedicated <c>GET /hubs</c> endpoint.</summary>
    IAsyncEnumerable<Hub> ListHubsAsync(
        string? hubIds = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default);

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

    /// <summary>List hub capacities (<c>GET /hub/capacities</c>). <paramref name="hubId"/> is required by the spec.</summary>
    IAsyncEnumerable<HubCapacity> ListCapacitiesAsync(
        string hubId,
        string? capacityIds = null,
        string? capacityNames = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>List hub custom properties (<c>GET /hub/customProperties</c>). <paramref name="hubId"/> is required by the spec.</summary>
    IAsyncEnumerable<HubCustomProperty> ListCustomPropertiesAsync(
        string hubId,
        string? customPropertyIds = null,
        string? customPropertyNames = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>Update a hub location (<c>PATCH /hub/location/{id}</c>) — the body is wrapped in <c>{ data: T }</c>.</summary>
    Task<HubLocation> UpdateLocationAsync(string id, UpdateHubLocationEnvelopeRequest request, CancellationToken cancellationToken = default);

    /// <summary>List hub locations (<c>GET /hub/locations</c>). <paramref name="hubId"/> is required by the spec.</summary>
    IAsyncEnumerable<HubLocation> ListLocationsAsync(
        string hubId,
        string? locationIds = null,
        string? customerLocationIds = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create hub locations in bulk (<c>POST /hub/locations</c>) — the body is wrapped in <c>{ data: T[] }</c>.</summary>
    Task<HubLocation> CreateLocationAsync(CreateHubLocationsRequest request, CancellationToken cancellationToken = default);

    /// <summary>List hub skills (<c>GET /hub/skills</c>). <paramref name="hubId"/> is required by the spec.</summary>
    IAsyncEnumerable<HubSkill> ListSkillsAsync(
        string hubId,
        string? skillIds = null,
        string? skillNames = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default);

    Task<HubPlan> CreatePlanAsync(CreateHubPlanRequest request, CancellationToken cancellationToken = default);

    /// <summary>List hub plans (<c>GET /hub/plans</c>). <paramref name="hubId"/> is required by the spec.</summary>
    IAsyncEnumerable<HubPlan> ListPlansAsync(
        string hubId,
        string? planIds = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create hub plan orders in bulk (<c>POST /hub/plan/orders</c>) — the body is wrapped in <c>{ data: T[] }</c>.</summary>
    Task<HubPlanOrder> CreatePlanOrdersAsync(CreateHubPlanOrdersRequest request, CancellationToken cancellationToken = default);
}
