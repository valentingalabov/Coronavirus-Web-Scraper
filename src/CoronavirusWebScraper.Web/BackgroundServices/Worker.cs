namespace CoronavirusWebScraper.Web.BackgroundServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoronavirusWebScraper.Services;
    using Microsoft.Extensions.Hosting;

    public class Worker : BackgroundService
    {
        private readonly ICovidDataScraperService covidScraper;
        private readonly IBackgroundServiceConfiguration configuration;

        public Worker(ICovidDataScraperService covidScraper, IBackgroundServiceConfiguration configuration)
        {
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
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                await Task.Delay(TimeSpan.FromHours(this.configuration.Hours), stoppingToken);
            }
        }
    }
}
