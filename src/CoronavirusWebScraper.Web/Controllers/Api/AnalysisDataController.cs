namespace CoronavirusWebScraper.Web.Controllers.Api
{
    using CoronavirusWebScraper.Services;
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
            var result = this.dataService.GetActiveAndHospitalized();

            return this.Ok(result);
        }
    }
}
