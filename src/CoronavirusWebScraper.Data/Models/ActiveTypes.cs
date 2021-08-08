namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class ActiveTypes
    {
        [BsonElement("hospitalized")]
        public int Hospitalized { get; set; }

        [BsonElement("icu")]
        public int Icu { get; set; }
    }
}
