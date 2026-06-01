namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Documents;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the Forms domain (Phase 3). <see cref="FormTemplate"/> exposes
/// <c>title</c>/<c>revisionId</c> (NOT the old <c>name</c>), and <see cref="FormSubmission"/>
/// binds typed nested records (template reference, polymorphic submitter, score).
/// </summary>
public sealed class FormsContractTests
{
    // ── GET /form-templates ─────────────────────────────────────────────────
    [Fact]
    public async Task ListTemplatesAsync_BindsTitleAndRevisionId_NotName()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "tmpl-1",
                    title = "Pre-Trip Inspection",
                    description = "Daily pre-trip checklist",
                    revisionId = "11111111-1111-1111-1111-111111111111",
                    formCategory = "safety",
                    sections = Array.Empty<object>(),
                    createdAtTime = "2024-01-01T00:00:00Z",
                    updatedAtTime = "2024-01-02T00:00:00Z",
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new FormsClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListTemplatesAsync());

        items.Should().HaveCount(1);
        var template = items[0];
        template.Id.Should().Be("tmpl-1");
        // The reworked record uses `title`, not `name`.
        template.Title.Should().Be("Pre-Trip Inspection");
        template.RevisionId.Should().Be("11111111-1111-1111-1111-111111111111");
        template.FormCategory.Should().Be("safety");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("form-templates");
    }

    // ── GET /form-submissions ───────────────────────────────────────────────
    [Fact]
    public async Task ListSubmissionsAsync_BindsTypedNestedRecords()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "sub-1",
                    title = "Pre-Trip 2024-01-01",
                    status = "completed",
                    isRequired = true,
                    formTemplate = new { id = "tmpl-1", revisionId = "rev-1" },
                    submittedBy = new { id = "drv-1", type = "driver" },
                    score = new { maxPoints = 100.0, scorePercent = 95.0, scorePoints = 95.0 },
                    submittedAtTime = "2024-01-01T09:00:00Z",
                    createdAtTime = "2024-01-01T08:00:00Z",
                    updatedAtTime = "2024-01-01T09:00:00Z",
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new FormsClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListSubmissionsAsync(new[] { "sub-1" }));

        items.Should().HaveCount(1);
        var sub = items[0];
        sub.Id.Should().Be("sub-1");
        sub.Status.Should().Be("completed");
        sub.IsRequired.Should().BeTrue();
        // Typed nested template reference.
        sub.FormTemplate.Should().NotBeNull();
        sub.FormTemplate!.Id.Should().Be("tmpl-1");
        sub.FormTemplate.RevisionId.Should().Be("rev-1");
        // Typed polymorphic submitter.
        sub.SubmittedBy.Should().NotBeNull();
        sub.SubmittedBy!.Type.Should().Be("driver");
        // Typed score.
        sub.Score.Should().NotBeNull();
        sub.Score!.ScorePercent.Should().Be(95.0);

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("form-submissions");
        url.Should().Contain("ids=sub-1");
    }

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
