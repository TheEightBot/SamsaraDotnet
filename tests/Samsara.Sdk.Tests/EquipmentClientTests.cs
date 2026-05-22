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
    public async Task UpdateAsync_PatchesToCorrectPath()
    {
        var resp = new { data = new { id = "eq-1", name = "Updated Trailer" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new EquipmentClient(TestFactory.CreateHttpClient(handler));

        var equipment = await client.UpdateAsync("eq-1", new UpdateEquipmentRequest { Name = "Updated Trailer" });

        equipment.Name.Should().Be("Updated Trailer");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("beta/fleet/equipment/eq-1");
    }
}
