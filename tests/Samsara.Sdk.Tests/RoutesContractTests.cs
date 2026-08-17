namespace Samsara.Sdk.Tests;

using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Routes;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the route-stop upsert tree the API accepts on write and the
/// richer stop object it returns on read.
/// </summary>
/// <remarks>
/// The two halves are deliberately asymmetric: <c>POST /fleet/routes</c> and
/// <c>PATCH /fleet/routes/{id}</c> accept a full <c>stops[].orders[].tasks[]</c> tree
/// (<c>RouteStopOrderUpsertInputRequestBody</c>), while <c>GET /fleet/routes</c>
/// returns only <c>{ id, taskId }</c> references
/// (<c>RouteStopOrderTaskReferenceObjectResponseBody</c>).
/// </remarks>
public sealed class RoutesContractTests
{
    // ── POST /fleet/routes — request serialization ──────────────────────────
    [Fact]
    public async Task CreateAsync_NestsOrdersAndTasksUnderStops()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { data = new { id = "route-1" } });
        var client = new RoutesClient(TestFactory.CreateHttpClient(handler));

        await client.CreateAsync(new CreateRouteRequest
        {
            Name = "Morning Run",
            Stops = new[]
            {
                new CreateRouteStopRequest
                {
                    Name = "Stop 1",
                    AddressId = "addr-1",
                    Orders = new[]
                    {
                        new RouteStopOrderInput
                        {
                            Id = "order-1",
                            SamsaraCustomerOrderName = "PO-4711",
                            ExternalIds = new Dictionary<string, string> { ["tmsOrder"] = "12345" },
                            CustomerProperties = new[]
                            {
                                new FleetOrderCustomerPropertyInput { Key = "dock", Value = "B" },
                            },
                            Tasks = new[]
                            {
                                new FleetOrderTaskInput
                                {
                                    TaskType = "delivery",
                                    HubId = "hub-1",
                                    PositionConstraintType = "first",
                                    DispatcherNotes = "call ahead",
                                    ServiceDurationSeconds = 900,
                                    ServiceWindowIdsToRemove = new[] { "sw-old" },
                                    Quantities = new[]
                                    {
                                        new FleetOrderQuantityInput
                                        {
                                            Label = "pallets",
                                            Value = 3.5,
                                            CapacityId = "cap-1",
                                        },
                                    },
                                    ServiceWindows = new[]
                                    {
                                        new FleetOrderServiceWindowInput
                                        {
                                            StartTime = new DateTimeOffset(2024, 1, 1, 8, 0, 0, TimeSpan.Zero),
                                            EndTime = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero),
                                        },
                                    },
                                    ServiceLocation = new FleetOrderServiceLocationInput
                                    {
                                        ServiceLocationType = "customAddress",
                                        CustomAddress = new FleetOrderCustomAddressInput
                                        {
                                            AddressLine1 = "1 Market St",
                                            City = "San Francisco",
                                            State = "CA",
                                            PostalCode = "94105",
                                            Country = "US",
                                            Latitude = 37.7749,
                                            Longitude = -122.4194,
                                        },
                                    },
                                },
                            },
                        },
                    },
                    AppointmentWindows = new[]
                    {
                        new RouteStopAppointmentWindowInput
                        {
                            StartTime = "2024-01-01T08:00:00Z",
                            EndTime = "2024-01-01T12:00:00Z",
                        },
                    },
                    Forms = new[]
                    {
                        new RouteStopFormInput { FormTemplateId = "tmpl-1", IsRequired = true },
                    },
                },
            },
        });

        // LastRequestBody, not LastRequest.Content: HttpClient disposes request content
        // once the request completes.
        using var doc = JsonDocument.Parse(handler.LastRequestBody!);
        var stop = doc.RootElement.GetProperty("stops")[0];

        var order = stop.GetProperty("orders")[0];
        order.GetProperty("id").GetString().Should().Be("order-1");
        order.GetProperty("samsaraCustomerOrderName").GetString().Should().Be("PO-4711");
        order.GetProperty("externalIds").GetProperty("tmsOrder").GetString().Should().Be("12345");
        order.GetProperty("customerProperties")[0].GetProperty("key").GetString().Should().Be("dock");

        var task = order.GetProperty("tasks")[0];
        task.GetProperty("taskType").GetString().Should().Be("delivery");
        task.GetProperty("hubId").GetString().Should().Be("hub-1");
        task.GetProperty("positionConstraintType").GetString().Should().Be("first");
        task.GetProperty("serviceDurationSeconds").GetInt32().Should().Be(900);
        task.GetProperty("serviceWindowIdsToRemove")[0].GetString().Should().Be("sw-old");

        var quantity = task.GetProperty("quantities")[0];
        quantity.GetProperty("label").GetString().Should().Be("pallets");
        quantity.GetProperty("value").GetDouble().Should().Be(3.5);
        quantity.GetProperty("capacityId").GetString().Should().Be("cap-1");

        task.GetProperty("serviceWindows")[0].GetProperty("startTime").GetString()
            .Should().StartWith("2024-01-01T08:00:00");

        var location = task.GetProperty("serviceLocation");
        location.GetProperty("serviceLocationType").GetString().Should().Be("customAddress");
        location.GetProperty("customAddress").GetProperty("addressLine1").GetString()
            .Should().Be("1 Market St");
        location.GetProperty("customAddress").GetProperty("latitude").GetDouble().Should().Be(37.7749);

        // Unset optional members are omitted, not written as null.
        task.TryGetProperty("driverNotes", out _).Should().BeFalse();
        location.TryGetProperty("addressId", out _).Should().BeFalse();

        var window = stop.GetProperty("appointmentWindows")[0];
        window.GetProperty("startTime").GetString().Should().Be("2024-01-01T08:00:00Z");
        window.GetProperty("endTime").GetString().Should().Be("2024-01-01T12:00:00Z");

        var form = stop.GetProperty("forms")[0];
        form.GetProperty("formTemplateId").GetString().Should().Be("tmpl-1");
        form.GetProperty("isRequired").GetBoolean().Should().BeTrue();
    }

    // ── PATCH /fleet/routes/{id} — same child shapes as CREATE ──────────────
    [Fact]
    public async Task UpdateAsync_NestsTheSameOrderTreeUnderStops()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { data = new { id = "route-1" } });
        var client = new RoutesClient(TestFactory.CreateHttpClient(handler));

        await client.UpdateAsync("route-1", new UpdateRouteRequest
        {
            Stops = new[]
            {
                new UpdateRouteStopRequest
                {
                    Id = "stop-1",
                    Orders = new[]
                    {
                        new RouteStopOrderInput
                        {
                            Id = "order-1",
                            Tasks = new[]
                            {
                                new FleetOrderTaskInput
                                {
                                    Id = "task-1",
                                    TaskType = "pickupDelivery",
                                    ServiceLocation = new FleetOrderServiceLocationInput
                                    {
                                        ServiceLocationType = "savedAddress",
                                        AddressId = "addr-9",
                                    },
                                },
                            },
                        },
                    },
                    AppointmentWindows = new[]
                    {
                        new RouteStopAppointmentWindowInput
                        {
                            StartTime = "2024-02-01T08:00:00Z",
                            EndTime = "2024-02-01T10:00:00Z",
                        },
                    },
                    Forms = new[] { new RouteStopFormInput { FormTemplateId = "tmpl-2" } },
                },
            },
        });

        using var doc = JsonDocument.Parse(handler.LastRequestBody!);
        var stop = doc.RootElement.GetProperty("stops")[0];
        stop.GetProperty("id").GetString().Should().Be("stop-1");

        var task = stop.GetProperty("orders")[0].GetProperty("tasks")[0];
        task.GetProperty("id").GetString().Should().Be("task-1");
        task.GetProperty("taskType").GetString().Should().Be("pickupDelivery");
        task.GetProperty("serviceLocation").GetProperty("addressId").GetString().Should().Be("addr-9");

        stop.GetProperty("appointmentWindows")[0].GetProperty("endTime").GetString()
            .Should().Be("2024-02-01T10:00:00Z");
        stop.GetProperty("forms")[0].GetProperty("formTemplateId").GetString().Should().Be("tmpl-2");
    }

    // ── GET /fleet/routes — response binding ────────────────────────────────
    [Fact]
    public async Task ListAsync_BindsRouteStopAppointmentWindowsDocumentsFormsIssuesAndLinks()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "route-1",
                    stops = new[]
                    {
                        new
                        {
                            id = "stop-1",
                            name = "Stop 1",
                            state = "scheduled",
                            // Read side returns references, not the upsert tree.
                            orders = new[] { new { id = "order-1", taskId = "task-1" } },
                            appointmentWindows = new[]
                            {
                                new { startTime = "2024-01-01T08:00:00Z", endTime = "2024-01-01T12:00:00Z" },
                            },
                            documents = new[] { new { id = "doc-1", name = "BOL" } },
                            forms = new[]
                            {
                                new { id = "sub-1", formTemplateId = "tmpl-1", isRequired = true },
                            },
                            issues = new[] { new { id = "issue-1" } },
                            locationLiveSharingLinks = new[]
                            {
                                new
                                {
                                    name = "Stop link",
                                    liveSharingUrl = "https://samsara.com/s/abc",
                                    expiresAtTime = "2024-01-02T00:00:00Z",
                                },
                            },
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new RoutesClient(TestFactory.CreateHttpClient(handler));

        var routes = await CollectAsync(client.ListAsync(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero)));

        var stop = routes.Should().ContainSingle().Subject.Stops.Should().ContainSingle().Subject;

        stop.Orders.Should().ContainSingle().Which.TaskId.Should().Be("task-1");

        var window = stop.AppointmentWindows.Should().ContainSingle().Subject;
        window.StartTime.Should().Be("2024-01-01T08:00:00Z");
        window.EndTime.Should().Be("2024-01-01T12:00:00Z");

        var document = stop.Documents.Should().ContainSingle().Subject;
        document.Id.Should().Be("doc-1");
        document.Name.Should().Be("BOL");

        var form = stop.Forms.Should().ContainSingle().Subject;
        form.Id.Should().Be("sub-1");
        form.FormTemplateId.Should().Be("tmpl-1");
        form.IsRequired.Should().BeTrue();

        stop.Issues.Should().ContainSingle().Which.Id.Should().Be("issue-1");

        var link = stop.LocationLiveSharingLinks.Should().ContainSingle().Subject;
        link.Name.Should().Be("Stop link");
        link.LiveSharingUrl.Should().Be("https://samsara.com/s/abc");
        link.ExpiresAtTime.Should().Be("2024-01-02T00:00:00Z");
    }

    private static async Task<List<T>> CollectAsync<T>(IAsyncEnumerable<T> source)
    {
        var items = new List<T>();
        await foreach (var item in source)
        {
            items.Add(item);
        }

        return items;
    }
}
