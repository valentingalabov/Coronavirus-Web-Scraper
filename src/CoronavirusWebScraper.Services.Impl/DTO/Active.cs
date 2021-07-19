using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.DTO
{
    public class Active
    {
        [BsonElement("current")]
        public int Curent { get; set; }

        [BsonElement("current_by_type")]
        public ActiveTypes CurrentByType { get; set; }

    }
}