namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold information about tested by type.
    /// </summary>
    public class TestedByTypeViewModel
    {
        /// <summary>
        /// Gets or sets count of tested by pcr.
        /// </summary>
        public int PCR { get; set; }

        /// <summary>
        /// Gets or sets count of tested by antigen.
        /// </summary>
        public int Antigen { get; set; }
    }
}
