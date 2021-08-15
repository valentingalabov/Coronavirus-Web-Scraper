namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold overall statistical information about covid19.
    /// </summary>
    public class OverallServiceModel
    {
        /// <summary>
        /// Gets or sets values about tested people.
        /// </summary>
        public TestedServiceModel Tested { get; set; }

        /// <summary>
        /// Gets or sets values about confimed cases.
        /// </summary>
        public ConfirmedServiceModel Confirmed { get; set; }

        /// <summary>
        /// Gets or sets values about active cases.
        /// </summary>
        public ActiveServiceModel Active { get; set; }

        /// <summary>
        /// Gets or sets values about recovered cases.
        /// </summary>
        public TotalAndLastServiceModel Recovered { get; set; }

        /// <summary>
        /// Gets or sets values about deceased cases.
        /// </summary>
        public TotalAndLastServiceModel Deceased { get; set; }

        /// <summary>
        /// Gets or sets values about vaccinated cases.
        /// </summary>
        public VaccinatedServiceModel Vaccinated { get; set; }
    }
}
