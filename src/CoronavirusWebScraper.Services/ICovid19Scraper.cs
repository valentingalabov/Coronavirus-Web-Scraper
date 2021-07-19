using MongoDB.Bson;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Services
{ 
    public interface ICovid19Scraper
    {
        Task ScrapeData();
    }
}
