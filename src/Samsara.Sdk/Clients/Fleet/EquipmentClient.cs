namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class EquipmentClient : SamsaraServiceClientBase, IEquipmentClient
{
    private const string BasePath = "fleet/equipment";

    public EquipmentClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Equipment> ListAsync(
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Equipment>(
            QueryBuilder.WithParams(BasePath,
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds))),
            cancellationToken: cancellationToken);

    public Task<Equipment> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Equipment>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Equipment> UpdateAsync(string id, UpdateEquipmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Equipment>($"beta/fleet/equipment/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<EquipmentLocation> ListLocationsAsync(
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<EquipmentLocation>(
            QueryBuilder.WithParams($"{BasePath}/locations",
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("equipmentIds", equipmentIds is null ? null : string.Join(",", equipmentIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<EquipmentLocation> GetLocationsFeedAsync(
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<EquipmentLocation>(
            QueryBuilder.WithParams($"{BasePath}/locations/feed",
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("equipmentIds", equipmentIds is null ? null : string.Join(",", equipmentIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<EquipmentLocation> GetLocationsHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<EquipmentLocation>(
            QueryBuilder.WithParams(QueryBuilder.WithTimeRange($"{BasePath}/locations/history", startTime, endTime),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("equipmentIds", equipmentIds is null ? null : string.Join(",", equipmentIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<EquipmentStats> GetStatsFeedAsync(
        string? types = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<EquipmentStats>(
            QueryBuilder.WithParams($"{BasePath}/stats/feed",
                ("types", types),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("equipmentIds", equipmentIds is null ? null : string.Join(",", equipmentIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<EquipmentStats> GetStatsHistoryAsync(
        string? types = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<EquipmentStats>(
            QueryBuilder.WithParams(QueryBuilder.WithTimeRange($"{BasePath}/stats/history", startTime, endTime),
                ("types", types),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("equipmentIds", equipmentIds is null ? null : string.Join(",", equipmentIds))),
            cancellationToken: cancellationToken);

    /// <summary>Equipment stats snapshot (<c>GET /fleet/equipment/stats</c>).</summary>
    public IAsyncEnumerable<EquipmentStats> GetStatsAsync(
        string? types = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<EquipmentStats>(
            QueryBuilder.WithParams($"{BasePath}/stats",
                ("types", types),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("equipmentIds", equipmentIds is null ? null : string.Join(",", equipmentIds))),
            cancellationToken: cancellationToken);
}
