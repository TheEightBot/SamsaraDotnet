namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class TachographClient : SamsaraServiceClientBase, ITachographClient
{
    public TachographClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<TachographActivity> ListActivitiesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? driverIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TachographActivity>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/drivers/tachograph-activity/history", startTime, endTime),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<TachographFile> ListFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? driverIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TachographFile>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/drivers/tachograph-files/history", startTime, endTime),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken: cancellationToken);

    /// <summary>Vehicle tachograph files history (<c>GET /fleet/vehicles/tachograph-files/history</c>).</summary>
    public IAsyncEnumerable<TachographFile> ListVehicleFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? vehicleIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TachographFile>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/vehicles/tachograph-files/history", startTime, endTime),
                ("vehicleIds", vehicleIds is null ? null : string.Join(",", vehicleIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken: cancellationToken);

    /// <summary>Latest tachograph live-data (beta, <c>GET /fleet/tachograph-live-data/latest</c>).</summary>
    public IAsyncEnumerable<TachographLiveData> ListLiveDataAsync(
        string? driverIds = null,
        string? vehicleIds = null,
        DateTimeOffset? startTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<TachographLiveData>(
            QueryBuilder.WithParams("fleet/tachograph-live-data/latest",
                ("driverIds", driverIds),
                ("vehicleIds", vehicleIds),
                ("startTime", startTime?.ToString("O"))),
            cancellationToken: cancellationToken);

    /// <summary>Create a tachograph file upload (<c>POST /fleet/tachograph/file-uploads</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<TachographFileUpload> CreateFileUploadAsync(
        CreateTachographFileUploadRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<TachographFileUpload>(
            "fleet/tachograph/file-uploads", request, cancellationToken);
}
