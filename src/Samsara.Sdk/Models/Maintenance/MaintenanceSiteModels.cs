namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;

/// <summary>
/// A maintenance site — a warehouse, shop, yard or service vehicle that holds parts inventory
/// and performs work. Mirrors the spec's <c>EntityListMaintenanceSitesTypeResponseBody</c> and
/// its byte-identical create/update twins, so one record serves <c>GET</c>, <c>POST</c> and
/// <c>PATCH</c> <c>/maintenance/sites</c>.
/// </summary>
/// <remarks>
/// Timestamps on these schemas are declared <c>type: string</c> with no
/// <c>format: date-time</c>, so they are modelled as <c>string</c>.
/// </remarks>
public sealed record MaintenanceSite
{
    /// <summary>Samsara ID for the maintenance site.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the maintenance site. Unique within the organization.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Organization-unique 3-character code for the site, used to generate inventory batch
    /// numbers.
    /// </summary>
    [JsonPropertyName("siteCode")]
    public string? SiteCode { get; init; }

    /// <summary>
    /// Type of maintenance site: <c>Unknown</c>, <c>CentralWarehouse</c>,
    /// <c>MaintenanceShop</c>, <c>MobileServiceVehicle</c>, <c>YardOnsite</c>,
    /// <c>Consignment</c> or <c>Other</c>.
    /// </summary>
    [JsonPropertyName("siteType")]
    public string? SiteType { get; init; }

    /// <summary>Description of the maintenance site.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Places this site is linked to. Mutually exclusive with <see cref="CustomAddress"/>.</summary>
    [JsonPropertyName("places")]
    public IReadOnlyList<MaintenanceEntityRef>? Places { get; init; }

    /// <summary>A free-form address for a site not linked to a place.</summary>
    [JsonPropertyName("customAddress")]
    public MaintenanceSiteCustomAddress? CustomAddress { get; init; }

    /// <summary>
    /// Customer-supplied external identifiers. Only included when the request sets
    /// <c>includeExternalIds</c>.
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalId>? ExternalIds { get; init; }

    /// <summary>Whether the site is archived (retained for history but no longer active).</summary>
    [JsonPropertyName("isArchived")]
    public bool? IsArchived { get; init; }

    /// <summary>When the site was archived. Unset when <see cref="IsArchived"/> is false.</summary>
    [JsonPropertyName("archivedAt")]
    public string? ArchivedAt { get; init; }

    /// <summary>When the site was created.</summary>
    [JsonPropertyName("createdAt")]
    public string? CreatedAt { get; init; }

    /// <summary>When the site was last updated.</summary>
    [JsonPropertyName("updatedAt")]
    public string? UpdatedAt { get; init; }
}

/// <summary>
/// The free-form address of a <see cref="MaintenanceSite"/>. Mirrors the spec's
/// <c>...MaintenanceSiteCustomAddressTypeResponseBody</c> variants.
/// </summary>
public sealed record MaintenanceSiteCustomAddress
{
    /// <summary>The formatted address.</summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// The free-form address of a site, supplied on <see cref="CreateMaintenanceSiteRequest"/>.
/// Mirrors the spec's
/// <c>CreateMaintenanceSiteEntityMaintenanceSiteMaintenanceSiteCustomAddressInputTypeRequestBody</c>,
/// which marks every member REQUIRED.
/// </summary>
public sealed record MaintenanceSiteCustomAddressInput
{
    /// <summary>The formatted address. Spec REQUIRED.</summary>
    [JsonPropertyName("formattedAddress")]
    public required string FormattedAddress { get; init; }

    /// <summary>Latitude in decimal degrees. Spec REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}

/// <summary>
/// Request body for <c>POST /maintenance/sites</c> (<c>createMaintenanceSite</c>). Mirrors the
/// spec's <c>EntityMaintenanceSitesServiceCreateMaintenanceSiteRequestBody</c>. Supply either
/// <see cref="PlaceIds"/> or <see cref="CustomAddress"/>, not both.
/// </summary>
public sealed record CreateMaintenanceSiteRequest
{
    /// <summary>Name of the maintenance site. Unique within the organization. Spec REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Organization-unique 3-character site code. Spec REQUIRED.</summary>
    [JsonPropertyName("siteCode")]
    public required string SiteCode { get; init; }

    /// <summary>Type of maintenance site (see <see cref="MaintenanceSite.SiteType"/>). Spec REQUIRED.</summary>
    [JsonPropertyName("siteType")]
    public required string SiteType { get; init; }

    /// <summary>Description of the maintenance site.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Places this site is linked to. At most one entry is accepted today; mutually exclusive
    /// with <see cref="CustomAddress"/>.
    /// </summary>
    [JsonPropertyName("placeIds")]
    public IReadOnlyList<string>? PlaceIds { get; init; }

    /// <summary>A free-form address for a site not linked to a place.</summary>
    [JsonPropertyName("customAddress")]
    public MaintenanceSiteCustomAddressInput? CustomAddress { get; init; }

    /// <summary>Customer-supplied external identifiers for the site.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalIdInput>? ExternalIds { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /maintenance/sites</c> (<c>updateMaintenanceSite</c>). Mirrors the
/// spec's <c>EntityMaintenanceSitesServiceUpdateMaintenanceSiteRequestBody</c>, which marks
/// nothing required and cannot change the site's places or custom address. The site is
/// identified by the <c>id</c> query parameter.
/// </summary>
public sealed record UpdateMaintenanceSiteRequest
{
    /// <summary>Name of the maintenance site.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Organization-unique 3-character site code.</summary>
    [JsonPropertyName("siteCode")]
    public string? SiteCode { get; init; }

    /// <summary>Type of maintenance site (see <see cref="MaintenanceSite.SiteType"/>).</summary>
    [JsonPropertyName("siteType")]
    public string? SiteType { get; init; }

    /// <summary>Description of the maintenance site.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Customer-supplied external identifiers for the site.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalIdInput>? ExternalIds { get; init; }
}
