namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

/// <summary>Real-time route-event stream (<c>/route-events/stream</c>).</summary>
public interface IRouteEventsClient
{
    /// <summary>Stream route events (<c>GET /route-events/stream</c>).</summary>
    IAsyncEnumerable<RouteEvent> GetStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);
}

internal sealed class RouteEventsClient : SamsaraServiceClientBase, IRouteEventsClient
{
    public RouteEventsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<RouteEvent> GetStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<RouteEvent>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("route-events/stream", startTime, endTime),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);
}
