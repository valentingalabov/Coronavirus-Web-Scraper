namespace CoronavirusWebScraper.Web.Controllers.Api
{
    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Retrieve statistical information for analysis.
    /// </summary>
    [ApiController]
    [Route("api/analysis")]
    public class AnalysisDataController : Controller
    {
        private readonly IStatisticsDataService dataService;

        /// <summary>
        /// Constructor implementing interface for reading data.
        /// </summary>
        /// <param name="dataService">Service which read data from database.</param>
        public AnalysisDataController(IStatisticsDataService dataService)
        {
            this.dataService = dataService;
        }

        /// <summary>
        /// Retrieve analysis data and return it on api/analysis.
        /// </summary>
        /// <returns>Statistical information about covid19 or not found if doesn't have information.</returns>
        [HttpGet]
        public ActionResult Analysis()
        {
            var data = this.dataService.GetAnalysisDataForLastDay();

            if (data == null)
            {
                return this.NotFound();
            }

            var analysis = Conversion.ConvertToAnalysisModel(data);

            return this.Ok(analysis);
        }
    }
}
