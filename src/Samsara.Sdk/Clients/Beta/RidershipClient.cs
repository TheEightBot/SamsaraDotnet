namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>Beta — Ridership passengers and route setups (<c>/ridership/*</c>).</summary>
public interface IRidershipClient
{
    // Passengers
    /// <summary>List passengers (<c>GET /ridership/passengers</c>) — required <paramref name="tagId"/>.</summary>
    IAsyncEnumerable<RidershipPassenger> ListPassengersAsync(
        string tagId,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get a passenger (<c>GET /ridership/passengers/{id}</c>).</summary>
    Task<RidershipPassenger> GetPassengerAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create a passenger (<c>POST /ridership/passengers</c>).</summary>
    Task<RidershipPassenger> CreatePassengerAsync(
        RidershipPassengerInput request,
        CancellationToken cancellationToken = default);

    /// <summary>Update (<c>PUT /ridership/passengers</c>) — required <paramref name="id"/> query param.</summary>
    Task<RidershipPassenger> UpdatePassengerAsync(
        string id,
        RidershipPassengerInput request,
        CancellationToken cancellationToken = default);

    /// <summary>Delete a passenger (<c>DELETE /ridership/passengers</c>) — id in query.</summary>
    Task DeletePassengerAsync(string id, CancellationToken cancellationToken = default);

    // Route setups
    /// <summary>List route setups (<c>GET /ridership/route-setups</c>) — required <paramref name="accountId"/>.</summary>
    IAsyncEnumerable<RidershipRouteSetup> ListRouteSetupsAsync(string accountId, CancellationToken cancellationToken = default);

    /// <summary>Get a route setup (<c>GET /ridership/route-setups/{routeId}</c>).</summary>
    Task<RidershipRouteSetup> GetRouteSetupAsync(string routeId, CancellationToken cancellationToken = default);

    /// <summary>Create a route setup (<c>POST /ridership/route-setups</c>).</summary>
    Task<RidershipRouteSetup> CreateRouteSetupAsync(
        RidershipRouteSetupCreateRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Update (<c>PUT /ridership/route-setups</c>) — required <paramref name="routeId"/> query param.</summary>
    Task<RidershipRouteSetup> UpdateRouteSetupAsync(
        string routeId,
        RidershipRouteSetupUpdateRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Delete a route setup (<c>DELETE /ridership/route-setups</c>) — routeId in query.</summary>
    Task DeleteRouteSetupAsync(string routeId, CancellationToken cancellationToken = default);
}

internal sealed class RidershipClient : SamsaraServiceClientBase, IRidershipClient
{
    private const string PassengersPath = "ridership/passengers";
    private const string RouteSetupsPath = "ridership/route-setups";

    public RidershipClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<RidershipPassenger> ListPassengersAsync(
        string tagId,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<RidershipPassenger>(
            QueryBuilder.WithParams(PassengersPath,
                ("tagId", tagId),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<RidershipPassenger> GetPassengerAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<RidershipPassenger>(
            QueryBuilder.WithParams($"{PassengersPath}/{Uri.EscapeDataString(id)}",
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken);

    public Task<RidershipPassenger> CreatePassengerAsync(
        RidershipPassengerInput request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<RidershipPassenger>(PassengersPath, request, cancellationToken);

    public Task<RidershipPassenger> UpdatePassengerAsync(
        string id,
        RidershipPassengerInput request,
        CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<RidershipPassenger>(
            QueryBuilder.WithParams(PassengersPath, ("id", id)), request, cancellationToken);

    public Task DeletePassengerAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(PassengersPath, ("id", id)), cancellationToken);

    public IAsyncEnumerable<RidershipRouteSetup> ListRouteSetupsAsync(string accountId, CancellationToken cancellationToken = default)
        => PaginateAsync<RidershipRouteSetup>(QueryBuilder.WithParams(RouteSetupsPath, ("accountId", accountId)), cancellationToken: cancellationToken);

    public Task<RidershipRouteSetup> GetRouteSetupAsync(string routeId, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<RidershipRouteSetup>($"{RouteSetupsPath}/{Uri.EscapeDataString(routeId)}", cancellationToken);

    public Task<RidershipRouteSetup> CreateRouteSetupAsync(
        RidershipRouteSetupCreateRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<RidershipRouteSetup>(RouteSetupsPath, request, cancellationToken);

    public Task<RidershipRouteSetup> UpdateRouteSetupAsync(
        string routeId,
        RidershipRouteSetupUpdateRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<RidershipRouteSetup>(
            QueryBuilder.WithParams(RouteSetupsPath, ("routeId", routeId)), request, cancellationToken);

    public Task DeleteRouteSetupAsync(string routeId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(RouteSetupsPath, ("routeId", routeId)), cancellationToken);
}
