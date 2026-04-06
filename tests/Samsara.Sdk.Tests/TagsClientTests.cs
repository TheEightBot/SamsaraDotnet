namespace Samsara.Sdk.Tests;

using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Tags;
using Samsara.Sdk.Tests.Helpers;

public sealed class TagsClientTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    };

    [Fact]
    public async Task GetAsync_CallsCorrectPath()
    {
        var resp = new { data = new { id = "tag-1", name = "Warehouse" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var httpClient = TestFactory.CreateHttpClient(handler);
        var client = new TagsClient(httpClient);

        var tag = await client.GetAsync("tag-1");

        tag.Id.Should().Be("tag-1");
        tag.Name.Should().Be("Warehouse");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/tags/tag-1");
    }

    [Fact]
    public async Task CreateAsync_PostsToCorrectPath()
    {
        var resp = new { data = new { id = "new-tag", name = "New Tag" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var httpClient = TestFactory.CreateHttpClient(handler);
        var client = new TagsClient(httpClient);

        var tag = await client.CreateAsync(new CreateTagRequest { Name = "New Tag" });

        tag.Name.Should().Be("New Tag");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/tags");
    }

    [Fact]
    public async Task UpdateAsync_PatchesToCorrectPath()
    {
        var resp = new { data = new { id = "tag-1", name = "Updated" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var httpClient = TestFactory.CreateHttpClient(handler);
        var client = new TagsClient(httpClient);

        var tag = await client.UpdateAsync("tag-1", new UpdateTagRequest { Name = "Updated" });

        tag.Name.Should().Be("Updated");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/tags/tag-1");
    }

    [Fact]
    public async Task DeleteAsync_DeletesCorrectPath()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var httpClient = TestFactory.CreateHttpClient(handler);
        var client = new TagsClient(httpClient);

        await client.DeleteAsync("tag-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/tags/tag-1");
    }

    [Fact]
    public async Task ListAsync_ReturnsAllItemsFromPaginatedEndpoint()
    {
        var page1Body = JsonSerializer.Serialize(
            new
            {
                data = new[] { new { id = "t1", name = "Tag1" } },
                pagination = new { endCursor = "cur1", hasNextPage = true }
            }, JsonOptions);

        var page2Body = JsonSerializer.Serialize(
            new
            {
                data = new[] { new { id = "t2", name = "Tag2" } },
                pagination = new { endCursor = "cur2", hasNextPage = false }
            }, JsonOptions);

        var callCount = 0;
        var handler = new MockHttpMessageHandler((req, _) =>
        {
            callCount++;
            var body = callCount == 1 ? page1Body : page2Body;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            });
        });

        var httpClient = TestFactory.CreateHttpClient(handler);
        var client = new TagsClient(httpClient);

        var tags = new List<Tag>();
        await foreach (var tag in client.ListAsync())
        {
            tags.Add(tag);
        }

        tags.Should().HaveCount(2);
        tags[0].Id.Should().Be("t1");
        tags[1].Id.Should().Be("t2");
    }

    [Fact]
    public async Task GetAsync_EscapesIdInPath()
    {
        var resp = new { data = new { id = "tag/special", name = "Special" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var httpClient = TestFactory.CreateHttpClient(handler);
        var client = new TagsClient(httpClient);

        await client.GetAsync("tag/special");

        // The '/' in the id should be escaped
        handler.LastRequest.RequestUri!.PathAndQuery.Should().NotContain("fleet/tags/tag/special");
    }
}
