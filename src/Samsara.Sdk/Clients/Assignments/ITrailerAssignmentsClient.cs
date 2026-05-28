namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>
/// Client for managing trailer assignments.
/// </summary>
public interface ITrailerAssignmentsClient
{
    IAsyncEnumerable<TrailerAssignment> ListAsync(long? startMs = null, long? endMs = null, CancellationToken cancellationToken = default);
    /// <summary>Assignments for a specific trailer.</summary>
    IAsyncEnumerable<TrailerAssignment> GetByTrailerAsync(string trailerId, long? startMs = null, long? endMs = null, CancellationToken cancellationToken = default);
}
