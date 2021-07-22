using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class TotalAndLast
    {
        [BsonElement("total")]
        public int Total { get; set; }

        [BsonElement("last")]
        public int Last { get; set; }
    }
}