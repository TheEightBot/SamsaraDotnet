namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Compliance;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the tachograph file-upload operation, which graduated out of
/// <c>/preview</c> in the Samsara spec.
/// <para>
/// These lock in two things the checkers cannot prove on their own: that the request goes
/// to the <b>current</b> path (the old <c>/preview/...</c> path 404s), and that the
/// response binds to typed records rather than <see cref="object"/>.
/// </para>
/// </summary>
// SAMSARA001 is the [Experimental] diagnostic on beta-tagged operations. It is an ERROR by
// design, so consumers must consciously opt in; these tests are that opt-in. Note it cannot
// be silenced with [SuppressMessage] — only #pragma or <NoWarn>, which is what consumers do.
#pragma warning disable SAMSARA001
public sealed class TachographContractTests
{
    private static object UploadResponse() => new
    {
        data = new
        {
            expiresAtTime = "2026-08-17T12:30:00Z",
            uploadUrl = "https://uploads.samsara.com/signed/abc123",
            requiredHeaders = new[]
            {
                new { name = "Content-MD5", value = "1B2M2Y8AsgTpgAmY7PhCfg==" },
                new { name = "Content-Type", value = "application/octet-stream" },
            },
        },
    };

    [Fact]
    public async Task CreateFileUploadAsync_PostsToTheGraduatedPath()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(UploadResponse());
        var client = new TachographClient(TestFactory.CreateHttpClient(handler));

        await client.CreateFileUploadAsync(new CreateTachographFileUploadRequest
        {
            ContentMd5 = "1B2M2Y8AsgTpgAmY7PhCfg==",
            ContentType = "application/octet-stream",
            FileSizeBytes = 4096,
            FileType = "driverCard",
        });

        var uri = handler.LastRequest.RequestUri!.ToString();
        uri.Should().Contain("fleet/tachograph/file-uploads");
        uri.Should().NotContain("preview", "the operation graduated out of /preview and the old path 404s");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
    }

    [Fact]
    public async Task CreateFileUploadAsync_BindsTypedUploadTarget()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(UploadResponse());
        var client = new TachographClient(TestFactory.CreateHttpClient(handler));

        var upload = await client.CreateFileUploadAsync(new CreateTachographFileUploadRequest
        {
            ContentMd5 = "1B2M2Y8AsgTpgAmY7PhCfg==",
            ContentType = "application/octet-stream",
            FileSizeBytes = 4096,
            FileType = "vehicleUnit",
        });

        upload.UploadUrl.Should().Be("https://uploads.samsara.com/signed/abc123");
        upload.ExpiresAtTime.Should().Be(DateTimeOffset.Parse("2026-08-17T12:30:00Z"));
        upload.RequiredHeaders.Should().HaveCount(2);
        upload.RequiredHeaders![0].Name.Should().Be("Content-MD5");
        upload.RequiredHeaders[1].Value.Should().Be("application/octet-stream");
    }

    [Fact]
    public async Task ObsoletePreviewShim_ForwardsToTheGraduatedPath()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(UploadResponse());
        var client = new PreviewApisClient(TestFactory.CreateHttpClient(handler));

#pragma warning disable CS0618 // deliberately exercising the back-compat shim
        var upload = await client.CreateTachographFileUploadAsync(new CreateTachographFileUploadRequest
        {
            ContentMd5 = "1B2M2Y8AsgTpgAmY7PhCfg==",
            ContentType = "application/octet-stream",
            FileSizeBytes = 4096,
            FileType = "driverCard",
        });
#pragma warning restore CS0618

        upload.UploadUrl.Should().Be(
            "https://uploads.samsara.com/signed/abc123",
            "the shim now unwraps the { data: ... } envelope like the graduated method");

        handler.LastRequest.RequestUri!.ToString()
            .Should().Contain("fleet/tachograph/file-uploads")
            .And.NotContain("preview", "the shim must forward to the live path, not the dead one");
    }
}
#pragma warning restore SAMSARA001
