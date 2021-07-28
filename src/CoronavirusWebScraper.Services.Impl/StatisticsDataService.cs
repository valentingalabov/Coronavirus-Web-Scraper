using CoronavirusWebScraper.Data;
using CoronavirusWebScraper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CoronavirusWebScraper.Services.ServiceModels;

namespace CoronavirusWebScraper.Services.Impl
{
    public class StatisticsDataService : IStatisticsDataService
    {
        private readonly IMongoRepository<CovidStatistic> _repository;

        public StatisticsDataService(IMongoRepository<CovidStatistic> repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetAllDates()
        {
            var dates = _repository.FilterBy(
                filter => filter.Date != "",
                projection => projection.Date);

            return dates;
        }

        public CovidStatisticServiceModel GetStatisticForDay(string date)
        {
            DateTime result;
            var formatedDate = DateTime.TryParse(date, out result);

            if (!formatedDate)
            {
                return null;
            }

            var currentDateData = _repository
                .FilterBy(filter => filter.Date == result.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz"),
                projected => Conversion.ConvertToCovidStatisticServiceModel(projected))
                .FirstOrDefault();

            return currentDateData;
        }

    }
}
