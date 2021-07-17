using MongoDB.Bson;

namespace CoronavirusWebScraper.Services
{ 
    public interface ICovid19Scraper
    {
        void ScrapeData();
    }
}
