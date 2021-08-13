namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold count of currently infected people by type.
    /// </summary>
    public class ActiveTypes
    {
        /// <summary>
        /// Gets or sets count of current hospitalized people.
        /// </summary>
        [BsonElement("hospitalized")]
        public int Hospitalized { get; set; }

        /// <summary>
        /// Gets or sets count of current people int the intensive care unit.
        /// </summary>
        [BsonElement("icu")]
        public int Icu { get; set; }
    }
}
