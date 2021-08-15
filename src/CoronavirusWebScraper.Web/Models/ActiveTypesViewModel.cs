namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold count of currently infected people by type of care.
    /// </summary>
    public class ActiveTypesViewModel
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
