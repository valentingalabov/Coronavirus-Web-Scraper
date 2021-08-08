namespace CoronavirusWebScraper.Web.HealthChecks
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using CoronavirusWebScraper.Services;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    public class DBResponseTimeHealthCheck : IHealthCheck
    {
        private readonly IStatisticsDataService scraperService;

        public DBResponseTimeHealthCheck(IStatisticsDataService scraperService)
        {
            this.scraperService = scraperService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var watch = new Stopwatch();
            watch.Start();
            watch.Stop();
            var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;

            if (responseTimeForCompleteRequest < 1000)
            {
                return Task.FromResult(HealthCheckResult.Healthy($"Current Db response time is {responseTimeForCompleteRequest}"));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"Current Db response time is {responseTimeForCompleteRequest}"));
            }
        }
    }
}
