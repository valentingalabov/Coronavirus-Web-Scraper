namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold statistical information about region.
    /// </summary>
    public class RegionStatisticsViewModel
    {
        /// <summary>
        /// Gets or sets information about confirmed cases for region.
        /// </summary>
        public TotalAndLastViewModel Confirmed { get; set; }

        /// <summary>
        /// Gets or sets information about vaccinated for region.
        /// </summary>
        public VaccinatedViewModel Vaccinated { get; set; }
    }
}
