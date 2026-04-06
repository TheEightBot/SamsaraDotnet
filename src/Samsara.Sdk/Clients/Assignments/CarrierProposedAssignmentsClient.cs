namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Assignments;

internal sealed class CarrierProposedAssignmentsClient : SamsaraServiceClientBase, ICarrierProposedAssignmentsClient
{
    private const string BasePath = "fleet/carrier-proposed-assignments";

    public CarrierProposedAssignmentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<CarrierProposedAssignment> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<CarrierProposedAssignment>(BasePath, cancellationToken: cancellationToken);

    public Task<CarrierProposedAssignment> CreateAsync(CreateCarrierProposedAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<CarrierProposedAssignment>(BasePath, request, cancellationToken);

    public Task<CarrierProposedAssignment> UpdateAsync(string id, UpdateCarrierProposedAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<CarrierProposedAssignment>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
