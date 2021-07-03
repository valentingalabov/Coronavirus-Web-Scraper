using System.Threading.Tasks;

namespace CoronavirusWebScraper.Services.Data.Interfaces
{
    public interface ICovid19Scraper
    {
        Task PopultateDBWithCurrentDayStatisticcAsync();
    }
}
