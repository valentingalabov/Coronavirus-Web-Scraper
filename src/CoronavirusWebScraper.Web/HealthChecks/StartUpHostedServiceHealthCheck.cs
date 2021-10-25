namespace CoronavirusWebScraper.Web.HealthChecks
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Diagnostics.HealthChecks;

    public class StartupHostedServiceHealthCheck : IHealthCheck
    {
        private volatile bool startupTaskCompleted = false;

        public string Name => "slow_dependency_check";

        public bool StartupTaskCompleted
        {
            get => this.startupTaskCompleted;
            set => this.startupTaskCompleted = value;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (this.StartupTaskCompleted)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("The startup task is finished."));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("The startup task is still running."));
        }
    }
}
