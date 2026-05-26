namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fuel;

internal sealed class FuelClient : SamsaraServiceClientBase, IFuelClient
{
    public FuelClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>Fuel/energy report per vehicle (<c>GET /fleet/reports/vehicles/fuel-energy</c>).</summary>
    public Task<FuelEnergyVehicleReportsResponse> ListVehicleFuelEnergyReportsAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        IReadOnlyList<string>? vehicleIds = null,
        string? energyType = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FuelEnergyVehicleReportsResponse>(
            QueryBuilder.WithParams("fleet/reports/vehicles/fuel-energy",
                ("startDate", startDate.ToString("yyyy-MM-dd")),
                ("endDate", endDate.ToString("yyyy-MM-dd")),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("energyType", energyType),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken);

    /// <summary>Fuel/energy report per driver (<c>GET /fleet/reports/drivers/fuel-energy</c>).</summary>
    public Task<FuelEnergyDriverReportsResponse> ListDriverFuelEnergyReportsAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FuelEnergyDriverReportsResponse>(
            QueryBuilder.WithParams("fleet/reports/drivers/fuel-energy",
                ("startDate", startDate.ToString("yyyy-MM-dd")),
                ("endDate", endDate.ToString("yyyy-MM-dd")),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken);

    /// <summary>Driver-efficiency scores (<c>GET /driver-efficiency/drivers</c>).</summary>
    public IAsyncEnumerable<DriverEfficiencyByDriver> GetDriverEfficiencyByDriverAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DriverEfficiencyByDriver>("driver-efficiency/drivers", cancellationToken: cancellationToken);

    /// <summary>Driver-efficiency scores per vehicle (<c>GET /driver-efficiency/vehicles</c>).</summary>
    public IAsyncEnumerable<DriverEfficiencyByVehicle> GetDriverEfficiencyByVehicleAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DriverEfficiencyByVehicle>("driver-efficiency/vehicles", cancellationToken: cancellationToken);

    /// <summary>Record a fuel purchase (<c>POST /fuel-purchase</c>).</summary>
    public Task<FuelPurchase> CreateFuelPurchaseAsync(CreateFuelPurchaseRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FuelPurchase>("fuel-purchase", request, cancellationToken);
}
