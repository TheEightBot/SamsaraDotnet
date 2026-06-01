namespace Samsara.Sdk.Tests;

using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Media;
using Samsara.Sdk.Tests.Helpers;

public sealed class MediaClientTests
{
    // ── GET /cameras/media/retrieval ────────────────────────────────────────
    // Response shape is { data: { media: [ MediaObjectResponseBody, ... ] } } — the
    // items are nested under data.media, not data itself.

    [Fact]
    public async Task GetRetrievalAsync_DeserializesNestedMediaArray()
    {
        var resp = new
        {
            data = new
            {
                media = new[]
                {
                    new
                    {
                        input = "dashcamRoadFacing",
                        mediaType = "image",
                        status = "available",
                        vehicleId = "veh-1",
                        startTime = "2024-01-01T00:00:00Z",
                        endTime = "2024-01-01T00:00:00Z",
                        availableAtTime = "2024-01-01T00:05:00Z",
                        cameraRole = "front",
                        urlInfo = new { url = "https://media.samsara.com/x.jpg", urlExpiryTime = "2024-01-01T08:00:00Z" },
                    },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MediaClient(TestFactory.CreateHttpClient(handler));

        var result = await client.GetRetrievalAsync("ret-1");

        result.Should().HaveCount(1);
        var item = result[0];
        item.Input.Should().Be("dashcamRoadFacing");
        item.MediaType.Should().Be("image");
        item.Status.Should().Be("available");
        item.VehicleId.Should().Be("veh-1");
        item.UrlInfo!.Url.Should().Be("https://media.samsara.com/x.jpg");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("cameras/media/retrieval");
        url.Should().Contain("retrievalId=ret-1");
    }

    [Fact]
    public async Task GetRetrievalAsync_ReturnsEmptyList_WhenMediaOmitted()
    {
        // A pending retrieval that has produced no media yet returns { data: {} };
        // the wrapper must yield an empty list rather than null or a throw.
        var handler = MockHttpMessageHandler.WithJsonResponse(new { data = new { } });
        var client = new MediaClient(TestFactory.CreateHttpClient(handler));

        var result = await client.GetRetrievalAsync("ret-1");

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    // ── GET /cameras/media ──────────────────────────────────────────────────
    // Response shape is { data: { media: [...] }, pagination: {...} } — paginated,
    // but the item array is nested under data.media, not data itself.

    [Fact]
    public async Task ListAsync_DeserializesNestedMediaArray_SinglePage()
    {
        var resp = new
        {
            data = new
            {
                media = new[]
                {
                    new
                    {
                        availableAtTime = "2024-01-01T00:05:00Z",
                        endTime = "2024-01-01T00:01:00Z",
                        input = "dashcamForwardFacing",
                        mediaType = "videoHighRes",
                        startTime = "2024-01-01T00:00:00Z",
                        triggerReason = "api",
                        vehicleId = "veh-1",
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MediaClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListAsync("veh-1", "2024-01-01T00:00:00Z", "2024-01-02T00:00:00Z"));

        items.Should().HaveCount(1);
        items[0].VehicleId.Should().Be("veh-1");
        items[0].MediaType.Should().Be("videoHighRes");
        items[0].Input.Should().Be("dashcamForwardFacing");
        items[0].TriggerReason.Should().Be("api");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("cameras/media");
        url.Should().Contain("vehicleIds=veh-1");
    }

    [Fact]
    public async Task ListAsync_PaginatesAcrossPages_ThreadingCursor()
    {
        var page1 = new
        {
            data = new { media = new[] { MediaFileObj("veh-1") } },
            pagination = new { endCursor = "CURSOR2", hasNextPage = true },
        };
        var page2 = new
        {
            data = new { media = new[] { MediaFileObj("veh-2") } },
            pagination = new { hasNextPage = false },
        };

        var handler = new MockHttpMessageHandler((req, _) =>
        {
            var body = req.RequestUri!.Query.Contains("after=") ? (object)page2 : page1;
            return Task.FromResult(JsonResponse(body));
        });
        var client = new MediaClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListAsync("veh-1,veh-2", "2024-01-01T00:00:00Z", "2024-01-02T00:00:00Z"));

        items.Should().HaveCount(2);
        items.Select(m => m.VehicleId).Should().ContainInOrder("veh-1", "veh-2");

        handler.Requests.Should().HaveCount(2);
        handler.Requests[1].RequestUri!.Query.Should().Contain("after=CURSOR2");
    }

    private static object MediaFileObj(string vehicleId) => new
    {
        availableAtTime = "2024-01-01T00:05:00Z",
        endTime = "2024-01-01T00:01:00Z",
        input = "dashcamForwardFacing",
        mediaType = "videoHighRes",
        startTime = "2024-01-01T00:00:00Z",
        triggerReason = "api",
        vehicleId,
    };

    private static HttpResponseMessage JsonResponse(object body) => new(HttpStatusCode.OK)
    {
        Content = new StringContent(
            JsonSerializer.Serialize(body, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            }),
            Encoding.UTF8,
            "application/json"),
    };

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
