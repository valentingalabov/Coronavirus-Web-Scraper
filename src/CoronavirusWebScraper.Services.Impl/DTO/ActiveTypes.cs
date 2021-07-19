using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.DTO
{
    public class ActiveTypes
    {
        [BsonElement("hospitalized")]
        public int Hospitalized { get; set; }

        [BsonElement("icu")]
        public int Icu { get; set; }
    }
}