namespace Samsara.Sdk.Models.Organization;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

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

    /// <summary>
    /// Roles assigned to this user. Each entry nests the role itself under
    /// <see cref="UserRoleAssignment.Role"/> — the wire shape is
    /// <c>{ "role": { "id", "name" }, "tag": { … }, "expireAt": … }</c>, not a flat role.
    /// </summary>
    [JsonPropertyName("roles")]
    public required IReadOnlyList<UserRoleAssignment> Roles { get; init; }
}

/// <summary>
/// A role definition, as returned by <c>GET /user-roles</c>. Mirrors the spec's
/// <c>UserRole</c>.
/// </summary>
public sealed record UserRole
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// One entry of <see cref="User.Roles"/>. Mirrors the spec's <c>UserRoleAssignment</c>.
/// </summary>
/// <remarks>
/// A user may hold <i>organizational</i> roles, which apply account-wide (<see cref="Tag"/>
/// is null), and <i>tag-specific</i> roles, which apply only within <see cref="Tag"/>.
/// </remarks>
public sealed record UserRoleAssignment
{
    /// <summary>The role itself.</summary>
    [JsonPropertyName("role")]
    public UserRole? Role { get; init; }

    /// <summary>The tag this role is scoped to; null for an organizational role.</summary>
    [JsonPropertyName("tag")]
    public TagReference? Tag { get; init; }

    /// <summary>When this role assignment expires, if it is time-limited.</summary>
    [JsonPropertyName("expireAt")]
    public DateTimeOffset? ExpireAt { get; init; }
}

/// <summary>
/// A role assignment as sent on user create/update. Mirrors the spec's
/// <c>CreateUserRequest_roles</c>.
/// </summary>
/// <remarks>
/// The request shape differs from the response shape: requests carry flat
/// <c>roleId</c>/<c>tagId</c> strings, whereas responses nest full
/// <see cref="UserRoleAssignment.Role"/> and <see cref="UserRoleAssignment.Tag"/> objects.
/// </remarks>
public sealed record UserRoleInput
{
    /// <summary>ID of the role to assign.</summary>
    [JsonPropertyName("roleId")]
    public required string RoleId { get; init; }

    /// <summary>Tag to scope the role to. Omit for an organizational role.</summary>
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
    public required IReadOnlyList<UserRoleInput> Roles { get; init; }

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
    public IReadOnlyList<UserRoleInput>? Roles { get; init; }

    [JsonPropertyName("authType")]
    public string? AuthType { get; init; }

    [JsonPropertyName("expireAt")]
    public DateTimeOffset? ExpireAt { get; init; }
}
