namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the Settings domain (Phase 3). <c>GET /fleet/settings/safety</c>
/// and <c>.../driver-app</c> bind typed nested records — the safety-score weight
/// configuration, the speeding/voice-coaching alert sub-objects, and the driver-app
/// gamification/trailer-selection configs.
/// </summary>
public sealed class SettingsContractTests
{
    // ── GET /fleet/settings/safety ──────────────────────────────────────────
    [Fact]
    public async Task GetSafetySettingsAsync_BindsTypedNestedRecords()
    {
        // SafetySettings has many spec-required members; the payload must populate
        // them all or System.Text.Json's `required` check throws.
        var resp = new
        {
            data = new
            {
                defaultVehicleType = "truck",
                distractedDrivingDetectionAlerts = new { isEnabled = true },
                followingDistanceDetectionAlerts = new { isEnabled = true, durationMs = 2000L, speedingThresholdMph = 45.0 },
                forwardCollisionDetectionAlerts = new { isEnabled = true, sensitivity = "medium" },
                harshEventSensitivity = new { },
                harshEventSensitivityV2 = new { },
                policyViolationsDetectionAlerts = new { isEnabled = false },
                rollingStopDetectionAlerts = new { isEnabled = true, speedingThresholdMph = 5.0 },
                safetyScoreConfiguration = new { crashWeight = 1000, harshBrakeWeight = 10, speedingWeight = 5 },
                safetyScoreTarget = 95L,
                speedingSettings = new { unit = "milesPerHour", severityLevels = Array.Empty<object>() },
                voiceCoaching = new { isEnabled = true, language = "english" },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new SettingsClient(TestFactory.CreateHttpClient(handler));

        var settings = await client.GetSafetySettingsAsync();

        settings.DefaultVehicleType.Should().Be("truck");
        settings.SafetyScoreTarget.Should().Be(95L);
        settings.ForwardCollisionDetectionAlerts.Sensitivity.Should().Be("medium");
        settings.FollowingDistanceDetectionAlerts.DurationMs.Should().Be(2000L);
        // Typed safety-score weight configuration.
        settings.SafetyScoreConfiguration.CrashWeight.Should().Be(1000);
        settings.SafetyScoreConfiguration.HarshBrakeWeight.Should().Be(10);
        settings.SpeedingSettings.Unit.Should().Be("milesPerHour");
        settings.VoiceCoaching.Language.Should().Be("english");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/settings/safety");
    }

    // ── GET /fleet/settings/driver-app ──────────────────────────────────────
    [Fact]
    public async Task GetDriverAppSettingsAsync_BindsNestedConfigs()
    {
        var resp = new
        {
            data = new
            {
                driverFleetId = "fleet-1",
                gamification = true,
                gamificationConfig = new { anonymizeDriverNames = true },
                orgVehicleSearch = false,
                trailerSelection = true,
                trailerSelectionConfig = new
                {
                    driverTrailerCreationEnabled = true,
                    maxNumOfTrailersSelected = 3,
                    orgTrailerSearch = false,
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new SettingsClient(TestFactory.CreateHttpClient(handler));

        var settings = await client.GetDriverAppSettingsAsync();

        settings.DriverFleetId.Should().Be("fleet-1");
        settings.Gamification.Should().BeTrue();
        settings.GamificationConfig!.AnonymizeDriverNames.Should().BeTrue();
        settings.TrailerSelectionConfig!.MaxNumOfTrailersSelected.Should().Be(3);
        settings.TrailerSelectionConfig.DriverTrailerCreationEnabled.Should().BeTrue();

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/settings/driver-app");
    }
}
