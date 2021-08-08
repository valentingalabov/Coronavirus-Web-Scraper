namespace CoronavirusWebScraper.Web.HealthChecks
{
    using System.Net.NetworkInformation;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Diagnostics.HealthChecks;

    public class CoronaviursPagePingHelthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync("coronavirus.bg");
                    if (reply.Status != IPStatus.Success)
                    {
                        return HealthCheckResult.Unhealthy();
                    }

                    if (reply.RoundtripTime > 100)
                    {
                        return HealthCheckResult.Degraded($"Current ping is {reply.RoundtripTime}ms");
                    }

                    return HealthCheckResult.Healthy($"Current ping is {reply.RoundtripTime}ms");
                }
            }
            catch
            {
                return HealthCheckResult.Unhealthy($"Current ping is more than 100ms.");
            }
        }
    }
}
