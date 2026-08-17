namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Maintenance;

internal sealed class WorkOrdersClient : SamsaraServiceClientBase, IWorkOrdersClient
{
    private const string BasePath = "maintenance/work-orders";

    public WorkOrdersClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<InvoiceScan> PostInvoiceScanAsync(PostInvoiceScanRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<InvoiceScan>("maintenance/invoice-scans", request, cancellationToken);

    public IAsyncEnumerable<ServiceTask> ListServiceTasksAsync(IReadOnlyList<string>? ids = null, bool? includeArchived = null, CancellationToken cancellationToken = default)
        => PaginateAsync<ServiceTask>(
            QueryBuilder.WithParams("maintenance/service-tasks",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("includeArchived", includeArchived?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<WorkOrder> ListWorkOrdersAsync(IReadOnlyList<string>? ids = null, bool? includeExternalIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<WorkOrder>(
            QueryBuilder.WithParams(BasePath,
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<WorkOrder> CreateWorkOrderAsync(CreateWorkOrderRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<WorkOrder>(BasePath, request, cancellationToken);

    public Task<WorkOrder> UpdateWorkOrderAsync(UpdateWorkOrderRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<WorkOrder>(BasePath, request, cancellationToken);

    public Task DeleteWorkOrdersAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);

    public IAsyncEnumerable<WorkOrder> GetWorkOrdersStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? assetIds = null, IReadOnlyList<string>? assignedUserIds = null, IReadOnlyList<string>? workOrderStatuses = null, bool? includeExternalIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<WorkOrder>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange($"{BasePath}/stream", startTime, endTime),
                ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
                ("assignedUserIds", assignedUserIds is null ? null : string.Join(",", assignedUserIds)),
                ("workOrderStatuses", workOrderStatuses is null ? null : string.Join(",", workOrderStatuses)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    /// <summary>Get work order templates (<c>GET /maintenance/work-order-templates</c>) — beta.</summary>
    public IAsyncEnumerable<WorkOrderTemplate> GetWorkOrderTemplatesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<WorkOrderTemplate>("maintenance/work-order-templates", cancellationToken: cancellationToken);
}
