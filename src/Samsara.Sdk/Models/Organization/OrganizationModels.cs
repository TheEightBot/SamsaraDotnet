namespace Samsara.Sdk.Models.Organization;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a Samsara organization.
/// </summary>
public sealed record OrganizationInfo
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Street address. Not part of the spec inner schema; retained as
    /// a nullable back-compat convenience — callers should prefer
    /// <see cref="CarrierSettings"/>.<see cref="OrganizationCarrierSettings.MainOfficeAddress"/>
    /// for the canonical address field.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>City. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — no canonical replacement exists on
    /// the current spec inner schema.</summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>State or region. Not part of the spec inner schema; retained
    /// as a nullable back-compat convenience — no canonical replacement
    /// exists on the current spec inner schema.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Postal / ZIP code. Not part of the spec inner schema;
    /// retained as a nullable back-compat convenience — no canonical
    /// replacement exists on the current spec inner schema.</summary>
    [JsonPropertyName("zip")]
    public string? Zip { get; init; }

    /// <summary>Country. Not part of the spec inner schema; retained as a
    /// nullable back-compat convenience — no canonical replacement exists on
    /// the current spec inner schema.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("carrierSettings")]
    public OrganizationCarrierSettings? CarrierSettings { get; init; }
}

/// <summary>
/// Carrier settings for FMCSA / DOT.
/// </summary>
public sealed record OrganizationCarrierSettings
{
    [JsonPropertyName("dotNumber")]
    public string? DotNumber { get; init; }

    [JsonPropertyName("mainOfficeAddress")]
    public string? MainOfficeAddress { get; init; }
}

/// <summary>
/// Represents a user in the Samsara organization.
/// </summary>
public sealed record User
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("authType")]
    public required string AuthType { get; init; }

    [JsonPropertyName("roles")]
    public required IReadOnlyList<UserRole> Roles { get; init; }
}

/// <summary>
/// A role assigned to a user.
/// </summary>
public sealed record UserRole
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("tagId")]
    public string? TagId { get; init; }
}

/// <summary>
/// Request body for creating a user.
/// </summary>
public sealed record CreateUserRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("authType")]
    public required string AuthType { get; init; }

    [JsonPropertyName("roles")]
    public required IReadOnlyList<UserRole> Roles { get; init; }

    [JsonPropertyName("expireAt")]
    public DateTimeOffset? ExpireAt { get; init; }
}

/// <summary>
/// Request body for updating a user.
/// </summary>
public sealed record UpdateUserRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("roles")]
    public IReadOnlyList<UserRole>? Roles { get; init; }

    [JsonPropertyName("authType")]
    public string? AuthType { get; init; }

    [JsonPropertyName("expireAt")]
    public DateTimeOffset? ExpireAt { get; init; }
}
