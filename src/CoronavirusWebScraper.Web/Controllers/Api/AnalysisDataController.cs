using CoronavirusWebScraper.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusWebScraper.Web.Controllers.Api
{
    [ApiController]
    [Route("api/analaysis")]
    public class AnalysisDataController : Controller
    {
        private readonly IStatisticsDataService _dataService;

        public AnalysisDataController(IStatisticsDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        public ActionResult Analysis()
        {
            var result = _dataService.GetActiveAndHospitalized();

            return Ok(result);
        }
    }
}
