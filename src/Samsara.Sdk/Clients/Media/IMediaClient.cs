namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Media;

/// <summary>
/// Client for retrieving Samsara media files.
/// </summary>
public interface IMediaClient
{
    IAsyncEnumerable<MediaFile> ListAsync(CancellationToken cancellationToken = default);
    Task<MediaFile> GetAsync(string id, CancellationToken cancellationToken = default);
}
