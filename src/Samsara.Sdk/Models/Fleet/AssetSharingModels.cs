namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

/// <summary>
/// Models for the beta Data Sharing Agreement (asset sharing) API,
/// <c>/fleet/asset-sharing/agreements*</c>.
/// </summary>
/// <remarks>
/// <para>
/// Timestamp properties on these schemas are declared <c>type: string</c> with no
/// <c>format: date-time</c>, and several are documented as accepting/returning an
/// empty string (see <see cref="UpdateSharedAssetInput.EndTime"/>). They are therefore
/// modelled as <c>string</c> rather than <c>DateTimeOffset</c>, mirroring the spec
/// exactly instead of guessing a stricter type the API does not promise.
/// </para>
/// <para>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </para>
/// </remarks>
public sealed record AssetSharingAgreement
{
    /// <summary>Unique identifier for the agreement. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Which side of the agreement the calling organization acts as:
    /// <c>provider</c> or <c>recipient</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; init; }

    /// <summary>
    /// Data packages the provider shares. Values: <c>safety</c>, <c>telematics</c>,
    /// <c>location</c>, <c>maintenance</c>, <c>reefer</c>, <c>all</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("providerDataPackages")]
    public IReadOnlyList<string>? ProviderDataPackages { get; init; }

    /// <summary>ID of the organization that owns the assets shared under the agreement. Spec marks REQUIRED.</summary>
    [JsonPropertyName("providerOrganizationId")]
    public string? ProviderOrganizationId { get; init; }

    /// <summary>
    /// Data packages the recipient receives. Values: <c>safety</c>, <c>telematics</c>,
    /// <c>location</c>, <c>maintenance</c>, <c>reefer</c>, <c>all</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("recipientDataPackages")]
    public IReadOnlyList<string>? RecipientDataPackages { get; init; }

    /// <summary>ID of the organization that rents the assets from the provider. Spec marks REQUIRED.</summary>
    [JsonPropertyName("recipientOrganizationId")]
    public string? RecipientOrganizationId { get; init; }

    /// <summary>
    /// Current agreement status: <c>pending</c>, <c>accepted</c>, <c>rejected</c>
    /// or <c>canceled</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Time the agreement was created, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>Time the agreement was last updated, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }

    /// <summary>Time the agreement was accepted, in RFC 3339 format.</summary>
    [JsonPropertyName("acceptedAtTime")]
    public string? AcceptedAtTime { get; init; }

    /// <summary>User ID who accepted the agreement (visible only to the recipient).</summary>
    [JsonPropertyName("acceptedByUserId")]
    public string? AcceptedByUserId { get; init; }

    /// <summary>Time the agreement was canceled, in RFC 3339 format.</summary>
    [JsonPropertyName("canceledAtTime")]
    public string? CanceledAtTime { get; init; }

    /// <summary>Which party canceled the agreement: <c>provider</c> or <c>recipient</c>.</summary>
    [JsonPropertyName("canceledByParty")]
    public string? CanceledByParty { get; init; }

    /// <summary>User ID who canceled the agreement (visible only to the canceling party).</summary>
    [JsonPropertyName("canceledByUserId")]
    public string? CanceledByUserId { get; init; }

    /// <summary>User ID in the provider org who created the agreement.</summary>
    [JsonPropertyName("createdByUserId")]
    public string? CreatedByUserId { get; init; }

    /// <summary>User ID in the provider org who soft-deleted the agreement.</summary>
    [JsonPropertyName("deletedByUserId")]
    public string? DeletedByUserId { get; init; }

    /// <summary>Time the agreement was rejected, in RFC 3339 format.</summary>
    [JsonPropertyName("rejectedAtTime")]
    public string? RejectedAtTime { get; init; }

    /// <summary>User ID who rejected the agreement (visible only to the recipient).</summary>
    [JsonPropertyName("rejectedByUserId")]
    public string? RejectedByUserId { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/asset-sharing/agreements</c>
/// (<c>createAssetSharingAgreement</c>). Mirrors the spec's
/// <c>AssetSharingAgreementsCreateAssetSharingAgreementRequestBody</c>.
/// </summary>
public sealed record CreateAssetSharingAgreementRequest
{
    /// <summary>
    /// Which side the calling organization acts as: <c>provider</c> or
    /// <c>recipient</c>. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("operator")]
    public required string Operator { get; init; }

    /// <summary>
    /// Data packages the provider shares. Values: <c>safety</c>, <c>telematics</c>,
    /// <c>location</c>, <c>maintenance</c>, <c>reefer</c>, <c>all</c>. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("providerDataPackages")]
    public required IReadOnlyList<string> ProviderDataPackages { get; init; }

    /// <summary>
    /// Data packages the recipient receives. Values: <c>safety</c>, <c>telematics</c>,
    /// <c>location</c>, <c>maintenance</c>, <c>reefer</c>, <c>all</c>. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("recipientDataPackages")]
    public required IReadOnlyList<string> RecipientDataPackages { get; init; }

    /// <summary>The ID of the recipient organization. Spec REQUIRED.</summary>
    [JsonPropertyName("recipientOrganizationId")]
    public required string RecipientOrganizationId { get; init; }

    /// <summary>The display name to use for the recipient organization. Spec REQUIRED.</summary>
    [JsonPropertyName("recipientOrganizationName")]
    public required string RecipientOrganizationName { get; init; }
}

/// <summary>
/// An asset shared under a Data Sharing Agreement. Mirrors the spec's
/// <c>SharedAssetResponseObjectResponseBody</c>, returned by
/// <c>GET /fleet/asset-sharing/agreements/assets</c> and by the batch
/// create/update operations.
/// </summary>
public sealed record SharedAsset
{
    /// <summary>Unique identifier for the shared asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Asset ID in the provider organization. Spec marks REQUIRED.</summary>
    [JsonPropertyName("providerAssetId")]
    public string? ProviderAssetId { get; init; }

    /// <summary>Asset ID in the recipient organization. Only populated after the asset is mirrored.</summary>
    [JsonPropertyName("recipientAssetId")]
    public string? RecipientAssetId { get; init; }

    /// <summary>The serial number of the shared asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    /// <summary>Start time of the sharing period in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End time of the sharing period in RFC 3339 format. Null means indefinite sharing.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>Time the shared asset was created, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>User ID who created the shared asset.</summary>
    [JsonPropertyName("createdByUserId")]
    public string? CreatedByUserId { get; init; }
}

/// <summary>
/// One asset to share, as an element of <see cref="CreateSharedAssetsBatchRequest"/>.
/// Mirrors the spec's <c>CreateSharedAssetRequestObjectRequestBody</c>.
/// </summary>
public sealed record CreateSharedAssetInput
{
    /// <summary>The serial number of the asset to share. Spec REQUIRED.</summary>
    [JsonPropertyName("serial")]
    public required string Serial { get; init; }

    /// <summary>Start time of the sharing period in RFC 3339 format. Defaults to now when omitted.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End time of the sharing period in RFC 3339 format. Null or omitted means indefinite sharing.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/asset-sharing/agreements/assets/batch</c>
/// (<c>createSharedAssetsBatch</c>). The spec wraps the array in a
/// <c>{ data: [...] }</c> envelope.
/// </summary>
public sealed record CreateSharedAssetsBatchRequest
{
    /// <summary>List of assets to share. Spec REQUIRED.</summary>
    [JsonPropertyName("data")]
    public required IReadOnlyList<CreateSharedAssetInput> Data { get; init; }
}

/// <summary>
/// One shared-asset update, as an element of <see cref="UpdateSharedAssetsBatchRequest"/>.
/// Mirrors the spec's <c>UpdateSharedAssetRequestObjectRequestBody</c>.
/// </summary>
public sealed record UpdateSharedAssetInput
{
    /// <summary>The unique identifier of the shared asset. Spec REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// New end time for the sharing period in RFC 3339 format. Spec REQUIRED —
    /// send an empty string to make the sharing period indefinite, which is why
    /// this is a <c>string</c> and not a <c>DateTimeOffset</c>.
    /// </summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /fleet/asset-sharing/agreements/assets/batch</c>
/// (<c>updateSharedAssetsBatch</c>). The spec wraps the array in a
/// <c>{ data: [...] }</c> envelope.
/// </summary>
public sealed record UpdateSharedAssetsBatchRequest
{
    /// <summary>List of shared assets to update. Spec REQUIRED.</summary>
    [JsonPropertyName("data")]
    public required IReadOnlyList<UpdateSharedAssetInput> Data { get; init; }
}
