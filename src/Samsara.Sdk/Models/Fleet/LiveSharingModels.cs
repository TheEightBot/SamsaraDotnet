namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a Samsara Live Sharing Link. Mirrors the spec's
/// <c>LiveSharingLinkFullResponseObjectResponseBody</c>.
/// </summary>
public sealed record LiveSharingLink
{
    /// <summary>Unique identifier for the Live Sharing Link. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the Live Sharing Link. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// The shareable URL of the vehicle's location. Spec-required (returned for
    /// every Live Sharing Link).
    /// </summary>
    [JsonPropertyName("liveSharingUrl")]
    public required string LiveSharingUrl { get; init; }

    /// <summary>
    /// Type of the Live Sharing Link. Spec-required. Valid values:
    /// <c>assetsLocation</c>, <c>assetsNearLocation</c>, <c>assetsOnRoute</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>
    /// Optional description for the Live Sharing Link (not applicable for the
    /// <c>assetsOnRoute</c> type).
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Date when this link expires, in RFC 3339 format
    /// (e.g., <c>2020-01-27T07:06:25Z</c>). When unset, the link does not expire.
    /// </summary>
    [JsonPropertyName("expiresAtTime")]
    public string? ExpiresAtTime { get; init; }

    /// <summary>
    /// Configuration specific to the <c>assetsLocation</c> ("By Asset") link type.
    /// Populated only when <see cref="Type"/> is <c>assetsLocation</c>.
    /// </summary>
    [JsonPropertyName("assetsLocationLinkConfig")]
    public LiveSharingLinkAssetsLocationLinkConfig? AssetsLocationLinkConfig { get; init; }

    /// <summary>
    /// Configuration specific to the <c>assetsNearLocation</c> ("By Location")
    /// link type. Populated only when <see cref="Type"/> is
    /// <c>assetsNearLocation</c>.
    /// </summary>
    [JsonPropertyName("assetsNearLocationLinkConfig")]
    public LiveSharingLinkAssetsNearLocationLinkConfig? AssetsNearLocationLinkConfig { get; init; }

    /// <summary>
    /// Configuration specific to the <c>assetsOnRoute</c> ("By Recurring Route")
    /// link type. Populated only when <see cref="Type"/> is
    /// <c>assetsOnRoute</c>.
    /// </summary>
    [JsonPropertyName("assetsOnRouteLinkConfig")]
    public LiveSharingLinkAssetsOnRouteLinkConfig? AssetsOnRouteLinkConfig { get; init; }

    // ── Back-compat (not in spec inner schema) ──────────────────────────────

    /// <summary>
    /// Legacy alias for <see cref="LiveSharingUrl"/>. Not part of the current
    /// spec inner schema; retained as a nullable back-compat convenience.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Legacy alias for <see cref="ExpiresAtTime"/> typed as
    /// <see cref="DateTimeOffset"/>. Not part of the current spec inner schema;
    /// retained as a nullable back-compat convenience.
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset? ExpiresAt { get; init; }

    /// <summary>
    /// Legacy flat identifier for the entity that this link tracks (asset id,
    /// address id, or recurring route id depending on <see cref="Type"/>). Not
    /// part of the current spec inner schema; retained as a nullable back-compat
    /// convenience. Prefer the typed <c>*LinkConfig</c> properties for new code.
    /// </summary>
    [JsonPropertyName("entityId")]
    public string? EntityId { get; init; }

    /// <summary>
    /// Legacy entity-type descriptor. Not part of the current spec inner schema;
    /// retained as a nullable back-compat convenience. Prefer the typed
    /// <see cref="Type"/> property for new code.
    /// </summary>
    [JsonPropertyName("entityType")]
    public string? EntityType { get; init; }
}

/// <summary>
/// Configuration for the <c>assetsLocation</c> ("By Asset") Live Sharing Link.
/// Mirrors the spec's <c>AssetsLocationLinkResponseConfigObjectResponseBody</c>.
/// </summary>
public sealed record LiveSharingLinkAssetsLocationLinkConfig
{
    /// <summary>Unique asset ID that the Live Sharing Link will show.</summary>
    [JsonPropertyName("assetId")]
    public string? AssetId { get; init; }

    /// <summary>
    /// Address information (destination point and/or ETA) the Live Sharing
    /// Link displays.
    /// </summary>
    [JsonPropertyName("location")]
    public LiveSharingLinkLocation? Location { get; init; }

    /// <summary>
    /// Tags associated with assets for the Live Sharing Link. Only populated
    /// when the link is configured by tags rather than by a single asset ID.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<LiveSharingLinkTag>? Tags { get; init; }
}

/// <summary>
/// Configuration for the <c>assetsNearLocation</c> ("By Location") Live
/// Sharing Link. Mirrors the spec's
/// <c>AssetsNearLocationLinkConfigObjectResponseBody</c>.
/// </summary>
public sealed record LiveSharingLinkAssetsNearLocationLinkConfig
{
    /// <summary>
    /// ID of the address. May be a Samsara ID or an external ID. Spec-required.
    /// </summary>
    [JsonPropertyName("addressId")]
    public required string AddressId { get; init; }
}

/// <summary>
/// Configuration for the <c>assetsOnRoute</c> ("By Recurring Route") Live
/// Sharing Link. Mirrors the spec's
/// <c>AssetsOnRouteLinkConfigObjectResponseBody</c>.
/// </summary>
public sealed record LiveSharingLinkAssetsOnRouteLinkConfig
{
    /// <summary>Samsara ID of the recurring route. Spec-required.</summary>
    [JsonPropertyName("recurringRouteId")]
    public required string RecurringRouteId { get; init; }
}

/// <summary>
/// Address information shown by an <c>assetsLocation</c> Live Sharing Link.
/// Mirrors the spec's
/// <c>AssetsLocationLinkConfigAddressDetailsObjectResponseBody</c>.
/// </summary>
public sealed record LiveSharingLinkLocation
{
    /// <summary>Formatted address of the location. Spec-required.</summary>
    [JsonPropertyName("formattedAddress")]
    public required string FormattedAddress { get; init; }

    /// <summary>Latitude of the location. Spec-required.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude of the location. Spec-required.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    /// <summary>Name of the location. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

/// <summary>
/// A minified tag reference used by Live Sharing Link configuration. Mirrors
/// the spec's <c>GoaTagTinyResponseResponseBody</c>.
/// </summary>
public sealed record LiveSharingLinkTag
{
    /// <summary>ID of the tag. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the tag. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// If this tag is part of a hierarchical tag tree, the ID of the parent
    /// tag; otherwise omitted.
    /// </summary>
    [JsonPropertyName("parentTagId")]
    public string? ParentTagId { get; init; }
}

/// <summary>Request body for creating a Live Sharing Link.</summary>
public sealed record CreateLiveSharingLinkRequest
{
    /// <summary>Name of the Live Sharing Link. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Type of the Live Sharing Link. Spec-required. Determines which one of
    /// the <c>*LinkConfig</c> objects below is used. Valid values:
    /// <c>assetsLocation</c>, <c>assetsNearLocation</c>, <c>assetsOnRoute</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>
    /// Optional description for the Live Sharing Link (not applicable for the
    /// <c>assetsOnRoute</c> type).
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Optional expiration timestamp in RFC 3339 format. Cannot be in the past.
    /// When omitted, the link never expires.
    /// </summary>
    [JsonPropertyName("expiresAtTime")]
    public string? ExpiresAtTime { get; init; }

    /// <summary>
    /// Configuration when <see cref="Type"/> is <c>assetsLocation</c>.
    /// </summary>
    [JsonPropertyName("assetsLocationLinkConfig")]
    public CreateAssetsLocationLinkConfig? AssetsLocationLinkConfig { get; init; }

    /// <summary>
    /// Configuration when <see cref="Type"/> is <c>assetsNearLocation</c>.
    /// </summary>
    [JsonPropertyName("assetsNearLocationLinkConfig")]
    public LiveSharingLinkAssetsNearLocationLinkConfig? AssetsNearLocationLinkConfig { get; init; }

    /// <summary>
    /// Configuration when <see cref="Type"/> is <c>assetsOnRoute</c>.
    /// </summary>
    [JsonPropertyName("assetsOnRouteLinkConfig")]
    public LiveSharingLinkAssetsOnRouteLinkConfig? AssetsOnRouteLinkConfig { get; init; }
}

/// <summary>
/// Request configuration for the <c>assetsLocation</c> ("By Asset") Live
/// Sharing Link. Mirrors the spec's
/// <c>AssetsLocationLinkRequestConfigObject</c>. Differs from the response
/// shape in that it accepts <c>tagIds</c> instead of returning the resolved
/// <c>tags</c> array.
/// </summary>
public sealed record CreateAssetsLocationLinkConfig
{
    /// <summary>Unique asset ID that the Live Sharing Link will show.</summary>
    [JsonPropertyName("assetId")]
    public string? AssetId { get; init; }

    /// <summary>
    /// Address information (destination point and/or ETA) the link will
    /// display.
    /// </summary>
    [JsonPropertyName("location")]
    public LiveSharingLinkLocation? Location { get; init; }

    /// <summary>Array of tag IDs to filter data by.</summary>
    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }
}

/// <summary>Request body for updating a Live Sharing Link.</summary>
public sealed record UpdateLiveSharingLinkRequest
{
    /// <summary>Name of the Live Sharing Link. Spec-required on update.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Optional description for the Live Sharing Link (not applicable for the
    /// <c>assetsOnRoute</c> type).
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Optional expiration timestamp in RFC 3339 format. Cannot be in the past.
    /// When omitted, the link never expires.
    /// </summary>
    [JsonPropertyName("expiresAtTime")]
    public string? ExpiresAtTime { get; init; }
}
