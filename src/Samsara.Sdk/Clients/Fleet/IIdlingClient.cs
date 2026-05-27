namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>Client for Samsara idling events.</summary>
public interface IIdlingClient
{
    /// <summary>
    /// Lists idling events (<c>GET /idling/events</c>). The Samsara API requires
    /// <c>startTime</c> and <c>endTime</c>; all other parameters are optional filters.
    /// </summary>
    /// <param name="startTime">Inclusive lower bound for the event start time (RFC 3339).</param>
    /// <param name="endTime">Inclusive upper bound for the event start time (RFC 3339).</param>
    /// <param name="assetIds">Optional filter by Samsara asset IDs.</param>
    /// <param name="operatorIds">Optional filter by Samsara operator (driver) IDs.</param>
    /// <param name="ptoState">Optional PTO state filter (<c>active</c> or <c>inactive</c>).</param>
    /// <param name="minAirTemperatureMillicelsius">Optional minimum air temperature in millicelsius.</param>
    /// <param name="maxAirTemperatureMillicelsius">Optional maximum air temperature in millicelsius.</param>
    /// <param name="excludeEventsWithUnknownAirTemperature">If true, exclude events with unknown air temperature.</param>
    /// <param name="minDurationMilliseconds">Optional minimum event duration in milliseconds.</param>
    /// <param name="maxDurationMilliseconds">Optional maximum event duration in milliseconds.</param>
    /// <param name="tagIds">Optional filter by tag IDs.</param>
    /// <param name="parentTagIds">Optional filter by parent tag IDs.</param>
    /// <param name="includeExternalIds">If true, include external IDs on nested asset/operator/address objects.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<IdlingEvent> ListEventsAsync(
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
        CancellationToken cancellationToken = default);
}
