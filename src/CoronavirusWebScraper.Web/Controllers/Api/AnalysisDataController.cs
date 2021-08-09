namespace CoronavirusWebScraper.Web.Controllers.Api
{
    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/analaysis")]
    public class AnalysisDataController : Controller
    {
        private readonly IStatisticsDataService dataService;

        public AnalysisDataController(IStatisticsDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]
        public ActionResult Analysis()
        {
            var analysis = Conversion.ConvertToAnalysisModel(this.dataService.GetAnalysisData());

            return this.Ok(analysis);
        }
    }
}
