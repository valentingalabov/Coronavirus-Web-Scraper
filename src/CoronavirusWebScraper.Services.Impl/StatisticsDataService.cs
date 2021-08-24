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
        /// Contructor Implementation repository to read covid19 statistics data.
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
            var dateToFind = DateTime.TryParse(string.Concat(year, "/", month), out DateTime dateAsDateTime);

            if (dateToFind == true)
            {
                var currMonthToFind = dateAsDateTime.ToString(Constants.DateTimeYearAndMonthFormat);

                return this.repository.FilterBy(
                    filter => filter.Date.Contains(currMonthToFind),
                    projection => projection.Date);
            }

            return null;
        }

        /// <inheritdoc />
        public CovidStatisticServiceModel GetStatisticsForSpecificDay(string date)
        {
            var formatedDate = DateTime.TryParse(date, out DateTime currDateToFind);

            if (formatedDate == true)
            {
                return this.repository
                .FilterBy(
                    filter => filter.Date == currDateToFind.ToString(Constants.DateTimeFormatISO8601WithTimeZone),
                    projection => Conversion.ConvertToCovidStatisticServiceModel(projection))
                .FirstOrDefault();
            }

            return null;
        }
    }
}
