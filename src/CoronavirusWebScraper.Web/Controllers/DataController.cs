using AutoMapper;
using CoronavirusWebScraper.Services;
using CoronavirusWebScraper.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusWebScraper.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly IStatisticsDataService _dataService;
        private readonly IMapper mapper;

        public DataController(IStatisticsDataService dataService,IMapper mapper)
        {
            _dataService = dataService;
            this.mapper = mapper;
        }

        public IActionResult Calendar()
        {           
                return this.View(); 
        }

        public IActionResult DateDetails(string date)
        {

            var stats = _dataService.GetStatisticForDay(date);

            var viewModel = mapper.Map<CovidStatisticsViewModel>(stats);
            

            return this.View(viewModel);
        }
    }
}
