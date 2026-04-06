namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Pagination;

public sealed class PaginationExtensionsTests
{
    [Fact]
    public async Task PaginateAsync_YieldsAllItemsAcrossPages()
    {
        var page1 = new PagedResponse<string>
        {
            Data = ["a", "b"],
            Pagination = new PaginationInfo { EndCursor = "cur1", HasNextPage = true }
        };
        var page2 = new PagedResponse<string>
        {
            Data = ["c"],
            Pagination = new PaginationInfo { EndCursor = "cur2", HasNextPage = false }
        };

        var callCount = 0;
        Task<PagedResponse<string>> FetchPage(string? cursor, CancellationToken ct)
        {
            callCount++;
            return Task.FromResult(cursor is null ? page1 : page2);
        }

        var items = new List<string>();
        await foreach (var item in PaginationExtensions.PaginateAsync<string>(FetchPage))
        {
            items.Add(item);
        }

        items.Should().BeEquivalentTo(["a", "b", "c"]);
        callCount.Should().Be(2);
    }

    [Fact]
    public async Task PaginateAsync_StopsOnSinglePage()
    {
        var page = new PagedResponse<int>
        {
            Data = [1, 2, 3],
            Pagination = new PaginationInfo { HasNextPage = false }
        };

        var items = new List<int>();
        await foreach (var item in PaginationExtensions.PaginateAsync<int>((_, _) => Task.FromResult(page)))
        {
            items.Add(item);
        }

        items.Should().HaveCount(3);
    }

    [Fact]
    public async Task PaginateAsync_StopsWhenCancelled()
    {
        var cts = new CancellationTokenSource();
        var page = new PagedResponse<int>
        {
            Data = [1],
            Pagination = new PaginationInfo { EndCursor = "next", HasNextPage = true }
        };

        var items = new List<int>();
        var callCount = 0;

        await foreach (var item in PaginationExtensions.PaginateAsync<int>((_, _) =>
        {
            callCount++;
            if (callCount >= 2)
                cts.Cancel();
            return Task.FromResult(page);
        }, cts.Token))
        {
            items.Add(item);
        }

        items.Should().HaveCountGreaterThanOrEqualTo(1);
        callCount.Should().BeLessThanOrEqualTo(3);
    }

    [Fact]
    public async Task PaginateAsync_StopsWhenNoPagination()
    {
        var page = new PagedResponse<string>
        {
            Data = ["only"],
            Pagination = null
        };

        var items = new List<string>();
        await foreach (var item in PaginationExtensions.PaginateAsync<string>((_, _) => Task.FromResult(page)))
        {
            items.Add(item);
        }

        items.Should().Equal("only");
    }

    [Fact]
    public async Task PaginatePagesAsync_YieldsAllPages()
    {
        var page1 = new PagedResponse<string>
        {
            Data = ["a"],
            Pagination = new PaginationInfo { EndCursor = "c1", HasNextPage = true }
        };
        var page2 = new PagedResponse<string>
        {
            Data = ["b"],
            Pagination = new PaginationInfo { HasNextPage = false }
        };

        var pages = new List<PagedResponse<string>>();
        await foreach (var page in PaginationExtensions.PaginatePagesAsync<string>(
            (cursor, _) => Task.FromResult(cursor is null ? page1 : page2)))
        {
            pages.Add(page);
        }

        pages.Should().HaveCount(2);
        pages[0].Data.Should().Equal("a");
        pages[1].Data.Should().Equal("b");
    }
}
