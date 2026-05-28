namespace Samsara.Sdk.Tests;

using System.Net;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Tests.Helpers;

public sealed class VehiclesClientTests
{
    [Fact]
    public async Task GetAsync_CallsCorrectPath()
    {
        // Spec marks createdAtTime as REQUIRED on the Vehicle response payload — the
        // mock payload must include it or System.Text.Json's `required` check throws
        // on deserialization.
        var resp = new { data = new { id = "v-1", name = "Truck 1", createdAtTime = "2024-01-01T00:00:00Z" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.GetAsync("v-1");

        vehicle.Id.Should().Be("v-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles/v-1");
    }

    [Fact]
    public async Task UpdateAsync_PatchesToCorrectPath()
    {
        var resp = new { data = new { id = "v-1", name = "Updated Truck", createdAtTime = "2024-01-01T00:00:00Z" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.UpdateAsync("v-1", new UpdateVehicleRequest { Name = "Updated Truck" });

        vehicle.Name.Should().Be("Updated Truck");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles/v-1");
    }
}
