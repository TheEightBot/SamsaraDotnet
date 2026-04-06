namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>
/// Client for Samsara tachograph data.
/// </summary>
public interface ITachographClient
{
    IAsyncEnumerable<TachographActivity> ListActivitiesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TachographFile> ListFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
