namespace Samsara.Sdk.Tests;

using System.Net;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Compliance;
using Samsara.Sdk.Models.Fleet;
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
}
