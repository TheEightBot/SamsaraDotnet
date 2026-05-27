namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fuel;

/// <summary>
/// Client for Samsara fuel &amp; energy reports, driver-efficiency scores, and fuel purchases.
/// </summary>
public interface IFuelClient
{
    /// <summary>Fuel/energy report per vehicle (<c>GET /fleet/reports/vehicles/fuel-energy</c>).</summary>
    Task<FuelEnergyVehicleReportsResponse> ListVehicleFuelEnergyReportsAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        IReadOnlyList<string>? vehicleIds = null,
        string? energyType = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        string? after = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fuel/energy report per driver (<c>GET /fleet/reports/drivers/fuel-energy</c>).</summary>
    Task<FuelEnergyDriverReportsResponse> ListDriverFuelEnergyReportsAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        string? after = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Driver-efficiency scores per driver (<c>GET /driver-efficiency/drivers</c>).
    /// <paramref name="startTime"/> and <paramref name="endTime"/> are REQUIRED per spec
    /// (RFC 3339, multiple-of-hours, at least one day apart).
    /// </summary>
    IAsyncEnumerable<DriverEfficiencyByDriver> GetDriverEfficiencyByDriverAsync(
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? dataFormats = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Driver-efficiency scores per vehicle (<c>GET /driver-efficiency/vehicles</c>).
    /// <paramref name="startTime"/> and <paramref name="endTime"/> are REQUIRED per spec
    /// (RFC 3339, multiple-of-hours, at least one day apart).
    /// </summary>
    IAsyncEnumerable<DriverEfficiencyByVehicle> GetDriverEfficiencyByVehicleAsync(
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        string? vehicleIds = null,
        IReadOnlyList<string>? dataFormats = null,
        string? tagIds = null,
        string? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Record a fuel purchase (<c>POST /fuel-purchase</c>).</summary>
    Task<FuelPurchase> CreateFuelPurchaseAsync(CreateFuelPurchaseRequest request, CancellationToken cancellationToken = default);
}
