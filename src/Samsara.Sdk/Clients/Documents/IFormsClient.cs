namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Documents;

/// <summary>
/// Client for Samsara forms (templates and submissions).
/// </summary>
public interface IFormsClient
{
    IAsyncEnumerable<FormTemplate> ListTemplatesAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<FormSubmission> ListSubmissionsAsync(CancellationToken cancellationToken = default);
    Task<FormSubmission> GetSubmissionAsync(string id, CancellationToken cancellationToken = default);
}
