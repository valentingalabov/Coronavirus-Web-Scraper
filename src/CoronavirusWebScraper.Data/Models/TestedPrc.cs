namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold percentage information abaut tested.
    /// </summary>
    public class TestedPrc
    {
        /// <summary>
        /// Gets or Sets percentage of total test by their types.
        /// </summary>
        [BsonElement("total_by_type_prc")]
        public PcrAntigenPrc TotalByTyprPrc { get; set; }

        /// <summary>
        /// Gets or Sets percentage of test by their types for last 24 hours.
        /// </summary>
        [BsonElement("last_by_type_prc")]
        public PcrAntigenPrc LastByTypePrc { get; set; }
    }
}
