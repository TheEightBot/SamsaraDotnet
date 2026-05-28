namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

internal sealed class TripsClient : SamsaraServiceClientBase, ITripsClient
{
    private const string BasePath = "v1/fleet/trips";

    public TripsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Trip> ListAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, string? vehicleId = null, string? driverId = null, CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithTimeRange(BasePath, startTime, endTime);
        path = QueryBuilder.WithParams(path,
            ("vehicleId", vehicleId),
            ("driverId", driverId));
        return PaginateAsync<Trip>(path, cancellationToken: cancellationToken);
    }

    public IAsyncEnumerable<Trip> GetStreamAsync(
        IReadOnlyList<string> ids,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? completionStatus = null,
        string? queryBy = null,
        bool? includeAsset = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Trip>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("trips/stream", startTime, endTime),
                ("ids", string.Join(",", ids)),
                ("completionStatus", completionStatus),
                ("queryBy", queryBy),
                ("includeAsset", includeAsset?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);
}
