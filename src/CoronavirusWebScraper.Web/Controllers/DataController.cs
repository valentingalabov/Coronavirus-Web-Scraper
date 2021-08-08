namespace CoronavirusWebScraper.Web.Controllers
{
    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class DataController : Controller
    {
        private readonly IStatisticsDataService dataService;

        public DataController(IStatisticsDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]
        public IActionResult Data()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult DateData(string date)
        {
            var stats = this.dataService.GetStatisticForDay(date);
            if (stats == null)
            {
                return this.RedirectToAction("Data");
            }

            var viewModel = Conversion.ConvertToCovidStatisticViewModel(stats);

            return this.View(viewModel);
        }
    }
}
