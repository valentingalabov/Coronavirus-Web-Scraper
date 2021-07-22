using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using dataScraperExample;
using CoronavirusWebScraper.Services;

namespace TestWorker
{
    public class Worker : BackgroundService
    {
        private readonly ICovidDataScraperService covid19Scraper;

        public Worker(ICovidDataScraperService covid19Scraper)
        {
            this.covid19Scraper = covid19Scraper;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this.covid19Scraper.ScrapeData();

                await Task.Delay(5, stoppingToken);
                
            }
        }
    }
}
