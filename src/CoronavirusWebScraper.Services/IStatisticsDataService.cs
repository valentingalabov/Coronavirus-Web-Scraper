namespace CoronavirusWebScraper.Services
{
    using System.Collections.Generic;

    using CoronavirusWebScraper.Services.ServiceModels;

    /// <summary>
    /// Defines methods for reading covid statistical data.
    /// </summary>
    public interface IStatisticsDataService
    {
        /// <summary>
        /// Get all dates in given month and year with statistical information about covid.
        /// </summary>
        /// <param name="year">Year to serch dates.</param>
        /// <param name="month">Month to serch dates.</param>
        /// <returns>Return collection of dates as string for which have statistical data.</returns>
        IEnumerable<string> GetAllDatesForSpecificMonthAndYear(string year, string month);

        /// <summary>
        /// Get statistical information for specific date.
        /// </summary>
        /// <param name="date">the date for which to find information.</param>
        /// <returns>Statistical information about the maching day or null if doesn't find information.</returns>
        CovidStatisticServiceModel GetStatisticsForSpecificDay(string date);

        /// <summary>
        /// Get statistical information for analisys for the last day.
        /// </summary>
        /// <returns>Analysis information for last day.</returns>
        AnalysisServiceModel GetAnalysisDataForLastDay();
    }
}
