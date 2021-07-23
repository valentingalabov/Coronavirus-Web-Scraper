using CoronavirusWebScraper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoronavirusWebScraper.Services.Impl.ServiceModels;

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

        public CovidStatistic GetStatisticForDay(string date)
        {
            var formatedDate = DateTime.Parse(date).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");

            var currentDayData = _repository.FilterBy(filter => filter.Date == formatedDate).FirstOrDefault();
      
            //var currentDayData = _repository.AsQueryable().Where(x => x.Date == formatedDate).FirstOrDefault();

            return currentDayData;
        }
    }
}
