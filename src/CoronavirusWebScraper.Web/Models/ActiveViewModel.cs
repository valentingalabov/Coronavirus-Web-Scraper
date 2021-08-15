namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold currently infected by covid19.
    /// </summary>
    public class ActiveViewModel
    {
        /// <summary>
        /// Gets or sets count of current infected people.
        /// </summary>
        public int Curent { get; set; }

        /// <summary>
        /// Gets or sets count of current infected people by type of care.
        /// </summary>
        public ActiveTypesViewModel CurrentByType { get; set; }
    }
}
