namespace Samsara.Sdk.Tests;

using System.Net;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Tests.Helpers;

public sealed class EquipmentClientTests
{
    [Fact]
    public async Task GetAsync_CallsCorrectPath()
    {
        var resp = new { data = new { id = "eq-1", name = "Trailer A" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new EquipmentClient(TestFactory.CreateHttpClient(handler));

        var equipment = await client.GetAsync("eq-1");

        equipment.Id.Should().Be("eq-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/equipment/eq-1");
    }

    [Fact]
    public async Task CreateAsync_PostsToCorrectPath()
    {
        var resp = new { data = new { id = "eq-new", name = "New Trailer" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new EquipmentClient(TestFactory.CreateHttpClient(handler));

        var equipment = await client.CreateAsync(new CreateEquipmentRequest { Name = "New Trailer" });

        equipment.Name.Should().Be("New Trailer");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/equipment");
    }

    [Fact]
    public async Task UpdateAsync_PatchesToCorrectPath()
    {
        var resp = new { data = new { id = "eq-1", name = "Updated Trailer" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new EquipmentClient(TestFactory.CreateHttpClient(handler));

        var equipment = await client.UpdateAsync("eq-1", new UpdateEquipmentRequest { Name = "Updated Trailer" });

        equipment.Name.Should().Be("Updated Trailer");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/equipment/eq-1");
    }

    [Fact]
    public async Task DeleteAsync_DeletesCorrectPath()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var client = new EquipmentClient(TestFactory.CreateHttpClient(handler));

        await client.DeleteAsync("eq-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/equipment/eq-1");
    }
}
