using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.DTO
{
    public class Tested
    {
        [BsonElement("total")]
        public int Total { get; set; }

        [BsonElement("total_by_type")]
        public TestedByType TotalByType { get; set; }

        [BsonElement("last")]
        public int Last24 { get; set; }

        [BsonElement("last_by_type")]
        public TestedByType TotalByType24 { get; set; }
    }
}