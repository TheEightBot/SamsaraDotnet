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
    public IAsyncEnumerable<Hub> ListHubsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Hub>("hubs", cancellationToken: cancellationToken);

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

    public IAsyncEnumerable<HubCapacity> ListCapacitiesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<HubCapacity>("hub/capacities", cancellationToken: cancellationToken);

    public IAsyncEnumerable<HubCustomProperty> ListCustomPropertiesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<HubCustomProperty>("hub/customProperties", cancellationToken: cancellationToken);

    public Task<HubLocation> UpdateLocationAsync(string id, UpdateHubLocationRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<HubLocation>($"hub/location/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<HubLocation> ListLocationsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<HubLocation>("hub/locations", cancellationToken: cancellationToken);

    public Task<HubLocation> CreateLocationAsync(CreateHubLocationRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<HubLocation>("hub/locations", request, cancellationToken);

    public IAsyncEnumerable<HubSkill> ListSkillsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<HubSkill>("hub/skills", cancellationToken: cancellationToken);

    public Task<HubPlan> CreatePlanAsync(CreateHubPlanRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<HubPlan>("hub/plan", request, cancellationToken);

    public IAsyncEnumerable<HubPlan> ListPlansAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<HubPlan>("hub/plans", cancellationToken: cancellationToken);

    public Task<HubPlanOrder> CreatePlanOrdersAsync(CreateHubPlanOrdersRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<HubPlanOrder>("hub/plan/orders", request, cancellationToken);
}
