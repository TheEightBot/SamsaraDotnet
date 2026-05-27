namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Beta — Ridership passengers and route setups (<c>/ridership/*</c>).</summary>
public interface IRidershipClient
{
    // Passengers
    /// <summary>List passengers (<c>GET /ridership/passengers</c>) — required <paramref name="tagId"/>.</summary>
    IAsyncEnumerable<object> ListPassengersAsync(
        string tagId,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);
    Task<object> GetPassengerAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);
    Task<object> CreatePassengerAsync(object request, CancellationToken cancellationToken = default);
    /// <summary>Update (<c>PUT /ridership/passengers</c>) — required <paramref name="id"/> query param.</summary>
    Task<object> UpdatePassengerAsync(string id, object request, CancellationToken cancellationToken = default);
    Task DeletePassengerAsync(string id, CancellationToken cancellationToken = default);

    // Route setups
    /// <summary>List route setups (<c>GET /ridership/route-setups</c>) — required <paramref name="accountId"/>.</summary>
    IAsyncEnumerable<object> ListRouteSetupsAsync(string accountId, CancellationToken cancellationToken = default);
    Task<object> GetRouteSetupAsync(string routeId, CancellationToken cancellationToken = default);
    Task<object> CreateRouteSetupAsync(object request, CancellationToken cancellationToken = default);
    /// <summary>Update (<c>PUT /ridership/route-setups</c>) — required <paramref name="routeId"/> query param.</summary>
    Task<object> UpdateRouteSetupAsync(string routeId, object request, CancellationToken cancellationToken = default);
    Task DeleteRouteSetupAsync(string routeId, CancellationToken cancellationToken = default);
}

internal sealed class RidershipClient : SamsaraServiceClientBase, IRidershipClient
{
    private const string PassengersPath = "ridership/passengers";
    private const string RouteSetupsPath = "ridership/route-setups";

    public RidershipClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> ListPassengersAsync(
        string tagId,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(PassengersPath,
                ("tagId", tagId),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<object> GetPassengerAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<object>(
            QueryBuilder.WithParams($"{PassengersPath}/{Uri.EscapeDataString(id)}",
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken);

    public Task<object> CreatePassengerAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<object>(PassengersPath, request, cancellationToken);

    public Task<object> UpdatePassengerAsync(string id, object request, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<object>(QueryBuilder.WithParams(PassengersPath, ("id", id)), request, cancellationToken);

    public Task DeletePassengerAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(PassengersPath, ("id", id)), cancellationToken);

    public IAsyncEnumerable<object> ListRouteSetupsAsync(string accountId, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithParams(RouteSetupsPath, ("accountId", accountId)), cancellationToken: cancellationToken);

    public Task<object> GetRouteSetupAsync(string routeId, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<object>($"{RouteSetupsPath}/{Uri.EscapeDataString(routeId)}", cancellationToken);

    public Task<object> CreateRouteSetupAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<object>(RouteSetupsPath, request, cancellationToken);

    public Task<object> UpdateRouteSetupAsync(string routeId, object request, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<object>(QueryBuilder.WithParams(RouteSetupsPath, ("routeId", routeId)), request, cancellationToken);

    public Task DeleteRouteSetupAsync(string routeId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(RouteSetupsPath, ("routeId", routeId)), cancellationToken);
}
