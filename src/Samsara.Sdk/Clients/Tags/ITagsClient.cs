namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Tags;

/// <summary>
/// Client for managing Samsara tags.
/// </summary>
public interface ITagsClient
{
    /// <summary>Lists all tags.</summary>
    IAsyncEnumerable<Tag> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets a single tag by ID.</summary>
    Task<Tag> GetAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>Creates a new tag.</summary>
    Task<Tag> CreateAsync(CreateTagRequest request, CancellationToken cancellationToken = default);

    /// <summary>Updates a tag.</summary>
    Task<Tag> UpdateAsync(string id, UpdateTagRequest request, CancellationToken cancellationToken = default);

    /// <summary>Deletes a tag.</summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
