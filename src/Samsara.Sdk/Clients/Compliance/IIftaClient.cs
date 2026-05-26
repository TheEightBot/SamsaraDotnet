namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>
/// Client for Samsara IFTA reports and detail CSV jobs.
/// </summary>
public interface IIftaClient
{
    /// <summary>IFTA jurisdiction totals (<c>GET /fleet/reports/ifta/jurisdiction</c>). Year is required.</summary>
    Task<IftaJurisdictionReportsResponse> ListJurisdictionReportsAsync(
        int year,
        string? month = null,
        string? quarter = null,
        IReadOnlyList<string>? jurisdictions = null,
        string? fuelType = null,
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>IFTA per-vehicle totals (<c>GET /fleet/reports/ifta/vehicle</c>). Year is required.</summary>
    Task<IftaVehicleReportsResponse> ListVehicleReportsAsync(
        int year,
        string? month = null,
        string? quarter = null,
        IReadOnlyList<string>? jurisdictions = null,
        string? fuelType = null,
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create an IFTA detail CSV export job (<c>POST /ifta-detail/csv</c>).</summary>
    Task<IftaDetailJob> CreateDetailJobAsync(CreateIftaDetailJobRequest request, CancellationToken cancellationToken = default);

    /// <summary>Fetch a previously created CSV job (<c>GET /ifta-detail/csv/{id}</c>).</summary>
    Task<IftaDetailJob> GetDetailJobAsync(string id, CancellationToken cancellationToken = default);
}
