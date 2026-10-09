namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;

/// <summary>
/// An asset-attribute selection on a vendor or vendor group: the attribute and the values of
/// it the vendor can service. Mirrors the spec's <c>VendorPublicAttributeSelectionResponseBody</c>
/// and the structurally identical <c>...VendorAssetAttributeSelectionTypeResponseBody</c> variants.
/// </summary>
public sealed record VendorAssetAttributeSelection
{
    /// <summary>ID of the asset attribute. Spec marks REQUIRED on the list shape.</summary>
    [JsonPropertyName("attributeId")]
    public string? AttributeId { get; init; }

    /// <summary>The selected attribute values. Spec marks REQUIRED on the list shape.</summary>
    [JsonPropertyName("values")]
    public IReadOnlyList<MaintenanceEntityRef>? Values { get; init; }
}

/// <summary>
/// An asset-attribute selection supplied on a vendor or vendor-group request body. Mirrors the
/// spec's <c>...VendorAssetAttributeSelectionInputTypeRequestBody</c> variants.
/// </summary>
public sealed record VendorAssetAttributeSelectionInput
{
    /// <summary>ID of the asset attribute. Spec REQUIRED.</summary>
    [JsonPropertyName("attributeId")]
    public required string AttributeId { get; init; }

    /// <summary>The selected attribute values. Spec REQUIRED.</summary>
    [JsonPropertyName("values")]
    public required IReadOnlyList<MaintenanceEntityRefInput> Values { get; init; }
}

/// <summary>
/// The effective settings of a <see cref="MaintenanceVendor"/> once vendor-group and system
/// defaults are applied. Mirrors the spec's <c>VendorPublicResolvedSettingsResponseBody</c>.
/// Each <c>...Source</c> member names where the value came from: <c>system</c>,
/// <c>vendorGroup</c>, <c>vendorLocation</c> or <c>unknown</c>.
/// </summary>
/// <remarks>
/// Spec marks every member except <see cref="DefaultLaborRatePerHour"/> REQUIRED; they stay
/// nullable because this is a response record.
/// </remarks>
public sealed record VendorResolvedSettings
{
    /// <summary>Whether both the vendor location and its parent group, if any, are active.</summary>
    [JsonPropertyName("isActive")]
    public bool? IsActive { get; init; }

    /// <summary>Resolved mobile service. System default <c>false</c>.</summary>
    [JsonPropertyName("isMobile")]
    public bool? IsMobile { get; init; }

    /// <summary>Where <see cref="IsMobile"/> came from.</summary>
    [JsonPropertyName("isMobileSource")]
    public string? IsMobileSource { get; init; }

    /// <summary>Resolved preferred status. System default <c>false</c>.</summary>
    [JsonPropertyName("isPreferred")]
    public bool? IsPreferred { get; init; }

    /// <summary>Where <see cref="IsPreferred"/> came from.</summary>
    [JsonPropertyName("isPreferredSource")]
    public string? IsPreferredSource { get; init; }

    /// <summary>Resolved default hourly labor rate.</summary>
    [JsonPropertyName("defaultLaborRatePerHour")]
    public MaintenanceMoney? DefaultLaborRatePerHour { get; init; }

    /// <summary>Where <see cref="DefaultLaborRatePerHour"/> came from.</summary>
    [JsonPropertyName("defaultLaborRatePerHourSource")]
    public string? DefaultLaborRatePerHourSource { get; init; }

    /// <summary>Resolved asset-attribute selections. System default empty.</summary>
    [JsonPropertyName("assetAttributeSelections")]
    public IReadOnlyList<VendorAssetAttributeSelection>? AssetAttributeSelections { get; init; }

    /// <summary>Where <see cref="AssetAttributeSelections"/> came from.</summary>
    [JsonPropertyName("assetAttributeSelectionsSource")]
    public string? AssetAttributeSelectionsSource { get; init; }
}

/// <summary>
/// A contact person at a vendor, or a vendor group's primary corporate contact. Mirrors the
/// spec's <c>...VendorContactTypeResponseBody</c> and
/// <c>...VendorGroupPrimaryCorporateContactTypeResponseBody</c> variants.
/// </summary>
public sealed record MaintenanceVendorContact
{
    /// <summary>Name of the contact.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Email address of the contact.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>Phone number of the contact.</summary>
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; init; }
}

/// <summary>
/// A vendor contact supplied on a vendor request body. Mirrors the spec's
/// <c>...VendorContactInputTypeRequestBody</c> variants, which mark every member REQUIRED.
/// </summary>
public sealed record MaintenanceVendorContactInput
{
    /// <summary>Name of the contact. Spec REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Email address of the contact. Spec REQUIRED.</summary>
    [JsonPropertyName("email")]
    public required string Email { get; init; }

    /// <summary>Phone number of the contact. Spec REQUIRED.</summary>
    [JsonPropertyName("phoneNumber")]
    public required string PhoneNumber { get; init; }
}

/// <summary>
/// A maintenance vendor as returned by <c>POST</c> and <c>PATCH /fleet/maintenance/vendors</c>.
/// Mirrors the spec's <c>EntityCreateVendorTypeResponseBody</c> and its byte-identical update
/// twin.
/// </summary>
/// <remarks>
/// This is not the same shape as the list item <see cref="MaintenanceVendor"/>: the write
/// operations return contact details, notes and an <c>externalIds</c> <b>list</b>, while the
/// list endpoint returns category IDs, resolved settings and an <c>externalIds</c> <b>map</b>.
/// </remarks>
public sealed record MaintenanceVendorDetails
{
    /// <summary>Internal unique identifier for the vendor.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vendor.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>User-defined identifier for the vendor.</summary>
    [JsonPropertyName("vendorId")]
    public string? VendorId { get; init; }

    /// <summary>External identifier for the vendor from an external system.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>
    /// Customer-supplied external identifiers. Only included when the request sets
    /// <c>includeExternalIds</c>.
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalId>? ExternalIds { get; init; }

    /// <summary>Own lifecycle status: <c>active</c>, <c>inactive</c> or <c>unknown</c>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Vendor group the vendor belongs to.</summary>
    [JsonPropertyName("vendorGroupId")]
    public string? VendorGroupId { get; init; }

    /// <summary>Address of the vendor.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>The place linked to the vendor address.</summary>
    [JsonPropertyName("placeId")]
    public MaintenanceEntityRef? PlaceId { get; init; }

    /// <summary>People to contact at the vendor.</summary>
    [JsonPropertyName("contacts")]
    public IReadOnlyList<MaintenanceVendorContact>? Contacts { get; init; }

    /// <summary>Email addresses for the vendor.</summary>
    [JsonPropertyName("emailAddresses")]
    public IReadOnlyList<string>? EmailAddresses { get; init; }

    /// <summary>Phone numbers for the vendor.</summary>
    [JsonPropertyName("phoneNumbers")]
    public IReadOnlyList<string>? PhoneNumbers { get; init; }

    /// <summary>Additional notes about the vendor.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Free-text AP/ERP payee identifier for the vendor.</summary>
    [JsonPropertyName("payeeId")]
    public string? PayeeId { get; init; }

    /// <summary>Description of services provided by the vendor.</summary>
    [JsonPropertyName("servicesProvided")]
    public string? ServicesProvided { get; init; }

    /// <summary>Whether this vendor provides mobile service.</summary>
    [JsonPropertyName("isMobile")]
    public bool? IsMobile { get; init; }

    /// <summary>Whether this vendor location is preferred.</summary>
    [JsonPropertyName("isPreferred")]
    public bool? IsPreferred { get; init; }

    /// <summary>Default hourly labor rate.</summary>
    [JsonPropertyName("defaultLaborRatePerHour")]
    public MaintenanceMoney? DefaultLaborRatePerHour { get; init; }

    /// <summary>Assets this vendor can service.</summary>
    [JsonPropertyName("assetAttributeSelections")]
    public IReadOnlyList<VendorAssetAttributeSelection>? AssetAttributeSelections { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/maintenance/vendors</c> (<c>createVendor</c>, beta).
/// Mirrors the spec's <c>EntityVendorsServiceCreateVendorRequestBody</c>.
/// </summary>
public sealed record CreateMaintenanceVendorRequest
{
    /// <summary>Name of the vendor. Spec REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>User-defined identifier for the vendor.</summary>
    [JsonPropertyName("vendorId")]
    public string? VendorId { get; init; }

    /// <summary>Customer-supplied external identifiers for the vendor.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalIdInput>? ExternalIds { get; init; }

    /// <summary>
    /// Own lifecycle status: <c>active</c> (the default) or <c>inactive</c>. <c>unknown</c>
    /// is read-only.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Vendor group ID.</summary>
    [JsonPropertyName("vendorGroupId")]
    public string? VendorGroupId { get; init; }

    /// <summary>Address of the vendor.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>Linked place identifier for the vendor address.</summary>
    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    /// <summary>People to contact at the vendor.</summary>
    [JsonPropertyName("contacts")]
    public IReadOnlyList<MaintenanceVendorContactInput>? Contacts { get; init; }

    /// <summary>Email addresses for the vendor.</summary>
    [JsonPropertyName("emailAddresses")]
    public IReadOnlyList<string>? EmailAddresses { get; init; }

    /// <summary>Phone numbers for the vendor.</summary>
    [JsonPropertyName("phoneNumbers")]
    public IReadOnlyList<string>? PhoneNumbers { get; init; }

    /// <summary>Additional notes about the vendor.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Free-text AP/ERP payee identifier for the vendor.</summary>
    [JsonPropertyName("payeeId")]
    public string? PayeeId { get; init; }

    /// <summary>Description of services provided by the vendor.</summary>
    [JsonPropertyName("servicesProvided")]
    public string? ServicesProvided { get; init; }

    /// <summary>Whether this vendor provides mobile service. When unset, the group or system default applies.</summary>
    [JsonPropertyName("isMobile")]
    public bool? IsMobile { get; init; }

    /// <summary>Whether this vendor location is preferred. When unset, the group or system default applies.</summary>
    [JsonPropertyName("isPreferred")]
    public bool? IsPreferred { get; init; }

    /// <summary>Default hourly labor rate.</summary>
    [JsonPropertyName("defaultLaborRatePerHour")]
    public MaintenanceMoneyInput? DefaultLaborRatePerHour { get; init; }

    /// <summary>Assets this vendor can service. An empty list replaces inherited selections.</summary>
    [JsonPropertyName("assetAttributeSelections")]
    public IReadOnlyList<VendorAssetAttributeSelectionInput>? AssetAttributeSelections { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /fleet/maintenance/vendors</c> (<c>updateVendor</c>, beta).
/// Mirrors the spec's <c>EntityVendorsServiceUpdateVendorRequestBody</c>, which — unlike the
/// create body — marks nothing required. The vendor is identified by the <c>id</c> query
/// parameter.
/// </summary>
public sealed record UpdateMaintenanceVendorRequest
{
    /// <summary>Name of the vendor.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>User-defined identifier for the vendor.</summary>
    [JsonPropertyName("vendorId")]
    public string? VendorId { get; init; }

    /// <summary>Customer-supplied external identifiers for the vendor.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalIdInput>? ExternalIds { get; init; }

    /// <summary>Own lifecycle status: <c>active</c> or <c>inactive</c>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Vendor group ID.</summary>
    [JsonPropertyName("vendorGroupId")]
    public string? VendorGroupId { get; init; }

    /// <summary>Address of the vendor.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>Linked place identifier for the vendor address.</summary>
    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    /// <summary>People to contact at the vendor.</summary>
    [JsonPropertyName("contacts")]
    public IReadOnlyList<MaintenanceVendorContactInput>? Contacts { get; init; }

    /// <summary>Email addresses for the vendor.</summary>
    [JsonPropertyName("emailAddresses")]
    public IReadOnlyList<string>? EmailAddresses { get; init; }

    /// <summary>Phone numbers for the vendor.</summary>
    [JsonPropertyName("phoneNumbers")]
    public IReadOnlyList<string>? PhoneNumbers { get; init; }

    /// <summary>Additional notes about the vendor.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Free-text AP/ERP payee identifier for the vendor.</summary>
    [JsonPropertyName("payeeId")]
    public string? PayeeId { get; init; }

    /// <summary>Description of services provided by the vendor.</summary>
    [JsonPropertyName("servicesProvided")]
    public string? ServicesProvided { get; init; }

    /// <summary>Whether this vendor provides mobile service.</summary>
    [JsonPropertyName("isMobile")]
    public bool? IsMobile { get; init; }

    /// <summary>Whether this vendor location is preferred.</summary>
    [JsonPropertyName("isPreferred")]
    public bool? IsPreferred { get; init; }

    /// <summary>Default hourly labor rate.</summary>
    [JsonPropertyName("defaultLaborRatePerHour")]
    public MaintenanceMoneyInput? DefaultLaborRatePerHour { get; init; }

    /// <summary>Assets this vendor can service. An empty list replaces inherited selections.</summary>
    [JsonPropertyName("assetAttributeSelections")]
    public IReadOnlyList<VendorAssetAttributeSelectionInput>? AssetAttributeSelections { get; init; }
}

/// <summary>
/// A vendor group (vendor profile): defaults inherited by the vendor locations that belong to
/// it. Mirrors the spec's <c>EntityListVendorGroupsTypeResponseBody</c> and its byte-identical
/// create/update twins, so one record serves <c>GET</c>, <c>POST</c> and <c>PATCH</c>
/// <c>/fleet/maintenance/vendor-groups</c>.
/// </summary>
public sealed record MaintenanceVendorGroup
{
    /// <summary>Unique identifier for the vendor group.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vendor group.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Own lifecycle status: <c>active</c>, <c>inactive</c> or <c>unknown</c>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>External identifiers belonging to this vendor group.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalId>? ExternalIds { get; init; }

    /// <summary>Whether vendor locations inherit mobile service as their default.</summary>
    [JsonPropertyName("isMobile")]
    public bool? IsMobile { get; init; }

    /// <summary>Default preferred status inherited by vendor locations.</summary>
    [JsonPropertyName("isPreferred")]
    public bool? IsPreferred { get; init; }

    /// <summary>Default hourly labor rate inherited by vendor locations.</summary>
    [JsonPropertyName("defaultLaborRatePerHour")]
    public MaintenanceMoney? DefaultLaborRatePerHour { get; init; }

    /// <summary>Asset-attribute selections inherited by vendor locations.</summary>
    [JsonPropertyName("assetAttributeSelections")]
    public IReadOnlyList<VendorAssetAttributeSelection>? AssetAttributeSelections { get; init; }

    /// <summary>The group's primary corporate contact.</summary>
    [JsonPropertyName("primaryCorporateContact")]
    public MaintenanceVendorContact? PrimaryCorporateContact { get; init; }

    /// <summary>When the vendor group was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>When the vendor group was last updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// A vendor group's primary corporate contact, supplied on a request body. Mirrors the spec's
/// <c>...VendorGroupPrimaryCorporateContactInputTypeRequestBody</c> variants, which — unlike
/// <see cref="MaintenanceVendorContactInput"/> — mark nothing required.
/// </summary>
public sealed record MaintenanceVendorGroupContactInput
{
    /// <summary>Name of the contact.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Email address of the contact.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>Phone number of the contact.</summary>
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/maintenance/vendor-groups</c> (<c>createVendorGroup</c>,
/// beta). Mirrors the spec's <c>EntityVendorProfilesServiceCreateVendorGroupRequestBody</c>.
/// </summary>
public sealed record CreateMaintenanceVendorGroupRequest
{
    /// <summary>Name of the vendor group. Spec REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Own lifecycle status: <c>active</c> (the default) or <c>inactive</c>. <c>unknown</c>
    /// is read-only.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>External identifiers for the vendor group.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalIdInput>? ExternalIds { get; init; }

    /// <summary>Whether vendor locations inherit mobile service as their default.</summary>
    [JsonPropertyName("isMobile")]
    public bool? IsMobile { get; init; }

    /// <summary>Default preferred status inherited by vendor locations.</summary>
    [JsonPropertyName("isPreferred")]
    public bool? IsPreferred { get; init; }

    /// <summary>Default hourly labor rate inherited by vendor locations.</summary>
    [JsonPropertyName("defaultLaborRatePerHour")]
    public MaintenanceMoneyInput? DefaultLaborRatePerHour { get; init; }

    /// <summary>Asset-attribute selections inherited by vendor locations.</summary>
    [JsonPropertyName("assetAttributeSelections")]
    public IReadOnlyList<VendorAssetAttributeSelectionInput>? AssetAttributeSelections { get; init; }

    /// <summary>The group's primary corporate contact.</summary>
    [JsonPropertyName("primaryCorporateContact")]
    public MaintenanceVendorGroupContactInput? PrimaryCorporateContact { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /fleet/maintenance/vendor-groups</c> (<c>updateVendorGroup</c>,
/// beta). Mirrors the spec's <c>EntityVendorProfilesServiceUpdateVendorGroupRequestBody</c>,
/// which marks nothing required. The group is identified by the <c>id</c> query parameter.
/// </summary>
public sealed record UpdateMaintenanceVendorGroupRequest
{
    /// <summary>Name of the vendor group.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Own lifecycle status: <c>active</c> or <c>inactive</c>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>External identifiers for the vendor group.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<MaintenanceExternalIdInput>? ExternalIds { get; init; }

    /// <summary>Whether vendor locations inherit mobile service as their default.</summary>
    [JsonPropertyName("isMobile")]
    public bool? IsMobile { get; init; }

    /// <summary>Default preferred status inherited by vendor locations.</summary>
    [JsonPropertyName("isPreferred")]
    public bool? IsPreferred { get; init; }

    /// <summary>Default hourly labor rate inherited by vendor locations.</summary>
    [JsonPropertyName("defaultLaborRatePerHour")]
    public MaintenanceMoneyInput? DefaultLaborRatePerHour { get; init; }

    /// <summary>Asset-attribute selections inherited by vendor locations.</summary>
    [JsonPropertyName("assetAttributeSelections")]
    public IReadOnlyList<VendorAssetAttributeSelectionInput>? AssetAttributeSelections { get; init; }

    /// <summary>The group's primary corporate contact.</summary>
    [JsonPropertyName("primaryCorporateContact")]
    public MaintenanceVendorGroupContactInput? PrimaryCorporateContact { get; init; }
}
