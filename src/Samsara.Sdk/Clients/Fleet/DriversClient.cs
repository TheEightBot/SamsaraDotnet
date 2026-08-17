namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Drivers;

internal sealed class DriversClient : SamsaraServiceClientBase, IDriversClient
{
    private const string BasePath = "fleet/drivers";

    public DriversClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Driver> ListAsync(
        string? driverActivationStatus = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? attributeValueIds = null,
        IReadOnlyList<string>? attributes = null,
        string? updatedAfterTime = null,
        string? createdAfterTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Driver>(
            QueryBuilder.WithParams(BasePath,
                ("driverActivationStatus", driverActivationStatus),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("attributeValueIds", attributeValueIds is null ? null : string.Join(",", attributeValueIds)),
                ("attributes", attributes is null ? null : string.Join(",", attributes)),
                ("updatedAfterTime", updatedAfterTime),
                ("createdAfterTime", createdAfterTime)),
            cancellationToken: cancellationToken);

    public Task<Driver> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Driver>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Driver> CreateAsync(CreateDriverRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Driver>(BasePath, request, cancellationToken);

    public Task<Driver> UpdateAsync(string id, UpdateDriverRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Driver>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public async Task RemoteSignOutAsync(RemoteSignOutRequest request, CancellationToken cancellationToken = default)
        => await HttpClient.PostAsync("fleet/drivers/remote-sign-out", request, cancellationToken).ConfigureAwait(false);

    public Task<DriverAuthToken> CreateAuthTokenAsync(CreateDriverAuthTokenRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverAuthToken>("fleet/drivers/auth-token", request, cancellationToken);

    public IAsyncEnumerable<DriverQrCode> ListQrCodesAsync(
        IReadOnlyList<string> driverIds,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DriverQrCode>(
            QueryBuilder.WithParams("drivers/qr-codes",
                ("driverIds", string.Join(",", driverIds))),
            cancellationToken: cancellationToken);

    public Task<DriverQrCode> CreateQrCodeAsync(CreateDriverQrCodeRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverQrCode>("drivers/qr-codes", request, cancellationToken);

    public Task DeleteQrCodeAsync(string driverId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"drivers/qr-codes?driverId={Uri.EscapeDataString(driverId)}", cancellationToken);

    // ── Beta ─────────────────────────────────────────────────────────────────

    /// <summary>List driver workflows (beta, <c>GET /fleet/drivers/workflows</c>).</summary>
    public IAsyncEnumerable<DriverWorkflow> ListWorkflowsAsync(
        string? workflowType = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DriverWorkflow>(
            QueryBuilder.WithParams("fleet/drivers/workflows",
                ("workflowType", workflowType)),
            cancellationToken: cancellationToken);

    /// <summary>Assign a workflow to a driver (beta, <c>POST /fleet/drivers/workflow-assignments</c>).</summary>
    public Task<DriverWorkflowAssignment> CreateWorkflowAssignmentAsync(CreateDriverWorkflowAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverWorkflowAssignment>("fleet/drivers/workflow-assignments", request, cancellationToken);

    /// <summary>Resolve a voice sign-in assignment (beta).</summary>
    public Task<VoiceSignInAssignment> ResolveVoiceSignInAssignmentAsync(ResolveVoiceSignInAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<VoiceSignInAssignment>("fleet/drivers/voice-sign-in/resolve-assignment", request, cancellationToken);
}
