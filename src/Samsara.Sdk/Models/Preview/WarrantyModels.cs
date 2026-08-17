namespace Samsara.Sdk.Models.Preview;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Maintenance;

/// <summary>
/// A maintenance warranty (preview). Mirrors the spec's
/// <c>EntityListWarrantiesTypeResponseBody</c> and its byte-identical
/// create/update twins, so one record serves <c>GET</c>, <c>POST</c> and
/// <c>PATCH</c> <c>/preview/maintenance/warranties</c>.
/// </summary>
/// <remarks>
/// <para>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </para>
/// <para>
/// Money amounts and <c>{ id }</c> entity references reuse
/// <see cref="MaintenanceMoney"/>, <see cref="MaintenanceMoneyInput"/> and
/// <see cref="MaintenanceEntityRef"/> from the maintenance domain: the warranty
/// schemas declare structurally identical shapes.
/// </para>
/// </remarks>
public sealed record Warranty
{
    /// <summary>Samsara ID for the warranty.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the warranty.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Description of the warranty.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Type of warranty, for example <c>manufacturer</c>, <c>extended</c>,
    /// <c>other</c> or <c>unknown</c>.
    /// </summary>
    [JsonPropertyName("warrantyType")]
    public string? WarrantyType { get; init; }

    /// <summary>The vendor that provides this warranty.</summary>
    [JsonPropertyName("vendor")]
    public MaintenanceEntityRef? Vendor { get; init; }

    /// <summary>The primary coverage group on this warranty.</summary>
    [JsonPropertyName("baseCoverage")]
    public WarrantyCoverage? BaseCoverage { get; init; }

    /// <summary>Additional coverage groups defined on this warranty.</summary>
    [JsonPropertyName("coverages")]
    public IReadOnlyList<WarrantyCoverage>? Coverages { get; init; }

    /// <summary>Warranty length in days. Mutually exclusive with duration in months.</summary>
    [JsonPropertyName("durationDays")]
    public long? DurationDays { get; init; }

    /// <summary>Warranty length in months. Mutually exclusive with duration in days.</summary>
    [JsonPropertyName("durationMonths")]
    public long? DurationMonths { get; init; }

    /// <summary>Warranty length by engine hours since the warranty start.</summary>
    [JsonPropertyName("engineDurationHours")]
    public long? EngineDurationHours { get; init; }

    /// <summary>
    /// Warranty length by distance travelled since the warranty start, in meters.
    /// </summary>
    [JsonPropertyName("odometerDistanceMeters")]
    public long? OdometerDistanceMeters { get; init; }

    /// <summary>Customer-supplied external identifiers for the warranty.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<WarrantyExternalId>? ExternalIds { get; init; }

    /// <summary>When the warranty was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>When the warranty was last updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// A coverage group on a <see cref="Warranty"/>. Mirrors the spec's
/// <c>...WarrantyWarrantyCoverageTypeResponseBody</c> variants.
/// </summary>
public sealed record WarrantyCoverage
{
    /// <summary>Name of the coverage group.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Description of what this coverage group covers.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Free-form notes about this coverage group.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Items explicitly covered by this coverage group.</summary>
    [JsonPropertyName("inclusions")]
    public IReadOnlyList<WarrantyCoverageItem>? Inclusions { get; init; }

    /// <summary>Items explicitly excluded from this coverage group.</summary>
    [JsonPropertyName("exclusions")]
    public IReadOnlyList<WarrantyCoverageItem>? Exclusions { get; init; }

    /// <summary>Coverage length in days. Mutually exclusive with duration in months.</summary>
    [JsonPropertyName("durationDays")]
    public long? DurationDays { get; init; }

    /// <summary>Coverage length in months. Mutually exclusive with duration in days.</summary>
    [JsonPropertyName("durationMonths")]
    public long? DurationMonths { get; init; }

    /// <summary>Coverage length by engine hours since the coverage start.</summary>
    [JsonPropertyName("engineDurationHours")]
    public long? EngineDurationHours { get; init; }

    /// <summary>
    /// Coverage length by distance travelled since the coverage start, in meters.
    /// </summary>
    [JsonPropertyName("odometerDistanceMeters")]
    public long? OdometerDistanceMeters { get; init; }

    /// <summary>
    /// When true, the coverage never expires by mileage and the odometer
    /// distance is ignored.
    /// </summary>
    [JsonPropertyName("isOdometerDistanceUnlimited")]
    public bool? IsOdometerDistanceUnlimited { get; init; }

    /// <summary>Engine hours at the start of this coverage.</summary>
    [JsonPropertyName("startEngineHours")]
    public long? StartEngineHours { get; init; }

    /// <summary>Odometer reading at the start of this coverage, in meters.</summary>
    [JsonPropertyName("startOdometerMeters")]
    public long? StartOdometerMeters { get; init; }

    /// <summary>Start time of this coverage.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }
}

/// <summary>
/// An item covered by (or excluded from) a <see cref="WarrantyCoverage"/>.
/// Mirrors the spec's <c>...WarrantyWarrantyCoverageItemTypeResponseBody</c>.
/// </summary>
public sealed record WarrantyCoverageItem
{
    /// <summary>
    /// Identifier of the covered item. For a service task item this is the
    /// service task ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Kind of covered item, indicating how the ID should be interpreted (VMRS
    /// code or entity ID).
    /// </summary>
    [JsonPropertyName("itemType")]
    public string? ItemType { get; init; }

    /// <summary>Dotted VMRS code path (e.g. <c>034-005-001</c>).</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }
}

/// <summary>
/// A customer-supplied external identifier on a warranty or warranty claim.
/// Mirrors the spec's <c>...WarrantyExternalIdTypeResponseBody</c> and
/// <c>...WarrantyClaimExternalIdTypeResponseBody</c> — structurally identical.
/// </summary>
public sealed record WarrantyExternalId
{
    /// <summary>Name of the external ID namespace, for example a source system identifier.</summary>
    [JsonPropertyName("key")]
    public string? Key { get; init; }

    /// <summary>The external ID value within the given key's namespace.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// A customer-supplied external identifier on a warranty or warranty-claim
/// request body. Mirrors the spec's <c>...ExternalIdInputTypeRequestBody</c>
/// variants, which — unlike their response twins — mark both members REQUIRED.
/// </summary>
public sealed record WarrantyExternalIdInput
{
    /// <summary>Name of the external ID namespace. Spec REQUIRED.</summary>
    [JsonPropertyName("key")]
    public required string Key { get; init; }

    /// <summary>The external ID value within the given key's namespace. Spec REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }
}

/// <summary>
/// An item covered by (or excluded from) a coverage group on a warranty request
/// body. Mirrors the spec's <c>...WarrantyCoverageItemInputTypeRequestBody</c>,
/// which marks <c>itemType</c> REQUIRED.
/// </summary>
public sealed record WarrantyCoverageItemInput
{
    /// <summary>
    /// Kind of covered item, indicating how the ID should be interpreted (VMRS
    /// code or entity ID). Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("itemType")]
    public required string ItemType { get; init; }

    /// <summary>
    /// Identifier of the covered item. For a service task item this is the
    /// service task ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Dotted VMRS code path (e.g. <c>034-005-001</c>).</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }
}

/// <summary>
/// A coverage group supplied on a warranty request body. Mirrors the spec's
/// <c>...WarrantyCoverageInputTypeRequestBody</c>, which marks <c>name</c>
/// REQUIRED.
/// </summary>
public sealed record WarrantyCoverageInput
{
    /// <summary>Name of the coverage group. Spec REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Description of what this coverage group covers.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Free-form notes about this coverage group.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Items explicitly covered by this coverage group.</summary>
    [JsonPropertyName("inclusions")]
    public IReadOnlyList<WarrantyCoverageItemInput>? Inclusions { get; init; }

    /// <summary>Items explicitly excluded from this coverage group.</summary>
    [JsonPropertyName("exclusions")]
    public IReadOnlyList<WarrantyCoverageItemInput>? Exclusions { get; init; }

    /// <summary>Coverage length in days. Mutually exclusive with duration in months.</summary>
    [JsonPropertyName("durationDays")]
    public long? DurationDays { get; init; }

    /// <summary>Coverage length in months. Mutually exclusive with duration in days.</summary>
    [JsonPropertyName("durationMonths")]
    public long? DurationMonths { get; init; }

    /// <summary>Coverage length by engine hours since the coverage start.</summary>
    [JsonPropertyName("engineDurationHours")]
    public long? EngineDurationHours { get; init; }

    /// <summary>
    /// Coverage length by distance travelled since the coverage start, in meters.
    /// </summary>
    [JsonPropertyName("odometerDistanceMeters")]
    public long? OdometerDistanceMeters { get; init; }

    /// <summary>
    /// When true, the coverage never expires by mileage and the odometer
    /// distance is ignored.
    /// </summary>
    [JsonPropertyName("isOdometerDistanceUnlimited")]
    public bool? IsOdometerDistanceUnlimited { get; init; }

    /// <summary>Engine hours at the start of this coverage.</summary>
    [JsonPropertyName("startEngineHours")]
    public long? StartEngineHours { get; init; }

    /// <summary>Odometer reading at the start of this coverage, in meters.</summary>
    [JsonPropertyName("startOdometerMeters")]
    public long? StartOdometerMeters { get; init; }

    /// <summary>Start time of this coverage.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }
}

/// <summary>
/// Request body for <c>POST /preview/maintenance/warranties</c>
/// (<c>createWarranty</c>). Mirrors the spec's
/// <c>EntityWarrantiesServiceCreateWarrantyRequestBody</c>.
/// </summary>
public sealed record CreateWarrantyRequest
{
    /// <summary>Name of the warranty. Spec REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Description of the warranty.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Type of warranty, for example <c>manufacturer</c>, <c>extended</c> or
    /// <c>other</c>.
    /// </summary>
    [JsonPropertyName("warrantyType")]
    public string? WarrantyType { get; init; }

    /// <summary>ID of the vendor that provides this warranty.</summary>
    [JsonPropertyName("vendorId")]
    public string? VendorId { get; init; }

    /// <summary>The primary coverage group on this warranty.</summary>
    [JsonPropertyName("baseCoverage")]
    public WarrantyCoverageInput? BaseCoverage { get; init; }

    /// <summary>Additional coverage groups defined on this warranty.</summary>
    [JsonPropertyName("coverages")]
    public IReadOnlyList<WarrantyCoverageInput>? Coverages { get; init; }

    /// <summary>Warranty length in days. Mutually exclusive with duration in months.</summary>
    [JsonPropertyName("durationDays")]
    public long? DurationDays { get; init; }

    /// <summary>Warranty length in months. Mutually exclusive with duration in days.</summary>
    [JsonPropertyName("durationMonths")]
    public long? DurationMonths { get; init; }

    /// <summary>Warranty length by engine hours since the warranty start.</summary>
    [JsonPropertyName("engineDurationHours")]
    public long? EngineDurationHours { get; init; }

    /// <summary>
    /// Warranty length by distance travelled since the warranty start, in meters.
    /// </summary>
    [JsonPropertyName("odometerDistanceMeters")]
    public long? OdometerDistanceMeters { get; init; }

    /// <summary>Customer-supplied external identifiers for the warranty.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<WarrantyExternalIdInput>? ExternalIds { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /preview/maintenance/warranties</c>
/// (<c>updateWarranty</c>). Mirrors the spec's
/// <c>EntityWarrantiesServiceUpdateWarrantyRequestBody</c>, which — unlike the
/// create body — marks nothing required.
/// </summary>
public sealed record UpdateWarrantyRequest
{
    /// <summary>Name of the warranty.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Description of the warranty.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Type of warranty, for example <c>manufacturer</c>, <c>extended</c> or
    /// <c>other</c>.
    /// </summary>
    [JsonPropertyName("warrantyType")]
    public string? WarrantyType { get; init; }

    /// <summary>ID of the vendor that provides this warranty.</summary>
    [JsonPropertyName("vendorId")]
    public string? VendorId { get; init; }

    /// <summary>The primary coverage group on this warranty.</summary>
    [JsonPropertyName("baseCoverage")]
    public WarrantyCoverageInput? BaseCoverage { get; init; }

    /// <summary>Additional coverage groups defined on this warranty.</summary>
    [JsonPropertyName("coverages")]
    public IReadOnlyList<WarrantyCoverageInput>? Coverages { get; init; }

    /// <summary>Warranty length in days. Mutually exclusive with duration in months.</summary>
    [JsonPropertyName("durationDays")]
    public long? DurationDays { get; init; }

    /// <summary>Warranty length in months. Mutually exclusive with duration in days.</summary>
    [JsonPropertyName("durationMonths")]
    public long? DurationMonths { get; init; }

    /// <summary>Warranty length by engine hours since the warranty start.</summary>
    [JsonPropertyName("engineDurationHours")]
    public long? EngineDurationHours { get; init; }

    /// <summary>
    /// Warranty length by distance travelled since the warranty start, in meters.
    /// </summary>
    [JsonPropertyName("odometerDistanceMeters")]
    public long? OdometerDistanceMeters { get; init; }

    /// <summary>Customer-supplied external identifiers for the warranty.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<WarrantyExternalIdInput>? ExternalIds { get; init; }
}

/// <summary>
/// One asset covered by a warranty, supplied on
/// <c>POST /preview/maintenance/warranties/assets/replace</c>. Mirrors the
/// spec's
/// <c>EntityReplaceWarrantyAssetAssignmentsWarrantyAssetAssignmentInputTypeRequestBody</c>.
/// </summary>
public sealed record WarrantyAssetAssignmentInput
{
    /// <summary>ID of the asset to assign. Spec REQUIRED.</summary>
    [JsonPropertyName("assetId")]
    public required string AssetId { get; init; }

    /// <summary>When coverage starts for this asset.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>Asset engine hours at coverage start.</summary>
    [JsonPropertyName("startEngineHours")]
    public long? StartEngineHours { get; init; }

    /// <summary>Asset odometer reading at coverage start, in meters.</summary>
    [JsonPropertyName("startOdometerMeters")]
    public long? StartOdometerMeters { get; init; }
}

/// <summary>
/// Request body for <c>POST /preview/maintenance/warranties/assets/replace</c>
/// (<c>replaceWarrantyAssetAssignments</c>). The list replaces the warranty's
/// entire asset set.
/// </summary>
public sealed record ReplaceWarrantyAssetAssignmentsRequest
{
    /// <summary>The full desired asset set for the warranty.</summary>
    [JsonPropertyName("assets")]
    public IReadOnlyList<WarrantyAssetAssignmentInput>? Assets { get; init; }
}

/// <summary>
/// One asset covered by a warranty. Mirrors the spec's
/// <c>EntityReplaceWarrantyAssetAssignmentsWarrantyAssetAssignmentTypeResponseBody</c>.
/// </summary>
public sealed record WarrantyAssetAssignment
{
    /// <summary>
    /// Synthetic identifier for the assignment, formatted as
    /// <c>warrantyId:assetId</c>.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>ID of the warranty the asset is assigned to.</summary>
    [JsonPropertyName("warrantyId")]
    public string? WarrantyId { get; init; }

    /// <summary>ID of the asset covered by the warranty.</summary>
    [JsonPropertyName("assetId")]
    public string? AssetId { get; init; }

    /// <summary>When coverage starts for this asset.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>Asset engine hours at coverage start.</summary>
    [JsonPropertyName("startEngineHours")]
    public long? StartEngineHours { get; init; }

    /// <summary>Asset odometer reading at coverage start, in meters.</summary>
    [JsonPropertyName("startOdometerMeters")]
    public long? StartOdometerMeters { get; init; }

    /// <summary>When the assignment was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>When the assignment was last updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// The payload of <c>POST /preview/maintenance/warranties/assets/replace</c>.
/// Mirrors the spec's
/// <c>ReplaceWarrantyAssetAssignmentsResponseObjectTypeResponseBody</c>, which
/// nests a second <c>data</c> array inside the standard <c>{ data: ... }</c>
/// envelope.
/// </summary>
public sealed record WarrantyAssetAssignmentReplaceResult
{
    /// <summary>The resulting asset assignments after the replace.</summary>
    [JsonPropertyName("data")]
    public IReadOnlyList<WarrantyAssetAssignment>? Data { get; init; }
}

/// <summary>
/// A warranty claim (preview). Mirrors the spec's
/// <c>EntityListWarrantyClaimsTypeResponseBody</c> and its byte-identical
/// create/update twins, so one record serves <c>GET</c>, <c>POST</c> and
/// <c>PATCH</c> <c>/preview/maintenance/warranty-claims</c>.
/// </summary>
public sealed record WarrantyClaim
{
    /// <summary>Samsara ID for the warranty claim.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The asset the claim is filed for.</summary>
    [JsonPropertyName("asset")]
    public MaintenanceEntityRef? Asset { get; init; }

    /// <summary>The warranty this claim is filed against.</summary>
    [JsonPropertyName("linkedWarranty")]
    public MaintenanceEntityRef? LinkedWarranty { get; init; }

    /// <summary>The vendor handling the claim.</summary>
    [JsonPropertyName("warrantyVendor")]
    public MaintenanceEntityRef? WarrantyVendor { get; init; }

    /// <summary>Current status of the claim.</summary>
    [JsonPropertyName("claimStatus")]
    public string? ClaimStatus { get; init; }

    /// <summary>The concern of the 3 Cs — what was reported.</summary>
    [JsonPropertyName("concern")]
    public string? Concern { get; init; }

    /// <summary>The cause of the 3 Cs — the root cause found.</summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; init; }

    /// <summary>The correction of the 3 Cs — the work performed.</summary>
    [JsonPropertyName("correction")]
    public string? Correction { get; init; }

    /// <summary>Engine hours at the time of repair.</summary>
    [JsonPropertyName("claimEngineHours")]
    public long? ClaimEngineHours { get; init; }

    /// <summary>Asset odometer reading at the time of repair, in meters.</summary>
    [JsonPropertyName("claimOdometerMeters")]
    public long? ClaimOdometerMeters { get; init; }

    /// <summary>IDs of the component instances covered by the claim.</summary>
    [JsonPropertyName("componentInstanceIds")]
    public IReadOnlyList<string>? ComponentInstanceIds { get; init; }

    /// <summary>IDs of the work orders linked to the claim.</summary>
    [JsonPropertyName("linkedWorkOrderIds")]
    public IReadOnlyList<string>? LinkedWorkOrderIds { get; init; }

    /// <summary>IDs of media items attached to the claim.</summary>
    [JsonPropertyName("mediaItemIds")]
    public IReadOnlyList<string>? MediaItemIds { get; init; }

    /// <summary>Labor being claimed.</summary>
    [JsonPropertyName("labor")]
    public IReadOnlyList<WarrantyClaimLabor>? Labor { get; init; }

    /// <summary>Parts being claimed.</summary>
    [JsonPropertyName("parts")]
    public IReadOnlyList<WarrantyClaimPart>? Parts { get; init; }

    /// <summary>Costs on the claim not attributable to a labor or part line.</summary>
    [JsonPropertyName("otherCost")]
    public MaintenanceMoney? OtherCost { get; init; }

    /// <summary>Reimbursement amounts, optionally linked to a work order.</summary>
    [JsonPropertyName("reimbursements")]
    public IReadOnlyList<WarrantyClaimReimbursement>? Reimbursements { get; init; }

    /// <summary>Audit trail of status changes. Server-managed and read-only.</summary>
    [JsonPropertyName("statusHistory")]
    public IReadOnlyList<WarrantyClaimStatusHistoryEntry>? StatusHistory { get; init; }

    /// <summary>Customer-supplied external identifiers for the warranty claim.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<WarrantyExternalId>? ExternalIds { get; init; }

    /// <summary>When the repair was completed.</summary>
    [JsonPropertyName("repairCompletedAtTime")]
    public string? RepairCompletedAtTime { get; init; }

    /// <summary>When the claim was submitted to the vendor.</summary>
    [JsonPropertyName("submittedAtTime")]
    public string? SubmittedAtTime { get; init; }

    /// <summary>When the claim was resolved.</summary>
    [JsonPropertyName("resolutionAtTime")]
    public string? ResolutionAtTime { get; init; }

    /// <summary>When reimbursement was received.</summary>
    [JsonPropertyName("reimbursedAtTime")]
    public string? ReimbursedAtTime { get; init; }

    /// <summary>When the claim was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>When the claim was last updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// A labor line on a <see cref="WarrantyClaim"/>. Mirrors the spec's
/// <c>...WarrantyClaimWarrantyClaimLaborTypeResponseBody</c>.
/// </summary>
public sealed record WarrantyClaimLabor
{
    /// <summary>Free-text labor description.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Cost of the labor line.</summary>
    [JsonPropertyName("cost")]
    public MaintenanceMoney? Cost { get; init; }

    /// <summary>Service task the labor belongs to.</summary>
    [JsonPropertyName("serviceTaskId")]
    public string? ServiceTaskId { get; init; }

    /// <summary>Work order the labor cost originated from.</summary>
    [JsonPropertyName("sourceWorkOrderId")]
    public string? SourceWorkOrderId { get; init; }

    /// <summary>Dotted VMRS code path (e.g. <c>034-005-001</c>) for this labor line.</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }
}

/// <summary>
/// A part line on a <see cref="WarrantyClaim"/>. Mirrors the spec's
/// <c>...WarrantyClaimWarrantyClaimPartTypeResponseBody</c>.
/// </summary>
public sealed record WarrantyClaimPart
{
    /// <summary>ID of the part definition being claimed.</summary>
    [JsonPropertyName("partDefinitionId")]
    public string? PartDefinitionId { get; init; }

    /// <summary>Specific part-instance ID.</summary>
    [JsonPropertyName("partId")]
    public string? PartId { get; init; }

    /// <summary>Free-text part description.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Quantity claimed.</summary>
    [JsonPropertyName("quantity")]
    public long? Quantity { get; init; }

    /// <summary>Cost of the part line.</summary>
    [JsonPropertyName("cost")]
    public MaintenanceMoney? Cost { get; init; }

    /// <summary>Service task the part belongs to.</summary>
    [JsonPropertyName("serviceTaskId")]
    public string? ServiceTaskId { get; init; }

    /// <summary>Work order the part cost originated from.</summary>
    [JsonPropertyName("sourceWorkOrderId")]
    public string? SourceWorkOrderId { get; init; }

    /// <summary>Dotted VMRS code path (e.g. <c>034-005-001</c>) for this part line.</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }
}

/// <summary>
/// A reimbursement on a <see cref="WarrantyClaim"/>. Mirrors the spec's
/// <c>...WarrantyClaimClaimReimbursementTypeResponseBody</c>.
/// </summary>
public sealed record WarrantyClaimReimbursement
{
    /// <summary>The reimbursed amount.</summary>
    [JsonPropertyName("reimbursement")]
    public MaintenanceMoney? Reimbursement { get; init; }

    /// <summary>Work order the reimbursement is applied to.</summary>
    [JsonPropertyName("workOrderId")]
    public string? WorkOrderId { get; init; }
}

/// <summary>
/// One status transition in a warranty claim's audit trail. Mirrors the spec's
/// <c>...WarrantyClaimWarrantyClaimStatusHistoryTypeResponseBody</c>.
/// </summary>
public sealed record WarrantyClaimStatusHistoryEntry
{
    /// <summary>The status the claim moved into.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>When the transition occurred.</summary>
    [JsonPropertyName("happenedAtTime")]
    public string? HappenedAtTime { get; init; }

    /// <summary>ID of the user who made the change.</summary>
    [JsonPropertyName("userId")]
    public string? UserId { get; init; }
}

/// <summary>
/// A labor line supplied on a warranty-claim request body. Mirrors the spec's
/// <c>...WarrantyClaimLaborInputTypeRequestBody</c>, which marks nothing
/// required but takes the <c>*Input</c> money shape.
/// </summary>
public sealed record WarrantyClaimLaborInput
{
    /// <summary>Free-text labor description.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Cost of the labor line.</summary>
    [JsonPropertyName("cost")]
    public MaintenanceMoneyInput? Cost { get; init; }

    /// <summary>Service task the labor belongs to.</summary>
    [JsonPropertyName("serviceTaskId")]
    public string? ServiceTaskId { get; init; }

    /// <summary>Work order the labor cost originated from.</summary>
    [JsonPropertyName("sourceWorkOrderId")]
    public string? SourceWorkOrderId { get; init; }

    /// <summary>Dotted VMRS code path (e.g. <c>034-005-001</c>) for this labor line.</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }
}

/// <summary>
/// A part line supplied on a warranty-claim request body. Mirrors the spec's
/// <c>...WarrantyClaimPartInputTypeRequestBody</c>, which marks
/// <c>partDefinitionId</c> REQUIRED.
/// </summary>
public sealed record WarrantyClaimPartInput
{
    /// <summary>ID of the part definition being claimed. Spec REQUIRED.</summary>
    [JsonPropertyName("partDefinitionId")]
    public required string PartDefinitionId { get; init; }

    /// <summary>Specific part-instance ID.</summary>
    [JsonPropertyName("partId")]
    public string? PartId { get; init; }

    /// <summary>Free-text part description.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Quantity claimed.</summary>
    [JsonPropertyName("quantity")]
    public long? Quantity { get; init; }

    /// <summary>Cost of the part line.</summary>
    [JsonPropertyName("cost")]
    public MaintenanceMoneyInput? Cost { get; init; }

    /// <summary>Service task the part belongs to.</summary>
    [JsonPropertyName("serviceTaskId")]
    public string? ServiceTaskId { get; init; }

    /// <summary>Work order the part cost originated from.</summary>
    [JsonPropertyName("sourceWorkOrderId")]
    public string? SourceWorkOrderId { get; init; }

    /// <summary>Dotted VMRS code path (e.g. <c>034-005-001</c>) for this part line.</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }
}

/// <summary>
/// A reimbursement supplied on a warranty-claim request body. Mirrors the spec's
/// <c>...ClaimReimbursementInputTypeRequestBody</c>.
/// </summary>
public sealed record WarrantyClaimReimbursementInput
{
    /// <summary>The reimbursed amount.</summary>
    [JsonPropertyName("reimbursement")]
    public MaintenanceMoneyInput? Reimbursement { get; init; }

    /// <summary>Work order the reimbursement is applied to.</summary>
    [JsonPropertyName("workOrderId")]
    public string? WorkOrderId { get; init; }
}

/// <summary>
/// Request body for <c>POST /preview/maintenance/warranty-claims</c>
/// (<c>createWarrantyClaim</c>). Mirrors the spec's
/// <c>EntityWarrantyClaimsServiceCreateWarrantyClaimRequestBody</c>.
/// </summary>
public sealed record CreateWarrantyClaimRequest
{
    /// <summary>
    /// ID of the asset the claim is filed for. Immutable once set. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("assetId")]
    public required string AssetId { get; init; }

    /// <summary>ID of the warranty this claim is filed against.</summary>
    [JsonPropertyName("linkedWarrantyId")]
    public string? LinkedWarrantyId { get; init; }

    /// <summary>ID of the vendor handling the claim.</summary>
    [JsonPropertyName("warrantyVendorId")]
    public string? WarrantyVendorId { get; init; }

    /// <summary>Current status of the claim.</summary>
    [JsonPropertyName("claimStatus")]
    public string? ClaimStatus { get; init; }

    /// <summary>The concern of the 3 Cs — what was reported.</summary>
    [JsonPropertyName("concern")]
    public string? Concern { get; init; }

    /// <summary>The cause of the 3 Cs — the root cause found.</summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; init; }

    /// <summary>The correction of the 3 Cs — the work performed.</summary>
    [JsonPropertyName("correction")]
    public string? Correction { get; init; }

    /// <summary>Engine hours at the time of repair.</summary>
    [JsonPropertyName("claimEngineHours")]
    public long? ClaimEngineHours { get; init; }

    /// <summary>Asset odometer reading at the time of repair, in meters.</summary>
    [JsonPropertyName("claimOdometerMeters")]
    public long? ClaimOdometerMeters { get; init; }

    /// <summary>IDs of the component instances covered by the claim.</summary>
    [JsonPropertyName("componentInstanceIds")]
    public IReadOnlyList<string>? ComponentInstanceIds { get; init; }

    /// <summary>IDs of the work orders linked to the claim.</summary>
    [JsonPropertyName("linkedWorkOrderIds")]
    public IReadOnlyList<string>? LinkedWorkOrderIds { get; init; }

    /// <summary>IDs of media items attached to the claim.</summary>
    [JsonPropertyName("mediaItemIds")]
    public IReadOnlyList<string>? MediaItemIds { get; init; }

    /// <summary>Labor being claimed.</summary>
    [JsonPropertyName("labor")]
    public IReadOnlyList<WarrantyClaimLaborInput>? Labor { get; init; }

    /// <summary>Parts being claimed.</summary>
    [JsonPropertyName("parts")]
    public IReadOnlyList<WarrantyClaimPartInput>? Parts { get; init; }

    /// <summary>Costs on the claim not attributable to a labor or part line.</summary>
    [JsonPropertyName("otherCost")]
    public MaintenanceMoneyInput? OtherCost { get; init; }

    /// <summary>Reimbursement amounts, optionally linked to a work order.</summary>
    [JsonPropertyName("reimbursements")]
    public IReadOnlyList<WarrantyClaimReimbursementInput>? Reimbursements { get; init; }

    /// <summary>Customer-supplied external identifiers for the warranty claim.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<WarrantyExternalIdInput>? ExternalIds { get; init; }

    /// <summary>When the repair was completed.</summary>
    [JsonPropertyName("repairCompletedAtTime")]
    public string? RepairCompletedAtTime { get; init; }

    /// <summary>When the claim was submitted to the vendor.</summary>
    [JsonPropertyName("submittedAtTime")]
    public string? SubmittedAtTime { get; init; }

    /// <summary>When the claim was resolved.</summary>
    [JsonPropertyName("resolutionAtTime")]
    public string? ResolutionAtTime { get; init; }

    /// <summary>When reimbursement was received.</summary>
    [JsonPropertyName("reimbursedAtTime")]
    public string? ReimbursedAtTime { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /preview/maintenance/warranty-claims</c>
/// (<c>updateWarrantyClaim</c>). Mirrors the spec's
/// <c>EntityWarrantyClaimsServiceUpdateWarrantyClaimRequestBody</c>, which —
/// unlike the create body — marks nothing required.
/// </summary>
public sealed record UpdateWarrantyClaimRequest
{
    /// <summary>ID of the asset the claim is filed for. Immutable once set.</summary>
    [JsonPropertyName("assetId")]
    public string? AssetId { get; init; }

    /// <summary>ID of the warranty this claim is filed against.</summary>
    [JsonPropertyName("linkedWarrantyId")]
    public string? LinkedWarrantyId { get; init; }

    /// <summary>ID of the vendor handling the claim.</summary>
    [JsonPropertyName("warrantyVendorId")]
    public string? WarrantyVendorId { get; init; }

    /// <summary>Current status of the claim.</summary>
    [JsonPropertyName("claimStatus")]
    public string? ClaimStatus { get; init; }

    /// <summary>The concern of the 3 Cs — what was reported.</summary>
    [JsonPropertyName("concern")]
    public string? Concern { get; init; }

    /// <summary>The cause of the 3 Cs — the root cause found.</summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; init; }

    /// <summary>The correction of the 3 Cs — the work performed.</summary>
    [JsonPropertyName("correction")]
    public string? Correction { get; init; }

    /// <summary>Engine hours at the time of repair.</summary>
    [JsonPropertyName("claimEngineHours")]
    public long? ClaimEngineHours { get; init; }

    /// <summary>Asset odometer reading at the time of repair, in meters.</summary>
    [JsonPropertyName("claimOdometerMeters")]
    public long? ClaimOdometerMeters { get; init; }

    /// <summary>IDs of the component instances covered by the claim.</summary>
    [JsonPropertyName("componentInstanceIds")]
    public IReadOnlyList<string>? ComponentInstanceIds { get; init; }

    /// <summary>IDs of the work orders linked to the claim.</summary>
    [JsonPropertyName("linkedWorkOrderIds")]
    public IReadOnlyList<string>? LinkedWorkOrderIds { get; init; }

    /// <summary>IDs of media items attached to the claim.</summary>
    [JsonPropertyName("mediaItemIds")]
    public IReadOnlyList<string>? MediaItemIds { get; init; }

    /// <summary>Labor being claimed.</summary>
    [JsonPropertyName("labor")]
    public IReadOnlyList<WarrantyClaimLaborInput>? Labor { get; init; }

    /// <summary>Parts being claimed.</summary>
    [JsonPropertyName("parts")]
    public IReadOnlyList<WarrantyClaimPartInput>? Parts { get; init; }

    /// <summary>Costs on the claim not attributable to a labor or part line.</summary>
    [JsonPropertyName("otherCost")]
    public MaintenanceMoneyInput? OtherCost { get; init; }

    /// <summary>Reimbursement amounts, optionally linked to a work order.</summary>
    [JsonPropertyName("reimbursements")]
    public IReadOnlyList<WarrantyClaimReimbursementInput>? Reimbursements { get; init; }

    /// <summary>Customer-supplied external identifiers for the warranty claim.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyList<WarrantyExternalIdInput>? ExternalIds { get; init; }

    /// <summary>When the repair was completed.</summary>
    [JsonPropertyName("repairCompletedAtTime")]
    public string? RepairCompletedAtTime { get; init; }

    /// <summary>When the claim was submitted to the vendor.</summary>
    [JsonPropertyName("submittedAtTime")]
    public string? SubmittedAtTime { get; init; }

    /// <summary>When the claim was resolved.</summary>
    [JsonPropertyName("resolutionAtTime")]
    public string? ResolutionAtTime { get; init; }

    /// <summary>When reimbursement was received.</summary>
    [JsonPropertyName("reimbursedAtTime")]
    public string? ReimbursedAtTime { get; init; }
}
