using AngleSharp;
using CoronavirusWebScraper.Services.Data.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Services
{
    public class Covid19Scraper : ICovid19Scraper
    {
        private readonly IConfiguration config;
        private readonly IBrowsingContext context;
        public Covid19Scraper()
        {
            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(this.config);

        }

        public Task PopultateDBWithCurrentDayStatisticcAsync()
        {
            throw new NotImplementedException();
        }

        private void GetDataForCurrentDay() 
        {
            var url = "https://coronavirus.bg/";
            var document = context.OpenAsync(url)
                .GetAwaiter()
                .GetResult();

            if (document.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException();
            }

            var currentDateSpan = document.QuerySelector(".statistics-header-wrapper span").TextContent.Split(" ");
            var time = currentDateSpan[2];
            var date = currentDateSpan[5];
            var month = currentDateSpan[6];
            var year = currentDateSpan[7];
            var currDateAsString = date + " " + month + " " + year + " " + time;
            var currDate = DateTime.Parse(currDateAsString);

            var dateToAdd = currDate.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            var dateScraped = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");





            var statistics = document.QuerySelectorAll(".statistics-container > div > p").Select(x => x.TextContent).ToArray();
            //stats.TotalTests = IntParser(statistics[0]);
            //stats.TotalTests24 = IntParser(statistics[2]);
            //stats.TotalConfirmed = IntParser(statistics[4]);
            //stats.Active = IntParser(statistics[6]);
            //stats.TotalCured = IntParser(statistics[8]);
            //stats.TotalCured24 = IntParser(statistics[10]);
            //stats.Hospitalized = IntParser(statistics[12]);
            //stats.IntensiveCare = IntParser(statistics[14]);
            //stats.Died = IntParser(statistics[16].Trim());
            //stats.Died24 = IntParser(statistics[18].Trim());
            //stats.Vaccinated = IntParser(statistics[20]);
            //stats.Vaccinated24 = IntParser(statistics[22]);

            //var tested = new Tested();
            //tested.Total = IntParser(statistics[0]);

        }


    }
}
