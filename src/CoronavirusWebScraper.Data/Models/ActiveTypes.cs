using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class ActiveTypes
    {
        [BsonElement("hospitalized")]
        public int Hospitalized { get; set; }

        [BsonElement("icu")]
        public int Icu { get; set; }
    }
}