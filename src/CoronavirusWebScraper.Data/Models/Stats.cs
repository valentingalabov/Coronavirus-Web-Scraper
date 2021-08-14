namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold percentage infromation abaut tested, confirmed and active.
    /// </summary>
    public class Stats
    {
        /// <summary>
        /// Gets or sets percentage information abaut tested.
        /// </summary>
        [BsonElement("tested")]
        public TestedPrc TestedPrc { get; set; }

        /// <summary>
        /// Gets or sets percentage information abaut confirmed.
        /// </summary>
        [BsonElement("confirmed")]
        public ConfirmedPrc ConfirmedPrc { get; set; }

        /// <summary>
        /// Gets or sets percentage information abaut active.
        /// </summary>
        [BsonElement("active")]
        public ActivePrc Active { get; set; }
    }
}
