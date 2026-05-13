namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

internal sealed class HubsClient : SamsaraServiceClientBase, IHubsClient
{
    private const string BasePath = "addresses";

    public HubsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Hub> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Hub>(BasePath, cancellationToken: cancellationToken);

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
