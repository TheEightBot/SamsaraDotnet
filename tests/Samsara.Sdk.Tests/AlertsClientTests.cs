namespace Samsara.Sdk.Tests;

using System.Net;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Communication;
using Samsara.Sdk.Tests.Helpers;

public sealed class AlertsClientTests
{
    [Fact]
    public async Task CreateConfigurationAsync_PostsToCorrectPath()
    {
        // Spec marks id, name, isEnabled, scope, actions, triggers, createdAtTime,
        // and lastModifiedAtTime as REQUIRED on the response payload — the mock
        // payload must include all of them or System.Text.Json's `required` check
        // throws on deserialization.
        var resp = new
        {
            data = new
            {
                id = "cfg-new",
                name = "Speed Alert",
                isEnabled = true,
                scope = new { all = true },
                actions = new[] { new { actionTypeId = 1 } },
                triggers = new[] { new { triggerTypeId = 1000 } },
                createdAtTime = "2024-01-01T00:00:00Z",
                lastModifiedAtTime = "2024-01-01T00:00:00Z",
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        var cfg = await client.CreateConfigurationAsync(new CreateAlertConfigurationRequest
        {
            Name = "Speed Alert",
            IsEnabled = true,
            Scope = new AlertScope { All = true },
            Triggers = new[] { new AlertTrigger { TriggerTypeId = 1000 } },
            Actions = new[] { new AlertAction { ActionTypeId = 1 } },
        });

        cfg.Name.Should().Be("Speed Alert");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/configurations");
    }

    [Fact]
    public async Task UpdateConfigurationAsync_PatchesToCorrectPathWithIdInBody()
    {
        var resp = new
        {
            data = new
            {
                id = "cfg-1",
                name = "Updated Alert",
                isEnabled = true,
                scope = new { all = true },
                actions = new[] { new { actionTypeId = 1 } },
                triggers = new[] { new { triggerTypeId = 1000 } },
                createdAtTime = "2024-01-01T00:00:00Z",
                lastModifiedAtTime = "2024-01-02T00:00:00Z",
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        var cfg = await client.UpdateConfigurationAsync(new UpdateAlertConfigurationRequest
        {
            Id = "cfg-1",
            Name = "Updated Alert",
        });

        cfg.Name.Should().Be("Updated Alert");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        // Spec: PATCH /alerts/configurations (no id in URL — id is in the body).
        handler.LastRequest.RequestUri!.AbsolutePath.Should().EndWith("/alerts/configurations");
    }

    [Fact]
    public async Task DeleteConfigurationAsync_DeletesWithIdInQuery()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var client = new AlertsClient(TestFactory.CreateHttpClient(handler));

        await client.DeleteConfigurationAsync("cfg-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        // Spec: DELETE /alerts/configurations?id=cfg-1 (id in query, not path).
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("alerts/configurations");
        handler.LastRequest.RequestUri.Query.Should().Contain("id=cfg-1");
    }
}
