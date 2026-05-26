namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>
/// Beta — miscellaneous endpoints that don't fit cleanly into a domain client
/// (industrial jobs, devices, detections, AEMP, driver efficiency).
/// All return loosely-typed objects; subject to change.
/// </summary>
public interface IBetaClient
{
    // Industrial jobs
    Task<object> ListIndustrialJobsAsync(CancellationToken cancellationToken = default);
    Task<object> CreateIndustrialJobAsync(object request, CancellationToken cancellationToken = default);
    Task<object> UpdateIndustrialJobAsync(object request, CancellationToken cancellationToken = default);
    Task DeleteIndustrialJobAsync(string id, CancellationToken cancellationToken = default);

    // Other
    Task<object> ListDevicesAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> GetDetectionsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<object> GetAempEquipmentListAsync(int pageNumber, CancellationToken cancellationToken = default);
    Task<object> GetDriverEfficiencyAsync(CancellationToken cancellationToken = default);
}

internal sealed class BetaClient : SamsaraServiceClientBase, IBetaClient
{
    public BetaClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<object> ListIndustrialJobsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("beta/industrial/jobs", cancellationToken);

    public Task<object> CreateIndustrialJobAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("beta/industrial/jobs", request, cancellationToken);

    public Task<object> UpdateIndustrialJobAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>("beta/industrial/jobs", request, cancellationToken);

    public Task DeleteIndustrialJobAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams("beta/industrial/jobs", ("id", id)), cancellationToken);

    public Task<object> ListDevicesAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("devices", cancellationToken);

    public IAsyncEnumerable<object> GetDetectionsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("detections/stream", startTime, endTime), cancellationToken: cancellationToken);

    public Task<object> GetAempEquipmentListAsync(int pageNumber, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"beta/aemp/Fleet/{pageNumber}", cancellationToken);

    public Task<object> GetDriverEfficiencyAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("beta/fleet/drivers/efficiency", cancellationToken);
}
