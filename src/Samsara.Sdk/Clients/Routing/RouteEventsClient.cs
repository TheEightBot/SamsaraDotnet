namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Real-time route-event stream (<c>/route-events/stream</c>).</summary>
public interface IRouteEventsClient
{
    /// <summary>Stream route events (<c>GET /route-events/stream</c>).</summary>
    IAsyncEnumerable<object> GetStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}

internal sealed class RouteEventsClient : SamsaraServiceClientBase, IRouteEventsClient
{
    public RouteEventsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> GetStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("route-events/stream", startTime, endTime), cancellationToken: cancellationToken);
}
