namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Documents;

internal sealed class FormsClient : SamsaraServiceClientBase, IFormsClient
{
    public FormsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<FormTemplate> ListTemplatesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<FormTemplate>("fleet/forms/templates", cancellationToken: cancellationToken);

    public IAsyncEnumerable<FormSubmission> ListSubmissionsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<FormSubmission>("fleet/forms/submissions", cancellationToken: cancellationToken);

    public Task<FormSubmission> GetSubmissionAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FormSubmission>($"fleet/forms/submissions/{Uri.EscapeDataString(id)}", cancellationToken);
}
