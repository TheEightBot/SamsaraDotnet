namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>
/// Beta — fleet installer photo uploads (<c>/fleet/installer/photo-uploads*</c>).
/// </summary>
/// <remarks>
/// <para>
/// Uploading is a three-step flow:
/// </para>
/// <list type="number">
/// <item><description>
/// <c>CreateInstallerPhotoUploadAsync</c> registers the file and returns a
/// presigned S3 URL plus the headers to send.
/// </description></item>
/// <item><description>
/// The caller PUTs the file bytes to that URL, verbatim headers included, before
/// the returned expiry. The SDK does not perform this step — it targets a
/// non-Samsara host.
/// </description></item>
/// <item><description>
/// <c>CompleteInstallerPhotoUploadAsync</c> marks the session complete.
/// </description></item>
/// </list>
/// <para>
/// Every operation here is tagged <c>[beta]</c> by Samsara and is annotated
/// <c>[Experimental("SAMSARA001")]</c>; suppress that diagnostic to opt in.
/// </para>
/// </remarks>
public interface IInstallerClient
{
    /// <summary>
    /// List fleet installer photo upload sessions
    /// (<c>GET /fleet/installer/photo-uploads</c>,
    /// <c>getFleetInstallerPhotoUploads</c>). Sessions are ordered by
    /// <c>updatedAtTime</c> ascending; pagination is handled transparently.
    /// </summary>
    /// <param name="ids">Optional upload session IDs to filter by. Max 100.</param>
    /// <param name="startTime">Optional RFC 3339 lower bound on <c>updatedAtTime</c>.</param>
    /// <param name="endTime">Optional RFC 3339 upper bound on <c>updatedAtTime</c>. Requires <paramref name="startTime"/>.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<InstallerPhotoUploadSession> GetInstallerPhotoUploadsAsync(
        IReadOnlyList<string>? ids = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a fleet installer photo upload session
    /// (<c>POST /fleet/installer/photo-uploads</c>,
    /// <c>postFleetInstallerPhotoUpload</c>). The response carries the presigned
    /// upload target in
    /// <see cref="InstallerPhotoUploadSession.UploadContext"/>.
    /// </summary>
    [Experimental("SAMSARA001")]
    Task<InstallerPhotoUploadSession> CreateInstallerPhotoUploadAsync(
        CreateInstallerPhotoUploadRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Mark a fleet installer photo upload session complete
    /// (<c>POST /fleet/installer/photo-uploads/complete</c>,
    /// <c>postFleetInstallerPhotoUploadComplete</c>). The spec defines no request
    /// body; the session is identified by query string.
    /// </summary>
    /// <param name="id">Upload session ID. Exactly one. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<InstallerPhotoUploadSession> CompleteInstallerPhotoUploadAsync(
        string id,
        CancellationToken cancellationToken = default);
}

internal sealed class InstallerClient : SamsaraServiceClientBase, IInstallerClient
{
    private const string BasePath = "fleet/installer/photo-uploads";

    public InstallerClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>List installer photo upload sessions (<c>GET /fleet/installer/photo-uploads</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<InstallerPhotoUploadSession> GetInstallerPhotoUploadsAsync(
        IReadOnlyList<string>? ids = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<InstallerPhotoUploadSession>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange(BasePath, startTime, endTime),
                ("ids", ids is null ? null : string.Join(",", ids))),
            cancellationToken: cancellationToken);

    /// <summary>Create an installer photo upload session (<c>POST /fleet/installer/photo-uploads</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<InstallerPhotoUploadSession> CreateInstallerPhotoUploadAsync(
        CreateInstallerPhotoUploadRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<InstallerPhotoUploadSession>(BasePath, request, cancellationToken);

    /// <summary>Complete an installer photo upload session (<c>POST /fleet/installer/photo-uploads/complete</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<InstallerPhotoUploadSession> CompleteInstallerPhotoUploadAsync(
        string id,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<InstallerPhotoUploadSession>(
            QueryBuilder.WithParams($"{BasePath}/complete", ("id", id)), new { }, cancellationToken);
}
