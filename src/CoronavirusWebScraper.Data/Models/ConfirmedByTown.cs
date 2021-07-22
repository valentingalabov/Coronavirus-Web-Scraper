using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class ConfirmedByTown
    {
        [BsonElement("total")]
        public int Total { get; set; }

        [BsonElement("last")]
        public int Last { get; set; }
    }
}