namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold statistical information about region.
    /// </summary>
    public class RegionsServiceModel
    {
        /// <summary>
        /// Gets or sets region name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets statistical information about current Region.
        /// </summary>
        public RegionStatisticsServiceModel RegionStatistics { get; set; }
    }
}
