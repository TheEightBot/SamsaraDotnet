namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Maintenance;

internal sealed class WorkOrdersClient : SamsaraServiceClientBase, IWorkOrdersClient
{
    private const string BasePath = "maintenance/work-orders";

    public WorkOrdersClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<InvoiceScan> PostInvoiceScanAsync(PostInvoiceScanRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<InvoiceScan>("maintenance/invoice-scans", request, cancellationToken);

    public IAsyncEnumerable<ServiceTask> ListServiceTasksAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<ServiceTask>("maintenance/service-tasks", cancellationToken: cancellationToken);

    public IAsyncEnumerable<WorkOrder> ListWorkOrdersAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<WorkOrder>(BasePath, cancellationToken: cancellationToken);

    public Task<WorkOrder> CreateWorkOrderAsync(CreateWorkOrderRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<WorkOrder>(BasePath, request, cancellationToken);

    public Task<WorkOrder> UpdateWorkOrderAsync(UpdateWorkOrderRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<WorkOrder>(BasePath, request, cancellationToken);

    public Task DeleteWorkOrdersAsync(string[] ids, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}?ids={string.Join("&ids=", ids.Select(Uri.EscapeDataString))}", cancellationToken);

    public IAsyncEnumerable<WorkOrder> GetWorkOrdersStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<WorkOrder>(QueryBuilder.WithTimeRange($"{BasePath}/stream", startTime, endTime), cancellationToken: cancellationToken);
}
