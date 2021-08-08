namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Vaccinated
    {
        [BsonElement("total")]
        public int Total { get; set; }

        [BsonElement("last")]
        public int Last { get; set; }

        [BsonElement("last_by_type")]
        public VaccineType LastByType { get; set; }

        [BsonElement("total_completed")]
        public int TotalCompleted { get; set; }
    }
}
