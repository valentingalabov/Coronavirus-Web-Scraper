namespace CoronavirusWebScraper.Services
{
    using System.Collections.Generic;

    using CoronavirusWebScraper.Services.ServiceModels;

    public interface IStatisticsDataService
    {
        IEnumerable<string> GetAllDates(string year, string month);

        CovidStatisticServiceModel GetStatisticForDay(string day);

        AnalysisServiceModel GetActiveAndHospitalized();
    }
}
