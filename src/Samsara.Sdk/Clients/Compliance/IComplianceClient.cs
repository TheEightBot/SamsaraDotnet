namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>
/// Client for Samsara compliance (HOS, DVIRs).
/// </summary>
public interface IComplianceClient
{
    /// <summary>List HOS logs (<c>GET /fleet/hos/logs</c>).</summary>
    /// <param name="startTime">Beginning of the time range (RFC 3339).</param>
    /// <param name="endTime">End of the time range (RFC 3339).</param>
    /// <param name="driverIds">Filter to the specified drivers.</param>
    /// <param name="tagIds">Filter to the specified tags.</param>
    /// <param name="parentTagIds">Filter to descendants of the specified parent tags.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<HosLog> ListHosLogsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>List HOS violations (<c>GET /fleet/hos/violations</c>).</summary>
    /// <param name="startTime">Beginning of the time range (RFC 3339).</param>
    /// <param name="endTime">End of the time range (RFC 3339).</param>
    /// <param name="driverIds">Filter to the specified drivers.</param>
    /// <param name="tagIds">Filter to the specified tags (comma-delimited per spec).</param>
    /// <param name="parentTagIds">Filter to descendants of the specified parent tags
    /// (comma-delimited per spec).</param>
    /// <param name="types">Filter to the specified violation types.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<HosViolation> ListHosViolationsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        IReadOnlyList<string>? types = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get current HOS clocks for one or more drivers (<c>GET /fleet/hos/clocks</c>).</summary>
    /// <param name="driverIds">Drivers whose clocks should be returned.</param>
    /// <param name="tagIds">Filter by tag IDs.</param>
    /// <param name="parentTagIds">Filter by parent tag IDs.</param>
    /// <param name="after">Cursor for pagination.</param>
    /// <param name="limit">Page-size hint.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<IReadOnlyList<HosClocksForDriver>> GetHosClocksAsync(
        IReadOnlyList<string> driverIds,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>List HOS daily logs (<c>GET /fleet/hos/daily-logs</c>).</summary>
    /// <param name="startTime">Deprecated. Prefer <paramref name="startDate"/>. If provided
    /// and <paramref name="startDate"/> is null, the date portion of this value is sent
    /// as <c>startDate</c>.</param>
    /// <param name="endTime">Deprecated. Prefer <paramref name="endDate"/>. If provided
    /// and <paramref name="endDate"/> is null, the date portion of this value is sent
    /// as <c>endDate</c>.</param>
    /// <param name="driverIds">Filter to the specified drivers.</param>
    /// <param name="startDate">Start date (YYYY-MM-DD).</param>
    /// <param name="endDate">End date (YYYY-MM-DD).</param>
    /// <param name="tagIds">Filter to the specified tags (comma-delimited per spec).</param>
    /// <param name="parentTagIds">Filter to descendants of the specified parent tags
    /// (comma-delimited per spec).</param>
    /// <param name="driverActivationStatus">Filter by driver activation status
    /// (<c>active</c> or <c>deactivated</c>, default <c>active</c>).</param>
    /// <param name="expand">Optional expansion (<c>vehicle</c>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<HosDailyLog> ListHosDailyLogsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        string? startDate = null,
        string? endDate = null,
        string? tagIds = null,
        string? parentTagIds = null,
        string? driverActivationStatus = null,
        string? expand = null,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<HosEldEvent> ListHosEldEventsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default);

    /// <summary>HOS authentication logs (v1 legacy, <c>GET /v1/fleet/hos_authentication_logs</c>).</summary>
    /// <param name="driverId">Driver ID to query. Spec-required.</param>
    /// <param name="startTime">Beginning of the time range. Converted to milliseconds for
    /// the v1 query parameter <c>startMs</c>.</param>
    /// <param name="endTime">End of the time range. Converted to milliseconds for the v1
    /// query parameter <c>endMs</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<object> V1ListHosAuthenticationLogsAsync(
        long driverId,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>Set a driver's current duty status (v1 legacy).</summary>
    Task V1SetCurrentDutyStatusAsync(string driverId, object request, CancellationToken cancellationToken = default);

    /// <summary>Update shipping-doc metadata on HOS daily logs (beta).</summary>
    Task<object> UpdateShippingDocsAsync(string driverID, string hosDate, object request, CancellationToken cancellationToken = default);
}
