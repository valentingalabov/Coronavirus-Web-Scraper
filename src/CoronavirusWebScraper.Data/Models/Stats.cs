using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class Stats
    {
        [BsonElement("tested")]
        public TestedPrc TestedPrc { get; set; }

        [BsonElement("confirmed")]
        public ConfirmedPrc ConfirmedPrc { get; set; }

        [BsonElement("active")]
        public ActivePrc Active { get; set; }
    }
}