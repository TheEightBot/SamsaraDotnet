namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Configuration;

public sealed class SamsaraClientOptionsTests
{
    [Fact]
    public void DefaultValues_AreCorrect()
    {
        var options = new SamsaraClientOptions();

        options.BaseUrl.Should().Be("https://api.samsara.com");
        options.Timeout.Should().Be(TimeSpan.FromSeconds(30));
        options.RetryCount.Should().Be(3);
        options.DefaultPageSize.Should().BeNull();
        options.ApiToken.Should().BeNull();
        options.TokenProvider.Should().BeNull();
    }

    [Fact]
    public void EuBaseUrl_IsCorrect()
    {
        SamsaraClientOptions.EuBaseUrl.Should().Be("https://api.eu.samsara.com");
    }

    [Fact]
    public void SectionName_IsSamsara()
    {
        SamsaraClientOptions.SectionName.Should().Be("Samsara");
    }

    [Fact]
    public void DefaultBaseUrl_IsSamsara()
    {
        SamsaraClientOptions.DefaultBaseUrl.Should().Be("https://api.samsara.com");
    }
}
