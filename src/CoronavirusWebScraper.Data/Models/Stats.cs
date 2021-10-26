namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold percentage information about tested, confirmed and active.
    /// </summary>
    public class Stats
    {
        /// <summary>
        /// Gets or sets percentage information about tested.
        /// </summary>
        [BsonElement("tested")]
        public TestedPrc TestedPrc { get; set; }

        /// <summary>
        /// Gets or sets percentage information about confirmed.
        /// </summary>
        [BsonElement("confirmed")]
        public ConfirmedPrc ConfirmedPrc { get; set; }

        /// <summary>
        /// Gets or sets percentage information about active.
        /// </summary>
        [BsonElement("active")]
        public ActivePrc Active { get; set; }
    }
}
