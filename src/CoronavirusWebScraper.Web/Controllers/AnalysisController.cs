namespace CoronavirusWebScraper.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Return Analysis view.
    /// </summary>
    public class AnalysisController : Controller
    {
        /// <summary>
        /// Analisys page.
        /// </summary>
        /// <returns>Return Analysis view.</returns>
        [HttpGet]
        public IActionResult Analysis()
        {
            return this.View();
        }
    }
}
