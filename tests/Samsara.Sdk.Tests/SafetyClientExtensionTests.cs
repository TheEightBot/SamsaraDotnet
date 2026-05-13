namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Tests.Helpers;

public sealed class SafetyClientExtensionTests
{
    [Fact]
    public async Task ListTagSafetyScoresAsync_CallsCorrectPath()
    {
        var resp = new
        {
            data = new[] { new { tagId = "tag-1", tagName = "Region A", safetyScore = 88.5 } },
            pagination = new { endCursor = (string?)null, hasNextPage = false }
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new SafetyClient(TestFactory.CreateHttpClient(handler));

        var scores = new List<Samsara.Sdk.Models.Safety.TagSafetyScore>();
        await foreach (var s in client.ListTagSafetyScoresAsync())
            scores.Add(s);

        scores.Should().HaveCount(1);
        scores[0].TagId.Should().Be("tag-1");
        scores[0].SafetyScore.Should().Be(88.5);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("safety-scores/tags");
    }

    [Fact]
    public async Task ListTagGroupSafetyScoresAsync_CallsCorrectPath()
    {
        var resp = new
        {
            data = new[] { new { tagGroupId = "tg-1", tagGroupName = "Division West", safetyScore = 91.0 } },
            pagination = new { endCursor = (string?)null, hasNextPage = false }
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new SafetyClient(TestFactory.CreateHttpClient(handler));

        var scores = new List<Samsara.Sdk.Models.Safety.TagGroupSafetyScore>();
        await foreach (var s in client.ListTagGroupSafetyScoresAsync())
            scores.Add(s);

        scores.Should().HaveCount(1);
        scores[0].TagGroupId.Should().Be("tg-1");
        scores[0].SafetyScore.Should().Be(91.0);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("safety-scores/tag-group");
    }
}
