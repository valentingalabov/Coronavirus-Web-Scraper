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

            do
            {
                int hourSpan = 24 - DateTime.Now.Hour;
                int numberOfHours = hourSpan;

                if (hourSpan == 24)
                {
                    var data = this.covidScraper.ScrapeData();
                    await mongoRepository.AddAsync(data);

                    numberOfHours = 24;
                }

                await Task.Delay(TimeSpan.FromHours(numberOfHours), stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);


            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    var data = this.covidScraper.ScrapeData();
            //    await mongoRepository.AddAsync(data);

            //    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

            //}

        }
    }
}
