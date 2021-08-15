namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold information about count of tested cases.
    /// </summary>
    public class TestedServiceModel
    {
        /// <summary>
        /// Gets or sets count of total tests.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets information about count of tests by type.
        /// </summary>
        public TestedByTypeServiceModel TotalByType { get; set; }

        /// <summary>
        /// Gets or sets count of tests for last 24 hours.
        /// </summary>
        public int Last24 { get; set; }

        /// <summary>
        /// Gets or sets information about count of tests by type for last 24 hours.
        /// </summary>
        public TestedByTypeServiceModel TotalByType24 { get; set; }
    }
}
