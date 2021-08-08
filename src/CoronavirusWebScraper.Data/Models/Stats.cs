namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

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
