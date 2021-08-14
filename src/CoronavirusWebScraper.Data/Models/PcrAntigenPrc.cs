namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold percentage information about tests by their types.
    /// </summary>
    public class PcrAntigenPrc
    {
        /// <summary>
        /// Gets or sets percentage of pcr tests by all tests.
        /// </summary>
        [BsonElement("pcr")]
        public double PCR { get; set; }

        /// <summary>
        /// Gets or sets percentage of antigen by tests.
        /// </summary>
        [BsonElement("antigen")]
        public double Antigen { get; set; }
    }
}
