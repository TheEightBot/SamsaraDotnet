namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Maintenance;

/// <summary>Client for Samsara maintenance work orders and service tasks.</summary>
public interface IWorkOrdersClient
{
    Task<InvoiceScan> PostInvoiceScanAsync(PostInvoiceScanRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<ServiceTask> ListServiceTasksAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<WorkOrder> ListWorkOrdersAsync(CancellationToken cancellationToken = default);
    Task<WorkOrder> CreateWorkOrderAsync(CreateWorkOrderRequest request, CancellationToken cancellationToken = default);
    Task<WorkOrder> UpdateWorkOrderAsync(UpdateWorkOrderRequest request, CancellationToken cancellationToken = default);
    Task DeleteWorkOrdersAsync(string[] ids, CancellationToken cancellationToken = default);
    IAsyncEnumerable<WorkOrder> GetWorkOrdersStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
