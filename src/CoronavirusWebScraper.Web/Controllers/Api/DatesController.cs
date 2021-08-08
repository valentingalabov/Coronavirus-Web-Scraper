namespace CoronavirusWebScraper.Web.Controllers.Api
{
    using CoronavirusWebScraper.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/dates")]
    public class DatesController : Controller
    {
        private readonly IStatisticsDataService dataService;

        public DatesController(IStatisticsDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]
        public ActionResult Dates(string year, string month)
        {
            var dates = this.dataService.GetAllDates(year, month);

            return this.Ok(dates);
        }
    }
}
