namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class MedicalServiceModel
    {
        public int Total { get; set; }

        public MedicalTypesServiceModel TotalByType { get; set; }

        public int Last24 { get; set; }

        public MedicalTypesServiceModel LastByType24 { get; set; }
    }
}
