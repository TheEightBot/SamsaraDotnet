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
        string? after = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FuelEnergyVehicleReportsResponse>(
            QueryBuilder.WithParams("fleet/reports/vehicles/fuel-energy",
                ("startDate", startDate.ToString("yyyy-MM-dd")),
                ("endDate", endDate.ToString("yyyy-MM-dd")),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("energyType", energyType),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("after", after)),
            cancellationToken);

    /// <summary>Fuel/energy report per driver (<c>GET /fleet/reports/drivers/fuel-energy</c>).</summary>
    public Task<FuelEnergyDriverReportsResponse> ListDriverFuelEnergyReportsAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        string? after = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FuelEnergyDriverReportsResponse>(
            QueryBuilder.WithParams("fleet/reports/drivers/fuel-energy",
                ("startDate", startDate.ToString("yyyy-MM-dd")),
                ("endDate", endDate.ToString("yyyy-MM-dd")),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("after", after)),
            cancellationToken);

    /// <summary>Driver-efficiency scores per driver (<c>GET /driver-efficiency/drivers</c>).</summary>
    public IAsyncEnumerable<DriverEfficiencyByDriver> GetDriverEfficiencyByDriverAsync(
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? dataFormats = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DriverEfficiencyByDriver>(
            QueryBuilder.WithParams("driver-efficiency/drivers",
                ("startTime", startTime.ToString("O")),
                ("endTime", endTime.ToString("O")),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("dataFormats", dataFormats is null ? null : string.Join(",", dataFormats)),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds)),
            cancellationToken: cancellationToken);

    /// <summary>Driver-efficiency scores per vehicle (<c>GET /driver-efficiency/vehicles</c>).</summary>
    public IAsyncEnumerable<DriverEfficiencyByVehicle> GetDriverEfficiencyByVehicleAsync(
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        string? vehicleIds = null,
        IReadOnlyList<string>? dataFormats = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DriverEfficiencyByVehicle>(
            QueryBuilder.WithParams("driver-efficiency/vehicles",
                ("startTime", startTime.ToString("O")),
                ("endTime", endTime.ToString("O")),
                ("vehicleIds", vehicleIds),
                ("dataFormats", dataFormats is null ? null : string.Join(",", dataFormats)),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds)),
            cancellationToken: cancellationToken);

    /// <summary>Record a fuel purchase (<c>POST /fuel-purchase</c>).</summary>
    public Task<FuelPurchase> CreateFuelPurchaseAsync(CreateFuelPurchaseRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FuelPurchase>("fuel-purchase", request, cancellationToken);
}
