namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for managing Samsara trailers.
/// </summary>
public interface ITrailersClient
{
    IAsyncEnumerable<Trailer> ListAsync(CancellationToken cancellationToken = default);
    Task<Trailer> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Trailer> CreateAsync(CreateTrailerRequest request, CancellationToken cancellationToken = default);
    Task<Trailer> UpdateAsync(string id, UpdateTrailerRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
