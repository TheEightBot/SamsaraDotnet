namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class VehiclesClient : SamsaraServiceClientBase, IVehiclesClient
{
    private const string BasePath = "fleet/vehicles";

    public VehiclesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Vehicle> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Vehicle>(BasePath, cancellationToken: cancellationToken);

    public Task<Vehicle> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Vehicle>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Vehicle> UpdateAsync(string id, UpdateVehicleRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Vehicle>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<VehicleLocation> ListLocationsAsync(
        IReadOnlyList<string>? vehicleIds = null, IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null, string? time = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleLocation>(
            QueryBuilder.WithParams($"{BasePath}/locations",
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("time", time)),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleStats> ListStatsAsync(string types, IReadOnlyList<string>? vehicleIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, string? time = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleStats>(
            QueryBuilder.WithParams($"{BasePath}/stats",
                ("types", types),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("time", time)),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleLocation> GetLocationsFeedAsync(
        IReadOnlyList<string>? vehicleIds = null, IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleLocation>(
            QueryBuilder.WithParams($"{BasePath}/locations/feed",
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleLocation> GetLocationsHistoryAsync(
        DateTimeOffset? startTime = null, DateTimeOffset? endTime = null,
        IReadOnlyList<string>? vehicleIds = null, IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleLocation>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange($"{BasePath}/locations/history", startTime, endTime),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleStats> GetStatsFeedAsync(string types, IReadOnlyList<string>? vehicleIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, IReadOnlyList<string>? decorations = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleStats>(
            QueryBuilder.WithParams($"{BasePath}/stats/feed",
                ("types", types),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("decorations", decorations is null ? null : string.Join(",", decorations))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleStats> GetStatsHistoryAsync(string types, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? vehicleIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, IReadOnlyList<string>? decorations = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleStats>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange($"{BasePath}/stats/history", startTime, endTime),
                ("types", types),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("decorations", decorations is null ? null : string.Join(",", decorations))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<SpeedingInterval> GetSpeedingIntervalsStreamAsync(
        IReadOnlyList<string> assetIds,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? queryBy = null,
        IReadOnlyList<string>? severityLevels = null,
        bool? includeAsset = null,
        bool? includeDriverId = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<SpeedingInterval>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("speeding-intervals/stream", startTime, endTime),
                ("assetIds", string.Join(",", assetIds)),
                ("queryBy", queryBy),
                ("severityLevels", severityLevels is null ? null : string.Join(",", severityLevels)),
                ("includeAsset", includeAsset?.ToString().ToLowerInvariant()),
                ("includeDriverId", includeDriverId?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    /// <summary>
    /// Engine immobilizer states stream (beta, <c>GET /fleet/vehicles/immobilizer/stream</c>).
    /// <paramref name="vehicleIds"/> is required by the spec (comma-separated vehicle ids).
    /// </summary>
    public IAsyncEnumerable<object> GetImmobilizerStreamAsync(
        string vehicleIds,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/vehicles/immobilizer/stream", startTime, endTime),
                ("vehicleIds", vehicleIds)),
            cancellationToken: cancellationToken);

    /// <summary>Update an engine immobilizer state (beta, <c>PATCH /beta/fleet/vehicles/{id}/immobilizer</c>).</summary>
    public Task<object> UpdateImmobilizerStateAsync(string id, object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>($"beta/fleet/vehicles/{Uri.EscapeDataString(id)}/immobilizer", request, cancellationToken);
}
