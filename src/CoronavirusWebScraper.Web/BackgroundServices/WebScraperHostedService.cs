namespace CoronavirusWebScraper.Web.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoronavirusWebScraper.Services;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class WebScraperHostedService : IHostedService, IDisposable
    {
        private readonly ICovidDataScraperService covidScraper;
        private readonly ILogger<WebScraperHostedService> logger;
        private readonly IOptions<HostedServiceOptions> webScraperOptions;
        private Timer timer;

        public WebScraperHostedService(ICovidDataScraperService covidScraper, ILogger<WebScraperHostedService> logger, IOptions<HostedServiceOptions> webScraperOptions)
        {
            this.covidScraper = covidScraper;
            this.logger = logger;
            this.webScraperOptions = webScraperOptions;
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
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
            }
        }
    }
}
