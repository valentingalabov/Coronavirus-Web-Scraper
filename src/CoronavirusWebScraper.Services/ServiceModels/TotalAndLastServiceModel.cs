namespace CoronavirusWebScraper.Services.ServiceModels
{
    using MongoDB.Bson.Serialization.Attributes;

    public class TotalAndLastServiceModel
    {
        [BsonElement("total")]
        public int Total { get; set; }

        [BsonElement("last")]
        public int Last { get; set; }
    }
}
