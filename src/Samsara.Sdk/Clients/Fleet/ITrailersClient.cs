namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for managing Samsara trailers.
/// </summary>
public interface ITrailersClient
{
    IAsyncEnumerable<Trailer> ListAsync(string? parentTagIds = null, string? tagIds = null, CancellationToken cancellationToken = default);
    Task<Trailer> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Trailer> CreateAsync(CreateTrailerRequest request, CancellationToken cancellationToken = default);
    Task<Trailer> UpdateAsync(string id, UpdateTrailerRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a trailer stats snapshot (<c>GET /fleet/trailers/stats</c>). <paramref name="types"/> is
    /// spec-required.
    /// </summary>
    IAsyncEnumerable<TrailerStats> GetStatsSnapshotAsync(string types, string? parentTagIds = null, string? tagIds = null, string? time = null, string? trailerIds = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stream the trailer stats feed (<c>GET /fleet/trailers/stats/feed</c>). <paramref name="types"/> is
    /// spec-required.
    /// </summary>
    IAsyncEnumerable<TrailerStats> GetStatsFeedAsync(string types, string? decorations = null, string? parentTagIds = null, string? tagIds = null, string? trailerIds = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get trailer stats history over a time window (<c>GET /fleet/trailers/stats/history</c>).
    /// <paramref name="types"/> is spec-required.
    /// </summary>
    IAsyncEnumerable<TrailerStats> GetStatsHistoryAsync(string types, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, string? decorations = null, string? parentTagIds = null, string? tagIds = null, string? trailerIds = null, CancellationToken cancellationToken = default);
}
