namespace Samsara.Sdk.Tests;

using System.Net;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Communication;
using Samsara.Sdk.Tests.Helpers;

public sealed class AlertsClientTests
{
    [Fact]
    public async Task GetAsync_CallsCorrectPath()
    {
        var resp = new { data = new { id = "a-1", configurationId = "cfg-1" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        var alert = await client.GetAsync("a-1");

        alert.Id.Should().Be("a-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/a-1");
    }

    [Fact]
    public async Task CreateAsync_PostsToCorrectPath()
    {
        var resp = new { data = new { id = "a-new", configurationId = "cfg-1" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        var alert = await client.CreateAsync(new CreateAlertRequest { ConfigurationId = "cfg-1" });

        alert.ConfigurationId.Should().Be("cfg-1");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts");
    }

    [Fact]
    public async Task UpdateAsync_PatchesToCorrectPath()
    {
        var resp = new { data = new { id = "a-1", configurationId = "cfg-1" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        var alert = await client.UpdateAsync("a-1", new UpdateAlertRequest());

        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/a-1");
    }

    [Fact]
    public async Task DeleteAsync_DeletesCorrectPath()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        await client.DeleteAsync("a-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/a-1");
    }

    [Fact]
    public async Task CreateConfigurationAsync_PostsToCorrectPath()
    {
        var resp = new { data = new { id = "cfg-new", name = "Speed Alert", conditionType = "speeding" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        var cfg = await client.CreateConfigurationAsync(new CreateAlertConfigurationRequest
        {
            Name = "Speed Alert",
            ConditionType = "speeding"
        });

        cfg.Name.Should().Be("Speed Alert");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/configurations");
    }

    [Fact]
    public async Task UpdateConfigurationAsync_PatchesToCorrectPath()
    {
        var resp = new { data = new { id = "cfg-1", name = "Updated Alert" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        var cfg = await client.UpdateConfigurationAsync("cfg-1", new UpdateAlertConfigurationRequest { Name = "Updated Alert" });

        cfg.Name.Should().Be("Updated Alert");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/configurations/cfg-1");
    }

    [Fact]
    public async Task DeleteConfigurationAsync_DeletesCorrectPath()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        await client.DeleteConfigurationAsync("cfg-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/configurations/cfg-1");
    }

    [Fact]
    public async Task GetIncidentAsync_CallsCorrectPath()
    {
        var resp = new { data = new { id = "inc-1", alertId = "a-1" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        var incident = await client.GetIncidentAsync("inc-1");

        incident.Id.Should().Be("inc-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/incidents/inc-1");
    }
}
