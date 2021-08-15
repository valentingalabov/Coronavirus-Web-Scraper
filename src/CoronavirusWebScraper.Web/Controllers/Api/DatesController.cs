namespace CoronavirusWebScraper.Web.Controllers.Api
{
    using CoronavirusWebScraper.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Get dates for which have statistical infromation.
    /// </summary>
    [ApiController]
    [Route("api/dates")]
    public class DatesController : Controller
    {
        private readonly IStatisticsDataService dataService;

        /// <summary>
        /// Constructor implementing interface for reading data.
        /// </summary>
        /// <param name="dataService">Service which read data from database.</param>
        public DatesController(IStatisticsDataService dataService)
        {
            this.dataService = dataService;
        }

        /// <summary>
        /// Gets dates for which have statistical infromation.
        /// </summary>
        /// <param name="year">Year to search information for.</param>
        /// <param name="month">Month to search information for.</param>
        /// <returns>Collection of dates for which have statistical information in database.</returns>
        [HttpGet]
        public ActionResult Dates(string year, string month)
        {
            var dates = this.dataService.GetAllDatesForSpecificMonthAndYear(year, month);

            return this.Ok(dates);
        }
    }
}
