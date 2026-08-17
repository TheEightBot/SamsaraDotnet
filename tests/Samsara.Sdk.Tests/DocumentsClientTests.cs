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

    // ── GET /fleet/documents — field values are NESTED under `value` ────────
    [Fact]
    public async Task GetAsync_BindsNestedDocumentFieldValueUnion()
    {
        var resp = new
        {
            data = new
            {
                id = "doc-1",
                documentType = new { id = "dt-1", name = "Bill of Lading" },
                driver = new { id = "drv-1", name = "Jane Doe" },
                state = "submitted",
                createdAtTime = "2024-01-01T00:00:00Z",
                fields = new object[]
                {
                    new
                    {
                        label = "Load weight",
                        type = "number",
                        value = new { numberValue = 123.456 },
                    },
                    new
                    {
                        label = "Notes",
                        type = "string",
                        value = new { stringValue = "Red Truck" },
                    },
                    new
                    {
                        label = "Photos",
                        type = "photo",
                        value = new
                        {
                            photoValue = new[]
                            {
                                new { id = "pho-1", url = "https://media.samsara.com/p1.jpg" },
                            },
                        },
                    },
                    new
                    {
                        label = "Signature",
                        type = "signature",
                        value = new
                        {
                            signatureValue = new
                            {
                                id = "sig-1",
                                name = "John Smith",
                                signedAtTime = "2024-01-01T05:00:00Z",
                                url = "https://media.samsara.com/sig.png",
                            },
                        },
                    },
                    new
                    {
                        label = "Scan",
                        type = "barcode",
                        value = new
                        {
                            barcodeValue = new[]
                            {
                                new { barcodeType = "org.gs1.EAN-13", barcodeValue = "0853883003114" },
                            },
                        },
                    },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DocumentsClient(TestFactory.CreateHttpClient(handler));

        var doc = await client.GetAsync("doc-1");

        doc.Fields.Should().HaveCount(5);

        // Before the 2026-08-17 sweep DocumentField carried flat photoValue /
        // stringValue / numberValue siblings, which bound NOTHING: the spec nests
        // all eight per-type values inside `value`.
        doc.Fields[0].Value!.NumberValue.Should().Be(123.456);
        doc.Fields[1].Value!.StringValue.Should().Be("Red Truck");

        var photo = doc.Fields[2].Value!.PhotoValue.Should().ContainSingle().Subject;
        photo.Id.Should().Be("pho-1");
        photo.Url.Should().Be("https://media.samsara.com/p1.jpg");

        var signature = doc.Fields[3].Value!.SignatureValue!;
        signature.Name.Should().Be("John Smith");
        signature.SignedAtTime.Should().Be(new DateTimeOffset(2024, 1, 1, 5, 0, 0, TimeSpan.Zero));

        var barcode = doc.Fields[4].Value!.BarcodeValue.Should().ContainSingle().Subject;
        barcode.BarcodeType.Should().Be("org.gs1.EAN-13");
        barcode.BarcodeValue.Should().Be("0853883003114");
    }

    // ── POST /fleet/documents — the same value union serializes ─────────────
    [Fact]
    public async Task CreateAsync_SerializesNestedDocumentFieldValue()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            data = new
            {
                id = "doc-1",
                documentType = new { id = "dt-1" },
                driver = new { id = "drv-1" },
                state = "submitted",
                createdAtTime = "2024-01-01T00:00:00Z",
                fields = Array.Empty<object>(),
            },
        });
        var client = new DocumentsClient(TestFactory.CreateHttpClient(handler));

        await client.CreateAsync(new CreateDocumentRequest
        {
            DocumentTypeId = "dt-1",
            DriverId = "drv-1",
            Fields = new[]
            {
                new DocumentFieldInput
                {
                    Label = "Load weight",
                    Type = "number",
                    Value = new DocumentFieldValue { NumberValue = 123.456 },
                },
            },
        });

        var body = handler.LastRequestBody!;
        body.Should().Contain("\"value\"");
        body.Should().Contain("\"numberValue\"");
        body.Should().Contain("123.456");
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
