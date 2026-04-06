namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>
/// Client for managing carrier proposed assignments.
/// </summary>
public interface ICarrierProposedAssignmentsClient
{
    IAsyncEnumerable<CarrierProposedAssignment> ListAsync(CancellationToken cancellationToken = default);
    Task<CarrierProposedAssignment> CreateAsync(CreateCarrierProposedAssignmentRequest request, CancellationToken cancellationToken = default);
    Task<CarrierProposedAssignment> UpdateAsync(string id, UpdateCarrierProposedAssignmentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
