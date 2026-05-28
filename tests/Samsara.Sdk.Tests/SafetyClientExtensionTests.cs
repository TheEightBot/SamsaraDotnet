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
            data = new[]
            {
                new
                {
                    tagId = "tag-1",
                    tagName = "Region A",
                    safetyScore = 88.5,
                    tagScore = 89,
                    behaviors = Array.Empty<object>(),
                    speeding = Array.Empty<object>(),
                    driveDistanceMeters = 1000L,
                    driveTimeMilliseconds = 60000L,
                },
            },
            pagination = new { endCursor = (string?)null, hasNextPage = false }
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new SafetyClient(TestFactory.CreateHttpClient(handler));

        var scores = new List<Samsara.Sdk.Models.Safety.TagSafetyScore>();
        await foreach (var s in client.ListTagSafetyScoresAsync("driver"))
            scores.Add(s);

        scores.Should().HaveCount(1);
        scores[0].TagId.Should().Be("tag-1");
        scores[0].TagScore.Should().Be(89);
        scores[0].SafetyScore.Should().Be(88.5);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("safety-scores/tags");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("scoreType=driver");
    }

    [Fact]
    public async Task ListTagGroupSafetyScoresAsync_CallsCorrectPath()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    tagGroupId = "tg-1",
                    tagGroupName = "Division West",
                    safetyScore = 91.0,
                    combinedScore = 92,
                    behaviors = Array.Empty<object>(),
                    speeding = Array.Empty<object>(),
                    driveDistanceMeters = 2000L,
                    driveTimeMilliseconds = 120000L,
                },
            },
            pagination = new { endCursor = (string?)null, hasNextPage = false }
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new SafetyClient(TestFactory.CreateHttpClient(handler));

        var scores = new List<Samsara.Sdk.Models.Safety.TagGroupSafetyScore>();
        await foreach (var s in client.ListTagGroupSafetyScoresAsync("vehicle"))
            scores.Add(s);

        scores.Should().HaveCount(1);
        scores[0].TagGroupId.Should().Be("tg-1");
        scores[0].CombinedScore.Should().Be(92);
        scores[0].SafetyScore.Should().Be(91.0);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("safety-scores/tag-group");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("scoreType=vehicle");
    }
}
