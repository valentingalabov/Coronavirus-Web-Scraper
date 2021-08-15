namespace CoronavirusWebScraper.Services.ServiceModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Hold statistical data about spread, treatment and the fight against COVID-19.
    /// </summary>
    public class CovidStatisticServiceModel
    {
        /// <summary>
        /// Gets or sets date and time to which the data relate in format ISO 8601 and current time zone.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets get or sets overall statistical information about covid19.
        /// </summary>
        public OverallServiceModel Overall { get; set; }

        /// <summary>
        /// Gets or sets statistical information about covid19 for all regions in current country.
        /// </summary>
        public IEnumerable<RegionsServiceModel> Regions { get; set; }

        /// <summary>
        /// Gets or sets information about vaccinated by vaccine type for last 24 hours.
        /// </summary>
        public TotalVaccineByType24ServiceModel TotalVaccineByType24 { get; set; }
    }
}
