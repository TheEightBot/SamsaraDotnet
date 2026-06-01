namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Routes;

/// <summary>
/// Client for retrieving Samsara trip data.
/// </summary>
public interface ITripsClient
{
    /// <summary>
    /// List a vehicle's trips over a time window via the legacy
    /// <c>GET /v1/fleet/trips</c> endpoint. <paramref name="vehicleId"/>,
    /// <paramref name="startMs"/> and <paramref name="endMs"/> (Unix milliseconds)
    /// are all spec-required. Returns the trips in a single page (this v1 endpoint
    /// is not cursor-paginated).
    /// </summary>
    Task<IReadOnlyList<V1Trip>> ListAsync(string vehicleId, long startMs, long endMs, CancellationToken cancellationToken = default);
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
