namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Assignments;

internal sealed class DriverVehicleAssignmentsClient : SamsaraServiceClientBase, IDriverVehicleAssignmentsClient
{
    private const string BasePath = "fleet/driver-vehicle-assignments";

    public DriverVehicleAssignmentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>
    /// List driver-vehicle assignments. The spec's <c>getDriverVehicleAssignments</c> requires
    /// <c>filterBy</c> (e.g. <c>"driverIds"</c>, <c>"vehicleIds"</c>) plus the matching id list.
    /// </summary>
    public IAsyncEnumerable<DriverVehicleAssignment> ListAsync(
        string filterBy,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? vehicleIds = null,
        string? assignmentType = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DriverVehicleAssignment>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange(BasePath, startTime, endTime),
                ("filterBy", filterBy),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("assignmentType", assignmentType)),
            cancellationToken: cancellationToken);

    public Task<DriverVehicleAssignment> CreateAsync(CreateDriverVehicleAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverVehicleAssignment>(BasePath, request, cancellationToken);

    /// <summary>Update an assignment. Identifiers (driverId/vehicleId/startTime) live in the body.</summary>
    public Task<DriverVehicleAssignment> UpdateAsync(UpdateDriverVehicleAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<DriverVehicleAssignment>(BasePath, request, cancellationToken);

    /// <summary>End/delete an assignment. The vehicleId (and optional fields) live in the body.</summary>
    public Task DeleteAsync(DeleteDriverVehicleAssignmentsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(BasePath, request, cancellationToken);
}
