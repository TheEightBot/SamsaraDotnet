namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class IftaClient : SamsaraServiceClientBase, IIftaClient
{
    public IftaClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>IFTA jurisdiction totals (<c>GET /fleet/reports/ifta/jurisdiction</c>). Year is required.</summary>
    public Task<IftaJurisdictionReportsResponse> ListJurisdictionReportsAsync(
        int year,
        string? month = null,
        string? quarter = null,
        IReadOnlyList<string>? jurisdictions = null,
        string? fuelType = null,
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IftaJurisdictionReportsResponse>(
            QueryBuilder.WithParams("fleet/reports/ifta/jurisdiction",
                ("year", year.ToString()),
                ("month", month),
                ("quarter", quarter),
                ("jurisdictions", jurisdictions is null ? null : string.Join(",", jurisdictions)),
                ("fuelType", fuelType),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken);

    /// <summary>IFTA per-vehicle totals (<c>GET /fleet/reports/ifta/vehicle</c>). Year is required.</summary>
    public Task<IftaVehicleReportsResponse> ListVehicleReportsAsync(
        int year,
        string? month = null,
        string? quarter = null,
        IReadOnlyList<string>? jurisdictions = null,
        string? fuelType = null,
        IReadOnlyList<string>? vehicleIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        string? after = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IftaVehicleReportsResponse>(
            QueryBuilder.WithParams("fleet/reports/ifta/vehicle",
                ("year", year.ToString()),
                ("month", month),
                ("quarter", quarter),
                ("jurisdictions", jurisdictions is null ? null : string.Join(",", jurisdictions)),
                ("fuelType", fuelType),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("after", after)),
            cancellationToken);

    /// <summary>Create an IFTA detail CSV export job (<c>POST /ifta-detail/csv</c>).</summary>
    public Task<IftaDetailJob> CreateDetailJobAsync(CreateIftaDetailJobRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<IftaDetailJob>("ifta-detail/csv", request, cancellationToken);

    /// <summary>Fetch a previously created CSV job (<c>GET /ifta-detail/csv/{id}</c>).</summary>
    public Task<IftaDetailJob> GetDetailJobAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IftaDetailJob>($"ifta-detail/csv/{Uri.EscapeDataString(id)}", cancellationToken);
}
