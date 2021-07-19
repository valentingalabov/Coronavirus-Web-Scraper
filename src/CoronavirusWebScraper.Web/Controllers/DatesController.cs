
using CoronavirusWebScraper.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatesController : ControllerBase
    {
        private readonly ICovid19Scraper covid19Scraper;

        public DatesController(ICovid19Scraper covid19Scraper)
        {
            this.covid19Scraper = covid19Scraper;
        }

        [HttpGet]
        public ActionResult Dates()
        {
            var dates = this.covid19Scraper.GetAllDates();
       
            return Ok(dates.ToArray());
        }
    }
}
