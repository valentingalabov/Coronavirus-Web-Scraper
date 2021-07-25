namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class RegionsServiceModel
    {
        public string Name { get; set; }

        public RegionStatisticsServiceModel RegionStatistics { get; set; }
    }
}