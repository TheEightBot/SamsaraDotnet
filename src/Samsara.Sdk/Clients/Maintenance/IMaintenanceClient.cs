namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Maintenance;

/// <summary>
/// Client for Samsara maintenance (DVIRs, diagnostics).
/// </summary>
public interface IMaintenanceClient
{
    IAsyncEnumerable<MaintenanceDvir> ListDvirsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<MaintenanceDvir> GetDvirAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DiagnosticTroubleCode> ListDtcsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<MaintenanceDvir> GetDvirsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<MaintenanceDvir> GetDvirByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<MaintenanceDvir> CreateDvirAsync(CreateDvirRequest request, CancellationToken cancellationToken = default);
    Task<MaintenanceDvir> UpdateDvirAsync(string id, UpdateDvirRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DefectRecord> GetDefectsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<DefectRecord> GetDefectAsync(string id, CancellationToken cancellationToken = default);
    Task<DefectRecord> UpdateDefectAsync(string id, UpdateDefectRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DefectType> ListDefectTypesAsync(CancellationToken cancellationToken = default);
}
