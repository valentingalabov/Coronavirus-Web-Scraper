namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold information about confimed covid19 cases.
    /// </summary>
    public class ConfirmedServiceModel
    {
        /// <summary>
        /// Gets or sets count of total confirmed cases.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets information about total confirmed cases by their type.
        /// </summary>
        public TestedByTypeServiceModel TotalByType { get; set; }

        /// <summary>
        /// Gets or sets count of  confirmed cases for last 24 hours.
        /// </summary>
        public int Last24 { get; set; }

        /// <summary>
        /// Gets or sets information about last 24 hours confirmed cases by their type.
        /// </summary>
        public TestedByTypeServiceModel TotalByType24 { get; set; }

        /// <summary>
        /// Gets or sets information about confirmed cases for Medical staff.
        /// </summary>
        public MedicalServiceModel Medical { get; set; }
    }
}
