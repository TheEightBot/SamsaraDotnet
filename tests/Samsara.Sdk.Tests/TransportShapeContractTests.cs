namespace Samsara.Sdk.Tests;

using System.Globalization;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Compliance;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Models.Industrial;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Models.Safety;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests pinning the <b>transport shape</b> of four operations whose
/// success payload does not match the SDK's default envelope conventions.
/// <para>
/// Each of these was a live defect: the wrong HTTP helper was used, so the call
/// threw (or silently returned nothing) against a perfectly valid response. None
/// of them is caught by the model checkers — those compare record shapes, not the
/// envelope the helper expects — and none is caught by a plain "does it
/// deserialize" test, because the failure is in how the body is unwrapped. A
/// refactor that swaps the helper back would reintroduce the bug invisibly, hence
/// these tests.
/// </para>
/// </summary>
public sealed class TransportShapeContractTests
{
    // ── GET /v1/fleet/maintenance/list ──────────────────────────────────────
    // Spec: inline_response_200_4 == { vehicles: [...] }. No `data` array and no
    // `pagination` block, so PaginateAsync<T> binds SamsaraListResponse.Data to
    // null (lenient deserialization relaxes `required`) and the pagination loop
    // dereferences it -> NullReferenceException.
    [Fact]
    public async Task V1ListMaintenanceAsync_ReadsTheVehiclesWrapper_NotADataEnvelope()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            // object[] because the two elements are different anonymous types.
            vehicles = new object[]
            {
                new
                {
                    id = 1234L,
                    j1939 = new { checkEngineLight = new { emissionsIsOn = true } },
                },
                new { id = 5678L },
            },
        });
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        IReadOnlyList<V1VehicleMaintenance> vehicles = await client.V1ListMaintenanceAsync();

        vehicles.Should().HaveCount(2);
        vehicles[0].Id.Should().Be(1234L);
        vehicles[0].J1939!.CheckEngineLight!.EmissionsIsOn.Should().BeTrue();
        vehicles[1].Id.Should().Be(5678L);

        handler.LastRequest.Method.Should().Be(HttpMethod.Get);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("v1/fleet/maintenance/list");
        handler.Requests.Should().HaveCount(1, "the v1 body carries no pagination cursor to follow");
    }

    [Fact]
    public async Task V1ListMaintenanceAsync_ReturnsEmpty_WhenTheWrapperOmitsVehicles()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { });
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var vehicles = await client.V1ListMaintenanceAsync();

        vehicles.Should().BeEmpty();
    }

    // ── GET /v1/fleet/hos_authentication_logs ───────────────────────────────
    // Spec: V1HosAuthenticationLogsResponse == { authenticationLogs: [...] }.
    // Same defect shape as the maintenance list above.
    [Fact]
    public async Task V1ListHosAuthenticationLogsAsync_ReadsTheAuthenticationLogsWrapper()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            authenticationLogs = new[]
            {
                new
                {
                    actionType = "signIn",
                    address = "1 Main St",
                    addressName = "Depot",
                    city = "Scranton",
                    state = "PA",
                    happenedAtMs = 1_635_881_752_799L,
                },
            },
        });
        var client = new ComplianceClient(TestFactory.CreateHttpClient(handler));

        IReadOnlyList<V1HosAuthenticationLog> logs = await client.V1ListHosAuthenticationLogsAsync(
            driverId: 42,
            startTime: DateTimeOffset.FromUnixTimeMilliseconds(1_635_881_000_000),
            endTime: DateTimeOffset.FromUnixTimeMilliseconds(1_635_882_000_000));

        logs.Should().HaveCount(1);
        logs[0].ActionType.Should().Be("signIn");
        logs[0].AddressName.Should().Be("Depot");
        logs[0].HappenedAtMs.Should().Be(1_635_881_752_799L);

        var query = handler.LastRequest.RequestUri!.PathAndQuery;
        query.Should().Contain("v1/fleet/hos_authentication_logs");
        query.Should().Contain("driverId=42");
        query.Should().Contain("startMs=1635881000000");
        query.Should().Contain("endMs=1635882000000");
        handler.Requests.Should().HaveCount(1, "the v1 body carries no pagination cursor to follow");
    }

    [Fact]
    public async Task V1ListHosAuthenticationLogsAsync_ReturnsEmpty_WhenTheWrapperOmitsLogs()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { });
        var client = new ComplianceClient(TestFactory.CreateHttpClient(handler));

        var logs = await client.V1ListHosAuthenticationLogsAsync(driverId: 42);

        logs.Should().BeEmpty();
    }

    // ── PATCH /safety-events/batch ──────────────────────────────────────────
    // Spec: SafetyEventsV2PatchSafetyEventsV2BatchResponseBody puts
    // { requestId, responses } at the TOP level — there is no { data: ... }
    // envelope, so PatchDataAsync<T> unwraps a `data` that is not there.
    [Fact]
    public async Task PatchEventsBatchAsync_ReadsTheTopLevelPayload_NotADataEnvelope()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(
            new
            {
                requestId = "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
                responses = new[]
                {
                    new { status = 202L, data = new { safetyEventId = "evt-1" } },
                    new { status = 404L, data = new { safetyEventId = "evt-2" } },
                },
            },
            HttpStatusCode.Accepted);
        var client = new SafetyClient(TestFactory.CreateHttpClient(handler));

        SafetyEventsBatchResult result = await client.PatchEventsBatchAsync(new PatchSafetyEventsBatchRequest
        {
            SafetyEventIds = new[] { "evt-1", "evt-2" },
            EventState = "dismissed",
        });

        result.RequestId.Should().Be("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
        result.Responses.Should().HaveCount(2);
        result.Responses![0].Status.Should().Be(202L);
        result.Responses[0].Data!.SafetyEventId.Should().Be("evt-1");
        result.Responses[1].Status.Should().Be(404L);
        result.Responses[1].Data!.SafetyEventId.Should().Be("evt-2");

        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("safety-events/batch");
        handler.LastRequestBody.Should().Contain("evt-1");
    }

    // ── PATCH /beta/fleet/vehicles/{id}/immobilizer ─────────────────────────
    // Spec: the only success response is 202 with `content: {}` — literally no
    // body. Any helper that deserializes will throw on the empty payload.
    [Fact]
    public async Task UpdateImmobilizerStateAsync_SucceedsOnAnEmpty202()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.Accepted)
        {
            Content = new StringContent(string.Empty),
        });
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var act = () => client.UpdateImmobilizerStateAsync("veh-9", new UpdateEngineImmobilizerStateRequest
        {
            RelayStates = new[] { new EngineImmobilizerRelayStateInput { Id = "relay1", IsOpen = true } },
        });

        await act.Should().NotThrowAsync("the spec declares 202 with no content at all");

        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery
            .Should().Contain("beta/fleet/vehicles/veh-9/immobilizer");
        handler.LastRequestBody.Should().Contain("relay1");
    }

    [Fact]
    public void UpdateImmobilizerStateAsync_ReturnsANonGenericTask()
    {
        // A generic Task<T> here means a deserializing helper crept back in.
        typeof(IVehiclesClient)
            .GetMethod(nameof(IVehiclesClient.UpdateImmobilizerStateAsync))!
            .ReturnType
            .Should().Be(typeof(Task));
    }

    // ── GET /v1/fleet/trailers/assignments ──────────────────────────────────
    // Spec: inline_response_200_7 == { pagination, trailers: [...] }. Same class of
    // bug as V1ListMaintenanceAsync above — a top-level NAMED array with no `data`
    // member, so PaginateAsync<T> bound SamsaraListResponse.Data to null and the
    // pagination loop dereferenced it. This body previously threw on every call.
    [Fact]
    public async Task TrailerAssignments_ListAsync_ReadsTheTrailersWrapper_NotADataEnvelope()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            pagination = new
            {
                startCursor = "c0",
                endCursor = "c1",
                hasNextPage = false,
                hasPrevPage = false,
            },
            // object[] because the two elements are different anonymous types.
            trailers = new object[]
            {
                new
                {
                    id = 2041L,
                    name = "myTrailer",
                    trailerAssignments = new[]
                    {
                        new { driverId = 2047L, startMs = 1462878398034L, endMs = (long?)1462881998034L },
                        // A current assignment: the spec omits endMs entirely.
                        new { driverId = 3000L, startMs = 1462891998034L, endMs = (long?)null },
                    },
                },
                new { id = 2042L, name = "otherTrailer", trailerAssignments = Array.Empty<object>() },
            },
        });
        var client = new TrailerAssignmentsClient(TestFactory.CreateHttpClient(handler));

        var trailers = await CollectAsync(client.ListAsync());

        trailers.Should().HaveCount(2);
        trailers[0].Id.Should().Be(2041L);
        trailers[0].Name.Should().Be("myTrailer");

        var assignments = trailers[0].TrailerAssignments;
        assignments.Should().HaveCount(2);
        assignments![0].DriverId.Should().Be(2047L);
        assignments[0].StartMs.Should().Be(1462878398034L);
        assignments[1].EndMs.Should().BeNull();
        trailers[1].Id.Should().Be(2042L);

        handler.LastRequest.Method.Should().Be(HttpMethod.Get);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("v1/fleet/trailers/assignments");
        handler.Requests.Should().HaveCount(1, "hasNextPage was false");
    }

    [Fact]
    public async Task TrailerAssignments_ListAsync_ReturnsEmpty_WhenTheWrapperOmitsTrailers()
    {
        // The exact payload that used to NRE: no `trailers`, no `data`.
        var handler = MockHttpMessageHandler.WithJsonResponse(new { pagination = new { hasNextPage = false } });
        var client = new TrailerAssignmentsClient(TestFactory.CreateHttpClient(handler));

        var trailers = await CollectAsync(client.ListAsync());

        trailers.Should().BeEmpty();
    }

    [Fact]
    public async Task TrailerAssignments_ListAsync_FollowsTheCursorOn_startingAfter_NotAfter()
    {
        // V1getAllTrailerAssignments declares `startingAfter`, not the v2 `after`.
        // Sending the wrong name is worse than an NRE: the server ignores it and
        // re-serves page 1 with hasNextPage:true, so the enumeration never ends.
        var page1 = new
        {
            pagination = new { endCursor = "cursor-1", hasNextPage = true },
            trailers = new[] { new { id = 1L, name = "t1" } },
        };
        var page2 = new
        {
            pagination = new { endCursor = "cursor-2", hasNextPage = false },
            trailers = new[] { new { id = 2L, name = "t2" } },
        };
        // Serve page 2 for ANY cursor spelling, so a regression to `after=` fails the
        // assertions below rather than looping forever on page 1.
        var handler = new MockHttpMessageHandler((req, _) =>
        {
            var body = req.RequestUri!.Query.Contains("ursor-1") ? (object)page2 : page1;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(body),
                    System.Text.Encoding.UTF8,
                    "application/json"),
            });
        });
        var client = new TrailerAssignmentsClient(TestFactory.CreateHttpClient(handler));

        var trailers = await CollectAsync(client.ListAsync());

        trailers.Select(t => t.Id).Should().ContainInOrder(1L, 2L);
        handler.Requests.Should().HaveCount(2);
        handler.Requests[1].RequestUri!.Query.Should().Contain("startingAfter=cursor-1");
        handler.Requests[1].RequestUri!.Query.Should().NotContain("after=cursor-1");
    }

    // ── GET /v1/fleet/trailers/{trailerId}/assignments ──────────────────────
    // Spec returns a single V1TrailerAssignmentsResponse — no pagination block and
    // no cursor parameters — so this must be a plain GET, not a paginated stream.
    [Fact]
    public async Task TrailerAssignments_GetByTrailerAsync_ReadsASingleObject_NotAPage()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            id = 2041L,
            name = "myTrailer",
            trailerAssignments = new[] { new { driverId = 2047L, startMs = 1462878398034L } },
        });
        var client = new TrailerAssignmentsClient(TestFactory.CreateHttpClient(handler));

        var trailer = await client.GetByTrailerAsync("2041");

        trailer.Id.Should().Be(2041L);
        trailer.TrailerAssignments.Should().ContainSingle();
        handler.Requests.Should().HaveCount(1);
        handler.LastRequest.RequestUri!.PathAndQuery
            .Should().Contain("v1/fleet/trailers/2041/assignments");
    }

    // ── POST /readings ──────────────────────────────────────────────────────
    // Spec: requestBody is { data: [ReadingDatapointRequestBody] } and the success
    // response is 201 with `content: {}` — literally empty. The old signature was
    // Task<object> CreateAsync(object), i.e. PostAsync<object>, which deserialized
    // the empty payload and threw.
    [Fact]
    public async Task Readings_CreateAsync_PostsTheTypedEnvelope_AndAcceptsAnEmpty201()
    {
        var handler = new MockHttpMessageHandler(
            new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(string.Empty) });
        var client = new ReadingsClient(TestFactory.CreateHttpClient(handler));

        var request = new CreateReadingsRequest
        {
            Data =
            [
                new ReadingDatapoint
                {
                    EntityId = "123451234512345",
                    EntityType = "asset",
                    HappenedAtTime = DateTimeOffset.Parse("2023-10-27T10:00:00Z", CultureInfo.InvariantCulture),
                    ReadingId = "engineState",
                    Value = JsonDocument.Parse("\"off\"").RootElement,
                },
            ],
        };

        Func<Task> act = () => client.CreateAsync(request);

        await act.Should().NotThrowAsync("the spec declares 201 with an empty content block");

        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("/readings");
        // The body must carry the spec's { data: [...] } envelope, not a bare datapoint.
        handler.LastRequestBody.Should().Contain("\"data\"");
        handler.LastRequestBody.Should().Contain("\"readingId\":\"engineState\"");
        handler.LastRequestBody.Should().Contain("\"entityType\":\"asset\"");
    }

    [Fact]
    public void Readings_CreateAsync_ReturnsANonGenericTask_AndTakesATypedRequest()
    {
        // Task<object>/object here means the untyped body crept back in.
        var method = typeof(IReadingsClient).GetMethod(nameof(IReadingsClient.CreateAsync))!;
        method.ReturnType.Should().Be(typeof(Task));
        method.GetParameters()[0].ParameterType.Should().Be(typeof(CreateReadingsRequest));
    }

    private static async Task<IReadOnlyList<T>> CollectAsync<T>(IAsyncEnumerable<T> source)
    {
        var items = new List<T>();
        await foreach (var item in source)
        {
            items.Add(item);
        }

        return items;
    }
}
