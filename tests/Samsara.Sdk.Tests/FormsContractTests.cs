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

    // ── GET /form-templates — typed field definitions + approval requirements ──
    [Fact]
    public async Task ListTemplatesAsync_BindsTypedFieldDefinitionsAndApprovalRequirements()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "tmpl-1",
                    title = "Pre-Trip Inspection",
                    approvalConfig = new
                    {
                        type = "single",
                        singleApprovalConfig = new
                        {
                            allowManualApproverSelection = true,
                            requirements = new { roleIds = new[] { "role-1", "role-2" } },
                        },
                    },
                    fields = new object[]
                    {
                        new
                        {
                            id = "fld-1",
                            label = "Tyre condition",
                            type = "multiple_choice",
                            isRequired = true,
                            questionWeight = 5L,
                            options = new[]
                            {
                                new { id = "opt-1", label = "Good", optionScoreWeight = 5L },
                            },
                            conditionalActions = new[]
                            {
                                new
                                {
                                    condition = new
                                    {
                                        type = "multipleChoiceValueCondition",
                                        selectedOptionIds = new[] { "opt-1" },
                                    },
                                    actions = new[]
                                    {
                                        new { type = "askFollowupQuestion", fieldId = "fld-2" },
                                    },
                                },
                            },
                        },
                        new
                        {
                            id = "fld-3",
                            label = "Axle readings",
                            type = "table",
                            isRequired = false,
                            columns = new[]
                            {
                                new { id = "col-1", label = "PSI", type = "number", numDecimalPlaces = 2L },
                            },
                        },
                    },
                    sections = Array.Empty<object>(),
                    createdAtTime = "2024-01-01T00:00:00Z",
                    updatedAtTime = "2024-01-02T00:00:00Z",
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new FormsClient(TestFactory.CreateHttpClient(handler));

        var template = (await CollectAsync(client.ListTemplatesAsync()))[0];

        // requirements was JsonElement — the nested roleIds bound nothing typed.
        template.ApprovalConfig!.SingleApprovalConfig!.Requirements.Should().NotBeNull();
        template.ApprovalConfig.SingleApprovalConfig.Requirements!.RoleIds
            .Should().ContainInOrder("role-1", "role-2");

        template.Fields.Should().HaveCount(2);
        var choice = template.Fields![0];
        choice.Label.Should().Be("Tyre condition");
        choice.Type.Should().Be("multiple_choice");
        choice.IsRequired.Should().BeTrue();
        choice.QuestionWeight.Should().Be(5);
        choice.Options.Should().ContainSingle().Which.Label.Should().Be("Good");
        var conditional = choice.ConditionalActions.Should().ContainSingle().Subject;
        conditional.Condition!.Type.Should().Be("multipleChoiceValueCondition");
        conditional.Condition.SelectedOptionIds.Should().ContainSingle().Which.Should().Be("opt-1");
        conditional.Actions.Should().ContainSingle().Which.FieldId.Should().Be("fld-2");

        var table = template.Fields[1];
        table.Columns.Should().ContainSingle().Which.NumDecimalPlaces.Should().Be(2);
    }

    // ── GET /form-submissions — typed field inputs (value union) ────────────
    [Fact]
    public async Task ListSubmissionsAsync_BindsTypedFieldInputs()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "sub-1",
                    status = "completed",
                    isRequired = true,
                    durationMs = 91_000L,
                    submittedAtTime = "2024-01-01T09:00:00Z",
                    createdAtTime = "2024-01-01T08:00:00Z",
                    updatedAtTime = "2024-01-01T09:00:00Z",
                    fields = new object[]
                    {
                        new
                        {
                            id = "fld-1",
                            type = "multiple_choice",
                            label = "Tyre condition",
                            note = "Left front worn",
                            multipleChoiceValue = new { value = "Good", valueId = "opt-1" },
                            issue = new { id = "iss-1" },
                        },
                        new
                        {
                            id = "fld-2",
                            type = "table",
                            tableValue = new
                            {
                                columns = new[] { new { id = "col-1", label = "PSI", type = "number" } },
                                rows = new[]
                                {
                                    new
                                    {
                                        id = "row-1",
                                        cells = new[]
                                        {
                                            new { id = "cell-1", type = "number", numberValue = new { value = 32.5 } },
                                        },
                                    },
                                },
                            },
                        },
                        new
                        {
                            id = "fld-3",
                            type = "signature",
                            signatureValue = new
                            {
                                media = new
                                {
                                    id = "med-1",
                                    processingStatus = "finished",
                                    url = "https://media.samsara.com/sig.png",
                                    urlExpiresAt = "2024-01-02T09:00:00Z",
                                },
                            },
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new FormsClient(TestFactory.CreateHttpClient(handler));

        var sub = (await CollectAsync(client.ListSubmissionsAsync(new[] { "sub-1" })))[0];

        sub.DurationMs.Should().Be(91_000);
        sub.Fields.Should().HaveCount(3);

        var choice = sub.Fields![0];
        choice.Label.Should().Be("Tyre condition");
        choice.Note.Should().Be("Left front worn");
        choice.MultipleChoiceValue!.ValueId.Should().Be("opt-1");
        choice.Issue!.Id.Should().Be("iss-1");

        var cell = sub.Fields[1].TableValue!.Rows.Should().ContainSingle().Subject
            .Cells.Should().ContainSingle().Subject;
        cell.Type.Should().Be("number");
        cell.NumberValue!.Value.Should().Be(32.5);

        var media = sub.Fields[2].SignatureValue!.Media!;
        media.ProcessingStatus.Should().Be("finished");
        media.UrlExpiresAt.Should().Be(new DateTimeOffset(2024, 1, 2, 9, 0, 0, TimeSpan.Zero));
    }

    // ── POST /form-submissions — typed field inputs serialize ──────────────
    [Fact]
    public async Task CreateSubmissionAsync_SerializesTypedFieldInputs()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            data = new
            {
                id = "sub-1",
                status = "notStarted",
                isRequired = false,
                submittedAtTime = "2024-01-01T09:00:00Z",
                createdAtTime = "2024-01-01T08:00:00Z",
                updatedAtTime = "2024-01-01T09:00:00Z",
            },
        });
        var client = new FormsClient(TestFactory.CreateHttpClient(handler));

        await client.CreateSubmissionAsync(new CreateFormSubmissionRequest
        {
            FormTemplate = new FormTemplateRequest { Id = "tmpl-1" },
            Status = "notStarted",
            Fields = new[]
            {
                new FormSubmissionRequestFieldInput
                {
                    Id = "fld-1",
                    Type = "text",
                    TextValue = new FormSubmissionRequestTextValue { Value = "All good" },
                },
            },
        });

        var body = handler.LastRequestBody!;
        body.Should().Contain("\"fields\"");
        body.Should().Contain("\"textValue\"");
        body.Should().Contain("All good");
    }

    // ── PATCH /form-submissions — `fields` was absent from the record ───────
    [Fact]
    public async Task UpdateSubmissionAsync_SerializesTypedFieldInputs()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            data = new
            {
                id = "sub-1",
                status = "inProgress",
                isRequired = false,
                submittedAtTime = "2024-01-01T09:00:00Z",
                createdAtTime = "2024-01-01T08:00:00Z",
                updatedAtTime = "2024-01-01T09:00:00Z",
            },
        });
        var client = new FormsClient(TestFactory.CreateHttpClient(handler));

        await client.UpdateSubmissionAsync(new UpdateFormSubmissionRequest
        {
            Id = "sub-1",
            Status = "inProgress",
            Fields = new[]
            {
                new FormSubmissionRequestFieldInput
                {
                    Id = "fld-1",
                    Type = "check_boxes",
                    CheckBoxesValue = new FormSubmissionRequestCheckBoxesValue
                    {
                        ValueIds = new[] { "opt-1", "opt-2" },
                    },
                },
                new FormSubmissionRequestFieldInput
                {
                    Id = "fld-2",
                    Type = "asset",
                    AssetValue = new FormSubmissionRequestAssetValue
                    {
                        Asset = new FormSubmissionRequestAsset { Id = "veh-1" },
                    },
                },
            },
        });

        // Before the 2026-08-17 sweep UpdateFormSubmissionRequest had no `fields`
        // member at all, so answers could never be written on PATCH.
        var body = handler.LastRequestBody!;
        body.Should().Contain("\"fields\"");
        body.Should().Contain("\"valueIds\"");
        body.Should().Contain("opt-2");
        body.Should().Contain("\"assetValue\"");
        body.Should().Contain("veh-1");
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
