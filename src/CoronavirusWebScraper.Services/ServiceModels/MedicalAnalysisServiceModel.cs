namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold information about confirmed cases for types of Medical staff.
    /// </summary>
    public class MedicalAnalysisServiceModel
    {
        /// <summary>
        /// Gets or sets the value of confirmed doctors.
        /// </summary>
        public int Doctror { get; set; }

        /// <summary>
        /// Gets or sets the value of confirmed nurces.
        /// </summary>
        public int Nurces { get; set; }

        /// <summary>
        /// Gets or sets the value of confirmed paramedics1.
        /// </summary>
        public int Paramedics_1 { get; set; }

        /// <summary>
        /// Gets or sets the value of confirmed paramedics2.
        /// </summary>
        public int Paramedics_2 { get; set; }

        /// <summary>
        /// Gets or sets the value of others medical staff.
        /// </summary>
        public int Others { get; set; }
    }
}
