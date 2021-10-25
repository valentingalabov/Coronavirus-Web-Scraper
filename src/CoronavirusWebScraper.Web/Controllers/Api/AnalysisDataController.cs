namespace CoronavirusWebScraper.Web.Controllers.Api
{
    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Retrive statistical infromation for analisys.
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
        /// Retrive analysis data and return it on api/analysis.
        /// </summary>
        /// <returns>Statistical infomation about covid 19 ot not faund if doesn't have information.</returns>
        [HttpGet]
        public ActionResult Analysis()
        {
            var data = this.dataService.GetAnalysisData();
            var analysis = Conversion.ConvertToAnalysisModel(data);

            if (analysis == null)
            {
                return this.NotFound();
            }

            return this.Ok(analysis);
        }
    }
}
