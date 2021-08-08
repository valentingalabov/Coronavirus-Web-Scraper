namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Active
    {
        [BsonElement("current")]
        public int Curent { get; set; }

        [BsonElement("current_by_type")]
        public ActiveTypes CurrentByType { get; set; }
    }
}
