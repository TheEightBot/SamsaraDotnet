namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fuel;

internal sealed class FuelClient : SamsaraServiceClientBase, IFuelClient
{
    public FuelClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<FuelPurchase> ListPurchasesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<FuelPurchase>(QueryBuilder.WithTimeRange("fleet/vehicles/fuel/purchases", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<FuelEnergyLevel> ListEnergyLevelsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<FuelEnergyLevel>(QueryBuilder.WithTimeRange("fleet/vehicles/fuel/energy-levels", startTime, endTime), cancellationToken: cancellationToken);
}
