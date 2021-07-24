namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class OverallServiceModel
    {
        public TestedServiceModel Tested { get; set; }

        public ConfirmedServiceModel Confirmed { get; set; }

        public ActiveServiceModel Active { get; set; }

        public TotalAndLastServiceModel Recovered { get; set; }

        public TotalAndLastServiceModel Deceased { get; set; }

        public VaccinatedServiceModel Vaccinated { get; set; }
    }
}