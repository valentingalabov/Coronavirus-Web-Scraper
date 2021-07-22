using System.Threading.Tasks;

namespace CoronavirusWebScraper.Services
{
    public interface ICovidDataScraperService
    {
        Task ScrapeData();
    }
}
