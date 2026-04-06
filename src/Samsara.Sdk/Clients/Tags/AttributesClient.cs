namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Tags;

internal sealed class AttributesClient : SamsaraServiceClientBase, IAttributesClient
{
    private const string BasePath = "attributes";

    public AttributesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<AttributeDefinition> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<AttributeDefinition>(BasePath, cancellationToken: cancellationToken);

    public Task<AttributeDefinition> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<AttributeDefinition>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<AttributeDefinition> CreateAsync(CreateAttributeRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<AttributeDefinition>(BasePath, request, cancellationToken);

    public Task<AttributeDefinition> UpdateAsync(string id, UpdateAttributeRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<AttributeDefinition>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
