
using CoronavirusWebScraper.Services.Impl.ServiceModels;
using System.Collections.Generic;

namespace CoronavirusWebScraper.Services.Impl
{
    public interface IStatisticsDataService
    {
        IEnumerable<string> GetAllDates();

        CovidStatistic GetStatisticForDay(string day);
    }
}
