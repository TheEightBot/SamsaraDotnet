namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

internal sealed class HubsClient : SamsaraServiceClientBase, IHubsClient
{
    private const string BasePath = "addresses";

    public HubsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Hub> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Hub>(BasePath, cancellationToken: cancellationToken);

    /// <summary>List hubs via the dedicated <c>GET /hubs</c> endpoint (listHubs).</summary>
    public IAsyncEnumerable<Hub> ListHubsAsync(
        string? hubIds = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Hub>(
            QueryBuilder.WithParams("hubs",
                ("hubIds", hubIds),
                ("startTime", startTime),
                ("endTime", endTime)),
            cancellationToken: cancellationToken);

    /// <summary>List hub plan routes (<c>GET /hub/plan/routes</c>).</summary>
    public IAsyncEnumerable<object> ListPlanRoutesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("hub/plan/routes", cancellationToken: cancellationToken);

    /// <summary>
    /// List hub plan orders (<c>GET /hub/plan/orders</c>) — <paramref name="planId"/> is required by the spec.
    /// </summary>
    public IAsyncEnumerable<HubPlanOrder> ListPlanOrdersAsync(
        string planId,
        string? orderIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HubPlanOrder>(
            QueryBuilder.WithParams("hub/plan/orders",
                ("planId", planId),
                ("orderIds", orderIds)),
            cancellationToken: cancellationToken);

    /// <summary>
    /// Delete one or more hub plan orders (<c>DELETE /hub/plan/orders</c>) — <paramref name="planId"/> required.
    /// </summary>
    public Task DeletePlanOrdersAsync(
        string planId,
        string? orderIds = null,
        bool? deleteAll = null,
        CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(
            QueryBuilder.WithParams("hub/plan/orders",
                ("planId", planId),
                ("orderIds", orderIds),
                ("deleteAll", deleteAll?.ToString().ToLowerInvariant())),
            cancellationToken);

    /// <summary>
    /// List hub route templates (<c>GET /hub/route-templates</c>) — <paramref name="hubId"/> required.
    /// </summary>
    public IAsyncEnumerable<object> ListRouteTemplatesAsync(
        string hubId,
        string? id = null,
        string? name = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams("hub/route-templates",
                ("hubId", hubId),
                ("id", id),
                ("name", name)),
            cancellationToken: cancellationToken);

    /// <summary>
    /// Delete one or more hub route templates (<c>DELETE /hub/route-templates</c>) — <paramref name="id"/> required.
    /// </summary>
    public Task DeleteRouteTemplatesAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams("hub/route-templates", ("id", id)), cancellationToken);

    public Task<Hub> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Hub>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Hub> CreateAsync(CreateHubRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Hub>(BasePath, request, cancellationToken);

    public Task<Hub> UpdateAsync(string id, UpdateHubRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Hub>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    /// <summary>List hub capacities (<c>GET /hub/capacities</c>) — <paramref name="hubId"/> required by spec.</summary>
    public IAsyncEnumerable<HubCapacity> ListCapacitiesAsync(
        string hubId,
        string? capacityIds = null,
        string? capacityNames = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HubCapacity>(
            QueryBuilder.WithParams("hub/capacities",
                ("hubId", hubId),
                ("capacityIds", capacityIds),
                ("capacityNames", capacityNames),
                ("startTime", startTime),
                ("endTime", endTime)),
            cancellationToken: cancellationToken);

    /// <summary>List hub custom properties (<c>GET /hub/customProperties</c>) — <paramref name="hubId"/> required by spec.</summary>
    public IAsyncEnumerable<HubCustomProperty> ListCustomPropertiesAsync(
        string hubId,
        string? customPropertyIds = null,
        string? customPropertyNames = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HubCustomProperty>(
            QueryBuilder.WithParams("hub/customProperties",
                ("hubId", hubId),
                ("customPropertyIds", customPropertyIds),
                ("customPropertyNames", customPropertyNames),
                ("startTime", startTime),
                ("endTime", endTime)),
            cancellationToken: cancellationToken);

    /// <summary>Update a hub location (<c>PATCH /hub/location/{id}</c>). The spec wraps the body in <c>{ data: T }</c>.</summary>
    public Task<HubLocation> UpdateLocationAsync(string id, UpdateHubLocationEnvelopeRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<HubLocation>($"hub/location/{Uri.EscapeDataString(id)}", request, cancellationToken);

    /// <summary>List hub locations (<c>GET /hub/locations</c>) — <paramref name="hubId"/> required by spec.</summary>
    public IAsyncEnumerable<HubLocation> ListLocationsAsync(
        string hubId,
        string? locationIds = null,
        string? customerLocationIds = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HubLocation>(
            QueryBuilder.WithParams("hub/locations",
                ("hubId", hubId),
                ("locationIds", locationIds),
                ("customerLocationIds", customerLocationIds),
                ("startTime", startTime),
                ("endTime", endTime)),
            cancellationToken: cancellationToken);

    /// <summary>Create hub locations in bulk (<c>POST /hub/locations</c>). The spec wraps the array in <c>{ data: T[] }</c>.</summary>
    public Task<HubLocation> CreateLocationAsync(CreateHubLocationsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<HubLocation>("hub/locations", request, cancellationToken);

    /// <summary>List hub skills (<c>GET /hub/skills</c>) — <paramref name="hubId"/> required by spec.</summary>
    public IAsyncEnumerable<HubSkill> ListSkillsAsync(
        string hubId,
        string? skillIds = null,
        string? skillNames = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HubSkill>(
            QueryBuilder.WithParams("hub/skills",
                ("hubId", hubId),
                ("skillIds", skillIds),
                ("skillNames", skillNames),
                ("startTime", startTime),
                ("endTime", endTime)),
            cancellationToken: cancellationToken);

    public Task<HubPlan> CreatePlanAsync(CreateHubPlanRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<HubPlan>("hub/plan", request, cancellationToken);

    /// <summary>List hub plans (<c>GET /hub/plans</c>) — <paramref name="hubId"/> required by spec.</summary>
    public IAsyncEnumerable<HubPlan> ListPlansAsync(
        string hubId,
        string? planIds = null,
        string? startTime = null,
        string? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HubPlan>(
            QueryBuilder.WithParams("hub/plans",
                ("hubId", hubId),
                ("planIds", planIds),
                ("startTime", startTime),
                ("endTime", endTime)),
            cancellationToken: cancellationToken);

    /// <summary>Create hub plan orders in bulk (<c>POST /hub/plan/orders</c>). The spec wraps the array in <c>{ data: T[] }</c>.</summary>
    public Task<HubPlanOrder> CreatePlanOrdersAsync(CreateHubPlanOrdersRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<HubPlanOrder>("hub/plan/orders", request, cancellationToken);
}
