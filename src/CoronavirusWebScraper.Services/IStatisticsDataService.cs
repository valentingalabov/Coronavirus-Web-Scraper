using CoronavirusWebScraper.Services.ServiceModels;
using System.Collections.Generic;

namespace CoronavirusWebScraper.Services
{
    public interface IStatisticsDataService
    {
        IEnumerable<string> GetAllDates();

        CovidStatisticServiceModel GetStatisticForDay(string day);
    }
}
