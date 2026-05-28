namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class IdlingClient : SamsaraServiceClientBase, IIdlingClient
{
    private const string BasePath = "idling/events";

    public IdlingClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<IdlingEvent> ListEventsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? operatorIds = null,
        string? ptoState = null,
        int? minAirTemperatureMillicelsius = null,
        int? maxAirTemperatureMillicelsius = null,
        bool? excludeEventsWithUnknownAirTemperature = null,
        int? minDurationMilliseconds = null,
        int? maxDurationMilliseconds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            BasePath,
            ("startTime", startTime?.ToString("O")),
            ("endTime", endTime?.ToString("O")),
            ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
            ("operatorIds", operatorIds is null ? null : string.Join(",", operatorIds)),
            ("ptoState", ptoState),
            ("minAirTemperatureMillicelsius", minAirTemperatureMillicelsius?.ToString(CultureInfo.InvariantCulture)),
            ("maxAirTemperatureMillicelsius", maxAirTemperatureMillicelsius?.ToString(CultureInfo.InvariantCulture)),
            ("excludeEventsWithUnknownAirTemperature", excludeEventsWithUnknownAirTemperature?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()),
            ("minDurationMilliseconds", minDurationMilliseconds?.ToString(CultureInfo.InvariantCulture)),
            ("maxDurationMilliseconds", maxDurationMilliseconds?.ToString(CultureInfo.InvariantCulture)),
            ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
            ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
            ("includeExternalIds", includeExternalIds?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()));

        return PaginateAsync<IdlingEvent>(path, cancellationToken: cancellationToken);
    }
}
