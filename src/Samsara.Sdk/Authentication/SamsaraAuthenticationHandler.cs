namespace Samsara.Sdk.Authentication;

using System.Net.Http.Headers;
using Samsara.Sdk.Configuration;
using Microsoft.Extensions.Options;

/// <summary>
/// A delegating handler that injects the Bearer token into outgoing requests.
/// Supports both static API tokens and dynamic token providers for OAuth 2.0 scenarios.
/// </summary>
internal sealed class SamsaraAuthenticationHandler : DelegatingHandler
{
    private readonly IOptionsMonitor<SamsaraClientOptions> _options;

    public SamsaraAuthenticationHandler(IOptionsMonitor<SamsaraClientOptions> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var opts = _options.CurrentValue;
        string token;

        if (opts.TokenProvider is not null)
        {
            token = await opts.TokenProvider(cancellationToken).ConfigureAwait(false);
        }
        else if (!string.IsNullOrWhiteSpace(opts.ApiToken))
        {
            token = opts.ApiToken!;
        }
        else
        {
            throw new InvalidOperationException(
                "No API token or token provider has been configured. " +
                "Set SamsaraClientOptions.ApiToken or SamsaraClientOptions.TokenProvider.");
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
