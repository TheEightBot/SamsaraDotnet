namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>
/// Client for Samsara tachograph data.
/// </summary>
public interface ITachographClient
{
    IAsyncEnumerable<TachographActivity> ListActivitiesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TachographFile> ListFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    /// <summary>Vehicle tachograph files history (<c>GET /fleet/vehicles/tachograph-files/history</c>).</summary>
    IAsyncEnumerable<TachographFile> ListVehicleFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    /// <summary>Latest tachograph live-data (beta).</summary>
    IAsyncEnumerable<object> ListLiveDataAsync(CancellationToken cancellationToken = default);
}
