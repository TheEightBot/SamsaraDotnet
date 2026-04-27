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
        var resp = new { data = new { id = "v-1", name = "Truck 1" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.GetAsync("v-1");

        vehicle.Id.Should().Be("v-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles/v-1");
    }

    [Fact]
    public async Task CreateAsync_PostsToCorrectPath()
    {
        var resp = new { data = new { id = "v-new", name = "New Truck" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.CreateAsync(new CreateVehicleRequest { Name = "New Truck" });

        vehicle.Name.Should().Be("New Truck");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles");
    }

    [Fact]
    public async Task UpdateAsync_PatchesToCorrectPath()
    {
        var resp = new { data = new { id = "v-1", name = "Updated Truck" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.UpdateAsync("v-1", new UpdateVehicleRequest { Name = "Updated Truck" });

        vehicle.Name.Should().Be("Updated Truck");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles/v-1");
    }

    [Fact]
    public async Task DeleteAsync_DeletesCorrectPath()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        await client.DeleteAsync("v-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles/v-1");
    }
}
