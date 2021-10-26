namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information about confirmed covid19 cases.
    /// </summary>
    public class Confirmed
    {
        /// <summary>
        /// Gets or sets count of total confirmed cases.
        /// </summary>
        [BsonElement("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets information about total confirmed cases by their type.
        /// </summary>
        [BsonElement("total_by_type")]
        public TestedByType TotalByType { get; set; }

        /// <summary>
        /// Gets or sets count of confirmed cases for last 24 hours.
        /// </summary>
        [BsonElement("last")]
        public int Last24 { get; set; }

        /// <summary>
        /// Gets or sets information about last 24 hours confirmed cases by their type.
        /// </summary>
        [BsonElement("last_by_type")]
        public TestedByType TotalByType24 { get; set; }

        /// <summary>
        /// Gets or sets information about confirmed cases for Medical staff.
        /// </summary>
        [BsonElement("medical")]
        public Medical Medical { get; set; }
    }
}
