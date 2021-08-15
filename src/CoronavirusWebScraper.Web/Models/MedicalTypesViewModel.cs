namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold information about infected medical staff by their type.
    /// </summary>
    public class MedicalTypesViewModel
    {
        /// <summary>
        /// Gets or sets count of confirmed doctors.
        /// </summary>
        public int Doctors { get; set; }

        /// <summary>
        /// Gets or sets count of confirmed nurces.
        /// </summary>
        public int Nurces { get; set; }

        /// <summary>
        /// Gets or sets count of confirmed paramedics1.
        /// </summary>
        public int Paramedics_1 { get; set; }

        /// <summary>
        /// Gets or sets count of confirmed paramedics2.
        /// </summary>
        public int Paramedics_2 { get; set; }

        /// <summary>
        /// Gets or sets count of others medical staff.
        /// </summary>
        public int Other { get; set; }
    }
}
