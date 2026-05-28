namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>Client for Samsara CARB CTC compliance data.</summary>
public interface ICarbCtcClient
{
    /// <summary>
    /// Lists CARB CTC enrolled vehicles.
    /// </summary>
    /// <param name="tagIds">Optional comma-separated list of tag IDs to filter by.</param>
    /// <param name="parentTagIds">Optional comma-separated list of parent tag IDs to filter by.</param>
    /// <param name="testStatus">Optional list of CARB CTC test statuses to filter by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<CarbCtcVehicle> ListVehiclesAsync(
        string? tagIds = null,
        string? parentTagIds = null,
        IReadOnlyList<string>? testStatus = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists CARB CTC vehicle collection history.
    /// </summary>
    /// <param name="vehicleIds">REQUIRED. Comma-separated list of Samsara vehicle IDs to query history for.</param>
    /// <param name="startTime">Optional inclusive lower bound of the time range, RFC 3339.</param>
    /// <param name="endTime">Optional inclusive upper bound of the time range, RFC 3339.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<CarbCtcVehicleHistory> ListVehicleHistoryAsync(
        string vehicleIds,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default);
}
