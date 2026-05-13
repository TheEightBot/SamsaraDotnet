namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>Client for managing driver-trailer assignments.</summary>
public interface IDriverTrailerAssignmentsClient
{
    IAsyncEnumerable<DriverTrailerAssignment> ListAsync(CancellationToken cancellationToken = default);
    Task<DriverTrailerAssignment> CreateAsync(CreateDriverTrailerAssignmentRequest request, CancellationToken cancellationToken = default);
    Task<DriverTrailerAssignment> UpdateAsync(UpdateDriverTrailerAssignmentRequest request, CancellationToken cancellationToken = default);
}
