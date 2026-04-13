#if !NET8_0_OR_GREATER

namespace Samsara.Sdk.Http;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// A simple retry <see cref="DelegatingHandler"/> used on netstandard2.0 targets in place of
/// <c>Microsoft.Extensions.Http.Resilience</c>, which requires .NET 6+.
/// <para>
/// Retries only on <see cref="HttpRequestException"/> (network-level failures where no response
/// was received), using exponential back-off with jitter. HTTP 4xx/5xx error responses are
/// <em>not</em> retried because retrying them safely would require cloning the request content.
/// </para>
/// </summary>
internal sealed class SamsaraRetryHandler : DelegatingHandler
{
    private const int MaxAttempts = 3;
    private static readonly TimeSpan BaseDelay = TimeSpan.FromSeconds(1);

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (HttpRequestException) when (attempt < MaxAttempts - 1)
            {
                attempt++;
                // Exponential back-off: 1 s, 2 s, … (capped at MaxAttempts)
                var delay = TimeSpan.FromSeconds(BaseDelay.TotalSeconds * Math.Pow(2, attempt - 1));
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}

#endif
