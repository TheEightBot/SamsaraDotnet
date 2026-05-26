namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Beta — Ridership passengers and route setups (<c>/ridership/*</c>).</summary>
public interface IRidershipClient
{
    // Passengers
    IAsyncEnumerable<object> ListPassengersAsync(CancellationToken cancellationToken = default);
    Task<object> GetPassengerAsync(string id, CancellationToken cancellationToken = default);
    Task<object> CreatePassengerAsync(object request, CancellationToken cancellationToken = default);
    /// <summary>Update (PUT /ridership/passengers) — id in body.</summary>
    Task<object> UpdatePassengerAsync(object request, CancellationToken cancellationToken = default);
    Task DeletePassengerAsync(string id, CancellationToken cancellationToken = default);

    // Route setups
    IAsyncEnumerable<object> ListRouteSetupsAsync(CancellationToken cancellationToken = default);
    Task<object> GetRouteSetupAsync(string routeId, CancellationToken cancellationToken = default);
    Task<object> CreateRouteSetupAsync(object request, CancellationToken cancellationToken = default);
    Task<object> UpdateRouteSetupAsync(object request, CancellationToken cancellationToken = default);
    Task DeleteRouteSetupAsync(string routeId, CancellationToken cancellationToken = default);
}

internal sealed class RidershipClient : SamsaraServiceClientBase, IRidershipClient
{
    private const string PassengersPath = "ridership/passengers";
    private const string RouteSetupsPath = "ridership/route-setups";

    public RidershipClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> ListPassengersAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>(PassengersPath, cancellationToken: cancellationToken);

    public Task<object> GetPassengerAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<object>($"{PassengersPath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<object> CreatePassengerAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<object>(PassengersPath, request, cancellationToken);

    public Task<object> UpdatePassengerAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<object>(PassengersPath, request, cancellationToken);

    public Task DeletePassengerAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(PassengersPath, ("id", id)), cancellationToken);

    public IAsyncEnumerable<object> ListRouteSetupsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>(RouteSetupsPath, cancellationToken: cancellationToken);

    public Task<object> GetRouteSetupAsync(string routeId, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<object>($"{RouteSetupsPath}/{Uri.EscapeDataString(routeId)}", cancellationToken);

    public Task<object> CreateRouteSetupAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<object>(RouteSetupsPath, request, cancellationToken);

    public Task<object> UpdateRouteSetupAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<object>(RouteSetupsPath, request, cancellationToken);

    public Task DeleteRouteSetupAsync(string routeId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(RouteSetupsPath, ("routeId", routeId)), cancellationToken);
}
