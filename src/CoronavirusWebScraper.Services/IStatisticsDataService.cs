namespace CoronavirusWebScraper.Services
{
    using System.Collections.Generic;

    using CoronavirusWebScraper.Services.ServiceModels;

    public interface IStatisticsDataService
    {
        IEnumerable<string> GetAllDatesForSpecificMonthAndYear(string year, string month);

        CovidStatisticServiceModel GetStatisticForDay(string day);

        AnalysisServiceModel GetAnalysisData();
    }
}
