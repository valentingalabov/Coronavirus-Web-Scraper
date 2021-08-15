namespace CoronavirusWebScraper.Web.HealthChecks
{
    using System.Net.NetworkInformation;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Diagnostics.HealthChecks;

    /// <summary>
    /// Custom health chek for coronavirus.bg ping.
    /// </summary>
    public class CoronaviursPagePingHelthCheck : IHealthCheck
    {
        private const string CovidUrl = "https://coronavirus.bg/";

        /// <summary>
        /// Check ping of CovidUrl.
        /// </summary>
        /// <param name="context">Registration health check in context.</param>
        /// <param name="cancellationToken">Notification that operation shult be canceled.</param>
        /// <returns>Return unhealthy statys if page is down or ping is more than 120ms.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(CovidUrl);
                    if (reply.Status != IPStatus.Success)
                    {
                        return HealthCheckResult.Unhealthy();
                    }

                    if (reply.RoundtripTime > 120)
                    {
                        return HealthCheckResult.Degraded($"Current ping is {reply.RoundtripTime}ms");
                    }

                    return HealthCheckResult.Healthy($"Current ping is {reply.RoundtripTime}ms");
                }
            }
            catch
            {
                return HealthCheckResult.Unhealthy($"Current ping is more than 120ms.");
            }
        }
    }
}
