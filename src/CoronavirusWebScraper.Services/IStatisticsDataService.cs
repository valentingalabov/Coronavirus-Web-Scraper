using CoronavirusWebScraper.Data.Models;
using System.Collections.Generic;

namespace CoronavirusWebScraper.Services
{
    public interface IStatisticsDataService
    {
        IEnumerable<string> GetAllDates();

        CovidStatistic GetStatisticForDay(string day);
    }
}
