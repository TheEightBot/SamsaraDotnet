namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Safety;

internal sealed class SafetyClient : SamsaraServiceClientBase, ISafetyClient
{
    public SafetyClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<SafetyEvent> ListEventsAsync(
        IReadOnlyList<string> safetyEventIds,
        bool? includeAsset = null,
        bool? includeDriver = null,
        bool? includeVgOnlyEvents = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<SafetyEvent>(
            QueryBuilder.WithParams(
                "safety-events",
                ("safetyEventIds", string.Join(",", safetyEventIds)),
                ("includeAsset", includeAsset?.ToString().ToLowerInvariant()),
                ("includeDriver", includeDriver?.ToString().ToLowerInvariant()),
                ("includeVgOnlyEvents", includeVgOnlyEvents?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleSafetyScore> ListVehicleSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? vehicleIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleSafetyScore>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("safety-scores/vehicles", startTime, endTime),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<DriverSafetyScore> ListDriverSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? driverIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DriverSafetyScore>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("safety-scores/drivers", startTime, endTime),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<TagSafetyScore> ListTagSafetyScoresAsync(string scoreType, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? tagIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TagSafetyScore>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("safety-scores/tags", startTime, endTime),
                ("scoreType", scoreType),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<TagGroupSafetyScore> ListTagGroupSafetyScoresAsync(string scoreType, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? tagIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TagGroupSafetyScore>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("safety-scores/tag-group", startTime, endTime),
                ("scoreType", scoreType),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<SafetyEvent> GetEventsStreamAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        string? queryByTimeField = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? assignedCoaches = null,
        IReadOnlyList<string>? behaviorLabels = null,
        IReadOnlyList<string>? eventStates = null,
        bool? includeAsset = null,
        bool? includeDriver = null,
        bool? includeVgOnlyEvents = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<SafetyEvent>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("safety-events/stream", startTime, endTime),
                ("queryByTimeField", queryByTimeField),
                ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("assignedCoaches", assignedCoaches is null ? null : string.Join(",", assignedCoaches)),
                ("behaviorLabels", behaviorLabels is null ? null : string.Join(",", behaviorLabels)),
                ("eventStates", eventStates is null ? null : string.Join(",", eventStates)),
                ("includeAsset", includeAsset?.ToString().ToLowerInvariant()),
                ("includeDriver", includeDriver?.ToString().ToLowerInvariant()),
                ("includeVgOnlyEvents", includeVgOnlyEvents?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    /// <summary>Driver safety score (v1 legacy, <c>GET /v1/fleet/drivers/{driverId}/safety/score</c>).</summary>
    public Task<V1DriverSafetyScore> V1GetDriverSafetyScoreAsync(string driverId, long startMs, long endMs, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<V1DriverSafetyScore>(
            QueryBuilder.WithParams(
                $"v1/fleet/drivers/{Uri.EscapeDataString(driverId)}/safety/score",
                ("startMs", startMs.ToString()),
                ("endMs", endMs.ToString())),
            cancellationToken);

    /// <summary>Vehicle safety score (v1 legacy, <c>GET /v1/fleet/vehicles/{vehicleId}/safety/score</c>).</summary>
    public Task<V1VehicleSafetyScore> V1GetVehicleSafetyScoreAsync(string vehicleId, long startMs, long endMs, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<V1VehicleSafetyScore>(
            QueryBuilder.WithParams(
                $"v1/fleet/vehicles/{Uri.EscapeDataString(vehicleId)}/safety/score",
                ("startMs", startMs.ToString()),
                ("endMs", endMs.ToString())),
            cancellationToken);

    /// <summary>
    /// Batch update safety events (beta, <c>PATCH /safety-events/batch</c>).
    /// </summary>
    /// <remarks>
    /// The 202 payload (<c>SafetyEventsV2PatchSafetyEventsV2BatchResponseBody</c>) is
    /// <c>{ requestId, responses }</c> at the TOP level — there is no <c>{ data: ... }</c>
    /// envelope — so this uses the non-unwrapping <c>PatchAsync&lt;T&gt;</c> helper.
    /// </remarks>
    public Task<SafetyEventsBatchResult> PatchEventsBatchAsync(PatchSafetyEventsBatchRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchAsync<SafetyEventsBatchResult>("safety-events/batch", request, cancellationToken);
}
