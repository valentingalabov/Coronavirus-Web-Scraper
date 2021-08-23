namespace CoronavirusWebScraper.Services.Impl
{
    /// <summary>
    /// Static class containing global constants for service implementations.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Url containing stsatistical infromation about covid19.
        /// </summary>
        public const string CovidUrl = "https://coronavirus.bg/";

        /// <summary>
        /// DateTime iso 8601 format with current time zone.
        /// </summary>
        public const string DateTimeFormatISO8601WithTimeZone = "yyyy-MM-ddTHH\\:mm\\:sszzz";

        /// <summary>
        /// DateTime iso 8601 format.
        /// </summary>
        public const string DateTimeFormatISO8601 = "yyyy-MM-ddTHH:mm:ssZ";

        /// <summary>
        /// DatiTime format containing only year and month.
        /// </summary>
        public const string DateTimeYearAndMonthFormat = "yyyy-MM";
    }
}
