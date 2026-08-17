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

    /// <summary>Create a tachograph file upload — <b>moved</b>.</summary>
    /// <remarks>
    /// This operation graduated out of <c>/preview</c> in the Samsara spec: it is now
    /// <c>POST /fleet/tachograph/file-uploads</c> and lives on
    /// <see cref="ITachographClient.CreateFileUploadAsync"/>, which returns a typed
    /// <see cref="Samsara.Sdk.Models.Compliance.TachographFileUpload"/> instead of
    /// <see cref="object"/>. This member forwards to the new location and will be removed
    /// in the next major release.
    /// </remarks>
    [Obsolete("Moved to ITachographClient.CreateFileUploadAsync — the endpoint graduated out of /preview and this overload is untyped. This member will be removed in the next major release.", error: false)]
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

    // Forwarding shim. The path below is the CURRENT spec path, not the old /preview
    // one: the old path 404s. Callers keep compiling (with an obsoletion warning)
    // and keep working.
    [Obsolete("Moved to ITachographClient.CreateFileUploadAsync — see the interface for details.", error: false)]
    public Task<object> CreateTachographFileUploadAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("fleet/tachograph/file-uploads", request, cancellationToken);
}
