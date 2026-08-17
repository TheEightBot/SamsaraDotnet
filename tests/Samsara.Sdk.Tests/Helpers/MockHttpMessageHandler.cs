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
    private readonly List<string?> _requestBodies = new();

    public IReadOnlyList<HttpRequestMessage> Requests => _requests;
    public HttpRequestMessage LastRequest => _requests[^1];

    /// <summary>
    /// Request bodies captured as text, positionally matching <see cref="Requests"/>.
    /// Captured during send because <c>HttpClient</c> disposes request content once the
    /// request completes, so reading <c>LastRequest.Content</c> afterwards throws.
    /// </summary>
    public IReadOnlyList<string?> RequestBodies => _requestBodies;

    /// <summary>Body of the most recent request, or null if it had none.</summary>
    public string? LastRequestBody => _requestBodies[^1];

    public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
    {
        _handler = handler;
    }

    public MockHttpMessageHandler(HttpResponseMessage response)
        : this((_, _) => Task.FromResult(response))
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _requests.Add(request);
        _requestBodies.Add(
            request.Content is null
                ? null
                : await request.Content.ReadAsStringAsync().ConfigureAwait(false));
        var response = await _handler(request, cancellationToken).ConfigureAwait(false);

        // Mirror the real handlers (HttpClientHandler/SocketsHttpHandler), which attach the
        // originating request to the response. The SDK reads this to describe failed requests.
        response.RequestMessage ??= request;
        return response;
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
