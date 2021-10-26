namespace CoronavirusWebScraper.Services.Impl
{
    /// <summary>
    /// Static class containing global constants for service implementations.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// URL containing statistical information about covid19.
        /// </summary>
        public const string CovidUrl = "https://coronavirus.bg/";

        /// <summary>
        /// DateTime iso8601 format with current time zone.
        /// </summary>
        public const string DateTimeFormatISO8601WithTimeZone = "yyyy-MM-ddTHH\\:mm\\:sszzz";

        /// <summary>
        /// DateTime iso8601 format.
        /// </summary>
        public const string DateTimeFormatISO8601 = "yyyy-MM-ddTHH:mm:ssZ";

        /// <summary>
        /// DatiTime format containing only year and month.
        /// </summary>
        public const string DateTimeYearAndMonthFormat = "yyyy-MM";

        /// <summary>
        /// DateTime format containing hour and minutes with date.
        /// </summary>
        public const string TimeAndDateFormat = "HH:mm,dd MMM yyy";

        /// <summary>
        /// DateTime format containing only date.
        /// </summary>
        public const string DateFormat = "dd MMM yyy";
    }
}
