namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Tags;

/// <summary>
/// Client for managing Samsara attributes.
/// </summary>
public interface IAttributesClient
{
    /// <summary>
    /// List all attributes for the given <paramref name="entityType"/>
    /// (<c>GET /attributes</c>). The spec marks <c>entityType</c> as REQUIRED.
    /// </summary>
    IAsyncEnumerable<AttributeDefinition> ListAsync(string entityType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve a single attribute by <paramref name="id"/> for the given
    /// <paramref name="entityType"/> (<c>GET /attributes/{id}</c>). The spec
    /// marks <c>entityType</c> as REQUIRED.
    /// </summary>
    Task<AttributeDefinition> GetAsync(string id, string entityType, CancellationToken cancellationToken = default);

    Task<AttributeDefinition> CreateAsync(CreateAttributeRequest request, CancellationToken cancellationToken = default);

    Task<AttributeDefinition> UpdateAsync(string id, UpdateAttributeRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an attribute by <paramref name="id"/> for the given
    /// <paramref name="entityType"/> (<c>DELETE /attributes/{id}</c>). The spec
    /// marks <c>entityType</c> as REQUIRED.
    /// </summary>
    Task DeleteAsync(string id, string entityType, CancellationToken cancellationToken = default);
}
