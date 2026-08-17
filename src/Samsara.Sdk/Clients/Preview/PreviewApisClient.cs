namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;
using Samsara.Sdk.Models.Drivers;
using Samsara.Sdk.Models.Preview;

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
    /// <remarks>
    /// The preview request body is <b>not</b> the same schema as the stable
    /// <c>POST /fleet/drivers/auth-token</c> body: it identifies the driver with
    /// <c>id</c> rather than <c>driverId</c>. That is why this method takes
    /// <see cref="PreviewCreateDriverAuthTokenRequest"/> and not
    /// <see cref="CreateDriverAuthTokenRequest"/>. The success payloads are
    /// identical, so both operations return
    /// <see cref="Samsara.Sdk.Models.Drivers.DriverAuthToken"/>.
    /// </remarks>
    Task<DriverAuthToken> CreateDriverAuthTokenAsync(PreviewCreateDriverAuthTokenRequest request, CancellationToken cancellationToken = default);

    /// <summary>Create a tachograph file upload — <b>moved</b>.</summary>
    /// <remarks>
    /// This operation graduated out of <c>/preview</c> in the Samsara spec: it is now
    /// <c>POST /fleet/tachograph/file-uploads</c> and lives on
    /// <see cref="ITachographClient.CreateFileUploadAsync"/>. This member forwards to
    /// the new location and will be removed in the next major release; prefer the
    /// tachograph client, which carries the <c>[Experimental("SAMSARA001")]</c>
    /// annotation recording that Samsara still tags the operation <c>[beta]</c>.
    /// </remarks>
    [Obsolete("Moved to ITachographClient.CreateFileUploadAsync — the endpoint graduated out of /preview. This member will be removed in the next major release.", error: false)]
    Task<TachographFileUpload> CreateTachographFileUploadAsync(CreateTachographFileUploadRequest request, CancellationToken cancellationToken = default);
}

internal sealed class PreviewApisClient : SamsaraServiceClientBase, IPreviewApisClient
{
    public PreviewApisClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task LockVehicleAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<object>($"preview/fleet/vehicles/{Uri.EscapeDataString(id)}/lock", new { }, cancellationToken);

    public Task UnlockVehicleAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"preview/fleet/vehicles/{Uri.EscapeDataString(id)}/lock", cancellationToken);

    public Task<DriverAuthToken> CreateDriverAuthTokenAsync(PreviewCreateDriverAuthTokenRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverAuthToken>("preview/fleet/drivers/create-auth-token", request, cancellationToken);

    // Forwarding shim. The path below is the CURRENT spec path, not the old /preview
    // one: the old path 404s. Callers keep compiling (with an obsoletion warning)
    // and keep working.
    [Obsolete("Moved to ITachographClient.CreateFileUploadAsync — see the interface for details.", error: false)]
    public Task<TachographFileUpload> CreateTachographFileUploadAsync(CreateTachographFileUploadRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<TachographFileUpload>("fleet/tachograph/file-uploads", request, cancellationToken);
}
