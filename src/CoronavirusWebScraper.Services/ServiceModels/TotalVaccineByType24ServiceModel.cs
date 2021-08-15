namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold infromation of vaccinated by type of vaccine for last 24 hours.
    /// </summary>
    public class TotalVaccineByType24ServiceModel
    {
        /// <summary>
        /// Gets or sets count of vacinnated with comirnaty vaccine.
        /// </summary>
        public int Comirnaty { get; set; }

        /// <summary>
        /// Gets or sets count of vacinnated with moderna vaccine.
        /// </summary>
        public int Moderna { get; set; }

        /// <summary>
        /// Gets or sets count of vacinnated with astrazeneka vaccine.
        /// </summary>
        public int AstraZeneca { get; set; }

        /// <summary>
        /// Gets or sets count of vacinnated with jansen vaccine.
        /// </summary>
        public int Janssen { get; set; }
    }
}
