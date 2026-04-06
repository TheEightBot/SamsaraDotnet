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
}
