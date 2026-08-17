namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for managing Samsara vehicles.
/// </summary>
public interface IVehiclesClient
{
    IAsyncEnumerable<Vehicle> ListAsync(
        IReadOnlyList<string>? attributes = null,
        string? attributeValueIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        string? createdAfterTime = null,
        string? updatedAfterTime = null,
        CancellationToken cancellationToken = default);
    Task<Vehicle> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Vehicle> UpdateAsync(string id, UpdateVehicleRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleLocation> ListLocationsAsync(
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        string? time = null,
        CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleStats> ListStatsAsync(
        string types,
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        string? time = null,
        CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleLocation> GetLocationsFeedAsync(
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleLocation> GetLocationsHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleStatsSample> GetStatsFeedAsync(
        string types,
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? decorations = null,
        CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleStatsSample> GetStatsHistoryAsync(
        string types,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? decorations = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Stream speeding intervals over a time window (<c>GET /speeding-intervals/stream</c>).
    /// <paramref name="assetIds"/> is spec-required.
    /// </summary>
    IAsyncEnumerable<SpeedingInterval> GetSpeedingIntervalsStreamAsync(
        IReadOnlyList<string> assetIds,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? queryBy = null,
        IReadOnlyList<string>? severityLevels = null,
        bool? includeAsset = null,
        bool? includeDriverId = null,
        CancellationToken cancellationToken = default);
    /// <summary>Engine immobilizer states stream (beta).</summary>
    IAsyncEnumerable<EngineImmobilizerState> GetImmobilizerStreamAsync(
        string vehicleIds,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an engine immobilizer state (beta). The spec declares a
    /// <c>202 Accepted</c> with no response content, so nothing is returned.
    /// </summary>
    Task UpdateImmobilizerStateAsync(string id, UpdateEngineImmobilizerStateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Current locations of vehicles (legacy v1, <c>GET /v1/fleet/locations</c>,
    /// <c>getFleetLocations</c>). Pagination is handled transparently.
    /// </summary>
    /// <param name="vehicleIds">Optional vehicle IDs to filter on (sent comma-separated).</param>
    /// <param name="tagIds">Optional tag IDs to filter on (sent comma-separated).</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    /// <remarks>
    /// Returns the v1 <see cref="V1VehicleLocation"/> shape — flat latitude/longitude with
    /// integer ids and a Unix-millisecond timestamp — not the v2
    /// <see cref="VehicleLocation"/> returned by <see cref="ListLocationsAsync"/>.
    /// </remarks>
    IAsyncEnumerable<V1VehicleLocation> V1GetFleetLocationsAsync(
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        CancellationToken cancellationToken = default);
}
