namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Assignments;

internal sealed class DriverVehicleAssignmentsClient : SamsaraServiceClientBase, IDriverVehicleAssignmentsClient
{
    private const string BasePath = "fleet/driver-vehicle-assignments";

    public DriverVehicleAssignmentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<DriverVehicleAssignment> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DriverVehicleAssignment>(BasePath, cancellationToken: cancellationToken);

    public Task<DriverVehicleAssignment> CreateAsync(CreateDriverVehicleAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverVehicleAssignment>(BasePath, request, cancellationToken);

    public Task<DriverVehicleAssignment> UpdateAsync(string id, UpdateDriverVehicleAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<DriverVehicleAssignment>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
