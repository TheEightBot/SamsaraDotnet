namespace Samsara.Sdk.Models.Organization;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a Samsara organization.
/// </summary>
public sealed record OrganizationInfo
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

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
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>ID of the tag this role applies to (organizational role when absent).
    /// Present on the user create/update request role schema
    /// (<c>CreateUserRequest_roles.tagId</c>); not returned by <c>GET /user-roles</c>.</summary>
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
