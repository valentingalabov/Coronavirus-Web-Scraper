namespace CoronavirusWebScraper.Web.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Web.HealthChecks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class WebScraperHostedService : IHostedService, IDisposable
    {
        private readonly ICovidDataScraperService covidScraper;
        private readonly ILogger<WebScraperHostedService> logger;
        private readonly IOptions<HostedServiceOptions> webScraperOptions;
        private readonly StartupHostedServiceHealthCheck startupHostedServiceHealthCheck;
        private Timer timer;

        public WebScraperHostedService(
            ICovidDataScraperService covidScraper,
            ILogger<WebScraperHostedService> logger,
            IOptions<HostedServiceOptions> webScraperOptions,
            StartupHostedServiceHealthCheck startupHostedServiceHealthCheck)
        {
            this.covidScraper = covidScraper;
            this.logger = logger;
            this.webScraperOptions = webScraperOptions;
            this.startupHostedServiceHealthCheck = startupHostedServiceHealthCheck;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("WebScraperHostedService running.");

            var hours = this.webScraperOptions.Value.Hours;

            this.timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(hours));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("WebScraperHostedService is stopping.");
            this.startupHostedServiceHealthCheck.StartupTaskCompleted = false;

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }

        private async void DoWork(object state)
        {
            try
            {
                await this.covidScraper.ScrapeData();
                this.startupHostedServiceHealthCheck.StartupTaskCompleted = true;
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
            }
        }
    }
}
