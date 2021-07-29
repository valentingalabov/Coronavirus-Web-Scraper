using CoronavirusWebScraper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoronavirusWebScraper.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly IStatisticsDataService _dataService;

        public DataController(IStatisticsDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("dates")]
        public ActionResult Dates()
        {
            var dates = _dataService.GetAllDates().ToArray();

            return Ok(dates);
        }

        [HttpGet("analysis")]
        public ActionResult Analysis()
        {
            var result = _dataService.GetActiveAndHospitalized();
            return Ok(result);
        }
    }
}
