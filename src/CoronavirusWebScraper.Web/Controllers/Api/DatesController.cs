using CoronavirusWebScraper.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusWebScraper.Web.Controllers.Api
{
    [ApiController]
    [Route("api/dates")]
    public class DatesController : Controller
    {
        private readonly IStatisticsDataService _dataService;

        public DatesController(IStatisticsDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ActionResult Dates()
        {
            var dates = _dataService.GetAllDates();

            return Ok(dates);
        }

    }
}
