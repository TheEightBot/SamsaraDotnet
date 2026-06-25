namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>
/// Preview / beta endpoints under <c>/preview/*</c> — subject to change.
/// </summary>
public interface IPreviewApisClient
{
    /// <summary>Lock a vehicle remotely (<c>PUT /preview/fleet/vehicles/{id}/lock</c>).</summary>
    Task LockVehicleAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>Unlock a vehicle remotely (<c>DELETE /preview/fleet/vehicles/{id}/lock</c>).</summary>
    Task UnlockVehicleAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>Create a driver auth token via the preview endpoint
    /// (<c>POST /preview/fleet/drivers/create-auth-token</c>).</summary>
    Task<object> CreateDriverAuthTokenAsync(object request, CancellationToken cancellationToken = default);

    /// <summary>Create a tachograph file upload
    /// (<c>POST /preview/fleet/tachograph/file-uploads</c>) — preview. Loosely typed.</summary>
    Task<object> CreateTachographFileUploadAsync(object request, CancellationToken cancellationToken = default);
}

internal sealed class PreviewApisClient : SamsaraServiceClientBase, IPreviewApisClient
{
    public PreviewApisClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task LockVehicleAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<object>($"preview/fleet/vehicles/{Uri.EscapeDataString(id)}/lock", new { }, cancellationToken);

    public Task UnlockVehicleAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"preview/fleet/vehicles/{Uri.EscapeDataString(id)}/lock", cancellationToken);

    public Task<object> CreateDriverAuthTokenAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("preview/fleet/drivers/create-auth-token", request, cancellationToken);

    public Task<object> CreateTachographFileUploadAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("preview/fleet/tachograph/file-uploads", request, cancellationToken);
}
