namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>Client for Samsara CARB CTC compliance data.</summary>
public interface ICarbCtcClient
{
    IAsyncEnumerable<CarbCtcVehicle> ListVehiclesAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<CarbCtcVehicleHistory> ListVehicleHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
