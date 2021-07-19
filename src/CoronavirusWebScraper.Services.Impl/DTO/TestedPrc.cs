using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.DTO
{
    public class TestedPrc
    {
        [BsonElement("total_by_type_prc")]
        public PcrAntigenPrc TotalByTyprPrc { get; set; }

        [BsonElement("last_by_type_prc")]
        public PcrAntigenPrc LastByTypePrc { get; set; }
    }
}