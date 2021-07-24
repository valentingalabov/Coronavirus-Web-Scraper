namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class TestedServiceModel
    {
        public int Total { get; set; }

        public TestedByTypeServiceModel TotalByType { get; set; }

        public int Last24 { get; set; }

        public TestedByTypeServiceModel TotalByType24 { get; set; }
    }
}