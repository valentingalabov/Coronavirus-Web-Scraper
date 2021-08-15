namespace CoronavirusWebScraper.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Hold statistical data about spread, treatment and the fight against COVID-19.
    /// </summary>
    public class CovidStatisticViewModel
    {
        /// <summary>
        /// Gets or sets date and time to which the data relate in format ISO 8601 and current time zone.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets values about tested people.
        /// </summary>
        public TestedViewModel Tested { get; set; }

        /// <summary>
        /// Gets or sets values about confimed cases.
        /// </summary>
        public ConfirmedViewModel Confirmed { get; set; }

        /// <summary>
        /// Gets or sets values about currently infected people.
        /// </summary>
        public ActiveViewModel Active { get; set; }

        /// <summary>
        /// Gets or sets values about recovered cases.
        /// </summary>
        public TotalAndLastViewModel Recovered { get; set; }

        /// <summary>
        /// Gets or sets values about deceased cases.
        /// </summary>
        public TotalAndLastViewModel Deceased { get; set; }

        /// <summary>
        /// Gets or sets values about vaccinated cases.
        /// </summary>
        public VaccinatedViewModel Vaccinated { get; set; }

        /// <summary>
        /// Gets or sets values about region.
        /// </summary>
        public IEnumerable<RegionsViewModel> Regions { get; set; }

        /// <summary>
        /// Gets or sets values for vaccinated by vaccine type.
        /// </summary>
        public TotalVaccineByType24ViewModel TotalVaccineByType24 { get; set; }
    }
}
