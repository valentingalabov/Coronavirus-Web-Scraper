using AutoMapper;
using CoronavirusWebScraper.Services;
using CoronavirusWebScraper.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusWebScraper.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly IStatisticsDataService _dataService;

        public DataController(IStatisticsDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Calendar()
        {
            return this.View();
        }

        public IActionResult DateDetails(string date)
        {
            var stats = _dataService.GetStatisticForDay(date);
            var viewModel = Conversion.ConvertToCovidStatisticViewModel(stats);       

            return this.View(viewModel);
        }
    }
}
