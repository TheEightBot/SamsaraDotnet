namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>
/// Read-only legacy fleet endpoints. These predate the v2 surface and are kept available
/// for migration. Most return loosely-typed objects — model them as you need them.
/// </summary>
public interface ILegacyApisClient
{
    /// <summary>DVIR defects history (<c>GET /fleet/defects/history</c>).</summary>
    IAsyncEnumerable<object> GetDvirDefectsHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);

    /// <summary>Driver→vehicle assignments (<c>GET /fleet/drivers/vehicle-assignments</c>).</summary>
    IAsyncEnumerable<object> GetDriversVehicleAssignmentsAsync(CancellationToken cancellationToken = default);

    /// <summary>DVIR history (<c>GET /fleet/dvirs/history</c>).</summary>
    IAsyncEnumerable<object> GetDvirHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);

    /// <summary>Vehicle idling report (<c>GET /fleet/reports/vehicle/idling</c>).</summary>
    Task<object> GetVehicleIdlingReportAsync(CancellationToken cancellationToken = default);

    /// <summary>Safety events (legacy v1, <c>GET /fleet/safety-events</c>).</summary>
    IAsyncEnumerable<object> GetSafetyEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);

    /// <summary>Safety-events audit log feed (<c>GET /fleet/safety-events/audit-logs/feed</c>).</summary>
    IAsyncEnumerable<object> GetSafetyEventsAuditFeedAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);

    /// <summary>Vehicle→driver assignments (<c>GET /fleet/vehicles/driver-assignments</c>).</summary>
    IAsyncEnumerable<object> GetVehiclesDriverAssignmentsAsync(CancellationToken cancellationToken = default);

    /// <summary>Vehicle harsh event details (v1, <c>GET /v1/fleet/vehicles/{vehicleId}/safety/harsh_event</c>).</summary>
    Task<object> V1GetVehicleHarshEventAsync(string vehicleId, long timestampMs, CancellationToken cancellationToken = default);
}

internal sealed class LegacyApisClient : SamsaraServiceClientBase, ILegacyApisClient
{
    public LegacyApisClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> GetDvirDefectsHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("fleet/defects/history", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetDriversVehicleAssignmentsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("fleet/drivers/vehicle-assignments", cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetDvirHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("fleet/dvirs/history", startTime, endTime), cancellationToken: cancellationToken);

    public Task<object> GetVehicleIdlingReportAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("fleet/reports/vehicle/idling", cancellationToken);

    public IAsyncEnumerable<object> GetSafetyEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("fleet/safety-events", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetSafetyEventsAuditFeedAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("fleet/safety-events/audit-logs/feed", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetVehiclesDriverAssignmentsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("fleet/vehicles/driver-assignments", cancellationToken: cancellationToken);

    public Task<object> V1GetVehicleHarshEventAsync(string vehicleId, long timestampMs, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams($"v1/fleet/vehicles/{Uri.EscapeDataString(vehicleId)}/safety/harsh_event",
                ("timestamp", timestampMs.ToString())),
            cancellationToken);
}
