namespace Samsara.Sdk.Pagination;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the cursor-based pagination metadata returned by the Samsara API.
/// </summary>
public sealed record PaginationInfo
{
    /// <summary>
    /// The cursor to use as the <c>after</c> query parameter for the next page.
    /// </summary>
    [JsonPropertyName("endCursor")]
    public string? EndCursor { get; init; }

    /// <summary>
    /// Whether there are more results available after this page.
    /// </summary>
    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage { get; init; }
}
