namespace CoronavirusWebScraper.Data.Models
{
    using CoronavirusWebScraper.Data.Attributes;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold statistical data about spread, treatment and the fight against COVID-19.
    /// </summary>
    [BsonCollection("CovidStatistics")]
    public class CovidStatistics : Document
    {
        /// <summary>
        /// Gets or sets date and time to which the data relate in format ISO 8601 and current time zone.
        /// </summary>
        [BsonElement("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets date and time when the data was retrieved in format ISO 8601.
        /// </summary>
        [BsonElement("date_scraped")]
        public string ScrapedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the country to which the data relate.
        /// </summary>
        [BsonElement("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets get or sets overall statistical information about covid19.
        /// </summary>
        [BsonElement("overall")]
        public Overall Overall { get; set; }

        /// <summary>
        /// Gets or sets statistical information about covid19 for all regions in current country.
        /// </summary>
        [BsonElement("regions")]
        public BsonDocument Regions { get; set; }

        /// <summary>
        /// Gets or sets percentage information about covid19.
        /// </summary>
        [BsonElement("stats")]
        public Stats Stats { get; set; }

        /// <summary>
        /// Gets or sets information about the results of non-compliance checks.
        /// </summary>
        [BsonElement("condition-result")]
        public BsonDocument ConditionResult { get; set; }
    }
}
