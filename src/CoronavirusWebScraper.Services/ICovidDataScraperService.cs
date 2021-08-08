namespace CoronavirusWebScraper.Services
{
    using System.Threading.Tasks;

    public interface ICovidDataScraperService
    {
        Task ScrapeData();
    }
}
