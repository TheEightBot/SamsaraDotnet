namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Maintenance;

/// <summary>Client for Samsara maintenance work orders and service tasks.</summary>
public interface IWorkOrdersClient
{
    Task<InvoiceScan> PostInvoiceScanAsync(PostInvoiceScanRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<ServiceTask> ListServiceTasksAsync(IReadOnlyList<string>? ids = null, bool? includeArchived = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<WorkOrder> ListWorkOrdersAsync(IReadOnlyList<string>? ids = null, bool? includeExternalIds = null, CancellationToken cancellationToken = default);
    Task<WorkOrder> CreateWorkOrderAsync(CreateWorkOrderRequest request, CancellationToken cancellationToken = default);
    Task<WorkOrder> UpdateWorkOrderAsync(UpdateWorkOrderRequest request, CancellationToken cancellationToken = default);
    Task DeleteWorkOrdersAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<WorkOrder> GetWorkOrdersStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? assetIds = null, IReadOnlyList<string>? assignedUserIds = null, IReadOnlyList<string>? workOrderStatuses = null, bool? includeExternalIds = null, CancellationToken cancellationToken = default);

    /// <summary>Get work order templates (<c>GET /maintenance/work-order-templates</c>) — beta. Loosely typed.</summary>
    IAsyncEnumerable<object> GetWorkOrderTemplatesAsync(CancellationToken cancellationToken = default);
}
