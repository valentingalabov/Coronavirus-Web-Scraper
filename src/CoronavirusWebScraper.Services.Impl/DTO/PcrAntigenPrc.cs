using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.DTO
{
    public class PcrAntigenPrc
    {
        [BsonElement("pcr")]
        public double PCR { get; set; }

        [BsonElement("antigen")]
        public double Antigen { get; set; }
    }
}