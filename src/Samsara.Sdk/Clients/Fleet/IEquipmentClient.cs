namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for managing Samsara equipment (trailers, powered assets, unpowered assets).
/// </summary>
public interface IEquipmentClient
{
    /// <summary>List equipment (<c>GET /fleet/equipment</c>).</summary>
    IAsyncEnumerable<Equipment> ListAsync(
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        CancellationToken cancellationToken = default);

    Task<Equipment> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Equipment> UpdateAsync(string id, UpdateEquipmentRequest request, CancellationToken cancellationToken = default);

    /// <summary>List most recent equipment locations (<c>GET /fleet/equipment/locations</c>).</summary>
    IAsyncEnumerable<EquipmentLocation> ListLocationsAsync(
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Follow the equipment locations feed (<c>GET /fleet/equipment/locations/feed</c>).</summary>
    IAsyncEnumerable<EquipmentLocation> GetLocationsFeedAsync(
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get historical equipment locations (<c>GET /fleet/equipment/locations/history</c>).</summary>
    IAsyncEnumerable<EquipmentLocation> GetLocationsHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Follow the equipment stats feed (<c>GET /fleet/equipment/stats/feed</c>).</summary>
    IAsyncEnumerable<EquipmentStats> GetStatsFeedAsync(
        string? types = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get historical equipment stats (<c>GET /fleet/equipment/stats/history</c>).</summary>
    IAsyncEnumerable<EquipmentStats> GetStatsHistoryAsync(
        string? types = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Equipment stats snapshot (<c>GET /fleet/equipment/stats</c>).</summary>
    IAsyncEnumerable<EquipmentStats> GetStatsAsync(
        string? types = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? equipmentIds = null,
        CancellationToken cancellationToken = default);
}
