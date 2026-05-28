namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Routes;

/// <summary>
/// Client for retrieving Samsara trip data.
/// </summary>
public interface ITripsClient
{
    IAsyncEnumerable<Trip> ListAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, string? vehicleId = null, string? driverId = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Stream trips over a time window (<c>GET /trips/stream</c>).
    /// <paramref name="ids"/> is spec-required.
    /// </summary>
    IAsyncEnumerable<Trip> GetStreamAsync(
        IReadOnlyList<string> ids,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? completionStatus = null,
        string? queryBy = null,
        bool? includeAsset = null,
        CancellationToken cancellationToken = default);
}
