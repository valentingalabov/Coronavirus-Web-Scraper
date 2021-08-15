namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information about count of tested cases.
    /// </summary>
    public class Tested
    {
        /// <summary>
        /// Gets or sets count of total tests.
        /// </summary>
        [BsonElement("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets count of total tests by their type.
        /// </summary>
        [BsonElement("total_by_type")]
        public TestedByType TotalByType { get; set; }

        /// <summary>
        /// Gets or sets count of total tests for last 24 
        /// s.
        /// </summary>
        [BsonElement("last")]
        public int Last24 { get; set; }

        /// <summary>
        /// Gets or sets value of total tests by their type for last 24 hours.
        /// </summary>
        [BsonElement("last_by_type")]
        public TestedByType TotalByType24 { get; set; }
    }
}
