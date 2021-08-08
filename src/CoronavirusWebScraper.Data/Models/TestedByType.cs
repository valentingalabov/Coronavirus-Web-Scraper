namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class TestedByType
    {
        [BsonElement("pcr")]
        public int PCR { get; set; }

        [BsonElement("antigen")]
        public int Antigen { get; set; }
    }
}
