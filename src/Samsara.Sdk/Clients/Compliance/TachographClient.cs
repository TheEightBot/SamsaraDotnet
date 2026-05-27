namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class TachographClient : SamsaraServiceClientBase, ITachographClient
{
    public TachographClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<TachographActivity> ListActivitiesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TachographActivity>(QueryBuilder.WithTimeRange("fleet/drivers/tachograph-activity/history", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<TachographFile> ListFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TachographFile>(QueryBuilder.WithTimeRange("fleet/drivers/tachograph-files/history", startTime, endTime), cancellationToken: cancellationToken);

    /// <summary>Vehicle tachograph files history (<c>GET /fleet/vehicles/tachograph-files/history</c>).</summary>
    public IAsyncEnumerable<TachographFile> ListVehicleFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TachographFile>(QueryBuilder.WithTimeRange("fleet/vehicles/tachograph-files/history", startTime, endTime), cancellationToken: cancellationToken);

    /// <summary>Latest tachograph live-data (beta, <c>GET /fleet/tachograph-live-data/latest</c>).</summary>
    public IAsyncEnumerable<object> ListLiveDataAsync(
        string? driverIds = null,
        string? vehicleIds = null,
        DateTimeOffset? startTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams("fleet/tachograph-live-data/latest",
                ("driverIds", driverIds),
                ("vehicleIds", vehicleIds),
                ("startTime", startTime?.ToString("O"))),
            cancellationToken: cancellationToken);
}
