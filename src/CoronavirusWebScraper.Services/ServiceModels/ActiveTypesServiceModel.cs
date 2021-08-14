namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold count of currently infected people by type of care.
    /// </summary>
    public class ActiveTypesServiceModel
    {
        /// <summary>
        /// Gets or sets count of current hospitalized people.
        /// </summary>
        public int Hospitalized { get; set; }

        /// <summary>
        /// Gets or sets count of current people int the intensive care unit.
        /// </summary>
        public int Icu { get; set; }
    }
}
