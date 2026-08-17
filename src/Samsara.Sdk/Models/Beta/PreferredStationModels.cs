namespace Samsara.Sdk.Models.Beta;

using System.Text.Json.Serialization;

/// <summary>
/// A preferred fuel station. Mirrors the spec's
/// <c>PreferredStationObjectResponseBody</c> (the <c>data</c> payload of
/// <c>GET/POST/PATCH /preferred-stations</c> and <c>GET /preferred-stations/{id}</c>).
/// </summary>
/// <remarks>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </remarks>
public sealed record PreferredStation
{
    /// <summary>Samsara-assigned station ID.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Display name of the station. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Postal address of the station.</summary>
    [JsonPropertyName("address")]
    public PreferredStationAddress? Address { get; init; }

    /// <summary>Latitude in WGS84 degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in WGS84 degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>A map of external ids for the station.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Discount overrides per fuel type.</summary>
    [JsonPropertyName("discounts")]
    public IReadOnlyList<PreferredStationDiscount>? Discounts { get; init; }

    /// <summary>Per-fuel-type prices.</summary>
    [JsonPropertyName("prices")]
    public IReadOnlyList<PreferredStationPrice>? Prices { get; init; }
}

/// <summary>
/// Postal address of a preferred station. Mirrors the spec's
/// <c>PreferredStationAddressResponseResponseBody</c>.
/// </summary>
public sealed record PreferredStationAddress
{
    /// <summary>Street address of the station.</summary>
    [JsonPropertyName("line1")]
    public string? Line1 { get; init; }

    /// <summary>City of the station.</summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>State or province code.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Postal or ZIP code.</summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; init; }

    /// <summary>Country code.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }
}

/// <summary>
/// A discount override for one fuel type at a preferred station. Mirrors the
/// spec's <c>PreferredStationDiscountResponseResponseBody</c>.
/// </summary>
public sealed record PreferredStationDiscount
{
    /// <summary>
    /// Fuel type the discount applies to (e.g. <c>gasoline</c>, <c>diesel</c>,
    /// <c>electricity</c>, <c>unknown</c>). Spec-required.
    /// </summary>
    [JsonPropertyName("fuelType")]
    public string? FuelType { get; init; }

    /// <summary>
    /// Discount type: <c>centsPerUnit</c>, <c>percentage</c>, <c>fixedPrice</c> or
    /// <c>unknown</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("discountType")]
    public string? DiscountType { get; init; }

    /// <summary>The monetary discount, for the non-percentage discount types.</summary>
    [JsonPropertyName("discount")]
    public PreferredStationMoney? Discount { get; init; }

    /// <summary>The discount percentage value.</summary>
    [JsonPropertyName("discountPercent")]
    public string? DiscountPercent { get; init; }

    /// <summary>
    /// Volume unit the discount is expressed in: <c>liter</c>, <c>gallon</c>,
    /// <c>imperialGallon</c> or <c>unknown</c>.
    /// </summary>
    [JsonPropertyName("volumeUnit")]
    public string? VolumeUnit { get; init; }
}

/// <summary>
/// A per-fuel-type price at a preferred station. Mirrors the spec's
/// <c>PreferredStationPriceResponseResponseBody</c>.
/// </summary>
public sealed record PreferredStationPrice
{
    /// <summary>
    /// Fuel type the price applies to (e.g. <c>gasoline</c>, <c>diesel</c>,
    /// <c>electricity</c>, <c>unknown</c>). Spec-required.
    /// </summary>
    [JsonPropertyName("fuelType")]
    public string? FuelType { get; init; }

    /// <summary>
    /// Volume unit the price is expressed in: <c>liter</c>, <c>gallon</c>,
    /// <c>imperialGallon</c> or <c>unknown</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("volumeUnit")]
    public string? VolumeUnit { get; init; }

    /// <summary>Price before discounts.</summary>
    [JsonPropertyName("grossPrice")]
    public PreferredStationMoney? GrossPrice { get; init; }

    /// <summary>Price after discounts.</summary>
    [JsonPropertyName("netPrice")]
    public PreferredStationMoney? NetPrice { get; init; }
}

/// <summary>
/// A money amount returned by the preferred-stations API. Mirrors the spec's
/// three byte-identical money schemas
/// <c>PreferredStationDiscountResponseMoneyResponseBody</c>,
/// <c>PreferredStationGrossPriceResponseResponseBody</c> and
/// <c>PreferredStationNetPriceResponseResponseBody</c>.
/// </summary>
public sealed record PreferredStationMoney
{
    /// <summary>The money amount, as a decimal string. Spec-required.</summary>
    [JsonPropertyName("amount")]
    public string? Amount { get; init; }

    /// <summary>
    /// Currency code: <c>usd</c>, <c>gbp</c>, <c>cad</c>, <c>eur</c>, <c>chf</c> or
    /// <c>mxn</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; init; }
}

/// <summary>
/// Body for <c>POST /preferred-stations</c>. Mirrors the spec's
/// <c>PreferredStationsPostPreferredStationRequestBody</c>.
/// </summary>
public sealed record PreferredStationCreateRequest
{
    /// <summary>Display name of the station. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Postal address of the station. Spec marks REQUIRED.</summary>
    [JsonPropertyName("address")]
    public required PreferredStationAddressInput Address { get; init; }

    /// <summary>A map of external ids for the station. Spec marks REQUIRED.</summary>
    [JsonPropertyName("externalIds")]
    public required IReadOnlyDictionary<string, string> ExternalIds { get; init; }

    /// <summary>Latitude in WGS84 degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in WGS84 degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Discount overrides per fuel type.</summary>
    [JsonPropertyName("discounts")]
    public IReadOnlyList<PreferredStationDiscountInput>? Discounts { get; init; }

    /// <summary>Per-fuel-type prices.</summary>
    [JsonPropertyName("prices")]
    public IReadOnlyList<PreferredStationPriceInput>? Prices { get; init; }
}

/// <summary>
/// Body for <c>PATCH /preferred-stations</c> (the station is identified by the
/// <c>id</c> query parameter). Mirrors the spec's
/// <c>PreferredStationsPatchPreferredStationRequestBody</c>.
/// </summary>
public sealed record PreferredStationUpdateRequest
{
    /// <summary>Replacement discount overrides per fuel type.</summary>
    [JsonPropertyName("discounts")]
    public IReadOnlyList<PreferredStationDiscountInput>? Discounts { get; init; }

    /// <summary>Replacement per-fuel-type prices.</summary>
    [JsonPropertyName("prices")]
    public IReadOnlyList<PreferredStationPriceInput>? Prices { get; init; }
}

/// <summary>
/// Postal address written to a preferred station. Mirrors the spec's
/// <c>PreferredStationAddressRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>PreferredStationAddress</c>: the request marks
/// <c>line1</c>, <c>city</c>, <c>postalCode</c> and <c>country</c> REQUIRED while the
/// response leaves every member optional.
/// </remarks>
public sealed record PreferredStationAddressInput
{
    /// <summary>Street address of the station. Spec marks REQUIRED.</summary>
    [JsonPropertyName("line1")]
    public required string Line1 { get; init; }

    /// <summary>City of the station. Spec marks REQUIRED.</summary>
    [JsonPropertyName("city")]
    public required string City { get; init; }

    /// <summary>Postal or ZIP code. Spec marks REQUIRED.</summary>
    [JsonPropertyName("postalCode")]
    public required string PostalCode { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code. Spec marks REQUIRED.</summary>
    [JsonPropertyName("country")]
    public required string Country { get; init; }

    /// <summary>State or province code. Required by the API when <c>country</c> is <c>US</c>.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }
}

/// <summary>
/// A discount override written to a preferred station. Mirrors the spec's
/// <c>PreferredStationDiscountInputRequestBody</c>.
/// </summary>
public sealed record PreferredStationDiscountInput
{
    /// <summary>
    /// Fuel type the discount applies to (e.g. <c>gasoline</c>, <c>diesel</c>,
    /// <c>unknown</c>). Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("fuelType")]
    public required string FuelType { get; init; }

    /// <summary>
    /// Discount type: <c>centsPerUnit</c>, <c>percentage</c> or <c>fixedPrice</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("discountType")]
    public required string DiscountType { get; init; }

    /// <summary>The monetary discount, for the non-percentage discount types.</summary>
    [JsonPropertyName("discount")]
    public PreferredStationMoneyInput? Discount { get; init; }

    /// <summary>The discount percentage. Used for the <c>percentage</c> discount type.</summary>
    [JsonPropertyName("discountPercent")]
    public string? DiscountPercent { get; init; }

    /// <summary>
    /// Volume unit the discount is expressed in: <c>liter</c>, <c>gallon</c> or
    /// <c>imperialGallon</c>.
    /// </summary>
    [JsonPropertyName("volumeUnit")]
    public string? VolumeUnit { get; init; }
}

/// <summary>
/// A per-fuel-type price written to a preferred station. Mirrors the spec's
/// <c>PreferredStationPriceInputRequestBody</c>.
/// </summary>
public sealed record PreferredStationPriceInput
{
    /// <summary>
    /// Fuel type the price applies to (e.g. <c>gasoline</c>, <c>diesel</c>,
    /// <c>unknown</c>). Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("fuelType")]
    public required string FuelType { get; init; }

    /// <summary>
    /// Volume unit the price is expressed in: <c>liter</c>, <c>gallon</c> or
    /// <c>imperialGallon</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("volumeUnit")]
    public required string VolumeUnit { get; init; }

    /// <summary>Price before discounts.</summary>
    [JsonPropertyName("grossPrice")]
    public PreferredStationMoneyInput? GrossPrice { get; init; }

    /// <summary>Price after discounts.</summary>
    [JsonPropertyName("netPrice")]
    public PreferredStationMoneyInput? NetPrice { get; init; }
}

/// <summary>
/// A money amount written to the preferred-stations API. Mirrors the spec's three
/// byte-identical money schemas <c>PreferredStationDiscountMoneyRequestBody</c>,
/// <c>PreferredStationGrossPriceRequestBody</c> and
/// <c>PreferredStationNetPriceRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>PreferredStationMoney</c> because the request
/// marks both members REQUIRED.
/// </remarks>
public sealed record PreferredStationMoneyInput
{
    /// <summary>The money amount, as a decimal string. Spec marks REQUIRED.</summary>
    [JsonPropertyName("amount")]
    public required string Amount { get; init; }

    /// <summary>
    /// Currency code: <c>usd</c>, <c>gbp</c>, <c>cad</c>, <c>eur</c>, <c>chf</c> or
    /// <c>mxn</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }
}
