namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

internal sealed class RoutesClient : SamsaraServiceClientBase, IRoutesClient
{
    private const string BasePath = "fleet/routes";

    public RoutesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Route> ListAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? include = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Route>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange(BasePath, startTime, endTime),
                ("include", include is null ? null : string.Join(",", include)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken: cancellationToken);

    public Task<Route> GetAsync(string id, IReadOnlyList<string>? include = null, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Route>(
            QueryBuilder.WithParams(
                $"{BasePath}/{Uri.EscapeDataString(id)}",
                ("include", include is null ? null : string.Join(",", include))),
            cancellationToken);

    public Task<Route> CreateAsync(CreateRouteRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Route>(BasePath, request, cancellationToken);

    public Task<Route> UpdateAsync(string id, UpdateRouteRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Route>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<RouteAuditEvent> GetAuditLogFeedAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? expand = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<RouteAuditEvent>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/routes/audit-logs/feed", startTime, endTime),
                ("expand", expand)),
            cancellationToken: cancellationToken);

    /// <summary>Delete a dispatch route (v1) by id or external id.</summary>
    public Task V1DeleteDispatchRouteAsync(string idOrExternalId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"v1/fleet/dispatch/routes/{Uri.EscapeDataString(idOrExternalId)}", cancellationToken);
}
