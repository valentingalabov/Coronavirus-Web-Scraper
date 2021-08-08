namespace CoronavirusWebScraper.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AnalysisController : Controller
    {
        [HttpGet]
        public IActionResult Analysis()
        {
            return this.View();
        }
    }
}
