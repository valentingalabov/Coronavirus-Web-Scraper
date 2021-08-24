namespace CoronavirusWebScraper.Web.Controllers
{
    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Retrive stastical information about covid 19.
    /// </summary>
    public class DataController : Controller
    {
        private readonly IStatisticsDataService dataService;

        /// <summary>
        /// Constructor implementing interface for reading data.
        /// </summary>
        /// <param name="dataService">Reading data interface.</param>
        public DataController(IStatisticsDataService dataService)
        {
            this.dataService = dataService;
        }

        /// <summary>
        /// Data page.
        /// </summary>
        /// <returns>Data view.</returns>
        [HttpGet]
        public IActionResult Data()
        {
            return this.View();
        }

        /// <summary>
        /// Retrive statistical information for given date.
        /// </summary>
        /// <param name="date">Date to search statistical information for.</param>
        /// <returns>Statistical information about choosen date or redirect to DataView if date is invalid.</returns>
        [HttpGet]
        public IActionResult DateData(string date)
        {
            var stats = this.dataService.GetStatisticsForSpecificDay(date);
            if (stats == null)
            {
                return this.RedirectToAction("Data");
            }

            var viewModel = Conversion.ConvertToCovidStatisticViewModel(stats);

            return this.View(viewModel);
        }
    }
}
