namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a vehicle idling event returned by <c>GET /idling/events</c>.
/// Mirrors the spec's <c>IdlingEventObject_V2025_10_23ResponseBody</c> inner
/// schema (see <c>AdvancedIdlingGetIdlingEventsResponseBody.data[*]</c>).
/// </summary>
public sealed record IdlingEvent
{
    /// <summary>
    /// The asset associated with the idling event. Returns vehicle details at this time.
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("asset")]
    public required IdlingEventAsset Asset { get; init; }

    /// <summary>
    /// Duration of the idling event in milliseconds. Spec-required.
    /// </summary>
    [JsonPropertyName("durationMilliseconds")]
    public required long DurationMilliseconds { get; init; }

    /// <summary>
    /// Universally unique identifier of the idling event. Spec-required.
    /// </summary>
    [JsonPropertyName("eventUuid")]
    public required string EventUuid { get; init; }

    /// <summary>
    /// Amount of liquid fuel consumed in milliliters during the idling event. Spec-required.
    /// </summary>
    [JsonPropertyName("fuelConsumedMilliliters")]
    public required double FuelConsumedMilliliters { get; init; }

    /// <summary>
    /// Cost incurred based on the liquid fuel consumed during the idling event. Spec-required.
    /// </summary>
    [JsonPropertyName("fuelCost")]
    public required IdlingEventFuelCost FuelCost { get; init; }

    /// <summary>
    /// Amount of gaseous fuel consumed in grams during the idling event. Spec-required.
    /// </summary>
    [JsonPropertyName("gaseousFuelConsumedGrams")]
    public required double GaseousFuelConsumedGrams { get; init; }

    /// <summary>
    /// Cost incurred based on the gaseous fuel consumed during the idling event. Spec-required.
    /// </summary>
    [JsonPropertyName("gaseousFuelCost")]
    public required IdlingEventGaseousFuelCost GaseousFuelCost { get; init; }

    /// <summary>
    /// The PTO (Power Take-Off) state during the idling event. Valid values: <c>active</c>,
    /// <c>inactive</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("ptoState")]
    public required string PtoState { get; init; }

    /// <summary>
    /// Start time of the idling event in RFC 3339 format (e.g., <c>2019-06-13T17:08:25Z</c>).
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("startTime")]
    public required DateTimeOffset StartTime { get; init; }

    /// <summary>
    /// The geofence address associated with the idling event, if applicable. Optional per spec.
    /// </summary>
    [JsonPropertyName("address")]
    public IdlingEventAddress? Address { get; init; }

    /// <summary>
    /// Air temperature in millicelsius during the idling event. Returned only when known.
    /// </summary>
    [JsonPropertyName("airTemperatureMillicelsius")]
    public long? AirTemperatureMillicelsius { get; init; }

    /// <summary>
    /// The operator associated with the idling event (returns driver details).
    /// Present only when a driver is assigned to the vehicle.
    /// </summary>
    [JsonPropertyName("operator")]
    public IdlingEventOperator? Operator { get; init; }

    /// <summary>
    /// Latitude of the location where the idling event occurred.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// Longitude of the location where the idling event occurred.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// Geofence address associated with an idling event. Mirrors the spec's
/// <c>IdlingEventAddressObjectResponseBody</c>.
/// </summary>
public sealed record IdlingEventAddress
{
    /// <summary>ID of the geofence address of the idling location. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The types of the geofence address of the idling location. An address can have multiple
    /// types (e.g., <c>yard</c>, <c>industrialSite</c>).
    /// </summary>
    [JsonPropertyName("addressTypes")]
    public IReadOnlyList<string>? AddressTypes { get; init; }

    /// <summary>Map of external IDs for the address.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Asset associated with an idling event. Returns vehicle details at this time.
/// Mirrors the spec's <c>IdlingEventAssetObjectResponseBody</c>.
/// </summary>
public sealed record IdlingEventAsset
{
    /// <summary>
    /// Samsara ID of the asset assigned to the event (returns vehicle ID at this time).
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("id")]
    public required long Id { get; init; }

    /// <summary>Map of external IDs for the asset.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Operator associated with an idling event. Returns driver details at this time.
/// Mirrors the spec's <c>IdlingEventOperatorObjectResponseBody</c>.
/// </summary>
public sealed record IdlingEventOperator
{
    /// <summary>
    /// Samsara ID of the operator assigned to the event (returns driver ID at this time).
    /// Spec-required.
    /// </summary>
    [JsonPropertyName("id")]
    public required long Id { get; init; }

    /// <summary>Map of external IDs for the operator.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Cost incurred based on the liquid fuel consumed during an idling event.
/// Mirrors the spec's <c>FuelCostObjectResponseBody</c>.
/// </summary>
public sealed record IdlingEventFuelCost
{
    /// <summary>The money amount (decimal string, e.g., <c>"640.2"</c>). Spec-required.</summary>
    [JsonPropertyName("amount")]
    public required string Amount { get; init; }

    /// <summary>
    /// The currency of money. ISO 4217 currency code. Valid values: <c>usd</c>, <c>gbp</c>,
    /// <c>cad</c>, <c>eur</c>, <c>chf</c>, <c>mxn</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }
}

/// <summary>
/// Cost incurred based on the gaseous fuel consumed during an idling event.
/// Mirrors the spec's <c>GaseousFuelCostObjectResponseBody</c>.
/// </summary>
public sealed record IdlingEventGaseousFuelCost
{
    /// <summary>The money amount (decimal string, e.g., <c>"640.2"</c>). Spec-required.</summary>
    [JsonPropertyName("amount")]
    public required string Amount { get; init; }

    /// <summary>
    /// The currency of money. ISO 4217 currency code. Valid values: <c>usd</c>, <c>gbp</c>,
    /// <c>cad</c>, <c>eur</c>, <c>chf</c>, <c>mxn</c>. Spec-required.
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }
}
