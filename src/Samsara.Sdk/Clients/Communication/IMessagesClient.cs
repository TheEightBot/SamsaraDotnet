namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Communication;

/// <summary>
/// Client for Samsara driver messages (legacy v1 endpoints under
/// <c>/v1/fleet/messages</c>).
/// </summary>
public interface IMessagesClient
{
    /// <summary>
    /// List driver messages (<c>GET /v1/fleet/messages</c>).
    /// </summary>
    /// <param name="endMs">
    /// Time in Unix milliseconds that represents the end of the range of
    /// messages to return. Used in combination with <paramref name="durationMs"/>.
    /// Defaults server-side to now.
    /// </param>
    /// <param name="durationMs">
    /// Duration in milliseconds before <paramref name="endMs"/> to query.
    /// Defaults server-side to 24 hours.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<DriverMessage> ListAsync(
        long? endMs = null,
        long? durationMs = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send a message to one or more drivers (<c>POST /v1/fleet/messages</c>).
    /// </summary>
    Task SendAsync(SendDriverMessageRequest request, CancellationToken cancellationToken = default);
}
