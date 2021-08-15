namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information about tested by type.
    /// </summary>
    public class TestedByType
    {
        /// <summary>
        /// Gets or sets count of pcr test.
        /// </summary>
        [BsonElement("pcr")]
        public int PCR { get; set; }

        /// <summary>
        /// Gets or sets count of antigen test.
        /// </summary>
        [BsonElement("antigen")]
        public int Antigen { get; set; }
    }
}
