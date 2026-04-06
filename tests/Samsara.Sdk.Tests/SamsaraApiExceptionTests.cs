namespace Samsara.Sdk.Tests;

using System.Net;
using FluentAssertions;
using Samsara.Sdk.Exceptions;

public sealed class SamsaraApiExceptionTests
{
    [Theory]
    [InlineData(HttpStatusCode.BadRequest, typeof(SamsaraBadRequestException))]
    [InlineData(HttpStatusCode.Unauthorized, typeof(SamsaraAuthenticationException))]
    [InlineData(HttpStatusCode.NotFound, typeof(SamsaraNotFoundException))]
    [InlineData((HttpStatusCode)429, typeof(SamsaraRateLimitException))]
    [InlineData(HttpStatusCode.InternalServerError, typeof(SamsaraServerException))]
    [InlineData(HttpStatusCode.BadGateway, typeof(SamsaraServerException))]
    [InlineData(HttpStatusCode.ServiceUnavailable, typeof(SamsaraServerException))]
    [InlineData(HttpStatusCode.GatewayTimeout, typeof(SamsaraServerException))]
    public void Create_ReturnsCorrectExceptionType(HttpStatusCode statusCode, Type expectedType)
    {
        var exception = SamsaraApiException.Create(statusCode, "test message", "req-1");

        exception.Should().BeOfType(expectedType);
        exception.StatusCode.Should().Be(statusCode);
        exception.Message.Should().Contain("test message");
        exception.RequestId.Should().Be("req-1");
    }

    [Fact]
    public void Create_ReturnsSamsaraApiException_ForUnmappedStatusCode()
    {
        var exception = SamsaraApiException.Create(HttpStatusCode.Conflict, "conflict", null);

        exception.Should().BeOfType<SamsaraApiException>();
        exception.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public void SamsaraRateLimitException_IncludesRetryAfter()
    {
        var ex = new SamsaraRateLimitException("rate limited", "r1", TimeSpan.FromSeconds(60));

        ex.RetryAfter.Should().Be(TimeSpan.FromSeconds(60));
        ex.StatusCode.Should().Be((HttpStatusCode)429);
    }

    [Fact]
    public void SamsaraApiException_WithInnerException_PreservesChain()
    {
        var inner = new HttpRequestException("Network error");
        var ex = new SamsaraApiException(HttpStatusCode.InternalServerError, "Server error", "r1", inner);

        ex.InnerException.Should().BeSameAs(inner);
        ex.Message.Should().Be("Server error");
    }
}
