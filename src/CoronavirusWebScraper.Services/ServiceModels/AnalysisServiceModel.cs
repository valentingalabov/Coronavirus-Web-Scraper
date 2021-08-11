namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class AnalysisServiceModel
    {
        public string Date { get; set; }

        public int Active { get; set; }

        public int Hospitalized { get; set; }

        public int Icu { get; set; }

        public int Confirmed { get; set; }

        public int TotalTests { get; set; }

        public int TotalRecovered { get; set; }

        public MedicalAnalysisServiceModel TotalMedicalAnalisys { get; set; }
    }
}
