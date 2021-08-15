namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold information about confirmed cases total and for last 24
    /// hours for Medical staff.
    /// </summary>
    public class MedicalViewModel
    {
        /// <summary>
        /// Gets or sets count of total confirmed cases for Medical staff.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets counts of total confirmed cases by types of Medical staff.
        /// </summary>
        public MedicalTypesViewModel TotalByType { get; set; }

        /// <summary>
        /// Gets or sets count of confirmed cases for Medical staff for last 24 hours.
        /// </summary>
        public int Last24 { get; set; }

        /// <summary>
        /// Gets or sets counts of confirmed cases by types of Medical staff for last 24 hours.
        /// </summary>
        public MedicalTypesViewModel LastByType24 { get; set; }
    }
}
