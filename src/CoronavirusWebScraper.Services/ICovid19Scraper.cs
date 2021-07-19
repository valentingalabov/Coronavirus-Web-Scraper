using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Services
{ 
    public interface ICovid19Scraper
    {
        Task ScrapeData();

        IEnumerable<string> GetAllDates();
    }
}
