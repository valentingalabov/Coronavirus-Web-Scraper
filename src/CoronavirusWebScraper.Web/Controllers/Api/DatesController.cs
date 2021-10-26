namespace CoronavirusWebScraper.Web.Controllers.Api
{
    using CoronavirusWebScraper.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Retrieve dates for which have statistical information.
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
        /// Retrieve dates for which have statistical information.
        /// </summary>
        /// <param name="year">Year to search information for.</param>
        /// <param name="month">Month to search information for.</param>
        /// <returns>Collection of dates for which have statistical information in database or not fauns if doesn't have information.</returns>
        [HttpGet]
        public ActionResult Dates(string year, string month)
        {
            var dates = this.dataService.GetAllDatesForSpecificMonthAndYear(year, month);

            if (dates == null)
            {
                return this.NotFound();
            }

            return this.Ok(dates);
        }
    }
}
