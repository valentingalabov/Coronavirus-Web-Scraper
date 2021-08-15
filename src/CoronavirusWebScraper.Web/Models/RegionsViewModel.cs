namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold statistical information about region.
    /// </summary>
    public class RegionsViewModel
    {
        /// <summary>
        /// Gets or sets region name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets statistical information about current Region.
        /// </summary>
        public RegionStatisticsViewModel RegionStatistics { get; set; }
    }
}
