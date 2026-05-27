namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;

/// <summary>
/// Read-only legacy fleet endpoints. These predate the v2 surface and are kept available
/// for migration. Most return loosely-typed objects — model them as you need them.
/// </summary>
public interface ILegacyApisClient
{
    /// <summary>
    /// DVIR defects history (<c>GET /fleet/defects/history</c>). The spec requires
    /// <paramref name="startTime"/> and <paramref name="endTime"/>; they are exposed as
    /// nullable for backward compatibility with existing callers.
    /// </summary>
    IAsyncEnumerable<object> GetDvirDefectsHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? isResolved = null,
        CancellationToken cancellationToken = default);

    /// <summary>Driver→vehicle assignments (<c>GET /fleet/drivers/vehicle-assignments</c>).</summary>
    IAsyncEnumerable<object> GetDriversVehicleAssignmentsAsync(
        IReadOnlyList<string>? driverIds = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? tagIds = null,
        string? parentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// DVIR history (<c>GET /fleet/dvirs/history</c>). The spec requires
    /// <paramref name="startTime"/> and <paramref name="endTime"/>; they are exposed as
    /// nullable for backward compatibility with existing callers.
    /// </summary>
    IAsyncEnumerable<object> GetDvirHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Vehicle idling report (<c>GET /fleet/reports/vehicle/idling</c>). The spec requires
    /// <paramref name="startTime"/> and <paramref name="endTime"/>.
    /// </summary>
    IAsyncEnumerable<object> GetVehicleIdlingReportAsync(
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        string? vehicleIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        bool? isPtoActive = null,
        int? minIdlingDurationMinutes = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Safety events (legacy v1, <c>GET /fleet/safety-events</c>). The spec requires
    /// <paramref name="startTime"/> and <paramref name="endTime"/>; they are exposed as
    /// nullable for backward compatibility with existing callers.
    /// </summary>
    IAsyncEnumerable<object> GetSafetyEventsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? vehicleIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Safety-events audit log feed (<c>GET /fleet/safety-events/audit-logs/feed</c>).</summary>
    IAsyncEnumerable<object> GetSafetyEventsAuditFeedAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);

    /// <summary>Vehicle→driver assignments (<c>GET /fleet/vehicles/driver-assignments</c>).</summary>
    IAsyncEnumerable<object> GetVehiclesDriverAssignmentsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? vehicleIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Vehicle harsh event details (v1, <c>GET /v1/fleet/vehicles/{vehicleId}/safety/harsh_event</c>).
    /// <paramref name="timestamp"/> is the millisecond timestamp of the harsh event.
    /// </summary>
    Task<object> V1GetVehicleHarshEventAsync(string vehicleId, long timestamp, CancellationToken cancellationToken = default);
}

internal sealed class LegacyApisClient : SamsaraServiceClientBase, ILegacyApisClient
{
    public LegacyApisClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> GetDvirDefectsHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? isResolved = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/defects/history", startTime, endTime),
                ("isResolved", isResolved?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetDriversVehicleAssignmentsAsync(
        IReadOnlyList<string>? driverIds = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? tagIds = null,
        string? parentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/drivers/vehicle-assignments", startTime, endTime),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds),
                ("driverActivationStatus", driverActivationStatus)),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetDvirHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/dvirs/history", startTime, endTime),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetVehicleIdlingReportAsync(
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        string? vehicleIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        bool? isPtoActive = null,
        int? minIdlingDurationMinutes = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/reports/vehicle/idling", startTime, endTime),
                ("vehicleIds", vehicleIds),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds),
                ("isPtoActive", isPtoActive?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()),
                ("minIdlingDurationMinutes", minIdlingDurationMinutes?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetSafetyEventsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? vehicleIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/safety-events", startTime, endTime),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetSafetyEventsAuditFeedAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("fleet/safety-events/audit-logs/feed", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetVehiclesDriverAssignmentsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? vehicleIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/vehicles/driver-assignments", startTime, endTime),
                ("vehicleIds", vehicleIds),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds)),
            cancellationToken: cancellationToken);

    public Task<object> V1GetVehicleHarshEventAsync(string vehicleId, long timestamp, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams($"v1/fleet/vehicles/{Uri.EscapeDataString(vehicleId)}/safety/harsh_event",
                ("timestamp", timestamp.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);
}
