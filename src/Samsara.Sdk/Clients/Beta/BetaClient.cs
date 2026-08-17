namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>
/// Beta — miscellaneous endpoints that don't fit cleanly into a domain client
/// (industrial jobs, devices, detections, AEMP, driver efficiency).
/// Subject to change.
/// </summary>
public interface IBetaClient
{
    // Industrial jobs

    /// <summary>
    /// List a page of industrial jobs (<c>GET /beta/industrial/jobs</c>). Pass the previous
    /// page's end cursor as <paramref name="after"/> to page forward.
    /// </summary>
    Task<IReadOnlyList<IndustrialJob>> ListIndustrialJobsAsync(
        string? after = null,
        string? id = null,
        string? customerName = null,
        IReadOnlyList<string>? fleetDeviceIds = null,
        IReadOnlyList<string>? industrialAssetIds = null,
        string? status = null,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create an industrial job (<c>POST /beta/industrial/jobs</c>).</summary>
    Task<IndustrialJob> CreateIndustrialJobAsync(CreateIndustrialJobRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update an industrial job (<c>PATCH /beta/industrial/jobs</c>).</summary>
    Task<IndustrialJob> UpdateIndustrialJobAsync(string id, UpdateIndustrialJobRequest request, CancellationToken cancellationToken = default);

    /// <summary>Delete an industrial job (<c>DELETE /beta/industrial/jobs</c>).</summary>
    Task DeleteIndustrialJobAsync(string id, CancellationToken cancellationToken = default);

    // Other

    /// <summary>
    /// List a page of devices (<c>GET /devices</c>). Pass the previous page's end cursor as
    /// <paramref name="after"/> to page forward.
    /// </summary>
    Task<IReadOnlyList<BetaDevice>> ListDevicesAsync(
        string? after = null,
        int? limit = null,
        IReadOnlyList<string>? models = null,
        IReadOnlyList<string>? healthStatuses = null,
        bool? includeHealth = null,
        bool? includeTags = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Stream safety detections (<c>GET /detections/stream</c>).</summary>
    IAsyncEnumerable<Detection> GetDetectionsStreamAsync(
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

    /// <summary>
    /// Get one page of the AEMP (ISO/TS 15143-3) equipment feed
    /// (<c>GET /beta/aemp/Fleet/{pageNumber}</c>). This endpoint has no <c>data</c> envelope.
    /// </summary>
    Task<AempEquipmentList> GetAempEquipmentListAsync(int pageNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get driver efficiency summaries (<c>GET /beta/fleet/drivers/efficiency</c>). Pass the
    /// previous page's end cursor as <paramref name="after"/> to page forward.
    /// </summary>
    Task<BetaDriverEfficiencySummary> GetDriverEfficiencyAsync(
        string? after = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? driverTagIds = null,
        IReadOnlyList<string>? driverParentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default);

    // Agent Studio — voice agent sessions (beta)

    /// <summary>
    /// Get voice agent session details (<c>GET /agent-studio/voice-sessions</c>) — beta.
    /// <paramref name="ids"/> is spec-required.
    /// </summary>
    Task<IReadOnlyList<VoiceSession>> GetVoiceSessionsAsync(
        IReadOnlyList<string> ids,
        string? after = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stream voice agent session summaries (<c>GET /agent-studio/voice-sessions/stream</c>)
    /// — beta. <paramref name="agentIds"/> is spec-required.
    /// </summary>
    IAsyncEnumerable<VoiceSessionSummary> GetVoiceSessionsStreamAsync(
        IReadOnlyList<string> agentIds,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default);
}

internal sealed class BetaClient : SamsaraServiceClientBase, IBetaClient
{
    public BetaClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<IReadOnlyList<IndustrialJob>> ListIndustrialJobsAsync(
        string? after = null,
        string? id = null,
        string? customerName = null,
        IReadOnlyList<string>? fleetDeviceIds = null,
        IReadOnlyList<string>? industrialAssetIds = null,
        string? status = null,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<IndustrialJob>>(
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

    public Task<IndustrialJob> CreateIndustrialJobAsync(CreateIndustrialJobRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<IndustrialJob>("beta/industrial/jobs", request, cancellationToken);

    public Task<IndustrialJob> UpdateIndustrialJobAsync(string id, UpdateIndustrialJobRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<IndustrialJob>(QueryBuilder.WithParams("beta/industrial/jobs", ("id", id)), request, cancellationToken);

    public Task DeleteIndustrialJobAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams("beta/industrial/jobs", ("id", id)), cancellationToken);

    public Task<IReadOnlyList<BetaDevice>> ListDevicesAsync(
        string? after = null,
        int? limit = null,
        IReadOnlyList<string>? models = null,
        IReadOnlyList<string>? healthStatuses = null,
        bool? includeHealth = null,
        bool? includeTags = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<BetaDevice>>(
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

    public IAsyncEnumerable<Detection> GetDetectionsStreamAsync(
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
        => PaginateAsync<Detection>(
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

    public Task<AempEquipmentList> GetAempEquipmentListAsync(int pageNumber, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<AempEquipmentList>($"beta/aemp/Fleet/{pageNumber}", cancellationToken);

    public Task<BetaDriverEfficiencySummary> GetDriverEfficiencyAsync(
        string? after = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? driverTagIds = null,
        IReadOnlyList<string>? driverParentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<BetaDriverEfficiencySummary>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("beta/fleet/drivers/efficiency", startTime, endTime),
                ("after", after),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("driverTagIds", driverTagIds is null ? null : string.Join(",", driverTagIds)),
                ("driverParentTagIds", driverParentTagIds is null ? null : string.Join(",", driverParentTagIds)),
                ("driverActivationStatus", driverActivationStatus)),
            cancellationToken);

    public Task<IReadOnlyList<VoiceSession>> GetVoiceSessionsAsync(
        IReadOnlyList<string> ids,
        string? after = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<VoiceSession>>(
            QueryBuilder.WithParams("agent-studio/voice-sessions",
                ("ids", string.Join(",", ids)),
                ("after", after)),
            cancellationToken);

    public IAsyncEnumerable<VoiceSessionSummary> GetVoiceSessionsStreamAsync(
        IReadOnlyList<string> agentIds,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<VoiceSessionSummary>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("agent-studio/voice-sessions/stream", startTime, endTime),
                ("agentIds", string.Join(",", agentIds))),
            cancellationToken: cancellationToken);
}
