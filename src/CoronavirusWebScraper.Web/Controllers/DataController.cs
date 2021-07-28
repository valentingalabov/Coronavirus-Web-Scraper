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

        public IActionResult Data()
        {
            return this.View();
        }

        public IActionResult DateData(string date)
        {
            var stats = _dataService.GetStatisticForDay(date);
            if (stats == null)
            {
                return this.RedirectToAction("Data");
            }
            var viewModel = Conversion.ConvertToCovidStatisticViewModel(stats);       

            return this.View(viewModel);
        }
    }
}
