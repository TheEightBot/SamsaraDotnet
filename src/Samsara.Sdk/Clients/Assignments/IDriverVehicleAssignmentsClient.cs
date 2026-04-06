namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>
/// Client for managing driver-vehicle assignments.
/// </summary>
public interface IDriverVehicleAssignmentsClient
{
    IAsyncEnumerable<DriverVehicleAssignment> ListAsync(CancellationToken cancellationToken = default);
    Task<DriverVehicleAssignment> CreateAsync(CreateDriverVehicleAssignmentRequest request, CancellationToken cancellationToken = default);
    Task<DriverVehicleAssignment> UpdateAsync(string id, UpdateDriverVehicleAssignmentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
