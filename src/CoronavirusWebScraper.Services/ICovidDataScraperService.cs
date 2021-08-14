namespace CoronavirusWebScraper.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines methods for covid statistics data collection.
    /// </summary>
    public interface ICovidDataScraperService
    {
        /// <summary>
        /// Scrape statistical data about covid19 from "coronavirus.bg" and
        /// add it to the database if doesn't exist.
        /// </summary>
        Task ScrapeData();
    }
}
