using System.Collections.Generic;

namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class CovidStatisticServiceModel
    {  
        public string Date { get; set; }

        public OverallServiceModel Overall { get; set; }

        public IEnumerable<RegionsServiceModel> Regions { get; set; }

        //public Stats Stats { get; set; }
       
    }
}
