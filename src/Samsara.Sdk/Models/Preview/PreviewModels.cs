namespace Samsara.Sdk.Models.Preview;

using System.Text.Json.Serialization;

/// <summary>
/// Request body for <c>POST /preview/fleet/drivers/create-auth-token</c>. Mirrors
/// the spec's <c>DriversAuthTokenCreateDriverAuthTokenRequestBody</c>.
/// </summary>
/// <remarks>
/// <b>This is not the same schema as the stable endpoint's request body.</b> The
/// preview operation identifies the driver with <c>id</c>, whereas
/// <c>POST /fleet/drivers/auth-token</c> (spec schema
/// <c>AuthTokenAuthTokenRequestBody</c>, modelled by
/// <c>Samsara.Sdk.Models.Drivers.CreateDriverAuthTokenRequest</c>) uses
/// <c>driverId</c>. The two records are deliberately not merged. The success
/// payloads <i>are</i> identical, so both operations return
/// <c>Samsara.Sdk.Models.Drivers.DriverAuthToken</c>.
/// </remarks>
public sealed record PreviewCreateDriverAuthTokenRequest
{
    /// <summary>
    /// Random 12+ character string, paired with the returned token to protect
    /// against interception. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("code")]
    public required string Code { get; init; }

    /// <summary>
    /// Samsara ID of the driver. Note the property name: the preview endpoint
    /// spells this <c>id</c>, not <c>driverId</c>.
    /// </summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>
    /// External ID of the driver, in <c>key:value</c> form (e.g.
    /// <c>payrollId:ABFS18600</c>).
    /// </summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Login username of the driver.</summary>
    [JsonPropertyName("username")]
    public string? Username { get; init; }
}
