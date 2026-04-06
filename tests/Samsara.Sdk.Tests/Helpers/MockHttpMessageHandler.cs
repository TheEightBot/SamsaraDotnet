namespace Samsara.Sdk.Tests.Helpers;

using System.Net;
using System.Text;
using System.Text.Json;

/// <summary>
/// A mock HttpMessageHandler that returns a preconfigured response.
/// </summary>
internal sealed class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handler;
    private readonly List<HttpRequestMessage> _requests = new();

    public IReadOnlyList<HttpRequestMessage> Requests => _requests;
    public HttpRequestMessage LastRequest => _requests[^1];

    public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
    {
        _handler = handler;
    }

    public MockHttpMessageHandler(HttpResponseMessage response)
        : this((_, _) => Task.FromResult(response))
    {
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _requests.Add(request);
        return _handler(request, cancellationToken);
    }

    public static MockHttpMessageHandler WithJsonResponse<T>(T body, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new MockHttpMessageHandler(new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(body, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                Encoding.UTF8,
                "application/json")
        });
    }

    public static MockHttpMessageHandler WithErrorResponse(HttpStatusCode statusCode, string? message = null, string? requestId = null)
    {
        var errorBody = new { message, requestId };
        return new MockHttpMessageHandler(new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(errorBody),
                Encoding.UTF8,
                "application/json")
        });
    }

    public static MockHttpMessageHandler WithSequence(params HttpResponseMessage[] responses)
    {
        var index = 0;
        return new MockHttpMessageHandler((_, _) =>
        {
            var response = responses[index];
            if (index < responses.Length - 1) index++;
            return Task.FromResult(response);
        });
    }
}
