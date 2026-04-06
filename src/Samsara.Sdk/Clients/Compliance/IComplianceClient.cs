namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>
/// Client for Samsara compliance (HOS, DVIRs).
/// </summary>
public interface IComplianceClient
{
    IAsyncEnumerable<HosLog> ListHosLogsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<HosViolation> ListHosViolationsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<HosClocks> GetHosClocksAsync(string driverId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DvirEntry> ListDvirsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<DvirEntry> GetDvirAsync(string id, CancellationToken cancellationToken = default);
}
