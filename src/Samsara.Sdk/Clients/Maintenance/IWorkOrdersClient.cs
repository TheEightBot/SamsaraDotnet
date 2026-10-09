namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
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

    /// <summary>Get work order templates (<c>GET /maintenance/work-order-templates</c>) — beta.</summary>
    IAsyncEnumerable<WorkOrderTemplate> GetWorkOrderTemplatesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Attach a file to a work order (<c>POST /maintenance/work-orders/attachments</c>,
    /// <c>postWorkOrderAttachment</c>) — beta. The file travels base64-encoded in the body.
    /// </summary>
    /// <param name="request">The work order and the file to attach.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The created attachment's ID.</returns>
    [Experimental("SAMSARA001")]
    Task<MaintenanceAttachment> PostWorkOrderAttachmentAsync(
        PostWorkOrderAttachmentRequest request,
        CancellationToken cancellationToken = default);
}
