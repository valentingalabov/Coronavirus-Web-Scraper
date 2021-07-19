using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.DTO
{
    public class TestedByType
    {
        [BsonElement("pcr")]
        public int PCR { get; set; }
        
        [BsonElement("antigen")]
        public int Antigen { get; set; }
    }
}
