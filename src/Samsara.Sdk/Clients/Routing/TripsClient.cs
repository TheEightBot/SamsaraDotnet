namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

internal sealed class TripsClient : SamsaraServiceClientBase, ITripsClient
{
    private const string BasePath = "v1/fleet/trips";

    public TripsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public async Task<IReadOnlyList<V1Trip>> ListAsync(string vehicleId, long startMs, long endMs, CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(BasePath,
            ("vehicleId", vehicleId),
            ("startMs", startMs.ToString(System.Globalization.CultureInfo.InvariantCulture)),
            ("endMs", endMs.ToString(System.Globalization.CultureInfo.InvariantCulture)));
        var response = await HttpClient.GetAsync<V1TripsResponse>(path, cancellationToken).ConfigureAwait(false);
        return response.Trips ?? [];
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
