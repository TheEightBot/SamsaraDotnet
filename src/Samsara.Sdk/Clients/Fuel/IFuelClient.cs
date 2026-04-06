namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fuel;

/// <summary>
/// Client for Samsara fuel data.
/// </summary>
public interface IFuelClient
{
    IAsyncEnumerable<FuelPurchase> ListPurchasesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<FuelEnergyLevel> ListEnergyLevelsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
