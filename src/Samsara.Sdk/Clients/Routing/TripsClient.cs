namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Routes;

internal sealed class TripsClient : SamsaraServiceClientBase, ITripsClient
{
    private const string BasePath = "fleet/vehicles/trips";

    public TripsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Trip> ListAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, string? vehicleId = null, string? driverId = null, CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithTimeRange(BasePath, startTime, endTime);
        path = QueryBuilder.WithParams(path,
            ("vehicleId", vehicleId),
            ("driverId", driverId));
        return PaginateAsync<Trip>(path, cancellationToken: cancellationToken);
    }
}
