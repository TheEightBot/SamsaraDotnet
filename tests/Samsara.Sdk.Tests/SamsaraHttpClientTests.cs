namespace Samsara.Sdk.Tests;

using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Exceptions;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Common;
using Samsara.Sdk.Pagination;
using Samsara.Sdk.Tests.Helpers;

public sealed class SamsaraHttpClientTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    };

    [Fact]
    public async Task GetAsync_ReturnsDeserializedResponse()
    {
        var expected = new SamsaraResponse<string> { Data = "hello" };
        var handler = MockHttpMessageHandler.WithJsonResponse(expected);
        var client = TestFactory.CreateHttpClient(handler);

        var result = await client.GetAsync<SamsaraResponse<string>>("test");

        result.Data.Should().Be("hello");
    }

    [Fact]
    public async Task GetDataAsync_UnwrapsDataEnvelope()
    {
        var payload = new { data = new { id = "123", name = "Test" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(payload);
        var client = TestFactory.CreateHttpClient(handler);

        var result = await client.GetDataAsync<TagReference>("test");

        result.Id.Should().Be("123");
        result.Name.Should().Be("Test");
    }

    [Fact]
    public async Task GetPageAsync_ReturnsPagedData()
    {
        var payload = new
        {
            data = new[] { new { id = "1" }, new { id = "2" } },
            pagination = new { endCursor = "abc", hasNextPage = true }
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(payload);
        var client = TestFactory.CreateHttpClient(handler);

        var result = await client.GetPageAsync<TagReference>("tags");

        result.Data.Should().HaveCount(2);
        result.Pagination!.EndCursor.Should().Be("abc");
        result.Pagination.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public async Task GetPageAsync_AppendsCursorAndLimit()
    {
        var payload = new { data = Array.Empty<object>(), pagination = new { hasNextPage = false } };
        var handler = MockHttpMessageHandler.WithJsonResponse(payload);
        var client = TestFactory.CreateHttpClient(handler);

        await client.GetPageAsync<TagReference>("tags", cursor: "cur123", limit: 50);

        var url = handler.LastRequest.RequestUri!.ToString();
        url.Should().Contain("after=cur123");
        url.Should().Contain("limit=50");
    }

    [Fact]
    public async Task PostDataAsync_SendsBodyAndUnwrapsResponse()
    {
        var payload = new { data = new { id = "new-id", name = "Created" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(payload);
        var client = TestFactory.CreateHttpClient(handler);

        var result = await client.PostDataAsync<TagReference>("tags", new { name = "Test" });

        result.Id.Should().Be("new-id");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.Content.Should().NotBeNull();
    }

    [Fact]
    public async Task PatchDataAsync_SendsPatchAndUnwrapsResponse()
    {
        var payload = new { data = new { id = "123", name = "Updated" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(payload);
        var client = TestFactory.CreateHttpClient(handler);

        var result = await client.PatchDataAsync<TagReference>("tags/123", new { name = "Updated" });

        result.Name.Should().Be("Updated");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
    }

    [Fact]
    public async Task DeleteAsync_SendsDeleteRequest()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var client = TestFactory.CreateHttpClient(handler);

        await client.DeleteAsync("tags/123");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
    }

    [Fact]
    public async Task GetAsync_ThrowsBadRequestException_On400()
    {
        var handler = MockHttpMessageHandler.WithErrorResponse(HttpStatusCode.BadRequest, "Invalid field", "req-1");
        var client = TestFactory.CreateHttpClient(handler);

        var act = () => client.GetAsync<object>("test");

        var ex = await act.Should().ThrowAsync<SamsaraBadRequestException>();
        ex.Which.Message.Should().Contain("Invalid field");
        ex.Which.RequestId.Should().Be("req-1");
    }

    [Fact]
    public async Task GetAsync_ThrowsAuthException_On401()
    {
        var handler = MockHttpMessageHandler.WithErrorResponse(HttpStatusCode.Unauthorized, "Bad token");
        var client = TestFactory.CreateHttpClient(handler);

        var act = () => client.GetAsync<object>("test");

        await act.Should().ThrowAsync<SamsaraAuthenticationException>();
    }

    [Fact]
    public async Task GetAsync_ThrowsNotFoundException_On404()
    {
        var handler = MockHttpMessageHandler.WithErrorResponse(HttpStatusCode.NotFound, "Not found");
        var client = TestFactory.CreateHttpClient(handler);

        var act = () => client.GetAsync<object>("test");

        await act.Should().ThrowAsync<SamsaraNotFoundException>();
    }

    [Fact]
    public async Task GetAsync_ThrowsRateLimitException_On429()
    {
        var response = new HttpResponseMessage((HttpStatusCode)429)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(new { message = "Rate limited" }),
                Encoding.UTF8,
                "application/json")
        };
        response.Headers.TryAddWithoutValidation("Retry-After", "30");

        var handler = new MockHttpMessageHandler(response);
        var client = TestFactory.CreateHttpClient(handler);

        var act = () => client.GetAsync<object>("test");

        var ex = await act.Should().ThrowAsync<SamsaraRateLimitException>();
        ex.Which.RetryAfter.Should().Be(TimeSpan.FromSeconds(30));
    }

    [Fact]
    public async Task GetAsync_ThrowsServerException_On500()
    {
        var handler = MockHttpMessageHandler.WithErrorResponse(HttpStatusCode.InternalServerError, "Internal error");
        var client = TestFactory.CreateHttpClient(handler);

        var act = () => client.GetAsync<object>("test");

        await act.Should().ThrowAsync<SamsaraServerException>();
    }

    [Fact]
    public async Task PostAsync_CompletesWithoutException_OnSuccess()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK));
        var client = TestFactory.CreateHttpClient(handler);

        var act = () => client.PostAsync("messages", new { text = "hello" });

        await act.Should().NotThrowAsync();
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
    }
}
