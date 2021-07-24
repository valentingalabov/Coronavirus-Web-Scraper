namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class VaccinatedServiceModel
    {
        public int Total { get; set; }

        public int Last { get; set; }

        public VaccineTypeServiceModel LastByType { get; set; }

        public int TotalCompleted { get; set; }

    }
}