namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Communication;

/// <summary>
/// Client for Samsara driver messages.
/// </summary>
public interface IMessagesClient
{
    IAsyncEnumerable<DriverMessage> ListAsync(CancellationToken cancellationToken = default);
    Task SendAsync(SendDriverMessageRequest request, CancellationToken cancellationToken = default);
}
