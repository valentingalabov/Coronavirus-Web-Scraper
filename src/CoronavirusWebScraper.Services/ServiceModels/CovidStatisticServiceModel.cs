namespace CoronavirusWebScraper.Services.ServiceModels
{
    using System.Collections.Generic;

    public class CovidStatisticServiceModel
    {
        public string Date { get; set; }

        public OverallServiceModel Overall { get; set; }

        public IEnumerable<RegionsServiceModel> Regions { get; set; }

        public TotalVaccineByType24ServiceModel TotalVaccineByType24 { get; set; }
    }
}
