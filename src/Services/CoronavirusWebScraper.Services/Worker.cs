
using CoronavirusWebScraper.Data.Repositories;
using CoronavirusWebScraper.Services.Data.Interfaces;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Services.Data
{
    public class Worker : BackgroundService
    {
        private readonly ICovid19Scraper covidScraper;
        private readonly IMongoRepository<BsonDocument> mongoRepository;

        public Worker(ICovid19Scraper covidScraper, IMongoRepository<BsonDocument> mongoRepository)
        {
            this.covidScraper = covidScraper;
            this.mongoRepository = mongoRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var data = this.covidScraper.ScrapeData();
                await mongoRepository.AddAsync(data);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            }

        }
    }
}
