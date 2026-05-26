namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>
/// Client for managing driver-vehicle assignments.
/// </summary>
public interface IDriverVehicleAssignmentsClient
{
    /// <summary>
    /// List assignments. <paramref name="filterBy"/> is required by the spec
    /// (e.g. <c>"driverIds"</c>, <c>"vehicleIds"</c>).
    /// </summary>
    IAsyncEnumerable<DriverVehicleAssignment> ListAsync(
        string filterBy,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? vehicleIds = null,
        string? assignmentType = null,
        CancellationToken cancellationToken = default);

    Task<DriverVehicleAssignment> CreateAsync(CreateDriverVehicleAssignmentRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update an assignment. Identifiers live in the body.</summary>
    Task<DriverVehicleAssignment> UpdateAsync(UpdateDriverVehicleAssignmentRequest request, CancellationToken cancellationToken = default);

    /// <summary>End/delete an assignment by sending vehicleId (and optional fields) in the body.</summary>
    Task DeleteAsync(DeleteDriverVehicleAssignmentsRequest request, CancellationToken cancellationToken = default);
}
