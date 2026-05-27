namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>
/// Client for Samsara compliance (HOS, DVIRs).
/// </summary>
public interface IComplianceClient
{
    IAsyncEnumerable<HosLog> ListHosLogsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<HosViolation> ListHosViolationsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<HosClocksForDriver>> GetHosClocksAsync(IReadOnlyList<string> driverIds, CancellationToken cancellationToken = default);
    IAsyncEnumerable<HosDailyLog> ListHosDailyLogsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<HosEldEvent> ListHosEldEventsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default);
    /// <summary>HOS authentication logs (v1 legacy).</summary>
    IAsyncEnumerable<object> V1ListHosAuthenticationLogsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    /// <summary>Set a driver's current duty status (v1 legacy).</summary>
    Task V1SetCurrentDutyStatusAsync(string driverId, object request, CancellationToken cancellationToken = default);
    /// <summary>Update shipping-doc metadata on HOS daily logs (beta).</summary>
    Task<object> UpdateShippingDocsAsync(string driverID, string hosDate, object request, CancellationToken cancellationToken = default);
}
