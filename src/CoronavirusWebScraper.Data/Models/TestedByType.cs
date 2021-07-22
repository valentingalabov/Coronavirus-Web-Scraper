using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class TestedByType
    {
        [BsonElement("pcr")]
        public int PCR { get; set; }
        
        [BsonElement("antigen")]
        public int Antigen { get; set; }
    }
}
