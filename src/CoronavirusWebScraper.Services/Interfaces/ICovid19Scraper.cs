using MongoDB.Bson;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Services.Data.Interfaces
{
    public interface ICovid19Scraper
    {
        BsonDocument ScrapeData();
    }
}
