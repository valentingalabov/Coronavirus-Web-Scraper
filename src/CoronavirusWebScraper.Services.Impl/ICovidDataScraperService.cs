using System.Threading.Tasks;

namespace CoronavirusWebScraper.Services.Impl
{
    public interface ICovidDataScraperService
    {
        Task ScrapeData();
    }
}
