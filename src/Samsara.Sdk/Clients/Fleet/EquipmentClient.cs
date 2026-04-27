namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class EquipmentClient : SamsaraServiceClientBase, IEquipmentClient
{
    private const string BasePath = "fleet/equipment";

    public EquipmentClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Equipment> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Equipment>(BasePath, cancellationToken: cancellationToken);

    public Task<Equipment> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Equipment>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Equipment> CreateAsync(CreateEquipmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Equipment>(BasePath, request, cancellationToken);

    public Task<Equipment> UpdateAsync(string id, UpdateEquipmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Equipment>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<EquipmentLocation> ListLocationsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<EquipmentLocation>($"{BasePath}/locations", cancellationToken: cancellationToken);
}
