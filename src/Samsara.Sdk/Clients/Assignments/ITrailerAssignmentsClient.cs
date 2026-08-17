namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>
/// Client for the legacy v1 trailer-assignment endpoints.
/// </summary>
public interface ITrailerAssignmentsClient
{
    /// <summary>
    /// Assignments for every trailer in the organization
    /// (<c>GET /v1/fleet/trailers/assignments</c>), streamed across pages.
    /// </summary>
    /// <param name="startMs">Start of the window, in milliseconds since the Unix epoch.
    /// Omitting both bounds returns only current assignments.</param>
    /// <param name="endMs">End of the window, in milliseconds since the Unix epoch.
    /// Omitting it means "now".</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<V1TrailerWithAssignments> ListAsync(long? startMs = null, long? endMs = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Assignments for a single trailer
    /// (<c>GET /v1/fleet/trailers/{trailerId}/assignments</c>). The spec returns one
    /// object, not a page, so this is a single request rather than a stream.
    /// </summary>
    /// <param name="trailerId">ID of the trailer. Digits only.</param>
    /// <param name="startMs">Start of the window, in milliseconds since the Unix epoch.
    /// Omitting both bounds returns only current assignments.</param>
    /// <param name="endMs">End of the window, in milliseconds since the Unix epoch.
    /// Omitting it means "now".</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<V1TrailerWithAssignments> GetByTrailerAsync(string trailerId, long? startMs = null, long? endMs = null, CancellationToken cancellationToken = default);
}
