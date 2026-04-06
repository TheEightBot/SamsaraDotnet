namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Tags;

/// <summary>
/// Client for managing Samsara attributes.
/// </summary>
public interface IAttributesClient
{
    IAsyncEnumerable<AttributeDefinition> ListAsync(CancellationToken cancellationToken = default);
    Task<AttributeDefinition> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<AttributeDefinition> CreateAsync(CreateAttributeRequest request, CancellationToken cancellationToken = default);
    Task<AttributeDefinition> UpdateAsync(string id, UpdateAttributeRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
