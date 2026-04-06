namespace Samsara.Sdk.Pagination;

/// <summary>
/// Wraps a page of results from the Samsara API.
/// </summary>
/// <typeparam name="T">The type of items in the page.</typeparam>
public sealed record PagedResponse<T>
{
    /// <summary>
    /// The items in this page.
    /// </summary>
    public required IReadOnlyList<T> Data { get; init; }

    /// <summary>
    /// Pagination metadata for retrieving the next page.
    /// </summary>
    public PaginationInfo? Pagination { get; init; }
}
