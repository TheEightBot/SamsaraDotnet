namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Documents;
using Samsara.Sdk.Tests.Helpers;

public sealed class DocumentsClientTests
{
    [Fact]
    public async Task ListAsync_ThreadsTimeRangeAndQueryBy()
    {
        var resp = new
        {
            data = Array.Empty<object>(),
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DocumentsClient(TestFactory.CreateHttpClient(handler));

        _ = await CollectAsync(client.ListAsync(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero),
            documentTypeId: "dt-1",
            queryBy: "created"));

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/documents");
        url.Should().Contain("startTime=");
        url.Should().Contain("endTime=");
        url.Should().Contain("documentTypeId=dt-1");
        url.Should().Contain("queryBy=created");
    }

    [Fact]
    public async Task GetAsync_BindsTypedNestedDocumentTypeAndDriver()
    {
        // Document requires id/documentType/driver — exercise the by-id binding.
        var resp = new
        {
            data = new
            {
                id = "doc-1",
                name = "BOL 12345",
                documentType = new { id = "dt-1", name = "Bill of Lading" },
                driver = new { id = "drv-1", name = "Jane Doe" },
                // `state`, `fields`, and `createdAtTime` are spec-required on Document.
                state = "completed",
                fields = Array.Empty<object>(),
                createdAtTime = "2024-01-01T00:00:00Z",
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DocumentsClient(TestFactory.CreateHttpClient(handler));

        var doc = await client.GetAsync("doc-1");

        doc.Id.Should().Be("doc-1");
        doc.DocumentType.Id.Should().Be("dt-1");
        doc.Driver.Id.Should().Be("drv-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/documents/doc-1");
    }

    [Fact]
    public async Task ListTypesAsync_CallsCorrectPath()
    {
        var resp = new
        {
            data = Array.Empty<object>(),
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DocumentsClient(TestFactory.CreateHttpClient(handler));

        _ = await CollectAsync(client.ListTypesAsync());

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/document-types");
    }

    private static async Task<IReadOnlyList<T>> CollectAsync<T>(IAsyncEnumerable<T> source)
    {
        var list = new List<T>();
        await foreach (var item in source)
        {
            list.Add(item);
        }

        return list;
    }
}
