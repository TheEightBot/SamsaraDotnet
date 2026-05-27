namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class CarbCtcClient : SamsaraServiceClientBase, ICarbCtcClient
{
    public CarbCtcClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<CarbCtcVehicle> ListVehiclesAsync(
        string? tagIds = null,
        string? parentTagIds = null,
        IReadOnlyList<string>? testStatus = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<CarbCtcVehicle>(
            QueryBuilder.WithParams("fleet/carb-ctc/vehicles",
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds),
                ("testStatus", testStatus is null ? null : string.Join(",", testStatus))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<CarbCtcVehicleHistory> ListVehicleHistoryAsync(
        string vehicleIds,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<CarbCtcVehicleHistory>(
            QueryBuilder.WithTimeRange(
                QueryBuilder.WithParams("fleet/carb-ctc/vehicles/history",
                    ("vehicleIds", vehicleIds)),
                startTime,
                endTime),
            cancellationToken: cancellationToken);
}
