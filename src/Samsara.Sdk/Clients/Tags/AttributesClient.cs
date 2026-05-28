namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Tags;

internal sealed class AttributesClient : SamsaraServiceClientBase, IAttributesClient
{
    private const string BasePath = "attributes";

    public AttributesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<AttributeDefinition> ListAsync(string entityType, CancellationToken cancellationToken = default)
        => PaginateAsync<AttributeDefinition>(
            QueryBuilder.WithParams(BasePath, ("entityType", entityType)),
            cancellationToken: cancellationToken);

    public Task<AttributeDefinition> GetAsync(string id, string entityType, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<AttributeDefinition>(
            QueryBuilder.WithParams($"{BasePath}/{Uri.EscapeDataString(id)}", ("entityType", entityType)),
            cancellationToken);

    public Task<AttributeDefinition> CreateAsync(CreateAttributeRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<AttributeDefinition>(BasePath, request, cancellationToken);

    public Task<AttributeDefinition> UpdateAsync(string id, UpdateAttributeRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<AttributeDefinition>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, string entityType, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(
            QueryBuilder.WithParams($"{BasePath}/{Uri.EscapeDataString(id)}", ("entityType", entityType)),
            cancellationToken);
}
