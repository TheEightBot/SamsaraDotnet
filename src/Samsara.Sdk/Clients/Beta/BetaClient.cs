namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;

/// <summary>
/// Beta — miscellaneous endpoints that don't fit cleanly into a domain client
/// (industrial jobs, devices, detections, AEMP, driver efficiency).
/// All return loosely-typed objects; subject to change.
/// </summary>
public interface IBetaClient
{
    // Industrial jobs
    Task<object> ListIndustrialJobsAsync(
        string? after = null,
        string? id = null,
        string? customerName = null,
        IReadOnlyList<string>? fleetDeviceIds = null,
        IReadOnlyList<string>? industrialAssetIds = null,
        string? status = null,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default);
    Task<object> CreateIndustrialJobAsync(object request, CancellationToken cancellationToken = default);
    Task<object> UpdateIndustrialJobAsync(string id, object request, CancellationToken cancellationToken = default);
    Task DeleteIndustrialJobAsync(string id, CancellationToken cancellationToken = default);

    // Other
    Task<object> ListDevicesAsync(
        string? after = null,
        int? limit = null,
        IReadOnlyList<string>? models = null,
        IReadOnlyList<string>? healthStatuses = null,
        bool? includeHealth = null,
        bool? includeTags = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> GetDetectionsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? detectionBehaviorLabels = null,
        IReadOnlyList<string>? inboxFilterReason = null,
        bool? inboxEvent = null,
        bool? inCabAlertPlayed = null,
        bool? includeAsset = null,
        bool? includeDriver = null,
        CancellationToken cancellationToken = default);
    Task<object> GetAempEquipmentListAsync(int pageNumber, CancellationToken cancellationToken = default);
    Task<object> GetDriverEfficiencyAsync(
        string? after = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? driverTagIds = null,
        IReadOnlyList<string>? driverParentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default);
}

internal sealed class BetaClient : SamsaraServiceClientBase, IBetaClient
{
    public BetaClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<object> ListIndustrialJobsAsync(
        string? after = null,
        string? id = null,
        string? customerName = null,
        IReadOnlyList<string>? fleetDeviceIds = null,
        IReadOnlyList<string>? industrialAssetIds = null,
        string? status = null,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams("beta/industrial/jobs",
                ("after", after),
                ("id", id),
                ("customerName", customerName),
                ("fleetDeviceIds", fleetDeviceIds is null ? null : string.Join(",", fleetDeviceIds)),
                ("industrialAssetIds", industrialAssetIds is null ? null : string.Join(",", industrialAssetIds)),
                ("status", status),
                ("startDate", startDate),
                ("endDate", endDate)),
            cancellationToken);

    public Task<object> CreateIndustrialJobAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("beta/industrial/jobs", request, cancellationToken);

    public Task<object> UpdateIndustrialJobAsync(string id, object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>(QueryBuilder.WithParams("beta/industrial/jobs", ("id", id)), request, cancellationToken);

    public Task DeleteIndustrialJobAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams("beta/industrial/jobs", ("id", id)), cancellationToken);

    public Task<object> ListDevicesAsync(
        string? after = null,
        int? limit = null,
        IReadOnlyList<string>? models = null,
        IReadOnlyList<string>? healthStatuses = null,
        bool? includeHealth = null,
        bool? includeTags = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams("devices",
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("models", models is null ? null : string.Join(",", models)),
                ("healthStatuses", healthStatuses is null ? null : string.Join(",", healthStatuses)),
                ("includeHealth", includeHealth?.ToString().ToLowerInvariant()),
                ("includeTags", includeTags?.ToString().ToLowerInvariant()),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds)),
            cancellationToken);

    public IAsyncEnumerable<object> GetDetectionsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? detectionBehaviorLabels = null,
        IReadOnlyList<string>? inboxFilterReason = null,
        bool? inboxEvent = null,
        bool? inCabAlertPlayed = null,
        bool? includeAsset = null,
        bool? includeDriver = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("detections/stream", startTime, endTime),
                ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("detectionBehaviorLabels", detectionBehaviorLabels is null ? null : string.Join(",", detectionBehaviorLabels)),
                ("inboxFilterReason", inboxFilterReason is null ? null : string.Join(",", inboxFilterReason)),
                ("inboxEvent", inboxEvent?.ToString().ToLowerInvariant()),
                ("inCabAlertPlayed", inCabAlertPlayed?.ToString().ToLowerInvariant()),
                ("includeAsset", includeAsset?.ToString().ToLowerInvariant()),
                ("includeDriver", includeDriver?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<object> GetAempEquipmentListAsync(int pageNumber, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"beta/aemp/Fleet/{pageNumber}", cancellationToken);

    public Task<object> GetDriverEfficiencyAsync(
        string? after = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? driverTagIds = null,
        IReadOnlyList<string>? driverParentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("beta/fleet/drivers/efficiency", startTime, endTime),
                ("after", after),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("driverTagIds", driverTagIds is null ? null : string.Join(",", driverTagIds)),
                ("driverParentTagIds", driverParentTagIds is null ? null : string.Join(",", driverParentTagIds)),
                ("driverActivationStatus", driverActivationStatus)),
            cancellationToken);
}
