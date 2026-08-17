namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using Samsara.Sdk.Models.Compliance;

/// <summary>
/// Client for Samsara tachograph data.
/// </summary>
public interface ITachographClient
{
    IAsyncEnumerable<TachographActivity> ListActivitiesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? driverIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TachographFile> ListFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? driverIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, CancellationToken cancellationToken = default);
    /// <summary>Vehicle tachograph files history (<c>GET /fleet/vehicles/tachograph-files/history</c>).</summary>
    IAsyncEnumerable<TachographFile> ListVehicleFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? vehicleIds = null, IReadOnlyList<string>? tagIds = null, IReadOnlyList<string>? parentTagIds = null, CancellationToken cancellationToken = default);
    /// <summary>Latest tachograph live-data (beta).</summary>
    IAsyncEnumerable<TachographLiveData> ListLiveDataAsync(
        string? driverIds = null,
        string? vehicleIds = null,
        DateTimeOffset? startTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a tachograph file upload (<c>POST /fleet/tachograph/file-uploads</c>),
    /// returning a pre-signed URL and the headers required to upload the file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This operation graduated out of <c>/preview</c> in the Samsara spec; it previously
    /// lived on <c>IPreviewApisClient.CreateTachographFileUploadAsync</c>, which now
    /// forwards here and is marked obsolete.
    /// </para>
    /// <para>
    /// Samsara still tags the operation <c>[beta]</c>, so its shape may change without a
    /// spec version bump. It is marked <c>[Experimental("SAMSARA001")]</c>; suppress that
    /// diagnostic (for example <c>&lt;NoWarn&gt;SAMSARA001&lt;/NoWarn&gt;</c>) to
    /// acknowledge the risk.
    /// </para>
    /// </remarks>
    [Experimental("SAMSARA001")]
    Task<TachographFileUpload> CreateFileUploadAsync(
        CreateTachographFileUploadRequest request,
        CancellationToken cancellationToken = default);
}
