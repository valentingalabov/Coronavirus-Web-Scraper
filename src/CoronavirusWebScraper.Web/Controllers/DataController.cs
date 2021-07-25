using AutoMapper;
using CoronavirusWebScraper.Services;
using CoronavirusWebScraper.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusWebScraper.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly IStatisticsDataService _dataService;
        private readonly IMapper _mapper;

        public DataController(IStatisticsDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public IActionResult Calendar()
        {
            return this.View();
        }

        public IActionResult DateDetails(string date)
        {
            var stats = _dataService.GetStatisticForDay(date);

            //var viewModel = _mapper.Map<CovidStatisticsViewModel>(stats);
            
            //var ser = BsonSerializer.Deserialize<RegionsViewModel>();

            return this.View();
        }
    }
}
