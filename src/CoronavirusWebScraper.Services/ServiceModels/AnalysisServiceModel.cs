namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold statistical information for analysis.
    /// </summary>
    public class AnalysisServiceModel
    {
        /// <summary>
        /// Gets or sets date and time to which the data relate.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets count of current infected people.
        /// </summary>
        public int Active { get; set; }

        /// <summary>
        /// Gets or sets count of hospitalized people.
        /// </summary>
        public int Hospitalized { get; set; }

        /// <summary>
        /// Gets or sets count of people in intensive care unit.
        /// </summary>
        public int Icu { get; set; }

        /// <summary>
        /// Gets or sets count of total confirmed cases.
        /// </summary>
        public int Confirmed { get; set; }

        /// <summary>
        /// Gets or sets coun of total tests.
        /// </summary>
        public int TotalTests { get; set; }

        /// <summary>
        /// Gets or sets count of total recovered people.
        /// </summary>
        public int TotalRecovered { get; set; }

        /// <summary>
        /// Gets or sets count of confirmed cases for Medical staff by type.
        /// </summary>
        public MedicalAnalysisServiceModel TotalMedicalAnalisys { get; set; }
    }
}
