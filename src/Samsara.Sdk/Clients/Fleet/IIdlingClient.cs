namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>Client for Samsara idling events.</summary>
public interface IIdlingClient
{
    IAsyncEnumerable<IdlingEvent> ListEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
