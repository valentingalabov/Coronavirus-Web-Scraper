namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold information about vaccinated count.
    /// </summary>
    public class VaccinatedViewModel
    {
        /// <summary>
        /// Gets or sets count of total vaccinated.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated for last 24 hours.
        /// </summary>
        public int Last { get; set; }

        /// <summary>
        /// Gets or sets information of vaccinated for last 24 hours by vaccine type.
        /// </summary>
        public VaccineTypeViewModel LastByType { get; set; }

        /// <summary>
        /// Gets or sets count fully complete vaccination.
        /// </summary>
        public int TotalCompleted { get; set; }
    }
}
