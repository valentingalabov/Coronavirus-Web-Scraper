using Microsoft.AspNetCore.Mvc;

namespace CoronavirusWebScraper.Web.Controllers
{
    public class AnalysisController : Controller
    {
        [HttpGet]
        public IActionResult Analysis()
        {
            return this.View();
        }
    }
}
