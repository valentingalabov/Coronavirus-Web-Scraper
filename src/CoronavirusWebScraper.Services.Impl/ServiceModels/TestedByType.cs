using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.ServiceModels
{
    public class TestedByType
    {
        [BsonElement("pcr")]
        public int PCR { get; set; }
        
        [BsonElement("antigen")]
        public int Antigen { get; set; }
    }
}
