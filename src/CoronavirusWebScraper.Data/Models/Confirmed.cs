namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information abaut confimed covid19 cases.
    /// </summary>
    public class Confirmed
    {
        /// <summary>
        /// Gets or sets information abaut total confirmed cases.
        /// </summary>
        [BsonElement("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets information abaut total confirmed cases by their type.
        /// </summary>
        [BsonElement("total_by_type")]
        public TestedByType TotalByType { get; set; }

        /// <summary>
        /// Gets or sets information abaut total confirmed cases for last 24 hours.
        /// </summary>
        [BsonElement("last")]
        public int Last24 { get; set; }

        /// <summary>
        /// Gets or sets information abaut last 24 hours confirmed cases by their type.
        /// </summary>
        [BsonElement("last_by_type")]
        public TestedByType TotalByType24 { get; set; }

        /// <summary>
        /// Gets or sets information abaut confirmed cases for Medical staff.
        /// </summary>
        [BsonElement("medical")]
        public Medical Medical { get; set; }
    }
}
