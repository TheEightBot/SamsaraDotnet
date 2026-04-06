namespace Samsara.Sdk.Pagination;

using System.Runtime.CompilerServices;
using System.Text.Json;
using Samsara.Sdk.Serialization;

/// <summary>
/// Provides extension methods for consuming paginated Samsara API responses as <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public static class PaginationExtensions
{
    /// <summary>
    /// Enumerates all pages of results from a paginated Samsara API endpoint,
    /// yielding individual items.
    /// </summary>
    /// <typeparam name="T">The type of items in each page.</typeparam>
    /// <param name="fetchPage">
    /// A function that takes an optional cursor and returns a <see cref="PagedResponse{T}"/>.
    /// Pass <c>null</c> for the first page.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable of all items across all pages.</returns>
    public static async IAsyncEnumerable<T> PaginateAsync<T>(
        Func<string?, CancellationToken, Task<PagedResponse<T>>> fetchPage,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        string? cursor = null;

        do
        {
            var page = await fetchPage(cursor, cancellationToken).ConfigureAwait(false);

            foreach (var item in page.Data)
            {
                yield return item;
            }

            cursor = page.Pagination?.EndCursor;

            if (page.Pagination?.HasNextPage != true)
            {
                break;
            }
        }
        while (!cancellationToken.IsCancellationRequested);
    }

    /// <summary>
    /// Enumerates all pages from a paginated Samsara API endpoint,
    /// yielding entire <see cref="PagedResponse{T}"/> objects.
    /// </summary>
    public static async IAsyncEnumerable<PagedResponse<T>> PaginatePagesAsync<T>(
        Func<string?, CancellationToken, Task<PagedResponse<T>>> fetchPage,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        string? cursor = null;

        do
        {
            var page = await fetchPage(cursor, cancellationToken).ConfigureAwait(false);

            yield return page;

            cursor = page.Pagination?.EndCursor;

            if (page.Pagination?.HasNextPage != true)
            {
                break;
            }
        }
        while (!cancellationToken.IsCancellationRequested);
    }
}
