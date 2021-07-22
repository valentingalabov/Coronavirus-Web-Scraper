using CoronavirusWebScraper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoronavirusWebScraper.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatesController : ControllerBase
    {
        private readonly IStatisticsDataService dataService;

        public DatesController(IStatisticsDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]

        public ActionResult Dates()
        {
            var dates = this.dataService.GetAllDates().ToArray();

            return Ok(dates);
        }
    }
}
