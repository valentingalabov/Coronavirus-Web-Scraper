using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using dataScraperExample;

namespace TestWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private CreateEntity collectDataClass;
        private MongoCrud db;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            collectDataClass = new CreateEntity();
           
            db = new MongoCrud("WebScraper");
            
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
          
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var data = collectDataClass.GetDateAndCountryData();
                db.InsertRecord("Statistics", data);

                _logger.LogInformation("data added");

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                
            }
        }
    }
}
