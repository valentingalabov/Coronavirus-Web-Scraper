using Microsoft.AspNetCore.Mvc;

namespace CoronavirusWebScraper.Web.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Calendar()
        {           
                return this.View(); 
        }

        public IActionResult DateDetails(string date)
        {


            return Ok(date);
        }
    }
}
