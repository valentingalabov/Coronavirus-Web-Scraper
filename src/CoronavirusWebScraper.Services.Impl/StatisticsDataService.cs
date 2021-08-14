namespace CoronavirusWebScraper.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CoronavirusWebScraper.Data;
    using CoronavirusWebScraper.Data.Models;
    using CoronavirusWebScraper.Services.ServiceModels;

    /// <summary>
    /// Reading covid statistical data.
    /// </summary>
    public class StatisticsDataService : IStatisticsDataService
    {
        private readonly IMongoRepository<CovidStatistics> repository;

        /// <summary>
        /// Contructor Implementation repository to read covid statistics data.
        /// </summary>
        /// <param name="repository">Mongo db repository.</param>
        public StatisticsDataService(IMongoRepository<CovidStatistics> repository)
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public CovidStatisticServiceModel GetStatisticsForSpecificDay(string date)
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
