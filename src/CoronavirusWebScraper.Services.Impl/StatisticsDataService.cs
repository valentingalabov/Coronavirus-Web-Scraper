namespace CoronavirusWebScraper.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CoronavirusWebScraper.Data;
    using CoronavirusWebScraper.Data.Models;
    using CoronavirusWebScraper.Services.ServiceModels;

    public class StatisticsDataService : IStatisticsDataService
    {
        private readonly IMongoRepository<CovidStatistic> repository;

        public StatisticsDataService(IMongoRepository<CovidStatistic> repository)
        {
            this.repository = repository;
        }

        public AnalysisServiceModel GetAnalysisData()
        {
            var result = Conversion
                .ConvertToAnalysisServiceModel(this.repository
                .AsQueryable()
                .OrderByDescending(x => x.Date)
                .FirstOrDefault());

            return result;
        }

        public IEnumerable<string> GetAllDatesForSpecificMonthAndYear(string year, string month)
        {
            DateTime dateAsDateTime;
            var dateToFind = DateTime.TryParse(string.Concat(year, "/", month), out dateAsDateTime);

            if (dateToFind == true)
            {
                var currMonthToFind = dateAsDateTime.ToString("yyyy-MM");

                return this.repository.FilterBy(
                    filter => filter.Date.Contains(currMonthToFind),
                    projection => projection.Date);
            }

            return null;
        }

        public CovidStatisticServiceModel GetStatisticForDay(string date)
        {
            DateTime currDateToFind;
            var formatedDate = DateTime.TryParse(date, out currDateToFind);

            if (formatedDate == true)
            {
                return this.repository
                .FilterBy(
                    filter => filter.Date == currDateToFind.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz"),
                    projection => Conversion.ConvertToCovidStatisticServiceModel(projection))
                .FirstOrDefault();
            }

            return null;
        }
    }
}
