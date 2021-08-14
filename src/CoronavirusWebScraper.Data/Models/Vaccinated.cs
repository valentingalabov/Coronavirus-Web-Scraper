namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information about vaccinated cases.
    /// </summary>
    public class Vaccinated
    {
        /// <summary>
        /// Gets or sets count of total vaccinated.
        /// </summary>
        [BsonElement("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated for last 24 hours.
        /// </summary>
        [BsonElement("last")]
        public int Last { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated by vaccine type.
        /// </summary>
        [BsonElement("last_by_type")]
        public VaccineType LastByType { get; set; }

        /// <summary>
        /// Gets or sets count of total fully vaccinated.
        /// </summary>
        [BsonElement("total_completed")]
        public int TotalCompleted { get; set; }
    }
}
