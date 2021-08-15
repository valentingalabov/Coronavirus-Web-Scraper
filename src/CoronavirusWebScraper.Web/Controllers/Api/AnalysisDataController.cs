namespace CoronavirusWebScraper.Web.Controllers.Api
{
    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Api controller which get statistical infromation for analisys.
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
        /// Api ActionResult which get analysis data and return it on api/analysis.
        /// </summary>
        /// <returns>Statistical infomation about covid.</returns>
        [HttpGet]
        public ActionResult Analysis()
        {
            var analysis = Conversion.ConvertToAnalysisModel(this.dataService.GetAnalysisData());

            return this.Ok(analysis);
        }
    }
}
