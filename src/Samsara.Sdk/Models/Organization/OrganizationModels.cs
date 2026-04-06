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

    [JsonPropertyName("address")]
    public string? Address { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("zip")]
    public string? Zip { get; init; }

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
    public string? Name { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("authType")]
    public string? AuthType { get; init; }

    [JsonPropertyName("roles")]
    public IReadOnlyList<UserRole>? Roles { get; init; }
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
    public IReadOnlyList<UserRole>? Roles { get; init; }
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
}
