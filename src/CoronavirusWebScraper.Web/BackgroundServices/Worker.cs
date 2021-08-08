using CoronavirusWebScraper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Web.BackgroundServices
{
    public class Worker : BackgroundService
    {
        private readonly ICovidDataScraperService _covidScraper;
        private readonly IBackgroundServiceConfiguration _configuration;

        public Worker(ICovidDataScraperService covidScraper, IBackgroundServiceConfiguration configuration)
        {
            _covidScraper = covidScraper;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _covidScraper.ScrapeData();
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
                await Task.Delay(TimeSpan.FromHours(_configuration.Hours), stoppingToken);
            }

            
        }

    }
}