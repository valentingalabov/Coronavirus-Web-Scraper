namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold infromation of vaccinated by type of vaccine for last 24 hours.
    /// </summary>
    public class TotalVaccineByType24ViewModel
    {
        /// <summary>
        /// Gets or sets count of vaccinated with comirnaty.
        /// </summary>
        public int Comirnaty { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated with moderna.
        /// </summary>
        public int Moderna { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated with astrazeneca.
        /// </summary>
        public int AstraZeneca { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated with janssen.
        /// </summary>
        public int Janssen { get; set; }
    }
}
