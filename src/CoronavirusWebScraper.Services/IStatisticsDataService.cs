using CoronavirusWebScraper.Services.ServiceModels;
using System.Collections.Generic;

namespace CoronavirusWebScraper.Services
{
    public interface IStatisticsDataService
    {
        IEnumerable<string> GetAllDates(string year, string month);

        CovidStatisticServiceModel GetStatisticForDay(string day);

        AnalysisServiceModel GetActiveAndHospitalized();
    }
}
