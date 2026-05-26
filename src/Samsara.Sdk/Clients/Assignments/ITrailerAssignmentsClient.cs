namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>
/// Client for managing trailer assignments.
/// </summary>
public interface ITrailerAssignmentsClient
{
    IAsyncEnumerable<TrailerAssignment> ListAsync(CancellationToken cancellationToken = default);
    /// <summary>Assignments for a specific trailer.</summary>
    IAsyncEnumerable<TrailerAssignment> GetByTrailerAsync(string trailerId, CancellationToken cancellationToken = default);
}
