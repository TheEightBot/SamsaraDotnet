namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class CarbCtcClient : SamsaraServiceClientBase, ICarbCtcClient
{
    public CarbCtcClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<CarbCtcVehicle> ListVehiclesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<CarbCtcVehicle>("fleet/carb-ctc/vehicles", cancellationToken: cancellationToken);

    public IAsyncEnumerable<CarbCtcVehicleHistory> ListVehicleHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<CarbCtcVehicleHistory>(QueryBuilder.WithTimeRange("fleet/carb-ctc/vehicles/history", startTime, endTime), cancellationToken: cancellationToken);
}
