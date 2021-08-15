namespace CoronavirusWebScraper.Web.Controllers
{
    using System.Diagnostics;

    using CoronavirusWebScraper.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        /// <summary>
        /// Home page.
        /// </summary>
        /// <returns>Index view.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Privacy page.
        /// </summary>
        /// <returns>Privacy view.</returns>
        [HttpGet]
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// Error page.
        /// </summary>
        /// <returns>Error view with error identifier.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
