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
        public ActionResult Dates(string year, string month)
        {

            var dates = _dataService.GetAllDates(year, month);

            return Ok(dates);
        }

    }
}
