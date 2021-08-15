namespace CoronavirusWebScraper.Web.Models
{
    /// <summary>
    /// Hold information about total and last 24 hours count.
    /// </summary>
    public class TotalAndLastViewModel
    {
        /// <summary>
        /// Gets or sets total count.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets count for last 24 hours.
        /// </summary>
        public int Last { get; set; }
    }
}
