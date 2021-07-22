using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class Active
    {
        [BsonElement("current")]
        public int Curent { get; set; }

        [BsonElement("current_by_type")]
        public ActiveTypes CurrentByType { get; set; }

    }
}