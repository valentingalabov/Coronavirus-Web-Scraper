namespace CoronavirusWebScraper.Web.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoronavirusWebScraper.Services;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly ICovidDataScraperService covidScraper;
        private readonly IBackgroundServiceConfiguration configuration;

        public Worker(ILogger<Worker> logger, ICovidDataScraperService covidScraper, IBackgroundServiceConfiguration configuration)
        {
            this.logger = logger;
            this.covidScraper = covidScraper;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await this.covidScraper.ScrapeData();
                    await Task.Delay(TimeSpan.FromHours(this.configuration.Hours), stoppingToken);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
