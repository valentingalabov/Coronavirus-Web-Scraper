using CoronavirusWebScraper.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Web.BackgroundServices
{
    public class Worker : BackgroundService
    {
        private readonly ICovidDataScraperService covidScraper;

        public Worker(ICovidDataScraperService covidScraper)
        {
            this.covidScraper = covidScraper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                await covidScraper.ScrapeData();
                int hourSpan = 24 - DateTime.Now.Hour;
                int numberOfHours = hourSpan;

                if (hourSpan == 24)
                {
                    await this.covidScraper.ScrapeData();

                    numberOfHours = 24;
                }

                await Task.Delay(TimeSpan.FromHours(numberOfHours), stoppingToken);
            }

            while (!stoppingToken.IsCancellationRequested);

        }
    }
}