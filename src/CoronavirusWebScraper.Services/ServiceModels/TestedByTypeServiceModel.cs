namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold information about tested by type.
    /// </summary>
    public class TestedByTypeServiceModel
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
