using System;
using System.Collections.Generic;

namespace CoronavirusWebScraper.Web.Models
{
    public class CovidStatisticViewModel
    {  
        public string Date { get; set; }

        public TestedViewModel Tested { get; set; }

        public ConfirmedViewModel Confirmed { get; set; }

        public ActiveViewModel Active { get; set; }

        public TotalAndLastViewModel Recovered { get; set; }

        public TotalAndLastViewModel Deceased { get; set; }

        public VaccinatedViewModel Vaccinated { get; set; }

        public IEnumerable<RegionsViewModel> Regions { get; set; }

        public TotalVaccineByType24ViewModel TotalVaccineByType24 { get; set; }

    }
}
