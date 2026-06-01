namespace Samsara.Sdk.Tests;

using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Tags;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract test for <see cref="CreateTagRequest"/> (Phase 3): <c>name</c> is a required
/// member (verified at compile time by the initializer) and must be serialized in
/// camelCase on the outgoing <c>POST /tags</c> body.
/// </summary>
public sealed class TagsContractTests
{
    [Fact]
    public async Task CreateAsync_SerializesRequiredNameInRequestBody()
    {
        string? capturedBody = null;
        var handler = new MockHttpMessageHandler(async (req, ct) =>
        {
            capturedBody = req.Content is null ? null : await req.Content.ReadAsStringAsync(ct);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new { data = new { id = "tag-1", name = "Warehouse" } }),
                    System.Text.Encoding.UTF8,
                    "application/json"),
            };
        });
        var client = new TagsClient(TestFactory.CreateHttpClient(handler));

        // `Name` is `required` on CreateTagRequest — omitting it is a compile error.
        var tag = await client.CreateAsync(new CreateTagRequest { Name = "Warehouse", ParentTagId = "parent-1" });

        tag.Id.Should().Be("tag-1");
        capturedBody.Should().NotBeNull();
        using var doc = JsonDocument.Parse(capturedBody!);
        doc.RootElement.GetProperty("name").GetString().Should().Be("Warehouse");
        doc.RootElement.GetProperty("parentTagId").GetString().Should().Be("parent-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("tags");
    }
}
