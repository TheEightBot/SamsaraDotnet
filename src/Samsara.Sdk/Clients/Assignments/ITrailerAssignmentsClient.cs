namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>
/// Client for managing trailer assignments.
/// </summary>
public interface ITrailerAssignmentsClient
{
    IAsyncEnumerable<TrailerAssignment> ListAsync(CancellationToken cancellationToken = default);
}
